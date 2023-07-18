' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Object which contains data relevant to printing Generic Reports.  Any report without filters can use this class.
''' </summary>
''' <remarks></remarks>
Public Class GenericListModel
    ''' <summary>
    ''' List and Label Report Design location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
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
    Public Property view As String
    Public ReadOnly type As LlProject = LlProject.List
    Public Property WSID As String

    Public Sub New(location As String, user As String, userDirectory As String, view As String, WSID As String)
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.view = view
        Me.WSID = WSID
    End Sub

End Class
