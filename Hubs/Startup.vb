' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports Owin
Imports Microsoft.Owin
'This Code Runs on Startup, and Starts Hosting the SignalR server that we want to connect to
<Assembly: OwinStartup(GetType(Startup))> 
Public Class Startup

    ''' <summary>
    ''' Configures SignalR settings.
    ''' </summary>
    ''' <param name="app"></param>
    ''' <remarks></remarks>
    Sub Configuration(ByVal app As IAppBuilder)
        ' several options are available for this call for debugging purposes
        app.Map("/signalr", Sub(map)
                                Dim hubConfiguration = New HubConfiguration
                                hubConfiguration.EnableDetailedErrors = True
                                map.RunSignalR(hubConfiguration)
                            End Sub)
    End Sub
End Class
