USE [DCS]
GO
ALTER TABLE [dbo].[ProjectTracking] DROP CONSTRAINT [FK_ProjectTracking_Projects]
GO
ALTER TABLE [dbo].[ProjectStatus] DROP CONSTRAINT [FK_ProjectStatus_Projects]
GO
ALTER TABLE [dbo].[ProjectSchedule] DROP CONSTRAINT [FK_ProjectSchedule_Projects]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ProjectOwners1]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ProjectOwners]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ETOPlaybooks1]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ETOPlaybooks]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[RiskMilestone]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[RiskMilestone]
GO
/****** Object:  Table [dbo].[ProjectTracking]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ProjectTracking]
GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ProjectStatus]
GO
/****** Object:  Table [dbo].[ProjectSchedule]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ProjectSchedule]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[ProjectOwners]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ProjectOwners]
GO
/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ProjectMilestones]
GO
/****** Object:  Table [dbo].[ETOPlaybooks]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP TABLE [dbo].[ETOPlaybooks]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsSummary]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP PROCEDURE [dbo].[usp_GetAllProjectsSummary]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectDetailById]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP PROCEDURE [dbo].[GetProjectDetailById]
GO
/****** Object:  StoredProcedure [dbo].[GetDetailForProject]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP PROCEDURE [dbo].[GetDetailForProject]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 7/7/2015 1:46:05 AM ******/
DROP PROCEDURE [dbo].[GetAllProjects]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllProjects]

AS

BEGIN



select p.ID,p.Name,p.Created,p.ProjectNumber,o.FirstName + ' ' + o.LastName as ProjectOwnerANTMName,

op.FirstName + ' ' + op.LastName as ProjectOwnerExternalName from Projects p 

inner join ProjectOwners o on p.ProjectOwnerANTMId = o.ID 

inner join ProjectOwners op on p.ProjectOwnerExternalId = op.ID



END



GO
/****** Object:  StoredProcedure [dbo].[GetDetailForProject]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDetailForProject] 



@ProjectId int



AS

BEGIN



select p. *,o.FirstName + ' ' + o.LastName as ProjectOwnerANTMName,

op.FirstName + ' ' + op.LastName as ProjectOwnerExternalName, ps.EndDate,ps.StartDate,ps.Complete,

ep.Name as ETOPlaybooks, pst.ScopeId,pst.ScheduleId,pst.ResourcesId,pst.FinancialId,pst.ProjectStatusId

 

from Projects p 

inner join ProjectOwners o on p.ProjectOwnerANTMId = o.ID 

inner join ProjectOwners op on p.ProjectOwnerExternalId = op.ID

inner join EtoPlaybooks ep on p.EOPlaybookId = ep.ID

inner join ProjectStatus pst on p.ID = pst.ProjectId

left join ProjectSchedule ps on p.ID = ps.ProjectId





where p.ID = @ProjectId



END
GO
/****** Object:  StoredProcedure [dbo].[GetProjectDetailById]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[GetProjectDetailById]



@ProjectId int



AS

BEGIN



select p. *,o.FirstName + ' ' + o.LastName as ProjectOwnerANTMName,

op.FirstName + ' ' + op.LastName as ProjectOwnerExternalName, ps.EndDate,ps.StartDate,ps.Complete,

pm.Complete as ProjectMilestoneComplete, pm.Name as ProjectMilestoneName, ep.Name as ETOPlaybooks

 

from Projects p 

inner join ProjectOwners o on p.ProjectOwnerANTMId = o.ID 

inner join ProjectOwners op on p.ProjectOwnerExternalId = op.ID

inner join EtoPlaybooks ep on p.EOPlaybookId = ep.ID

left join ProjectSchedule ps on p.ID = ps.ProjectId

left join ProjectMilestones pm on p.ID =  pm.ProjectId



where p.ID = @ProjectId



END









GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsSummary]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetAllProjectsSummary] 
	
AS
BEGIN

--DUMMY DATA
		SELECT 
			 A.ID AS ProjectId,A.Name As ProjectName
			,(B.FirstName+' '+B.LastName) AS AnthemOwner
			,ISNULL(C.StartDate,'05-07-2015') As ProjectStartDate,ISNULL(C.EndDate,'07-07-2015') As ProjectEndDate,ISNULL(C.Complete,50) As ProjectComplete
			,CASE ISNULL(D.ScopeId,0) WHEN 0 THEN 'N/A' END As ProjectScope,CASE ISNULL(D.ScheduleId,0) WHEN 0 THEN 'N/A' END AS ProjectSchedule,CASE ISNULL(D.ResourcesId,0) WHEN 0 THEN 'N/A' END AS ProjectResource,CASE ISNULL(D.FinancialId,0) WHEN 0 THEN 'N/A' END AS ProjectFinancial,CASE ISNULL(D.ProjectStatusId,0) WHEN 0 THEN 'N/A' END As ProjectOverAll
			,CASE ISNULL(A.EOPlaybook,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsETOPlayBook,CASE ISNULL(A.PeerReview,0) WHEN 0 THEN 'No' ELSE 'Ýes' END As IsPeerReview
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
		,CASE ISNULL(A.EOPlaybook,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsETOPlayBook,CASE ISNULL(A.PeerReview,0) WHEN 0 THEN 'No' ELSE 'Ýes' END As IsPeerReview
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
/****** Object:  Table [dbo].[ETOPlaybooks]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ETOPlaybooks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ETOPlaybook] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMilestones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Complete] [decimal](18, 2) NULL,
	[RiskMilestoneId] [int] NULL,
 CONSTRAINT [PK_ProjectMilestones] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectOwners]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectOwners](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ProjectOwner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectNumber] [nvarchar](50) NULL,
	[EOPlaybook] [bit] NULL,
	[EOPlaybookId] [int] NULL,
	[PeerReview] [bit] NULL,
	[ManagementApproval] [bit] NULL,
	[Name] [nvarchar](50) NULL,
	[ProjectOwnerANTMId] [int] NULL,
	[ProjectOwnerExternalId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[ExecutiveStatus] [nvarchar](max) NULL,
	[Accomplishments] [nvarchar](max) NULL,
	[KeyRisksAndIssues] [nvarchar](max) NULL,
	[Direction] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectSchedule]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Complete] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ProjectSchedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStatus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[ScopeId] [int] NULL,
	[ScheduleId] [int] NULL,
	[ResourcesId] [int] NULL,
	[FinancialId] [int] NULL,
	[ProjectStatusId] [int] NULL,
 CONSTRAINT [PK_ProjectStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectTracking]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTracking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[LastUpdateAny] [nvarchar](50) NULL,
	[LastUpdateDate] [datetime] NULL,
	[EffortRemaining] [decimal](18, 2) NULL,
	[DaysRemaining] [decimal](18, 2) NULL,
	[ErrorRAG] [nvarchar](200) NULL,
	[ErrorProgress] [nvarchar](200) NULL,
 CONSTRAINT [PK_ProjectTracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RiskMilestone]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiskMilestone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_RiskMilestone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 7/7/2015 1:46:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ETOPlaybooks] ON 

INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1, N'TestingPlayBook', 1, CAST(0x0000A4C20175908B AS DateTime), CAST(0x0000A4C20175908B AS DateTime), 7, 13)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (2, N'UpdatedValue', 1, CAST(0x0000A4CB000E0987 AS DateTime), CAST(0x0000A4CB017F88C2 AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (3, N'EditETO1', 1, CAST(0x0000A4CB00105D6A AS DateTime), CAST(0x0000A4CC00055DE2 AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (4, N'TestingCompleted', 1, CAST(0x0000A4CB0011AB26 AS DateTime), CAST(0x0000A4CB018333FE AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1002, N'NewPlaybook', 1, CAST(0x0000A4CC0005B646 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1003, N'NewCreated', 1, CAST(0x0000A4CC000C15A7 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1004, N'test', 1, CAST(0x0000A4CC000C319A AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ETOPlaybooks] OFF
SET IDENTITY_INSERT [dbo].[ProjectMilestones] ON 

INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (1, 1, N'TestingMileStone', CAST(0x0000A4C8016DA24F AS DateTime), CAST(0x0000A4C8016DA24F AS DateTime), CAST(50.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (2, 1005, N'Fist milestone', CAST(0x0000A4CE00000000 AS DateTime), CAST(0x0000A54900000000 AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[ProjectMilestones] OFF
SET IDENTITY_INSERT [dbo].[ProjectOwners] ON 

INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (30, N'mark', N'rowe', NULL, 1, 1, CAST(0x0000A4CA016C0EE7 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (31, N'Tom', N'', N'tom@gmail.com', 2, 1, CAST(0x0000A4CA016C297A AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (32, N'Lee', N'Smallwood', NULL, 1, 1, CAST(0x0000A4CA016C66C0 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1023, N'Smith', N'', NULL, 1, 1, CAST(0x0000A4CA017B99EA AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1024, N'John', N'', NULL, 2, 1, CAST(0x0000A4CA017BA4D3 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1025, N'Jason', N'', N'jason@gmail.com', 2, 1, CAST(0x0000A4CD01762287 AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProjectOwners] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1004, N'D001', 1, 2, 0, 1, N'HungryBears', 32, 1025, N'test', N'test', N'test', N'test', N'test', 1, CAST(0x0000A4CD01762F59 AS DateTime), CAST(0x0000A4CD01762F59 AS DateTime), NULL, NULL)
INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1005, N'D001', 0, 1002, 1, 1, N'Project Tracking', 30, 1024, N'This is test descriptionof Project racking', N'Good', N'First Accomplishments', N'No risks found yet', N'start to end', 1, CAST(0x0000A4CE00177B85 AS DateTime), CAST(0x0000A4CE00177B85 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[ProjectSchedule] ON 

INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (1, 1004, CAST(0x0000A4CD01762FC3 AS DateTime), CAST(0x0000A4CD01762FC3 AS DateTime), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (2, 1005, CAST(0x0000A4CE00177BFF AS DateTime), CAST(0x0000A4CE00177BFF AS DateTime), CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ProjectSchedule] OFF
SET IDENTITY_INSERT [dbo].[ProjectStatus] ON 

INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (1, 1004, 4, 4, 4, 4, 0)
INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (2, 1005, 3, 2, 1, 4, 1)
SET IDENTITY_INSERT [dbo].[ProjectStatus] OFF
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ETOPlaybooks] FOREIGN KEY([EOPlaybookId])
REFERENCES [dbo].[ETOPlaybooks] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ETOPlaybooks]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ETOPlaybooks1] FOREIGN KEY([EOPlaybookId])
REFERENCES [dbo].[ETOPlaybooks] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ETOPlaybooks1]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectOwners] FOREIGN KEY([ProjectOwnerANTMId])
REFERENCES [dbo].[ProjectOwners] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectOwners]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectOwners1] FOREIGN KEY([ProjectOwnerExternalId])
REFERENCES [dbo].[ProjectOwners] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectOwners1]
GO
ALTER TABLE [dbo].[ProjectSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProjectSchedule_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
ALTER TABLE [dbo].[ProjectSchedule] CHECK CONSTRAINT [FK_ProjectSchedule_Projects]
GO
ALTER TABLE [dbo].[ProjectStatus]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStatus_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
ALTER TABLE [dbo].[ProjectStatus] CHECK CONSTRAINT [FK_ProjectStatus_Projects]
GO
ALTER TABLE [dbo].[ProjectTracking]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTracking_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
ALTER TABLE [dbo].[ProjectTracking] CHECK CONSTRAINT [FK_ProjectTracking_Projects]
GO
