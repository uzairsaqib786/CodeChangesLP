' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace Admin
    Public MustInherit Class ICSAdminController
        Inherits PickProController

        Public PermissionsList As New Dictionary(Of String, String) From {{"BatchManager", "Batch Manager"}, _
                                                                          {"InventoryMap", "Inventory Map"}, _
                                                                          {"MoveItems", "Move Items"}, _
                                                                          {"CycleCount", "Cycle Count Manager"}, _
                                                                          {"ManualTransactions", "Manual Transactions"}, _
                                                                          {"EventLog", "Event Log Manager"}, _
                                                                          {"SystemReplenishment", "Replenishment"}, _
                                                                          {"InventoryMaster", "Inventory"}, _
                                                                          {"LocationAssignment", "Location Assignment"}, _
                                                                          {"CustomReports", "Reports"}, _
                                                                          {"Employees", "Employees"}, _
                                                                          {"DeAllocateOrders", "De-Allocate Orders"}, _
                                                                          {"Preferences", "Preferences"}, _
                                                                          {"Transactions", "Transaction Journal"}}
        ''' <summary>
        ''' initializes the session for the user
        ''' </summary>
        ''' <param name="filterContext">The view that is being loaded</param>
        Protected Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
            MyBase.OnActionExecuting(filterContext)

            Dim ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName

            If filterContext.ActionDescriptor.ActionName = "Index" Then
                Dim permission = ""
                If PermissionsList.ContainsKey(ControllerName) Then
                    permission = PermissionsList(ControllerName)
                End If
                Dim result As List(Of String) = ICSAdminFunctions.InitializeSession(user:=User.Identity.Name, permission:=permission, _
                                                              sessionRef:=Session, tempRef:=TempData, WSID:=Session("WSID"), clientCert:=Request.ClientCertificate)
                If Not IsNothing(result) And Not isRedirecting Then
                    Dim routeValues = New With {.Area = "ICSAdmin"}
                    filterContext.Result = RedirectToAction(result(0), result(1), routeValues)
                End If
            End If


            'Dim ActionName = filterContext.ActionDescriptor.ActionName
            'Dim ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName
            'If ActionName = "Index" Then
            '    If ControllerName  <> "GlobalConfig" Then
            '        filterContext.Result = RedirectToAction("Index", "GlobalConfig")
            '    End If
            'End If
        End Sub
    End Class
End Namespace

