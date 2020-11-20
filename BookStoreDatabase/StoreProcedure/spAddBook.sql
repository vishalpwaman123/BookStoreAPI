CREATE PROCEDURE [dbo].[spAddBook] 
	(
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
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF NOT EXISTS (SELECT * FROM Books WHERE [BookName] = @BookName AND AuthorName = @AuthorName AND Quantity != 0 )
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

					INSERT INTO  Books(
								BookName , AdminID , AuthorName , Description , Price , Pages , Quantity , CreatedDate , ModificationDate , Updater_AdminId , Image
								) values 
								( 
										 @BookName, 
										 @AdminID,
										 @AuthorName,  
										 @Description,
										 @Price,
										 @Pages,
										 @Quantity,
										 CURRENT_TIMESTAMP,
										 CURRENT_TIMESTAMP,
										 @AdminID,
										 @Image
								);

								Select *
								from Books 
								where [BookName] = @BookName ;

							

				COMMIT 
			END
			else
			BEGIN
			
				select BookID = 0 from Books
			END
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
