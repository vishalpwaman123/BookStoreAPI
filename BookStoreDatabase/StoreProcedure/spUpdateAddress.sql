CREATE PROCEDURE [dbo].[spUpdateAddress]
(
@claimID int,
@State varchar(20),
@Pincode varchar(10),
@PhoneNumber varchar(10),
@Locality varchar(30),
@LandMark varchar(30),
@City varchar(20)
)
AS
BEGIN
	

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM UserAddress where UserID = @claimID)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION

								update UserAddress
								set Locality = @Locality , City = @City , State = @State , PhoneNumber = @PhoneNumber , Pincode = @Pincode , LandMark = @LandMark
								where UserID = @claimID ;  


								Select *
								from UserAddress 
								where UserID = @claimID ;

				COMMIT 

			END
		
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;



END
GO
