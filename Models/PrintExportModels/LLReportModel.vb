' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class LLReportModel

    Public Property ReportName As String
    Public Property ReportLocation As String
    Public Property ReportType As String
    Public Property PrinterName As String
    Public Property OnePerQty As Boolean

    Public Property ConnectionString As String
    Public Property User As String
    Public Property UserDirectory As String
    Public Property WSID As String
    Public Property DataSPName As String
    Public Property DataParameters As String(,)
    Sub New()

    End Sub
    Sub New(ReportName As String, ReportLocation As String, UserDirectory As String, reporttype As String, SPName As String, Parameters As String(,), connectionstring As String, User As String, WSID As String, Optional PrinterName As String = "", Optional OnePerQty As Boolean = False)
        Me.ReportName = ReportName
        Me.ReportLocation = ReportLocation
        Me.User = User
        Me.UserDirectory = UserDirectory
        Me.WSID = WSID
        Me.DataSPName = SPName
        Me.DataParameters = Parameters
        Me.ReportType = reporttype
        Me.ConnectionString = connectionstring
        Me.PrinterName = PrinterName
        Me.OnePerQty = OnePerQty
    End Sub
End Class
