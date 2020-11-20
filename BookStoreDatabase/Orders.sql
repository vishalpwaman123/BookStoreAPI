CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[CartID] [int] NULL,
	[AddressID] [int] NULL,
	[IsActive] [bit] NULL,
	[IsPlaced] [bit] NULL,
	[Quantity] [varchar](5) NOT NULL,
	[TotalPrice] [varchar](10) NOT NULL,
	[CreatedDate] [varchar](40) NOT NULL,
	[ModificationDate] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [IsPlaced]
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([AddressID])
REFERENCES [dbo].[UserAddress] ([AddressID])
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO

ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
