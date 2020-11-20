CREATE PROCEDURE [dbo].[spUserRegistration] 
	(
	@FirstName varchar(30),
	@LastName varchar(30),
	@EmailId varchar(50),
	@Password varchar(50),
	@UserRole varchar(10),
	@Locality varchar(30),
    @City varchar(20),
    @State varchar(20),
    @PhoneNumber varchar(10),
    @Pincode varchar(6),
    @LandMark varchar(30)
	)
AS
BEGIN
	
	DECLARE @UserID int
	set @UserID=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF NOT EXISTS (SELECT * FROM Users WHERE [EmailId] = @EmailId)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

					INSERT INTO Users (
								FirstName , LastName , EmailId , Password , Role , CreatedDate , ModificationDate
								) values 
								( 
										 @FirstName,  
										 @LastName,  
										 @EmailId,
										 @Password,
										 @UserRole,
										 CURRENT_TIMESTAMP,
										 CURRENT_TIMESTAMP
								);

								Select @UserID=UserID
								from Users 
								where  [EmailId] = @EmailId ;

								INSERT INTO UserAddress (
								UserID , Locality , City , State , PhoneNumber , Pincode , LandMark , CreatedDate , ModificationDate
								) values 
								(		
										@UserID,
										@Locality ,
										@City ,
										@State ,
										@PhoneNumber ,
										@Pincode ,
										@LandMark ,
										 CURRENT_TIMESTAMP,
										 CURRENT_TIMESTAMP
								);
	
								Select UserID , ModificationDate
								from Users 
								where  [EmailId] = @EmailId ;

				COMMIT 

			END
		
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;


END
GO


