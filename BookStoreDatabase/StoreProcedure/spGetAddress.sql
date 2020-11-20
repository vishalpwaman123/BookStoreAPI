CREATE PROCEDURE [dbo].[spGetAddress]
(
@claimID int
)
AS
BEGIN
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM UserAddress where UserID = @claimID)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
					
								Select *
								from UserAddress 
								where UserID = @claimID ;

				COMMIT 

			END
		
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
