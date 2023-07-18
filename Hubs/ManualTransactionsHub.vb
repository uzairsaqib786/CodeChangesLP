' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class ManualTransactionsHub
    Inherits Hub
    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
    ''' </summary>
    ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task
        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Saves a new transaction to the temporary open transactions table
    ''' </summary>
    ''' <param name="orderNumber">Order number to save</param>
    ''' <param name="itemNumber">item number of the transaction</param>
    ''' <param name="transactionType">the trasnaction type of the transaction</param>
    ''' <param name="invMapID">The inventory map id of the transaction</param>
    ''' <returns>False if the order number already exists.</returns>
    ''' <remarks></remarks>
    Public Function saveNewTransaction(orderNumber As String, itemNumber As String, transactionType As String, invMapID As Integer) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function()
                                                     Dim identity As Integer = -1, DataReader As SqlDataReader = Nothing
                                                     Try
                                                         DataReader = RunSPArray("insXferOT", Context.QueryString.Get("WSID"), {{"@OrderNumber", orderNumber, strVar},
                                                                                                          {"@ItemNumber", itemNumber, strVar},
                                                                                                          {"@TransactionType", transactionType, strVar},
                                                                                                          {"@InvMapID", invMapID, intVar}})
                                                         If DataReader.HasRows Then
                                                             If DataReader.Read() Then
                                                                 If Not IsDBNull(DataReader(0)) Then
                                                                     identity = CInt(DataReader(0))
                                                                 End If
                                                             End If
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine("saveNewTransaction" & ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "saveNewTransaction", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(DataReader) Then
                                                             DataReader.Dispose()
                                                         End If
                                                     End Try
                                                     Return identity
                                                 End Function)
    End Function

    ''' <summary>
    ''' Returns field values of a Manual Transaction specified by ID
    ''' </summary>
    ''' <param name="ID">ID of the Manual Transaction to get from the database.</param>
    ''' <returns>The information about the desired manual trasnaction</returns>
    ''' <remarks></remarks>
    Public Function getTransactionInfo(ID As Integer) As Task(Of List(Of Dictionary(Of String, Object)))
        Return Task(Of List(Of Dictionary(Of String, Object))).Factory.StartNew(Function()
                                                                                    Return ManualTransactions.getTransactionInfo(ID, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                                End Function)
    End Function

    ''' <summary>
    ''' Gets the quantity/location combinations from inventory map based on item number.
    ''' </summary>
    ''' <param name="item">Item Number to look for in Inv Map</param>
    ''' <returns>A list of object that contains that quantity, location, and map id of the desired item number</returns>
    ''' <remarks></remarks>
    Public Function getQty(item As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function()
                                                             Dim datareader As SqlDataReader = Nothing, objList As New List(Of Object)
                                                             Try
                                                                 datareader = RunSPArray("selInvMapItemQuant", Context.QueryString.Get("WSID"), {{"@ItemNumber", item, strVar}})

                                                                 If datareader.HasRows Then
                                                                     While datareader.Read()
                                                                         objList.Add(New With {.qty = IIf(IsDBNull(datareader(0)), 0, datareader(0)),
                                                                                               .location = IIf(IsDBNull(datareader(1)), "", datareader(1)),
                                                                                               .mapID = IIf(IsDBNull(datareader(2)), 0, datareader(2))})
                                                                     End While
                                                                 End If
                                                             Catch ex As Exception
                                                                 Debug.WriteLine(ex.ToString())
                                                                 insertErrorMessages("ManualTransactionsHub", "getQty", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(datareader) Then
                                                                     datareader.Dispose()
                                                                 End If
                                                             End Try
                                                             Return objList
                                                         End Function)
    End Function

    ''' <summary>
    ''' Gets selected location's information
    ''' </summary>
    ''' <param name="invMapID">Inv Map ID of the location</param>
    ''' <returns>A list of string that contaisn the information of the desired location</returns>
    ''' <remarks></remarks>
    Public Function getLocationData(invMapID As Integer) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function()
                                                    Dim datareader As SqlDataReader = Nothing, location As New List(Of String)
                                                    Dim locationObj As Object = New With {.Zone = "-", .Carousel = "-", .Row = "-", .Shelf = "-", .Bin = "-",
                                                                                          .TotalQty = "0", .PickQty = "0", .PutQty = "0"}
                                                    Dim index As Integer = 0
                                                    Try
                                                        datareader = RunSPArray("selOTInvMapCount", Context.QueryString.Get("WSID"), {{"@InvMapID", invMapID, intVar}})

                                                        While datareader.HasRows
                                                            If datareader.Read() Then
                                                                For x As Integer = 0 To datareader.FieldCount - 1
                                                                    If Not IsDBNull(datareader(x)) Then
                                                                        location.Add(IIf(CStr(datareader(x)) = "", "-", datareader(x)))
                                                                    Else
                                                                        location.Add("-")
                                                                    End If
                                                                Next
                                                                If index = 0 Then
                                                                    locationObj.Zone = location(0)
                                                                    locationObj.Carousel = location(1)
                                                                    locationObj.Row = location(2)
                                                                    locationObj.Shelf = location(3)
                                                                    locationObj.Bin = location(4)
                                                                ElseIf index = 1 Then
                                                                    locationObj.TotalQty = location(0)
                                                                ElseIf index = 2 Then
                                                                    locationObj.PickQty = location(0)
                                                                ElseIf index = 3 Then
                                                                    locationObj.PutQty = location(0)
                                                                End If
                                                                location = New List(Of String)
                                                            End If
                                                            index += 1
                                                            datareader.NextResult()
                                                        End While
                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("ManualTransactionsHub", "getLocationData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(datareader) Then
                                                            datareader.Dispose()
                                                        End If
                                                    End Try

                                                    Return locationObj
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes specified transaction from xfer Open Transactions (manual transactions)
    ''' </summary>
    ''' <param name="TransID">xfer Open Transactions [ID] field to delete entry for.</param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function deleteTransaction(TransID As Integer) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim user As String = Context.User.Identity.Name, WSID As String = Context.QueryString.Get("WSID")
                                         Try
                                             RunActionSP("delXferOT", WSID,
                                                         {{"@ID", TransID, intVar}, {"@User", user, strVar},
                                                          {"@WSID", WSID, strVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.ToString())
                                             insertErrorMessages("ManualTransactionsHub", "deleteTransaction", ex.ToString(), user, WSID)
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Updates a manual transaction
    ''' </summary>
    ''' <param name="newValues">Values to update to.</param>
    ''' <param name="transID">Manual Transaction ID to update</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveTransaction(newValues As List(Of String), transID As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     If False Then
                                                         Return False
                                                     Else
                                                         Try
                                                             Dim ItemNumber As String = newValues(0), TransType As String = newValues(1), expDate As DateTime = nullDate

                                                             Dim expDateResult = DateTime.TryParse(newValues(2), expDate)
                                                             If expDateResult Then
                                                                 expDate = newValues(2)
                                                             End If

                                                             Dim revision As String = newValues(3), description As String = newValues(4), lot As String = newValues(5), UoM As String = newValues(6)
                                                             Dim notes As String = newValues(7), serial As String = newValues(8), required As DateTime = nullDate

                                                             Dim requiredResult = DateTime.TryParse(newValues(9), required)
                                                             If requiredResult Then
                                                                 required = newValues(9)
                                                             End If

                                                             Dim line As Integer = 0
                                                             Dim lineResult = Integer.TryParse(newValues(10), line)
                                                             If lineResult Then
                                                                 line = newValues(10)
                                                             End If

                                                             Dim transQty As Integer = 0
                                                             Dim transQtyResult = Integer.TryParse(newValues(11), transQty)
                                                             If transQtyResult Then
                                                                 transQty = newValues(11)
                                                             End If

                                                             Dim priority As Integer = 0
                                                             Dim priorityResult = Integer.TryParse(newValues(12), priority)
                                                             If priorityResult Then
                                                                 priority = newValues(12)
                                                             End If

                                                             Dim lineSeq As Integer = 0
                                                             Dim lineSeqResult = Integer.TryParse(newValues(13), lineSeq)
                                                             If lineSeqResult Then
                                                                 lineSeq = newValues(13)
                                                             End If

                                                             Dim host As Integer = 0
                                                             Dim hostResult = Integer.TryParse(newValues(14), host)
                                                             If hostResult Then
                                                                 host = newValues(14)
                                                             End If


                                                             Dim batch As String = newValues(15)
                                                             Dim emergency As Integer = IIf(newValues(16), 1, 0), warehouse As String = newValues(17), tote As String = newValues(18)
                                                             Dim zone As String = newValues(19), shelf As String = newValues(20), carousel As String = newValues(21)
                                                             Dim row As String = newValues(22), bin As String = newValues(23), inv As Integer = 0

                                                             Dim invResult = Integer.TryParse(newValues(24), inv)
                                                             If invResult Then
                                                                 inv = newValues(24)
                                                             End If

                                                             RunActionSP("updXferOT", Context.QueryString.Get("WSID"), {{"@ID", transID, intVar},
                                                                                                     {"@ItemNumber", ItemNumber, strVar},
                                                                                                     {"@TransactionType", TransType, strVar},
                                                                                                     {"@ExpirationDate", expDate, dteVar},
                                                                                                     {"@Revision", revision, strVar},
                                                                                                     {"@Description", description, strVar},
                                                                                                     {"@LotNumber", lot, strVar},
                                                                                                     {"@UoM", UoM, strVar},
                                                                                                     {"@Notes", notes, strVar},
                                                                                                     {"@SerialNumber", serial, strVar},
                                                                                                     {"@RequiredDate", required, dteVar},
                                                                                                     {"@LineNumber", line, strVar},
                                                                                                     {"@TransQty", transQty, intVar},
                                                                                                     {"@Priority", priority, intVar},
                                                                                                     {"@LineSequence", lineSeq, intVar},
                                                                                                     {"@HostTransID", host, intVar},
                                                                                                     {"@BatchPickID", batch, strVar},
                                                                                                     {"@Emergency", emergency, intVar},
                                                                                                     {"@ToteID", tote, strVar},
                                                                                                     {"@Zone", zone, strVar},
                                                                                                     {"@Shelf", shelf, strVar},
                                                                                                     {"@Carousel", carousel, strVar},
                                                                                                     {"@Row", row, strVar},
                                                                                                     {"@Bin", bin, strVar},
                                                                                                     {"@Warehouse", warehouse, strVar},
                                                                                                     {"@InvMapID", inv, intVar}})
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("ManualTransactionsHub", "saveTransactions", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End If
                                                 End Function)
    End Function

    ''' <summary>
    ''' Posts manual transaction to open transactions
    ''' </summary>
    ''' <param name="transactionID">ID of the manual transaction to post.</param>
    ''' <param name="deleteTrans">If the trasnactions is going to be deleted</param>
    ''' <returns>A boolean telling iif the operationc ompleted successfully</returns>
    ''' <remarks></remarks>
    Public Function postTransaction(transactionID As Integer, deleteTrans As Boolean) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("insOTManTrans", Context.QueryString.Get("WSID"), {{"@ID", transactionID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@Delete", IIf(deleteTrans, 1, 0), intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                         'GlobalFunctions.checkEmergencyTransactions(user:=Context.User.Identity.Name, WSID:=Context.QueryString.Get("WSID"))
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "postTransaction", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    Public Function ValidateManOrder(OrderNum As String, TransType As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim datareader As SqlDataReader = Nothing

                                                     Try
                                                         datareader = RunSPArray("selXferOTGenOrderValid", Context.QueryString.Get("WSID"), {{"@OrderNumber", OrderNum, strVar},
                                                                                                                                             {"@TransType", TransType, strVar}})
                                                         If datareader.HasRows() Then
                                                             'order exists with a different trans type
                                                             Return False
                                                         End If
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "postTransaction", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     Finally
                                                         If Not IsNothing(datareader) Then
                                                             datareader.Dispose()
                                                         End If
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function SelItemFromVal(Val As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("selInvItemFromVal", Context.QueryString.Get("WSID"), {{"@Value", Val, strVar}})

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            Return DataReader("Item Number")
                                                        End If

                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("ManualTransactionsHub", "SelItemFromVal", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return "Invalid"
                                                End Function)
    End Function

    Public Function InsertTransForOrder(OrderNumber As String, TransType As String, ItemNum As String, TransQty As Integer, ReqDate As String, ExpDate As String,
                                        LineNum As String, LineSeq As String, Priority As String, ToteNum As String, BatchPickID As String, Warehouse As String,
                                        LotNum As String, SerialNum As String, HostTransID As String, Emergency As String, Notes As String,
                                        UserField1 As String, UserField2 As String, UserField3 As String, UserField4 As String, UserField5 As String, UserField6 As String,
                                        UserField7 As String, UserField8 As String, UserField9 As String, UserField10 As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try

                                                         'insert transaction for order and transs type
                                                         RunActionSP("insXferOTOrderTrans", Context.QueryString.Get("WSID"),
                                                                    {{"@OrderNum", OrderNumber, strVar},
                                                                    {"@TransType", TransType, strVar},
                                                                    {"@ItemNum", ItemNum, strVar},
                                                                    {"@TransQty", TransQty, intVar},
                                                                    {"@ReqDate", ReqDate, If(ReqDate = "", nullVar, dteVar)},
                                                                    {"@ExpDate", ExpDate, If(ExpDate = "", nullVar, dteVar)},
                                                                    {"@LineNum", If(LineNum = "", "0", LineNum), intVar},
                                                                    {"@LineSeq", If(LineSeq = "", "0", LineSeq), intVar},
                                                                    {"@Priority", If(Priority = "", "0", Priority), intVar},
                                                                    {"@ToteNum", If(ToteNum = "", "0", ToteNum), intVar},
                                                                    {"@BatchPickID", BatchPickID, If(BatchPickID = "", nullVar, strVar)},
                                                                    {"@Warehouse", Warehouse, If(Warehouse = "", nullVar, strVar)},
                                                                    {"@LotNum", LotNum, If(LotNum = "", nullVar, strVar)},
                                                                    {"@SerialNum", SerialNum, If(SerialNum = "", nullVar, strVar)},
                                                                    {"@HostTransID", HostTransID, If(HostTransID = "", nullVar, strVar)},
                                                                    {"@Emergency", If(Emergency.ToLower() = "yes", True, False), boolVar},
                                                                    {"@Notes", Notes, If(Notes = "", nullVar, strVar)},
                                                                    {"@UserField1", UserField1, If(UserField1 = "", nullVar, strVar)},
                                                                    {"@UserField2", UserField2, If(UserField2 = "", nullVar, strVar)},
                                                                    {"@UserField3", UserField3, If(UserField3 = "", nullVar, strVar)},
                                                                    {"@UserField4", UserField4, If(UserField4 = "", nullVar, strVar)},
                                                                    {"@UserField5", UserField5, If(UserField5 = "", nullVar, strVar)},
                                                                    {"@UserField6", UserField6, If(UserField6 = "", nullVar, strVar)},
                                                                    {"@UserField7", UserField7, If(UserField7 = "", nullVar, strVar)},
                                                                    {"@UserField8", UserField8, If(UserField8 = "", nullVar, strVar)},
                                                                    {"@UserField9", UserField9, If(UserField9 = "", nullVar, strVar)},
                                                                    {"@UserField10", UserField10, If(UserField10 = "", nullVar, strVar)}}
                                                                    )


                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "InsertTransForOrder", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function UpdateTransForOrder(ID As Integer, ItemNum As String, TransQty As Integer, ReqDate As String, ExpDate As String,
                                        LineNum As String, LineSeq As String, Priority As String, ToteNum As String, BatchPickID As String, Warehouse As String,
                                        LotNum As String, SerialNum As String, HostTransID As String, Emergency As String, Notes As String,
                                        UserField1 As String, UserField2 As String, UserField3 As String, UserField4 As String, UserField5 As String, UserField6 As String,
                                        UserField7 As String, UserField8 As String, UserField9 As String, UserField10 As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try

                                                         'insert transaction for order and transs type
                                                         RunActionSP("updXferOTOrderTrans", Context.QueryString.Get("WSID"),
                                                                    {{"@ID", ID, intVar},
                                                                    {"@ItemNum", ItemNum, strVar},
                                                                    {"@TransQty", TransQty, intVar},
                                                                    {"@ReqDate", ReqDate, If(ReqDate = "", nullVar, dteVar)},
                                                                    {"@ExpDate", ExpDate, If(ExpDate = "", nullVar, dteVar)},
                                                                    {"@LineNum", If(LineNum = "", "0", LineNum), intVar},
                                                                    {"@LineSeq", If(LineSeq = "", "0", LineSeq), intVar},
                                                                    {"@Priority", If(Priority = "", "0", Priority), intVar},
                                                                    {"@ToteNum", If(ToteNum = "", "0", ToteNum), intVar},
                                                                    {"@BatchPickID", BatchPickID, If(BatchPickID = "", nullVar, strVar)},
                                                                    {"@Warehouse", Warehouse, If(Warehouse = "", nullVar, strVar)},
                                                                    {"@LotNum", LotNum, If(LotNum = "", nullVar, strVar)},
                                                                    {"@SerialNum", SerialNum, If(SerialNum = "", nullVar, strVar)},
                                                                    {"@HostTransID", HostTransID, If(HostTransID = "", nullVar, strVar)},
                                                                    {"@Emergency", If(Emergency.ToLower() = "yes", True, False), boolVar},
                                                                    {"@Notes", Notes, If(Notes = "", nullVar, strVar)},
                                                                    {"@UserField1", UserField1, If(UserField1 = "", nullVar, strVar)},
                                                                    {"@UserField2", UserField2, If(UserField2 = "", nullVar, strVar)},
                                                                    {"@UserField3", UserField3, If(UserField3 = "", nullVar, strVar)},
                                                                    {"@UserField4", UserField4, If(UserField4 = "", nullVar, strVar)},
                                                                    {"@UserField5", UserField5, If(UserField5 = "", nullVar, strVar)},
                                                                    {"@UserField6", UserField6, If(UserField6 = "", nullVar, strVar)},
                                                                    {"@UserField7", UserField7, If(UserField7 = "", nullVar, strVar)},
                                                                    {"@UserField8", UserField8, If(UserField8 = "", nullVar, strVar)},
                                                                    {"@UserField9", UserField9, If(UserField9 = "", nullVar, strVar)},
                                                                    {"@UserField10", UserField10, If(UserField10 = "", nullVar, strVar)}}
                                                                    )


                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "UpdateTransForOrder", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function DeleteManOrder(OrderNumber As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("delXferOTOrder", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNumber, strVar}})
                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "DeleteManOrder", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function PostManOrder(OrderNumber As String, ToteID As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("insOTFromXferOTOrder", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNumber, strVar}, {"@ToteID", ToteID, If(ToteID = "", nullVar, strVar)},
                                                                                                                                {"@User", Context.User.Identity.Name, strVar}})

                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.ToString())
                                                         insertErrorMessages("ManualTransactionsHub", "PostManOrder", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function
End Class
