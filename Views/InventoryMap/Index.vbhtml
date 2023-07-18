<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.InventoryMapModel
@Code
    ViewData("Title") = "Inventory Map"
    ViewData("PageName") = "| Inventory Map"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.App)
End Code

<div class="container-fluid">
    <div class="panel panel-info">
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <!--Custom DataTables Toolbar Reuse this and Edit for Toolbar Funcionality -->
                    <div class="row">
                        <div class="col-xs-7">
                            <label># Entries</label>
                            <select id="pageLength" class="form-control" style="width:auto; display:inline;">
                                <option>10</option>
                                <option>15</option>
                                <option selected="selected">20</option>
                                <option>25</option>
                                <option>50</option>
                                <option>100</option>
                            </select>
                            <button id="columnSequence" type="button" data-toggle="tooltip" data-placement="top" title="Set Column Sequence" class="btn btn-primary">
                                <span class="glyphicon glyphicon-list"></span>
                            </button>
                            @If (Model.userRights.Contains("Inv Map Add Location") And Model.AccessLevel = "Administrator") And Model.App = "Admin" Then
                                @<button id="addItem" type="button" data-toggle="tooltip" data-placement="top" title="Add New Location" class="btn btn-primary">
                                    <span class="glyphicon glyphicon-plus"></span>
                                </button>
                            End If
                            @If Model.App = "Admin" Or Model.App = "IM" Then
                                @<button id="printListIM" type="button" data-toggle="tooltip" data-placement="top" title="Print Label(s)" class="btn btn-primary">
                                    <span class="glyphicon glyphicon-print"></span>
                                </button>
                            End If
                            <button id="editInv" class="btn btn-primary" type="button" data-toggle="tooltip" title="Edit Record" disabled="disabled">
                                <span class="glyphicon glyphicon-pencil"></span>
                            </button>
                            <div class="btn-group">
                                <button disabled="disabled" id="goToToggle"  class="btn btn-primary" type="button" data-toggle="dropdown" data-original-title="View Item in Inventory Master"><span class="glyphicon glyphicon-share"></span><span class="caret"></span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a id="goToInvMaster">View item in Inventory Master</a></li>
                                    <li><a id="goToTransHist">View Location History</a></li>
                                </ul>
                            </div>
                            @If (Model.userRights.Contains("Inv Map Delete") And Model.AccessLevel = "Administrator") And Model.App = "Admin" Then
                                @<button id="deleteItem" data-toggle="tooltip" data-placement="top" title="Delete" type="button" class="btn btn-danger">
                                    <span class="glyphicon glyphicon-trash"></span>
                                </button>
                            End If
                            <div class="btn-group">
                                <button id="viewButton" type="button" value="" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                                    <u>V</u>iew All Locations <span class="caret"></span>
                                </button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a id="allRecords"><u>V</u>iew All Locations</a></li>
                                    <li><a id="openRecords" value="Open">View <u>O</u>pen Locations</a></li>
                                    <li><a id="quarRecords" value="Quarantine">View Quaran<u>t</u>ined Locations</a></li>
                                </ul>
                            </div>
                            <button id="adjustQuantity" disabled="disabled" class="btn btn-primary">Adjust Quantity</button>
                            @If Model.App = "OM" Then
                                @<button id="TransHistBtn" type="button" class="btn btn-primary">Transaction History</button>
                            End If
                            <button id="quarItemMain" disabled="disabled" class="btn btn-primary">Quarantine</button>
                        </div>
                        <div class="col-xs-5" id="searchStringTypeAhead">
                            <label class="pull-right">
                                Search
                                <select id="selection" class="form-control" style="width:auto; display:inline;">
                                    <option value=" "> </option>
                                    @For f As Integer = 0 To Model.columnNames.Count - 1
                                        If Model.ItemNumber <> "" And Model.columnNames(f) = "Item Number" Then
                                        @<option selected="selected" value="@Model.columnNames(f)">@Model.columnNames(f)</option>
                                        Else
                                        @<option value="@Model.columnNames(f)">@Model.columnNames(f)</option>
                                        End If

                                    Next
                                </select>
                                By

                                <input id="searchString" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" value="@Model.ItemNumber" />

                            </label>
                        </div>

                    </div>
                    <!--End Custom Toolbar Code -->
                    <div class="row">
                        <div class="col-md-12">
                            <table id="invMapTable" class="table table-hover table-condensed table-bordered table-striped" style="background:white;">
                                <thead>
                                    <tr>
                                        @For Each columnName In Model.columnNames
                                            If columnName = "Inv Map ID" Then
                                            @<th class="sorting_desc" style="min-width:130px;">@columnName</th>
                                            Else
                                            @<th class="sorting" style="min-width:130px;">@columnName</th>
                                            End If
                                        Next
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div hidden="hidden" class="col-md-12">
    <input type="hidden" value="@Model.columnNames.IndexOf("Warehouse")" id="warehouseCol" />
    <input type="hidden" value="@Model.columnNames.IndexOf("Inv Map ID")" id="invMapIDCol" />
    <input type="hidden" value="@Model.columnNames.IndexOf("Master Location")" id="masterLocCol" />
    <input type="hidden" value="@Model.columnNames.IndexOf("Date Sensitive")" id="dteSensCol" />
    <input type="hidden" value="@Model.columnNames.IndexOf("Dedicated")" id="dedicCol" />
    <form id="colSequenceForm" method="post" action="/Admin/ColumnSequence">
        <input type="hidden" name="table" value="Inventory Map" />
        <input type="hidden" name="app" value="@Model.App" />
    </form>
</div>
<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/toggles.min.js"></script>
<script src="~/Scripts/InventoryMap/InventoryMap.js"></script>
<script src="~/Scripts/InventoryMap/InvMapTypeaheads.js"></script>
<script src="~/Scripts/InventoryMap/InvMapDataTable.js"></script>
<script src="~/Scripts/InventoryMap/InvMapFilters.js"></script>
@Html.Partial("~/Views/InventoryMap/InvMapModal.vbhtml")
@Html.Partial("~/Views/InventoryMap/ItemQuantityModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/LocationsModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/WarehouseModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/ItemNumberModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/DescriptionModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/CellSizeModalPartial.vbhtml")
@Html.Partial("~/Views/ModalPartials/VelocityCodeModalPartial.vbhtml")
@Html.Partial("~/Views/InventoryMap/PrintModal.vbhtml")