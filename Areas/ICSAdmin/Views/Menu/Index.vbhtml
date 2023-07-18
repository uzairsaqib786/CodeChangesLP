<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.Admin.MenuObj
@Code
    ViewData("Title") = "Main Menu"
    ViewData("PageName") = ""
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<div class="container-fluid">
    <div class="row">
        <div class="redirect-warning">
            @If TempData.ContainsKey("PermissionError") Then
                @<div class="alert alert-warning alert-dismissible" role="alert">
                    <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    You Attempted to Access: <strong>@TempData("PermissionError")</strong> , but do not have the proper permissions
                </div>
            End If
        </div>
    </div>
    @If Model.PickWorkstation Then
        @Html.Partial("~/Areas/ICSAdmin/Views/Menu/MenuPickPartial.vbhtml")
    Else
        @Html.Partial("~/Areas/ICSAdmin/Views/Menu/MenuAdminPartial.vbhtml")
    End If
    
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Menu/Menu.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Menu/MenuHub.js"></script>