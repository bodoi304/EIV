/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 a.[Id]
      ,a.[InvCateID]
      ,[InvCateName]
      ,[TemplateName]
      ,[XmlFile]
      ,[XsltFile]
      ,[ServiceType]
      ,[InvoiceType]
      ,[InvoiceView]
      ,[IGenerator]
      ,[IViewer]
      ,[ParseService]
      ,[ImagePath]
      ,[IsPub]

  FROM [test_hddt_pvoil3].[dbo].[InvTemplate] a inner join RegisterTemp b
  on  a.id = b.tempID
  inner join Company c on b.ComId = c.ID 
   where c.TaxCode = '0305795054' and b.InvPattern= '01GTKT0/001' 


   USE [test_hddt_pvoil3]
GO
/****** Object:  StoredProcedure [dbo].[InvTemplate_GetTemplateInvoice]    Script Date: 10/20/2019 11:23:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[InvTemplate_GetTemplateInvoice]
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