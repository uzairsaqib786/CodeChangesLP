' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class OrderStatInfo
    ''' <summary>
    ''' Order type.  (Pick, Put Away, etc.)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property type As String = ""
    ''' <summary>
    ''' Total number of records the order consists of.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property totalLines As String = ""
    ''' <summary>
    ''' Order status.  (Complete, in progress, etc.)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property status As String = ""
    ''' <summary>
    ''' Completed lines in the order.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compLines As String = ""
    ''' <summary>
    ''' Open lines in the order.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property opLines As String = ""
    ''' <summary>
    ''' Reprocess lines in the order.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property reLines As String = ""
    ''' <summary>
    ''' Off carousel data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property offData As List(Of List(Of String))
    ''' <summary>
    ''' Carousel data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property onData As List(Of List(Of String))
    ''' <summary>
    ''' Order Number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property orderNumber As String = ""
    ''' <summary>
    ''' Tote ID.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property toteID As String = ""

    Public Sub New(ByVal type As String, ByVal totalLines As String, ByVal status As String, compLines As String, opLines As String, _
                  reLines As String, offData As List(Of List(Of String)), onData As List(Of List(Of String)), orderNumber As String, toteID As String)
        Me.type = type
        Me.totalLines = totalLines
        Me.status = status
        Me.compLines = compLines
        Me.opLines = opLines
        Me.reLines = reLines
        Me.offData = offData
        Me.onData = onData
        Me.orderNumber = orderNumber
        Me.toteID = toteID

    End Sub
End Class
