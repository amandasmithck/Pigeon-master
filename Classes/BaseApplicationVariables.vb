Imports Microsoft.VisualBasic
Imports System


Public Class BaseApplicationVariables

    Public Sub New()

    End Sub

#Region "Federal Express Variables"
    Public Shared ReadOnly Property FedExKey As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExKey").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property FedExPassword As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExPassword").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property FedExMeterNumber As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExMeterNumber").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property FedExWebServiceUrl As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExWebServiceUrl").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property FedExAccountNumber As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExAccountNumber").ToString()
        End Get
    End Property


    Public Shared ReadOnly Property FedExLabelLocation As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExLabelLocation").ToString()
        End Get
    End Property


    Public Shared ReadOnly Property FedExLabelDirectory As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("FedExLabelDirectory").ToString()
        End Get
    End Property

    Public Shared ReadOnly Property JSErrorMessageKeyword As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("JSErrorMessageKeyword").ToString()
        End Get
    End Property



#End Region

    Public Shared ReadOnly Property CertifiedUKey As String
        Get
            Return System.Web.Configuration.WebConfigurationManager.AppSettings("CertifiedUKey").ToString()
        End Get
    End Property
End Class
