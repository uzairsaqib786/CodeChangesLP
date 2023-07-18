' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class PickToteSetupHub
    Inherits Hub
    ''' <summary>
    ''' Gets the count info for the mixed, carousel, and off caorusel
    ''' </summary>
    ''' <returns>object containg the count data</returns>
    ''' <remarks></remarks>
    Public Function selectCountInfo() As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Return Induction.PickToteSetup.getCountInfo(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets the next batch id 
    ''' </summary>
    ''' <returns>string containg the next batch id</returns>
    ''' <remarks></remarks>
    Public Function nextBatchID() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Return GlobalFunctions.getNextBatchID(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function

    ''' <summary>
    ''' Tells if ogiven ordernumber is valid
    ''' </summary>
    ''' <param name="OrderNumber">order number to be checked</param>
    ''' <returns>string telling if the order number is valid</returns>
    ''' <remarks></remarks>
    Public Function validateOrderNumber(OrderNumber As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim result As String = ""

                                                    Try
                                                        DataReader = RunSPArray("selOTPickToteSetupOrderNum", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNumber, strVar}})
                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            result = DataReader(0)
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("PickToteSetupHub", "validateOrderNumber", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try
                                                    Return result
                                                End Function)
    End Function

    ''' <summary>
    ''' Gets a list of order numbers depending on  the type
    ''' </summary>
    ''' <param name="Type">Either MIxed, Carousel, or Off Carousel</param>
    ''' <returns>list of string containg the order numbers</returns>
    ''' <remarks></remarks>
    Public Function fillOrderNums(Type As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Dim DataReader As SqlDataReader = Nothing
                                                             Dim Data As New List(Of String)

                                                             Try
                                                                 DataReader = RunSPArray("selOTPickToteSetupAutoFillOrderNum", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                                                                 {"@Type", Type, strVar}})
                                                                 If DataReader.HasRows Then
                                                                     While DataReader.Read()
                                                                         Data.Add(DataReader(0))
                                                                     End While
                                                                 End If
                                                             Catch ex As Exception
                                                                 insertErrorMessages("PickToteSetupHub", "fillOrderNums", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(DataReader) Then
                                                                     DataReader.Dispose()
                                                                 End If
                                                             End Try

                                                             Return Data
                                                         End Function)
    End Function

    ''' <summary>
    ''' Gets the next tote id
    ''' </summary>
    ''' <returns>Integer containg the next tote id</returns>
    ''' <remarks></remarks>
    Public Function grabNextToteID() As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Return Induction.ProcessPutAways.GetNextTote(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the next tote id to the new value
    ''' </summary>
    ''' <param name="NewTote">The new tote id</param>
    ''' <returns>boolean telling if operationwas successful</returns>
    ''' <remarks></remarks>
    Public Function updateNextToteID(NewTote As Integer)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.ProcessPutAways.UpdateNextTote(NewTote, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Selects zone data for the zone tab of the pick tote batch manager modal
    ''' </summary>
    ''' <returns>List of object containg the zone, batch type, total order, total location, and if it is the defualt</returns>
    ''' <remarks></remarks>
    Public Function selectPickBatchZones() As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Dim DataReader As SqlDataReader = Nothing
                                                             Dim data As New List(Of Object)

                                                             Try
                                                                 DataReader = RunSPArray("selPickBatchZones", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                                 If DataReader.HasRows Then
                                                                     While DataReader.Read()
                                                                         data.Add(New With {.BatchZone = DataReader("BatchZone"), .BatchType = DataReader("BatchType"),
                                                                                            .TotalOrders = DataReader("TotalOrders"), .TotalLocations = DataReader("TotalLocations"),
                                                                                            .DefZone = DataReader("Default Zone")})
                                                                     End While
                                                                 End If

                                                             Catch ex As Exception
                                                                 insertErrorMessages("PickToteSetupHub", "selectPickBatchZones", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(DataReader) Then
                                                                     DataReader.Dispose()
                                                                 End If
                                                             End Try

                                                             Return data
                                                         End Function)
    End Function

    ''' <summary>
    ''' Selects the datat for the given filter
    ''' </summary>
    ''' <param name="Filter">The filter whose data is requested</param>
    ''' <returns>object containg the data for the filter</returns>
    ''' <remarks></remarks>
    Public Function selectFilterData(Filter As String) As Object
        Dim DataReader As SqlDataReader = Nothing
        Dim FilterData As New List(Of Object)
        Dim OrderData As New List(Of Object)
        Dim index As Integer = 0
        Try
            DataReader = RunSPArray("selPickBatchFilterOrder", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                 {"@Filter", Filter, strVar}})

            Do
                While DataReader.Read()
                    If index = 0 Then
                        FilterData.Add(New With {.OTSeq = DataReader("OTSequence"), .OTField = DataReader("OtField"), .OTCrit = DataReader("OTCriteria"),
                                                 .OTValue = DataReader("OTValue"), .OTAndOr = CheckDBNull(DataReader("OTAndOr"))})
                    Else
                        OrderData.Add(New With {.Seq = DataReader("Batch Sequence"), .Field = DataReader("Batch Field"), .Order = DataReader("Batch Order"), .ID = DataReader("ID")})
                    End If
                End While
                index += 1
            Loop While DataReader.NextResult

        Catch ex As Exception
            insertErrorMessages("PickToteSetupHub", "selectFilterData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return New With {.FilterData = FilterData, .OrderData = OrderData}
    End Function

    ''' <summary>
    ''' INserts a new filter row 
    ''' </summary>
    ''' <param name="Seq">The sequence which the filter will be executed</param>
    ''' <param name="Field">The column which will be checked</param>
    ''' <param name="Crit">The criteria of this column ex: equals</param>
    ''' <param name="Val">The  vlaue which will be checked against</param>
    ''' <param name="AndOr">if the next operation is an and or or after this filter</param>
    ''' <param name="BatchDesc">The filter that this row will be applied to</param>
    ''' <returns>String telling if this operation was successful</returns>
    ''' <remarks></remarks>
    Public Function insertNewFilterRow(Seq As Integer, Field As String, Crit As String, Val As String, AndOr As String, BatchDesc As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("insPickBatchFilter", Context.QueryString.Get("WSID"), {{"@Seq", Seq, intVar},
                                                                                                                            {"@Field", Field, strVar},
                                                                                                                            {"@Criteria", Crit, strVar},
                                                                                                                            {"@Val", Val, strVar},
                                                                                                                            {"@AndOr", AndOr, strVar},
                                                                                                                            {"@BatchDesc", BatchDesc, strVar},
                                                                                                                            {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "insertNewFilterRow", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' INserts a new order by row
    ''' </summary>
    ''' <param name="BatchDesc">The filter that this will be apoplied to</param>
    ''' <param name="Field">The column which will be ordered on</param>
    ''' <param name="Order">The sort order</param>
    ''' <param name="Seq">The sequence in which this orde rby will be ran</param>
    ''' <returns>string telling if operation was succcessful</returns>
    ''' <remarks></remarks>
    Public Function insertNewOrderRow(BatchDesc As String, Field As String, Order As String, Seq As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("insPickBatchOrder", Context.QueryString.Get("WSID"), {{"@BatchDesc", BatchDesc, strVar},
                                                                                                                           {"@Field", Field, strVar},
                                                                                                                           {"@Order", Order, strVar},
                                                                                                                           {"@Seq", Seq, intVar},
                                                                                                                           {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            success = DataReader("ID")
                                                        End If
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "insertNewOrderRow", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates the desired filter row
    ''' </summary>
    ''' <param name="Seq">The sequence that the filter is used in</param>
    ''' <param name="Field">The column which will be acted upon</param>
    ''' <param name="Crit">The criteria ex: equalss</param>
    ''' <param name="Val">The vlaue wihich will be checked</param>
    ''' <param name="AndOr">Telling if next filter added will be anded or ored</param>
    ''' <param name="BatchDesc">THe filter name</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function updatePickBatchFilter(Seq As Integer, Field As String, Crit As String, Val As String, AndOr As String, BatchDesc As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchFilter", Context.QueryString.Get("WSID"), {{"@Seq", Seq, intVar},
                                                                                                                               {"@Field", Field, strVar},
                                                                                                                               {"@Criteria", Crit, strVar},
                                                                                                                               {"@Val", Val, strVar},
                                                                                                                               {"@AndOr", AndOr, strVar},
                                                                                                                               {"@BatchDesc", BatchDesc, strVar},
                                                                                                                               {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "updatePickBatchFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates the desired order by row
    ''' </summary>
    ''' <param name="Field">The column which will be ordered on</param>
    ''' <param name="Order">The sort order</param>
    ''' <param name="Seq">the equence wof when this will be used</param>
    ''' <param name="ID">The id of this row wihtin the table</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function updatePickBatchOrder(Field As String, Order As String, Seq As Integer, ID As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchOrder", Context.QueryString.Get("WSID"), {{"@Field", Field, strVar},
                                                                                                                           {"@Order", Order, strVar},
                                                                                                                           {"@Seq", Seq, intVar},
                                                                                                                           {"@ID", ID, intVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "updatePickBatchOrder", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes the desired filter row from the filter
    ''' </summary>
    ''' <param name="BatchDesc">The filter who contains the row</param>
    ''' <param name="Seq">The sequence that this row is operated in</param>
    ''' <returns>string telling if operationwas completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deletePickBatchFilter(BatchDesc As String, Seq As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delPickBatchFilter", Context.QueryString.Get("WSID"), {{"@BatchDesc", BatchDesc, strVar},
                                                                                                                            {"@Seq", Seq, intVar},
                                                                                                                            {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "deletePickBatchFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes the desired order by row
    ''' </summary>
    ''' <param name="id">The  id in the table to be deleted</param>
    ''' <returns>string telling if operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deletePickBatchOrder(id As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delPickBatchOrder", Context.QueryString.Get("WSID"), {{"@ID", id, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "deletePickBatchOrder", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' renames the filter
    ''' </summary>
    ''' <param name="OldFilter">The old filter name</param>
    ''' <param name="NewFilter">The new filter name</param>
    ''' <returns>string telling if operaation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function renameFilter(OldFilter As String, NewFilter As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchRenameFilter", Context.QueryString.Get("WSID"), {{"@NewBatchDesc", NewFilter, strVar},
                                                                                                                                  {"@OldBatchDesc", OldFilter, strVar},
                                                                                                                                  {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "renameFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' selects the filter marked as default
    ''' </summary>
    ''' <returns>string contiang the default filter vlaue</returns>
    ''' <remarks></remarks>
    Public Function selectDefaultFilter() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim def As String = ""

                                                    Try
                                                        DataReader = RunSPArray("selPickBatchFilterDef", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            def = DataReader(0)
                                                        End If
                                                    Catch ex As Exception
                                                        def = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "renameFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return def
                                                End Function)
    End Function

    ''' <summary>
    ''' Unmarks the filter marked as defualt
    ''' </summary>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function clearDefaultFilter() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchFilterClearDef", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "clearDefaultFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Marks the desired filter as defualt
    ''' </summary>
    ''' <param name="BatchDesc">Filter to be marked</param>
    ''' <returns>string tellling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function markDefaultFilter(BatchDesc As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchFilterDef", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                               {"@BatchDesc", BatchDesc, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "markDefaultFilter", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Deletes desired filter
    ''' </summary>
    ''' <param name="BatchDesc">filter to be deleted</param>
    ''' <returns>sting telling if operationw as successful</returns>
    ''' <remarks></remarks>
    Public Function deletePickBatchFilterBatch(BatchDesc As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delPickBatchFilterBatch", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                                 {"@BatchDesc", BatchDesc, strVar}})

                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "deletePickBatchFilterBatch", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' marks the desired zone and batch type as default
    ''' </summary>
    ''' <param name="Zone">desired zoen to be marked</param>
    ''' <param name="BatchType">desired batch type to be marked</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function markZoneDefault(Zone As String, BatchType As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updPickBatchZonesDef", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                              {"@Zone", Zone, strVar},
                                                                                                                              {"@BatchType", BatchType, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "markZoneDefault", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Populates the roders table with orde rnumbers fromt he filter or zone
    ''' </summary>
    ''' <param name="Filter">desired filter</param>
    ''' <param name="Zone">desired zone</param>
    ''' <param name="BatchType">desired batch type for zone</param>
    ''' <param name="UseDefFilter">use default filter for auto fill</param>
    ''' <param name="UseDefZone">use default zone for auto fill</param>
    ''' <returns>list of list of string containg orders</returns>
    ''' <remarks></remarks>
    Public Function selectOrdersFilterZone(Filter As String, Zone As String, BatchType As String, UseDefFilter As Integer, UseDefZone As Integer, RP As Boolean) As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Dim Orders As New List(Of List(Of String))
                                                                      Dim inners As New List(Of String)
                                                                      Dim DataReader As SqlDataReader = Nothing
                                                                      Dim Parms(,) As String = {{}}

                                                                      If UseDefFilter = 1 Then
                                                                          Parms = {{"@UseDefFilter", UseDefFilter, intVar}, {"@UseDefZone", 0, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                                      ElseIf UseDefZone = 1 Then
                                                                          Parms = {{"@UseDefFilter", 0, intVar}, {"@UseDefZone", 1, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                                      ElseIf Filter <> "" Then
                                                                          Parms = {{"@Filter", Filter, strVar}, {"@UseDefFilter", 0, intVar}, {"@UseDefZone", 0, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                                      ElseIf Zone <> "" Then
                                                                          Parms = {{"@Zone", Zone, strVar}, {"@BatchType", BatchType, strVar}, {"@RP", RP, boolVar}, {"@UseDefFilter", 0, intVar}, {"@UseDefZone", 0, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                                      End If

                                                                      Try
                                                                          DataReader = RunSPArray("selPickBatchManagerOrdersDT", Context.QueryString.Get("WSID"), Parms)

                                                                          If DataReader.HasRows Then
                                                                              While DataReader.Read()
                                                                                  inners = New List(Of String)
                                                                                  inners.Add(If(IsDBNull(DataReader(0)), "", DataReader(0)))
                                                                                  inners.Add(If(IsDBNull(DataReader(1)), "", DataReader(1)))
                                                                                  inners.Add(If(IsDBNull(DataReader(2)), "", DataReader(2)))
                                                                                  Orders.Add(inners)
                                                                              End While
                                                                          End If
                                                                      Catch ex As Exception
                                                                          Orders = New List(Of List(Of String))
                                                                          inners = New List(Of String)
                                                                          inners.Add("Error")
                                                                          Orders.Add(inners)
                                                                          insertErrorMessages("PickToteSetupHub", "selectOrdersFilterZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                      Finally
                                                                          If Not IsNothing(DataReader) Then
                                                                              DataReader.Dispose()
                                                                          End If
                                                                      End Try

                                                                      Return Orders
                                                                  End Function)
    End Function

    ''' <summary>
    ''' process the current tote setup
    ''' </summary>
    ''' <param name="Positions">list of all positions</param>
    ''' <param name="ToteIDs">list of all tote ids</param>
    ''' <param name="OrderNums">list of all order numbers</param>
    ''' <param name="BatchID">batch name</param>
    ''' <param name="Count">if only the count is going to be executed</param>
    ''' <returns>string telling the count for an auto print</returns>
    ''' <remarks></remarks>
    Public Function processPickToteSetup(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String, Count As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Try
                                                        Dim temp As String = String.Join(",", ToteIDs.ToArray)
                                                        Dim temp2 As String = String.Join(",", OrderNums.ToArray)
                                                        Dim temp3 As String = String.Join(",", Positions.ToArray)

                                                        DataReader = RunSPArray("updOTPickToteProcess", Context.QueryString.Get("WSID"), {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                                                                                                              {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                                                                                                              {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                                                                                                              {"@BatchID", BatchID, strVar},
                                                                                                                              {"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                              {"@User", Context.User.Identity.Name, strVar},
                                                                                                                              {"@Count", Count, intVar}})
                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            success = DataReader(0).ToString()
                                                        End If
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "processPickToteSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Validates each tote to see if it already exists 
    ''' </summary>
    ''' <param name="ToteIDs">The totes to check for</param>
    ''' <returns>The tote that was invalid</returns>
    Public Function validateTotes(ToteIDs As List(Of String)) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function()
                                                    Dim InvalidTote As String = ""
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        For Each Tote In ToteIDs
                                                            DataReader = RunSPArray("selOTValidTote", Context.QueryString.Get("WSID"), {{"@ToteID", Tote, strVar}})

                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                InvalidTote = DataReader("Tote ID")
                                                                Exit For
                                                            End If

                                                            DataReader.Close()
                                                        Next
                                                    Catch ex As Exception
                                                        InvalidTote = "Error"
                                                        insertErrorMessages("PickToteSetupHub", "validateTotes", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return InvalidTote
                                                End Function)
    End Function

    ''' <summary>
    ''' Validates if order number has lines in the zone
    ''' </summary>
    ''' <param name="OrderNum">The order number to check for</param>
    ''' <returns>Boolean telling if order has lines</returns>
    Public Function validOrderForZone(OrderNum As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function()
                                                     Dim Datareader As SqlDataReader = Nothing

                                                     Try
                                                         Datareader = RunSPArray("selOTOrderZone", Context.QueryString.Get("WSID"), {{"@OrderNum", OrderNum, strVar},
                                                                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                         If Datareader.HasRows Then
                                                             Return True
                                                         End If

                                                     Catch ex As Exception
                                                         Return False
                                                         insertErrorMessages("PickToteSetupHub", "validOrderForZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Finally
                                                         If Not IsNothing(Datareader) Then
                                                             Datareader.Dispose()
                                                         End If
                                                     End Try

                                                     Return False
                                                 End Function)
    End Function

    ''' <summary>
    ''' Selects orders for the in zone value
    ''' </summary>
    ''' <returns>a list of list of orders</returns>
    Public Function selectOrdersInZone(OrderView As String) As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Dim Orders As New List(Of List(Of String))
                                                                      Dim inners As New List(Of String)
                                                                      Dim DataReader As SqlDataReader = Nothing

                                                                      Try
                                                                          DataReader = RunSPArray("selOTInZoneOrdersDT", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@OrderView", OrderView, strVar}})

                                                                          If DataReader.HasRows Then
                                                                              While DataReader.Read()
                                                                                  inners = New List(Of String)
                                                                                  inners.Add(DataReader("Order Number"))
                                                                                  Orders.Add(inners)
                                                                              End While
                                                                          End If
                                                                      Catch ex As Exception
                                                                          Orders = New List(Of List(Of String))
                                                                          inners = New List(Of String)
                                                                          inners.Add("Error")
                                                                          Orders.Add(inners)
                                                                          insertErrorMessages("PickToteSetupHub", "selectOrdersInZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                      Finally
                                                                          If Not IsNothing(DataReader) Then
                                                                              DataReader.Dispose()
                                                                          End If
                                                                      End Try

                                                                      Return Orders
                                                                  End Function)
    End Function

    ''' <summary>
    ''' Processes the in zone setup
    ''' </summary>
    ''' <param name="Positions">Tote position</param>
    ''' <param name="ToteIDs">Totes to process</param>
    ''' <param name="OrderNums">Orders assigend to totes</param>
    ''' <param name="BatchID">The batch to assign everything to</param>
    ''' <returns>String telling if operation completed successfully</returns>
    Public Function processInZoneSetup(Positions As List(Of String), ToteIDs As List(Of String), OrderNums As List(Of String), BatchID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = "Error"
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Try

                                                        DataReader = RunSPArray("updOTInZoneProcess", Context.QueryString.Get("WSID"), {{"@Positions", String.Join(",", Positions.ToArray), strVar},
                                                                                                                              {"@ToteIDs", String.Join(",", ToteIDs.ToArray), strVar},
                                                                                                                              {"@OrderNums", String.Join(",", OrderNums.ToArray), strVar},
                                                                                                                              {"@BatchID", BatchID, strVar},
                                                                                                                              {"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                              {"@User", Context.User.Identity.Name, strVar}})
                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            success = DataReader(0).ToString()
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("PickToteSetupHub", "processPickToteSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Processes a blossom (move ot records from one to to another)
    ''' </summary>
    ''' <param name="OldTote">The original tote</param>
    ''' <param name="NewTote">The tote that reocrds will now be assigend to</param>
    ''' <returns>String telling if operation completed successfully</returns>
    Public Function processBlossom(OldTote As String, NewTote As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim BatchID As String = "Error"
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("updIMOTBlossom", Context.QueryString.Get("WSID"), {{"@OldToteID", OldTote, strVar}, {"@NewToteID", NewTote, strVar}})

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            BatchID = DataReader("Batch Pick ID")
                                                        End If

                                                    Catch ex As Exception
                                                        insertErrorMessages("PickToteSetupHub", "processBlossom", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return BatchID
                                                End Function)
    End Function


    Public Function selectLocationZones() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Dim DataReader As SqlDataReader = Nothing
                                                             Dim Zones As New List(Of String)

                                                             Try
                                                                 DataReader = RunSPArray("selLocZonesInZoneDrop", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                 If DataReader.HasRows Then
                                                                     While DataReader.Read()
                                                                         Zones.Add(DataReader("Zone"))
                                                                     End While
                                                                 End If

                                                             Catch ex As Exception
                                                                 Debug.Print(ex.Message)
                                                                 insertErrorMessages("PickToteSetupHub", "SelectLocationZones", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(DataReader) Then
                                                                     DataReader.Dispose()
                                                                 End If
                                                             End Try

                                                             Return Zones
                                                         End Function)
    End Function


    Public Function selectWSPickZones() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return Induction.PickToteSetup.SelectWSZones(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    Public Function insertWSPickZone(Zone As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("insIMWorkstationPickZones", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", Zone, strVar}})

                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("PickToteSetupHub", "insertWSPickZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

    Public Function deleteWSPickZone(Zone As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("delIMWorkstationPickZones", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", Zone, strVar}})

                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("PickToteSetupHub", "insertWSPickZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function
End Class
