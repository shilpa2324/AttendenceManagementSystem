using AttendenceManagementSystem.DataAccess;
using AttendenceManagementSystem.Models;
using System;
using System.Collections.Generic;
namespace AttendenceManagementSystem.Service
{
    public class LeaveService
    {
        LeaveDA leaveDA = new LeaveDA();
        public List<Leave> LeaveHistory(int employeeId)
        {
            return leaveDA.GetLeaveDetail(employeeId);
        }
        public int AddLeaveDetails(Leave leave)
        {
            return leaveDA.AddLeave(leave);
        }
        public List<Employee> DisplayApproveLeave(int managerId)
        {
            return leaveDA.GetApproveLeaveDetail(managerId);
        }
        public int ApproveLeave(int leaveId,int id)
        {
            return leaveDA.UpdateLeave(leaveId,id);
        }

    }
}
