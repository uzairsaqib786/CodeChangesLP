' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

' Object with required data for use with DataTables plugin

Public Class TableObject
    ''' <summary>
    ''' DataTables plugin parameter.  Not to be changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property draw As Integer = 0
    ''' <summary>
    ''' Total records the table can contain.  (typically select count(*) from table.)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property recordsTotal As Integer = 0
    ''' <summary>
    ''' Filtered records' count.  (typically select count(*) where x = y from table.)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property recordsFiltered As Integer = 0
    ''' <summary>
    ''' Returned data as a row/column combination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property data As List(Of List(Of String))
    ''' <summary>
    ''' Extra data for the datatables instance.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Accessed using the XHR event on the datatables plugin in javascript</remarks>
    Public Property extraData As Object

    Public Sub New(ByVal draw As Integer, ByVal recordsTotal As Integer, ByVal recordsFiltered As Integer, ByVal data As List(Of List(Of String)), Optional extraData As Object = Nothing)
        Me.draw = draw
        Me.recordsTotal = recordsTotal
        Me.recordsFiltered = recordsFiltered
        Me.data = data
        Me.extraData = extraData
    End Sub

End Class
