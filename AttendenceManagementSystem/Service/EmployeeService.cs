using AttendenceManagementSystem.DataAccess;
using AttendenceManagementSystem.Models;
using System.Collections.Generic;

namespace AttendenceManagementSystem.Service
{
    public interface IEmployeeService
    {
        int AddEmployee(Employee emp);
        Employee GetEmployeeById(int employeeId);
        int EditEmployee(Employee emp);
        List<Employee> DisplayEmployeeDetails();
        int DeleteEmployee(Employee emp);
        Employee ValidateEmployee(LoginDetails loginDetails);

    }
    public class EmployeeService : IEmployeeService
    {
        EmployeeDA employeeDA = new EmployeeDA();
        public int AddEmployee(Employee emp)
        {
            return employeeDA.InsertEmployee(emp);
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return employeeDA.GetEmployeeByID(employeeId);
        }
        public int EditEmployee(Employee emp)
        {
            return employeeDA.UpdateEmployee(emp);
        }
        public List<Employee> DisplayEmployeeDetails()
        {
            return employeeDA.GetEmployeeDetails();
        }
        public int DeleteEmployee(Employee emp)
        {
            return employeeDA.Delete(emp);
        }
        public Employee ValidateEmployee(LoginDetails loginDetails)
        {
            Employee emp = employeeDA.ValidateEmployee(loginDetails);
            return emp;
        }
    }
}
