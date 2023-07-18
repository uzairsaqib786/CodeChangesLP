' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.ServiceProcess

Public Class CCSIFService

    ''' <summary>
    ''' Gets the Print Service's status on startup of the application
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Exception being caught here should only fire off if there is a problem with finding the service name. (if it wasn't installed, etc.)</remarks>
    Public Shared Function ServiceStatus() As Boolean
        Dim service As ServiceController = Nothing
        Dim running As Boolean = False
        Try
            service = New ServiceController("CCSIF")
            running = Not service.Status.Equals(ServiceControllerStatus.Stopped)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try
        Return running
    End Function

    Public Shared Function StartService() As Boolean
        Dim service As ServiceController = Nothing
        Dim started As Boolean = False
        Try
            service = New ServiceController("CCSIF")
            If service.Status.Equals(ServiceControllerStatus.Running) Then
                ' service already started
                started = True
            ElseIf service.Status.Equals(ServiceControllerStatus.StartPending) Then
                service.WaitForStatus(ServiceControllerStatus.Running)
                started = True
            Else
                service.Start()
                service.WaitForStatus(ServiceControllerStatus.Running)
                started = True
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return False ' service may be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return started
    End Function

    Public Shared Function StopService() As Boolean
        Dim service As ServiceController = Nothing
        Dim stopped As Boolean = False
        Try
            ' reference to the service
            service = New ServiceController("CCSIF")
            ' service already stopped
            If service.Status.Equals(ServiceControllerStatus.Stopped) Then
                stopped = True
            ElseIf service.Status.Equals(ServiceControllerStatus.StopPending) Then
                service.WaitForStatus(ServiceControllerStatus.Stopped)
                stopped = True
            Else
                service.Stop()
                service.WaitForStatus(ServiceControllerStatus.Stopped)
                stopped = True
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            Return False ' service may not be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return stopped
    End Function


End Class
