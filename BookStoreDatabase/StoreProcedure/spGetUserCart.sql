CREATE PROCEDURE [dbo].[spGetUserCart] 
(
@UserId int
)
AS
BEGIN
	
		BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								Select *
								from Cart
								where IsDeleted = 0 AND UserID = @UserId;

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;


END
GO