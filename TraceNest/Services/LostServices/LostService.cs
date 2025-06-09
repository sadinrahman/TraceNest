using System.Threading.Tasks;
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
		public async Task<bool> PostLostProduct(LostAddDto dto,Guid userid,IFormFile Image) 
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
			var result =await _repo.AddAsync(res);
			if(!result)
			{
				return false;
			}
			return true;
		}
		public async Task<List<LostItemDto>> GetAll()
		{
			var res =await _repo.GetAll();
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
					Category = item.Category.CategoryName,
					userid = item.UserId
				});
			}
			return result;
		}
		public async Task<List<LostItemDto>> GetPostBySpecificUser(Guid userid)
		{
			var res =await _repo.GetPostBySpecificUser(userid);
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
		public async Task<bool> UpdateLost(UpdateLostDto found, Guid userid)
		{
			var posts =await _repo.GetPostBySpecificUser(userid);
			var post = posts.FirstOrDefault(x => x.Id == found.Id);
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
			var result =await _repo.Update(post);
			return result;
		}
		public async Task<bool> DeleteLost(Guid id, Guid userid)
		{
			var ress = await _repo.GetPostBySpecificUser(userid);
			var res = ress.FirstOrDefault(x => x.Id == id);
			if (res == null)
			{
				return false;
			}
			var result =await _repo.Delete(res);
			if (!result)
			{
				return false;
			}
			return true;
		}
		public async Task<int> LostCount()
		{
			var res=await _repo.GetAllLosted();
			if (res != null)
			{
				return res.Count;
			}
			else
			{
				return 0;
			}
		}
	}
}
