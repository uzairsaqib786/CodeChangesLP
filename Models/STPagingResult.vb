' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class STPagingResult
    ''' <summary>
    ''' Whether there was an error while trying to get or set the data for the plugin.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property success As Boolean
    ''' <summary>
    ''' 2D Array used to contain the pages and each bit of their data.  Each Sub Array is a "page" or row for the plugin to process.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property pages As List(Of List(Of String))
    ''' <summary>
    ''' Usually used for an error message when there was a problem and the success flag is set to false.  Can be used for anything.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property message As String
    ''' <summary>
    ''' Total number of records that could be returned if there were no custom filters applied by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property numRecords As Integer
    ''' <summary>
    ''' Number of records returned as a result of filters applied by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property filteredRecords As Integer
    ''' <summary>
    ''' Anonymous object type.  Can be anything for the plugin and is not used by the plugin directly.  The plugin will pass the entire result (including this property) to the processExtraData function where it can be dealt with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property extraData As Object

    ''' <summary>
    ''' Creates a new instance of the standard STPaging.js plugin response.
    ''' </summary>
    ''' <param name="success">Whether there was an error</param>
    ''' <param name="pages">List of rows/pages which will be processed by the plugin's RowFunction parameter function.</param>
    ''' <param name="message">Usually used for an error message when the success flag is false.</param>
    ''' <param name="numRecords">Total number of records selectable before filtering occurs.</param>
    ''' <param name="filteredRecords">Total number of accessible records after custom filtering is applied.</param>
    ''' <param name="extraData">Anonymous object accessible in the plugin's processExtraData method after rows are created.</param>
    ''' <remarks></remarks>
    Sub New(success As Boolean, pages As List(Of List(Of String)), message As String, numRecords As Integer, filteredRecords As Integer, Optional extraData As Object = Nothing)
        Me.success = success
        Me.pages = pages
        Me.message = message
        Me.numRecords = numRecords
        Me.extraData = extraData
        Me.filteredRecords = filteredRecords
    End Sub
End Class