USE [Internet_Shop]
GO
/****** Object:  UserDefinedFunction [dbo].[ProductPriceWithSale]    Script Date: 22.12.2015 11:55:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[ProductPriceWithSale] 
(	
	-- Add the parameters for the function here
	@SaleDate date
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT ProductName,CategoryName,Price*((100-Size)/100) AS 'Price with sale' FROM
	SaleInfo INNER JOIN Sale 
	ON (SaleInfo.IdSaleInfo=Sale.IdSaleInfo AND SaleInfo.DateBegin<=@SaleDate
	AND SaleInfo.DateEnd>=@SaleDate) 
	INNER JOIN Product 
	ON Product.IdProduct=Sale.IdProduct
	INNER JOIN CategoryLast
	ON CategoryLast.IdCategory=Product.IdCategory
)






GO
