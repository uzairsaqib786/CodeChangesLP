Imports System.ServiceProcess

' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class STEService

    ''' <summary>
    ''' STE Service identifier in the service explorer.  Windows knows the service by this name, it's set in the Service's installer.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly STEServiceName As String = "STEService"


    ''' <summary>
    ''' Gets the STE Service's status on startup of the application
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Exception being caught here should only fire off if there is a problem with finding the service name. (if it wasn't installed, etc.)</remarks>
    Public Shared Function ServiceStatus() As Boolean
        Dim service As ServiceController = Nothing
        Dim running As Boolean = False
        Try
            service = New ServiceController(STEServiceName)
            running = Not service.Status.Equals(ServiceControllerStatus.Stopped)
        Catch ex As Exception
            insertErrorMessages("STEService", "ServiceStatus", ex.Message, "", "")
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try
        Return running
    End Function

    ''' <summary>
    ''' Stops the STE service
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <returns>True if the service was stopped successfully, false if there was an error</returns>
    ''' <remarks></remarks>
    Public Shared Function StopSTEService(user As String, WSID As String, Optional restart As Boolean = False) As Boolean
        Dim service As ServiceController = Nothing
        Dim stopped As Boolean = False
        Try
            ' reference to the service
            service = New ServiceController(STEServiceName)
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
            Debug.WriteLine(ex.Message)
            insertErrorMessages("STEService", "ServiceStatus", ex.Message, "", "")
            Return False ' service may not be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return stopped
    End Function

    ''' <summary>
    ''' Starts the STE service
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StartSTEService(user As String, WSID As String, Optional restart As Boolean = False) As Boolean
        Dim service As ServiceController = Nothing
        Dim started As Boolean = False
        Try
            service = New ServiceController(STEServiceName)
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
            Debug.WriteLine(ex.Message)
            insertErrorMessages("STEService", "ServiceStatus", ex.Message, "", "")
            Return False ' service may be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return started
    End Function

    ''' <summary>
    ''' Calls Stop on the STE service and then start
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <returns>True if the restart was successful, false if it was not (either because the service couldn't be stopped or because the service could not be started.  
    ''' See STE Log table for details on these two problems</returns>
    ''' <remarks></remarks>
    Public Shared Function RestartSTEService(user As String, WSID As String) As Boolean
        ' try to stop the service
        Dim stopped As Boolean = StopSTEService(user, WSID), started As Boolean = False
        ' if stop was successful then try starting
        If stopped Then
            started = StartSTEService(user, WSID)
        End If
        Return started
    End Function

End Class
