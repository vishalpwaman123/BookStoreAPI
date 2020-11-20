CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[AdminName] [varchar](20) NOT NULL,
	[AdminEmailId] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](10) NOT NULL,
	[Gender] [varchar](6) NOT NULL,
	[CreatedDate] [varchar](40) NOT NULL,
	[ModificationDate] [varchar](40) NOT NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Admin] ADD  DEFAULT ((1)) FOR [IsActive]
GO
