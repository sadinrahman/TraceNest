using System.Threading.Tasks;
using TraceNest.Dto;
using TraceNest.Models;
using TraceNest.Repository.CategoryRepositories;

namespace TraceNest.Services.CategoryServices
{
	public class CategoryService: ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository= categoryRepository;
		}
		public  async Task<List<Category>> GetAll()
		{
			var res =await _categoryRepository.GetAllAsync();
			return res;
		}
		public async Task<Guid> AddCategoryAsync(string category)
		{
			var cat = new Category
			{
				CategoryName = category
			};
			var res = await _categoryRepository.AddCategoryAsync(cat);
			var catId =await _categoryRepository.GetCategoryID(category);
			return catId;
		}
		public async Task<bool> RemoveCategory(Guid id)
		{
			var municipality =await _categoryRepository.GetCategoryById(id);
			if (municipality == null)
			{
				return false;
			}
			return await _categoryRepository.RemoveCategory(municipality);
		}
		public async Task<bool> UpdateMunicipality(CategoryDto category)
		{
			var res =await _categoryRepository.GetCategoryById(category.Id);
			if (res == null)
			{
				return false;
			}
			res.CategoryName = category.Name;
			return await _categoryRepository.UpdateCategory(res);
		}
		public async Task<int> CategoryCount()
		{
			var res = await _categoryRepository.GetAllAsync();
			return res.Count();
		}
	}
}
