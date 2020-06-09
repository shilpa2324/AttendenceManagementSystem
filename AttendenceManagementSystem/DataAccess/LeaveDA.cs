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
        readonly string connString = "Data Source=SKOLIYOTTU01\\SQLEXPRESS;Initial Catalog=attendanceManagement; Integrated Security=SSPI;";
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
                     leave.StartDate = Convert.ToDateTime(dr["StartDate"]);
                     String s = String.Format("{0:d/M/yyyy}", leave.StartDate);
                     leave.EndDate = Convert.ToDateTime(dr["EndDate"]);
                     String s1 = String.Format("{0:d/M/yyyy}", leave.EndDate);
                     leave.Reason = Convert.ToString(dr["Reason"]);
                     leave.Status = Convert.ToString(dr["Status"]);
                     leave.Count = Convert.ToInt32(dr["Count"]);
                     leave.LeaveID= Convert.ToInt32(dr["LeaveID"]);
                    leaveList.Add(leave);
                 }
             }
            return leaveList;
        }
        public int AddLeave(Leave leave)
        {           
            SqlCommand cmd = new SqlCommand("sp_InsertLeave", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", leave.StartDate);
            cmd.Parameters.AddWithValue("@endDate", leave.EndDate);
            cmd.Parameters.AddWithValue("@count", leave.Count);
            cmd.Parameters.AddWithValue("@reason", leave.Reason);
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
                        leave.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        leave.EndDate = Convert.ToDateTime(dr["EndDate"]);
                        leave.Reason = Convert.ToString(dr["Reason"]);
                        leave.Status = Convert.ToString(dr["Status"]);
                        leave.Count = Convert.ToInt32(dr["Count"] == DBNull.Value ? "0" : dr["Count"]);
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
        public int DeleteLeave(int leaveId)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_deleteLeave", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", leaveId);
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
        public List<string> GetHolidayList()
        {
            List<string> holidays = new List<string>();

            connection.Open();

            SqlCommand command = new SqlCommand("select holiday from HolidayList", connection);

            using (SqlDataReader reader = command.ExecuteReader())


            {
                while (reader.Read())
                {
                    holidays.Add(Convert.ToString(reader["Holiday"]));
                   
                }
            }

            connection.Close();
            return holidays;

            
        }
    }

    }

