<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Inventory Master"
    ViewData("PageName") = "&nbsp; | &nbsp; Inventory Master"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.App)
End Code
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div id="quarAlert">

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3 class="panel-title">
                                                @Model.Alias.ItemNumber Lookup
                                            </h3>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <input type="hidden" value="@Model.NewItem.ToString().ToLower()" id="NewItem" />
                                                <input  maxlength="50" oninput="$('#stockcodeCopy').val(this.value)" placeholder="" type="text" id="stockCode" class="form-control" value="@IIf(Model.ItemNum Is Nothing Or Model.ItemNum = "", "", Model.ItemNum)">
                                                <input type="text" style="display:none;" id="stockcodeCopy" />
                                            </div>
                                            <div class="col-md-2">
                                                <button type="button" class="btn btn-primary btn-block" id="clearButton"><u>C</u>lear</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <button type="button" class="btn btn-primary btn-block" id="goToInvMap" disabled="disabled">View Item Locations</button>
                                    </div>
                                    <div class="col-md-4">
                                        <h2 id="kitDisplay" style="margin-top:0px;"><span style="display:block" class="label label-default">Kit</span></h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="active"><a id="detailstab" style="border:1px solid #ddd;" href="#details" role="tab" data-toggle="tab">D<u>e</u>tails</a></li>
                                <li><a id="itemtab" href="#item" role="tab" data-toggle="tab"><u>I</u>tem Setup</a></li>
                                <li><a id="kittab" href="#kit" role="tab" data-toggle="tab"><u>K</u>it Items</a></li>
                                <li><a id="locationstab" href="#locations" role="tab" data-toggle="tab"><u>L</u>ocations</a></li>
                                <li><a id="reeltab" href="#reel" role="tab" data-toggle="tab">Reel Trackin<u>g</u></a></li>
                                <li><a id="scantab" href="#scan" role="tab" data-toggle="tab"><u>S</u>can Codes</a></li>
                                <li><a id="weightab" href="#weigh" role="tab" data-toggle="tab"><u>W</u>eigh Scale</a></li>
                                <li><a id="othertab" href="#other" role="tab" data-toggle="tab"><u>O</u>ther</a></li>
                                <li><button style="margin-left:5px;margin-top:2px;margin-right:2px;" type="button" class="btn btn-primary" id="addButton" data-toggle="tooltip" data-placement="top" title="Add New Item"><span class="glyphicon glyphicon-plus"></span></button></li>
                                <li><button disabled="disabled" style="margin-top:2px;margin-right:2px;" type="button" class="btn btn-primary" id="saveButton" data-toggle="tooltip" data-placement="top" title="Save Item"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                                <li><button disabled="disabled" style="margin-top:2px;margin-right:2px;" type="button" class="btn btn-danger" id="deleteButton" data-toggle="tooltip" data-placement="top" title="Delete Item"><span class="glyphicon glyphicon-trash"></span></button></li>
                                <li><button disabled="disabled" style="margin-top:2px;margin-right:2px;" type="button" class="btn btn-primary" id="quarantineButton"><u>Q</u>uarantine Item</button>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div class="tab-pane panel panel-info active" id="details">
                                    <div class="panel-body disable-inputs">
                                        @Html.Partial("ItemOptionsPartial")
                                    </div>
                                </div>
                                <div class="tab-pane panel panel-info" id="locations">
                                    <div class="panel-body disable-inputs">
                                        @Html.Partial("InvLocationsPartial")
                                    </div>
                                </div>

                                <div class="tab-pane" id="item">
                                    <div class="panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("ItemSetupPartial")
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="kit">
                                    <div class="tab-pane panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("KitItemsPartial")
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="weigh">
                                    <div class="tab-pane panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("WeighScalePartial")
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="scan">
                                    <div class="tab-pane panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("ScanCodesPartial")
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="reel">
                                    <div class="tab-pane panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("ReelTrackingPartial")
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="other">
                                    <div class="tab-pane panel panel-info" id="home">
                                        <div class="panel-body disable-inputs">
                                            @Html.Partial("OtherPartial")
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-1">
            <button type="button" id="previousItemNum" class="btn btn-primary"><span class="glyphicon glyphicon-arrow-left"></span></button>
            <button type="button" id="nextItemNum" class="btn btn-primary"><span class="glyphicon glyphicon-arrow-right"></span></button>
        </div>
    </div>
    <div class="row" style="padding-left:10px;">
        <div class="col-md-12">
            <p id="ItemNumPos" style="display:inline">@Model.CountData.Pos</p><p style="display:inline"> of </p><p id="ItemNumTotal" style="display:inline">@Model.CountData.Total</p>
        </div>
    </div>
</div>

<!-- Modal -->
<div class="modal fade" id="myModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header dynamic">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="myModalLabel">Modal title</h4>
            </div>
            <div class="modal-body">
                ...
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<!-- Modal -->
<div class="modal fade" id="quarantineModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="quarantineLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="quarantineLabel">Quarantine</h4>
            </div>
            <div class="modal-body">
                <div id="quarMessage"></div>
            </div>
            <div class="modal-footer">
                <div class="pull-left"><input type='checkbox' id='quarRP' /> Append Reprocess to Open Transactions?</div>
                <button type="button" id="quarantineClick" class="btn btn-primary">Submit</button>
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<!-- Modal -->
<div class="modal fade" id="addItemModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="addItemLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close modalClose" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                <h4 class="modal-title" id="addItemLabel">Add New Item</h4>
            </div>
            <div class="modal-body">
                <div id="itemAlert" class="row">

                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="newItemNumber">Item Number</label>
                            <input type="text" class="form-control" id="newItemNumber" maxlength="50" placeholder="Item Number">
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="newDescription">Description</label>
                            <input type="text" class="form-control" id="newDescription" placeholder="Description" maxlength="255">
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" id="newItemSave" class="btn btn-primary">Add Item</button>
                <button type="button" class="btn btn-default modalClose" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/Inventory Master/InventoryMaster.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterSave.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterItemSetup.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterKitItems.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterScanCodes.js"></script>
<script src="~/Scripts/Inventory Master/InventoryMasterHub.js"></script>
<script src="~/Scripts/Inventory Master/InventoryMasterModals.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterShortcuts.js"></script>
<script src="~/Scripts/Inventory Master/InvMasterFilters.js"></script>
<script>
    $(document).ready(function () {
        $('#stockCode').focus()
    })
</script>
@Html.Partial("~/Views/ModalPartials/DescriptionModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/CategoryModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/UnitOfMeasureModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/CellSizeModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/VelocityCodeModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/ItemNumberModalPartial.vbhtml")
@Html.Partial("~/Views/InventoryMaster/UpdateItemNumModal.vbhtml")