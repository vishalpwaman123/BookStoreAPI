CREATE PROCEDURE [dbo].[spSearchBookByName] 
(
@BookName varchar(20)
)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								Select *
								from Books 
								where [BookName] = @BookName AND IsDeleted = 0;

				COMMIT 

				set @status = 1
					select @status
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
