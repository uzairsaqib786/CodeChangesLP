' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Net
Imports System.Web.Http
Public Class LLExportController
    Inherits ApiController

    ''' <summary>
    ''' Returns the exported file specified by filename in a filestream.
    ''' </summary>
    ''' <param name="fileName">The desired file</param>
    ''' <param name="WSID">The workstation that the file is on</param>
    ''' <param name="cert">If the file is a certificate</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <HttpGet()>
    Public Function GetFile(fileName As String, WSID As String, Optional cert As Integer = 0) As Http.HttpResponseMessage
        Dim file = HttpContext.Current.Server.MapPath("~/temp/" & WSID & "/" & fileName)
        If cert = 1 Then
            file = "C:\ProgramData\Scotttech\Certs\Issued\" & fileName
        End If
        Dim returnFile As Http.HttpResponseMessage = Nothing
        Try
            If IO.File.Exists(file) Then
                Dim stream = New IO.FileStream(file, IO.FileMode.Open)
                returnFile = New Http.HttpResponseMessage(HttpStatusCode.OK)
                returnFile.Content = New Http.StreamContent(stream)
                If cert = 1 Then
                    returnFile.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("application/x-x509-user-cert")
                Else
                    returnFile.Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream")
                End If

                returnFile.Content.Headers.ContentDisposition = New Http.Headers.ContentDispositionHeaderValue("attachment") With {.FileName = fileName}
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
            insertErrorMessages("LLExportController", "GetFile", ex.Message, "Export Controller", WSID)
        End Try
        Return returnFile
    End Function


End Class
