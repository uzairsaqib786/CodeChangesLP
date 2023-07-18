' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Public Class OMPreferencesHub
    Inherits Hub
    ''' <summary>
    ''' Updates the order manager preferences
    ''' </summary>
    ''' <param name="MaxOrder">max orders to retrieve field</param>
    ''' <param name="AllowInProc">allow release for in process orders checkbox</param>
    ''' <param name="AllowPartRel">allow release of individual order lines checkbox</param>
    ''' <param name="DefUserFields">use default user fields checkbox</param>
    ''' <param name="CustomReport">location of the custom reports application</param>
    ''' <param name="CustomAdmin">location of the custom application to open fom the admin menu</param>
    ''' <param name="CustomAdminText">test for the custom application button on the admin menu</param>
    ''' <returns>Boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateOMPreferences(MaxOrder As Integer, AllowInProc As Boolean, AllowPartRel As Boolean, DefUserFields As Boolean, _
                                        CustomReport As String, CustomAdmin As String, CustomAdminText As String, PrintDirect As Boolean) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         RunActionSP("updOMPrefs", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                    {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                                    {"@MaxOrder", MaxOrder, intVar}, _
                                                                                                                    {"@AllowInProc", AllowInProc, boolVar}, _
                                                                                                                    {"@AllowPartRel", AllowPartRel, boolVar}, _
                                                                                                                    {"@DefUserFields", DefUserFields, boolVar}, _
                                                                                                                    {"@CustomReport", CustomReport, strVar}, _
                                                                                                                    {"@CustomAdmin", CustomAdmin, strVar}, _
                                                                                                                    {"@CustomAdminText", CustomAdminText, strVar}, _
                                                                                                                     {"@PrintDirect", CastAsSqlBool(PrintDirect), intVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("OMPreferencesHub", "selectOMCountData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         success = False
                                                     End Try
                                                     Return success
                                                 End Function)
    End Function
End Class
