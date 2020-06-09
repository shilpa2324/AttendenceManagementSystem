CREATE procedure [dbo].[sp_getEmployee] @id int
as
begin
select Employee.EmpID,Employee.EmpName,Employee.EmpType,Employee.MailID,SUM(Count) as leaveCount from Employee
left join LeaveDetails on Employee.EmpID=LeaveDetails.EmpID where Employee.EmpID=@id
group by Employee.EmpID,Employee.EmpName,Employee.EmpType,Employee.MailID
end
