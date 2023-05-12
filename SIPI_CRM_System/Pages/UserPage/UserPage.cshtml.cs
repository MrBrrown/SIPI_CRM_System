using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.UserPageRep;

namespace SIPI_CRM_System.Pages.UserPage
{
	public class UserPageModel : PageModel
    {
        private static string redirectUserString = ""; //Необоходимо задавать на каждой станице в методе  OnGet(), по нему происходит сохранение действующего пользователя
        private readonly IUserPageRepository _context;

        public Employee employee;
        public IEnumerable<Position> positions;

        public UserPageModel(IUserPageRepository context)
        {
            _context = context;
        }

        public IActionResult OnPostUpdate(int id)
        {
            var employee = _context.GetEmployeeById(id);

            var name = Request.Form["Name"];
            var login = Request.Form["Login"];
            var password = Request.Form["Password"];
            var pos = Request.Form["Position"];

            if (name != "" && name != employee.Name)
            {
                employee.Name = name;
            }

            if (login != "" && login != employee.Login)
            {
                if (_context.GetEmployeeByLogin(login) == default)
                {
                    employee.Login = login;
                }
                else
                {
                    return Redirect("/UserPage/UserPage" + redirectUserString + "&loginduplicate=True");
                }
            }

            if (password != "")
            {
                if ( password == Request.Form["ConfirmPassword"])
                {
                    employee.Password = password;
                }
                else
                {
                    return Redirect("/UserPage/UserPage" + redirectUserString + "&passwordvalidate=True");
                }
            }

            _context.UpdateEmployee(employee);

            return Redirect("/UserPage/UserPage" + redirectUserString);
        }

        public IActionResult OnPostExit()
        {
            return Redirect("/Index");
        }

        public void OnGet()
        {
            employee = _context.GetEmployeeById(Int32.Parse(Request.Query["id"]));
            positions = _context.GetPositions();
            redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"]; //Выражение для сохраниения пользователя, одинаково на каждой станице
        }
    }
}
