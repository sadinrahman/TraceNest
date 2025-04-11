using TraceNest.Dto;
using TraceNest.Helper.CloudinaryHelper;
using TraceNest.Models;
using TraceNest.Repository.LostRepositories;

namespace TraceNest.Services.LostServices
{
	public class LostService: ILostService
	{
		private readonly ILostRepository _repo;
		private readonly ICloudinaryService _service;
		public LostService(ILostRepository repo, ICloudinaryService service)
		{
			_repo = repo;
			_service = service;
		}
		public bool PostLostProduct(LostAddDto dto,Guid userid,IFormFile Image) 
		{
			var photo = _service.UploadImage(Image);
			Console.WriteLine("qwerty");
			var res = new Lost
			{
				Title = dto.Title,
				Description = dto.Description,
				LostDate = dto.LostDate,
				ImageUrl = photo,
				UserId = userid,
				MunicipalityId = dto.MunicipalityId,
				CategoryId = dto.CategoryId
			};
			Console.WriteLine("fbvhjhbfdcvjhbjhbjhbbjhuy");
			var result =_repo.AddAsync(res);
			if(!result)
			{
				return false;
			}
			return true;
		}
		public List<LostItemDto> GetAll()
		{
			var res = _repo.GetAll();
			var result = new List<LostItemDto>();
			foreach (var item in res)
			{
				result.Add(new LostItemDto
				{

					Title = item.Title,
					Description = item.Description,
					LostDate = item.LostDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName
				});
			}
			return result;
		}
	}
}
