CREATE PROCEDURE [dbo].[spGetOutOfStockBook]
	
AS
BEGIN
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								Select  BookID , BookName , AuthorName , ModificationDate
								from Books 
								where Quantity = 0;

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;


END
GO
