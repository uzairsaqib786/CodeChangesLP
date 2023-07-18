' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Drawing.Imaging.ImageFormat
Imports System.Drawing

Namespace Admin.Controllers
    <Authorize()>
    Public Class PreferencesController
        Inherits ICSAdminController

        ' GET: /Preferences
        Function Index() As ActionResult
            Return View(New With {.aliases = New AliasModel(User.Identity.Name, Session("WSID")), .OrderSort = Preferences.getOrderSort(User.Identity.Name, Session("WSID")), .PODs = Preferences.selPodIDDropDown(User.Identity.Name, Session("WSID"), "PrefSelPodIDDrop"),
                                  .CompanyLogo = GlobalFunctions.getCompanyLogoExtension(User.Identity.Name, Session("WSID")), .Admin = GlobalFunctions.getAccessLevel(User.Identity.Name, Session("WSID")),
                                  .SVNames = Preferences.getSVFieldNames(User.Identity.Name, Session("WSID")), .ConnectionStrings = Config.getAllConnectionStrings(), .printers = GlobalFunctions.getAllPrinters(Session("WSID"), User.Identity.Name),
                                  .TreeZones = Preferences.getLightTreeZones(Session("WSID"), User.Identity.Name), .TreeCars = Preferences.getLightTreeCars(Session("WSID"), User.Identity.Name),
                                  .TreeBinLocs = Preferences.getLightTreeBinLoc(Session("WSID"), User.Identity.Name), .CFZones = Preferences.selPodIDDropDown(User.Identity.Name, Session("WSID"), "SelLocZonesCartonFlow"), .FilterFields = getOSFieldFilterNames()})
        End Function


        Function getOSFieldFilterNames() As Dictionary(Of String, String)
            Return New Dictionary(Of String, String) From {
                {"UserField1", "User Field1"},
                {"UserField2", "User Field2"},
                {"UserField3", "User Field3"},
                {"UserField4", "User Field4"},
                {"UserField5", "User Field5"},
                {"UserField6", "User Field6"},
                {"UserField7", "User Field7"},
                {"UserField8", "User Field8"},
                {"UserField9", "User Field9"},
                {"UserField10", "User Field10"}
            }
        End Function
        ''' <summary>
        ''' Deletes all image files in the company logo directory before saving the newest uploaded file if the file is an image
        ''' </summary>
        ''' <returns>A boolean to tell if the operation completed successfully</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        Function CompanyLogoUpload() As ActionResult
            Try
                Dim image As HttpPostedFileBase = Request.Files(0)
                Dim mimetype As String = MimeMapping.GetMimeMapping(image.FileName)
                If Not mimetype.Contains("image") Then
                    Return Json(False, JsonRequestBehavior.AllowGet)
                End If

                Dim location As String = Server.MapPath("~/images/CompanyLogo/")

                Dim dfiles As String() = System.IO.Directory.GetFiles(location)
                For Each df In dfiles
                    If MimeMapping.GetMimeMapping(df).Contains("image") Then
                        System.IO.File.Delete(df)
                    End If
                Next
                Dim ext As String = System.IO.Path.GetExtension(image.FileName)
                Dim filename As String = "logo" & ext
                image.SaveAs(location & filename)

                RunActionSP("updateCompanyLogo", Session("WSID"), {{"@Ext", ext, strVar}, {"@User", User.Identity.Name, strVar}, {"@WSID", Session("WSID"), strVar}})

            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("PreferencesController", "CompanyLogoUpload", ex.ToString(), User.Identity.Name, Session("WSID"))
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the scan verification table
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function getScanVerifyTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(Preferences.getScanVerifyTable(data.draw, data.start + 1, data.length + data.start, _
                                                              Request.QueryString.Get("order[0][column]"), _
                                                              Request.QueryString.Get("order[0][dir]"), User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the device preferences table
        ''' </summary>
        ''' <param name="data">The data needed to get the records for the datatable</param>
        ''' <returns>A table object with the needed data in order to populate the datatable</returns>
        ''' <remarks></remarks>
        Function getDevicePreferencesTable(data As TableObjectSent) As ActionResult
            Return Json(Preferences.getDevicePreferencesTable(data.draw, data.zone, Request.QueryString.Get("order[0][column]"), _
                                                              Request.QueryString.Get("order[0][dir]"), data.start, data.length + data.start, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the Device Preference typeahead for zones
        ''' </summary>
        ''' <param name="query">Zone to filter on</param>
        ''' <returns>A list of string that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getZonesForDevPrefs(query As String) As ActionResult
            Return Json(Preferences.getDevPrefZoneTypeahead(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the Device Preferences typeahead for units of measure
        ''' </summary>
        ''' <param name="query"></param>
        ''' <returns>A list of string that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getUoMForDevPrefs(query As String) As ActionResult
            Return Json(Preferences.getDevPrefUoMTypeahead(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets the typeahead for device preferences cell sizes
        ''' </summary>
        ''' <param name="query">Cell Size to filter on</param>
        ''' <returns>A list of string that contains the suggestions for the typeahead</returns>
        ''' <remarks></remarks>
        Function getCellSizeForDevPrefs(query As String) As ActionResult
            Return Json(Preferences.getCellSizeTypeaheadDevPrefs(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
