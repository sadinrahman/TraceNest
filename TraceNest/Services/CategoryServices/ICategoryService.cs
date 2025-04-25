using TraceNest.Dto;
using TraceNest.Models;

namespace TraceNest.Services.CategoryServices
{
	public interface ICategoryService
	{
		Task<List<Category>> GetAll();
		Task<Guid> AddCategoryAsync(string category);
		Task<bool> RemoveCategory(Guid id);
		Task<bool> UpdateMunicipality(CategoryDto category);
		Task<int> CategoryCount();
	}
}
