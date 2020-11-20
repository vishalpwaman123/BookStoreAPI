CREATE PROCEDURE [dbo].[spEmailCheck]
(
@EmailId varchar(50)
)
AS
BEGIN
	
	declare @Status int
	set @Status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Users where EmailId = @EmailId)
		BEGIN
    -- Insert statements for procedure here
			
								Select UserID , Role 
								from Users
								where EmailId = @EmailId ;

		END
		Else
		Begin
		    select @Status
		End
			
	END TRY
	BEGIN CATCH  

		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO
