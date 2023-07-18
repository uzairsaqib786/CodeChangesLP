' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Module SQLHelpers
    ''' <summary>
    ''' Creates a list of Dictionaries(SQL Rows) from the stored procedure's results
    ''' </summary>
    ''' <param name="spName">Stored procedure name</param>
    ''' <param name="wsid">Work station id</param>
    ''' <param name="params">Parameters for the stored procedure in a 2d array, defaults to no params</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResultMapList(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing
                             ) As List(Of Dictionary(Of String, Object))
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Dim mapList = New List(Of Dictionary(Of String, Object))

        If dataReader IsNot Nothing Then
            Try
                Dim numOfCols = dataReader.FieldCount - 1

                While dataReader.Read()
                    Dim rowMap = New Dictionary(Of String, Object)
                    For iter As Integer = 0 To numOfCols
                        Dim colNameRaw = dataReader.GetName(iter)
                        Dim colName = If(String.IsNullOrEmpty(colNameRaw), iter, colNameRaw)
                        Dim val = dataReader.Item(iter)
                        If IsDBNull(val) Then val = Nothing
                        rowMap.Add(colName, val)
                    Next
                    mapList.Add(rowMap)
                End While
            Catch ex As Exception
                Debug.WriteLine("GetResultMapList " & spName & " error: " & ex.ToString())
                insertErrorMessages("SQLHelpers", "GetResultMapList", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
            Finally
                dataReader.Close()
            End Try

        End If

        Return mapList
    End Function

    ''' <summary>
    ''' Returns a list of dictionarys containing all result sets from a query (Where each query is a single line)
    ''' </summary>
    ''' <param name="spName"></param>
    ''' <param name="wsid"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResultListMultiDataSetSingle(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing
                             ) As List(Of Dictionary(Of String, Object))
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Dim mapList = New List(Of Dictionary(Of String, Object))

        If dataReader IsNot Nothing Then

            Try

                While dataReader.HasRows
                    Dim numOfCols = dataReader.FieldCount - 1
                    While dataReader.Read()
                        Dim rowMap = New Dictionary(Of String, Object)
                        For iter As Integer = 0 To numOfCols
                            Dim colNameRaw = dataReader.GetName(iter)
                            Dim colName = If(String.IsNullOrEmpty(colNameRaw), iter, colNameRaw)
                            Dim val = dataReader.Item(iter)
                            If IsDBNull(val) Then val = ""
                            rowMap.Add(colName, val)
                        Next
                        mapList.Add(rowMap)
                    End While
                    dataReader.NextResult()
                End While

            Catch ex As Exception
                Debug.WriteLine("GetResultListMultiDataSetSingle " & spName & " error: " & ex.ToString())
                insertErrorMessages("SQLHelpers", "GetResultListMultiDataSetSingle", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
            Finally
                dataReader.Close()
            End Try

        End If

        Return mapList
    End Function

    ''' <summary>
    ''' Gets a dictionary representing a single row from the result set of the SP
    ''' </summary>
    ''' <param name="spName">The stored procedure to run</param>
    ''' <param name="wsid">Work station id</param>
    ''' <param name="params">Parameters for the stored procedure in a 2d array, defaults to no params</param>
    ''' <returns>a dictionary representing a single row from the result set of the SP</returns>
    ''' <remarks></remarks>
    Public Function GetResultMap(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing
                             ) As Dictionary(Of String, Object)
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Dim retMap As New Dictionary(Of String, Object)

        If dataReader IsNot Nothing Then

            Try

                Dim numOfCols = dataReader.FieldCount - 1

                If dataReader.Read() Then
                    For iter As Integer = 0 To numOfCols
                        Dim colNameRaw = dataReader.GetName(iter)
                        Dim colName = If(String.IsNullOrEmpty(colNameRaw), iter, colNameRaw)
                        Dim val = dataReader.Item(iter)
                        If IsDBNull(val) Then val = Nothing
                        retMap.Add(colName, val)
                    Next
                End If

            Catch ex As Exception
                Debug.WriteLine("GetResultMap " & spName & " error: " & ex.ToString())
                insertErrorMessages("SQLHelpers", "GetResultMap", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
            Finally
                dataReader.Close()
            End Try

        End If

        Return retMap
    End Function

    ''' <summary>
    ''' Gets a list representing multiple single cell rows
    ''' </summary>
    ''' <param name="spName">The stored procedure to run</param>
    ''' <param name="wsid">Work station id</param>
    ''' <param name="params">Parameters for the stored procedure in a 2d array, defaults to no params</param>
    ''' <returns>a list representing multiple single cell rows</returns>
    ''' <remarks></remarks>
    Public Function GetResultList(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing
                             ) As List(Of Object)
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Dim retList As New List(Of Object)

        If dataReader IsNot Nothing Then

            Try

                While dataReader.Read()
                    Dim colNameRaw = dataReader.GetName(0)
                    Dim colName = If(String.IsNullOrEmpty(colNameRaw), 0, colNameRaw)
                    Dim val = dataReader.Item(0)
                    If IsDBNull(val) Then val = Nothing
                    retList.Add(val)
                End While

            Catch ex As Exception
                Debug.WriteLine("GetResultList " & spName & " error: " & ex.ToString())
                insertErrorMessages("SQLHelpers", "GetResultList", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
            Finally
                dataReader.Close()
            End Try

        End If

        Return retList
    End Function

    Public Function GetResultSingleCol(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing)

        Dim ret = Nothing

        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Try
            If dataReader IsNot Nothing And dataReader.Read() Then
                ret = dataReader.Item(0)
            End If
        Catch ex As Exception
            Debug.WriteLine("GetResultSingleCol " & spName & " error: " & ex.ToString())
            insertErrorMessages("SQLHelpers", "GetResultSingleCol", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
        Finally
            If dataReader IsNot Nothing Then
                dataReader.Close()
            End If
        End Try

        Return ret

    End Function

    Public Sub RunSP(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing)
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        RunActionSP(spName, wsid, params)
    End Sub

    ''' <summary>
    ''' Creates a list of list of Dictionaries(SQL Rows) from the stored procedure's results for each result set in the datareader
    ''' </summary>
    ''' <param name="spName">Stored procedure name</param>
    ''' <param name="wsid">Work station id</param>
    ''' <param name="params">Parameters for the stored procedure in a 2d array, defaults to no params</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMultiResultMapList(ByVal spName As String, ByVal wsid As String, Optional ByVal params As String(,) = Nothing) As List(Of List(Of Dictionary(Of String, Object)))
        If params Is Nothing Then
            params = {{"nothing"}}
        End If

        Dim dataReader = RunSPArray(spName, wsid, params)
        Dim mapList = New List(Of Dictionary(Of String, Object))
        Dim multiList As New List(Of List(Of Dictionary(Of String, Object)))

        If dataReader IsNot Nothing Then
            Try
                Dim numOfCols = dataReader.FieldCount - 1

                While dataReader.HasRows
                    While dataReader.Read()
                        Dim rowMap = New Dictionary(Of String, Object)
                        For iter As Integer = 0 To numOfCols
                            Dim colNameRaw = dataReader.GetName(iter)
                            Dim colName = If(String.IsNullOrEmpty(colNameRaw), iter, colNameRaw)
                            Dim val = dataReader.Item(iter)
                            If IsDBNull(val) Then val = Nothing
                            rowMap.Add(colName, val)
                        Next
                        mapList.Add(rowMap)
                    End While
                    multiList.Add(mapList)
                    mapList = New List(Of Dictionary(Of String, Object))
                    dataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine("GetResultMapList " & spName & " error: " & ex.ToString())
                insertErrorMessages("SQLHelpers", "GetResultMapList", "SP: " & spName & " Message: " & ex.ToString(), "SQLHelpers", wsid)
            Finally
                dataReader.Close()
            End Try

        End If

        Return multiList
    End Function

    ''' <summary>
    ''' Creates an instance of a TableObject for use with the datatables js plugin according to the parameters provided.
    ''' </summary>
    ''' <param name="draw">DataTables variable to track which request is most current.</param>
    ''' <param name="spName">Stored procedure to run to get the data</param>
    ''' <param name="wsid">Workstation ID</param>
    ''' <param name="user">Username</param>
    ''' <param name="params">Parameters in String(,) format to use for the stored procedure.</param>
    ''' <param name="totalRecordsIndex">Integer: which dataset (0-indexed) to find the count of total records that could be returned without filters.</param>
    ''' <param name="filteredRecordsIndex">Integer: which dataset (0-indexed) to find the count of filtered records.</param>
    ''' <param name="dataRecordsIndex">Integer: which dataset (0-indexed) to find the data for the table.</param>
    ''' <param name="columnOrder">List(string) which defines the sequence of the datareader column names.  If this is not provided they will be added in the default order by index.</param>
    ''' <returns></returns>
    ''' <remarks>extraData must be in a dataset within the range of optional index parameters or immediately after.</remarks>
    Public Function GetJQueryDataTableResult(ByVal draw As Integer, ByVal spName As String, ByVal wsid As String, ByVal user As String,
                                            Optional ByVal params As String(,) = Nothing,
                                            Optional totalRecordsIndex As Integer = 0, Optional filteredRecordsIndex As Integer = 1, Optional dataRecordsIndex As Integer = 2,
                                            Optional columnOrder As List(Of String) = Nothing) As TableObject
        If IsNothing(params) Then params = {{"nothing"}}
        Dim datareader As SqlDataReader = RunSPArray(spName, wsid, params)
        Dim data As New TableObject(draw, 0, 0, New List(Of List(Of String)))
        Dim extraData As New List(Of Dictionary(Of String, Object))
        Dim innerExtra As New Dictionary(Of String, Object)
        If Not IsNothing(datareader) Then
            Try
                Dim max As Integer = New List(Of Integer)() From {totalRecordsIndex, filteredRecordsIndex, dataRecordsIndex}.Max() + 1 ' plus one so that if there is "extraData" we get it
                For index As Integer = 0 To max
                    If datareader.HasRows Then
                        Dim innerList As New List(Of String)
                        While datareader.Read
                            If index = totalRecordsIndex AndAlso Not IsDBNull(datareader(0)) Then
                                data.recordsTotal = datareader(0)
                            End If
                            If index = filteredRecordsIndex AndAlso Not IsDBNull(datareader(0)) Then
                                data.recordsFiltered = datareader(0)
                            End If
                            If index = dataRecordsIndex Then
                                If IsNothing(columnOrder) Then
                                    For x As Integer = 0 To datareader.FieldCount - 1
                                        Dim DT As DateTime = Nothing
                                        Dim ColVal = CheckDBNull(datareader(x))

                                        'Datareader does not include milliseconds. Need to force it to
                                        If datareader.GetDataTypeName(x).ToLower() = "datetime" AndAlso DateTime.TryParse(ColVal, DT) Then
                                            Dim ColDT As DateTime = datareader.GetDateTime(x)
                                            innerList.Add(ColDT.ToString("MM/dd/yyyy hh:mm:ss.fff tt"))
                                        Else
                                            innerList.Add(ColVal)
                                        End If
                                    Next
                                Else
                                    For Each col As String In columnOrder
                                        Dim ColIndex As Integer = datareader.GetOrdinal(col)
                                        Dim DT As DateTime = Nothing

                                        Dim ColVal = CheckDBNull(datareader(col))

                                        'Datareader does not include milliseconds. Need to force it to
                                        If datareader.GetDataTypeName(ColIndex).ToLower() = "datetime" AndAlso DateTime.TryParse(ColVal, DT) Then
                                            Dim ColDT As DateTime = datareader.GetDateTime(ColIndex)
                                            'ColDT.ToString("MM/dd/yyyy hh:mm:ss:fff tt")
                                            innerList.Add(ColDT.ToString("MM/dd/yyyy hh:mm:ss.fff tt"))
                                        Else
                                            innerList.Add(ColVal)
                                        End If

                                    Next
                                End If
                                data.data.Add(innerList)
                                innerList = New List(Of String)
                            End If
                            If index <> dataRecordsIndex And index <> filteredRecordsIndex And index <> totalRecordsIndex Then
                                For x As Integer = 0 To datareader.FieldCount - 1
                                    innerExtra.Add(datareader.GetName(x), datareader(x))
                                Next
                                extraData.Add(innerExtra)
                                innerExtra = New Dictionary(Of String, Object)
                            End If
                        End While
                        datareader.NextResult()
                    End If
                Next
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("SQLHelpers", "GetJQueryDataTableResult", ex.ToString(), user, wsid)
            Finally
                If Not IsNothing(datareader) Then datareader.Dispose()
            End Try
        End If
        data.extraData = extraData
        Return data
    End Function

End Module