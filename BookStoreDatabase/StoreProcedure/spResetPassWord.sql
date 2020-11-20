CREATE PROCEDURE [dbo].[spResetPassWord]
(
@email varchar(30),
@PassWord varchar(30)
)
AS
BEGIN

	declare @Status int
	set @Status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Users where EmailId = @email)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								update Users 
								set Password = @PassWord , ModificationDate = CURRENT_TIMESTAMP
								where EmailId = @email ;

								select * from Users where EmailId = @email;
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
