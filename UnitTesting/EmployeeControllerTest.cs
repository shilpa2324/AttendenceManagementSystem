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
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;
using System.Web;

namespace UnitTesting
{
    class EmployeeControllerTest
    {
        [Test]
        public void GetEmployeeByIdTest()
        {
            // Act           
            IEmployeeService _employeeService = new MockEmployeeService();
            var result = _employeeService.GetEmployeeById(2);
            // Assertion
            Assert.IsNotNull(result);
        }
        [Test]
        public void AddEmployeeTest()
        {
            //Act
            IEmployeeService _employeeService = new MockEmployeeService();
            var result = _employeeService.GetEmployeeById(2);
            // Assertion
            Assert.IsNotNull(result);
        }
        
        
    }
}