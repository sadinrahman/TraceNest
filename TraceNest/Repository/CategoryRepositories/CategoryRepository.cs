using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.CategoryRepositories
{
	public class CategoryRepository: ICategoryRepository
	{
		private readonly AppDbContext _context;
		public CategoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Category>> GetAllAsync()
		{
			return await _context.Categories.ToListAsync();
		}
		public async Task<bool> AddCategoryAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<Guid> GetCategoryID(string category)
		{
			var cat =await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category);
			return cat.Id;
		}
		public async Task<bool> RemoveCategory(Category category)
		{
			_context.Categories.Remove(category);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<bool> UpdateCategory(Category category)
		{
			_context.Categories.Update(category);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<Category> GetCategoryById(Guid id)
		{
			return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
