CREATE PROCEDURE [dbo].[spAdminEmailCheck]
	@EmailId varchar(50)
AS
	declare @Status int
	set @Status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Admin where AdminEmailId = @EmailId)
		BEGIN
    -- Insert statements for procedure here
			
								Select AdminID , Role 
								from Admin
								where AdminEmailId = @EmailId ;

		END
		Else
		Begin
		    select @Status
		End
			
	END TRY
	BEGIN CATCH  

		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

RETURN 0
