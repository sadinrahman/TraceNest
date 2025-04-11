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
	}
}
