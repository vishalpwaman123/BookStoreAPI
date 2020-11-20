CREATE PROCEDURE [dbo].[spDeleteWishList] 
(
@UserId int,
@WishListId int
)
AS
BEGIN
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								
								update WishList
								set IsDeleted = 1
								where UserID = @UserId And  WishListId = @WishListId;

								Select *
								from WishList 
								where IsDeleted = 1 AND UserID = @UserId AND WishListId = @WishListId ;

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;


END
GO
