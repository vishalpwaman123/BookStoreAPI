CREATE PROCEDURE [dbo].[spAddImage]
(
@BookID int,
@AdminID int,
@Image varchar(200)
)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books WHERE BookID = @BookID AND IsDeleted = 0 )
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

				update Books
				set Image = @Image , ModificationDate = CURRENT_TIMESTAMP , Updater_AdminId = @AdminID
				where BookID = @BookID AND IsDeleted = 0;

				select * from Books where BookID = @BookID AND IsDeleted = 0;
					
				COMMIT 
			END
			else
			BEGIN
			
				select @status
			END
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
