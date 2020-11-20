CREATE TABLE Users (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(20) NOT NULL,
	LastName varchar(20) NOT NULL,
	EmailId varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
    Role varchar(20) NOT NULL,
    CreatedDate varchar(40) NOT NULL,
	ModificationDate varchar(40) NOT NULL
);

CREATE TABLE WishList (
    WishListID int IDENTITY(1,1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES Users(UserID),
    BookID int FOREIGN KEY REFERENCES Books(BookID),
	IsMoved bit default 0,
	IsDeleted bit default 0,
    CreatedDate varchar(40) NOT NULL,
	ModificationDate varchar(40) NOT NULL
);

CREATE TABLE Admin (
    AdminID int IDENTITY(1,1) PRIMARY KEY,
    AdminName varchar(20) NOT NULL,
	AdminEmailId varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Role varchar(10) NOT NULL,
	Gender varchar(6) NOT NULL,
    CreatedDate varchar(40) NOT NULL,
	ModificationDate varchar(40) NOT NULL
);

CREATE TABLE Books (
    BookID int IDENTITY(1,1) PRIMARY KEY,
	AdminID int FOREIGN KEY REFERENCES Admin(AdminID),
	BookName varchar(20) NOT NULL,
	AuthorName varchar(50) NOT NULL,
	Description varchar(50) NOT NULL,
    Price varchar(20) NOT NULL,
	Pages varchar(20) NOT NULL,
	Quantity int NOT NULL,
	IsDeleted bit default 0,
    CreatedDate varchar(30) NOT NULL,
	ModificationDate varchar(30) NOT NULL,
	Image Varchar(200)
);

CREATE TABLE Cart (
    CartID int IDENTITY(1,1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES Users(UserID),
    BookID int FOREIGN KEY REFERENCES Books(BookID),
	IsActive bit default 0,
	IsDeleted bit default 0,
    CreatedDate varchar(40) NOT NULL,
	ModificationDate varchar(40) NOT NULL
);

CREATE TABLE Orders (
    OrderID int IDENTITY(1,1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES Users(UserID),
    CartID int FOREIGN KEY REFERENCES Cart(CartID),
	AddressID int FOREIGN KEY REFERENCES UserAddress(AddressID),
	IsActive bit default 0,
	IsPlaced bit default 0,
	Quantity varchar(5) NOT NULL,
	TotalPrice varchar(10) NOT NULL,
    CreatedDate varchar(40) NOT NULL,
	ModificationDate varchar(40) NOT NULL
);

CREATE TABLE UserAddress (
	AddressID int IDENTITY(1,1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES Users(UserID),
	Locality varchar(30) default 'NA',
	City varchar(30) NOT NULL,
	State varchar(30) NOT NULL,
	PhoneNumber varchar(10) NOT NULL,
	Pincode varchar(6) NOT NULL,
	LandMark varchar(30) default 'NA',
	CreatedDate varchar(30),
	ModificationDate varchar(30)
);