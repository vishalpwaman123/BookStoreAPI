CREATE PROCEDURE [dbo].[spAddCart] 
(
@UserId int,
@BookId int
)
AS
BEGIN

	declare @status int
	set @status = 0

	BEGIN TRY
			IF EXISTS (select * from Books where BookID = @BookId AND Quantity = 0)
		Begin
				set @status=-1
				select @status
		END
		Else
			IF EXISTS (SELECT * FROM Books where BookId = @BookId )
			BEGIN
		-- Insert statements for procedure here
				BEGIN TRANSACTION
								

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

						Select *
						from Cart 
						where UserID = @UserId AND BookID = @BookId AND IsDeleted = 0 ;

					COMMIT 

				END
				Else
				Begin
					select @Status
				End
			
		END TRY
		BEGIN CATCH  

			ROLLBACK TRANSACTION
			SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
		END CATCH;


END
GO