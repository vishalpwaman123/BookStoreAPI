CREATE PROCEDURE [dbo].[spUpdateBook] 
	(
	@BookId int,
	@BookName varchar(20),
	@AuthorName varchar(20),
	@Description varchar(1000),
	@Price varchar(10),
	@Pages varchar(10),
	@AdminID varchar(10),
	@Quantity int,
	@Image varchar(100)
	)
AS
BEGIN
	
	
		
	declare @status int
	set @status = 0

	declare @Parameter varchar(15)

	declare @ParameterStringData varchar(20)

	declare @ParameterIntData int

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books where BookID = @BookId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								
								

									update Books
									set AuthorName = @AuthorName ,
										Description = @Description,
										Price = @Price,
										Pages = @Pages,
										Quantity=@Quantity,
										Image=@Image,
										Updater_AdminId = @AdminID , 
										ModificationDate = CURRENT_TIMESTAMP
									where BookID = @BookId ;

								
								  
								Select *
								from Books 
								where BookID = @BookId ;

				COMMIT 

			END
			Else
				Begin
					select BookID = 0 from Books
				End
		
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;
END
GO


