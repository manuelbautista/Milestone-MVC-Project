/****** Object:  StoredProcedure [dbo].[usp_RemoveProjectMileStone]    Script Date: 8/13/2015 10:00:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_RemoveProjectMileStone]
	@MileStoneId	INT,
	@ProjectId		INT
AS
BEGIN
DECLARE @AveragePercentage DECIMAL(18,2)
	BEGIN TRY
		BEGIN TRANSACTION

		DELETE FROM ProjectMilestones WHERE ID=@MileStoneId
		IF EXISTS (SELECT * FROM ProjectMilestones WHERE ProjectId=@ProjectId)
		BEGIN
			SET @AveragePercentage= (SELECT AVG(ISNULL(Complete,0)) AS AvgComplete from ProjectMilestones WHERE ProjectId=@ProjectId Group by ProjectId)
		END
		ELSE
		BEGIN
			SET @AveragePercentage=0.00
		END
		UPDATE ProjectSchedule SET Complete=@AveragePercentage WHERE ProjectId=@ProjectId
		

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION
				--RAISERROR(ERROR_MESSAGE(), ERROR_SEVERITY(), 1)
			END
	END CATCH
END

GO

/****** Object:  StoredProcedure [dbo].[usp_SaveProjectMileStone]    Script Date: 8/13/2015 10:01:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_SaveProjectMileStone]
	@MileStoneId	INT,
	@ProjectId		INT,
	@Name			NVARCHAR(50),
	@StartDate		DATETIME,
	@Complete		DECIMAL(18,2)
AS
BEGIN
DECLARE @AveragePercentage DECIMAL(18,2)
	BEGIN TRY
		BEGIN TRANSACTION
			IF @MileStoneId > 0
			BEGIN
				UPDATE ProjectMilestones SET Name=@Name,StartDate=@StartDate,Complete=@Complete
				WHERE ID=@MileStoneId
				
				IF EXISTS (SELECT * FROM ProjectMilestones WHERE ProjectId=@ProjectId)
				BEGIN
					SET @AveragePercentage= (SELECT AVG(ISNULL(Complete,0)) AS AvgComplete from ProjectMilestones WHERE ProjectId=@ProjectId Group by ProjectId)
				END
				ELSE
				BEGIN
					SET @AveragePercentage=0.00
				END
				UPDATE ProjectSchedule SET Complete=@AveragePercentage WHERE ProjectId=@ProjectId

			END
			ELSE
			BEGIN
				INSERT INTO ProjectMilestones (ProjectId,Name,StartDate,Complete)
				VALUES (@ProjectId,@Name,@StartDate,@Complete)
				
				IF EXISTS (SELECT * FROM ProjectMilestones WHERE ProjectId=@ProjectId)
				BEGIN
					SET @AveragePercentage= (SELECT AVG(ISNULL(Complete,0)) AS AvgComplete from ProjectMilestones WHERE ProjectId=@ProjectId Group by ProjectId)
				END
				ELSE
				BEGIN
					SET @AveragePercentage=0.00
				END
				UPDATE ProjectSchedule SET Complete=@AveragePercentage WHERE ProjectId=@ProjectId

			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			BEGIN
				ROLLBACK TRANSACTION
				--RAISERROR(ERROR_MESSAGE(), ERROR_SEVERITY(), 1)
			END
	END CATCH
END

GO
