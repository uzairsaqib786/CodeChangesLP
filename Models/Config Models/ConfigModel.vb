' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ConfigModel
    Public Property connections As List(Of Object)
    Public Property printers As List(Of Object)
    Public Property logonInfo As Object
    Public Property Workstations As Dictionary(Of String, String)
    Public Property LAConnectionString As String

    Public Sub New(ByVal connections As List(Of Object), logonInfo As Object, ByVal printers As List(Of Object), Workstations As Dictionary(Of String, String), ByVal LAConnectionString As String)
        Me.connections = connections
        Me.printers = printers
        Me.logonInfo = logonInfo
        Me.Workstations = Workstations
        Me.LAConnectionString = LAConnectionString
    End Sub
End Class
