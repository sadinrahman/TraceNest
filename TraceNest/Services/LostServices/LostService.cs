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
					Id = item.Id,
					Title = item.Title,
					Description = item.Description,
					LostDate = item.LostDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName
				});
			}
			return result;
		}
		public List<LostItemDto> GetPostBySpecificUser(Guid userid)
		{
			var res = _repo.GetPostBySpecificUser(userid);
			var result = new List<LostItemDto>();
			foreach (var item in res)
			{
				result.Add(new LostItemDto
				{
					Id = item.Id,
					Title = item.Title,
					Description = item.Description,
					LostDate = item.LostDate,
					ImageUrl = item.ImageUrl,
					Municipality = item.Municipality.MunicipalityName,
					Status = item.Status,
					Category = item.Category.CategoryName
				});
			}
			return result;
		}
		public bool UpdateLost(UpdateLostDto found, Guid userid)
		{
			var post = _repo.GetPostBySpecificUser(userid).FirstOrDefault(x => x.Id == found.Id);
			if (post == null)
			{
				return false;
			}
			post.Title = found.Title ?? post.Title;
			post.Description = found.Description ?? post.Description;
			post.MunicipalityId = found.Municipality ?? post.MunicipalityId;
			post.CategoryId = found.Category ?? post.CategoryId;
			post.LostDate = found.LostDate;
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
