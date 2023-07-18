' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class InvMastWeighScaleObj
    Public Property UseScale As Boolean
    Public Property AvgPieceWeight As Decimal
    Public Property SampleQuan As Integer
    Public Property MinUseScale As Integer
    ''' <summary>
    ''' Creates a new object for the weight scale tab of Inventory Master.
    ''' </summary>
    ''' <param name="UseScale">Boolean to use a scale with the item to weigh it.</param>
    ''' <param name="AvgPieceWeight">Average piece weight.</param>
    ''' <param name="SampleQuan">Sample quantity for average piece weight.</param>
    ''' <param name="MinUseScale">Minimum quantity to use the scale with.</param>
    ''' <remarks></remarks>
    Public Sub New(UseScale As Boolean, AvgPieceWeight As Decimal, SampleQuan As Integer, MinUseScale As Integer)
        Me.UseScale = UseScale
        Me.AvgPieceWeight = AvgPieceWeight
        Me.SampleQuan = SampleQuan
        Me.MinUseScale = MinUseScale
    End Sub
End Class
