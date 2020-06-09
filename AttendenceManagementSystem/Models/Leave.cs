using System;
using System.ComponentModel.DataAnnotations;

namespace AttendenceManagementSystem.Models
{
    public class Leave
    {        
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime endDate { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public int count { get; set; }
        public int EmpID { get; set; }
        public int LeaveID { get; set; }

    }
}
