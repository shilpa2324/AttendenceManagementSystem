using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AttendenceManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        //EmployeeService employeeService = new EmployeeService();
        //LeaveService leaveService = new LeaveService();

        /// <summary>
        /// First page for the user after login
        /// </summary>
        /// <returns>Employee name and leave count</returns>
        public IActionResult Index()
        {
            try
            {
                Logger.Information("loggin starts");
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                ViewBag.login = 1;
                var emp = _employeeService.GetEmployeeById(id);
                return View(emp);
            }
            catch (Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while accessing Employee by Id";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }

        }
        /// <summary>
        /// Display the Leave history of the loged in employee
        /// </summary>
        /// <returns>Employee list </returns>
        public IActionResult LeaveHistory()
        {
            try
            {
                Logger.Information("loggin starts");
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                ViewBag.login = 1;
                LeaveService leaveService = new LeaveService();
                List<Leave> leaveList = leaveService.LeaveHistory(id);
                List<Employee> employeeList = new List<Employee>();
                Employee employee = new Employee();
                employee.Listofleave = leaveList;
                employee.EmpID = id;
                employeeList.Add(employee);
                return View(employeeList);
            }
            catch (Exception e)
            {               
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while accessing Leave History";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
        }
        /// <summary>
        /// Employee can apply leaves
        /// </summary>
        /// <returns>Leave details</returns>
        [HttpGet]
        public IActionResult ApplyLeave()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            ViewBag.login = 1;
            return View();
        }
        [HttpPost]
        public IActionResult ApplyLeave(Leave leave)
        {
            bool isSuccess = false;

            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                ViewBag.login = 1;
                leave.EmpID = id;
                leave.Count = leave.EndDate.Subtract(leave.StartDate).Days;
                if (leave.Count >= 0)
                {
                    int weekendCountCheck = CheckIfWeekendsAreThere(leave);
                    leave.Count = (leave.Count - weekendCountCheck) + 1;
                }

                if (leave.Count == 0)
                {
                    ViewBag.holidays = 1;
                    return View();
                }
                if (leave.Count < 0)
                {
                    ViewBag.errorEntry = 1;
                    return View();
                }

                else
                {
                    LeaveService leaveService = new LeaveService();
                    int returnValue = leaveService.AddLeaveDetails(leave);
                    if (returnValue == 1)
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ViewBag.IsSuccess = isSuccess;
                    }
                }

                return View();
            }
            catch (Exception e)
            {

                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while Applying Leave";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }

        }
        /// <summary>
        ///  Display the leaves of the employees which have to be approved
        /// </summary>
        /// <returns>Employee list</returns>
        public IActionResult ApproveLeave()
        {
            try
            {
                int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

                ViewBag.login = 1;

                LeaveService leaveService = new LeaveService();
                List<Employee> employeeList = leaveService.DisplayApproveLeave(id);
                if (employeeList != null)
                {
                    return View(employeeList);
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }

            }
            catch (Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while displaying approve leave";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }
        }
        /// <summary>
        /// Manager can approve the submitted leaves
        /// </summary>
        /// <param name="leaveId">id of leave</param>
        /// <returns>Leave details</returns>
        public IActionResult Approve(int leaveId)
        {
            try
            {
                int empid = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

                ViewBag.login = 1;

                LeaveService leaveService = new LeaveService();
                int id = leaveService.ApproveLeave(leaveId, empid);
                return RedirectToAction("Index", "Employee", new { id });
            }
            catch (Exception e)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while approving leave";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }

        }
        /// <summary>
        /// Cancel an applied leave of an employee
        /// </summary>
        /// <param name="leaveId">id of leave</param>
        /// <returns>Leave history without the canceled data</returns>
        public IActionResult DeleteLeave(int leaveId)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            LeaveService leaveService = new LeaveService();
            leaveService.DeleteLeave(leaveId);
            return RedirectToAction("LeaveHistory", "Employee");
        }
        /// <summary>
        /// Method to calculate the week ends and holidays count 
        /// </summary>
        /// <param name="leave"></param>
        /// <returns>The  week end count</returns>
        #region Private Methods

        private static int CheckIfWeekendsAreThere(Leave leave)
        {
            DateTime startDateVal = leave.StartDate;
            DateTime endDateVal = leave.EndDate;
            int weekendCountCheck = 0;
            List<string> weekendVal = new List<string>() { "saturday", "sunday" };
            LeaveService leaveService = new LeaveService();
            List<string> holiday = leaveService.GetHolidayList();
            while (startDateVal <= endDateVal)
            {
                if (weekendVal.Contains(Enum.GetName(typeof(DayOfWeek), startDateVal.DayOfWeek).ToLower()) ||
                    holiday.Contains(startDateVal.ToString("MM/dd/yyyy")))
                {
                    weekendCountCheck += 1;
                }
                startDateVal = startDateVal.AddDays(1);
            }
            return weekendCountCheck;
        }
        #endregion
    }
}