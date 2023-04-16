USE StoreSample;
GO

IF OBJECT_ID('Sales.AddOrder','P') IS NOT NULL
	DROP VIEW Sales.AddOrder;
GO

CREATE PROCEDURE Sales.AddOrder
	@EmpId INT,
	@ShipperId INT,
	@ShipName NVARCHAR(40),
	@ShipAddr NVARCHAR(60),
	@ShipCity NVARCHAR(15),
	@OrderDate DATETIME,
	@ReqDate DATETIME,
	@ShipDate DATETIME,
	@Freight MONEY,
	@ShipCountry NVARCHAR(15),
	@ProdId INT,
	@UnitPrice MONEY,
	@Qty SMALLINT,
	@Discount NUMERIC(4,3)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO Sales.Orders (
		empid,
		shipperid,
		shipname,
		shipaddress,
		shipcity,
		orderdate,
		requireddate,
		shippeddate,
		freight,
		shipcountry)
	VALUES (
		@EmpId,
		@ShipperId,
		@ShipName,
		@ShipAddr,
		@ShipCity,
		@OrderDate,
		@ReqDate,
		@ShipDate,
		@Freight,
		@ShipCountry
	);

	DECLARE @OrderId INT;
	SELECT @OrderId = SCOPE_IDENTITY();

	INSERT INTO Sales.OrderDetails (
		orderid,
		productid,
		unitprice,
		qty,
		discount)
	VALUES (
		@OrderId,
		@ProdId,
		@UnitPrice,
		@Qty,
		@Discount
	);
	
END
GO

	
