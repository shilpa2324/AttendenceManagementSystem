CREATE TABLE [dbo].[EmployeeManagerRelation] (
    [RelID]     INT IDENTITY (1, 1) NOT NULL,
    [EmpID]     INT NULL,
    [ManagerID] INT NULL,
    PRIMARY KEY CLUSTERED ([RelID] ASC),
    FOREIGN KEY ([EmpID]) REFERENCES [dbo].[Employee] ([EmpID]),
    FOREIGN KEY ([ManagerID]) REFERENCES [dbo].[Employee] ([EmpID])
);

