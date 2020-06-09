CREATE procedure [dbo].[sp_InsertLeave] @EmpID int,@startDate date,@endDate date,@reason varchar(30),@count int
as
begin
if not exists(select * from LeaveDetails where EmpID=@EmpID and StartDate between @startDate and @endDate)
begin
insert into LeaveDetails (EmpID,StartDate,EndDate,Reason ,Status,Count)
values(@EmpID,@startDate,@endDate,@reason,'applied',@count)
end
end
