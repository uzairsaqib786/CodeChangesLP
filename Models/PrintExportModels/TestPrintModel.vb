' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21
Imports combit.ListLabel21.Dom

''' <summary>
''' Object which contains data relevant to printing Generic Reports.  Any report without filters can use this class.
''' </summary>
''' <remarks></remarks>
Public Class TestPrintModel
    ''' <summary>
    ''' List and Label Report Design location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property location As String
    ''' <summary>
    ''' User requesting print
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property user As String
    ''' <summary>
    ''' User's printing preferences directory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property userDirectory As String
    Property type As LlProject
    Property WSID As String
    Property printerName As String
    Property printerAddress As String
    Property isLabel As Boolean

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Constructor for the object
    ''' </summary>
    ''' <param name="location"></param>
    ''' <param name="user"></param>
    ''' <param name="userDirectory"></param>
    ''' <param name="WSID"></param>
    ''' <param name="type"></param>
    ''' <param name="printername"></param>
    ''' <param name="printeraddress"></param>
    ''' <param name="islabel"></param>
    ''' <remarks></remarks>
    Public Sub New(location As String, user As String, userDirectory As String, WSID As String, type As LlProject, _
                   printername As String, printeraddress As String, islabel As Boolean)
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.WSID = WSID
        Me.type = type
        Me.printerName = printername
        Me.printerAddress = printeraddress
        Me.isLabel = islabel
    End Sub

End Class
