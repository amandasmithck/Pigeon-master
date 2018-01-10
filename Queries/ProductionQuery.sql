declare @statement varchar(max), @i int, @client varchar(max)
--set @i=14
set @i=1
while @i<=(select count(client) from PartsManager.dbo.tblPigeonClients)
Begin
set @client=(select top 1 client from PartsManager.dbo.tblPigeonClients where ClientID=@i)
If @client='CK'
Begin
set @client='PartsManager'
End
Else If @client='GoPower'
Begin
set @client='GoPowerTrain'
End
set @statement=CONCAT('use [',@client,']; alter table aspnet_Membership add Password2 nvarchar(128);')
Print @statement
Exec(@statement)
set @statement=CONCAT('use [',@client,'];update aspnet_Membership set Password2=[Password];')
Print @statement
Exec(@statement)
set @i=@i+1
End