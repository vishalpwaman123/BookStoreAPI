CREATE PROCEDURE [dbo].[spBookFilter]
(
@FirstPrice int , 
@FinalPrice int 
)
AS
BEGIN
	
	DECLARE @status int
	set @status=0

	BEGIN TRY
    -- Insert statements for procedure here
		IF EXISTS (SELECT * FROM Books WHERE Price BETWEEN @FirstPrice AND @FinalPrice)
		BEGIN
					
								SELECT * FROM Books
								WHERE Price BETWEEN @FirstPrice AND @FinalPrice 

		END
		else
		BEGIN
			
							SELECT BookID=0 FROM Books

		END
	END TRY
	BEGIN CATCH  

		ROLLBACK TRANSACTION
		SELECT ERROR_MESSAGE() AS ErrorMessage;  
	
	END CATCH;


END
GO
