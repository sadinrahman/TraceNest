using TraceNest.Dto;
using TraceNest.Helper.CloudinaryHelper;
using TraceNest.Models;
using TraceNest.Repository.FoundRepositories;
using TraceNest.Services.ProfileServices;

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
					Id = item.Id,
					Title = item.Title,
					Description = item.Description,
					FoundDate = item.FoundDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName,
					Category = item.Category.CategoryName

				});
			}
			return result;
		}
		public List<FoundItemDto> GetByUser(Guid userid)
		{
			var res = _repo.GetPostBySpecificUser(userid);
			var result = new List<FoundItemDto>();
			foreach (var item in res)
			{
				result.Add(new FoundItemDto
				{
					Id = item.Id,
					Title = item.Title,
					Description = item.Description,
					FoundDate = item.FoundDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName,
					Category = item.Category.CategoryName,
					Status = item.Status
				});
			}
			return result;
		}
		public bool UpdateFound(UpdateFoundDto found, Guid userid)
		{
			var post = _repo.GetPostBySpecificUser(userid).FirstOrDefault(x => x.Id == found.Id);
			if(post == null)
			{
				return false;
			}
			post.Title = found.Title ?? post.Title;
			post.Description = found.Description ?? post.Description;
			post.MunicipalityId = found.Municipality ?? post.MunicipalityId;
			post.CategoryId = found.Category ?? post.CategoryId;
			post.FoundDate = found.FoundDate;
			post.Status = found.Status ?? post.Status;
			var result = _repo.Update(post);
			return result;
		}
		public bool DeleteLost(Guid id, Guid userid)
		{
			var res = _repo.GetPostBySpecificUser(userid).FirstOrDefault(x => x.Id == id);
			if (res == null)
			{
				return false;
			}
			var result = _repo.Delete(res);
			if (!result)
			{
				return false;
			}
			return true;
		}
	}
}
