' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Namespace Admin
    Public Class MenuHub
        Inherits Hub

        ''' <summary>
        ''' Updates the menu table and the counts of transactions associated with the table
        ''' </summary>
        ''' <returns>An object containing the data for the menu table and count info</returns>
        ''' <remarks></remarks>
        Public Function updateInfo() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Return Menu.getMenuData(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function

        ''' <summary>
        ''' Sets Diagnostic Mode for the current user.
        ''' </summary>
        ''' <param name="diagMode">Provides the value diagnostic mode should be set to.</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function toggleDiagMode(diagMode As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("insUsysLogFile", Context.QueryString.Get("WSID"), {{"@Log", CastAsSqlBool(diagMode), intVar}, _
                                                                  {"@User", Context.User.Identity.Name, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("MenuHub", "toggleDiagMode", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
    End Class
End Namespace
