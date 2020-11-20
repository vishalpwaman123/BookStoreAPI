CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[AdminID] [int] NULL,
	[BookName] [varchar](20) NOT NULL,
	[AuthorName] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Price] [varchar](20) NOT NULL,
	[Pages] [varchar](20) NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [varchar](30) NOT NULL,
	[ModificationDate] [varchar](30) NOT NULL,
	[Image] [varchar](200) NULL,
	[Updater_AdminId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Books] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD FOREIGN KEY([AdminID])
REFERENCES [dbo].[Admin] ([AdminID])
GO
