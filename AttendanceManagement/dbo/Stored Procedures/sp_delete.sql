CREATE procedure [dbo].[sp_delete] @id int
as
begin
update Employee set Action=0 where EmpID=@id
end
