Public Class ConnectionStrings

    Public Shared CKConnectionString = ConnectionStrings.GetSpecificConnectionString("CK")

    Public Shared Sub SetSessionConnectionStringsAndProviders(strURL As String)

        Dim customerIdentity As String = String.Empty

        Select Case strURL.ToLower()

            Case "anpcclearwater.com", "www.anpcclearwater.com", "autowaypigeon.ckautoparts.com", "autowayparts.com", "www.autowayparts.com", "autowaypartscenterclearwater.com", "www.autowaypartscenterclearwater.com", "autowaybeta.ckautoparts.com", "www.autowaybeta.ckautoparts.com" 'Autoway
                customerIdentity = "Autoway"

            Case "larrymillersandbox.ckautoparts.com", "larrymillerpigeon.ckautoparts.com", "larryhmillerpowertrain.com", "www.larryhmillerpowertrain.com", "larrymillerbeta.ckautoparts.com", "www.larrymillerbeta.ckautoparts.com" 'Larry Miller
                customerIdentity = "LarryMiller"

            Case "gosandbox.ckautoparts.com", "go", "anpcdenver.com", "www.anpcdenver.com", "gopigeon.ckautoparts.com", "gopartscenter.com", "www.gopartscenter.com", "gobeta.ckautoparts.com", "www.gobeta.ckautoparts.com" 'Go
                customerIdentity = "GO"

            Case "bigvalleypigeon.ckautoparts.com", "bigvalleypowertrain.com", "www.bigvalleypowertrain.com"
                customerIdentity = "BigValley"

            Case "fitzpigeon.ckautoparts.com", "fitzparts.com", "www.fitzparts.com", "fitz.ckautoparts.com", "www.fitz.ckautoparts.com"
                customerIdentity = "Fitz"

            Case "dickmyerspigeon.ckautoparts.com", "dickmyerspowertrain.com", "www.dickmyerspowertrain.com", "dickmyers.ckautoparts.com", "www.dickmyers.ckautoparts.com"
                customerIdentity = "DickMyers"

            Case "dupratt.ckautoparts.com", "duprattpowertrain.com", "www.duprattpowertrain.com"
                customerIdentity = "DuPratt"

            Case "quirkpigeon.ckautoparts.com", "quirkpowertrain.com", "www.quirkpowertrain.com", "quirk.ckautoparts.com", "www.quirk.ckautoparts.com"
                customerIdentity = "Quirk"

            Case "gaudinpowertrain.com", "www.gaudinpowertrain.com"
                customerIdentity = "Gaudin"

            Case "autonation.ckautoparts.com", "www.autonation.ckautoparts.com"
                customerIdentity = "AutoNation"

            Case "anpcmesa.com", "www.anpcmesa.com"
                customerIdentity = "Mesa"

            Case "gopowersandbox.ckautoparts.com", "parts.go-powertrain.com", "www.parts.go-powertrain.com", "gopowertrain.ckautoparts.com", "www.gopowertrain.ckautoparts.com"
                customerIdentity = "GoPower"

            Case "fvpsandbox.ckautoparts.com", "fvppowertrain.com", "www.fvppowertrain.com", "fmp.ckautoparts.com", "www.fmp.ckautoparts.com"
                customerIdentity = "FMP"

            Case "localhost", "www.sandboxprime.ckautoparts.com", "sandboxprime.ckautoparts.com", "sandbox.ckautoparts.com", "sandboxalpha.ckautoparts.com", "parts.ckautoparts.com", "www.parts.ckautoparts.com"
                customerIdentity = "CK"

            Case Else
                customerIdentity = String.Empty

        End Select


        If customerIdentity.Equals("CK") Then
            Dim ImpersonateCustomer As String = ConfigurationManager.AppSettings("ImpersonateCustomer").ToString()

            If (Not String.IsNullOrEmpty(ImpersonateCustomer)) AndAlso ImpersonateCustomer <> "none" AndAlso ImpersonateCustomer <> "CK" Then

                customerIdentity = ImpersonateCustomer

            End If

        End If

        HttpContext.Current.Session("client") = customerIdentity

    End Sub

    Public Shared Function GetClientConnectionString() As String
        Try

            Return GetSpecificConnectionString(CType(HttpContext.Current.Session("client"), String).ToUpper())
        Catch ex As Exception
            Return CKConnectionString
        End Try
    End Function

    Public Shared Function GetSpecificConnectionString(name As String) As String
        Select Case name.ToUpper()

            Case "AUTOWAY"
                Return ConfigurationManager.ConnectionStrings("AutowayConnectionString").ConnectionString
            Case "GO"
                Return ConfigurationManager.ConnectionStrings("GOConnectionString").ConnectionString
            Case "LARRYMILLER"
                Return ConfigurationManager.ConnectionStrings("LarryMillerConnectionString").ConnectionString
            Case "CK"
                Return ConfigurationManager.ConnectionStrings("CKConnectionString").ConnectionString
                'Return ConfigurationManager.ConnectionStrings("PartsManagerConnectionString").ConnectionString
            Case "BIGVALLEY"
                Return ConfigurationManager.ConnectionStrings("BigValleyConnectionString").ConnectionString
            Case "FITZ"
                Return ConfigurationManager.ConnectionStrings("FitzConnectionString").ConnectionString
            Case "DICKMYERS"
                Return ConfigurationManager.ConnectionStrings("DickMyersConnectionString").ConnectionString
            Case "DUPRATT"
                Return ConfigurationManager.ConnectionStrings("DuPrattConnectionString").ConnectionString
            Case "QUIRK"
                Return ConfigurationManager.ConnectionStrings("QuirkConnectionString").ConnectionString

            Case "GAUDIN"
                Return ConfigurationManager.ConnectionStrings("GaudinConnectionString").ConnectionString

            Case "AUTONATION"
                Return ConfigurationManager.ConnectionStrings("AutoNationConnectionString").ConnectionString

            Case "MESA"
                Return ConfigurationManager.ConnectionStrings("MesaConnectionString").ConnectionString

            Case "GOPOWER"
                Return ConfigurationManager.ConnectionStrings("GoPowerConnectionString").ConnectionString

            Case "FMP"
                Return ConfigurationManager.ConnectionStrings("FMPConnectionString").ConnectionString

        End Select

        Return String.Empty

    End Function


End Class
