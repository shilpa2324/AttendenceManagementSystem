using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AttendenceManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeService _employeeService;
        public HomeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
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
                    Employee emp = new Employee();
                    //Logger.Information("loggin starts");
                    
                    emp = _employeeService.ValidateEmployee(loginDetails);
                    
                    if (emp.Category == "employee" || emp.Category=="manager")
                    {                                            
                        HttpContext.Session.SetInt32("id",emp.EmpID);
                        
                        return RedirectToAction("Index", "Employee");
                    }
                    else if (emp.Category == "admin")
                    {
                        return RedirectToAction("Display", "Admin");
                    }
                    else if(emp.Category==null)
                    {
                        ViewBag.IsSuccess = 0;
                        return View();
                    }
                }
            }
            catch(Exception e)
            {
                Logger.Error(e.ToString());
                Logger.Error("Error occured");
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while login";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
            return View();
        }
        public IActionResult Logout()
        {
            Logger.Debug("logout");
            HttpContext.Session.Remove("id");
            return RedirectToAction("Login", "Home");
        }
       
       
    }
}