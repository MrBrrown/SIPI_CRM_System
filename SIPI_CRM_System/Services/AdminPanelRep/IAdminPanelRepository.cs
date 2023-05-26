using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.AdminPanelRep;

public interface IAdminPanelRepository
{
    void AddTable(Table table);
    
    void AddPosition(Position position);
    
    void AddEmployee(Employee employee);
    
    void UpdateEmployeeRightsByLogin(string login);
    
    void DeleteEmployeeByLogin(string login);
    
    IEnumerable<Table> GetTables();
    
    IEnumerable<Position> GetPositions();
    
    IEnumerable<Employee> GetEmployees();
}