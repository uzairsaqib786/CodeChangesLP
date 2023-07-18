' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class BatchManager
        ''' <summary>
        ''' Gets batch specific settings from preferences
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of string containing the batch manager settings</returns>
        ''' <remarks></remarks>
        Public Shared Function getBMSettings(user As String, WSID As String) As List(Of String)
            Dim datareader As SqlDataReader = Nothing, settings As New List(Of String)
            Try
                datareader = RunSPArray("selPrefsBatchMan", WSID, {{"@WSID", WSID, strVar}})

                If datareader.HasRows Then
                    If datareader.Read() Then
                        For x As Integer = 0 To datareader.FieldCount - 1
                            settings.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                        Next
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("BatchManager", "getNextBatchID", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return settings
        End Function

        ''' <summary>
        ''' Gets the table data for the left table for batch manager
        ''' </summary>
        ''' <param name="TransType">The selected trasction type (pick, put away, count)</param>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>A list of list of string containing the data for the batch manager table</returns>
        ''' <remarks></remarks>
        Public Shared Function getBMTable(TransType As String, user As String, WSID As String) As List(Of List(Of String))
            Dim datareader As SqlDataReader = Nothing, table As New List(Of List(Of String)), row As New List(Of String)
            Try
                datareader = RunSPArray("selBatchManOrders", WSID, {{"@TransType", TransType, strVar}})

                If datareader.HasRows Then
                    While datareader.Read()
                        For x As Integer = 0 To datareader.FieldCount - 1
                            row.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                        Next
                        row.AddRange({"0", ""}) ' add extras for the TOTE NUMBER/DETAIL fields in the selected table
                        table.Add(row)
                        row = New List(Of String)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("BatchManagerHub", "getTableData", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return table
        End Function

        ''' <summary>
        ''' Gets all batches that can be deleted to populate the dropdown
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of string containing all the batches that are able to be deleted</returns>
        ''' <remarks></remarks>
        Public Shared Function selectBatchesDeleteDrop(TransType As String, user As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim Batches As New List(Of String)
            Try
                DataReader = RunSPArray("dellOTBatchTA", WSID, {{"@TransType", TransType, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Batches.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("BatchManager", "selectBatchesDeleteDrop", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Batches
        End Function

        ''' <summary>
        ''' Gets the super batch tables
        ''' </summary>
        ''' <param name="draw">Tells if the table is currently being drawn</param>
        ''' <param name="user">The suer that is currently logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="singleLines">Specifies to be batched entries or already batched entries (left or right tables)</param>
        ''' <returns>Table object containing the data needed for the super batches datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getSBTables(draw As Integer, user As String, WSID As String, singleLines As Boolean) As TableObject
            Dim datareader As SqlDataReader = Nothing, table As New List(Of List(Of String)), row As New List(Of String), recordNumber As Integer = 0
            Dim SP As String = IIf(singleLines, "selOTSuperBatchLeft", "selOTSuperBatchRight")

            Try
                datareader = RunSPArray(SP, WSID, {{"nothing"}})

                For x As Integer = 0 To 1
                    If datareader.HasRows Then
                        While datareader.Read()
                            If x = 0 Then
                                row.AddRange({ _
                                         IIf(IsDBNull(datareader.Item("Zone")), "", datareader.Item("Zone")), _
                                         IIf(IsDBNull(datareader.Item("Location Name")), "", datareader.Item("Location Name")), _
                                         IIf(datareader.Item("Carousel"), "Carousel", IIf(datareader.Item("Carton Flow"), "Carton Flow", "Non-Carousel")), _
                                         IIf(IsDBNull(datareader.Item("Total Transactions")), "", datareader.Item("Total Transactions"))})
                                table.Add(row)
                                row = New List(Of String)
                            Else
                                recordNumber = IIf(IsDBNull(datareader(0)), 0, datareader(0))
                            End If
                        End While
                        datareader.NextResult()
                    End If
                Next
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("BatchManager", "getSBSingleOrders", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return New TableObject(draw, recordNumber, recordNumber, table)
        End Function
    End Class

End Namespace
