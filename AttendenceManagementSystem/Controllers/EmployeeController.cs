using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AttendenceManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeService employeeService = new EmployeeService();
        LeaveService leaveService = new LeaveService();
        public IActionResult Index(int id)
        {
            try
            {
                var emp = employeeService.GetEmployeeById(id);
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
        public IActionResult LeaveHistory(int id)
        {
            try
            {
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
        [HttpGet]
        public IActionResult ApplyLeave(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult ApplyLeave(int id, Leave leave)
        {
            try
            {
                leave.EmpID = id;
                leave.count = leave.endDate.Subtract(leave.startDate).Days + 1;
                leaveService.AddLeaveDetails(leave);
                return RedirectToAction("Index", "Employee", new { id = leave.EmpID });
            }
            catch (Exception e)
            {

                ErrorViewModel errorView = new ErrorViewModel();
                errorView.Message = "Error Occured while Applying Leave";
                errorView.Reason = e.Message;
                return RedirectToAction("Error", "ErrorHandling", errorView);
            }

        }
        public IActionResult ApproveLeave(int id)
        {
            try
            {
                List<Employee> employeeList = leaveService.DisplayApproveLeave(id);
                if (employeeList != null)
                {
                    return View(employeeList);
                }
                else
                {
                    return RedirectToAction("Index", "Employee", new { id });
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
        public IActionResult Approve(int leaveId, int empid)
        {
            try
            {
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
    }
}