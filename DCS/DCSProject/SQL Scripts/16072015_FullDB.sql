/****** Object:  StoredProcedure [dbo].[usp_GetProjectSummaryDetails]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectSummaryDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetProjectSummaryDetails]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectSchedules]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectSchedules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetProjectSchedules]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectMileStones]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectMileStones]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetProjectMileStones]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsSummary]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetAllProjectsSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetAllProjectsSummary]
GO
/****** Object:  StoredProcedure [dbo].[GetProjectDetailById]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectDetailById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProjectDetailById]
GO
/****** Object:  StoredProcedure [dbo].[GetDetailForProject]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDetailForProject]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDetailForProject]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjectsIndex]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProjectsIndex]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllProjectsIndex]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProjects]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetAllProjects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectTracking_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectTracking]'))
ALTER TABLE [dbo].[ProjectTracking] DROP CONSTRAINT [FK_ProjectTracking_Projects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectStatus_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectStatus]'))
ALTER TABLE [dbo].[ProjectStatus] DROP CONSTRAINT [FK_ProjectStatus_Projects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectSchedule_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectSchedule]'))
ALTER TABLE [dbo].[ProjectSchedule] DROP CONSTRAINT [FK_ProjectSchedule_Projects]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ProjectOwners1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ProjectOwners]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ETOPlaybooks1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_ETOPlaybooks]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[RiskMilestone]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RiskMilestone]') AND type in (N'U'))
DROP TABLE [dbo].[RiskMilestone]
GO
/****** Object:  Table [dbo].[ProjectTracking]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTracking]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectTracking]
GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectStatus]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectStatus]
GO
/****** Object:  Table [dbo].[ProjectSchedule]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectSchedule]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectSchedule]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[ProjectOwners]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectOwners]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectOwners]
GO
/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMilestones]') AND type in (N'U'))
DROP TABLE [dbo].[ProjectMilestones]
GO
/****** Object:  Table [dbo].[ETOPlaybooks]    Script Date: 16-07-2015 16:13:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETOPlaybooks]') AND type in (N'U'))
DROP TABLE [dbo].[ETOPlaybooks]
GO
/****** Object:  Table [dbo].[ETOPlaybooks]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ETOPlaybooks]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ProjectMilestones]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectMilestones]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ProjectOwners]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectOwners]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ProjectSchedule]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectSchedule]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectStatus]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ProjectTracking]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProjectTracking]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[RiskMilestone]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RiskMilestone]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[Status]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
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
END
GO
SET IDENTITY_INSERT [dbo].[ETOPlaybooks] ON 

INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1, N'TestingPlayBook', 1, CAST(N'2015-06-25 22:40:06.437' AS DateTime), CAST(N'2015-06-25 22:40:06.437' AS DateTime), 7, 13)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (2, N'UpdatedValue', 1, CAST(N'2015-07-04 00:51:06.477' AS DateTime), CAST(N'2015-07-04 23:16:24.327' AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (3, N'EditETO1', 1, CAST(N'2015-07-04 00:59:34.967' AS DateTime), CAST(N'2015-07-05 00:19:32.380' AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (4, N'TestingCompleted', 1, CAST(N'2015-07-04 01:04:19.753' AS DateTime), CAST(N'2015-07-04 23:29:45.807' AS DateTime), NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1002, N'NewPlaybook', 1, CAST(N'2015-07-05 00:20:47.807' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1003, N'NewCreated', 1, CAST(N'2015-07-05 00:43:59.917' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ETOPlaybooks] ([ID], [Name], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1004, N'test', 1, CAST(N'2015-07-05 00:44:23.767' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ETOPlaybooks] OFF
SET IDENTITY_INSERT [dbo].[ProjectMilestones] ON 

INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (1, 1, N'TestingMileStone', CAST(N'2015-07-01 22:11:13.970' AS DateTime), CAST(N'2015-07-01 22:11:13.970' AS DateTime), CAST(50.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (2, 1005, N'Fist milestone , John please join the room,acce', CAST(N'2015-07-07 00:00:00.000' AS DateTime), CAST(N'2015-11-07 00:00:00.000' AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (3, 1005, N'Second Milestone', CAST(N'2015-07-07 00:00:00.000' AS DateTime), CAST(N'2015-07-10 00:00:00.000' AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (4, 1005, N'Third MileStone-1', CAST(N'2015-07-07 00:00:00.000' AS DateTime), CAST(N'2015-07-11 00:00:00.000' AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5, 1005, N'Fourth MileStone-2', CAST(N'2015-07-11 00:00:00.000' AS DateTime), CAST(N'2015-07-11 00:00:00.000' AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (1001, 1005, N'Fifth MileStone', CAST(N'2015-07-15 00:00:00.000' AS DateTime), CAST(N'2015-07-15 00:00:00.000' AS DateTime), CAST(56.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (1002, 1005, N'Sixth MileStone', CAST(N'2015-07-16 00:00:00.000' AS DateTime), CAST(N'2015-07-16 00:00:00.000' AS DateTime), CAST(78.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (1003, 1005, N'Seventh Milestone', CAST(N'2015-07-07 00:00:00.000' AS DateTime), CAST(N'2015-07-08 00:00:00.000' AS DateTime), CAST(98.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (2001, 1006, N'Testing this milestone', CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (2002, 1004, N'Jason''s Milestone', CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (2003, 1004, N'this is a test from jason', CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(N'2015-07-12 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (3001, 1004, N'Tests', CAST(N'2015-07-13 00:00:00.000' AS DateTime), CAST(N'2015-07-13 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (3002, 1004, N'Jason milestone', CAST(N'2015-07-14 00:00:00.000' AS DateTime), CAST(N'2015-07-16 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (3003, 1004, N'JAson new milestone', CAST(N'2015-07-16 00:00:00.000' AS DateTime), CAST(N'2015-07-17 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (4001, 2006, N'Test Milestone', CAST(N'2015-05-14 00:00:00.000' AS DateTime), CAST(N'2015-07-14 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5001, 1004, N'Today is my birthday', CAST(N'2015-07-28 00:00:00.000' AS DateTime), NULL, CAST(25.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5002, 1005, N'September month Milestone', CAST(N'2015-09-30 00:00:00.000' AS DateTime), NULL, CAST(30.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5003, 1005, N'EightTH milstone', CAST(N'2015-08-28 00:00:00.000' AS DateTime), NULL, CAST(95.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5004, 1005, N'Ninth MileStone', CAST(N'2015-09-29 00:00:00.000' AS DateTime), NULL, CAST(85.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5005, 1005, N'Tenth Milestone', CAST(N'2015-10-03 00:00:00.000' AS DateTime), NULL, CAST(2.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[ProjectMilestones] ([ID], [ProjectId], [Name], [StartDate], [EndDate], [Complete], [RiskMilestoneId]) VALUES (5006, 1005, N'Eleventh Milestone', CAST(N'2015-11-02 00:00:00.000' AS DateTime), NULL, CAST(10.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[ProjectMilestones] OFF
SET IDENTITY_INSERT [dbo].[ProjectOwners] ON 

INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (30, N'mark', N'rowe', NULL, 1, 1, CAST(N'2015-07-03 22:05:29.730' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (31, N'Tom', N'', N'tom@gmail.com', 2, 1, CAST(N'2015-07-03 22:05:52.407' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (32, N'Lee', N'Smallwood', NULL, 1, 1, CAST(N'2015-07-03 22:06:44.693' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1023, N'Smith', N'', NULL, 1, 1, CAST(N'2015-07-03 23:02:05.153' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1024, N'John', N'', NULL, 2, 1, CAST(N'2015-07-03 23:02:14.463' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1025, N'Jason', N'', N'jason@gmail.com', 2, 1, CAST(N'2015-07-06 22:42:11.010' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1026, N'John', N'Wally', NULL, 1, 1, CAST(N'2015-07-07 16:51:53.023' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1027, N'Peddicord,', N'Shawn', NULL, 1, 1, CAST(N'2015-07-07 17:04:27.753' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[ProjectOwners] ([ID], [FirstName], [LastName], [Email], [Type], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (2026, N'Jason', N'Ridder', NULL, 1, 1, CAST(N'2015-07-12 21:01:11.347' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProjectOwners] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1004, N'D001', 1, 2, 0, 1, N'HungryBears', 32, 1025, N'test', N'test', N'test', N'test', N'test', 1, CAST(N'2015-07-06 22:42:21.950' AS DateTime), CAST(N'2015-07-13 09:19:58.360' AS DateTime), NULL, NULL)
INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1005, N'D001', 0, 1002, 1, 1, N'Project Tracking', 30, 1024, N'This is test descriptionof Project racking', N'Good', N'First Accomplishments', N'No risks found yet', N'start to end', 1, CAST(N'2015-07-07 01:25:29.830' AS DateTime), CAST(N'2015-07-07 01:25:29.830' AS DateTime), NULL, NULL)
INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1006, N'D001', 1, 1, 1, 1, N'Mc Donald Burger Project', 1026, 1025, N'McDonald''s is the world''s largest chain of hamburger fast food restaurants, serving around 68 million customers daily in 119 countries across 35,000 outlets.[5][6] Headquartered in the United States, the company began in 1940 as a barbecue restaurant operated by Richard and Maurice McDonald. In 1948, they reorganized their business as a hamburger stand using production line principles. Businessman Ray Kroc joined the company as a franchise agent in 1955. He subsequently purchased the chain from the McDonald brothers and oversaw its worldwide growth.[7]

A McDonald''s restaurant is operated by either a franchisee, an affiliate, or the corporation itself. The McDonald''s Corporation revenues come from the rent, royalties, and fees paid by the franchisees, as well as sales in company-operated restaurants. In 2012, the company had annual revenues of $27.5 billion and profits of $5.5 billion.[8] According to a 2012 BBC report, McDonald''s is the world''s second largest private employer—behind Walmart—with 1.9 million employees, 1.5 million of whom work for franchises.[9]', N'Please do effectively', N'Not Accomplishment', N'No Risk found', N'Just Started', 1, CAST(N'2015-07-07 16:53:42.727' AS DateTime), CAST(N'2015-07-07 16:53:42.727' AS DateTime), NULL, NULL)
INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (1007, N'D001', 1, 1, 1, 1, N'Anthem Transformation  Affiliates Storage/SAN/BUR', 1027, 1024, N'Platform health checks for the various major Anthem environments to align with vendor recommendations and best practices.', N'Part of ongoing Data Center Services platform reliability efforts is conducting health checks on our major platforms. Anthem has a significant investment in these platforms and they require annual health checks to ensure they are optimized for performance, maximized for resource efficiencies, and risk is mitigated by leveraging experienced consultants both internal and external with proven best practices.. These health checks are documented and provide Anthem with a roadmap for continual improvement. Issues or improvements are scheduled and tracked through remediation efforts.', N'Part of ongoing Data Center Services platform reliability efforts is conducting', N'Part of ongoing Data Center Services platform reliability efforts is conducting', N'Part of ongoing Data Center Services platform reliability efforts is conducting health checks on our major platforms', 1, CAST(N'2015-07-07 17:05:32.720' AS DateTime), CAST(N'2015-07-07 17:05:32.720' AS DateTime), NULL, NULL)
INSERT [dbo].[Projects] ([ID], [ProjectNumber], [EOPlaybook], [EOPlaybookId], [PeerReview], [ManagementApproval], [Name], [ProjectOwnerANTMId], [ProjectOwnerExternalId], [Description], [ExecutiveStatus], [Accomplishments], [KeyRisksAndIssues], [Direction], [IsActive], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (2006, N'1005', 0, 2, 1, 1, N'Testing Application', 2026, 1024, N'testing for business value', N'testing for status', N'testing for accomplishments', N'test text', N'test direction', 1, CAST(N'2015-07-14 08:47:11.223' AS DateTime), CAST(N'2015-07-14 08:47:11.223' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[ProjectSchedule] ON 

INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (1, 1004, CAST(N'2015-07-06 22:42:22.303' AS DateTime), CAST(N'2015-07-06 22:42:22.303' AS DateTime), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (2, 1005, CAST(N'2015-01-01 00:00:00.000' AS DateTime), CAST(N'2015-12-12 01:25:30.237' AS DateTime), CAST(50.00 AS Decimal(18, 2)))
INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (3, 1006, CAST(N'2015-03-10 16:53:43.457' AS DateTime), CAST(N'2015-10-07 16:53:43.457' AS DateTime), CAST(75.00 AS Decimal(18, 2)))
INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (4, 1007, CAST(N'2015-07-07 17:05:32.760' AS DateTime), CAST(N'2015-07-07 17:05:32.760' AS DateTime), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[ProjectSchedule] ([ID], [ProjectId], [StartDate], [EndDate], [Complete]) VALUES (1003, 2006, CAST(N'2015-05-08 00:00:00.000' AS DateTime), CAST(N'2015-08-12 00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ProjectSchedule] OFF
SET IDENTITY_INSERT [dbo].[ProjectStatus] ON 

INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (1, 1004, 1, 1, 2, 3, 1)
INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (2, 1005, 3, 2, 1, 4, 1)
INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (3, 1006, 2, 2, 1, 1, 1)
INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (4, 1007, 4, 4, 4, 4, 0)
INSERT [dbo].[ProjectStatus] ([ID], [ProjectId], [ScopeId], [ScheduleId], [ResourcesId], [FinancialId], [ProjectStatusId]) VALUES (2003, 2006, 2, 3, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[ProjectStatus] OFF
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ETOPlaybooks] FOREIGN KEY([EOPlaybookId])
REFERENCES [dbo].[ETOPlaybooks] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ETOPlaybooks]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ETOPlaybooks1] FOREIGN KEY([EOPlaybookId])
REFERENCES [dbo].[ETOPlaybooks] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ETOPlaybooks1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ETOPlaybooks1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectOwners] FOREIGN KEY([ProjectOwnerANTMId])
REFERENCES [dbo].[ProjectOwners] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectOwners]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectOwners1] FOREIGN KEY([ProjectOwnerExternalId])
REFERENCES [dbo].[ProjectOwners] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Projects_ProjectOwners1]') AND parent_object_id = OBJECT_ID(N'[dbo].[Projects]'))
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectOwners1]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectSchedule_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectSchedule]'))
ALTER TABLE [dbo].[ProjectSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProjectSchedule_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectSchedule_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectSchedule]'))
ALTER TABLE [dbo].[ProjectSchedule] CHECK CONSTRAINT [FK_ProjectSchedule_Projects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectStatus_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectStatus]'))
ALTER TABLE [dbo].[ProjectStatus]  WITH CHECK ADD  CONSTRAINT [FK_ProjectStatus_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectStatus_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectStatus]'))
ALTER TABLE [dbo].[ProjectStatus] CHECK CONSTRAINT [FK_ProjectStatus_Projects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectTracking_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectTracking]'))
ALTER TABLE [dbo].[ProjectTracking]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTracking_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProjectTracking_Projects]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProjectTracking]'))
ALTER TABLE [dbo].[ProjectTracking] CHECK CONSTRAINT [FK_ProjectTracking_Projects]
GO
/****** Object:  StoredProcedure [dbo].[GetAllProjects]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProjects]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllProjects] AS' 
END
GO

ALTER PROCEDURE [dbo].[GetAllProjects]

AS

BEGIN



select p.ID,p.Name,p.Created,p.ProjectNumber,o.FirstName + ' ' + o.LastName as ProjectOwnerANTMName,

op.FirstName + ' ' + op.LastName as ProjectOwnerExternalName from Projects p 

inner join ProjectOwners o on p.ProjectOwnerANTMId = o.ID 

inner join ProjectOwners op on p.ProjectOwnerExternalId = op.ID



END




GO
/****** Object:  StoredProcedure [dbo].[GetAllProjectsIndex]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProjectsIndex]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllProjectsIndex] AS' 
END
GO

ALTER PROCEDURE [dbo].[GetAllProjectsIndex]

AS

BEGIN



select p.ID,p.Name,p.Created,p.ProjectNumber,o.FirstName + ' ' + o.LastName as ProjectOwnerANTMName,

op.FirstName + ' ' + op.LastName as ProjectOwnerExternalName, ps.StartDate, ps.EndDate 

from Projects p 

inner join ProjectOwners o on p.ProjectOwnerANTMId = o.ID 

inner join ProjectOwners op on p.ProjectOwnerExternalId = op.ID

inner join ProjectSchedule ps on p.ID=ps.ProjectId

END




GO
/****** Object:  StoredProcedure [dbo].[GetDetailForProject]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDetailForProject]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetDetailForProject] AS' 
END
GO
ALTER PROCEDURE [dbo].[GetDetailForProject] 



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
/****** Object:  StoredProcedure [dbo].[GetProjectDetailById]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProjectDetailById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetProjectDetailById] AS' 
END
GO


ALTER PROCEDURE [dbo].[GetProjectDetailById]



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
/****** Object:  StoredProcedure [dbo].[usp_GetAllProjectsSummary]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetAllProjectsSummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_GetAllProjectsSummary] AS' 
END
GO

ALTER PROCEDURE [dbo].[usp_GetAllProjectsSummary] 
	
AS
BEGIN

--DUMMY DATA
		SELECT 
			 A.ID AS ProjectId,A.Name As ProjectName
			,(B.FirstName+' '+B.LastName) AS AnthemOwner
			,ISNULL(C.StartDate,'05-07-2015') As ProjectStartDate,ISNULL(C.EndDate,'07-07-2015') As ProjectEndDate,ISNULL(C.Complete,0) As ProjectComplete
			,CASE ISNULL(D.ScopeId,0) WHEN 0 THEN 'Not Set' ELSE CONVERT(NVARCHAR(MAX),D.ScopeId) END As ProjectScope,CASE ISNULL(D.ScheduleId,0) WHEN 0 THEN 'Not Set' ELSE CONVERT(NVARCHAR(MAX),D.ScheduleId) END AS ProjectSchedule,CASE ISNULL(D.ResourcesId,0) WHEN 0 THEN 'Not Set' ELSE CONVERT(NVARCHAR(MAX),D.ResourcesId) END AS ProjectResource,CASE ISNULL(D.FinancialId,0) WHEN 0 THEN 'Not Set' ELSE CONVERT(NVARCHAR(MAX),D.FinancialId) END AS ProjectFinancial,CASE ISNULL(D.ProjectStatusId,0) WHEN 0 THEN 'Not Set' ELSE CONVERT(NVARCHAR(MAX),D.ProjectStatusId) END As ProjectOverAll
			,CASE ISNULL(A.EOPlaybook,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsETOPlayBook,CASE ISNULL(A.PeerReview,0) WHEN 0 THEN 'No' ELSE 'Yes' END As IsPeerReview
			,ISNULL(E.LastUpdateDate,'05-07-2015') AS ProjectLastUpdate,ISNULL(E.EffortRemaining,100) AS ProjectEffortRemaining,
		--ISNULL(E.DaysRemaining,100) As ProjectDaysRemaining,
	  CONVERT (decimal(18,2),	DATEDIFF(DAY,GETDATE(),ISNULL(C.EndDate,'07-07-2015') ))  As ProjectDaysRemaining,
			ISNULL(E.ErrorRAG,'N/A') As ProjectRAGStatus
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
/****** Object:  StoredProcedure [dbo].[usp_GetProjectMileStones]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectMileStones]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_GetProjectMileStones] AS' 
END
GO

ALTER PROCEDURE [dbo].[usp_GetProjectMileStones]
	@ProjectId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ID,Name,StartDate,EndDate FROM ProjectMilestones WHERE ProjectId=@ProjectId
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectSchedules]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectSchedules]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_GetProjectSchedules] AS' 
END
GO

ALTER PROCEDURE [dbo].[usp_GetProjectSchedules]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT A.ID,A.StartDate,A.EndDate,A.Complete,A.ProjectId FROM ProjectSchedule A
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetProjectSummaryDetails]    Script Date: 16-07-2015 16:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetProjectSummaryDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[usp_GetProjectSummaryDetails] AS' 
END
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
