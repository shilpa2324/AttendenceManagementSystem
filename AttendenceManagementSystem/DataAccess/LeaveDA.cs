using AttendenceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AttendenceManagementSystem.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaveDA
    {
        SqlConnection connection = null;
        readonly string connString = "Data Source=NEUDESI-BP74JHF\\SQLEXPRESS;Initial Catalog=attendanceManagement; Integrated Security=SSPI;";
        public LeaveDA()
        {
            connection = new SqlConnection(connString);
        }
        
        public List<Leave> GetLeaveDetail(int id)
        {
            List<Leave> leaveList = new List<Leave>();
            connection.Open();
            SqlCommand com = new SqlCommand("sp_leaveHistory", connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            if (dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     Leave leave = new Leave();
                     leave.startDate = Convert.ToDateTime(dr["startDate"]);
                     String s = String.Format("{0:d/M/yyyy}", leave.startDate);
                     leave.endDate = Convert.ToDateTime(dr["endDate"]);
                     String s1 = String.Format("{0:d/M/yyyy}", leave.endDate);
                     leave.reason = Convert.ToString(dr["reason"]);
                     leave.status = Convert.ToString(dr["status"]);
                     leave.count = Convert.ToInt32(dr["count"]);
                     leaveList.Add(leave);
                 }
             }
            return leaveList;
        }
        public int AddLeave(Leave leave)
        {           
            SqlCommand cmd = new SqlCommand("sp_InsertLeave", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", leave.startDate);
            cmd.Parameters.AddWithValue("@endDate", leave.endDate);
            cmd.Parameters.AddWithValue("@count", leave.count);
            cmd.Parameters.AddWithValue("@reason", leave.reason);
            cmd.Parameters.AddWithValue("@EmpID", leave.EmpID);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
        public List<Employee> GetApproveLeaveDetail(int managerId)
        {                        
            List<Employee> employeeList = new List<Employee>();
            try
            {
                connection.Open();
                SqlCommand com = new SqlCommand("sp_approveLeaveDisplay", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@id", managerId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                connection.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee employee = new Employee();
                        employee.Name = Convert.ToString(dr["EmpName"]);
                        employee.ManagerID = Convert.ToInt32(dr["managerID"]);
                        Leave leave = new Leave();
                        leave.LeaveID = Convert.ToInt32(dr["LeaveID"]);
                        leave.startDate = Convert.ToDateTime(dr["startDate"]);
                        leave.endDate = Convert.ToDateTime(dr["endDate"]);
                        leave.reason = Convert.ToString(dr["reason"]);
                        leave.status = Convert.ToString(dr["status"]);
                        leave.count = Convert.ToInt32(dr["count"] == DBNull.Value ? "0" : dr["count"]);
                        List<Leave> leaveList = new List<Leave>();
                        leaveList.Add(leave);
                        employee.Listofleave = leaveList;
                        employeeList.Add(employee);

                    }
                }
            }
            catch (Exception)
            {
                employeeList = null; 
            }
            finally
            {
                if(null!= connection)
                    connection.Close();
            }
            return employeeList;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="leaveId"></param>
       /// <param name="empid"></param>
       /// <returns></returns>
        public int UpdateLeave(int leaveId,int empid)
        {
            int id = empid;           
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_approveLeave", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", leaveId);
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return id;
        }
    }
}
