CREATE PROCEDURE [dbo].[spMoveWishListToCart]
(
@UserId int,
@WishListId int
)
AS
BEGIN

DECLARE @BookId int 

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM WishList )
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								
								update WishList
								set IsDeleted = 1 , IsMoved = 1 , ModificationDate = CURRENT_TIMESTAMP
								where UserID = @UserId And  WishListId = @WishListId;

								Select @BookId = BookID 
								from WishList 
								where IsDeleted = 1 AND UserID = @UserId AND WishListId = @WishListId ;

								Select *
								from WishList 
								where IsDeleted = 1 AND UserID = @UserId AND WishListId = @WishListId ;


								insert into Cart (
								UserID , BookID , IsActive , CreatedDate , ModificationDate
								)
					values
							(
							@UserId, 
							@BookId, 
							1, 
							CURRENT_TIMESTAMP, 
							CURRENT_TIMESTAMP
							)

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
