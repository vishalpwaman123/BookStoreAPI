CREATE PROCEDURE [dbo].[spDeleteBook]
(
@BookId int,
@AdminID int 
)
AS
BEGIN
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books where BookID = @BookId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								update Books
								set IsDeleted = 1 , AdminID = @AdminID
								where BookID = @BookId ;  


								Select *
								from Books 
								where BookID = @BookId ;

				COMMIT 


			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
