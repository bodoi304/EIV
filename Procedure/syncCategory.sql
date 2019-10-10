create proc dbo.Warehouse_SelectByTaxCode
@TaxCode nvarchar(20)
As
Begin
	Select Warehouse.Name TenKho,Warehouse.Code MaKho,Warehouse.Address DiaChi,Company.Code MaDV
    from Warehouse inner join Company on Warehouse.ComID = Company.ID
	where TaxCode = @TaxCode
End

GO

create proc dbo.SyncCategory_DiemXuat
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select Company.Code MaDV, Department.Name Ten,Department.Code Ma ,Department.Address DiaChi
    from UserDepartment inner join BusinessDepartment on UserDepartment.ID = BusinessDepartment.UserID
	inner join Company on UserDepartment.ComID = Company.ID
	inner join Department on Department.ID=BusinessDepartment.DepartmentID
	where username=@username and TaxCode = @TaxCode
End

go

create proc dbo.SyncCategory_NghiepVu
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select  BusinessName Ten,BusinessCode Ma 
    from Business 
	inner join BusinessDepartment on Business.BusinessID = BusinessDepartment.UserID
	inner join UserDepartment  on UserDepartment.ID = BusinessDepartment.UserID
	where username=@username and Business.TaxCode = @TaxCode
End