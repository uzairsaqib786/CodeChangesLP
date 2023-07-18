<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Menu"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
@Imports PickPro_Web
<div class="container-fluid">
    <div class="row">
        <div class="col-sm-6">
            <a style="margin-bottom:30px;" href="/CM/Consolidation" type="button" class="btn btn-xl btn-block btn-primary">Consolidation</a>
        </div>
        <div class="col-sm-6">
            <a style="margin-bottom:30px;" href="/CM/Menu/StagingLocations" type="button" class="btn btn-xl btn-block btn-primary">Staging Locations</a>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            @If permissions.Contains("Consolidation Mgr Admin") Then
                @<a style="margin-bottom:30px;" href="/CM/Preferences" type="button" class="btn btn-xl btn-block btn-primary">Preferences</a>
            End If
        </div>
        <div class="col-sm-6">
            @If permissions.Contains("Consolidation Mgr Admin") Then
                @<a style="margin-bottom:30px;" href="/CustomReports?App=CM" class="btn btn-primary btn-block btn-xl">Reporting</a>
            End If
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <a style="margin-bottom:30px;" href="/Transactions?app=CM" class="btn btn-primary btn-block btn-xl">Order Status</a>
        </div>
    </div>
</div>
