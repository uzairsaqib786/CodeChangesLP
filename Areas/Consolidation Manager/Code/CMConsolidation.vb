' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Consolidation
    Public Class CMConsolidation
        ''' <summary>
        ''' Return all data required for Consolidation Screen when an Order/Tote ID is entered
        ''' </summary>
        ''' <param name="Type">Either ToteID or Order Number</param>
        ''' <param name="SelValue">Value of ToteID/Order</param>
        ''' <param name="WSID">WorkStation ID</param>
        ''' <param name="User">Users name</param>
        ''' <returns>a object which contains all the data for the actual page (left table, right table, count info, etc)</returns>
        ''' <remarks></remarks>
        Public Shared Function GetConsolidationData(Type As String, SelValue As String, WSID As String, User As String) As Object

            Dim DataReader As SqlDataReader = Nothing
            Dim StagDataReader As SqlDataReader = Nothing
            Dim leftTable As New List(Of List(Of String))
            Dim rightTable As New List(Of List(Of String))
            Dim row As List(Of String)
            Dim openLines As Integer = 0
            Dim compLines As Integer = 0
            Dim reprocLines As Integer = 0
            Dim toteIDTable As New List(Of List(Of String))
            Dim index As Integer = 0
            Dim newLeft = New List(Of List(Of String))
            Dim CMPrefs = Preferences.selectCMPrefs(User, WSID)

            If Type = "" Then
                Dim ValType As List(Of String) = VerifyOrderTote(SelValue, WSID, User)
                Type = ValType(0)
                If Type = "Tote ID" Or Type = "Super Batch" Then
                    SelValue = ValType(1)
                End If
                'Duplicate Order Number and Tote
                If Not (Type = "Order Number" Or Type = "Tote ID") And Not CMPrefs.NonPickproOrders Then
                    Return Type
                End If
            ElseIf Type = "Tote ID" Then
                SelValue = SelectOrderByTote(SelValue, WSID, User)
            End If
            Try

                If Type = "Super Batch" Then
                    DataReader = RunSPArray("selConsolDataSB", WSID, {{"@BatchPickID", SelValue, strVar},
                                                               {"@WSID", WSID, strVar}})
                Else
                    DataReader = RunSPArray("selConsolData", WSID, {{"@OrderNumber", SelValue, strVar},
                                                                {"@WSID", WSID, strVar}})
                End If
                'If inputVal exists within Pickpro Tables
                Dim DRTableRows As Boolean = False
                Do
                    If DataReader.HasRows Then
                        While DataReader.Read()
                            If index = 0 Then
                                DRTableRows = True
                                row = New List(Of String)
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    row.Add(CheckDBNull(DataReader(x)))
                                Next
                                leftTable.Add(row)
                            ElseIf index = 1 Then
                                DRTableRows = True
                                row = New List(Of String)
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    row.Add(CheckDBNull(DataReader(x)))
                                Next
                                rightTable.Add(row)
                            ElseIf index = 2 Then
                                openLines = DataReader(0)
                            ElseIf index = 3 Then
                                compLines = DataReader(0)
                            ElseIf index = 4 Then
                                reprocLines = DataReader(0)
                            ElseIf index = 5 Then
                                row = New List(Of String)
                                For x As Integer = 0 To DataReader.FieldCount - 1
                                    row.Add(CheckDBNull(DataReader(x)))
                                Next
                                toteIDTable.Add(row)
                            End If
                        End While
                    End If
                    index += 1
                Loop While DataReader.NextResult

                'If Verify Each line is turned on, split transactions across Quantity
                If CMPrefs.VerifyItems = "Yes" Then
                    'Goes through Non Verified Table
                    For Each leftRow In leftTable
                        Dim leftID = leftRow(0)
                        Dim leftQuantity As Integer
                        'If has been completed, use Trans. Quantity, otherwise use completed quantity
                        If leftRow(12) = "" Then
                            leftQuantity = leftRow(11)
                        Else
                            leftQuantity = leftRow(5)
                        End If
                        Dim rightQuantity = 0
                        'Find if there are an instances of left ID in right Table
                        For Each rightRow In rightTable
                            If (leftID = rightRow(0)) Then
                                rightQuantity += 1
                            End If
                        Next
                        leftQuantity -= rightQuantity
                        'Modify Left Row to be single quantity, and get rid of extra data
                        leftRow(5) = 1
                        leftRow.RemoveRange(11, 2)
                        'Creates 1 record for each quantity left after subtracting amount already verified
                        For x = 0 To leftQuantity - 1
                            newLeft.Add(leftRow)
                        Next
                    Next
                Else
                    For Each leftRow In leftTable
                        leftRow.RemoveRange(11, 2)
                        newLeft.Add(leftRow)
                    Next
                End If

                If Not DRTableRows Then
                    'Check if inputVal is a non-pickpro order already existing in Staging Locations
                    StagDataReader = RunSPArray("selStagingLocsNonPickpro", WSID, {{"@OrderNumber", SelValue, strVar}})
                    While (StagDataReader.Read)
                        row = New List(Of String)
                        For x As Integer = 0 To StagDataReader.FieldCount - 1
                            row.Add(CheckDBNull(StagDataReader(x)))
                        Next
                        toteIDTable.Add(row)
                    End While
                    If toteIDTable.Count = 0 Then
                        Return "DNENP"
                    End If
                End If


            Catch ex As Exception
                insertErrorMessages("Consolidation Hub", "selConsolLeftTableData", ex.Message, User, WSID)
                Return "Error"
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
                If Not IsNothing(StagDataReader) Then
                    StagDataReader.Close()
                End If
            End Try
            Return New CMData(newLeft, rightTable, openLines, compLines, reprocLines, Type, toteIDTable, SelValue)

        End Function


        ''' <summary>
        ''' Checks to see if value is Order or Tote, or if there is a conflicting Order/Tote ID
        ''' </summary>
        ''' <param name="value">The order or tote that is being checked</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <returns>a list of string which tells if a conflict occurs</returns>
        ''' <remarks></remarks>
        Public Shared Function VerifyOrderTote(value As String, WSID As String, User As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Try
                DataReader = RunSPArray("selAllTransValidOrderToteNew", WSID, {{"@Value", value, strVar}})
                DataReader.Read()
                'Either Returns Order Number, Tote ID, or Conflict, DNE as a String
                If DataReader(0) = "Tote ID" OrElse DataReader(0) = "Super Batch" Then
                    Return New List(Of String) From {DataReader(0), DataReader(1)}
                End If
                Return New List(Of String) From {DataReader(0)}
            Catch ex As Exception
                insertErrorMessages("Consolidation Hub", "VerifyOrderTote", ex.Message, User, WSID)

            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return New List(Of String)
        End Function

        ''' <summary>
        ''' Select an Order Number by its Tote ID for Consolidation
        ''' </summary>
        ''' <param name="ToteID">The given tote id to get an order number for</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>The order number for the given tote id</returns>
        ''' <remarks></remarks>
        Private Shared Function SelectOrderByTote(ToteID As String, WSID As String, User As String) As String
            Dim DataReader As SqlDataReader = Nothing
            Try
                DataReader = RunSPArray("selConsolidationOrderByTote", WSID, {{"@ToteID", ToteID, strVar}})
                DataReader.Read()
                Return CheckDBNull(DataReader(0))
            Catch ex As Exception
                insertErrorMessages("Consolidation Hub", "SelectOrderByTote", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return "Error"
        End Function

        ''' <summary>
        ''' Selects Typeahead Values for Verifying Consolidation Transactions
        ''' </summary>
        ''' <param name="column">The column that is begin checked</param>
        ''' <param name="val">The value that is being entered in for the column</param>
        ''' <param name="orderNum">The order number that is being dipslayed</param>
        ''' <param name="WSID">The current workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>An oject consisting of the typeahead values for the given column</returns>
        ''' <remarks></remarks>
        Public Shared Function selConsoleItemsTA(column As String, val As String, orderNum As String, WSID As String, User As String) As List(Of Object)
            Dim dataReader As SqlDataReader = Nothing
            Dim typeahead As New List(Of Object)
            Try
                dataReader = RunSPArray("selConsoleItemsTA", WSID, {{"@col", column, strVar}, {"@val", val, strVar}, {"@orderNum", orderNum, strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        typeahead.Add(New With {.value = CheckDBNull(dataReader(0)), .lineNum = CheckDBNull(dataReader(1)), .lineStatus = CheckDBNull(dataReader(2))})
                    End While
                End If


            Catch ex As Exception
                insertErrorMessages("CMConsolidation.vb", "selConsoleItemsTA", ex.Message, User, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return typeahead
        End Function

        ''' <summary>
        ''' Selects Data for Consolidation if muliple of the same Item exist
        ''' </summary>
        ''' <param name="ordernumber">The order number that is being displayed</param>
        ''' <param name="col">The column that is currently being used</param>
        ''' <param name="colVal">The value that the coloumn is being evalueated by</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The suer that is currenlty logged in</param>
        ''' <returns>A list of list of string which will populate the datatable in the select modal </returns>
        ''' <remarks></remarks>
        Public Shared Function selItemSelModelData(ordernumber As String, col As String, colVal As String, WSID As String, User As String) As List(Of List(Of String))
            Dim dataReader As SqlDataReader = Nothing
            Dim table As New List(Of List(Of String))
            Try

                dataReader = RunSPArray("selItemSelModelData", WSID, {{"@OrderNumber", ordernumber, strVar}, {"@Col", col, strVar}, {"@ColVal", colVal, strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        Dim row As New List(Of String)
                        For x As Integer = 0 To dataReader.FieldCount - 1
                            row.Add(CheckDBNull(dataReader(x)))
                        Next
                        table.Add(row)
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("CMConsolidation.vb", "selItemSelModelData", ex.Message, User, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return table
        End Function

        ''' <summary>
        ''' Selects data for the shipping label from the shipping transactions table
        ''' </summary>
        ''' <param name="orderNumber">The order number that will be checked in the stored procedure</param>
        ''' <param name="user">The user who is currently logged in</param>
        ''' <param name="WSID">The current workstation that the user is loggged in on</param>
        ''' <returns>a list of list of string for the shupping transactions datatable</returns>
        ''' <remarks></remarks>
        Public Shared Function insShipLabelData(orderNumber As String, user As String, WSID As String) As List(Of List(Of String))
            Dim dataReader As SqlDataReader = Nothing
            Dim table As New List(Of List(Of String))
            Try
                dataReader = RunSPArray("insShipLabelData", WSID, {{"@OrderNumber", orderNumber, strVar}, {"@User", user, strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        Dim row As New List(Of String)
                        For x As Integer = 0 To dataReader.FieldCount - 1
                            row.Add(CheckDBNull(dataReader(x)))
                        Next
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("CmConsolidation.vb", "insShipLabelData", ex.Message, user, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return table
        End Function
        ''' <summary>
        ''' Will select the default lookup type field in order to set the column dropdown to that column on page load
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>The column that is currently set within the CMPreferences table</returns>
        ''' <remarks></remarks>
        Public Shared Function selDefLookupType(WSID As String, User As String) As String
            Dim look As String = ""
            Dim DataReader As SqlDataReader = Nothing

            Try
                DataReader = RunSPArray("selCMPrefsDefLook", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    look = DataReader(0)
                End If
            Catch ex As Exception
                insertErrorMessages("CmConsolidation.vb", "selDefLookupType", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return look
        End Function

    End Class
End Namespace


