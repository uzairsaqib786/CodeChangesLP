' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Imports PickPro_Web.Consolidation

Public Class ConfirmAndPackingHub
    Inherits Hub
    ''' <summary>
    ''' Unpacks the desired record within shipping transactions
    ''' </summary>
    ''' <param name="ST_ID">The id to be updated</param>
    ''' <returns>a string teling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function updShipTransUnPack(ST_ID As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success = ""
                                                    Try
                                                        RunActionSP("updShipTransUnPack", Context.QueryString.Get("WSID"), {{"@ST_ID", ST_ID, intVar}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("ConfirmAndPackingHub", "updShipTransUnPack", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        success = "Fail"
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Runs the function that will select the next container id
    ''' </summary>
    ''' <param name="OrderNum">The order number whose container id will be set</param>
    ''' <returns>a string which is set to the next container id</returns>
    ''' <remarks></remarks>
    Public Function selContIDConfirmPack(OrderNum As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Return ConfirmAndPacking.selContIDConfirmPack(OrderNum, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                End Function)
    End Function
    ''' <summary>
    ''' Selects the information for the datatble within the select modal
    ''' </summary>
    ''' <param name="OrderNum">The order number that is displayed</param>
    ''' <param name="ItemNum">The item number with multiple records</param>
    ''' <returns>list of list of string containging the table data</returns>
    ''' <remarks></remarks>
    Public Function selConfPackSelectDT(OrderNum As String, ItemNum As String) As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Dim data As New List(Of List(Of String))
                                                                      Dim row As New List(Of String)
                                                                      Dim DataReader As SqlDataReader = Nothing

                                                                      Try
                                                                          DataReader = RunSPArray("selConfPackSelectModalDT", Context.QueryString.Get("WSID"), {{"@OrderNumber", OrderNum, strVar}, _
                                                                                                                                                                {"@ItemNumber", ItemNum, strVar}})

                                                                          If DataReader.HasRows Then
                                                                              While DataReader.Read()
                                                                                  row = New List(Of String)
                                                                                  For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                      row.Add(CheckDBNull(DataReader(x)))
                                                                                  Next
                                                                                  data.Add(row)
                                                                              End While
                                                                          End If
                                                                      Catch ex As Exception
                                                                          insertErrorMessages("ConfirmAndPackingHub", "selConfPackSelectDT", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                      Finally
                                                                          If Not IsNothing(DataReader) Then
                                                                              DataReader.Close()
                                                                          End If
                                                                      End Try
                                                                      Return data
                                                                  End Function)
    End Function
    ''' <summary>
    ''' Will either update the desired record in shipping transactions or set the string value to show the modal needs to be shown
    ''' </summary>
    ''' <param name="ID">Theid for the record that is being updated</param>
    ''' <param name="OrderNum">The order number that is being displayed</param>
    ''' <param name="ContID">The container id that is being updated</param>
    ''' <param name="Modal">If this came form the modal</param>
    ''' <returns>a string either telling to pop the modal or tell that the function ran successfully</returns>
    ''' <remarks></remarks>
    Public Function updateConfPackProcModal(ID As Integer, OrderNum As String, ContID As String, Modal As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim result As String = ""
                                                    Dim params As String(,) = {{"@ID", ID, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                               {"@Username", Context.User.Identity.Name, strVar}, {"@OrderNum", OrderNum, strVar}, _
                                                                               {"@ContID", ContID, strVar}}
                                                    Try
                                                        If Modal <> "" Then
                                                            params = {{"@ID", ID, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                   {"@Username", Context.User.Identity.Name, strVar}, {"@OrderNum", OrderNum, strVar}, _
                                                                                   {"@ContID", ContID, strVar}, {"@Modal", Modal, strVar}}
                                                        End If

                                                        DataReader = RunSPArray("updShipTransConfPackProcModal", Context.QueryString.Get("WSID"), params)

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            result = DataReader(0)
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("ConfirmAndPackingHub", "updateConfPackProcModal", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        result = "Fail"
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try
                                                    Return result
                                                End Function)
    End Function
    ''' <summary>
    ''' Selects the desired data for the process modal
    ''' </summary>
    ''' <param name="ID">The id whose data is displayed</param>
    ''' <returns>a list of list of string containg the data for the desired id</returns>
    ''' <remarks></remarks>
    Public Function selConfPackProcModal(ID As Integer) As Task(Of List(Of List(Of String)))
        Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))
                                                                      Dim DataReader As SqlDataReader = Nothing
                                                                      Dim data As New List(Of List(Of String))
                                                                      Dim row As New List(Of String)

                                                                      Try
                                                                          DataReader = RunSPArray("selShipTransConfPackProcModal", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar}})

                                                                          If DataReader.HasRows Then
                                                                              While DataReader.Read()
                                                                                  row = New List(Of String)
                                                                                  For x As Integer = 0 To DataReader.FieldCount - 1
                                                                                      row.Add(CheckDBNull(DataReader(x)))
                                                                                  Next
                                                                                  data.Add(row)
                                                                              End While
                                                                          End If
                                                                      Catch ex As Exception
                                                                          insertErrorMessages("ConfirmAndPackingHub", "selConfPackProcModal", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                      Finally
                                                                          If Not IsNothing(DataReader) Then
                                                                              DataReader.Close()
                                                                          End If
                                                                      End Try

                                                                      Return data
                                                                  End Function)
    End Function
    ''' <summary>
    ''' Updates the desired records in the shipping transactions table sets them to confirmed
    ''' </summary>
    ''' <param name="OrderNum">The displayed order number</param>
    ''' <param name="ContID">The container id to mark as confirmed</param>
    ''' <returns>a string telling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function confirmAllConfPack(OrderNum As String, ContID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing
                                                    Dim result As String = ""

                                                    Try
                                                        DataReader = RunSPArray("updShipTransConfPackAll", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                                             {"@Username", Context.User.Identity.Name, strVar},
                                                                                                                                             {"@OrderNum", OrderNum, strVar},
                                                                                                                                             {"@ContID", ContID, strVar}})

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            result = DataReader(0)
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("ConfirmAndPackingHub", "confirmAllConfPack", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        result = "Fail"
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Close()
                                                        End If
                                                    End Try

                                                    Return result
                                                End Function)
    End Function

    Public Function selectConfPackContIDDrop(OrderNum As String) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return ConfirmAndPacking.selConfPackContIDDrop(OrderNum, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                         End Function)
    End Function
End Class
