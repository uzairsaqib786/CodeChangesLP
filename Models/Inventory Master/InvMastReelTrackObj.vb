' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class InvMastReelTrackObj
    Public Property MinRTSReelQuan As Integer
    ''' <summary>
    ''' Includes item in RTS Reel Tracking updates.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncAutoUpd As Boolean
    ''' <summary>
    ''' Creates a new object for the reel tracking tab of Inventory Master.
    ''' </summary>
    ''' <param name="MinReelQaun">The minimum RTS rell quantity field.</param>
    ''' <param name="IncAutoUpd">The include in auto RTS update field.</param>
    ''' <remarks></remarks>
    Public Sub New(MinReelQaun As Integer, IncAutoUpd As Boolean)
        Me.MinRTSReelQuan = MinReelQaun
        Me.IncAutoUpd = IncAutoUpd
    End Sub
End Class
