declare @companyInfo Table
(
autoID int Identity(1,1),
companyID int,
vendorShippingAddressID int,
vanityName varchar(max)
)  
insert into @companyInfo(companyID,vanityName,vendorShippingAddressID) select C.companyID,C.VanityShippingName,VSA.VendorShippingID from tblCompany as C inner join tblVendorShippingAddress as VSA on C.CompanyID=VSA.CompanyID where type='Vendor' 
update @companyInfo set vanityName='' where vanityName is null
Insert Into tblVanityShippingAddress select vendorShippingAddressID,vanityName from @companyInfo