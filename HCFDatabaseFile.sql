USE [HCF]
GO
/****** Object:  Table [dbo].[ProgramBenefitSchedule]    Script Date: 23-09-2024 19:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgramBenefitSchedule](
	[ProgramCode] [varchar](100) NOT NULL,
	[MBSItemCode] [varchar](100) NOT NULL,
	[MBSScheduleFees] [decimal](18, 2) NULL,
	[ProgramMedicalFees] [decimal](18, 2) NOT NULL,
	[DateOn] [datetime] NOT NULL,
	[DateOff] [datetime] NOT NULL,
	[ChangedBy] [varchar](100) NOT NULL,
	[ChangedDateTime] [datetime] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
 CONSTRAINT [PK_ProgramBenefitSchedule_1] PRIMARY KEY CLUSTERED 
(
	[ProgramCode] ASC,
	[MBSItemCode] ASC,
	[DateOn] ASC,
	[DateOff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
