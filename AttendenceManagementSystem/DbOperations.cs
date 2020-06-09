using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AttendenceManagementSystem.Models;


namespace AttendenceManagementSystem.DataAccess
{
    public class DbOperations
    {
        
        readonly string connString = "Data Source=NEUDESI-BP74JHF\\SQLEXPRESS;Initial Catalog=attendanceManagement; Integrated Security=SSPI;";
        public int InsertEmployee(Employee emp)
        {

            SqlConnection connection = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand("SP_InsertEmployee", connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpName", emp.Name);
            cmd.Parameters.AddWithValue("@MailID", emp.MailID);
            cmd.Parameters.AddWithValue("@EmpType", emp.category);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();

            return 1;
        }
        public Employee GetEmployeeByID(int id)
        {
            Employee emp = new Employee();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_getEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                emp = new Employee();
                emp.EmpID= Convert.ToInt32(dr["EmpID"]);
                emp.Name = Convert.ToString(dr["EmpName"]);
                emp.MailID = Convert.ToString(dr["MailID"]);
                emp.category = Convert.ToString(dr["EmpType"]);
                emp.leaves_taken= Convert.ToInt32(dr["leaveCount"] == DBNull.Value ? "0" : dr["leaveCount"]);
            }
            return emp;
        }
        public int UpdateEmployee(Employee emp)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_update", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpName", emp.Name);
            cmd.Parameters.AddWithValue("@MailID", emp.MailID);
            cmd.Parameters.AddWithValue("@EmpType", emp.category);
            cmd.Parameters.AddWithValue("@id", emp.EmpID);
            int row = cmd.ExecuteNonQuery();
            connection.Close();

            return row;
        }
        public List<Employee> GetEmployeeDetails()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand com = new SqlCommand("sp_details", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Employee emp = new Employee();
                emp.EmpID = Convert.ToInt32(dr["EmpId"]);
                emp.Name = Convert.ToString(dr["EmpName"]);
                emp.MailID = Convert.ToString(dr["MailID"]);
                emp.category = Convert.ToString(dr["EmpType"]);
                emp.leaves_taken= Convert.ToInt32(dr["leaveCount"]==DBNull.Value ? "0" : dr["leaveCount"]);
                employees.Add(emp);
            }
            return employees;
        }

        public int Delete(Employee emp)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_delete", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", emp.EmpID);
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
        public List<Leave> GetLeaveDetail(int id)
        {
            List<Leave> leaveList = new List<Leave>();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand com = new SqlCommand("sp_leaveHistory", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id",id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Leave leave = new Leave();
                leave.startDate = Convert.ToDateTime(dr["startDate"]);
                leave.endDate = Convert.ToDateTime(dr["endDate"]);
                leave.reason = Convert.ToString(dr["reason"]);
                leave.status = Convert.ToString(dr["status"]);
                leave.count = Convert.ToInt32(dr["count"]);               
                leaveList.Add(leave);
            }
            return leaveList;
        }
        public int AddLeave(Leave leave)
        {

            SqlConnection connection = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand("sp_InsertLeave", connection);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate",leave.startDate);
            cmd.Parameters.AddWithValue("@endDate", leave.endDate);
            cmd.Parameters.AddWithValue("@count", leave.count);
            cmd.Parameters.AddWithValue("@reason", leave.reason);
            cmd.Parameters.AddWithValue("@EmpID", leave.EmpID);
            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();

            return 1;
        }
        public List<Employee> GetApproveLeaveDetail(int managerId)
        {
            List<Employee> employeeList = new List<Employee>();
            List<Leave> leaveList = new List<Leave>();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand com = new SqlCommand("sp_approveLeaveDisplay", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", managerId);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {               
                Employee employee = new Employee();
                employee.Name= Convert.ToString(dr["EmpName"]);           
                Leave leave = new Leave();
                leave.LeaveID = Convert.ToInt32(dr["LeaveID"]);
                leave.startDate = Convert.ToDateTime(dr["startDate"]);
                leave.endDate = Convert.ToDateTime(dr["endDate"]);
                leave.reason = Convert.ToString(dr["reason"]);
                leave.status = Convert.ToString(dr["status"]);
                leave.count = Convert.ToInt32(dr["count"]==DBNull.Value ? "0":dr["count"]);                
                leaveList.Add(leave);
                employee.Listofleave = leaveList;
                employeeList.Add(employee);
            }
            return employeeList;

        }
        public int UpdateLeave(int leaveId)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_approveLeave", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", leaveId);
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }


    }
}
