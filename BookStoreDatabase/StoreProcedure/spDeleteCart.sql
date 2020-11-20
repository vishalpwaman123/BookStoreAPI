CREATE PROCEDURE [dbo].[spDeleteCart]
	(
	@UserId int,
	@CartId int
	)
AS
BEGIN
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Cart where UserId = @UserId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								
								update Cart
								set IsDeleted = 1 , IsActive = 0
								where UserID = @UserId And  CartID = @CartId AND IsDeleted = 0 AND IsActive = 1;

								Select *
								from Cart 
								where IsDeleted = 1 AND UserID = @UserId AND CartID = @CartId ;

				COMMIT 

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO