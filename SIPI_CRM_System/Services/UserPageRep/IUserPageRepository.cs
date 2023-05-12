using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.UserPageRep
{
	public interface IUserPageRepository
	{
		Employee GetEmployeeById(int id);

        Employee GetEmployeeByLogin(string login);

        void UpdateEmployee(Employee employee);

		IEnumerable<Position> GetPositions();
	}
}

