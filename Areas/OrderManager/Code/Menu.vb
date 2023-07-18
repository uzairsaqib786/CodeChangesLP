' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace OrderManager
    Public Class Menu
        ''' <summary>
        ''' Selects the count information for the order manager home screen
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>An object that contains the count data</returns>
        ''' <remarks></remarks>
        Public Shared Function selectOMCountData(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim index As Integer = 0
            Dim OpenPicks As Integer = 0
            Dim CompPick As Integer = 0
            Dim OpenPuts As Integer = 0
            Dim CompPuts As Integer = 0
            Dim OpenCounts As Integer = 0
            Dim CompCounts As Integer = 0
            Dim CompAdjust As Integer = 0
            Dim CompLocChange As Integer = 0
            Dim ReprocCount As Integer = 0

            Try
                DataReader = RunSPArray("selOMCountInfo", WSID, {{"nothing"}})
                While DataReader.HasRows
                    While DataReader.Read
                        If index = 0 Then
                            OpenPicks = DataReader(0)
                        ElseIf index = 1 Then
                            CompPick = DataReader(0)
                        ElseIf index = 2 Then
                            OpenPuts = DataReader(0)
                        ElseIf index = 3 Then
                            CompPuts = DataReader(0)
                        ElseIf index = 4 Then
                            OpenCounts = DataReader(0)
                        ElseIf index = 5 Then
                            CompCounts = DataReader(0)
                        ElseIf index = 6 Then
                            CompAdjust = DataReader(0)
                        ElseIf index = 7 Then
                            CompLocChange = DataReader(0)
                        ElseIf index = 8 Then
                            ReprocCount = DataReader(0)
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While

            Catch ex As Exception
                insertErrorMessages("OrderManager Menu", "selectOMCountData", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return New With {.OpenPicks = OpenPicks, .CompPicks = CompPick, .OpenPuts = OpenPuts, .CompPuts = CompPuts, _
                             .OpenCounts = OpenCounts, .CompCounts = CompCounts, .CompAdjust = CompAdjust, .CompLocChange = CompLocChange, _
                             .ReprocCount = ReprocCount}
        End Function







    End Class
End Namespace
