﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using TraceNest.Data;
using TraceNest.Models;

namespace TraceNest.Repository.LostRepositories
{
	public class LostRepository: ILostRepository
	{
		private readonly AppDbContext _context;
		public LostRepository(AppDbContext context)
		{
			_context = context;
		}
		public bool AddAsync(Lost lost)
		{
			_context.Add(lost);
			_context.SaveChanges();
			return true;
		}
		public List<Lost> GetAll()
		{
			var losts = _context.Losts.Include(c => c.Municipality).Include(y=>y.Category).Where(x => x.Status == "Pending").ToList();
			return losts;
		}
		public List<Lost> GetPostBySpecificUser(Guid userid)
		{
			var losts = _context.Losts.Include(c => c.Municipality).Include(y => y.Category).Where(x=>x.UserId==userid).ToList();
			return losts;
		}
		public bool Update(Lost lost)
		{
			_context.Losts.Update(lost);
			_context.SaveChanges();
			return true;
		}
		public bool Delete(Lost lost)
		{
			_context.Losts.Remove(lost);
			_context.SaveChanges();
			return true;
		}
	}
}
