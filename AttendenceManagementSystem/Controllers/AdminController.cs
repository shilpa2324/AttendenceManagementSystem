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

