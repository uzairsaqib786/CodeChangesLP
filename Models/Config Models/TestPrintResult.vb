' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21

Public Class TestPrintResult
    ''' <summary>
    ''' Printer that this request was sent to.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Printer As String
    ''' <summary>
    ''' List or Label enumeration for the projectfile specified
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProjectType As LlProject
    ''' <summary>
    ''' LLReportModel instance that was sent to the service when the print was requested.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>During an export this will be nothing.</remarks>
    Property PrintDataReceived As LLReportModel
    ''' <summary>
    ''' ExportServiceModel instance that was sent to the service when the export was requested.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>During a print request this will be set to nothing.</remarks>
    Property ExportDataReceived As ExportServiceModel
    ''' <summary>
    ''' Indicates whether the dataset provided to list and label is nothing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSetIsNothing As Boolean
    ''' <summary>
    ''' Exception that was caught while attempting to print or export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CaughtException As Exception
    ''' <summary>
    ''' Whether the process finished without exceptions.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Success As Boolean
    ''' <summary>
    ''' Number of records in the dataset provided to list and label.  When there were no records we cannot be sure that the print/export process will be successful in the future
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RecordsInDS As Integer
    ''' <summary>
    ''' Optional, usually unset value that allows the test print functions to sync up to their individual requests when the service responds.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Used only in the case where many print/export requests are made in quick succession or simultaneously from the SAME workstation.</remarks>
    Property ExportID As Integer

    ''' <summary>
    ''' Constructor for the object
    ''' </summary>
    ''' <param name="Printer"></param>
    ''' <param name="ProjectType"></param>
    ''' <param name="ExportDataReceived"></param>
    ''' <param name="PrintDataReceived"></param>
    ''' <param name="DataSetIsNothing"></param>
    ''' <param name="CaughtException"></param>
    ''' <param name="Success"></param>
    ''' <param name="RecordsInDS"></param>
    ''' <param name="ExportID"></param>
    ''' <remarks></remarks>
    Public Sub New(Printer As String, ProjectType As LlProject, ExportDataReceived As ExportServiceModel, PrintDataReceived As LLReportModel, DataSetIsNothing As Boolean, CaughtException As Exception, Success As Boolean, RecordsInDS As Integer, ExportID As Integer)
        Me.Printer = Printer
        Me.ProjectType = ProjectType
        Me.ExportDataReceived = ExportDataReceived
        Me.PrintDataReceived = PrintDataReceived
        Me.DataSetIsNothing = DataSetIsNothing
        Me.CaughtException = CaughtException
        Me.Success = Success
        Me.RecordsInDS = RecordsInDS
        Me.ExportID = ExportID
    End Sub
End Class