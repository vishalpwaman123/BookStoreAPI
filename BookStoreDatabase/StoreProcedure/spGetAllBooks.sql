CREATE PROCEDURE [dbo].[spGetAllBooks]

AS
BEGIN
	
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								Select *
								from Books 
								where IsDeleted = 0;

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
