CREATE TABLE [dbo].[UserAddress](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Locality] [varchar](30) NULL,
	[City] [varchar](30) NOT NULL,
	[State] [varchar](30) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[Pincode] [varchar](6) NOT NULL,
	[LandMark] [varchar](30) NULL,
	[CreatedDate] [varchar](30) NULL,
	[ModificationDate] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserAddress] ADD  DEFAULT ('NA') FOR [Locality]
GO

ALTER TABLE [dbo].[UserAddress] ADD  DEFAULT ('NA') FOR [LandMark]
GO

ALTER TABLE [dbo].[UserAddress]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
