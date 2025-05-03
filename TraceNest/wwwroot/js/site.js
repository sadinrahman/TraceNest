// TraceNest Chat JavaScript
document.addEventListener('DOMContentLoaded', function () {
    // Initialize SignalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .withAutomaticReconnect()
        .build();

    // Cache DOM elements
    const messageForm = document.getElementById('message-form');
    const messageInput = document.getElementById('message-input');
    const messagesContainer = document.getElementById('chat-messages');
    const senderId = document.getElementById('sender-id');
    const receiverId = document.getElementById('receiver-id');
    const typingIndicator = document.getElementById('typing-indicator');
    const emojiButton = document.getElementById('emoji-button');

    // State variables
    let typingTimer;
    let isTyping = false;

    // Connect to SignalR hub
    function startConnection() {
        connection.start()
            .then(() => {
                console.log('Connected to SignalR hub');

                // Join user's personal group to receive messages
                if (senderId) {
                    connection.invoke('JoinUserGroup', senderId.value);
                }

                // Mark messages as read if in a conversation
                if (senderId && receiverId) {
                    markMessagesAsRead();
                }
            })
            .catch(err => {
                console.error('Error connecting to SignalR hub:', err);
                // Try to reconnect after 5 seconds
                setTimeout(startConnection, 5000);
            });
    }

    // Start the initial connection
    startConnection();

    // Handle connection closed event
    connection.onclose(() => {
        console.log('Connection closed. Attempting to reconnect...');
        setTimeout(startConnection, 5000);
    });

    // Handle incoming messages
    connection.on('ReceiveMessage', message => {
        // Only show messages relevant to this conversation
        if (receiverId && (message.senderId == receiverId.value || message.senderId == senderId.value)) {
            appendMessage(message);

            // Mark as read if we're in this conversation and the message is from the other user
            if (message.senderId == receiverId.value) {
                markMessagesAsRead();
            }

            // Play notification sound for new messages
            if (message.senderId != senderId.value) {
                playNotificationSound();
            }
        }

        // Refresh conversations list to show updated status
        refreshConversationsList();
    });

    // Handle read receipts
    connection.on('MessagesRead', userId => {
        if (receiverId && userId == receiverId.value) {
            // Update read status of messages
            document.querySelectorAll('.read-status.unread').forEach(el => {
                el.classList.remove('unread');
                el.classList.add('read');
                el.querySelector('i').classList.remove('bi-check');
                el.querySelector('i').classList.add('bi-check-all');
            });
        }
    });

    // Send message function
    function sendMessage(text) {
        if (!text.trim() || !senderId || !receiverId) return;

        const messageDto = {
            senderId: senderId.value,
            recieverId: receiverId.value,
            message: text.trim()
        };

        // Send via SignalR
        connection.invoke('SendPrivateMessage', messageDto)
            .catch(err => {
                console.error('Error sending message:', err);
                // Fallback to AJAX if SignalR fails
                sendMessageViaAjax(messageDto);
            });
    }

    // Fallback AJAX method for sending messages
    function sendMessageViaAjax(messageDto) {
        const csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

        fetch('/Messages/SendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': csrfToken
            },
            body: JSON.stringify(messageDto)
        })
            .then(response => response.json())
            .then(data => {
                if (!data.success) {
                    console.error('Failed to send message:', data.message);
                    alert('Failed to send message. Please try again.');
                }
            })
            .catch(error => {
                console.error('Error sending message:', error);
                alert('Failed to send message. Please try again.');
            });
    }

    // Add a new message to the UI
    function appendMessage(message) {
        if (!messagesContainer) return;

        const isOwnMessage = message.senderId == senderId.value;
        const now = new Date();

        // Check if we need to add a date separator
        const today = now.toLocaleDateString();
        const lastDateSeparator = messagesContainer.querySelector('.message-date-separator:last-of-type');
        const lastSeparatorDate = lastDateSeparator ?
            new Date(lastDateSeparator.querySelector('span').textContent).toLocaleDateString() : null;

        if (!lastDateSeparator || lastSeparatorDate !== today) {
            const dateSeparator = document.createElement('div');
            dateSeparator.className = 'message-date-separator';
            dateSeparator.innerHTML = `<span>${now.toLocaleDateString('en-US', {
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            })}</span>`;
            messagesContainer.appendChild(dateSeparator);
        }

        // Create message element
        const messageEl = document.createElement('div');
        messageEl.className = `message ${isOwnMessage ? 'message-own' : 'message-other'} mb-3`;

        messageEl.innerHTML = `
            <div class="message-content">
                <div class="message-text">${escapeHtml(message.message)}</div>
                <div class="message-time">
                    ${formatTime(now)}
                    ${isOwnMessage ? '<span class="read-status unread"><i class="bi bi-check"></i></span>' : ''}
                </div>
            </div>
        `;

        messagesContainer.appendChild(messageEl);
        scrollToBottom();
    }

    // Helper function to escape HTML
    function escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }

    // Format time for messages
    function formatTime(date) {
        return date.toLocaleTimeString([], { hour: 'numeric', minute: '2-digit' });
    }

    // Mark messages as read
    function markMessagesAsRead() {
        if (!senderId || !receiverId) return;

        connection.invoke('MarkAsRead', senderId.value, receiverId.value)
            .catch(err => {
                console.error('Error marking messages as read:', err);
            });
    }

    // Refresh conversations list
    function refreshConversationsList() {
        const conversationsList = document.getElementById('conversations-list');
        if (!conversationsList) return;

        fetch('/Messages/ConversationsList')
            .then(response => response.text())
            .then(html => {
                conversationsList.innerHTML = html;
            })
            .catch(err => {
                console.error('Error refreshing conversations list:', err);
            });
    }

    // Scroll chat to bottom
    function scrollToBottom() {
        if (messagesContainer) {
            messagesContainer.scrollTop = messagesContainer.scrollHeight;
        }
    }

    // Play notification sound
    function playNotificationSound() {
        const audio = new Audio('/sounds/notification.mp3');
        audio.volume = 0.5;
        audio.play().catch(err => {
            console.log('Audio playback prevented:', err);
        });
    }

    // Typing indicator functionality
    if (messageInput) {
        messageInput.addEventListener('input', () => {
            if (!isTyping) {
                isTyping = true;
                // Implement typing notification if needed via SignalR
                // connection.invoke('UserIsTyping', senderId.value, receiverId.value);
            }

            clearTimeout(typingTimer);
            typingTimer = setTimeout(() => {
                isTyping = false;
                // Implement stopped typing notification if needed
                // connection.invoke('UserStoppedTyping', senderId.value, receiverId.value);
            }, 1000);
        });
    }

    // Handle form submission
    if (messageForm) {
        messageForm.addEventListener('submit', e => {
            e.preventDefault();

            if (messageInput && messageInput.value.trim()) {
                sendMessage(messageInput.value);
                messageInput.value = '';
                messageInput.focus();
            }
        });
    }

    // Emoji picker functionality
    if (emojiButton) {
        emojiButton.addEventListener('click', () => {
            // You can implement a custom emoji picker or use a library
            // For this example, we'll just add some common emojis
            const emojis = ['😊', '👍', '❤️', '😂', '🎉', '👋', '🙏', '😎'];

            // Create or toggle emoji picker
            let emojiPicker = document.querySelector('.emoji-picker');

            if (emojiPicker) {
                emojiPicker.remove();
                return;
            }

            emojiPicker = document.createElement('div');
            emojiPicker.className = 'emoji-picker p-2 d-flex flex-wrap';

            emojis.forEach(emoji => {
                const emojiBtn = document.createElement('button');
                emojiBtn.className = 'btn btn-light m-1';
                emojiBtn.textContent = emoji;
                emojiBtn.addEventListener('click', () => {
                    messageInput.value += emoji;
                    emojiPicker.remove();
                    messageInput.focus();
                });
                emojiPicker.appendChild(emojiBtn);
            });

            document.querySelector('.card-footer').appendChild(emojiPicker);
        });
    }

    // Initial page setup
    scrollToBottom();
    refreshConversationsList();

    // Periodically refresh conversations list
    setInterval(refreshConversationsList, 30000); // Every 30 seconds