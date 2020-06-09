CREATE TABLE [dbo].[LeaveDetails] (
    [LeaveID]   INT          IDENTITY (1, 1) NOT NULL,
    [EmpID]     INT          NULL,
    [StartDate] DATE         NULL,
    [EndDate]   DATE         NULL,
    [Reason]    VARCHAR (40) NULL,
    [Status]    VARCHAR (20) NULL,
    [Count]     INT          NULL,
    PRIMARY KEY CLUSTERED ([LeaveID] ASC),
    FOREIGN KEY ([EmpID]) REFERENCES [dbo].[Employee] ([EmpID])
);

