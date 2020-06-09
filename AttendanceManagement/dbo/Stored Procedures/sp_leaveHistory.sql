create procedure [dbo].[sp_leaveHistory] @id int
as
begin
select StartDate,EndDate,Reason,Status,Count from LeaveDetails where EmpID=@id
end
