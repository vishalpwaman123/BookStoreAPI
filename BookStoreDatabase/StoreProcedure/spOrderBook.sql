CREATE PROCEDURE [dbo].[spOrderBook]
	(
	@CartID int,
	@claimID int
	)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	declare @FLAG int
	set @FLAG = 0

	declare @QuantityCount int
	
	
	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Cart where CartID = @CartID AND UserID = @claimID AND IsDeleted = 0)
		BEGIN
    -- Insert statements for procedure here
			BEGIN TRANSACTION
									select @QuantityCount = Quantity from Books where BookID IN ( select BookID from Cart where CartID = @CartID )

									IF (@QuantityCount > @FLAG)
									BEGIN

											update Books
											set Quantity -= 1 
											where BookID IN ( select BookID from Cart where CartID = @CartID )
									
											Select AddressID , Locality , City , State , PhoneNumber , Pincode , LandMark , CartID , Cart.UserID , BookID ,Cart.ModificationDate
											from UserAddress , Cart
											where UserAddress.UserID = @claimID OR IsDeleted = 1;
								
											update Cart
											set IsDeleted = 1 , ModificationDate = CURRENT_TIMESTAMP , IsActive = 0
											where CartID = @CartID;
							
											INSERT INTO Orders (UserID, 
																CartID , 
																AddressID , 
																IsActive , 
																IsPlaced , 
																Quantity , 
																TotalPrice , 
																CreatedDate , 
																ModificationDate )
											values( @claimID, 
												   @CartID, 
												   (Select AddressID from UserAddress where UserAddress.UserID = @claimID) ,
												   1 , 
												   1 , 
												   1 , 
												   (Select Price from Books where BookID IN (Select BookID from Cart where CartID = @CartID)),
												   CURRENT_TIMESTAMP , 
												   CURRENT_TIMESTAMP);

										END
										else
										BEGIN
			
											select @status

										END
			
			COMMIT

			END
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO