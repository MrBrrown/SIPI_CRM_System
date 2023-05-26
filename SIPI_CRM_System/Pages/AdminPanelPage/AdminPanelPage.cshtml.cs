using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.AdminPanelRep;

namespace SIPI_CRM_System.Pages.AdminPanelPage;

public class AdminPanelPage : PageModel
{
    private static string redirectUserString = "";
    private readonly IAdminPanelRepository _repository;
    
    public IEnumerable<Table> Tables { get; set; }
    public IEnumerable<Position> Positions { get; set; }
    public IEnumerable<Employee> Employees { get; set; }

    public AdminPanelPage(IAdminPanelRepository repository)
    {
        _repository = repository;
    }
    
    public IActionResult OnPostAddTable()
    {
        Tables = _repository.GetTables();
        
        try
        {
            Table table = new Table
            {
                Id = Tables.Any() ? Tables.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Places = int.Parse(Request.Form["Places"]),
            };

            _repository.AddTable(table);

            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString + "&incorrectFormat=True");
        }
    }
    
    public IActionResult OnPostAddPosition()
    {
        Positions = _repository.GetPositions();
        
        try
        {
            Position position = new Position
            {
                Id = Positions.Any() ? Positions.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Name = Request.Form["Name"]
            };

            _repository.AddPosition(position);

            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString + "&incorrectFormat=True");
        }
    }

    public IActionResult OnPostAddEmployee()
    {
        Employees = _repository.GetEmployees();
        Positions = _repository.GetPositions();
        
        try
        {
            Position position = new Position
            {
                Id = Positions.Any() ? Positions.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Name = Request.Form["Position"]
            };

            _repository.AddPosition(position);
            
            Employee employee = new Employee
            {
                Id = Employees.Any() ? Employees.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Name = Request.Form["Name"],
                Login = Request.Form["Login"],
                Password = Request.Form["Password"],
                IsAdmin = Request.Form["IsAdmin"] == "on",
                PositionId = position.Id
            };

            _repository.AddEmployee(employee);

            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString + "&incorrectFormat=True");
        }
    }

    public IActionResult OnPostDeleteEmployee()
    {
        try
        {
            string login = Request.Form["Login"];

            _repository.DeleteEmployeeByLogin(login);

            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString + "&incorrectFormat=True");
        }
    }
    
    public IActionResult OnPostUpdateRights()
    {
        try
        {
            string login = Request.Form["Login"];

            _repository.UpdateEmployeeRightsByLogin(login);

            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString);
        }
        catch (Exception e)
        {
            return Redirect("/AdminPanelPage/AdminPanelPage" + redirectUserString + "&incorrectFormat=True");
        }
    }
    
    
    public void OnGet()
    {
        redirectUserString = "?id=" + Request.Query["id"] + "&isadmin=" + Request.Query["isadmin"];
    }
}