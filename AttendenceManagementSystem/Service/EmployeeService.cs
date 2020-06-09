using AttendenceManagementSystem.DataAccess;
using AttendenceManagementSystem.Models;
using System.Collections.Generic;

namespace AttendenceManagementSystem.Service
{
    public class EmployeeService
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
