CREATE PROCEDURE [dbo].[spResetAdminPassWord]
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
		IF EXISTS (SELECT * FROM Admin where AdminEmailId = @email)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
								update Admin 
								set Password = @PassWord , ModificationDate = CURRENT_TIMESTAMP
								where AdminEmailId = @email ;

								select * from Admin where AdminEmailId = @email;
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