using TraceNest.Models;

namespace TraceNest.Repository.CategoryRepositories
{
	public interface ICategoryRepository
	{
		List<Category> GetAllAsync();
		Task<bool> AddCategoryAsync(Category category);
		Guid GetCategoryID(string category);
	}
}
