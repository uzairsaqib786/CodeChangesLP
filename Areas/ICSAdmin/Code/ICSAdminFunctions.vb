' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin
    Public Class ICSAdminFunctions
        ''' <summary>
        ''' Initializes Session for Admin Menu of Pickpro
        ''' </summary>
        ''' <param name="user">The user that is currently logged in</param>
        ''' <param name="permission">The permission for the page</param>
        ''' <param name="sessionRef">The current session reference</param>
        ''' <param name="tempRef">Temporary data for the reference</param>
        ''' <param name="clientCert">The client certificate</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>Nothing or a list of string that contains all the pages the user is able to access</returns>
        ''' <remarks></remarks>
        Public Shared Function InitializeSession(user As String, permission As String, sessionRef As HttpSessionStateBase, _
                                                  ByRef tempRef As System.Web.Mvc.TempDataDictionary, clientCert As HttpClientCertificate, WSID As String) As List(Of String)
            'Initializes Session Data
            getMenuData(user, sessionRef, WSID)
            If (sessionRef("Admin") = "") Then

                sessionRef("Admin") = If(getAccessLevel(user, WSID) = "Administrator", "True", "False")
                sessionRef("ColumnAlias") = New AliasModel(user, WSID)
                getIdleTimes(sessionRef("WSID"), user, sessionRef)
            End If

            'Checks for Page Permission
            If permission <> "" Then
                If Not getLoadPermission(permission, sessionRef) Then
                    tempRef("PermissionError") = permission
                    Return New List(Of String) From {"Index", "Menu"}
                End If
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Get's all Required Data for a user to be displayed in the menu bar that is present on all pages
        ''' </summary>
        ''' <param name="user">User to get Data for</param>
        ''' <param name="sessionRef">Reference to a user's session to store the data</param>
        ''' <remarks></remarks>
        Private Shared Sub getMenuData(user As String, sessionRef As HttpSessionStateBase, WSID As String)
            sessionRef("Work") = Menu.inWorkMode(user, WSID)
        End Sub


        ''' <summary>
        ''' Selects the idle timeout settings for a workstation.
        ''' </summary>
        ''' <param name="WSID"></param>
        ''' <param name="User"></param>
        ''' <param name="sessionRef"></param>
        ''' <remarks>When idlepoptime expires without user input a modal will pop up warning the user of the timeout.  
        ''' If the other timeout expires after without more user input the user will be logged out.</remarks>
        Public Shared Sub getIdleTimes(WSID As String, User As String, sessionRef As HttpSessionStateBase)
            Dim DataReader As SqlDataReader = Nothing
            Dim IdlePopTime As Integer = 0
            Dim IdleShutTime As Integer = 0
            Dim index As Integer = 0

            Try
                DataReader = RunSPArray("selIdleTimes", WSID, {{"@WSID", WSID, strVar}})
                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            Dim temp = DataReader(0)
                            If temp <> "" Then
                                IdlePopTime = CInt(DataReader(0))
                            End If
                        ElseIf index = 1 Then
                            Dim temp = DataReader(0)
                            If temp <> "" Then
                                IdleShutTime = CInt(DataReader(0))
                            End If
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("GlobalFunctions", "getIdleTimes", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            sessionRef("IdlePopTime") = IdlePopTime
            sessionRef("IdleShutTime") = IdleShutTime
        End Sub

        ''' <summary>
        ''' Gets a User's Access level(Administrator, User, etc)
        ''' </summary>
        ''' <param name="username">Username to get Acces Level of</param>
        ''' <returns>String that contains the User's access level</returns>
        ''' <remarks></remarks>
        Public Shared Function getAccessLevel(username As String, WSID As String) As String
            Dim level As String = "", DataReader As SqlDataReader = Nothing
            Try
                DataReader = RunSPArray("selEmployeesAccessLevel", WSID, {{"@UserName", username, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        level = DataReader(0)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return level
        End Function

        ''' <summary>
        ''' Gets the permissions of the user signed in. 
        ''' </summary>
        ''' <param name="view">The current page</param>
        ''' <param name="sessionRef">The current session for this user</param>
        ''' <returns>The permissions for this user</returns>
        ''' <remarks></remarks>
        Public Shared Function getLoadPermission(view As String, sessionRef As HttpSessionStateBase) As Boolean
            Return sessionRef("Permissions").contains(view)
        End Function
    End Class
End Namespace



