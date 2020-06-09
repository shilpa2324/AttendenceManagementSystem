using AttendenceManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendenceManagementSystem;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using AttendenceManagementSystem.Models;
using AttendenceManagementSystem.Service;

namespace UnitTesting
{
    class HomeControllerTest
    {
        [Test]
        public void Login()
        {
            HomeController controller = new HomeController(new MockEmployeeService());
            // Act
            ViewResult result = controller.Login() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void LoginEmployee()
        {
            HomeController controller = new HomeController(new MockEmployeeService());
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.Username = "alka";
            loginDetails.Password = "alka";
            // Act    
            var actResult = (RedirectToActionResult)controller.Login(loginDetails);
            // Assertion
            Assert.AreEqual("Index", actResult.ActionName);
        }
        [Test]
        public void LoginAdmin()
        {
            HomeController controller = new HomeController(new MockEmployeeService());
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.Username = "shilpa";
            loginDetails.Password = "789";
            // Act    
            var actResult = (RedirectToActionResult)controller.Login(loginDetails);
            // Assertion
            Assert.AreEqual("Display", actResult.ActionName);

        }
        [Test]
        public void InvalidUserLogin()
        {
            HomeController controller = new HomeController(new MockEmployeeService());
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.Username = "ram";
            loginDetails.Password = "abcd";
            // Act    
            var actResult = (ViewResult)controller.Login(loginDetails);
            // Assertion
            Assert.IsNotNull(actResult);
        }
        [Test]
        public void Logout()
        {
            HomeController controller = new HomeController(new MockEmployeeService());
            // Act
            var actResult = (RedirectToActionResult)controller.Logout();
            // Assertion
            Assert.AreEqual("Login", actResult.ActionName);
        }
    }

    class MockEmployeeService : IEmployeeService
    {
        public Employee[] employees = { new Employee { EmpID = 1, Category = "admin" ,Username="shilpa",Password="789"},
        new Employee { EmpID = 2, Category = "employee" ,Username="alka",Password="alka"},
        new Employee { EmpID = 3, Category =null, Username = "ram", Password = "abcd" } };
        public int AddEmployee(Employee emp)
        {
            return 1;
        }

        public int DeleteEmployee(Employee emp)
        {
            throw new NotImplementedException();
        }

        public List<Employee> DisplayEmployeeDetails()
        {
            throw new NotImplementedException();
        }

        public int EditEmployee(Employee emp)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return employees.FirstOrDefault(x => x.EmpID == employeeId);
        }

        public Employee ValidateEmployee(LoginDetails loginDetails)
        {
            return employees.FirstOrDefault(x => x.Username == loginDetails.Username && x.Password == loginDetails.Password);
        }
    }
}
