Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Public Class tracytest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Create a request using a URL that can receive a post. 
        Dim request As WebRequest = WebRequest.Create("http://64.58.179.22:8080/b2c/xmls ")
        ' Set the Method property of the request to POST.
        request.Method = "POST"
        'Create POST data and convert it to a byte array.
        '        Dim postData As String = "<WEBPartInquiryRequest>" & _
        '  "<Envelope>" & _
        '      "<BuyPartnerID>010055555</BuyPartnerID>" & _
        '      "<DocVersNum>1.0</DocVersNum>" & _
        '      "<DocGenBy>CandKAutomotive</DocGenBy>" & _
        '  "</Envelope>" & _
        '  "<RequestRouter>" & _
        '    "<SellPartnerID>DST</SellPartnerID>" & _
        '    "<CustNum>010055555</CustNum>" & _
        '    "<ReturnReceipt value='No'/>" & _
        '  "</RequestRouter>" & _
        '  "<PartRequestAction>" & _
        '    "<AskPayForm value='No'/>" & _
        '    "<AskShipList value='No'/>" & _
        '    "<AskCatalog value='No'>" & _
        '      "<LastMCLVersion>" & _
        '        "<Type value='Dealer'/>" & _
        '        "<TimeStamp>1/1/1900 12:00:00 AM</TimeStamp>" & _
        '        "<FileSize>0</FileSize>" & _
        '      "</LastMCLVersion>" & _
        '    "</AskCatalog>" & _
        '    "<ReturnAltLocInfo value='Always'/>" & _
        '    "<AskPriceTypes value='Yes'/>" & _
        '  "</PartRequestAction>" & _
        '  "<PartInquiryRequest>" & _
        '    "<ItemDefaults>" & _
        '      "<BackOrderCode value='Cancel'/>" & _
        '    "</ItemDefaults>" & _
        '    "<RequestItem>" & _
        '      "<ID>1</ID>" & _
        '      "<PartNum>FL1A</PartNum>" & _
        '      "<BuyerDesc/>" & _
        '      "<MfgCode>MC</MfgCode>" & _
        '      "<Qty>1</Qty>" & _
        '      "<Msg/>" & _
        '      "<ShipCode/>" & _
        '      "<RelCode value='Regular'/>" & _
        '      "<BackOrderCode value='Cancel'/>" & _
        '      "<InventoryAction value='Default'/>" & _
        '      "<Vehicle>" & _
        '        "<CatVehID/>" & _
        '        "<Year>0</Year>" & _
        '        "<Make/>" & _
        '        "<Model/>" & _
        '        "<Engine/>" & _
        '        "<SpeclCond/>" & _
        '      "</Vehicle>" & _
        '    "</RequestItem>" & _
        '    "<RequestItem>" & _
        '      "<ID>2</ID>" & _
        '      "<PartNum>FL1A</PartNum>" & _
        '      "<BuyerDesc/>" & _
        '      "<MfgCode>*</MfgCode>" & _
        '      "<Qty>1</Qty>" & _
        '      "<Msg/>" & _
        '      "<ShipCode/>" & _
        '      "<RelCode value='Regular'/>" & _
        '      "<BackOrderCode value='Cancel'/>" & _
        '      "<InventoryAction value='Default'/>" & _
        '      "<Vehicle>" & _
        '        "<CatVehID/>" & _
        '        "<Year>0</Year>" & _
        '        "<Make/>" & _
        '        "<Model/>" & _
        '        "<Engine/>" & _
        '        "<SpeclCond/>" & _
        '      "</Vehicle>" & _
        '    "</RequestItem>" & _
        '    "<RequestItem>" & _
        '      "<ID>3</ID>" & _
        '      "<PartNum>FL1ABAD</PartNum>" & _
        '      "<BuyerDesc/>" & _
        '      "<MfgCode>*</MfgCode>" & _
        '      "<Qty>1</Qty>" & _
        '      "<Msg/>" & _
        '      "<ShipCode/>" & _
        '      "<RelCode value='Regular'/>" & _
        '      "<BackOrderCode value='Cancel'/>" & _
        '      "<InventoryAction value='Default'/>" & _
        '      "<Vehicle>" & _
        '        "<CatVehID/>" & _
        '        "<Year>0</Year>" & _
        '        "<Make/>" & _
        '        "<Model/>" & _
        '        "<Engine/>" & _
        '        "<SpeclCond/>" & _
        '      "</Vehicle>" & _
        '    "</RequestItem>" & _
        '  "</PartInquiryRequest>" & _
        '"</WEBPartInquiryRequest>"

        Dim postData As String = "<WEBPartOrderRequest>" & _
 "<Envelope>" & _
   "<BuyPartnerID>010055555</BuyPartnerID>" & _
   "<DocVersNum>1.0</DocVersNum>" & _
   "<DocGenBy>CandKAutomotive</DocGenBy>" & _
 "</Envelope>" & _
 "<RequestRouter>" & _
   "<SellPartnerID>DST</SellPartnerID>" & _
   "<CustNum>010055555</CustNum>" & _
 "</RequestRouter>" & _
 "<PartOrderRequest>" & _
   "<ItemDefaults>" & _
     "<OrderType>stockorder</OrderType>" & _
     "<ShipCode/>" & _
     "<RelCode value='Regular'/>" & _
     "<Vehicle>" & _
       "<CatVehID>TESTVIN</CatVehID>" & _
       "<Year/>" & _
       "<Make/>" & _
       "<Model/>" & _
       "<Engine/>" & _
       "<SpeclCond/>" & _
     "</Vehicle>" & _
    "</ItemDefaults>" & _
   "<RequestItem>" & _
       "<ID>1</ID>" & _
       "<PartNum>PF2</PartNum>" & _
       "<MfgCode>AC</MfgCode>" & _
       "<Qty>20</Qty>" & _
    "</RequestItem>" & _
     "<RequestOrder>" & _
         "<OrderMsg/>" & _
         "<PONumber/>" & _
     "</RequestOrder>" & _
    "</PartOrderRequest>" & _
    "</WEBPartOrderRequest>"



        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
        ' Set the ContentType property of the WebRequest.
        request.ContentType = "application/x-www-form-urlencoded"
        ' Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length
        ' Get the request stream.
        Dim dataStream As Stream = request.GetRequestStream()
        ' Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length)
        ' Close the Stream object.
        dataStream.Close()
        ' Get the response.
        Dim response As WebResponse = request.GetResponse()
        ' Display the status.
        Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
        ' Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream()
        ' Open the stream using a StreamReader for easy access.
        Dim reader As New StreamReader(dataStream)
        ' Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()
        ' Display the content.
        Label1.Text = responseFromServer
        ' Clean up the streams.
        reader.Close()
        dataStream.Close()
        response.Close()






    End Sub

End Class