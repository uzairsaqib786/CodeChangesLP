' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CMData
    Property leftTable
    Property rightTable
    Property openLines
    Property completedLines
    Property reprocLines
    Property toteTable
    Property FilterType
    Property OrderNumber
    ''' <summary>
    ''' Sets the properties to their desired values for the consolidation page
    ''' </summary>
    ''' <param name="leftTable">Unverified table</param>
    ''' <param name="rightTable">Verified table</param>
    ''' <param name="openLines">number of open lines</param>
    ''' <param name="completedLines">number of completed lines</param>
    ''' <param name="reprocLines">number of reprocess lines</param>
    ''' <param name="filterType">Either Order Number or Tote ID</param>
    ''' <param name="toteTable">The tote datatable infomration</param>
    ''' <param name="OrderNumber"></param>
    ''' <remarks></remarks>
    Sub New(leftTable As List(Of List(Of String)), rightTable As List(Of List(Of String)), openLines As Integer, _
            completedLines As Integer, reprocLines As Integer, filterType As String, toteTable As List(Of List(Of String)), OrderNumber As String)
        Me.leftTable = leftTable
        Me.rightTable = rightTable
        Me.openLines = openLines
        Me.completedLines = completedLines
        Me.reprocLines = reprocLines
        Me.toteTable = toteTable
        Me.FilterType = filterType
        Me.OrderNumber = OrderNumber
    End Sub
End Class
