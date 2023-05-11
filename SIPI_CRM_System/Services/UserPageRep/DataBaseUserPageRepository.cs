using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.UserPageRep
{
	public class DataBaseUserPageRepository : IUserPageRepository
	{
		private readonly CRMdbContext _context;

		public DataBaseUserPageRepository(CRMdbContext context)
		{
			_context = context;
		}


	}
}

