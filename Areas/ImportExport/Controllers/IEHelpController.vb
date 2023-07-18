' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace ImportExport.Controllers
    <Authorize()>
    Public Class IEHelpController
        Inherits PickProController

        ' GET: IEHelp
        Function Index(Optional initialPage As String = "Help", Optional viewToShow As Integer = 0) As ActionResult
            Dim pages As New Dictionary(Of String, String) From {{"Status", "status"}, {"System Settings", "systemsettings"}, {"Transfer Settings", "transfersettings"},
                                                                 {"Assign Locations", "assignlocations"}, {"Manage Data", "managedata"}, {"Archive/Purge", "archivepurge"},
                                                                 {"Inv Fields to Modify", "invfieldstomodify"}, {"File Backup", "filebackup"}, {"FTP", "ftp"}, {"Inventory", "inventory"}}
            Return View(model:=New List(Of Object) From {initialPage, viewToShow, New AliasModel(User.Identity.Name, Session("WSID")), pages})
        End Function

        Function help() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/Help.vbhtml")
        End Function

        Function status() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/Status.vbhtml")
        End Function

        Function systemsettings() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/SystemSettings.vbhtml")
        End Function

        Function transfersettings() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/TransferSettings.vbhtml")
        End Function

        Function assignlocations() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/AssignLocations.vbhtml")
        End Function

        Function managedata() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/ManageData.vbhtml")
        End Function

        Function archivepurge() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/ArchivePurge.vbhtml")
        End Function

        Function invfieldstomodify() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/InvFieldsToModify.vbhtml")
        End Function

        Function filebackup() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/FileBackup.vbhtml")
        End Function

        Function ftp() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/FTP.vbhtml")
        End Function

        Function inventory() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/Inventory.vbhtml")
        End Function

        Function admintools() As ActionResult
            Return PartialView("~/Areas/ImportExport/Views/IEHelp/Partials/AdminTools.vbhtml")
        End Function
    End Class
End Namespace