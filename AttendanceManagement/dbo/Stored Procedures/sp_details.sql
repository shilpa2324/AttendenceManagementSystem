CREATE procedure [dbo].[sp_details]
as
begin
select Employee.EmpID,Employee.EmpName,Employee.EmpType,Employee.MailID,SUM(Count) as leaveCount 
from (Employee left join LeaveDetails on Employee.EmpID=LeaveDetails.EmpID)
where Employee.Action=1
 group by Employee.EmpID,Employee.EmpName,Employee.EmpType,Employee.MailID
end
