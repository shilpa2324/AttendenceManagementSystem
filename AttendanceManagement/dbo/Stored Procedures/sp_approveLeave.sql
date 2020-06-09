create procedure [dbo].[sp_approveLeave] @id int
as 
begin
update LeaveDetails set Status='approved' where LeaveID=@id
end
