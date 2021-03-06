
/****** Object:  StoredProcedure [dbo].[usp_GetProjectMileStones]    Script Date: 17-07-2015 15:25:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_GetProjectMileStones]
	@ProjectId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ID,Name,StartDate,EndDate,ISNULL(Complete,0) As Complete FROM ProjectMilestones WHERE ProjectId=@ProjectId
END
