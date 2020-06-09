using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AttendenceManagementSystem.Controllers
{
    public class HomeController : Controller
    {        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDetails loginDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeService employeeService = new EmployeeService();
                    Employee emp = employeeService.ValidateEmployee(loginDetails);

                    if (emp.category == "employee" || emp.category=="manager")
                    {
                        return RedirectToAction("Index", "Employee", new { id = emp.EmpID });
                    }
                    else if (emp.category == "admin")
                    {
                        return RedirectToAction("Display", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
            }
            catch(Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while login";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
            return View();
        }
    }
}