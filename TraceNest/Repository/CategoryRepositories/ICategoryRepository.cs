using TraceNest.Models;

namespace TraceNest.Repository.CategoryRepositories
{
	public interface ICategoryRepository
	{
		List<Category> GetAllAsync();
	}
}
