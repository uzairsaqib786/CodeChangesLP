' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Object which contains data relevant to printing Kit Reports.
''' </summary>
''' <remarks></remarks>
Public Class KitReportModel
    ''' <summary>
    ''' Item Number of the kit to be printed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property kit As String
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
    Public ReadOnly view As String = "Kit"
    Public ReadOnly type As LlProject = LlProject.List
    Public Property WSID As String

    Public Sub New(kit As String, location As String, user As String, userDirectory As String, WSID As String)
        Me.kit = kit
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.WSID = WSID
    End Sub

End Class