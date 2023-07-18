' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class ToteManagerHub
    Inherits Hub
    ''' <summary>
    ''' Gets the tote ids and their info
    ''' </summary>
    ''' <returns>list of object containf the tote ids and their info</returns>
    ''' <remarks></remarks>
    Public Function getTotes() As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return Induction.ToteManager.selectTotes(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Deletes the given tote id
    ''' </summary>
    ''' <param name="toteID">The tote id to be deleted</param>
    ''' <returns>boolean telling if operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteTote(toteID As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("delTote", Context.QueryString.Get("WSID"), {{"@ToteID", toteID, strVar},
                                                                                                                  {"@User", Context.User.Identity.Name, strVar},
                                                                                                                  {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                         Return True
                                                     Catch ex As Exception
                                                         insertErrorMessages("ToteManagerHub", "deleteTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try
                                                     Return False
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the infor for the desired tote id
    ''' </summary>
    ''' <param name="NewToteID">The new tote id name</param>
    ''' <param name="Cell">The new cell value</param>
    ''' <param name="OldToteID">The old tote id name</param>
    ''' <returns>string telling if operation was successful</returns>
    ''' <remarks></remarks>
    Public Function updateTotes(NewToteID As String, Cell As Integer, OldToteID As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updTotes", Context.QueryString.Get("WSID"), {{"@NewToteID", NewToteID, strVar},
                                                                                                                  {"@Cell", Cell, intVar},
                                                                                                                  {"@OldToteID", OldToteID, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("ToteManagerHub", "updateTotes", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' Adds a new tote id
    ''' </summary>
    ''' <param name="toteID">The  new tote id</param>
    ''' <param name="cells">the cell vlaue for the new tote id</param>
    ''' <returns>boolean telling if operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function addTote(toteID As String, cells As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("InsTote", Context.QueryString.Get("WSID"), {{"@ToteID", toteID, strVar}, {"@Cells", cells, intVar}})
                                                         Return True
                                                     Catch ex As Exception
                                                         insertErrorMessages("ToteManagerHub", "addTote", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try
                                                     Return False
                                                 End Function)
    End Function

    ''' <summary>
    ''' Selects the print direct preference
    ''' </summary>
    ''' <returns>boolean telling if the print direct preference is ture or false</returns>
    ''' <remarks></remarks>
    Public Function selectPrintDirectPref() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return Induction.Preferences.selectIMPreferences(Context.QueryString.Get("WSID"), Context.User.Identity.Name).PrintDirect
                                                 End Function)
    End Function
End Class
