' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Controllers
    Public Class GlobalConfigController
        Inherits PickProController

        ' GET: GlobalConfig
        ''' <summary>
        ''' Gets the globalconfig page before logon
        ''' </summary>
        ''' <returns>Empty view for the html</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            ViewData("CONFIG") = "CONFIG"
            If Not Request.ClientCertificate.IsPresent And Not GlobalFunctions.Testing Then
                'Return RedirectToAction("Index", "Certificates")
            End If

            'Session("WSID") = IIf(Not GlobalFunctions.Testing, Certificates.resolveWSID(Request.ClientCertificate), "TESTWSID")

            If Not Config.getConfigDBString() = "" Then
                TempData("Conn") = True
            Else
                TempData("Conn") = False
            End If
            Return View()
        End Function

        ''' <summary>
        ''' Gets the global config page after the logon form has been submitted
        ''' </summary>
        ''' <param name="Username">The username that was entered</param>
        ''' <param name="Password">The password that was entered</param>
        ''' <returns>An empty view. user will get redirected if successful</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        Function Index(Username As String, Password As String) As ActionResult
            Dim DataReader As SqlDataReader = Nothing
            Try
                DataReader = RunSPArray("selValidLogon", "CONFIG", {{"@User", Username, strVar}, {"@Password", Password, strVar}})
                If DataReader.HasRows Then
                    TempData("Success") = "Yes"
                    Session("globalConfigTimeout") = Now().AddMinutes(5)
                    Return RedirectToAction("Menu")
                End If
            Catch ex As Exception
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            ViewData("CONFIG") = "CONFIG"
            ViewData("Error") = "Invalid Username or Password"
            Return View()
        End Function

        ''' <summary>
        ''' Redirects to here after successful logon with proper permissions and good client certificate
        ''' </summary>
        ''' <returns>A redirect to the global config home page</returns>
        ''' <remarks></remarks>
        Function Menu() As ActionResult
            ViewData("CONFIG") = "CONFIG"
            If TempData.ContainsKey("Success") AndAlso Date.Compare(Session("globalConfigTimeout"), Now()) > 0 Then
                Return View(New ConfigModel(Config.getAllConnectionStrings, Config.getLogonInfo(), GlobalFunctions.getAllPrinters(Session("WSID"), User.Identity.Name), Config.getAllWorkstations(), Config.getLAConnStr()))
            Else
                Return RedirectToAction("Index")
            End If
        End Function

        ''' <summary>
        ''' Sets the PickPro_Config database connection string to the specified server and database
        ''' </summary>
        ''' <param name="ServerName">The server that is being connected to</param>
        ''' <param name="DBName">The database witin the server that is being connected to</param>
        ''' <returns>A redirect to the global config home page</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function setConfigDb(ServerName As String, DBName As String) As ActionResult
            Config.setConfigDBString(ServerName, DBName)
            GlobalFunctions.InitializeSharedPages()
            Config.AppLicenses = AESCrypter.GetAppPermissions()
            Return RedirectToAction("Index")
        End Function

        ''' <summary>
        ''' Changes the global account to the specified credentials
        ''' </summary>
        ''' <param name="username">The new username</param>
        ''' <param name="password">The new password</param>
        ''' <returns>A redirect to the global ocnfig home page</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function ChangeGlobalAccount(username As String, password As String) As ActionResult
            Try
                RunSPArray("updUser", "CONFIG", {{"@Username", username, strVar}, {"@Password", password, strVar}})
            Catch ex As Exception
                Debug.WriteLine("Error Occured when updating User Account")
                insertErrorMessages("GlobalConfig", "Update Config User", ex.Message, "GlobalConfig", Session("WSID"))
            End Try
            Return RedirectToAction("Index")
        End Function
    End Class
End Namespace