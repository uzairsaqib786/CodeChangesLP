' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class LocationAssignment

        ''' <summary>
        ''' Selects all count records that need to be location assigned
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of list of string containing the rows for the datable table</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocAssCountTable(User As String, WSID As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim CountTable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selOTLocAssTypeCountDT", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                        Next
                        CountTable.Add(row)
                        row = New List(Of String)
                    End While
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Location Assignment", "getLocAssCountTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return CountTable
        End Function

        ''' <summary>
        ''' Selects all Pick records that need to be Location Assigned
        ''' </summary>
        ''' <param name="User">The user that is curently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of list of string containing the rows for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocAssPickTable(User As String, WSID As String, orderNumberSearch As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim PickTable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selOTLocAssTypePickDT", WSID, {{"@orderNumberSearch", orderNumberSearch, strVar}})
                'Selects Orders that need to ba Location Assigned
                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                        Next
                        'Short Column
                        row.Add("false")
                        'AllShort Column
                        row.Add("false")
                        'FPZShort Column
                        row.Add("false")
                        PickTable.Add(row)
                        row = New List(Of String)
                    End While
                End If



                'If there are Picks to be assigned, check for Color Status
                If (PickTable.Count > 0) Then
                    DataReader.Close()
                    DataReader = RunSPArray("selLocAssOrderViews", WSID, {{"nothing"}})
                    'Read Data into Lists
                    Dim shortList As New List(Of String)
                    Dim allShortList As New List(Of String)
                    Dim FPZList As New List(Of String)
                    Dim index As Integer = 0

                    While DataReader.HasRows
                        While DataReader.Read()
                            If index = 0 Then
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    shortList.Add(CheckDBNull(DataReader(x)))
                                Next
                            ElseIf index = 1 Then
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    allShortList.Add(CheckDBNull(DataReader(x)))
                                Next
                            Else
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    FPZList.Add(CheckDBNull(DataReader(x)))
                                Next
                            End If
                        End While
                        index += 1
                        DataReader.NextResult()
                    End While

                    For Each row In PickTable
                        Dim OrderNumberIndex As Integer
                        Dim ShortIndex As Integer = PickTable(0).Count - 3
                        Dim AllShortIndex As Integer = PickTable(0).Count - 2
                        Dim FPZIndex As Integer = PickTable(0).Count - 1
                        If shortList.Contains(row(OrderNumberIndex)) Then
                            row(ShortIndex) = "True"
                        End If
                        If allShortList.Contains(row(OrderNumberIndex)) Then
                            row(AllShortIndex) = "True"
                        End If
                        If FPZList.Contains(row(OrderNumberIndex)) Then
                            row(FPZIndex) = "True"
                        End If
                    Next
                End If

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Location Assignment", "getLocAssPickTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return PickTable
        End Function

        ''' <summary>
        ''' Selects all Put Away records that need to be location Assigned
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of list of string containing the rows for the datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocAssPutAwayTable(User As String, WSID As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim PutAwayTable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selOTLocAssTypePutAwayDT", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                        Next
                        PutAwayTable.Add(row)
                        row = New List(Of String)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Location Assignment", "getLocAssPutAwayTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return PutAwayTable
        End Function

        ''' <summary>
        ''' Returns the number of Records that need to be assigned for each transaction type
        ''' </summary>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of objct containing the count data for the transaction types</returns>
        ''' <remarks></remarks>
        Public Shared Function getLocAssCountData(User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim CountInfo As New List(Of Object)

            Try
                DataReader = RunSPArray("selOTLocAssTransTypeCounts", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        CountInfo.Add(New With {.Type = IIf(IsDBNull(DataReader(0)), "", DataReader(0)), .Count = IIf(IsDBNull(DataReader(1)), 0, DataReader(1))})
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Location Assignment", "getLocAssCountData", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return CountInfo
        End Function


        ''' <summary>
        ''' Gets the Import Export Preferences for the given wsid
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <returns>A dictionary that contaisn the preferences and thier values</returns>
        Public Shared Function GetPreferences(ByVal wsid As String) As Dictionary(Of String, Object)
            Dim counter = 0
            Try

                Dim sysPref = GetResultMap("SelSystemPref", wsid)

                Return sysPref
            Catch ex As Exception
                insertErrorMessages("IE", "GetPreferences", counter & " " & ex.ToString(), "IEPref", wsid)
                Throw
            End Try
        End Function

    End Class
End Namespace

