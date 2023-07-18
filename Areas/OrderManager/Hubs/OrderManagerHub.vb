' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Public Class OrderManagerHub
    Inherits Hub
    ''' <summary>
    '''  Selects the desired data for the OrderManager Table by fpopulating the order mmanager temp table which the datatable selects from
    ''' </summary>
    ''' <param name="Col">The find order(s) by dropdown</param>
    ''' <param name="ColVal1">The value 1 text box</param>
    ''' <param name="ColVal2">If where clause is between, this is the second value</param>
    ''' <param name="WhereClause">The case check boxes</param>
    ''' <param name="TransType">either pick, put away, or count</param> 
    ''' <param name="ViewType">either order headers or order lines</param>
    ''' <param name="OrderType">either open or pending</param>
    ''' <param name="MaxOrders">number of orders to be displayed</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function fillOrderManTempData(Col As String, ColVal1 As String, ColVal2 As String, WhereClause As String, TransType As String,
                                                ViewType As String, OrderType As String, MaxOrders As String, filter As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("insOrderManagerTemp", Context.QueryString.Get("WSID"), {{"@Col", Col, strVar},
                                                                                                                             {"@ColVal1", ColVal1, strVar},
                                                                                                                             {"@ColVal2", ColVal2, strVar},
                                                                                                                             {"@WhereClause", WhereClause, strVar},
                                                                                                                             {"@TransType", TransType, strVar},
                                                                                                                             {"@ViewType", ViewType, strVar},
                                                                                                                             {"@OrderType", OrderType, strVar},
                                                                                                                             {"@MaxOrders", MaxOrders, strVar},
                                                                                                                             {"@filter", filter, strVar},
                                                                                                                             {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "fillOrderManTempData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)


    End Function


    ''' <summary>
    ''' updates the desired records from the order manager page
    ''' </summary> 
    ''' <param name="ViewType">If headers or lines are being viewed</param>
    ''' <param name="OrderType">If Open or pending records are being shown</param>
    ''' <param name="ID">The id of the record that is being updated</param>
    ''' <param name="RequiredDate">The new required date value</param>
    ''' <param name="Notes">The new notes value</param>
    ''' <param name="Priority">The new priority value</param>
    ''' <param name="User1">The new user field 1 value</param>
    ''' <param name="User2">The new user field 2 value</param>
    ''' <param name="User3">The new user field 3 value</param>
    ''' <param name="User4">The new user field 4 value</param>
    ''' <param name="User5">The new user field 5 value</param>
    ''' <param name="User6">The new user field 6 value</param>
    ''' <param name="User7">The new user field 7 value</param>
    ''' <param name="User8">The new user field 8 value</param>
    ''' <param name="User9">The new user field 9 value</param>
    ''' <param name="User10">The new user field 10 value</param>
    ''' <param name="Emergency">The new emergency value</param>
    ''' <param name="Label">The new label value</param>
    ''' <param name="CheckRequiredDate">If this field is updated for all records</param>
    ''' <param name="CheckNotes">If this field is updated for all records</param>
    ''' <param name="CheckPriority">If this field is updated for all records</param>
    ''' <param name="CheckUser1">If this field is updated for all records</param>
    ''' <param name="CheckUser2">If this field is updated for all records</param>
    ''' <param name="CheckUser3">If this field is updated for all records</param>
    ''' <param name="CheckUser4">If this field is updated for all records</param>
    ''' <param name="CheckUser5">If this field is updated for all records</param>
    ''' <param name="CheckUser6">If this field is updated for all records</param>
    ''' <param name="CheckUser7">If this field is updated for all records</param>
    ''' <param name="CheckUser8">If this field is updated for all records</param>
    ''' <param name="CheckUser9">If this field is updated for all records</param>
    ''' <param name="CheckUser10">If this field is updated for all records</param>
    ''' <param name="CheckEmergency">If this field is updated for all records</param>
    ''' <param name="CheckLabel">If this field is updated for all records</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateOrderManagerRecords(ViewType As String, OrderType As String, ID As String, _
                                              RequiredDate As string, Notes As String, Priority As String, User1 As String, User2 As String, User3 As String, _
                                              User4 As String, User5 As String, User6 As String, User7 As String, User8 As String, User9 As String, User10 As String, _
                                              Emergency As String, Label As String, CheckRequiredDate As Boolean, CheckNotes As Boolean, _
                                              CheckPriority As Boolean, CheckUser1 As Boolean, CheckUser2 As Boolean, CheckUser3 As Boolean, _
                                              CheckUser4 As Boolean, CheckUser5 As Boolean, CheckUser6 As Boolean, CheckUser7 As Boolean, _
                                              CheckUser8 As Boolean, CheckUser9 As Boolean, CheckUser10 As Boolean, CheckEmergency As Boolean, _
                                              CheckLabel As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updOTPendOTOrderManager", Context.QueryString.Get("WSID"), {{"@ViewType", ViewType, strVar},
                                                                                                                                  {"@OrderType", OrderType, strVar},
                                                                                                                                  {"@ID1", ID, strVar},
                                                                                                                                  {"@RequiredDate", RequiredDate, If(RequiredDate = "", nullVar, dteVar)},
                                                                                                                                  {"@Notes", Notes, If(Notes = "", nullVar, strVar)},
                                                                                                                                  {"@Priority", Priority, strVar},
                                                                                                                                  {"@UserField1", User1, If(User1 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField2", User2, If(User2 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField3", User3, If(User3 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField4", User4, If(User4 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField5", User5, If(User5 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField6", User6, If(User6 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField7", User7, If(User7 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField8", User8, If(User8 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField9", User9, If(User9 = "", nullVar, strVar)},
                                                                                                                                  {"@UserField10", User10, If(User10 = "", nullVar, strVar)},
                                                                                                                                  {"@Emergency", Emergency, strVar},
                                                                                                                                  {"@Label", Label, strVar},
                                                                                                                                  {"@CheckRequiredDate", CheckRequiredDate, boolVar},
                                                                                                                                  {"@CheckNotes", CheckNotes, boolVar},
                                                                                                                                  {"@CheckPriority", CheckPriority, boolVar},
                                                                                                                                  {"@CheckUserField1", CheckUser1, boolVar},
                                                                                                                                  {"@CheckUserField2", CheckUser2, boolVar},
                                                                                                                                  {"@CheckUserField3", CheckUser3, boolVar},
                                                                                                                                  {"@CheckUserField4", CheckUser4, boolVar},
                                                                                                                                  {"@CheckUserField5", CheckUser5, boolVar},
                                                                                                                                  {"@CheckUserField6", CheckUser6, boolVar},
                                                                                                                                  {"@CheckUserField7", CheckUser7, boolVar},
                                                                                                                                  {"@CheckUserField8", CheckUser8, boolVar},
                                                                                                                                  {"@CheckUserField9", CheckUser9, boolVar},
                                                                                                                                  {"@CheckUserField10", CheckUser10, boolVar},
                                                                                                                                  {"@CheckEmergency", CheckEmergency, boolVar},
                                                                                                                                  {"@CheckLabel", CheckLabel, boolVar},
                                                                                                                                 {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "updateOrderManagerRecords", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try

                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' selects data from open transactions pending for the create orders data table
    ''' </summary>
    ''' <param name="orderNum">The displayed order number</param>
    ''' <returns>Object containing the data for the datatable</returns>
    ''' <remarks></remarks>
    Public Function selectCreateOrdersDT(orderNum As String, filter As String) As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim inners As New List(Of String)
                                                    Dim data As New List(Of List(Of String))
                                                    Dim otcount As Boolean = False
                                                    Try

                                                        Dim columnSequence = GlobalFunctions.getDefaultColumnSequence("Order Manager Create", Context.User.Identity.Name, Context.QueryString("WSID"))
                                                        DataReader = RunSPArray("selOTPendDT", Context.QueryString.Get("WSID"), {{"@OrderNumber", orderNum, strVar}, {"@filter", filter, strVar}})


                                                        If DataReader.HasRows Then
                                                            While DataReader.Read()
                                                                inners = New List(Of String)
                                                                If DataReader("Total") > 0 Then
                                                                    otcount = True
                                                                End If
                                                                For Each col In columnSequence
                                                                    inners.Add(CheckDBNull(DataReader(col)))
                                                                Next
                                                                data.Add(inners)
                                                            End While
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "selectCreateOrdersDT", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try
                                                    Return New With {.info = data, .count = otcount}
                                                End Function)

    End Function
    ''' <summary>
    ''' selects the user field data for the table, returns an object
    ''' </summary>
    ''' <returns>Obect containing the value for user fields 1-10</returns>
    ''' <remarks></remarks>
    Public Function selUserFieldData() As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim data As New Object
                                                    Try
                                                        DataReader = RunSPArray("selUserFieldData", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                        If DataReader.HasRows Then
                                                            While DataReader.Read()
                                                                data = New With {.UserField1 = CheckDBNull(DataReader("User Field1")), .UserField2 = CheckDBNull(DataReader("User Field2")),
                                                                                 .UserField3 = CheckDBNull(DataReader("User Field3")), .UserField4 = CheckDBNull(DataReader("User Field4")),
                                                                                 .UserField5 = CheckDBNull(DataReader("User Field5")), .UserField6 = CheckDBNull(DataReader("User Field6")),
                                                                                 .UserField7 = CheckDBNull(DataReader("User Field7")), .UserField8 = CheckDBNull(DataReader("User Field8")),
                                                                                 .UserField9 = CheckDBNull(DataReader("User Field9")), .UserField10 = CheckDBNull(DataReader("User Field10"))}
                                                            End While
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("OMPreferencesHub", "selUserFieldData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try
                                                    Return data
                                                End Function)
    End Function
    ''' <summary>
    ''' updates the user field data table
    ''' </summary>
    ''' <param name="UserField1">The value for user field 1</param>
    ''' <param name="UserField2">The value for user field 2</param>
    ''' <param name="UserField3">The value for user field 3</param>
    ''' <param name="UserField4">The value for user field 4</param>
    ''' <param name="UserField5">The value for user field 5</param>
    ''' <param name="UserField6">The value for user field 6</param>
    ''' <param name="UserField7">The value for user field 7</param>
    ''' <param name="UserField8">The value for user field 8</param>
    ''' <param name="UserField9">The value for user field 9</param>
    ''' <param name="UserField10">The value for user field 10</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updUserFieldData(UserField1 As String, UserField2 As String, UserField3 As String, UserField4 As String, UserField5 As String, UserField6 As String, UserField7 As String,
                                     UserField8 As String, UserField9 As String, UserField10 As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updUserFieldData", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@UserField1", UserField1, strVar}, _
                                                                                                                          {"@UserField2", UserField2, strVar}, {"@UserField3", UserField3, strVar}, {"@UserField4", UserField4, strVar}, _
                                                                                                                          {"@UserField5", UserField5, strVar}, {"@UserField6", UserField6, strVar}, {"@UserField7", UserField7, strVar}, _
                                                                                                                          {"@UserField8", UserField8, strVar}, {"@UserField9", UserField9, strVar}, {"@UserField10", UserField10, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManagerHub", "updUserFieldData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' deletes the desired records from open transactions pending
    ''' inserts into an event log
    ''' </summary>
    ''' <param name="IDS">A list containg the displayed ids to delete</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteOTPend(IDS As List(Of String)) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try

                                                        RunActionSP("delOTPend", Context.QueryString.Get("WSID"), {{"@IDS", String.Join(",", IDS.ToArray), strVar}, _
                                                                                                                   {"@DeletedBy", Context.User.Identity.Name, strVar}, _
                                                                                                                   {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "deleteOTPend", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try

                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' releases orders for order manager
    ''' insert into open transactions pending history, and open transactions
    ''' Deletes from open transaction pending
    ''' </summary>
    ''' <param name="val">The order number or view that is being relased</param>
    ''' <param name="page">The page that it is coming from</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ReleaseOrders(val As String, page As String) As String
        Try
            If page = "Create Orders" Then
                RunActionSP("insOTOrderMan", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@OrderNumber", val, strVar}})
            Else
                RunActionSP("insOTOrderMan", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@View", val, strVar}})
            End If

        Catch ex As Exception
            insertErrorMessages("OrderManager", "ReleaseOrders", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            Return "Error"
        End Try
        Return "Success"
    End Function
    ''' <summary>
    ''' deletes the desired records from ot temp
    ''' </summary>
    ''' <param name="ViewType">headers or lines</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteOMOTPend(ViewType As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delOTPendOM", Context.QueryString.Get("WSID"), {{"@DeletedBy", Context.User.Identity.Name, strVar}, _
                                                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                     {"@ViewType", ViewType, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "deleteOTPend", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Inserts a new record into the open transactions pending table
    ''' </summary>
    ''' Fields for the new record below
    ''' <param name="OrderNumber"></param>
    ''' <param name="TransType"></param>
    ''' <param name="Warehouse"></param>
    ''' <param name="ItemNumber"></param>
    ''' <param name="Description"></param>
    ''' <param name="UnitofMeasure"></param>
    ''' <param name="TransQty"></param>
    ''' <param name="LineNumber"></param>
    ''' <param name="Priority"></param>
    ''' <param name="RequiredDate"></param>
    ''' <param name="HostTransID"></param>
    ''' <param name="Emergency"></param>
    ''' <param name="Label"></param>
    ''' <param name="LotNumber"></param>
    ''' <param name="ExpirationDate"></param>
    ''' <param name="SerialNumber"></param>
    ''' <param name="Revision"></param>
    ''' <param name="BatchPickID"></param>
    ''' <param name="ToteID"></param>
    ''' <param name="Cell"></param>
    ''' <param name="Notes"></param>
    ''' <param name="UserField1"></param>
    ''' <param name="UserField2"></param>
    ''' <param name="UserField3"></param>
    ''' <param name="UserField4"></param>
    ''' <param name="UserField5"></param>
    ''' <param name="UserField6"></param>
    ''' <param name="UserField7"></param>
    ''' <param name="UserField8"></param>
    ''' <param name="UserField9"></param>
    ''' <param name="UserField10"></param>
    ''' <param name="InProcess"></param>
    ''' <param name="ProcessBy"></param>
    ''' <param name="ImportBy"></param>
    ''' <param name="ImportDate"></param>
    ''' <param name="ImportFileName"></param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function insertOTTemp(OrderNumber As String, TransType As String, Warehouse As String, ItemNumber As String, Description As String, _
                                 UnitofMeasure As String, TransQty As Nullable(Of Integer), LineNumber As Nullable(Of Integer), Priority As Nullable(Of Integer), RequiredDate As String, _
                                 HostTransID As String, Emergency As Boolean, Label As Boolean, LotNumber As String, ExpirationDate As String, _
                                 SerialNumber As String, Revision As String, BatchPickID As String, ToteID As String, Cell As String, _
                                 Notes As String, UserField1 As String, UserField2 As String, UserField3 As String, UserField4 As String, _
                                 UserField5 As String, UserField6 As String, UserField7 As String, UserField8 As String, UserField9 As String, _
                                 UserField10 As String, InProcess As Boolean, ProcessBy As String, ImportBy As String, ImportDate As String, _
                                 ImportFileName As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        If ImportDate = "" Then
                                                            ImportDate = Now.ToString()
                                                        End If
                                                        RunActionSP("insOTTemp", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNumber, strVar}, _
                                                                                                                   {"@TransType", TransType, strVar}, _
                                                                                                                   {"@Warehouse", Warehouse, strVar}, _
                                                                                                                   {"@ItemNumber", ItemNumber, strVar}, _
                                                                                                                   {"@Description", Description, strVar}, _
                                                                                                                   {"@UoM", UnitofMeasure, strVar}, _
                                                                                                                   {"@TransQty", If(TransQty.HasValue, TransQty.Value, nullVar), intVar}, _
                                                                                                                   {"@LineNum", If(LineNumber.HasValue, LineNumber.Value, nullVar), intVar}, _
                                                                                                                   {"@Priority", If(Priority.HasValue, Priority.Value, nullVar), intVar}, _
                                                                                                                   {"@ReqDate", RequiredDate, dteVar}, _
                                                                                                                   {"@HostTransID", HostTransID, strVar}, _
                                                                                                                   {"@Emergency", Emergency, boolVar}, _
                                                                                                                   {"@Label", Label, boolVar}, _
                                                                                                                   {"@LotNumber", LotNumber, strVar}, _
                                                                                                                   {"@ExpirationDate", ExpirationDate, dteVar}, _
                                                                                                                   {"@SerialNumber", SerialNumber, strVar}, _
                                                                                                                   {"@Revision", Revision, strVar}, _
                                                                                                                   {"@BatchPickID", BatchPickID, strVar}, _
                                                                                                                   {"@ToteID", ToteID, strVar}, _
                                                                                                                   {"@Cell", Cell, strVar}, _
                                                                                                                   {"@Notes", Notes, strVar}, _
                                                                                                                   {"@UserField1", UserField1, strVar}, _
                                                                                                                   {"@UserField2", UserField2, strVar}, _
                                                                                                                   {"@UserField3", UserField3, strVar}, _
                                                                                                                   {"@UserField4", UserField4, strVar}, _
                                                                                                                   {"@UserField5", UserField5, strVar}, _
                                                                                                                   {"@UserField6", UserField6, strVar}, _
                                                                                                                   {"@UserField7", UserField7, strVar}, _
                                                                                                                   {"@UserField8", UserField8, strVar}, _
                                                                                                                   {"@UserField9", UserField9, strVar}, _
                                                                                                                   {"@UserField10", UserField10, strVar}, _
                                                                                                                   {"@InProcess", InProcess, boolVar}, _
                                                                                                                   {"@ProcBy", ProcessBy, strVar}, _
                                                                                                                   {"@ImportBy", ImportBy, strVar}, _
                                                                                                                   {"@ImportDate", ImportDate, dteVar}, _
                                                                                                                   {"@ImportFileName", ImportFileName, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "insertOTTemp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' updates desired record in the open transactions pending table
    ''' </summary>
    ''' <param name="ID">The id of the record to be updated</param>
    ''' The new values for the fields below
    ''' <param name="TransType"></param>
    ''' <param name="Warehouse"></param>
    ''' <param name="ItemNumber"></param>
    ''' <param name="Description"></param>
    ''' <param name="UnitofMeasure"></param>
    ''' <param name="TransQty"></param>
    ''' <param name="LineNumber"></param>
    ''' <param name="Priority"></param>
    ''' <param name="RequiredDate"></param>
    ''' <param name="HostTransID"></param>
    ''' <param name="Emergency"></param>
    ''' <param name="Label"></param>
    ''' <param name="LotNumber"></param>
    ''' <param name="ExpirationDate"></param>
    ''' <param name="SerialNumber"></param>
    ''' <param name="Revision"></param>
    ''' <param name="BatchPickID"></param>
    ''' <param name="ToteID"></param>
    ''' <param name="Cell"></param>
    ''' <param name="Notes"></param>
    ''' <param name="UserField1"></param>
    ''' <param name="UserField2"></param>
    ''' <param name="UserField3"></param>
    ''' <param name="UserField4"></param>
    ''' <param name="UserField5"></param>
    ''' <param name="UserField6"></param>
    ''' <param name="UserField7"></param>
    ''' <param name="UserField8"></param>
    ''' <param name="UserField9"></param>
    ''' <param name="UserField10"></param>
    ''' <param name="InProcess"></param>
    ''' <param name="ProcessBy"></param>
    ''' <param name="ImportBy"></param>
    ''' <param name="ImportDate"></param>
    ''' <param name="ImportFileName"></param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateOTTemp(ID As Integer, TransType As String, Warehouse As String, ItemNumber As String, Description As String, _
                                 UnitofMeasure As String, TransQty As Integer, LineNumber As Integer, Priority As Integer, RequiredDate As String, _
                                 HostTransID As String, Emergency As Boolean, Label As Boolean, LotNumber As String, ExpirationDate As String, _
                                 SerialNumber As String, Revision As String, BatchPickID As String, ToteID As String, Cell As String, _
                                 Notes As String, UserField1 As String, UserField2 As String, UserField3 As String, UserField4 As String, _
                                 UserField5 As String, UserField6 As String, UserField7 As String, UserField8 As String, UserField9 As String, _
                                 UserField10 As String, InProcess As Boolean, ProcessBy As String, ImportBy As String, ImportDate As String, _
                                 ImportFileName As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updOTTemp", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar}, _
                                                                                                                   {"@TransType", TransType, strVar}, _
                                                                                                                   {"@Warehouse", Warehouse, strVar}, _
                                                                                                                   {"@ItemNumber", ItemNumber, strVar}, _
                                                                                                                   {"@Description", Description, strVar}, _
                                                                                                                   {"@UoM", UnitofMeasure, strVar}, _
                                                                                                                   {"@TransQty", TransQty, intVar}, _
                                                                                                                   {"@LineNum", LineNumber, intVar}, _
                                                                                                                   {"@Priority", Priority, intVar}, _
                                                                                                                   {"@ReqDate", RequiredDate, dteVar}, _
                                                                                                                   {"@HostTransID", HostTransID, strVar}, _
                                                                                                                   {"@Emergency", Emergency, boolVar}, _
                                                                                                                   {"@Label", Label, boolVar}, _
                                                                                                                   {"@LotNumber", LotNumber, strVar}, _
                                                                                                                   {"@ExpirationDate", ExpirationDate, dteVar}, _
                                                                                                                   {"@SerialNumber", SerialNumber, strVar}, _
                                                                                                                   {"@Revision", Revision, strVar}, _
                                                                                                                   {"@BatchPickID", BatchPickID, strVar}, _
                                                                                                                   {"@ToteID", ToteID, strVar}, _
                                                                                                                   {"@Cell", Cell, strVar}, _
                                                                                                                   {"@Notes", Notes, strVar}, _
                                                                                                                   {"@UserField1", UserField1, strVar}, _
                                                                                                                   {"@UserField2", UserField2, strVar}, _
                                                                                                                   {"@UserField3", UserField3, strVar}, _
                                                                                                                   {"@UserField4", UserField4, strVar}, _
                                                                                                                   {"@UserField5", UserField5, strVar}, _
                                                                                                                   {"@UserField6", UserField6, strVar}, _
                                                                                                                   {"@UserField7", UserField7, strVar}, _
                                                                                                                   {"@UserField8", UserField8, strVar}, _
                                                                                                                   {"@UserField9", UserField9, strVar}, _
                                                                                                                   {"@UserField10", UserField10, strVar}, _
                                                                                                                   {"@InProcess", InProcess, boolVar}, _
                                                                                                                   {"@ProcBy", ProcessBy, strVar}, _
                                                                                                                   {"@ImportBy", ImportBy, strVar}, _
                                                                                                                   {"@ImportDate", ImportDate, dteVar}, _
                                                                                                                   {"@ImportFileName", ImportFileName, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("OrderManager Hub", "updateOTTemp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Error"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Return Item Data for Create Order Modal on enter click
    ''' </summary>
    ''' <param name="ItemNumber">Item Number to get Data for</param>
    ''' <returns>Gets the data fora given item number</returns>
    ''' <remarks></remarks>
    Public Function GetItemData(ItemNumber As String)
        Return PickPro_Web.ItemNumber.getItem(ItemNumber, Context.User.Identity.Name, "---", Context.QueryString.Get("WSID"), True)
    End Function

End Class
