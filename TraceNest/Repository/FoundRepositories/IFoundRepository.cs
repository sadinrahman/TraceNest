﻿using TraceNest.Models;

namespace TraceNest.Repository.FoundRepositories
{
	public interface IFoundRepository
	{
		bool Add(Found found);
		List<Found> GetAll();
		List<Found> GetPostBySpecificUser(Guid userid);
		bool Update(Found found);
		bool Delete(Found found);
	}
}
