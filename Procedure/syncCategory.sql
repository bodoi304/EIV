create proc [dbo].[Warehouse_SelectByTaxCode]
@TaxCode nvarchar(20)
As
Begin
	Select Warehouse.Id,Warehouse.Name TenKho,Warehouse.Code MaKho,Warehouse.Address DiaChi,Company.Code MaDV
    from Warehouse inner join Company on Warehouse.ComID = Company.ID
	where TaxCode = @TaxCode
End

GO

create proc [dbo].[SyncCategory_DiemXuat]
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select Department.id,Company.Code MaDV, Department.Name Ten,Department.Code Ma ,Department.Address DiaChi
    from UserDepartment inner join BusinessDepartment on UserDepartment.ID = BusinessDepartment.UserID
	inner join Company on UserDepartment.ComID = Company.ID
	inner join Department on Department.ID=BusinessDepartment.DepartmentID
	where username=@username and TaxCode = @TaxCode
End


go

create proc [dbo].[SyncCategory_NghiepVu]
@username nvarchar(64),
@TaxCode nvarchar(20)
As
Begin
	Select  Business.BusinessID id,BusinessName Ten,BusinessCode Ma 
    from Business 
	inner join BusinessDepartment on Business.BusinessID = BusinessDepartment.UserID
	inner join UserDepartment  on UserDepartment.ID = BusinessDepartment.UserID
	where username=@username and Business.TaxCode = @TaxCode
End


create proc [dbo].[InvTemplate_GetTemplateInvoice]
@pattern nvarchar(12),
@TaxCode nvarchar(20)
As
Begin
	 SELECT TOP 1000 a.[Id]
      ,a.[InvCateID]
      ,[InvCateName]
      ,[TemplateName]
      ,[XsltFile]
      ,[ServiceType]
      ,[InvoiceType]
      ,[InvoiceView]
      ,[IGenerator]
      ,[IViewer]
      ,[ParseService]
      ,[ImagePath]
      ,[IsPub]
	  ,a.CssData
	  ,a.CssLogo
	  ,a.CssBackgr
  FROM InvTemplate a inner join RegisterTemp b
  inner join Company c on  b.ComId = c.Id
  on  a.id = b.tempID where b.InvPattern=@pattern and c.TaxCode  =@TaxCode
End


create procedure [dbo].[userdata_CheckUserAPI]
@username nvarchar(50),
@TaxCode nvarchar(20),
@Type int
As
Begin
  SELECT distinct a.*,c.TaxCode
  FROM userdata a inner join  BusinessDepartment b
  on  a.userid = b.UserID
  inner join Business c
  on  b.BusinessID = c.BusinessID where 
  a.username =@username 
  and a.Type= @Type
  and c.TaxCode  =@TaxCode