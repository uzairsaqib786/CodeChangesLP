' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21
Imports System.ServiceProcess

Public Class PrintService
    ''' <summary>
    ''' The constant for the expected querystring data currentUser property for the Print Service in hubs 
    ''' (the hub instance uses the querystring to identify users, the service is known by this name)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly PrintServiceHubName As String = "PrintServer"

    ''' <summary>
    ''' Print Service identifier in the service explorer.  Windows knows the service by this name, it's set in the Service's installer.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly PrintServiceName As String = "LLPrintServer"

    ''' <summary>
    ''' Indicates if the Print Service is running (true) or not (false)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared PrintServiceRunning As Boolean = ServiceStatus()

    Public Shared PrintServiceCheckinTime As DateTime = Now()

    ''' <summary>
    ''' Checks the sattus of the print service and restarts it if the checkin time is greater than 2
    ''' </summary>
    Public Shared Sub checkPrintServiceStatus()
        If (Now - PrintServiceCheckinTime).TotalMinutes > 2 And Not GlobalFunctions.Testing Then
            RestartPrintService("System", "System")
        End If
    End Sub

    ''' <summary>
    ''' Inserts a message into the Print Log table
    ''' </summary>
    ''' <param name="err">Error 1=true, 0=false</param>
    ''' <param name="message"></param>
    ''' <param name="user"></param>
    ''' <param name="WSID"></param>
    ''' <param name="userDirectory"></param>
    ''' <param name="view"></param>
    ''' <param name="type"></param>
    ''' <param name="location"></param>
    ''' <remarks></remarks>
    Public Shared Sub insertPrintLog(err As Integer, message As String, user As String, WSID As String, userDirectory As String, view As String, type As Integer, location As String)
        Try
            Dim messageoverflow As String = ""
            Dim reportlabel As String
            Select Case type
                Case LlProject.List
                    reportlabel = "Report"
                Case LlProject.Label
                    reportlabel = "Label"
                Case LlProject.Card
                    reportlabel = "Card"
                Case Else
                    reportlabel = "None"
            End Select

            ' we were getting overflow errors from message size before on certain exceptions
            If message.Length >= 1000 Then
                messageoverflow = message.Substring(1000, 1000)
                message = message.Substring(0, 1000)
            End If

            RunActionSP("insPrintLog", "CONFIG", {{"@error", err, intVar},
                                        {"@message", message, strVar},
                                        {"@messageoverflow", messageoverflow, strVar},
                                        {"@user", user, strVar},
                                        {"@WSID", WSID, strVar},
                                        {"@userDir", userDirectory, strVar},
                                        {"@view", view, strVar},
                                        {"@type", reportlabel, strVar},
                                        {"@location", location, strVar}})
        Catch ex As Exception
            insertErrorMessages("Print Service", "insertPrintLog", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the Print Service's status on startup of the application
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Exception being caught here should only fire off if there is a problem with finding the service name. (if it wasn't installed, etc.)</remarks>
    Private Shared Function ServiceStatus() As Boolean
        Dim service As ServiceController = Nothing
        Dim running As Boolean = False
        Try
            service = New ServiceController(PrintServiceName)
            running = Not service.Status.Equals(ServiceControllerStatus.Stopped)
        Catch ex As Exception
            PrintService.insertPrintLog(1, "Print Service Status Query failed.  Service may not be installed correctly. Error: " _
                                        & ex.ToString(), "PickPro", "None", "", "PickPro", 0, "PickPro")
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try
        Return running
    End Function

    ''' <summary>
    ''' Stops the print service
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting workstation</param>
    ''' <returns>True if the service was stopped successfully, false if there was an error</returns>
    ''' <remarks></remarks>
    Public Shared Function StopPrintService(user As String, WSID As String, Optional restart As Boolean = False) As Boolean
        Dim service As ServiceController = Nothing
        Dim stopped As Boolean = False
        Try
            ' reference to the service
            service = New ServiceController(PrintServiceName)
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
            insertPrintLog(1, "Error attempting to stop the Print Service" & IIf(restart, " (during restart call)", "") & ":  " & ex.ToString(), user, WSID, "", "", 0, WSID)
            Return False ' service may not be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return stopped
    End Function

    ''' <summary>
    ''' Starts the print service
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StartPrintService(user As String, WSID As String, Optional restart As Boolean = False) As Boolean
        Dim service As ServiceController = Nothing
        Dim started As Boolean = False
        Try
            service = New ServiceController(PrintServiceName)
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
            insertPrintLog(1, "Error attempting to stop the Print Service" & IIf(restart, " (during restart call)", "") & ": " & ex.ToString(), user, WSID, "", "", 0, WSID)
            Return False ' service may be running, but there was a problem anyway
        Finally
            If Not IsNothing(service) Then
                service.Dispose()
            End If
        End Try

        Return started
    End Function

    ''' <summary>
    ''' Calls Stop on the print service and then start
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">Requesting WSID</param>
    ''' <returns>True if the restart was successful, false if it was not (either because the service couldn't be stopped or because the service could not be started.  
    ''' See Print Log table for details on these two problems</returns>
    ''' <remarks></remarks>
    Public Shared Function RestartPrintService(user As String, WSID As String) As Boolean
        ' try to stop the service
        Dim stopped As Boolean = StopPrintService(user, WSID), started As Boolean = False
        ' if stop was successful then try starting
        If stopped Then
            started = StartPrintService(user, WSID)
        End If
        Return started
    End Function
End Class
