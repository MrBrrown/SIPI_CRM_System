using System;
using Microsoft.EntityFrameworkCore;
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

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Id == id);
        }

        public Employee GetEmployeeByLogin(string login)
        {
            return _context.Employees.FirstOrDefault(x => x.Login == login);
        }

        public IEnumerable<Position> GetPositions()
        {
            return _context.Positions;
        }

        public async void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}

