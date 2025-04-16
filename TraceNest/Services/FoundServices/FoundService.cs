using TraceNest.Dto;
using TraceNest.Helper.CloudinaryHelper;
using TraceNest.Models;
using TraceNest.Repository.FoundRepositories;

namespace TraceNest.Services.FoundServices
{
	public class FoundService: IFoundService
	{
		private readonly IFoundRepository _repo;
		private readonly ICloudinaryService _service;
		public FoundService(IFoundRepository repo ,ICloudinaryService service)
		{
			_repo = repo;
			_service = service;
		}
		public bool PostFoundProduct(FoundPostDto dto, Guid userid, IFormFile Image)
		{
			var photo = _service.UploadImage(Image);
			var res = new Found
			{
				Title = dto.Title,
				Description = dto.Description,
				FoundDate = dto.FoundDate,
				ImageUrl = photo,
				UserId = userid,
				MunicipalityId = dto.MunicipalityId,
				CategoryId = dto.CategoryId
			};
			var result = _repo.Add(res);
			if (!result)
			{
				return false;
			}
			return true;
		}
		public List<FoundItemDto> GetAll()
		{
			var res = _repo.GetAll();
			var result = new List<FoundItemDto>();
			foreach (var item in res)
			{
				result.Add(new FoundItemDto
				{

					Title = item.Title,
					Description = item.Description,
					FoundDate = item.FoundDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName
				});
			}
			return result;
		}
	}
}
