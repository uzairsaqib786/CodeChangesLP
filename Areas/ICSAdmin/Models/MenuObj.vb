' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace Admin
    Public Class MenuObj
        Public Property TableData As List(Of List(Of String))
        Public Property CountsData As List(Of String)
        Public Property AutomatedPOD As String
        Public Property DiagnosticMode As Boolean
        Public Property PickWorkstation As Boolean

        ''' <summary>
        ''' Creates a new Menu Object with table data, count information, automated pod and diagnostic mode per user.
        ''' </summary>
        ''' <param name="td">Table data as a list of list of string from [Open Transactions] join [Location Zones].  Represents Row/Column on the HTML pages.</param>
        ''' <param name="counts">Count data as a list of string.  List order (0-end): Open Picks, Completed Picks, Open Puts, Completed Puts, Open Counts, Completed Counts, Adjustments and Reprocess. From tables: [Open Transactions], [Open Transactions Temp]</param>
        ''' <param name="pod">Contains Automated POD that the workstation is connected to.</param>
        ''' <param name="diag">Diagnostic Mode as Boolean from usysLogFile table.</param>
        ''' <remarks></remarks>
        Public Sub New(td As List(Of List(Of String)), counts As List(Of String), pod As String, diag As Boolean, pickWorkstation As Boolean)
            Me.TableData = td
            Me.CountsData = counts
            Me.AutomatedPOD = pod
            Me.DiagnosticMode = diag
            Me.PickWorkstation = pickWorkstation
        End Sub
    End Class
End Namespace

