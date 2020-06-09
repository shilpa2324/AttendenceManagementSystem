using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AttendenceManagementSystem.Models
{
    public class User
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MailID { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }       
    }
    public class Employee : User
    {   
        public int leaves_taken { get; set; }
        public List<Leave> Listofleave { get; set; }
        public int LeaveApply { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
       
    }
    public class Manager : Employee
    {
        public int LeaveApprove { get; set; }
    }
    public class Admin : User
    {
        public int AddEmployee { get; set; }
    }

}
