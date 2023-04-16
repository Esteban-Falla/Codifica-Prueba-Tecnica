USE StoreSample;
GO

IF OBJECT_ID('Sales.LastAndNextOrderDate','V') IS NOT NULL
	DROP VIEW Sales.LastAndNextOrderDate;
GO

CREATE VIEW Sales.LastAndNextOrderDate
AS

WITH
	dates AS
	(
		SELECT 
			o.custid,
			init_date = o.orderdate,
			next_date = LEAD(o.orderdate) OVER(ORDER BY o.orderdate ASC)
		FROM Sales.Orders o 
	)
SELECT
	c.contactname [CustomerName],
	o.orderdate [LastOrderDate],
	DATEADD(day,AVG(DATEDIFF(day,d.init_date,d.next_date)),o.orderdate) [NextPredictedOrder]
FROM Sales.Customers c
JOIN Sales.Orders o ON (c.custid = o.custid)
LEFT OUTER JOIN Sales.Orders o2 ON (c.custid = o2.custid AND
	(o.orderdate< o2.orderdate OR (o.orderdate = o2.orderdate AND o.orderid  < o2.orderid)))
INNER JOIN dates d ON c.custid = d.custid
WHERE o2.orderid IS NULL
GROUP BY c.contactname , o.orderdate;
GO