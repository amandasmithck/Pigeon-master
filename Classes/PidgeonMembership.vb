Public Class PidgeonMembership

#Region "Membership"
    Public Shared Function GetMembershipProvider() As SqlMembershipProvider

        Return GetSpecificMembershipProvider(HttpContext.Current.Session("client"))

    End Function

    Public Shared Function GetSpecificMembershipProvider(client As String) As SqlMembershipProvider

        Select Case client.ToLower()

            Case "autoway"
                Return Membership.Providers("AutowayMembershipProvider")

            Case "larrymiller"
                Return Membership.Providers("LarryMillerMembershipProvider")

            Case "localhost"
                Return Membership.Providers("CKMembershipProvider")

            Case "go"
                Return Membership.Providers("GoMembershipProvider")

            Case "bigvalley"
                Return Membership.Providers("BigValleyMembershipProvider")

            Case "fitz"
                Return Membership.Providers("FitzMembershipProvider")

            Case "dickmyers"
                Return Membership.Providers("DickMyersMembershipProvider")

            Case "dupratt"
                Return Membership.Providers("DuPrattMembershipProvider")

            Case "quirk"
                Return Membership.Providers("QuirkMembershipProvider")

            Case "gaudin"
                Return Membership.Providers("GaudinMembershipProvider")

            Case "autonation"
                Return Membership.Providers("AutoNationMembershipProvider")

            Case "mesa"
                Return Membership.Providers("MesaMembershipProvider")

            Case "gopower"
                Return Membership.Providers("GoPowerMembershipProvider")

            Case "fmp"
                Return Membership.Providers("FMPMembershipProvider")

            Case "ck"
                Return Membership.Providers("CKMembershipProvider")

        End Select

        Return Nothing

    End Function

#End Region

#Region "Roles"

    Public Shared Function CurrentUserIsInRole(role As String) As Boolean

        If IsNothing(HttpContext.Current.User.Identity.Name) Then Return False

        Dim rp As SqlRoleProvider = PidgeonMembership.GetRoleProvider()

        If IsNothing(rp) Then Return False

        Return rp.IsUserInRole(System.Web.HttpContext.Current.User.Identity.Name, role)

    End Function

    Public Shared Function GetCurrentUserRoles() As String()

        Dim rp As SqlRoleProvider = PidgeonMembership.GetRoleProvider()
        Return rp.GetRolesForUser(System.Web.HttpContext.Current.User.Identity.Name)

    End Function
    Public Shared Function GetRoleProvider() As SqlRoleProvider

        Return GetSpecificRoleProvider(HttpContext.Current.Session("client"))

    End Function

    Public Shared Function GetSpecificRoleProvider(client As String) As SqlRoleProvider

        Select Case client.ToLower()

            Case "autoway"
                Return Roles.Providers("AutowayRoleProvider")

            Case "larrymiller"
                Return Roles.Providers("LarryMillerRoleProvider")

            Case "localhost"
                Return Roles.Providers("CKRoleProvider")

            Case "go"
                Return Roles.Providers("GoRoleProvider")

            Case "bigvalley"
                Return Roles.Providers("BigValleyRoleProvider")

            Case "fitz"
                Return Roles.Providers("FitzRoleProvider")

            Case "dickmyers"
                Return Roles.Providers("DickMyersRoleProvider")

            Case "dupratt"
                Return Roles.Providers("DuPrattRoleProvider")

            Case "quirk"
                Return Roles.Providers("QuirkRoleProvider")

            Case "gaudin"
                Return Roles.Providers("GaudinRoleProvider")

            Case "autonation"
                Return Roles.Providers("AutoNationRoleProvider")

            Case "mesa"
                Return Roles.Providers("MesaRoleProvider")

            Case "gopower"
                Return Roles.Providers("GoPowerRoleProvider")

            Case "fmp"
                Return Roles.Providers("FMPRoleProvider")

            Case "ck"
                Return Roles.Providers("CKRoleProvider")

        End Select

        Return Nothing

    End Function

#End Region

End Class
