<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Tote Transaction View"
    ViewData("PageName") = "&nbsp; | &nbsp; Tote Transaction View"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<div class="container-fluid">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">Tote Info</h4>
                </div>
                <div class="panel-body white-bg">
                    <div class="row">
                        <div class="col-md-4">
                            <label>Batch ID</label>
                            <input type="text" class="form-control" maxlength="50" id="TTViewBatchID"  disabled="disabled" value="@Model.batchid" />
                        </div>
                        <div class="col-md-4">
                            <label>Tote</label>
                            <input type="text" class="form-control" maxlength="50" id="TTViewToteNum"  disabled="disabled" value="@Model.totenum" />
                        </div>
                        <div class="col-md-4">
                            <label>Tote ID</label>
                            <input type="text" class="form-control" maxlength="50" id="TTViewToteID" disabled="disabled" value="@Model.toteid" />
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-2">
                            <button type="button" class="btn btn-danger btn-block" id="TTViewClearTote">Clear Tote</button>
                        </div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-primary btn-block" id="TTViewFullTote">Full Tote</button>
                        </div>
                        <div class="col-md-2">
                            <div class="dropdown">
                                <button id="goTo" class="btn btn-primary dropdown-toggle btn-block" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a id="TTViewToteLabel">Print Tote Label</a></li>
                                    <li><a id="TTViewItemLabel">Print Item Labels</a></li>
                                    <li><a id="TTViewToteContents">Print Tote Contents</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">Items</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" class="btn btn-danger btn-block" id="TTViewClearItem" disabled>Clear</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" class="btn btn-danger btn-block" id="TTViewDeAlloItem" disabled>Clear and DeAllocate</button>
                        </div>
                        <div class="col-md-2" style="padding-top:20px;">
                            <button type="button" class="btn btn-primary btn-block" id="TTViewSingleItemLabel" disabled>Print Tote Label</button>
                        </div>
                        <div class="col-md-2">
                            @If Model.prefs.ReqNumPutLabels Then
                                @<label>Number of Copies</label>
                                @<input type="text" class="form-control" maxlength="1" oninput="setNumericInRange($(this), 1, 5)" id="PrintNumCopies" value="1" />
                            Else
                                @<label>Number of Copies</label>
                                @<input type="text" hidden class="form-control" maxlength="1" oninput="setNumeric($(this))" id="PrintNumCopies" value="1" />
                            End If
                        </div>
                    </div>
                    <div class="row" style="padding-top:10px;">
                        <div class="col-md-12">
                            <table id="ToteTransViewTable" class="table table-bordered table-striped table-condensed" style="background-color:white;margin-bottom:5px;">
                                <thead>
                                    <tr>
                                        <th class="text-center">ID</th>
                                        <th class="text-center">Cell</th>
                                        <th class="text-center">Item Number</th>
                                        <th class="text-center">Transaction Quantity</th>
                                        <th class="text-center">Location</th>
                                        <th class="text-center">Host Transaction ID</th>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--insert prefs here-->
    <input hidden id="TTViewPrintDir" value="@Model.prefs.PrintDirect.ToString().ToLower()" />
    <input hidden id="TTViewZoneLab" value="@Model.zonelab" />
</div>
<script>
    var batchHub = $.connection.processPutAwaysHub;
    $(window).on("beforeunload", function () {
        batchHub.server.refreshTotes();
    })
</script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Process/ToteTransView.js"></script>
@Html.partial("~/Areas/Induction/Views/Modal/DeleteBatchModalPartial.vbhtml")