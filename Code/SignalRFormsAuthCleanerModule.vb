' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class SignalRFormsAuthCleanerModule
    Implements IHttpModule

    Public Sub Dispose() Implements IHttpModule.Dispose
        'Throw New NotImplementedException()
    End Sub

    Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
        AddHandler context.PreRequestHandlerExecute, AddressOf OnPreSendRequestHeaders
        AddHandler context.PostRequestHandlerExecute, AddressOf OnPreSendRequestHeaders
    End Sub

    Private Function ShouldCleanResponse(path As String) As Boolean
        path = path.ToLower
        Dim urlsToClean = {"/signalr/"}
        For Each url In urlsToClean
            Dim result = path.IndexOf(url, StringComparison.OrdinalIgnoreCase) > -1
            If result Then
                Return True
            End If
        Next
        Return False
    End Function


    Protected Sub OnPreSendRequestHeaders(sender As Object, ev As EventArgs)
        Dim httpContext = DirectCast(sender, HttpApplication).Context
        If ShouldCleanResponse(httpContext.Request.Path) Then
            httpContext.Response.Cookies.Remove(FormsAuthentication.FormsCookieName)
        End If
    End Sub
End Class
