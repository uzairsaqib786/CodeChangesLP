' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class ToteTransactionsManagerHub
    Inherits Hub
    ''' <summary>
    ''' Clears the info of pick totes
    ''' </summary>
    ''' <returns>String telling if operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function clearPickToteInfo() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updOTClearPickTotes", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ToteTransactionsManagerHub", "clearPickToteInfo", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try

                                                    Return success
                                                End Function)
    End Function

End Class
