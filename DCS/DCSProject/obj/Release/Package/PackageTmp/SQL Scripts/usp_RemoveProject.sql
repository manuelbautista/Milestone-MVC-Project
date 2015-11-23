
/****** Object:  StoredProcedure [dbo].[usp_RemoveProject]    Script Date: 07/22/2015 09:28:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_RemoveProject]
	-- Add the parameters for the stored procedure here
	@ProjectId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   BEGIN TRANSACTION
   
   if exists( select * from ProjectMilestones where ProjectId=@ProjectId)
   begin
   delete from ProjectMilestones where ProjectId=@ProjectId
   end
   
   if exists( select * from ProjectSchedule where ProjectId=@ProjectId)
   BEGIN
   delete from ProjectSchedule where ProjectId=@ProjectId
   END
   
   if exists( select * from ProjectStatus where ProjectId=@ProjectId)
   BEGIN
   delete from ProjectStatus where ProjectId=@ProjectId
   END
   
   if exists( select * from ProjectTracking where ProjectId=@ProjectId)
   BEGIN
   delete from ProjectTracking where ProjectId=@ProjectId
   END
   
    if exists( select * from Projects where ID =@ProjectId)
   BEGIN
   delete from Projects where ID=@ProjectId
   END
   
   IF (@@ERROR <>0)
	   BEGIN
		rollback transaction
	   END
   ELSE
	   BEGIN
		commit transaction
	   END
   
    
	
END

GO

