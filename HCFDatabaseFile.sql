USE [HCF]
GO
/****** Object:  Table [dbo].[ProgramBenefitSchedule]    Script Date: 25-09-2024 18:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramBenefitSchedule](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProgramCode] [varchar](100) NOT NULL,
	[MBSItemCode] [varchar](100) NOT NULL,
	[MBSScheduleFees] [decimal](18, 2) NULL,
	[ProgramMedicalFees] [decimal](18, 2) NOT NULL,
	[DateOn] [datetime] NOT NULL,
	[DateOff] [datetime] NULL,
	[ChangedBy] [varchar](100) NOT NULL,
	[ChangedDateTime] [datetime] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[Status] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ProgramBenefitSchedule_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ProgramBenefitSchedule] ON 

INSERT [dbo].[ProgramBenefitSchedule] ([Id], [ProgramCode], [MBSItemCode], [MBSScheduleFees], [ProgramMedicalFees], [DateOn], [DateOff], [ChangedBy], [ChangedDateTime], [Comments], [Status]) VALUES (1, N'NGJ', N'49318', CAST(49093.00 AS Decimal(18, 2)), CAST(49094.00 AS Decimal(18, 2)), CAST(N'2024-09-02T00:00:00.000' AS DateTime), CAST(N'2024-09-04T08:52:52.697' AS DateTime), N'dino', CAST(N'2024-09-24T11:44:40.667' AS DateTime), N'Comments6', N'Publish')
INSERT [dbo].[ProgramBenefitSchedule] ([Id], [ProgramCode], [MBSItemCode], [MBSScheduleFees], [ProgramMedicalFees], [DateOn], [DateOff], [ChangedBy], [ChangedDateTime], [Comments], [Status]) VALUES (2, N'NGJ', N'00000', CAST(10000.00 AS Decimal(18, 2)), CAST(2000.00 AS Decimal(18, 2)), CAST(N'2024-09-02T00:00:00.000' AS DateTime), NULL, N'ravi', CAST(N'2024-09-24T12:09:21.177' AS DateTime), N'comments3', N'Publish')
SET IDENTITY_INSERT [dbo].[ProgramBenefitSchedule] OFF
GO
ALTER TABLE [dbo].[ProgramBenefitSchedule] ADD  CONSTRAINT [DF_ProgramBenefitSchedule_Status]  DEFAULT ('') FOR [Status]
GO
