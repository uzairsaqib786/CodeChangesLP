<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
@code
    Dim dateReturn As DateTime
    Dim edateReturn As DateTime
    Dim sreturnString As String = ""
    Dim ereturnString As String = ""
    If Model.defaultValues.Count() > 0 And Model.toShow = "Open Transactions" Then

        If (Model.defaultValues(0) <> "") Then
            dateReturn = DateTime.Parse(Model.defaultValues(0))
            sreturnString = dateReturn.ToString("MM/dd/yyyy")
        Else
            sreturnString = Now.AddYears(-50).ToString("MM/dd/yyyy")
        End If
        If Model.defaultValues(1) <> "" Then
            edateReturn = DateTime.Parse(Model.defaultValues(1))
            ereturnString = edateReturn.ToString("MM/dd/yyyy")
        Else
            ereturnString = Now.ToString("MM/dd/yyyy")
        End If
    Else
        sreturnString = Now.AddYears(-50).ToString("MM/dd/yyyy")
        ereturnString = Now.ToString("MM/dd/yyyy")
    End If
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<div id="OpenView">
    <ul id="contextMenu" class="dropdown-menu" role="menu" style="display:none">
        <li><a tabindex="-1" href="#">Delete Item</a></li>
        <li><a tabindex="-1" href="#">Delete All in Order by Transaction Type</a></li>
    </ul>
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-info">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">Filters</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Import Date (Start)</label>
                                <input type = "text" onchange="$('#sDateFilterOpenCopy').val(this.value)" oninput="$('#sDateFilterOpenCopy').val(this.value)" Class="form-control input-sm date-picker" id="sDateFilterOpen" value="@sreturnString" maxlength="50" />
                                <input type="text" id="sDateFilterOpenCopy" value="@sreturnString" style="display:none;" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>End Date</label>
                                <input type = "text" onchange="$('#eDateFilterOpenCopy').val(this.value)" oninput="$('#sDateFilterOpenCopy').val(this.value)" Class="form-control input-sm date-picker" id="eDateFilterOpen" value="@ereturnString" maxlength="50" />
                                <input type = "text" id="eDateFilterOpenCopy" value="@ereturnString" style="display:none;" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>By Status</label>
                                <select id="statusFilterOpen" class="form-control input-sm">
                                    @If Model.defaultValues(5) = "All Transactions"  Then
                                        @<option selected="selected">All Transactions</option>
                                    Else
                                        @<option>All Transactions</option>
                                    End If
                                    @If Model.toShow = "Open Transactions" And Model.defaultValues(5) = "Open Transactions" Then
                                        @<option selected="selected">Open Transactions</option>
                                    Else
                                        @<option>Open Transactions</option>
                                    End If
                                    @If Model.toShow = "Open Transactions" And Model.defaultValues(5) = "Completed Transactions" Then
                                        @<option selected="selected">Completed Transactions</option>
                                    Else
                                        @<option>Completed Transactions</option>
                                    End If
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Order Number</label>
                                <div id="openTransTypeAhead" class="form-group">
                                    <!--Textbox for the order number input-->
                                    @If Model.toShow <> "Open Transactions" Then
                                         @<input type = "text" oninput="$('#orderNumberFilterOpenCopy').val(this.val)" Class="form-control input-sm typeahead" id="orderNumberFilterOpen" placeholder="Order Number" maxlength="50">
                                    Else
                                        @<input type = "text" oninput="$('#orderNumberFilterOpenCopy').val(this.val)" Class="form-control input-sm typeahead" id="orderNumberFilterOpen" placeholder="Order Number" value="@Model.defaultValues(2)" maxlength="50">
                                    End If
                                    <input type = "text" id="orderNumberFilterOpenCopy" style="display:none;" />
                                </div>
                            </div>
                        </div>
                        <div Class="col-md-4">
                            <div Class="form-group">
                                <Label> Tote ID</label>
                                @If Model.toShow <> "Open Transactions" Then
                                    @<input style="display:none;" id="toteIDFilterOpenCopy" type="text" Class="form-control input-sm" placeholder="Tote ID" maxlength="50" />
                                    @<input id="toteIDFilterOpen" type="text" Class="form-control input-sm" placeholder="Tote ID" maxlength="50" />
                                Else
                                    @<input style="display:none;" id="toteIDFilterOpenCopy" type="text" Class="form-control input-sm" placeholder="Tote ID" value="@Model.defaultValues(3)" maxlength="50" />
                                    @<input id = "toteIDFilterOpen" type="text" Class="form-control input-sm" placeholder="Tote ID" value="@Model.defaultValues(3)" maxlength="50" />
                                End If
                            </div>
                        </div>
                        <div Class="col-md-4">
                            <div Class="form-group">
                                <Label> By Trans. Type</label>
                                <select id = "transFilterOpen" Class="form-control input-sm">
                                    @If Model.defaultValues(4) = "All Transactions" Then
                                        @<option selected="selected">All Transactions</option>
                                    Else
                                        @<option>All Transactions</option>
                                    End If
                                    @If Model.defaultValues(4) = "Adjustment" Then
                                        @<option selected="selected">Adjustment</option>
                                    Else
                                        @<option>Adjustment</option>
                                    End If
                                    @If Model.defaultValues(4) = "Complete" Then
                                        @<option selected="selected">Complete</option>
                                    Else
                                        @<option>Complete</option>
                                    End If
                                    @If Model.defaultValues(4) = "Count" Then
                                        @<option selected="selected">Count</option>
                                    Else
                                        @<option>Count</option>
                                    End If
                                    @If Model.defaultValues(4) = "Location Change" Then
                                        @<option selected="selected">Location Change</option>
                                    Else
                                        @<option>Location Change</option>
                                    End If
                                    @If Model.defaultValues(4) = "Pick" Then
                                        @<option selected="selected">Pick</option>
                                    Else
                                        @<option>Pick</option>
                                    End If
                                    @If Model.defaultValues(4) = "Put Away" Then
                                        @<option selected="selected">Put Away</option>
                                    Else
                                        @<option>Put Away</option>
                                    End If
                                    @If Model.defaultValues(4) = "Shipping" Then
                                        @<option selected="selected">Shipping</option>
                                    Else
                                        @<option>Shipping</option>
                                    End If
                                    @If Model.defaultValues(4) = "Shipping Complete" Then
                                        @<option selected="selected">Shipping Complete</option>
                                    Else
                                        @<option>Shipping Complete</option>
                                    End If
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <button id="resetDateOpen" class="btn btn-primary col-md-12">Reset to <u>T</u>oday's Date</button>
                        </div>
                        @If Model.app = "Admin" Then
                            @<div class="col-md-4">
                                <a href="/Admin/holdTransactions" class="btn btn-primary col-md-12">Place Trans. on Hold</a>
                            </div>
                        End If
                    </div>
                </div>
            </div>
        </div>
        @If permissions.Contains("Transaction Journal Delete") And Model.app = "Admin" Then
            @<div class="col-md-4">
                <div class="panel panel-danger">
                    <div class="panel-heading clearfix">
                        <h4 class="panel-title pull-left">Delete Transaction(s)</h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Selected Order Number</label>
                                    <input type="text" class="form-control" style="background-color:white;" disabled="disabled"  id="selectedOrderNumber" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Selected Transaction Type</label>
                                    <input type="text" class="form-control" style="background-color:white;" disabled="disabled"  id="selectedTransType" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown">
                                        Delete <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu" role="menu">
                                        <li class="text-danger"><a href="#" id="deleteSelected">Selected ONLY</a></li>
                                        <li class="text-danger"><a href="#" id="deleteByTransType">ALL by Transaction Type</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        End If
    </div>
    <!-- Added select for searching by column -->
    <div class="row">
        <div class="col-xs-6">
            <label># Entries</label>
            <select id="pageLength2" class="form-control" style="width:auto; display:inline;">
                <option>10</option>
                <option>15</option>
                <option>20</option>
                <option>25</option>
                <option>50</option>
                <option>100</option>
            </select>
            <button id="setDefaultOpen" type="button" data-toggle="tooltip" data-placement="top" title="Set Column Sequence" class="btn btn-primary">
                <span class="glyphicon glyphicon-list"></span>
            </button>
            <button id="cycleCountReport" class="btn btn-primary Print-Report" data-toggle="tooltip" data-placement="top" data-original-title="Print Cycle Count Report">
                <span class="glyphicon glyphicon-print"></span>
            </button>
            <button id="previewCycleCountReport" class="btn btn-primary Print-Report" data-toggle="tooltip" data-placement="top" data-original-title="Preview (Top ~50 pages only)">
                <span class="glyphicon glyphicon-list-alt"></span>
            </button>
            <div class="btn-group">
                <button id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown" disabled="disabled"><span class="glyphicon glyphicon-share"></span> <span class="caret"></span></button>
                <ul class="dropdown-menu" role="menu">
                    <li><a id="goToInvMaster">View Item in Inventory Master</a></li>
                    <li><a id="goToOrderStatus">View Order in Order Status</a></li>
                </ul>
            </div>
            @If permissions.Contains("Transaction Journal Send To History") And Model.app = "Admin" Then
                @<button id="sendCompletedToHist" class="btn btn-primary">Send Completed to <u>H</u>istory</button>
            End If
        </div>
        <div class="col-xs-6" id="searchStringTypeAhead">
            <label class="pull-right">
                Search
                <select id="selection2" class="form-control" style="display:inline;width:auto;" aria-controls="example">
                    <option value=" "> </option>
                    @For f As Integer = 0 To Model.openColumns.Count - 1
                        If Model.itemNumber <> "" And Model.openColumns(f) = "Item Number" Then
                        @<option selected="selected" value="@Model.openColumns(f)">@Model.openColumns(f)</option>
                        Else
                        @<option value="@Model.openColumns(f)">@Model.openColumns(f)</option>
                        End If
                    Next
                </select>
                By
                <input id="searchString2" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" value="@Model.itemNumber" />
            </label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <table id="openTransTable" class="table table-hover table-bordered table-striped table-condensed" cellspacing="0" role="grid" style="background-color:white;">
                <thead>
                    <tr>
                        @For x As Integer = 0 To Model.openColumns.Count - 1
                            If (Model.openColumns(x).Contains("Number")) Then
                                @<th>@Model.openColumns(x).Replace("Number", "#")</th>
                            ElseIf (Model.openColumns(x).Contains("Transaction")) Then
                                Dim str = Model.openColumns(x).Replace("Transaction", "Trans")
                                If (str.Contains("Quantity")) Then
                                    str = str.Replace("Quantity", "Qty")
                                End If
                                @<th>@str</th>
                            ElseIf (Model.openColumns(x).Contains("Filename")) Then
                                @<th>@Model.openColumns(x).Replace("Filename", "File")</th>
                            ElseIf (Model.openColumns(x).Contains("File Name")) Then
                                @<th>@Model.openColumns(x).Replace("File Name", "File")</th>
                            ElseIf (Model.openColumns(x).Contains("Unit of Measure")) Then
                                @<th>@Model.openColumns(x).Replace("Unit of Measure", "UOM")</th>
                            ElseIf (Model.openColumns(x).Contains("Required")) Then
                                @<th>@Model.openColumns(x).Replace("Required", "Req")</th>
                            ElseIf (Model.openColumns(x).Contains("Sequence")) Then
                                @<th>@Model.openColumns(x).Replace("Sequence", "Seq")</th>
                            ElseIf (Model.openColumns(x).Contains("Quantity")) Then
                                @<th>@Model.openColumns(x).Replace("Quantity", "Qty")</th>
                            Else
                                @<th>@Model.openColumns(x)</th>
                            End If
                        Next
                    </tr>
                </thead>
            </table>
        </div>
        <div>
            <div class="col-md-12">
                <div class="col-xs-1 col-xs-offset-10">
                    <button class="dataTables_paginate btn btn-secondary" id="page20LeftOT" style="margin-bottom:10px; background-color:white; color:#2a6496; border-color:white;"><< 20</button>
                </div>
                <div class="col-xs-1">
                    <button class="dataTables_paginate btn btn-secondary pull-right" id="page20RightOT" style="margin-bottom:10px; margin-right:30px; background-color:white; color:#2a6496; border-color:white;">20 >></button>
                </div>
            </div>
        </div>

        <input type="hidden" value="@Model.openColumns.IndexOf("Order Number")" id="orderNumber" />
        <input type="hidden" value="@Model.openColumns.IndexOf("Transaction Type")" id="transactionType" />
        <input type="hidden" value="@Model.openColumns.IndexOf("ID")" id="transID" />
        <input type="hidden" value="@Model.openColumns.IndexOf("Item Number")" id="itemNumber" />
        <input type="hidden" value="@Model.openColumns.IndexOf("Line Number")" id="lineNumber" />
    </div>
</div>
<form action="/InventoryMaster" method="GET" id="submitInv">
    <input type="hidden" id="ItemNumber" name="ItemNumber" />
    <input type="hidden" name="App" value="@Model.app" />
</form>