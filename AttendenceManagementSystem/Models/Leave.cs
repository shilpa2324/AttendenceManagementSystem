using System;
using System.ComponentModel.DataAnnotations;

namespace AttendenceManagementSystem.Models
{
    public class Leave
    {   [Required]    
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Reason { get; set; }

        public string Status { get; set; }
        public int Count { get; set; }
        public int EmpID { get; set; }
        public int LeaveID { get; set; }
       
    }
}
