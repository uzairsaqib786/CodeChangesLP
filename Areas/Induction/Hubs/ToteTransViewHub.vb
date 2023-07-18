' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class ToteTransViewHub
    Inherits Hub
    ''' <summary>
    ''' Deletes the desired item from the tote 
    ''' </summary>
    ''' <param name="ID">The item's id within the table</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function clearItemFromTote(ID As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delOTToteTransViewClearItem", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar},
                                                                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ToteTransViewHub", "clearItemFromTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' DeAllocates the item from the tote
    ''' </summary>
    ''' <param name="ID">The item's id within the table</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function deAlloItemFromTote(ID As Integer) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delOTToteTransViewDeAlloItem", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar},
                                                                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ToteTransViewHub", "deAlloItemFromTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Marks the tote as full
    ''' </summary>
    ''' <param name="ToteNum">The tote number of the current tote</param>
    ''' <param name="Cell">How many items are within it</param>
    ''' <param name="BatchID">The abthc that the tote is contained in</param>
    ''' <returns>string telling id operaiton was successful</returns>
    ''' <remarks></remarks>
    Public Function markToteAsFull(ToteNum As Integer, Cell As Integer, BatchID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("updToteSetupQueueMarkFull", Context.QueryString.Get("WSID"), {{"@ToteNum", ToteNum, intVar}, _
                                                                                                                                   {"@Cell", Cell, intVar}, _
                                                                                                                                   {"@BatchID", BatchID, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ToteTransViewHub", "deAlloItemFromTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

End Class
