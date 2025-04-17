using TraceNest.Models;

namespace TraceNest.Services.CategoryServices
{
	public interface ICategoryService
	{
		List<Category> GetAll();
		Guid AddCategoryAsync(string category);
	}
}
