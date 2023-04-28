using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.LoginPageRep
{
	public interface ILoginPageRepository
	{
		bool CheckAuthorization(string login, string password);

		Employee GetEmployee(string login);
	}
}

