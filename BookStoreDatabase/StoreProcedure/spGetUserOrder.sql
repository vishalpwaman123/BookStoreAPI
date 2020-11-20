CREATE PROCEDURE [dbo].[spGetUserOrder]
(
@UserId int 
)
AS
BEGIN
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Orders where UserId = @UserId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								Select *
								from Orders 
								where UserId = @UserId

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
