USE [StoreSample]
GO


CREATE PROCEDURE [dbo].[sp_Sales_Order_Prediction]
	AS

		SELECT C.custid, C.[companyname] AS CustomerName,
		 MAX(O.OrderDate) AS LastOrderDate,
		 OrderDate  AS NextPredictedOrder
		FROM [Sales].[Customers] AS C
			 JOIN  [Sales].[Orders] AS O
			 ON C.custid = O.custid
		GROUP BY C.companyname, OrderDate, C.custid
		ORDER BY C.companyname, OrderDate, C.custid

GO


CREATE procedure [sp_Sales_Order_Insert](
@SaleOrder_xml xml,
@OrderId int output
)
as


	
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

		insert into @orderdetail(productid,unitprice,qty,discount)
		select 
			nodo.elemento.value('Productid[1]','int') as Productid,
			nodo.elemento.value('Unitprice[1]','decimal(10,2)') as Unitprice,
			nodo.elemento.value('Qty[1]','int') as Qty,
			nodo.elemento.value('Discount[1]','numeric(4,3)') as Discount
		from @SaleOrder_xml.nodes('Order/OrderDetail/Item') nodo(elemento)


		--================================================
		-- EMPIEZA EL REGISTRO DE LA VENTA
		--================================================
		declare @identity as table(ID int)
		declare @id int = (SELECT isnull(max(orderid),0) +1 FROM [Sales].[Orders] )

		INSERT INTO [Sales].[Orders] (empid, shipperid, shipname, shipaddress, shipcity, orderdate, requireddate, shippeddate, freight, shipcountry)
		output inserted.orderid into @identity
		select Empid, Shipperid, Shipname ,Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry from @order
		

		update @orderdetail set orderid = (select ID from @identity)

		insert into [Sales].[OrderDetails] (orderid,productid,unitprice,qty,discount)
		select orderid, Productid ,Unitprice,Qty,Discount from @orderdetail


		set @OrderId = @id
