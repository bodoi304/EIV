USE [test_hddt_pvoil6]
GO

/****** Object:  StoredProcedure [dbo].[Warehouse_SelectByTaxCode]    Script Date: 11/2/2019 12:05:58 AM ******/
DROP PROCEDURE [dbo].[Warehouse_SelectByTaxCode]
GO

/****** Object:  StoredProcedure [dbo].[SyncCategory_NghiepVu]    Script Date: 11/2/2019 12:05:58 AM ******/
DROP PROCEDURE [dbo].[SyncCategory_NghiepVu]
GO

/****** Object:  StoredProcedure [dbo].[SyncCategory_DiemXuat]    Script Date: 11/2/2019 12:05:58 AM ******/
DROP PROCEDURE [dbo].[SyncCategory_DiemXuat]
GO

/****** Object:  StoredProcedure [dbo].[SyncCategory_DiemXuat]    Script Date: 11/2/2019 12:05:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SyncCategory_DiemXuat]
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select Department.id,Company.Code MaDV, Department.Name Ten,Department.Code Ma ,Department.Address DiaChi
    from Department 
	inner join Company on Department.ComID = Company.ID
     where TaxCode = @taxcode;

	
	
	--from UserDepartment inner join BusinessDepartment on UserDepartment.ID = BusinessDepartment.UserID
	--inner join Company on UserDepartment.ComID = Company.ID
	--inner join Department on Department.ID=BusinessDepartment.DepartmentID
	--where username=@username and  Department.comId in (select id from company where taxCode = @TaxCode)
End
GO

/****** Object:  StoredProcedure [dbo].[SyncCategory_NghiepVu]    Script Date: 11/2/2019 12:05:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SyncCategory_NghiepVu]
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select  Business.BusinessID id,BusinessName Ten,BusinessCode Ma 
    from Business 
		where  Business.TaxCode =  @TaxCode	
	--inner join BusinessDepartment on Business.BusinessID = BusinessDepartment.UserID
	--inner join UserDepartment  on UserDepartment.ID = BusinessDepartment.UserID
	--where username=@username and Business.TaxCode = @TaxCode
End
GO

/****** Object:  StoredProcedure [dbo].[Warehouse_SelectByTaxCode]    Script Date: 11/2/2019 12:05:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Warehouse_SelectByTaxCode]
@TaxCode nvarchar(20)
As
Begin
	Select Warehouse.Id,Warehouse.Name TenKho,Warehouse.Code MaKho,Warehouse.Address DiaChi,Company.Code MaDV
    from Warehouse inner join Company on Warehouse.ComID = Company.ID
	where TaxCode = @TaxCode
End
GO

