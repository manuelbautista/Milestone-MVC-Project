USE [DCS]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetProjectSummaryDetails]    Script Date: 07-07-2015 17:20:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetProjectSummaryDetails]
	@ProjectId INT
AS
BEGIN
	SELECT A.ID,
A.Name AS ProjectName,B.FirstName+' '+B.LastName As FullName,
C.StartDate As ProjectStartDate,C.EndDate AS ProjectEndDate,C.Complete AS ProjectComplete,
A.[Description] As ProjectDescription,A.ExecutiveStatus,A.Accomplishments,A.KeyRisksAndIssues,A.Direction

FROM Projects A
LEFT OUTER JOIN ProjectOwners B ON B.ID=A.ProjectOwnerANTMId
LEFT OUTER JOIN ProjectSchedule C ON C.ProjectId=A.ID

WHERE A.ID=@ProjectId
END

GO


