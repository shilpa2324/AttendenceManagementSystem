CREATE procedure [dbo].[sp_update] @EmpName varchar(20),@EmpType varchar(30),@MailID varchar(40),@id int
as
begin
update  Employee set EmpName=@EmpName,EmpType=@EmpType,MailID=@MailID where EmpID=@id
end
