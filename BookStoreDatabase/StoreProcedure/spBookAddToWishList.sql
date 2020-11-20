CREATE PROCEDURE [dbo].[spBookAddToWishList]
	(
@UserId int,
@BookId int
	)
AS
BEGIN

	BEGIN TRY
    -- Insert statements for procedure here
		IF NOT EXISTS (SELECT * FROM WishList WHERE UserID = @UserId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

					insert into WishList (
								UserID , BookID , IsMoved , CreatedDate , ModificationDate
								)
					values
							(
							@UserId, 
							@BookId, 
							0, 
							CURRENT_TIMESTAMP, 
							CURRENT_TIMESTAMP
							)

						select * from WishList

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;
END
GO
