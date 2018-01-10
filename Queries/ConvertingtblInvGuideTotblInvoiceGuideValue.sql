---Make sure you Use PartsManager..
declare @i int,@a int
declare @holderTable table
(
AutoID int Identity(1,1),
CompanyID int,
InvoiceElementID int,
Value varchar(4000)
)
declare @query varchar(max)

set @i=0

while(@i<=42)---(select count(*) from tblInvoiceGuideElement)
Begin
Print @i
set @query='select companyID,'+ Cast(@i as varchar(3)) +' as InvoiceGuideElementID,'+(select Elementname from tblInvoiceGuideElement where InvoiceGuideElementID=@i) + ' from tblInvGuide'
Insert Into @holderTable Execute(@query)
set @a=0
While(@a<=(select count(*) from @holderTable))
Begin
If (select count(*) from tblInvoiceGuideValue where companyID=(select companyID from @holderTable where AutoID=@a) and InvoiceGuideElementID=@i) > 0
Begin
Print 'Company Already In'
End
Else
Begin
Insert Into tblInvoiceGuideValue select InvoiceElementID,companyID,Value from @holderTable where AutoID=@a
End
set @a=@a+1
End
set @i=@i+1
End