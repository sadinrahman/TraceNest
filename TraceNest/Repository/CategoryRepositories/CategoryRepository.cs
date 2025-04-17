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
		public List<Category> GetAllAsync()
		{
			return _context.Categories.ToList();
		}
		public async Task<bool> AddCategoryAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			return await _context.SaveChangesAsync() > 0;
		}
		public  Guid GetCategoryID(string category)
		{
			var cat = _context.Categories.FirstOrDefault(x => x.CategoryName == category);
			return cat.Id;
		}
	}
}
