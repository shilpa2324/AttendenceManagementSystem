CREATE procedure [dbo].[sp_approveLeaveDisplay] @id int
as
begin
select LeaveDetails.LeaveID,Employee.EmpName,Employee.EmpID,EmployeeManagerRelation.ManagerID,LeaveDetails.StartDate,LeaveDetails.EndDate,
LeaveDetails.Count,LeaveDetails.Reason,LeaveDetails.Status 
from (Employee left join LeaveDetails on Employee.EmpID=LeaveDetails.EmpID )
inner join EmployeeManagerRelation on Employee.EmpID=EmployeeManagerRelation.EmpID where ManagerID=@id
end
