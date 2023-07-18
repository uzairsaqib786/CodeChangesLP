' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class IMMenuHub
    Inherits Hub

    Public Function ValidateSerialNumber(SerialNumber As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("selOTValidSerialNumber", Context.QueryString.Get("WSID"), {{"@SerialNumber", SerialNumber, strVar}})

                                                        If DataReader IsNot Nothing AndAlso DataReader.HasRows Then
                                                            Return "Valid"
                                                        End If

                                                    Catch ex As Exception
                                                        Debug.WriteLine(ex.Message)
                                                        insertErrorMessages("IMMenuHub", "ValidateSerialNumber", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Error"
                                                    End Try

                                                    Return "Invalid"
                                                End Function)
    End Function

    Public Function DeleteSerialNumberOpenTrans(SerialNumbers As List(Of String)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         Dim Execs As New List(Of SQLCommandInfo)

                                                         For Each Num In SerialNumbers
                                                             Dim DelSerial As New SQLCommandInfo With {.SP = "delOTSerialNumber", .Params = {{"@SerialNumber", Num, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}}
                                                             Execs.Add(DelSerial)
                                                         Next

                                                         RunActionSPMulti(Execs, Context.QueryString.Get("WSID"))

                                                     Catch ex As Exception
                                                         Debug.WriteLine(ex.Message)
                                                         insertErrorMessages("IMMenuHub", "DeleteSerialNumberOpenTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

End Class
