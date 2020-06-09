CREATE procedure [dbo].[sp_InsertLeave] @EmpID int,@startDate date,@endDate date,@reason varchar(30),@count int
as
begin
insert into LeaveDetails (EmpID,StartDate,EndDate,Reason ,Status,Count)
values(@EmpID,@startDate,@endDate,@reason,'applied',@count)
end
