CREATE PROCEDURE [dbo].[spUserLogin]
(
@EmailId varchar(50),
@Password varchar(50)
)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Users WHERE [EmailId] = @EmailId)
		BEGIN

								Select UserID , FirstName , LastName , EmailId , ModificationDate , Role
								from Users 
								where  [EmailId] = @EmailId AND Password = @Password AND IsActive = 1;
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

