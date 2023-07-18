' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Represents a Kit Item in the Kit Item tab in Inventory Master.
''' </summary>
''' <remarks></remarks>
Public Class InvMastKitItemObj
    Public Property ItemNum As List(Of String)
    Public Property Desc As List(Of String)
    Public Property KitQuan As List(Of Integer)
    Public Property KitSpecFeats As List(Of String)

    ''' <summary>
    ''' Creates a new object for the kit item tab of Inventory Master.
    ''' </summary>
    ''' <param name="ItemNum">The item number field.</param>
    ''' <param name="Desc">The description field.</param>
    ''' <param name="KitQuan">The kit quantity field.</param>>
    ''' <remarks></remarks>
    Public Sub New(ItemNum As List(Of String), Desc As List(Of String), KitQuan As List(Of Integer), KitSpecFeats As List(Of String))
        Me.ItemNum = ItemNum
        Me.Desc = Desc
        Me.KitQuan = KitQuan
        Me.KitSpecFeats = KitSpecFeats
    End Sub
End Class
