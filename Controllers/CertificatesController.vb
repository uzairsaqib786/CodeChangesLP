' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Controllers
    Public Class CertificatesController
        Inherits Controller

        ' GET: Certificates
        Function Index(Optional makeCookie As String = "") As ActionResult
            Dim isAndroid As Boolean = GlobalFunctions.isAndroid(Request)
            If makeCookie <> "" Then
                Dim cookie = New HttpCookie("androidWSID", makeCookie)
                cookie.Expires = Now.AddYears(10)
                Me.ControllerContext.HttpContext.Response.Cookies.Add(cookie)
                Return RedirectToAction("Index", "Logon")
            End If

            If (Not Request.ClientCertificate.IsPresent) And Not (isAndroid And Request.Cookies.AllKeys.Contains("androidWSID")) Then
                Return View(isAndroid)
            End If

            Return RedirectToAction("Index", "Logon")
        End Function

        ''' <summary>
        ''' Generates a client certificate for the requesting user
        ''' </summary>
        ''' <param name="certName">The certificate that will be genrated</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GenerateClientCert(certName As String)

            Dim isAndroid = GlobalFunctions.isAndroid(Request)
            If (isAndroid) Then
                Dim result = Certificates.generateAndroidIdent(certName)
                Return Json(result)
            Else
                Return Json(Certificates.generateCertificate(certName, certName))
            End If

        End Function
    End Class
End Namespace