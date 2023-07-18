' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Admin.Controllers
    <Authorize>
    Public Class EmployeesController
        Inherits ICSAdminController

        ' GET: Employees
        ''' <summary>
        ''' Returns the Employees view
        ''' </summary>
        ''' <returns>View object that contains the information needed for the html page</returns>
        ''' <remarks></remarks>
        Function Index() As ActionResult
            Return View(New With {.allControls = Employees.allAccess(User.Identity.Name, Session("WSID")), .allGroups = Employees.allGroups(User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Gets the data for the Employees datatable instance
        ''' </summary>
        ''' <param name="data">Filters for selecting the Employees data</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function employeeStatInfo(data As TableObjectSent) As ActionResult
            Return Json(EmployeesStats.getEmployeeStats(data.users, data.sDate, data.eDate, data.start + 1, data.length + data.start, data.draw, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets typeahead for all Button Control Names
        ''' </summary>
        ''' <param name="query">Button Control searching for</param>
        ''' <returns>A list of all the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getControlNames(query As String) As ActionResult
            Return Json(Employees.allAccessFilter(query & "%", User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets typeahead for all Button Control Names
        ''' </summary>
        ''' <param name="query">Button Control searching for</param>
        ''' <returns>A list of all the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getGroupNames(query As String) As ActionResult
            Return Json(Employees.allGroupsFilter(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets typeahead with Employee data for Employee management
        ''' </summary>
        ''' <param name="query">employee to look up</param>
        ''' <returns>A list of object that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function employeeLookup(query As String) As ActionResult
            Return Json(Employees.employeeLookup(query & "%", User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Calls the print service to print the proper report employee stats
        ''' </summary>
        ''' <param name="startDate">The start date value for the records displayed</param>
        ''' <param name="endDate">The end date value for the records displayed</param>
        ''' <param name="checkVal">The identifier to print by date or user</param>
        ''' <param name="userList">The string of all users selected</param>
        ''' <returns>A boolean telling if the print job was successful</returns>
        ''' <remarks></remarks>
        Public Function printEmployeeStatsReport(startDate As Date, endDate As Date, checkVal As String, userList As String) As ActionResult
            ' calls windows service procedure for printing after setting up the print
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selUserStatsLL"
                Dim params As String(,) = {{"@Start", startDate, strVar}, {"@End", endDate, strVar}, _
                                           {"@UserList", userList, strVar}, {"@ByDate", IIf(checkVal = "date", 1, 0), intVar}}
                Dim filename As String = "EmployeeStatsReport.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Employee Stats Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("EmployeesCOntroller", "printEmployeeStatsReport", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Sends a print message to the service to print an Employees report
        ''' </summary>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function printEmployees() As ActionResult
            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance


            Try
                Dim sp As String = "selEmployeesLL"
                Dim params As String(,) = {{"nothing"}}
                Dim filename As String = "Employees.lst", LLType As String = "List"


                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Employee  Report", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("EmployeesController", "printEmployees", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Calls the print service to print the proper report employee stats
        ''' </summary>
        ''' <param name="checkVal">The identifier to print by date or user</param>
        ''' <param name="group">The string of the desired group to print</param>
        ''' <returns>A boolean telling if the print job exectued successfully</returns>
        ''' <remarks></remarks>
        Public Function printEmployeeGroup(checkVal As String, group As String) As ActionResult
            ' calls windows service procedure for printing after setting up the print

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance


            Try
                Dim sp As String = IIf(checkVal = "group", "selGroupControlsLL", "selGroupControlsAll")
                Dim params As String(,) = If(checkVal = "group", {{"@Group", group, strVar}}, {{"nothing"}})
                Dim filename As String = IIf(checkVal = "group", "PrintFunctionsByGroup.lst", "PrintFunctionsAllGroups.lst"), LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Employee Group  Report", LLType, filename, sp, params)
                Clients.Print(m)

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("EmployeeController", "printEmployeeGroup", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace