' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ConnectedUser
    Public Property Name As String
    Public Property heartbeat As DateTime
    Public Property PCName As String

    Public Sub New(ByVal name As String, pcname As String, heartbeat As DateTime)
        Me.Name = name
        Me.PCName = pcname
        Me.heartbeat = heartbeat
    End Sub
End Class
