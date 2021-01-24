USE [Connect.Planet.API]
GO
/****** Object:  Table [dbo].[GroupInfo]    Script Date: 1/24/2021 9:04:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupInfo](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[MemberIds] [text] NULL,
	[IsActive] [int] NOT NULL,
 CONSTRAINT [PK_GroupInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [u_groupid] UNIQUE NONCLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [u_name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageHistories]    Script Date: 1/24/2021 9:04:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageHistories](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[Message] [text] NOT NULL,
	[MessageDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[GroupId] [varchar](50) NOT NULL,
	[IsActive] [int] NOT NULL,
 CONSTRAINT [PK_MessageHistories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/24/2021 9:04:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](200) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyDate] [datetime] NULL,
	[LastSignInDate] [datetime] NULL,
	[ConnectionId] [varchar](100) NULL,
	[Token] [varchar](1) NULL,
	[IsLive] [int] NOT NULL,
	[IsActive] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [u_email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MessageHistories]  WITH CHECK ADD  CONSTRAINT [FK_MessageHistories_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[GroupInfo] ([GroupId])
GO
ALTER TABLE [dbo].[MessageHistories] CHECK CONSTRAINT [FK_MessageHistories_GroupId]
GO
ALTER TABLE [dbo].[MessageHistories]  WITH CHECK ADD  CONSTRAINT [FK_MessageHistories_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MessageHistories] CHECK CONSTRAINT [FK_MessageHistories_UserId]
GO
