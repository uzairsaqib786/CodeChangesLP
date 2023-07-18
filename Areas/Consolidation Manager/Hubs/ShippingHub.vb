' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Public Class ShippingHub
    Inherits Hub
    ''' <summary>
    ''' Deletes the desired record from the Shipping table
    ''' </summary>
    ''' <param name="id">The record to be deleted</param>
    ''' <param name="orderNum">The order number that is dispalyed</param>
    ''' <param name="contId">The container id that the record was in</param>
    ''' <param name="carrier">The records carrier</param>
    ''' <param name="trackingNum">The tracking number for the record</param>
    ''' <returns>string telling of the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteShipmentItem(id As Integer, orderNum As String, contId As String, carrier As String, _
                                       trackingNum As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("delShippingItem", Context.QueryString.Get("WSID"), {{"@id", id, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                         {"@User", Context.User.Identity.Name, strVar}, {"@OrderNum", orderNum, strVar}, _
                                                                                                                         {"@contID", contId, strVar}, {"@carrier", carrier, strVar}, {"@trackingNum", trackingNum, strVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("ShippingHub", "deleteShipmentItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Fail"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Updates the desired recod within the Shipping table
    ''' </summary>
    ''' <param name="id">The id of the recprd to be updated</param>
    ''' <param name="carrier">The new carrier value</param>
    ''' <param name="trackingNum">The new tracking number</param>
    ''' <param name="Freight">The new freight value</param>
    ''' <param name="Freight1">The new freight 1 value</param>
    ''' <param name="Freight2">The new freight 2 vlaue</param>
    ''' <param name="Weight">The new wieght of the container</param>
    ''' <param name="Length">The nwe length of the container</param>
    ''' <param name="Width">The new width of the container</param>
    ''' <param name="Height">The new height of the container</param>
    ''' <param name="Cube">The new cube of the container</param>
    ''' <returns>string depicting if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateShipmentItem(id As Integer, carrier As String, trackingNum As String, Freight As String,
                                       Freight1 As String, Freight2 As String, Weight As String, Length As String,
                                       Width As String, Height As String, Cube As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updShippingItem", Context.QueryString.Get("WSID"), {{"@ID", id, strVar}, {"@carrier", carrier, strVar}, {"@trackNum", trackingNum, strVar}, {"@freight", Freight, strVar},
                                                                {"@freight1", Freight1, strVar}, {"@freight2", Freight2, strVar}, {"@weight", Weight, decVar}, {"@length", Length, decVar}, {"@width", Width, decVar},
                                                                {"@height", Height, decVar}, {"@cube", Cube, decVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("ShippingHub", "updateShipmentItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Fail"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Adds a new record to the Shipping table
    ''' </summary>
    ''' <param name="OrderNumber">The order number for the new record</param>
    ''' <param name="ContID">The container id for the new record</param>
    ''' <returns>string telling if the function executed</returns>
    ''' <remarks></remarks>
    Public Function addShippingItem(OrderNumber As String, ContID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("insShippingItem", Context.QueryString.Get("WSID"), {{"@OrderNumber", OrderNumber, strVar}, _
                                                                                                                         {"@ContID", ContID, strVar}, _
                                                                                                                         {"@User", Context.User.Identity.Name, strVar}})

                                                    Catch ex As Exception
                                                        insertErrorMessages("ShippingHub", "addShippingItem", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Fail"
                                                    End Try

                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Runs SPs to mark the desired order number as shipping complete
    ''' </summary>
    ''' <param name="orderNum">The order number to complete shipping for</param>
    ''' <returns>string telling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function completeShipment(orderNum As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        'RunActionSP("updShippingComplete", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}})
                                                        'RunActionSP("delStagingLocOrderNum", Context.QueryString.Get("WSID"), {{"@orderNum", orderNum, strVar}})
                                                        'RunActionSP("CompPackReelTrack", Context.QueryString.Get("WSID"), {{"OrderNum", orderNum, strVar}})
                                                        'RunActionSP("insOTShipComp", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                        '                                                               {"@UserName", Context.User.Identity.Name, strVar},
                                                        '                                                               {"@OrderNum", orderNum, strVar},
                                                        '                                                               {"@FromShipping", 1, intVar}})



                                                        Dim SPs As New List(Of SQLCommandInfo)
                                                        SPs.Add(New SQLCommandInfo With {.SP = "updShippingComplete", .Params = {{"@orderNum", orderNum, strVar}}})
                                                        SPs.Add(New SQLCommandInfo With {.SP = "delStagingLocOrderNum", .Params = {{"@orderNum", orderNum, strVar}}})
                                                        SPs.Add(New SQLCommandInfo With {.SP = "CompPackReelTrack", .Params = {{"OrderNum", orderNum, strVar}}})
                                                        SPs.Add(New SQLCommandInfo With {.SP = "insOTShipComp", .Params = {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                           {"@UserName", Context.User.Identity.Name, strVar},
                                                                                                                           {"@OrderNum", orderNum, strVar},
                                                                                                                           {"@FromShipping", 1, intVar}}})

                                                        RunActionSPMulti(SPs, Context.QueryString.Get("WSID"))

                                                    Catch ex As Exception
                                                        insertErrorMessages("ShippingHub", "completeShipment", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Fail"
                                                    End Try

                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Selects the number of records of the given order number within the Open Transations Temp table
    ''' </summary>
    ''' <param name="orderNum">The displayed order number</param>
    ''' <returns>The number of records for the given order number</returns>
    ''' <remarks></remarks>
    Public Function selOTTempCount(orderNum As String) As Task(Of String)
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
                                                        insertErrorMessages("ShippingTransactionsHub", "selCountOpenTransactionsTemp", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        count = -1
                                                    Finally
                                                        If Not IsNothing(dataReader) Then
                                                            dataReader.Dispose()
                                                        End If
                                                    End Try
                                                    Return count
                                                End Function)
    End Function
End Class
