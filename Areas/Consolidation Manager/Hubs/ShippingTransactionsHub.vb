' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO

Namespace Consolidation
    Public Class ShippingTransactionsHub
        Inherits Hub
        ''' <summary>
        ''' Splits the desired transaction by the desired amount 
        ''' </summary>
        ''' <param name="quant">The quantity to split from it (total-quant= new total for the original)</param>
        ''' <param name="ID">The id of the record to split from</param>
        ''' <param name="Page">the current page that is being worked on</param>
        ''' <returns>list of string containg the new record that was added as a result of the split</returns>
        ''' <remarks></remarks>
        Public Function splitLineTrans(quant As Integer, ID As Integer, Page As String) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim row As New List(Of String)
                                                                 Dim DataReader As SqlDataReader = Nothing

                                                                 Try
                                                                     DataReader = RunSPArray("insShipTransSplitLine", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar},
                                                                                                                                                        {"@ID", ID, intVar},
                                                                                                                                                        {"@Quant", quant, intVar},
                                                                                                                                                        {"@Page", Page, strVar}})
                                                                     If DataReader.HasRows Then
                                                                         While DataReader.Read()
                                                                             For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                 row.Add(CheckDBNull(DataReader(x)))
                                                                             Next
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     insertErrorMessages("ShippingTransactionsHub", "SplitTransaction", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                     row.Add("Error")
                                                                     Return row
                                                                 Finally
                                                                     If Not IsNothing(DataReader) Then
                                                                         DataReader.Dispose()
                                                                     End If
                                                                 End Try
                                                                 Return row
                                                             End Function)
        End Function
        ''' <summary>
        ''' Updates the container id field for the desired record
        ''' </summary>
        ''' <param name="toteID">The tote id which is being updated</param>
        ''' <param name="orderNum">The over all order number</param>
        ''' <param name="contID">The new container id that is assigned ot the tote id for the order number</param>
        ''' <returns>string depicting of the function executed successfully</returns>
        ''' <remarks></remarks>
        Public Function updContIDShipTrans(toteID As String, orderNum As String, contID As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try
                                                            RunActionSP("updShipTransContID", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}, {"@toteID", toteID, strVar}, {"@contID", contID, strVar}})

                                                        Catch ex As Exception
                                                            insertErrorMessages("ShippingTransactionsHub", "updContIDShipTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success
                                                    End Function)


        End Function
        ''' <summary>
        ''' Updates the ship quantity field for the desired record
        ''' </summary>
        ''' <param name="STID">The id for the record ot be updated</param>
        ''' <param name="shipQTY">The new quantity</param>
        ''' <param name="reason">The explanation for the quantity change</param>
        ''' <returns>String telling if the function executed successfully</returns>
        ''' <remarks></remarks>
        Public Function updShipQTYShipTrans(STID As String, shipQTY As String, reason As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try
                                                            RunActionSP("updShipTransShipQTY", Context.QueryString.Get("WSID"), {{"@STID", STID, strVar}, {"shipQTY", shipQTY, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@Reason", reason, strVar}})
                                                        Catch ex As Exception
                                                            insertErrorMessages("ShippingTransactionsHub", "updShipQTYShipTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success
                                                    End Function)
        End Function
        ''' <summary>
        ''' Will complete the packing for the given order number
        ''' </summary>
        ''' <param name="orderNum">The order number that is displayed</param>
        ''' <returns>string telling if the function executed successfully</returns>
        ''' <remarks></remarks>
        Public Function updCompletePacking(orderNum As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try

                                                            'RunActionSP("updCompleteShipTrans", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}})
                                                            'RunActionSP("delStagingLocOrderNum", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}})
                                                            'RunActionSP("CompPackReelTrack", Context.QueryString.Get("WSID"), {{"OrderNum", orderNum, strVar}})
                                                            'RunActionSP("insOTShipComp", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                            '                                                               {"@UserName", Context.User.Identity.Name, strVar},
                                                            '                                                               {"@OrderNum", orderNum, strVar},
                                                            '                                                               {"@FromShipping", 0, intVar}})

                                                            Dim SPs As New List(Of SQLCommandInfo)
                                                            'SPs.Add(New SQLCommandInfo With {.SP = "updShippingComplete", .Params = {{"@orderNum", orderNum, strVar}}})

                                                            SPs.Add(New SQLCommandInfo With {.SP = "updShipTransComplete", .Params = {{"@UserName", Context.User.Identity.Name, strVar}, {"@OrderNum", orderNum, strVar}}})
                                                            SPs.Add(New SQLCommandInfo With {.SP = "delStagingLocOrderNum", .Params = {{"@orderNum", orderNum, strVar}}})
                                                            SPs.Add(New SQLCommandInfo With {.SP = "CompPackReelTrack", .Params = {{"OrderNum", orderNum, strVar}}})
                                                            SPs.Add(New SQLCommandInfo With {.SP = "insOTShipComp", .Params = {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                           {"@UserName", Context.User.Identity.Name, strVar},
                                                                                                                           {"@OrderNum", orderNum, strVar},
                                                                                                                           {"@FromShipping", 0, intVar}}})

                                                            RunActionSPMulti(SPs, Context.QueryString.Get("WSID"))

                                                        Catch ex As Exception
                                                            insertErrorMessages("ShippingTransactionsHub", "updCompletePacking", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            Return "Fail"
                                                        End Try
                                                        Return success
                                                    End Function)
        End Function
        ''' <summary>
        ''' Selects the number of records within OTTep table for the given record
        ''' </summary>
        ''' <param name="orderNum">The order number that is displayed</param>
        ''' <returns>the number of records for the given order number</returns>
        ''' <remarks></remarks>
        Public Function selCountOpenTransactionsTemp(orderNum As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim dataReader As SqlDataReader = Nothing
                                                        Dim count As Integer
                                                        Try
                                                            dataReader = RunSPArray("selCountOfOpenTransactionsTemp", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}})
                                                            If dataReader.HasRows Then
                                                                dataReader.Read()
                                                                count = dataReader(0)
                                                            End If
                                                        Catch ex As Exception
                                                            insertErrorMessages("ShippingTransactionsHub", "selCountOpenTransactionsTemp", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            count = -1
                                                        Finally
                                                            If Not IsNothing(dataReader) Then
                                                                dataReader.Close()
                                                            End If
                                                        End Try
                                                        Return count
                                                    End Function)
        End Function

        Public Function updContainerIdSingleShipTrans(STID As Integer, ContainerID As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try
                                                            RunActionSP("updShipTransSTIDContID", Context.QueryString.Get("WSID"), {{"@STID", STID, intVar}, {"@ContID", ContainerID, strVar}})

                                                        Catch ex As Exception
                                                            insertErrorMessages("ShippingTransactionsHub", "updContainerIdShipTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success
                                                    End Function)
        End Function
    End Class
End Namespace

