' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ExportServiceModel

    'Contains the report information
    Public Property ExportModel As CustomReportsModel

    'The file format
    Public Property DesiredFormat As String

    'The location where the export is placed
    Public Property ExportPath As String

    'The location of the report file
    Public Property ProjectFile As String

    'The suer's working directory
    Public Property UserDirectory As String

    'The stored procedure that gets the data
    Public Property SPName As String

    'The parameters and their values for the stored procedure
    Public Property Params As String(,)

    'The sql connection string
    Public Property ConnectionString As String

    'The workstasion that is currently being worked on
    Public Property WSID As String

    'The user that is currently logged in
    Public Property User As String

    'The export's identifier
    Public Property ExportID As Integer

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Constructor for the object with an export model
    ''' </summary>
    ''' <param name="DesiredFormat"></param>
    ''' <param name="UserDirectory"></param>
    ''' <param name="ExportPath"></param>
    ''' <param name="ProjectFile"></param>
    ''' <param name="SPName"></param>
    ''' <param name="Params"></param>
    ''' <param name="ConnectionString"></param>
    ''' <param name="WSID"></param>
    ''' <param name="User"></param>
    ''' <param name="ExportModel"></param>
    ''' <remarks></remarks>
    Public Sub New(DesiredFormat As String, UserDirectory As String, ExportPath As String, ProjectFile As String, SPName As String, Params As String(,), ConnectionString As String, WSID As String, User As String, ExportModel As CustomReportsModel)
        Me.ExportModel = ExportModel
        Me.DesiredFormat = DesiredFormat
        Me.UserDirectory = UserDirectory
        Me.ExportPath = ExportPath
        Me.ProjectFile = ProjectFile
        Me.SPName = SPName
        Me.Params = Params
        Me.ConnectionString = ConnectionString
        Me.WSID = WSID
        Me.User = User
    End Sub

    ''' <summary>
    ''' Constructor for the object without an export model
    ''' </summary>
    ''' <param name="DesiredFormat"></param>
    ''' <param name="UserDirectory"></param>
    ''' <param name="ExportPath"></param>
    ''' <param name="ProjectFile"></param>
    ''' <param name="SPName"></param>
    ''' <param name="Params"></param>
    ''' <param name="ConnectionString"></param>
    ''' <param name="WSID"></param>
    ''' <param name="User"></param>
    ''' <remarks></remarks>
    Public Sub New(DesiredFormat As String, UserDirectory As String, ExportPath As String, ProjectFile As String, SPName As String, Params As String(,), ConnectionString As String, WSID As String, User As String)
        Me.ExportModel = Nothing
        Me.DesiredFormat = DesiredFormat
        Me.UserDirectory = UserDirectory
        Me.ExportPath = ExportPath
        Me.ProjectFile = ProjectFile
        Me.SPName = SPName
        Me.Params = Params
        Me.ConnectionString = ConnectionString
        Me.WSID = WSID
        Me.User = User
    End Sub
End Class
