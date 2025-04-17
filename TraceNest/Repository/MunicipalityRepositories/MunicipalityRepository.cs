using Microsoft.EntityFrameworkCore;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.MunicipalityRepositories
{
	public class MunicipalityRepository: IMunicipalityRepository
	{
		private readonly AppDbContext _context;
		public MunicipalityRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddAsync(Municipality municipality)
		{
			await _context.Municipality.AddAsync(municipality);
			await _context.SaveChangesAsync();
			return true;
		}
		public List<Municipality> GetAllAsync()
		{
			var result =  _context.Municipality.ToList();
			return result;
		}
		public async Task<Guid> GetCategoryID(string category)
		{
			var cat =await _context.Municipality.FirstOrDefaultAsync(x => x.MunicipalityName == category);
			return cat.Id;
		}
	}
}
