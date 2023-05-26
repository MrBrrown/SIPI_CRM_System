using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.AdminPanelRep;

public class DataBaseAdminPanelRepository : IAdminPanelRepository
{
    private readonly CRMdbContext _context;
    
    public DataBaseAdminPanelRepository(CRMdbContext context)
    {
        _context = context;
    }

    public void AddTable(Table table)
    {
        _context.Tables.Add(table);
        _context.SaveChanges();
    }

    public void AddPosition(Position position)
    {
        _context.Positions.Add(position);
        _context.SaveChanges();
    }

    public void AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void UpdateEmployeeRightsByLogin(string login)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Login == login);

        if (employee != null) 
            employee.IsAdmin = true;

        _context.SaveChanges();
    }

    public void DeleteEmployeeByLogin(string login)
    {
        var employee = _context.Employees.FirstOrDefault(x => x.Login == login);
        if (employee != null) 
            _context.Employees.Remove(employee);
        
        _context.SaveChanges();
    }

    public IEnumerable<Table> GetTables()
    {
        return _context.Tables;
    }

    public IEnumerable<Position> GetPositions()
    {
        return _context.Positions;
    }
    
    public IEnumerable<Employee> GetEmployees()
    {
        return _context.Employees;
    }
}