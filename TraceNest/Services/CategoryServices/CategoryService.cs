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
	}
}
