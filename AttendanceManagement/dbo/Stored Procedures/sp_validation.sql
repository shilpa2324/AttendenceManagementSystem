CREATE procedure [dbo].[sp_validation] @username varchar(20),@password varchar(20)
as
begin
select EmpID,EmpName,EmpType,MailID from 
Employee  where 
Username=@username and Password=@password
end
