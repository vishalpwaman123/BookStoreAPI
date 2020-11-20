CREATE PROCEDURE [dbo].[spOrderAllBook]
(
@claimID int
)	
AS
BEGIN
	
	DECLARE @CartId int
	
	DECLARE @UserId int 

	declare @BookId int
	
	declare @IsActive int
	
	
	BEGIN TRY

		declare OrderAllBook cursor for

		select CartID , UserID , BookID , IsActive from Cart where UserID = @claimID AND  IsActive = 1

		open orderAllBook

		fetch next from orderAllBook into @cartId , @UserId , @BookId , @IsActive

		while @@FETCH_STATUS = 0
		begin

			iF EXISTS ( select * from Books , Cart where Quantity !=0 AND Books.BookID = @BookId AND CartID = @CartId AND UserID = @claimID AND Cart.IsDeleted = 0 )
			BEGIN

				BEGIN TRANSACTION
										

												update Books
												set Quantity -= 1 , ModificationDate = CURRENT_TIMESTAMP
												where BookID = @BookId

												update Cart
												set IsDeleted = 1 , ModificationDate = CURRENT_TIMESTAMP , IsActive = 0
												where CartID = @CartId AND BookID = @BookId;
							
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
													   @CartId, 
													   (Select AddressID from UserAddress where UserAddress.UserID = @claimID) ,
													   1 , 
													   1 , 
													   1 , 
													   (Select Price from Books where BookID IN (Select BookID from Cart where CartID = @CartId)),
													   CURRENT_TIMESTAMP , 
													   CURRENT_TIMESTAMP);

													   --select * from Cart
			
				COMMIT
			END
			
			fetch next from OrderAllBook into @cartId , @UserId , @BookId , @IsActive

			end
		close OrderAllBook;
		deallocate OrderAllBook;

						  Select AddressID , Locality , City , State , PhoneNumber , Pincode , LandMark , CartID , Cart.UserID , BookID , IsActive ,Cart.ModificationDate
						  from UserAddress , Cart
						  where UserAddress.UserID = @claimID AND (IsDeleted = 1 OR Cart.IsActive = 1);
			
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;

END
GO


