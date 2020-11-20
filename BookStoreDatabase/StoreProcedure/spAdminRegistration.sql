CREATE PROCEDURE  [dbo].[spAdminRegistration]
	(
	@AdminName varchar(20),
	@AdminEmailId varchar(30),
	@Password varchar(30),
	@Role varchar(10),
	@Gender varchar(10)
	)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF NOT EXISTS (SELECT * FROM Admin WHERE [AdminEmailId] = @AdminEmailId )
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

					INSERT INTO Admin (
								AdminName , AdminEmailId , Password , Role , Gender , CreatedDate , ModificationDate
								) values 
								( 
										 @AdminName,  
										 @AdminEmailId,  
										 @Password,
										 @Role,
										 @Gender,
										 CURRENT_TIMESTAMP,
										 CURRENT_TIMESTAMP
								);

								Select AdminID , AdminName , AdminEmailId , Password , Role , Gender , ModificationDate
								from Admin 
								where  [AdminEmailId] = @AdminEmailId ;

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
