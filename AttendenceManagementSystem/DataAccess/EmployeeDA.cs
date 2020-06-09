using AttendenceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AttendenceManagementSystem.DataAccess
{
    public class EmployeeDA
    {
        SqlConnection connection = null;
        readonly string connString = "Data Source=NEUDESI-BP74JHF\\SQLEXPRESS;Initial Catalog=attendanceManagement; Integrated Security=SSPI;";
        public EmployeeDA()
        {
            connection = new SqlConnection(connString);
        }
        public int InsertEmployee(Employee emp)
        {           
            SqlCommand cmd = new SqlCommand("SP_InsertEmployee", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpName", emp.Name);
            cmd.Parameters.AddWithValue("@MailID", emp.MailID);
            cmd.Parameters.AddWithValue("@EmpType", emp.category);
            cmd.Parameters.AddWithValue("@UserName", emp.Username);
            cmd.Parameters.AddWithValue("@Password", emp.Password);

            connection.Open();
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }
        public Employee GetEmployeeByID(int id)
        {
            Employee emp = new Employee();           
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_getEmployee", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    emp = new Employee();
                    emp.EmpID = Convert.ToInt32(dr["EmpID"]);
                    emp.Name = Convert.ToString(dr["EmpName"]);
                    emp.MailID = Convert.ToString(dr["MailID"]);
                    emp.category = Convert.ToString(dr["EmpType"]);
                    emp.leaves_taken = Convert.ToInt32(dr["leaveCount"] == DBNull.Value ? "0" : dr["leaveCount"]);
                }
            }
            return emp;
        }
        public int UpdateEmployee(Employee emp)
        {            
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
            connection.Open();
            SqlCommand com = new SqlCommand("sp_details", connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Employee emp = new Employee();
                    emp.EmpID = Convert.ToInt32(dr["EmpId"]);
                    emp.Name = Convert.ToString(dr["EmpName"]);
                    emp.MailID = Convert.ToString(dr["MailID"]);
                    emp.category = Convert.ToString(dr["EmpType"]);
                    emp.leaves_taken = Convert.ToInt32(dr["leaveCount"] == DBNull.Value ? "0" : dr["leaveCount"]);
                    employees.Add(emp);
                }
            }
            return employees;
        }
        public int Delete(Employee emp)
        {          
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_delete", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", emp.EmpID);
            int row = cmd.ExecuteNonQuery();
            connection.Close();
            return row;
        }

        public Employee ValidateEmployee(LoginDetails loginDetails)
        {
            Employee emp = new Employee();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_validation", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@username", loginDetails.Username);
            cmd.Parameters.AddWithValue("@password", loginDetails.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    emp.EmpID = Convert.ToInt32(dr["EmpID"]);
                    emp.Name = Convert.ToString(dr["EmpName"]);
                    emp.MailID = Convert.ToString(dr["MailID"]);
                    emp.category = Convert.ToString(dr["EmpType"]);
                }
            }
            return emp;
        }
    }
}
