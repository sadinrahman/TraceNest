using TraceNest.Repository.FoundRepositories;

namespace TraceNest.Services.FoundServices
{
	public class FoundService: IFoundService
	{
		private readonly IFoundRepository _repo;
		public FoundService(IFoundRepository repo)
		{
			_repo = repo;
		}
		//public bool PostFound()
		//{

		//}
	}
}
