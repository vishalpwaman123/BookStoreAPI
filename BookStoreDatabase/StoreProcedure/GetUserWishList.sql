CREATE PROCEDURE [dbo].[GetUserWishList]
	@UserId int
AS
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM WishList where UserID = @UserId)
		BEGIN
    -- Insert statements for procedure here
			

								Select *
								from WishList 
								where IsDeleted = 0 AND UserID = @UserId;


			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;
RETURN 0
