' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom
Public Class EmployeeGroupModel
    ''' <summary>
    ''' List and Label Report Design location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
    ''' <summary>
    ''' Check value to determine to print by user or date
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkVal As String
    ''' <summary>
    ''' The group to be printed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property group As String
    ''' <summary>
    ''' User requesting print
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property user As String
    ''' <summary>
    ''' User's printing preferences directory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userDirectory As String
    Public ReadOnly view As String = "EmployeeGroup"
    Public ReadOnly type As LlProject = LlProject.List
    Public Property WSID As String

    Public Sub New(location As String, user As String, userDirectory As String, checkVal As String, group As String, WSID As String)
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.checkVal = checkVal
        Me.group = group
        Me.WSID = WSID
    End Sub
End Class
