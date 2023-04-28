using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.LoginPageRep
{
	public class DataBaseLoginPageRepository : ILoginPageRepository
	{
		private readonly CRMdbContext _context;

		public DataBaseLoginPageRepository(CRMdbContext context)
		{
            _context = context;
		}

        public bool CheckAuthorization(string login, string password)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.Login == login);
            if ( employee != default && employee.Password == password )
                return true;
            else
                return false;
        }

        public Employee GetEmployee(string login)
        {
            return _context.Employees.FirstOrDefault(x => x.Login == login);
        }
    }
}

