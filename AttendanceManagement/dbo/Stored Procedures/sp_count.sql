create procedure [dbo].[sp_count] @id int
as
begin
select SUM(Count) from LeaveDetails where EmpID=@id group by EmpID
end
