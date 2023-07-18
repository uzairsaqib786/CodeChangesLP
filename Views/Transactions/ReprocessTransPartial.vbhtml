<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
@Code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="row">
    <div class="col-md-12" id="TransactionsInReprocess">
        <div class="panel panel-info" style="margin-bottom:0">
            <div class="panel-heading">
                <h3 class="panel-title">Transactions in Reprocess</h3>
            </div>
            <div class="panel-body" style="padding-bottom:20px; margin-bottom:0;">
                <div class="row" style="padding-bottom:-15px;">
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Records to View</label>
                            </div>
                            <div class="col-md-6">
                                <label>Reason Filter</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                @If Model.defaultValues(1) = "history" And Model.toShow = "Reprocess Transactions" Then

                                    @<Label Class="radio-inline">
                                        <input type="radio" name="HistOrReprocess" id="reprocess" /> Reprocess
                                    </Label>
                                    @<Label Class="radio-inline">
                                        <input checked="checked" type="radio" name="HistOrReprocess" id="hist" /> History
                                    </Label>
                                Else
                                    @<Label Class="radio-inline">
                                        <input checked="checked" type="radio" name="HistOrReprocess" id="reprocess" /> Reprocess
                                    </Label>
                                    @<Label Class="radio-inline">
                                        <input type="radio" name="HistOrReprocess" id="hist" /> History
                                    </Label>
                                End If
                            </div>
                            <div Class="col-md-6">
                                @If Model.holds Or Model.defaultValues(2) = "hold" And Model.toShow = "Reprocess Transactions" Then
                                    @<label class="radio-inline">
                                        <input type="radio" name="HoldOrAll" id="all" /> None
                                    </label>
                                    @<label class="radio-inline">
                                        <input checked="checked" type="radio" name="HoldOrAll" id="hold" /> On Hold
                                    </label>
                                Else
                                    @<label class="radio-inline">
                                        <input checked="checked" type="radio" name="HoldOrAll" id="all" /> None
                                    </label>
                                    @<label class="radio-inline">
                                        <input type="radio" name="HoldOrAll" id="hold" /> On Hold
                                    </label>
                                End If
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            @If Model.toShow = "Reprocess Transactions" Then
                                @<input Class="form-control" type="text" id="ROrder" placeholder="Order Number" maxlength="50" value="@Model.defaultValues(3)" />
                            Else
                                @<input Class="form-control" type="text" id="ROrder" placeholder="Order Number" maxlength="50" />
                            End If
                        </div>
                    </div>
                    <div class="col-md-3" style="margin-left:20px">
                        <div class="row">
                            @If Model.toShow = "Reprocess Transactions" Then
                                @<input Class="form-control" type="text" id="RItem" placeholder="@Model.aliases.ItemNumber" value="@Model.defaultValues(4)" maxlength="50" />
                            Else
                                @<input Class="form-control" type="text" id="RItem" placeholder="@Model.aliases.ItemNumber" maxlength="50" />
                            End If
                        </div>
                    </div>
                    <div class="col-md-2" style="margin-left:20px;">
                        <button id="clearInputs" type="button" data-toggle="tooltip" data-placement="top" title="Clear Filters" class="btn btn-primary">
                            <span class="glyphicon glyphicon-remove"></span>
                        </button>
                        <div class="btn-group">
                            <button id="RPrint" type="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
                                <span class="glyphicon glyphicon-print"></span> <span class="caret"></span>
                            </button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="PrintSelected" style="display:none;" class="OrderSelectedOnly Print-Report">Print Selected</a>
                                    <a href="#" id="PrintAll" class="Print-Report">Print All Records</a>
                                    <a href="#" id="PrintReason" style="display:none;" class="OrderSelectedOnly Print-Report">Print By Selected Reason</a>
                                    <a href="#" id="PrintMessage" style="display:none;" class="OrderSelectedOnly Print-Report">Print By Selected Message</a>
                                    <a href="#" id="PrintDate" style="display:none;" class="OrderSelectedOnly Print-Report">Print By Date/Time</a>
                                    <a href="#" id="PrintItem" style="display:none;" class="ItemOnly Print-Report">Print By @Model.aliases.ItemNumber</a>
                                    <a href="#" id="PrintOrder" style="display:none;" class="OrderOnly Print-Report">Print By Order Number</a>
                                </li>
                            </ul>
                        </div>
                        <div class="btn-group" data-toggle="tooltip" data-placement="top" title="Preview (Top ~50 pages only)">
                            <button id="PreviewPrint" type="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
                                <span class="glyphicon glyphicon-list-alt"></span> <span class="caret"></span>
                            </button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="PrintPreviewSelected" style="display:none;" class="OrderSelectedOnly Print-Report">Print Preview Selected</a>
                                    <a href="#" id="PrintPreviewAll" class="Print-Report">Print Preview All Records</a>
                                    <a href="#" id="PrintPreviewReason" style="display:none;" class="OrderSelectedOnly Print-Report">Print Preview By Selected Reason</a>
                                    <a href="#" id="PrintPreviewMessage" style="display:none;" class="OrderSelectedOnly Print-Report">Print Preview By Selected Message</a>
                                    <a href="#" id="PrintPreviewDate" style="display:none;" class="OrderSelectedOnly Print-Report">Print Preview By Date/Time</a>
                                    <a href="#" id="PrintPreviewItem" style="display:none;" class="ItemOnly Print-Report">Print Preview By @Model.aliases.ItemNumber</a>
                                    <a href="#" id="PrintPreviewOrder" style="display:none;" class="OrderOnly Print-Report">Print Preview By Order Number</a>
                                </li>
                            </ul>
                        </div>
                        <div class="btn-group">
                            <button id="delete" type="button" data-toggle="dropdown" title="Delete" class="btn btn-danger dropdown-toggle">
                                <span class="glyphicon glyphicon-trash"></span>
                                <span class="caret"></span>
                            </button>
                            <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                <li>
                                    <a href="#" id="RPdeleteSelected" style="display:none;" class="OrderSelectedOnly">Delete Selected</a>
                                    @If permissions.Contains("Reprocess Delete All Orders") Then
                                        @<a href="#" id="RPdeleteAll">Delete All Records</a>
                                    End If
                                    <a href="#" id="RPdeleteReason" style="display:none;" class="OrderSelectedOnly">Delete By Selected Reason</a>
                                    <a href="#" id="RPdeleteMessage" style="display:none;" class="OrderSelectedOnly">Delete By Selected Message</a>
                                    <a href="#" id="RPdeleteDate" style="display:none;" class="OrderSelectedOnly">Delete By Date/Time</a>
                                    <a href="#" id="RPdeleteItem" style="display:none;" class="ItemOnly">Delete By @Model.aliases.ItemNumber</a>
                                    <a href="#" id="RPdeleteOrder" style="display:none;" class="OrderOnly">Delete By Order Number</a>
                                    <a href="#" id="RPdeleteReplenishment">Delete Replenishments</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="reprocTableSize" class="col-md-9">
        <div class="row" style="margin-left:10px; margin-bottom:0">
            <div class="col-md-2" style="margin-top:20px">
                <label># Entries</label>
                <select id="pageLength4" class="form-control" style="width:auto; display:inline;">
                    <option>10</option>
                    <option>15</option>
                    <option>20</option>
                    <option>25</option>
                    <option>50</option>
                    <option>100</option>
                </select>
                <button id="setDefaultReproc" type="button" data-toggle="tooltip" data-placement="top" title="Set Column Sequence" class="btn btn-primary">
                    <span class="glyphicon glyphicon-list"></span>
                </button>
            </div>
            <div class="col-md-5" style="display:none; margin-bottom:0; padding:0" id="selectedTransaction">
                <div class="panel panel-info" style="margin-bottom:0; margin-top:10px; margin-right:0">
                    <div class="panel panel-heading" style="margin-bottom:0">
                        <a data-toggle="collapse" data-target="#selTransactionInfo">
                            <h3 class="panel-title text-center">
                                Selected Transaction Info
                                <span class="accordion-caret-down"></span>
                            </h3>
                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="selTransactionInfo">
                        <div class="row">
                            <div class="col-md-6">
                                <label>Record created by</label>
                                <input type="text" class="form-control" id="createdBy" readonly="readonly" />
                            </div>
                            <div class="col-md-6">
                                <label>Transaction Date/Time</label>
                                <input type="text" class="form-control" id="createdOn" readonly="readonly" />
                            </div>
                        </div>
                        <div class="row" style="margin-top:10px;">
                            <div class="col-md-6">
                                <label>Reason</label>
                                <input type="text" readonly="readonly" class="form-control" id="RReason" />
                            </div>
                            <div class="col-md-6">
                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                    <label class="control-label">Reason Message</label>
                                    <input type="text" readonly="readonly" class="form-control notes-modal modal-launch-style" id="RReasonMessage" />
                                    <i class="glyphicon glyphicon-resize-full form-control-feedback notes-modal modal-launch-style"></i>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-top:10px;">
                            <div class="col-md-12">
                                <button id="editTransaction" type="button" class="btn btn-primary pull-right">Edit This Transaction <span class="glyphicon glyphicon-resize-full"></span></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5 pull-right" style="margin-top:20px;" id="searchStringTypeAhead">
                <label class="pull-right">
                    Search
                    <select id="selection4" class="form-control" style="display:inline;width:auto;" aria-controls="example">
                        <option value=" "> </option>
                        @For f As Integer = 0 To Model.transTempColumns.Count - 1
                            If Model.itemNumber <> "" And Model.transTempColumns(f) = "Item Number" Then
                                @<option selected="selected" value="@Model.transTempColumns(f)">@Model.transTempColumns(f)</option>
                            Else
                                @<option value="@Model.transTempColumns(f)">@Model.transTempColumns(f)</option>
                            End If
                        Next
                    </select>
                    By
                    <input id="searchString4" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" value="@Model.itemNumber" />
                </label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div style="overflow-y:scroll; max-height:600px">
                    <table id="reprocTransTable" style="background-color:white;" class="table table-bordered table-striped table-condensed datatable table-hover">
                        <thead>
                            <tr>
                                @For x As Integer = 0 To Model.transTempColumns.Count - 1
                                    If (Model.transTempColumns(x).Contains("Number")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Number", "#")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Transaction")) Then
                                        Dim str = Model.transTempColumns(x).Replace("Transaction", "Trans")
                                        If (str.Contains("Quantity")) Then
                                            str = str.Replace("Quantity", "Qty")
                                        End If
                                        @<th>@str</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Filename")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Filename", "File")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("File Name")) Then
                                        @<th>@Model.transTempColumns(x).Replace("File Name", "File")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Unit of Measure")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Unit of Measure", "UOM")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Required")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Required", "Req")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Sequence")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Sequence", "Seq")</th>
                                    ElseIf (Model.transTempColumns(x).Contains("Quantity")) Then
                                        @<th>@Model.transTempColumns(x).Replace("Quantity", "Qty")</th>
                                    Else
                                        @<th>@Model.transTempColumns(x)</th>
                                    End If
                                Next
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                    <div class="col-md-12">
                        <div class="col-xs-1 col-xs-offset-9">
                            <button class="dataTables_paginate btn btn-secondary" id="page20LeftRT" style="margin-bottom:10px; padding-left:0; padding-right:0; background-color:white; color:#2a6496; border-color:white; margin-left:50px; width:60px;"><< 20</button>
                        </div>
                        <div class="col-xs-2">
                            <button class="dataTables_paginate btn btn-secondary pull-right" id="page20RightRT" style="margin-bottom:10px; padding-left:0; padding-right:0; background-color:white; color:#2a6496; border-color:white; margin-right:30px; width:60px;">20 >></button>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="col-md-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-3" id="rightSidePanel" style="padding-right:15px; padding-left:0">
        <div class="col-md-12" style="padding-left:0; margin-top:10px">
            <div class="panel panel-info" id="reprocessChoicesPanel" style="margin-bottom:0;">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="btn-group">
                                <button id="MarkDisplayed" type="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
                                    Mark Table <span class="caret"></span>
                                </button>
                                <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                    <li>
                                        <a href="#" id="MarkReprocDisplay">Mark for Reprocess</a>
                                        <a href="#" id="MarkCompDisplay">Mark for Post as Complete</a>
                                        <a href="#" id="MarkHistoryDisplay">Mark to Send to History</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <h3 class="panel-title">Reprocess Choices</h3>
                        </div>
                    </div>
                </div>
                <div id="reprocessChoices" class="panel-body" style="padding:0;">
                    <div class="row" style="margin-bottom:10px;">
                        <legend class="text-center" style="margin-bottom:5px">
                            <font size="4" color="dodgerblue">Reprocess</font>
                        </legend>
                        <div class="col-md-1 col-md-offset-4">
                            <input id="reprocessCheckBox" data-id="" name="reprocess" type="checkbox" />
                        </div>
                        <div class="col-md-1 col-md-offset-1">
                            <p id="reprocessCount">0</p>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom:10px;">
                        <legend class="text-center" style="margin-bottom:5px">
                            <font size="4" color="dodgerblue">Post as Complete</font>
                        </legend>
                        <div class="col-md-1 col-md-offset-4">
                            <input id="completeCheckBox" data-id="" name="complete" type="checkbox" />
                        </div>
                        <div class="col-md-1 col-md-offset-1">
                            <p id="completeCount">0</p>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom:10px;">
                        <legend class="text-center" style="margin-bottom:5px">
                            <font size="4" color="dodgerblue">Send to Reprocess History</font>
                        </legend>
                        <div class="col-md-1 col-md-offset-4">
                            <input id="historyCheckBox" data-id="" name="hist" type="checkbox" />
                        </div>
                        <div class="col-md-1 col-md-offset-1">
                            <p id="historyCount">0</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="padding-left:0">
            <button class="btn btn-primary btn-block" id="PostTransactions">Post Transactions</button>
        </div>
        <div class="col-md-12" style="padding-left:0">
            <div style="margin-top:10px;">
                <div class="panel panel-info" id="OrdersToPost">
                    <div class="panel-heading">
                        <a data-toggle="collapse" data-target="#OTP">
                            <h3 class="panel-title">
                                Selected Orders
                                <span class="accordion-caret-down"></span>
                            </h3>

                        </a>
                    </div>
                    <div class="panel-body collapse accordion-toggle" id="OTP">
                        <div class="row">
                            <div class="col-md-12">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="active"><a href="#ReProcess" role="tab" data-toggle="tab">ReProcess <span class="label label-primary" id="reprocessTab">0</span></a></li>
                                    <li><a href="#Complete" role="tab" data-toggle="tab">Complete <span class="label label-primary" id="completeTab">0</span></a></li>
                                    <li><a href="#History" role="tab" data-toggle="tab">History <span class="label label-primary" id="historyTab">0</span></a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div class="tab-pane active" id="ReProcess">
                                        <legend class="text-center">Re-Process</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="reprocessAll">Mark All</button>
                                            </div>
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="reprocessNone">Unmark All</button>
                                            </div>
                                        </div>
                                        <div style="overflow-y:scroll; max-height:305px;">
                                            <table style="background-color:white;" class="table table-bordered table-striped table-condensed
                                                                            table-hover">
                                                <tbody id="reprocessBody"></tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="Complete">
                                        <legend class="text-center">Post as Completed</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="completeAll">Mark All</button>
                                            </div>
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="completeNone">Unmark All</button>
                                            </div>
                                        </div>

                                        <div style="overflow-y: scroll; max-height: 305px;">
                                            <table style="background-color:white;" class="table table-bordered table-striped table-condensed datatable table-hover">
                                                <tbody id="completeBody"></tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="History">
                                        <legend class="text-center">Send to Re-Process History</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="historyAll">Mark All</button>
                                            </div>
                                            <div class="col-md-6">
                                                <button type="button" class="btn btn-primary btn-block" id="historyNone">Unmark All</button>
                                            </div>
                                        </div>

                                        <div style="overflow-y: scroll; max-height: 305px;">
                                            <table style="background-color:white;" class="table table-bordered table-striped table-condensed datatable table-hover">
                                                <tbody id="historyBody"></tbody>
                                            </table>
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
</div>


@Html.Partial("~/Views/ModalPartials/NotesModalPartial.vbhtml")
@Html.Partial("ReprocessModalPartial", Model)