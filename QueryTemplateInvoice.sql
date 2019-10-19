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