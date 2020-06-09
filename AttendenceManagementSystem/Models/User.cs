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
        public string Category { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }       
    }
    public class Employee : User
    {   
        public int Leaves_taken { get; set; }
        public List<Leave> Listofleave { get; set; }
        public int EmpID { get; set; }
        public int ManagerID { get; set; }
       
    }
   

}
