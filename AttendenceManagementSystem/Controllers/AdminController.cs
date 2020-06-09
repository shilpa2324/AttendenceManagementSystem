using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace AttendenceManagementSystem.Controllers
{
    public class AdminController : Controller
    {       
        
        EmployeeService employeeService = new EmployeeService();
        LeaveService leaveService = new LeaveService();

        /// <summary>
        /// Display the details of all the employees
        /// </summary>
        /// <returns>It returns the employee list</returns>
        public IActionResult Display()
        {
            try
            {
                var employeeList = employeeService.DisplayEmployeeDetails();
                return View(employeeList);
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while Displaying Employee Details";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }            
        }
        /// <summary>
        /// Add a new Employee to the application
        /// </summary>
        /// <returns>It display the Employee details with the new data</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeService.AddEmployee(emp);
                    return Redirect("Display");
                }
                return View();
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while adding new employee";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
        }
        /// <summary>
        /// To edit the employee details
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>It display the Employee details with the edited data</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var emp = employeeService.GetEmployeeById(id);
                return View(emp);
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while accessing Employee by Id";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }           
        }
        [HttpPost]
        public IActionResult Edit(int id,Employee emp)
        {
            try
            {               
                emp.EmpID = id;
                employeeService.EditEmployee(emp);
                return RedirectToAction("Display");                    
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while Editing Employee details";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }          
        }
        /// <summary>
        /// To delete an employee from the application
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>It display the Employee details without the deleted data</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var emp = employeeService.GetEmployeeById(id);
                return View(emp);
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while accessing Employee by Id";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
            
        }
        [HttpPost]
        public IActionResult Delete(int id,Employee emp)
        {
            try
            {
                emp.EmpID = id;
                employeeService.DeleteEmployee(emp);
                return RedirectToAction("Display");
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while Deleting Employee";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
           
        }
        /// <summary>
        /// Leave details of a perticular employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Leave list</returns>
        public IActionResult Detail(int id)
        {
            try
            {
                List<Leave> leaveList = leaveService.LeaveHistory(id);
                return View(leaveList);
            }
            catch (Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while accessing LeaveHistory";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }

        }
    }
}

