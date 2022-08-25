-- Server=tcp:adamdev.database.windows.net,1433;Initial Catalog=AdamTest;Persist Security Info=False;User ID=adam;Password=Move123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

IF OBJECT_ID('[Order]', 'U') IS NOT NULL
DROP TABLE [Order]
GO

IF OBJECT_ID('Product', 'U') IS NOT NULL
DROP TABLE Product
GO

IF OBJECT_ID('Customer', 'U') IS NOT NULL
DROP TABLE Customer
GO

IF OBJECT_ID('Category', 'U') IS NOT NULL
DROP TABLE Category
GO

IF OBJECT_ID('Segment', 'U') IS NOT NULL
DROP TABLE Segment
GO

IF OBJECT_ID('Region', 'U') IS NOT NULL
DROP TABLE Region
GO

IF OBJECT_ID('Shipping', 'U') IS NOT NULL
DROP TABLE Shipping
GO



Create Table Category
(
    CatID INT PRIMARY KEY,
    CatName [NVARCHAR] (50)
);
GO

Create Table Product
(
    ProdID [NVARCHAR] (200) PRIMARY KEY,
    Description [NVARCHAR] (200),
    UnitPrice INT, 
    CatID INT,
    FOREIGN KEY(CatID) REFERENCES [Category] (CatID)
);
GO

Create Table Segment
(
    SegID INT PRIMARY KEY,
    SegName [NVARCHAR] (50)
);
GO

Create Table Region
(
    Region [NVARCHAR] (100) PRIMARY KEY
);
GO



Create Table Customer
(
    CustID [NVARCHAR] (100) PRIMARY KEY,
    FullName [NVARCHAR] (100),
    Country [NVARCHAR] (100), 
    City [NVARCHAR] (100),
    State [NVARCHAR] (100),
    PostCode INT,
    SegID INT,
    Region [NVARCHAR] (100),
    FOREIGN KEY(SegID) REFERENCES [Segment] (SegID),
    FOREIGN KEY(Region) REFERENCES [Region] (Region)
);
GO

Create Table Shipping
(
    ShipMode [NVARCHAR] (100) PRIMARY KEY
);
GO

Create Table [Order]
(
    OrderDate [NVARCHAR] (100),
    Quantity INT,
    ShipDate [NVARCHAR] (100),
    ShipMode [NVARCHAR] (100),
    ProdID [NVARCHAR] (200),
    CustID [NVARCHAR] (100),
    FOREIGN KEY (ShipMode) REFERENCES [Shipping] (ShipMode),
    FOREIGN KEY (ProdID) REFERENCES [Product] (ProdID),
    FOREIGN KEY (CustID) REFERENCES [Customer] (CustID),
    CONSTRAINT PK_ORDER PRIMARY KEY (OrderDate, ProdID, CustID)
);
GO





INSERT INTO Category (CatID, CatName)
VALUES
    (1, 'Furniture'),
    (2, 'Office Supplies'),
    (3, 'Technology')

INSERT INTO Segment (SegID, SegName)
VALUES
    (1, 'Consumer'),
    (2, 'Corporate'),
    (3, 'Home Office')

INSERT INTO Product (ProdID, CatID, Description, UnitPrice)
VALUES
    ('FUR-BO-10001798', 1, 'Bush Somerset Collection Bookcase', 261.96),
    ('FUR-CH-10000454', 3, 'Mitel 5320 IP Phone VoIP phone', 731.94),
    ('OFF-LA-10000240', 2, 'Self-Adhesive Address Labels for Typewriters by Universal', 14.62)

INSERT INTO Region (Region)
VALUES
    ('South'),
    ('Central'),
    ('West'),
    ('East'),
    ('North')

INSERT INTO Shipping (ShipMode)
VALUES
    ('Second Class'),
    ('Standard Class'),
    ('First Class'),
    ('Overnight Express')



INSERT INTO Customer (CustID, FullName, SegID, Country, City, State, PostCode, Region)
VALUES
    ('CG-12520', 'Claire Gute', 1, 'United States', 'Henderson', 'Oklahoma', 42420 ,'Central'),
    ('DV-13045', 'Darrin Van Huff', 2, 'United States', 'Los Angeles', 'California', 90036, 'West'),
    ('SO-20335', 'Sean O''Donnell', 1, 'United States', 'Fort Lauderdale', 'Florida', 33311, 'South'),
    ('BH-11710', 'Brosina Hoffman', 3, 'United States', 'Los Angeles', 'California', 90032, 'West')

INSERT INTO [Order] (CustID, ProdID, OrderDate, Quantity, ShipDate, ShipMode)
VALUES
('CG-12520',	'FUR-BO-10001798',	'2016-11-08',	2,	'2016-11-11',	'Second Class'),
('CG-12520',	'FUR-CH-10000454',	'2016-11-08',	3,	'2016-11-11',	'Second Class'),
('CG-12520',	'OFF-LA-10000240',	'2016-06-12',	2,	'2016-06-16',	'Second Class'),
('DV-13045',	'OFF-LA-10000240',	'2015-11-21',	2,	'2015-11-26',	'Second Class'),
('DV-13045',	'OFF-LA-10000240',	'2014-10-11',	1,	'2014-10-15',	'Standard Class'),
('DV-13045',	'FUR-CH-10000454',	'2016-11-12',	9,	'2016-11-16',	'Standard Class'),
('SO-20335',	'OFF-LA-10000240',	'2016-09-02',	5,	'2016-09-08',	'Standard Class'),
('SO-20335',	'FUR-BO-10001798',	'2017-08-25',	2,	'2017-08-29',	'Overnight Express'),
('SO-20335',	'FUR-CH-10000454',	'2017-06-22',	2,	'2017-06-26',	'Standard Class'),
('SO-20335',	'FUR-BO-10001798',	'2017-05-01',	3,	'2017-05-02',	'First Class')


