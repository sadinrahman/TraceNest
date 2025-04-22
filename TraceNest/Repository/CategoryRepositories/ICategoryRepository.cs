using TraceNest.Models;

namespace TraceNest.Repository.CategoryRepositories
{
	public interface ICategoryRepository
	{
		Task<List<Category>> GetAllAsync();
		Task<bool> AddCategoryAsync(Category category);
		Task<Guid> GetCategoryID(string category);
		Task<bool> RemoveCategory(Category category);
		Task<bool> UpdateCategory(Category category);
		Task<Category> GetCategoryById(Guid id);
	}
}
