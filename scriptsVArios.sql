select * from [Sales].[Orders] ORDER BY orderid Desc

select [orderid],[requireddate], [shippeddate] ,[shipaddress], [shipcity], [custid] from [Sales].[Orders] ORDER BY orderid Desc
select [orderid],[requireddate], [shippeddate] ,[shipaddress], [shipcity], [custid] from [Sales].[Orders] where [custid] = 85
SELECT [empid], [firstname] + ' ' + [lastname] AS FullName from [HR].[Employees] WITH(NOLOCK)
select [shipperid], [companyname]  from [Sales].[Shippers]
select productid, [productname]  from [Production].[Products]




    INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
    VALUES (9, 3, 'Shipper ZHISN','XXXXX', 'BOGOTA', '2023-03-13', '2023-03-13', '2023-03-13', 45, 'COLOMBIA' )
/*
	    DROP PROCEDURE [dbo].[sp_Sales_Order_Insert]
    CREATE PROCEDURE [dbo].[sp_Sales_Order_Insert]
	@empid int,
	@shipperid      INT,
	@shipname       NVARCHAR (40),
	@shipaddress   NVARCHAR (60),
	@shipcity       NVARCHAR (15),
	@orderdate      DATETIME,
	@requireddate   DATETIME ,
	@shippeddate    DATETIME ,
	@freight        MONEY,
	@shipcountry    NVARCHAR (15)
AS


   INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
   VALUES (@empid, @shipperid, @shipname ,@shipaddress, @shipcity, @orderdate, @requireddate, @shippeddate, @freight, @shipcountry )

   SELECT SCOPE_IDENTITY() AS orderID;
GO

*/

   INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
    VALUES (9, 3, 'Shipper ZHISN','XXXXX', 'BOGOTA', '2023-03-13', '2023-03-13', '2023-03-13', 45, 'COLOMBIA' )

	EXEC [dbo].[sp_Sales_Order_Insert] 9, 3, 'Shipper ZHISN','XXXXX', 'ECUADIR', '2023-03-13', '2023-03-13', '2023-03-13', 45, 'QUITO'

	EXEC sp_Sales_Order_Prediction
	/*

	CREATE PROCEDURE [dbo].[sp_Sales_Order_Prediction]
	AS

		SELECT C.custid, C.[companyname] AS CustomerName,
		 MAX(O.OrderDate) AS LastOrderDate,
		 OrderDate  AS NextPredictedOrder
		FROM [Sales].[Customers] AS C
			 JOIN  [Sales].[Orders] AS O
			 ON C.custid = O.custid
		GROUP BY C.companyname, OrderDate, C.custid
		ORDER BY C.companyname, OrderDate, 
		GO
		*/
	/*

	CREATE PROCEDURE [dbo].[sp_Sales_Order_Insert]
	@empid int,
	@shipperid      INT,
	@shipname       NVARCHAR (40),
	@shipaddress   NVARCHAR (60),
	@shipcity       NVARCHAR (15),
	@orderdate      DATETIME,
	@requireddate   DATETIME ,
	@shippeddate    DATETIME ,
	@freight        MONEY,
	@shipcountry    NVARCHAR (15)
AS


   INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
   VALUES (@empid, @shipperid, @shipname ,@shipaddress, @shipcity, @orderdate, @requireddate, @shippeddate, @freight, @shipcountry )
GO

*/

   INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
    VALUES (9, 3, 'Shipper ZHISN','XXXXX', 'BOGOTA', '2023-03-13', '2023-03-13', '2023-03-13', 45, 'COLOMBIA' )

	/*
CREATE TABLE [Sales].[Orders] (
    empid         INT           NOT NULL,
    orderdate      DATETIME      NOT NULL,
    requireddate   DATETIME      NOT NULL,
    shippeddate    DATETIME      NULL,
    shipperid      INT           NOT NULL,
    freight        MONEY         NOT NULL,
    shipname       NVARCHAR (40) NOT NULL,
    shipaddress   NVARCHAR (60) NOT NULL,
    shipcity       NVARCHAR (15) NOT NULL,
    shipregion     NVARCHAR (15) NULL,
    shippostalcode NVARCHAR (10) NULL,
    shipcountry    NVARCHAR (15) NOT NULL
);

create procedure [sp_Sales_Order_Insertar](
@SaleOrder_xml xml,
@OrderId int output
)
as
begin

	
	declare @order table (
		 empid int,
		 shipperid      INT,
		 shipname       NVARCHAR (40),
		 shipaddress   NVARCHAR (60),
		 shipcity       NVARCHAR (15),
		 orderdate      DATETIME,
		 requireddate   DATETIME ,
		 shippeddate    DATETIME ,
		 freight        MONEY,
		 shipcountry    NVARCHAR (15)
	)

	declare @orderdetail table (
	orderid int default 0,
	productid int,
	unitprice money,
	qty smallint,
	discount numeric(4,3)
	)
	
	BEGIN TRY
		BEGIN TRANSACTION

		insert into @order(empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
		select 
			nodo.elemento.value('Empid[1]','int') as Empid,
			nodo.elemento.value('Shipperid[1]','int') as Shipperid,
			nodo.elemento.value('Shipname[1]','nvarchar(40)') as Shipname,
			nodo.elemento.value('Shipaddress[1]','nvarchar(60)') as Shipaddress,
			nodo.elemento.value('Shipcity[1]','nvarchar(15)') as Shipcity,
			nodo.elemento.value('Orderdate[1]','datetime') as Orderdate,
			nodo.elemento.value('Requireddate[1]','datetime') as Requireddate,
			nodo.elemento.value('Shippeddate[1]','datetime') as Shippeddate,
			nodo.elemento.value('Freight[1]','money') as Freight,
			nodo.elemento.value('Shipcountry[1]','nvarchar(15)') as Shipcountry
		from @SaleOrder_xml.nodes('Order') nodo(elemento)

		insert into @orderdetail(productid,unitprice,qty,discount)
		select 
			nodo.elemento.value('Productid[1]','int') as Productid,
			nodo.elemento.value('Unitprice[1]','decimal(10,2)') as Unitprice,
			nodo.elemento.value('Qty[1]','int') as Qty,
			nodo.elemento.value('Discount[1]','decimal(10,2)') as Discount
		from @SaleOrder_xml.nodes('Order/OrderDetail/Item') nodo(elemento)


		--================================================
		-- EMPIEZA EL REGISTRO DE LA VENTA
		--================================================
		declare @identity as table(ID int)
		declare @id int = (SELECT isnull(max(orderid),0) +1 FROM [Sales].[Orders] )

		INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
		output inserted.orderid into @identity
		select Empid, Shipperid, Shipname ,Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry from @order
		
		SET @OrderId = SELECT SCOPE_IDENTITY();

		update @orderdetail set orderid = (select ID from @identity)

		insert into [Sales].[OrderDetails] (orderid,productid,unitprice,qty,discount)
		select orderid, Productid ,Unitprice,Qty,Discount from @orderdetail

		COMMIT
		set @OrderId = @id

	END TRY
	BEGIN CATCH
		ROLLBACK
		set @OrderId = 0
	END CATCH

end

go


*/
select * from [Sales].[Orders] ORDER BY orderid Desc
select * from [Sales].[OrderDetails] ORDER BY orderid Desc

--Declare @SaleOrder_xml xml = '<Order>
--  <Empid>1</Empid>
--  <Shipperid>1</Shipperid>
--  <Shipname>JOSE</Shipname>
--  <Shipaddress>kr34</Shipaddress>
--  <Shipcity>madrid</Shipcity>
--  <Orderdate>2023-03-14</Orderdate>
--  <Requireddate>2023-03-14</Requireddate>
--  <Shippeddate>2023-03-14</Shippeddate>
--  <Freight>2</Freight>
--  <Shipcountry>Espana</Shipcountry>
--  <OrderDetail>
--    <Item>
--      <Productid>1</Productid>
--      <Unitprice>1</Unitprice>
--      <Qty>1</Qty>
--      <Discount>1</Discount>
--    </Item>
--    <Item>
--      <Productid>2</Productid>
--      <Unitprice>2</Unitprice>
--      <Qty>2</Qty>
--      <Discount>1</Discount>
--    </Item>
--  </OrderDetail>
--</Order>'

Declare @SaleOrder_xml xml = '<Order>
  <Empid>3</Empid>
  <Shipperid>3</Shipperid>
  <Shipname>aaa</Shipname>
  <Shipaddress>sss</Shipaddress>
  <Shipcity>ddd</Shipcity>
  <Orderdate>2023-03-21</Orderdate>
  <Requireddate>2023-03-21</Requireddate>
  <Shippeddate>2023-03-21</Shippeddate>
  <Freight>111</Freight>
  <Shipcountry>fff</Shipcountry>
  <OrderDetail>
    <Item>
      <Productid>51</Productid>
      <Unitprice>1</Unitprice>
      <Qty>2</Qty>
      <Discount>33.001</Discount>
    </Item>
  </OrderDetail>
</Order>'


--select @SaleOrder_xml


	declare @order table (
		 empid int,
		 shipperid      INT,
		 shipname       NVARCHAR (40),
		 shipaddress   NVARCHAR (60),
		 shipcity       NVARCHAR (15),
		 orderdate      DATETIME,
		 requireddate   DATETIME ,
		 shippeddate    DATETIME ,
		 freight        MONEY,
		 shipcountry    NVARCHAR (15)
	)

	declare @orderdetail table (
	orderid int default 0,
	productid int,
	unitprice money,
	qty smallint,
	discount numeric(4,3)
	)

			insert into @order(empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
		select 
			nodo.elemento.value('Empid[1]','int') as Empid,
			nodo.elemento.value('Shipperid[1]','int') as Shipperid,
			nodo.elemento.value('Shipname[1]','nvarchar(40)') as Shipname,
			nodo.elemento.value('Shipaddress[1]','nvarchar(60)') as Shipaddress,
			nodo.elemento.value('Shipcity[1]','nvarchar(15)') as Shipcity,
			nodo.elemento.value('Orderdate[1]','datetime') as Orderdate,
			nodo.elemento.value('Requireddate[1]','datetime') as Requireddate,
			nodo.elemento.value('Shippeddate[1]','datetime') as Shippeddate,
			nodo.elemento.value('Freight[1]','money') as Freight,
			nodo.elemento.value('Shipcountry[1]','nvarchar(15)') as Shipcountry
		from @SaleOrder_xml.nodes('Order') nodo(elemento)


		Select * from @order

	--	insert into @orderdetail(productid,unitprice,qty,discount)
		select 
			nodo.elemento.value('Productid[1]','int') as Productid,
			nodo.elemento.value('Unitprice[1]','decimal(10,2)') as Unitprice,
			nodo.elemento.value('Qty[1]','int') as Qty,
			nodo.elemento.value('Discount[1]','numeric(4,3)') as Discount
		from @SaleOrder_xml.nodes('Order/OrderDetail/Item') nodo(elemento)

	Select * from @orderdetail

		declare @identity as table(ID int)
		declare @id int = (SELECT isnull(max(orderid),0) +1 FROM [Sales].[Orders] )

		INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
		output inserted.orderid into @identity
		select Empid, Shipperid, Shipname ,Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry from @order

		update @orderdetail set orderid = (select ID from @identity)

	insert into [Sales].[OrderDetails] (orderid,productid,unitprice,qty,discount)
	select orderid, Productid ,Unitprice,Qty,Discount from @orderdetail



SELECT O.orderid, O.custid, O.empid, O.shipperid, O.orderdate,
  CAST(SUM(OD.qty * OD.unitprice * (1 - discount))
       AS NUMERIC(12, 2)) AS val
FROM Sales.Orders AS O
  JOIN Sales.OrderDetails AS OD
    ON O.orderid = OD.orderid
GROUP BY O.orderid, O.custid, O.empid, O.shipperid, O.orderdate;



    SELECT * FROM [Sales].Orders where custid = 72

SELECT 
  custid, 
  AvgLag = AVG(CONVERT(decimal(7,2), DATEDIFF(DAY, PriorDate, OrderDate))),
  PriorDate,
  OrderDate
FROM
(
  SELECT custid, OrderDate, PriorDate = LAG(OrderDate,1) 
    OVER (PARTITION BY custid ORDER BY OrderDate)
  FROM [Sales].[Orders]
  WHERE custid = 72 AND YEAR(OrderDate) BETWEEN 2006 AND 2008
) AS lagged
GROUP BY custid,PriorDate, OrderDate



SELECT custid, AVG(DATEDIFF(day, prev_order.OrderDate, Orders.OrderDate)) AS AvgDaysBetweenOrders, prev_order.OrderDate, Orders.OrderDate
FROM [Sales].[Orders]
OUTER APPLY (
   SELECT TOP 1 OrderDate FROM [Sales].[Orders] prev
   WHERE prev.custid = Orders.custid AND prev.OrderDate < Orders.OrderDate 
   ORDER BY prev.OrderDate DESC
) prev_order
GROUP BY custid, prev_order.OrderDate, Orders.OrderDate

SELECT CONVERT(date, OrderDate) AS date, AVG(DATEDIFF(DAY, OrderDate, specified_datetime)) AS daily_average
FROM [Sales].[Orders]
GROUP BY CONVERT(date, OrderDate);


SELECT C.[companyname] AS CustomerName,
     MAX(O.OrderDate) AS LastOrderDate,
     OrderDate  AS NextPredictedOrder
    FROM [Sales].[Customers] AS C
         JOIN  [Sales].[Orders] AS O
         ON C.custid = O.custid
GROUP BY C.companyname, OrderDate
ORDER BY C.companyname, OrderDate




WITH cte AS (
  SELECT OrderID, OrderDate,
         DATEDIFF(day, LAG(OrderDate, 1) OVER (ORDER BY OrderDate), OrderDate) AS days_between_orders
  FROM Sales.Orders   where custid = 72
),
avg_days AS (
  SELECT AVG(days_between_orders) AS avg_days_between_orders
  FROM cte
  WHERE days_between_orders IS NOT NULL
)
-- Use the average time to predict the next order date
SELECT DATEADD(day, avg_days_between_orders, MAX(OrderDate)) AS NextPredictedOrder
FROM  Sales.Orders   where custid = 72

WITH cte AS (
    SELECT 
        custid, OrderDate,
        AVG(DATEDIFF(day, LAG(OrderDate) OVER (PARTITION BY custid ORDER BY OrderDate), OrderDate)) 
        OVER (PARTITION BY custid) AS AvgDaysBetweenOrders
    FROM Sales.Orders
)
SELECT 
    CustomerId,
    DATEADD(day, AvgDaysBetweenOrders, MAX(OrderDate)) AS NextPredictedOrder
FROM cte
GROUP BY custid, AvgDaysBetweenOrders