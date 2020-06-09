CREATE procedure [dbo].[sp_InsertEmployee] @EmpName varchar(20),@EmpType varchar(30),@MailID varchar(40),
@UserName varchar(20),@Password varchar(10)
as
begin
insert into Employee (EmpName,EmpType,MailID,Username,Password,Action) values(@EmpName,@EmpType,@MailID,@UserName,@Password,1)
end
