
GO

/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsSummary]    Script Date: 13-07-2015 09:04:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetAllProjectsSummary] 
	
AS
BEGIN

--DUMMY DATA
		SELECT 
			 A.ID AS ProjectId,A.Name As ProjectName
			,(B.FirstName+' '+B.LastName) AS AnthemOwner
			,ISNULL(C.StartDate,'05-07-2015') As ProjectStartDate,ISNULL(C.EndDate,'07-07-2015') As ProjectEndDate,ISNULL(C.Complete,50) As ProjectComplete
			,CASE ISNULL(D.ScopeId,0) WHEN 0 THEN 'N/A' END As ProjectScope,CASE ISNULL(D.ScheduleId,0) WHEN 0 THEN 'N/A' END AS ProjectSchedule,CASE ISNULL(D.ResourcesId,0) WHEN 0 THEN 'N/A' END AS ProjectResource,CASE ISNULL(D.FinancialId,0) WHEN 0 THEN 'N/A' END AS ProjectFinancial,CASE ISNULL(D.ProjectStatusId,0) WHEN 0 THEN 'N/A' END As ProjectOverAll
			,CASE ISNULL(A.EOPlaybook,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsETOPlayBook,CASE ISNULL(A.PeerReview,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsPeerReview
			,ISNULL(E.LastUpdateDate,'05-07-2015') AS ProjectLastUpdate,ISNULL(E.EffortRemaining,100) AS ProjectEffortRemaining,ISNULL(E.DaysRemaining,100) As ProjectDaysRemaining,ISNULL(E.ErrorRAG,'N/A') As ProjectRAGStatus
		FROM Projects A
			left outer join ProjectOwners B ON A.ProjectOwnerANTMId=B.Id
			left outer join ProjectSchedule C ON A.ID=C.ProjectId
			left outer join ProjectStatus D ON A.ID = D.ProjectId
			left outer join PRojectTracking E ON A.ID=E.ProjectId
		--WHERE A.IsActive=1
	/* UN-COMMENT ONCE REQUIREMENT IS CLEARED.
	SELECT 
		 A.ID AS ProjectId,A.Name As ProjectName
		,(B.FirstName+' '+B.LastName) AS AnthemOwner
		,C.StartDate As ProjectStartDate,C.EndDate As ProjectEndDate,C.Complete As ProjectComplete
		,D.ScopeId As ProjectScope,D.ScheduleId AS ProjectSchedule,D.ResourcesId AS ProjectResource,D.FinancialId AS ProjectFinancial,D.ProjectStatusId As ProjectOverAll
		,CASE ISNULL(A.EOPlaybook,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsETOPlayBook,CASE ISNULL(A.PeerReview,0) WHEN 0 THEN 'No' ELSE '�es' END As IsPeerReview
		,E.LastUpdateDate AS ProjectLastUpdate,E.EffortRemaining AS ProjectEffortRemaining,E.DaysRemaining As ProjectDaysRemaining,E.ErrorRAG As ProjectRAGStatus
	FROM Projects A
	LEFT OUTER JOIN ProjectOwners B ON A.ProjectOwnerANTMId=B.Id
	LEFT OUTER JOIN ProjectSchedule C ON A.ID=C.ProjectId
	LEFT OUTER JOIN ProjectStatus D ON A.ID = D.ProjectId
	LEFT OUTER JOIN PRojectTracking E ON A.ID=E.ProjectId
 --WHERE A.IsActive=1
 */
END



GO



GO

/****** Object:  StoredProcedure [dbo].[usp_GetProjectSummaryDetails]    Script Date: 13-07-2015 09:04:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetProjectSummaryDetails]
	@ProjectId INT
AS
BEGIN
	SELECT A.ID,
A.Name AS ProjectName,B.FirstName+' '+B.LastName As FullName,
C.StartDate As ProjectStartDate,C.EndDate AS ProjectEndDate,C.Complete AS ProjectComplete,
A.[Description] As ProjectDescription,A.ExecutiveStatus,A.Accomplishments,A.KeyRisksAndIssues,A.Direction
,D.ScopeId,D.ResourcesId,D.FinancialId,D.ProjectStatusId,D.ScheduleId
FROM Projects A
LEFT OUTER JOIN ProjectOwners B ON B.ID=A.ProjectOwnerANTMId
LEFT OUTER JOIN ProjectSchedule C ON C.ProjectId=A.ID
LEFT OUTER JOIN (SELECT TOP 1 ProjectId,ScopeId,ScheduleId,ResourcesId,FinancialId,ProjectStatusId FROM ProjectStatus WHERE ProjectId=@ProjectId) AS D ON D.ProjectId=A.ID
WHERE A.ID=@ProjectId



END

GO


