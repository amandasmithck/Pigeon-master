Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class TestWebService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function test(ByVal thedate As Date)

        Dim dtExpArrival As DateTime = thedate


        Dim FromDate = Now()
        Dim ToDate = dtExpArrival


        Dim TotalDaysInt As Integer = ToDate.Date.Subtract(FromDate.Date).Days
        Dim WeeksInt As Integer = Math.Floor(TotalDaysInt / 7)
        Dim WeekendDaysInt As Integer = WeeksInt * 2

        If TotalDaysInt Mod 7 <> 0 Then
            If FromDate.DayOfWeek = DayOfWeek.Sunday Then
                WeekendDaysInt += 1
            Else
                Dim SaturdayOffsetInt = Math.Max((TotalDaysInt Mod 7) - (DayOfWeek.Saturday - FromDate.DayOfWeek), 0)
                WeekendDaysInt += Math.Min(SaturdayOffsetInt, 2)
            End If
        End If

        If WeekendDaysInt Mod 2 = 0 Then
            dtExpArrival = DateAdd(DateInterval.Day, WeekendDaysInt, dtExpArrival)
        Else
            dtExpArrival = DateAdd(DateInterval.Day, WeekendDaysInt + 1, dtExpArrival)

        End If

        If dtExpArrival.DayOfWeek = DayOfWeek.Saturday Then
            dtExpArrival = DateAdd(DateInterval.Day, 2, dtExpArrival)
        ElseIf dtExpArrival.DayOfWeek = DayOfWeek.Sunday Then
            dtExpArrival = DateAdd(DateInterval.Day, 1, dtExpArrival)
        End If

        'Return WeekendDaysInt

        Return dtExpArrival
    End Function

End Class