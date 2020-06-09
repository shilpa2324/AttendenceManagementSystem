using AttendenceManagementSystem.DataAccess;
using AttendenceManagementSystem.Models;
using System;
using System.Collections.Generic;
namespace AttendenceManagementSystem.Service
{
    public interface ILeaveService
    {
        List<Leave> LeaveHistory(int employeeId);
        int AddLeaveDetails(Leave leave);
        List<Employee> DisplayApproveLeave(int managerId);
        int ApproveLeave(int leaveId, int id);
        int DeleteLeave(int leaveId);
        List<string> GetHolidayList();

    }
        public class LeaveService : ILeaveService
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
        public int DeleteLeave(int leaveId)
        {
            return leaveDA.DeleteLeave(leaveId);
        }
        public List<string> GetHolidayList()
        {
            return leaveDA.GetHolidayList();
        }

    }
}
