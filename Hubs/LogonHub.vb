' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports ActiveDirectorySecurityDriver

Public Class LogonHub
    Inherits Hub

    ''' <summary>
    ''' Handles changing a user's password.
    ''' </summary>
    ''' <param name="username">The user requesting a password change.</param>
    ''' <param name="password">User's password to change.</param>
    ''' <param name="newPassword">User's new requested password.</param>
    ''' <returns>Returns a success or error message to the user.</returns>
    ''' <remarks></remarks>
    Public Function changePassword(username As String, password As String, newPassword As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function()
                                                    Dim DataReader As SqlDataReader = Nothing, returnString As String = ""

                                                    If SecurityConfig.getSecurityEnvironment.Contains("AD") Then
                                                        Try
                                                            Dim message As String = AuthenticationDriver.ChangePassword(username, password, newPassword)
                                                            returnString = message
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("Logon", "changePassword", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            returnString = ex.Message
                                                        End Try
                                                    Else
                                                        Try
                                                            DataReader = RunSPArray("PasswordChange", Context.QueryString.Get("WSID"), {{"@Username", username, strVar},
                                                                                            {"@OldPass", password, strVar},
                                                                                            {"@NewPass", newPassword, strVar},
                                                                                            {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                            If DataReader.HasRows Then
                                                                If DataReader.Read() Then
                                                                    returnString = DataReader(0)
                                                                End If
                                                            End If
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("Logon", "changePassword", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            returnString = "Password Change Failed"
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                    End If

                                                    Return returnString
                                                End Function)

    End Function


    ''' <summary>
    ''' Sets the workstation's SQL connection string
    ''' </summary>
    ''' <param name="connectionName"></param>
    ''' <returns>None task of sub</returns>
    ''' <remarks></remarks>
    Public Function setConnectionString(connectionName As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim wsid = Context.QueryString.Get("WSID")
                                         Config.addupdateWSConnectionString(wsid, connectionName)
                                         If userCS.ContainsKey(wsid) Then
                                             Dim connVal As String = ""
                                             userCS.TryRemove(wsid, connVal)
                                         End If
                                     End Sub)
    End Function

End Class
