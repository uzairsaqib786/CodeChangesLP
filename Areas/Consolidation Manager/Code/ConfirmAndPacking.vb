' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Consolidation
    Public Class ConfirmAndPacking
        ''' <summary>
        ''' Selects the data for the tote information datatable 
        ''' </summary>
        ''' <param name="OrderNum">The order number that is being looked at</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>A list of list of string containing the table information</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackToteTable(OrderNum As String, WSID As String, User As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim toteTable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selConfPackToteDT", WSID, {{"@OrderNumber", OrderNum, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        row = New List(Of String)
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(CheckDBNull(DataReader(x)))
                        Next
                        toteTable.Add(row)
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackToteTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return toteTable
        End Function
        ''' <summary>
        ''' Selects the data for the shipping transactions datatable
        ''' </summary>
        ''' <param name="OrderNum">The order number that is being displayed</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>A list of list of string containing the table information</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackShipTransTable(OrderNum As String, WSID As String, User As String) As List(Of List(Of String))
            Dim DataReader As SqlDataReader = Nothing
            Dim transTable As New List(Of List(Of String))
            Dim row As New List(Of String)

            Try
                DataReader = RunSPArray("selConfPackShipTransDT", WSID, {{"@OrderNumber", OrderNum, strVar}, {"@User", User, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        row = New List(Of String)
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            row.Add(CheckDBNull(DataReader(x)))
                        Next
                        transTable.Add(row)
                    End While
                End If

            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackShipTransTable", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return transTable
        End Function
        ''' <summary>
        ''' Selects a list of all the container ids for the container id dropdown on the page
        ''' </summary>
        ''' <param name="OrderNum">The order number that is being displayed</param>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <returns>A list of string that contains all the container ids</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackContIDDrop(OrderNum As String, WSID As String, User As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim ContIDs As New List(Of String)

            Try
                DataReader = RunSPArray("selConfPackContID", WSID, {{"@OrderNumber", OrderNum, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            ContIDs.Add(CheckDBNull(DataReader(x)))
                        Next
                    End While
                End If

            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackContIDDrop", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return ContIDs
        End Function
        ''' <summary>
        ''' Will return a different string vlalue depending on if the current order number is marked as shipping complete
        ''' </summary>
        ''' <param name="OrderNum">The order number that is displayed</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <returns>Either an exmpty string if it's not marked, or the order number if it marked</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackShipComp(OrderNum As String, WSID As String, User As String) As String
            Dim DataReader As SqlDataReader = Nothing
            Dim retVal As String = ""

            Try
                DataReader = RunSPArray("selConfPackCheckShipComp", WSID, {{"@OrderNumber", OrderNum, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    retVal = CheckDBNull(DataReader(0))
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackShipComp", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return retVal
        End Function
        ''' <summary>
        ''' Will select the next container id for the page
        ''' </summary>
        ''' <param name="OrderNum">The order number that is displayed</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <returns>A string consisting of the next container id value</returns>
        ''' <remarks></remarks>
        Public Shared Function selContIDConfirmPack(OrderNum As String, WSID As String, User As String) As String
            Dim dataReader As SqlDataReader = Nothing
            Dim contID As String = ""
            Try
                dataReader = RunSPArray("selConfirmPackContID", WSID, {{"@OrderNum", OrderNum, strVar}, {"@WSID", WSID, strVar}})
                If dataReader.HasRows Then
                    dataReader.Read()
                    contID = CheckDBNull(dataReader(0))
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selContIDConfirmPack", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return contID
        End Function
        ''' <summary>
        ''' Will select the item number from the value that was scanned into the program
        ''' </summary>
        ''' <param name="Scanned">The value that was scanned</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>returns a string consisting of the itemnumber, which corresponds to the scanned value</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackScanItemNum(Scanned As String, WSID As String, User As String) As String
            Dim dataReader As SqlDataReader = Nothing
            Dim itemNumber As String = ""
            Dim startPos As Integer = 0
            Dim codeLength As Integer = 0
            Dim scanCode As String = ""
            Dim scanRange As String = ""

            Try
                dataReader = RunSPArray("selConfPackItemNum", WSID, {{"@Scanned", Scanned, strVar}})
                If dataReader.HasRows > 1 Then
                    While dataReader.Read()
                        If dataReader.FieldCount = 1 Then
                            itemNumber = dataReader(0)
                        Else
                            For x As Integer = 0 To dataReader.FieldCount - 1
                                startPos = dataReader("Start Position")
                                codeLength = dataReader("Code Length")
                                scanRange = Mid(Scanned, startPos, codeLength)
                                If (dataReader("Scan Code") = scanRange) Then
                                    itemNumber = dataReader("Item Number")
                                End If
                            Next
                        End If
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackScanItemNum", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return itemNumber
        End Function
        ''' <summary>
        ''' Checks if the current order number is confirmed and packed 
        ''' </summary>
        ''' <param name="OrderNum">The order number that is being checked</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <returns>A string which tells if the order is confirmed (string is disable) or not (string is empty)</returns>
        ''' <remarks></remarks>
        Public Shared Function selConfPackEnable(OrderNum As String, WSID As String, User As String) As String
            Dim DataReader As SqlDataReader = Nothing
            Dim retVal As String = ""

            Try
                DataReader = RunSPArray("selConfPackCheckConfPack", WSID, {{"@OrderNumber", OrderNum, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    retVal = CheckDBNull(DataReader(0))
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackShipComp", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return retVal
        End Function
        ''' <summary>
        ''' Selects the preferences associated with printing from the CMPreferences table 
        ''' </summary>
        ''' <param name="WSID">The workstation being worked on</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <returns>A object containing the values of all the print preferences</returns>
        ''' <remarks></remarks>
        Public Shared Function SelCMPrefs(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim Prefs As New Object

            Try
                DataReader = RunSPArray("selCMPrefs", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    DataReader.NextResult()
                    While DataReader.Read()
                        Prefs = New With {.PrintContPL = DataReader("Auto Print ContLabel"), 
                                          .PrintOrderPL = DataReader("Auto Print OrderPL"),
                                          .PrintContLabel = DataReader("Auto Print ContLabel"), 
                                          .PackListSort = DataReader("Packing List Sort")}
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("ConfirmAndPacking", "selConfPackShipComp", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return Prefs
        End Function
    End Class
End Namespace

