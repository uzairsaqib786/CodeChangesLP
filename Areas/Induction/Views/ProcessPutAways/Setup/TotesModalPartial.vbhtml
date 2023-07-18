<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<div class="modal fade" id="tote_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="tote_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1000px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="tote_label">Totes - Add, Edit, Delete, and Print.</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" style="display:none;" id="ShowButtons" value="@(IIf(permissions.Contains("Tote Manager") And permissions.Contains("Tote Admin Menu"), "true", "false"))" />
                <div class="row top-spacer">
                    <div class="col-md-4">
                        <input type="text" class="form-control no-focus" id="FromToteID" placeholder="From Tote ID" />
                    </div>
                    <div class="col-md-4">
                        <input type="text" class="form-control no-focus" disabled="disabled" id="ToToteID" placeholder="To Tote ID"  />
                    </div>
                    <div class="col-md-4">
                        <button type="button" class="btn btn-primary btn-block" disabled id="TotePrintRange">Print Range</button>
                    </div>
                </div>
                <div class="row" id="UnmanagedRow">
                    <div class="col-md-12">
                        <input type="hidden" id="ManageTotes" value="@Model.preferences.ValTotes.ToString().ToLower()" />
                        <div class="row">
                            <div class="col-md-6">
                                <label>Unmanaged Tote ID:</label>
                            </div>
                            <div class="col-md-3">
                                <label>Cells:</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <input type="text" class="form-control" id="UnmanagedTote" maxlength="50" placeholder="Tote ID" />
                            </div>
                            <div class="col-md-2">
                                <input type="text" class="form-control" id="UnmanagedCells" placeholder="Cells" />
                            </div>
                            <div class="col-md-1 col-md-offset-3">
                                <button type="button" disabled="disabled" class="btn btn-primary set-tote-other"><span class="glyphicon glyphicon-edit"></span></button>
                            </div>
                        </div>
                    </div>
                    </div>
                <div class="row top-spacer">
                    <div class="col-md-6">
                        <label>Managed Tote IDs:</label>
                    </div>
                    <div class="col-md-2">
                        <label>Cells:</label>
                    </div>
                    <div class="col-md-offset-3 col-md-1">
                        <button id="tote_add" type="button" data-toggle="tooltip" data-placement="top" title="Add New Tote" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="tote_container" style="overflow-y:scroll;">

                    </div>
                </div>
            </div>
            <div class="modal-footer"> 
                <button type="button" class="btn btn-default " data-dismiss="modal" id="tote_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/ProcessPutAways/Totes.js"></script>