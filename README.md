Overview:
TraceNest is a modern Lost and Found tracking system built using ASP.NET Core MVC, designed to help individuals and communities efficiently report, track, and recover lost or found items. The platform serves as a digital hub where users can register lost items, report found ones, and collaborate to reunite belongings with their rightful owners.

Key Features:

🔐 User Authentication:

Custom registration and login.

Google OAuth 2.0 integration for easy sign-in.

Password visibility toggle and confirm password validation.

📍 Location-Aware Tracking:

Items are associated with municipalities for better local discovery.

📦 Item Management:

Separate modules for Lost and Found item submissions.

Categorization of items (e.g., electronics, documents, personal belongings).

Ability to upload images using Cloudinary for better identification.

🗂 Admin and Service Layers:

Clean service-based architecture managing categories, municipalities, and item records.

Role-based access for managing listings and overseeing reported items.

🎨 User-Friendly Interface:

Responsive UI designed with Bootstrap 5.

Clear navigation for searching and filtering items based on categories or locations.

Tech Stack:

Backend: ASP.NET Core MVC (C#)

Frontend: Razor Pages + Bootstrap 5

Authentication: Identity + Google OAuth

Image Uploads: Cloudinary

Database: SQL Server (Code-First approach)

Architecture: Layered (Controllers, Services, Repositories)

Purpose:
TraceNest aims to bridge the gap between people who’ve lost valuables and those who find them, leveraging community cooperation and technology to create a more organized, reliable, and fast way to reunite people with their belongings.
