using AspNetCoreGeneratedDocument;
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
		public async  Task<List<Municipality>> GetAllAsync()
		{
			var result = await _context.Municipality.ToListAsync();
			return result;
		}
		public async Task<Guid> GetCategoryID(string category)
		{
			var cat =await _context.Municipality.FirstOrDefaultAsync(x => x.MunicipalityName == category);
			return cat.Id;
		}
		public async Task<bool> RemoveMuncipality(Municipality municipality)
		{
			_context.Municipality.Remove(municipality);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<bool> UpdateMunicipality(Municipality category)
		{
			 _context.Municipality.Update(category);
			return await _context.SaveChangesAsync() > 0;
		}
		public async  Task<Municipality> GetMunicipalityById(Guid id)
		{
			return await _context.Municipality.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
