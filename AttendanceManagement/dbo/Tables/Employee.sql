CREATE TABLE [dbo].[Employee] (
    [EmpID]    INT          IDENTITY (1, 1) NOT NULL,
    [EmpName]  VARCHAR (30) NULL,
    [EmpType]  VARCHAR (30) NULL,
    [MailID]   VARCHAR (40) NULL,
    [Action]   INT          NULL,
    [Username] VARCHAR (30) NULL,
    [Password] VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([EmpID] ASC)
);

