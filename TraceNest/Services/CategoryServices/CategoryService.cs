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
		public  List<Category> GetAll()
		{
			var res = _categoryRepository.GetAllAsync();
			return res;
		}
		public  Guid AddCategoryAsync(string category)
		{
			var cat = new Category
			{
				CategoryName = category
			};
			var res =  _categoryRepository.AddCategoryAsync(cat);
			var catId = _categoryRepository.GetCategoryID(category);
			return catId;
		}
	}
}
