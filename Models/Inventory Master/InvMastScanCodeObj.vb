' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class InvMastScanCodeObj
    Public Property ScanCode As List(Of String)
    Public Property ScanType As List(Of String)
    Public Property ScanRange As List(Of String)
    Public Property StartPos As List(Of Integer)
    Public Property CodeLen As List(Of Integer)



    ''' <summary>
    ''' Creates a new object for the scan code tab of Inventory Master.
    ''' </summary>
    ''' <param name="ScanCode">Scan code field.</param>
    ''' <param name="ScanType">Scan type field.</param>
    ''' <param name="ScanRange">Scan range field.</param>>
    ''' <param name="StartPos">Start position of the scan code.</param>>
    ''' <param name="CodeLen">Length of scan code.</param>>
    ''' <remarks></remarks>
    Public Sub New(ScanCode As List(Of String), ScanType As List(Of String), ScanRange As List(Of String), StartPos As List(Of Integer), CodeLen As List(Of Integer))
        Me.ScanCode = ScanCode
        Me.ScanType = ScanType
        Me.ScanRange = ScanRange
        Me.StartPos = StartPos
        Me.CodeLen = CodeLen
    End Sub
End Class