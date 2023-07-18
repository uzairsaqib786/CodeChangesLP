<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Set Column Sequence"
    ViewData("PageName") = "&nbsp; | &nbsp; Column Sequence"
    If Model.columns(0).IndexOf("Golden Zone") <> -1 Then
        model.columns(0)(model.columns(0).IndexOf("Golden Zone")) = "Velocity Code"
    End If
    If model.columns(0).IndexOf("Location ID") <> -1 Then
        model.columns(0)(model.columns(0).IndexOf("Location ID")) = "Alternate Light"
    End If
End Code

<div class="container-fluid">
    <!-- Handle user coming to page with identifying view (OT or TH or Inventory Map) and reload for buttons that call a refresh -->
    <form action="/Admin/ColumnSequence" method="post" id="reload">
        <input name="table" type="hidden" id="tableName" value="@model.columns(2)(0)" />
        <input type="hidden" id="tableNum" value="@model.columns(2)(1)"/>
    </form>
    @If Model.columns(4)(0) = "Open Transactions" Or Model.columns(4)(0) = "Transaction History" Or Model.columns(4)(0) = "Open Transactions Temp" Or Model.columns(4)(0) = "ReProcessed" Then
        @<form action="/Transactions" method="get" id="backToPage">
            <input type="hidden" name="App" value="@Model.app" />
            <input type="hidden" name="viewToShow" value="@Model.columns(2)(1)" />
            @If Model.columns(4)(0) = "Open Transactions" Then
                @<input id="sDateOpen" name="sDateOpen" type="hidden" value="@Model.columns(3)(0)">
                @<input id="eDateOpen" name="eDateOpen" type="hidden" value="@Model.columns(3)(1)">
                @<input id="orderNumberOpen" name="orderNumberOpen" type="hidden" value="@Model.columns(3)(2)">
                @<input id="toteIDOpen" name="toteIDOpen" type="hidden" value="@Model.columns(3)(3)">
                @<input id="transactType" name="transactType" type="hidden" value="@Model.columns(3)(4)">
                @<input id="transactStat" name="transactStat" type="hidden" value="@Model.columns(3)(5)">
            ElseIf Model.columns(4)(0) = "Transaction History" Then
                @<input id="sDateTrans" name="sDateTrans" type="hidden" value="@Model.columns(3)(0)">
                @<input id="eDateTrans" name="eDateTrans" type="hidden" value="@Model.columns(3)(1)">
                @<input id="orderNumberTrans" name="orderNumberTrans" type="hidden" value="@Model.columns(3)(2)">
            ElseIf Model.columns(4)(0) = "Open Transactions Temp" Then
                @<input id="RecToView" name="RecToView" type="hidden" value="@Model.columns(3)(0)">
                @<input id="ReasFilt" name="ReasFilt" type="hidden" value="@Model.columns(3)(1)">
                @<input id="OrderNumTemp" name="OrderNumTemp" type="hidden" value="@Model.columns(3)(2)">
                @<input id="ItemNumTemp" name="ItemNumTemp" type="hidden" value="@Model.columns(3)(3)">
            End If
    </form>
    ElseIf model.columns(4)(0) = "Inventory Map" Then
        @<form action="/InventoryMap" id="backToPage">
            <input type="hidden" name="App" value="@Model.app" />
        </form>
    ElseIf Model.columns(4)(0) = "Order Manager" Then
        @<form action="/OM/OrderManager" id="backToPage"></form>
    ElseIf model.columns(4)(0) = "Order Manager Create" Then
        @<form action="/OM/OrderManager/CreateOrders" id="backToPage"></form>
    End If
    
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Selectable Columns</h3>
                        </div>
                        <div class="panel-body">
                            <!-- List off all the columns in the table so they can be ordered (split into two rows for readability) -->
                            <div class="row">
                                <div class="col-md-4 col-md-offset-2">
                                    <ul id="draggablePanelList" class="list-unstyled btn-group-vertical together">
                                        <li class="static btn btn-sm btn-black">Unordered Columns</li>
                                        @For x As Integer = 0 To CInt(model.columns(0).Count / 2)
                                            If Not model.columns(1).Contains(model.columns(0)(x)) Then
                                                @<li class="btn btn-sm btn-primary">@model.columns(0)(x)</li>
                                            End If
                                        Next
                                    </ul>
                                </div>
                                <div class="col-md-4 col-md-offset-1 ">
                                    <ul id="draggableList" class="list-unstyled btn-group-vertical together ">
                                        <li class="static btn btn-sm btn-black">Unordered Columns</li> 
                                        @If Model.columns(4)(0) = "Open Transactions Temp" Then
                                            For x As Integer = CInt(Model.columns(0).Count / 2) + 1 To Model.columns(0).Count - 2
                                                If Not Model.columns(1).Contains(Model.columns(0)(x)) Then
                                                    @<li class="btn btn-sm btn-primary">@Model.columns(0)(x)</li>
                                                End If
                                            Next
                                        Else
                                            @For x As Integer = CInt(Model.columns(0).Count / 2) + 1 To Model.columns(0).Count - 1
                                                If Not Model.columns(1).Contains(Model.columns(0)(x)) Then
                                                    @<li class="btn btn-sm btn-primary">@Model.columns(0)(x)</li>
                                                End If
                                            Next
                                        End If
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">Current Default Column Sequence</h3>
                        </div>
                        <!-- Displays the default column sequence as it is being set or as user default (blank if no user defined) on page load -->
                        <div class="panel-body" >
                            <div class="row">
                                <div class="col-md-4">
                                    <ul style="min-height:100px;" id="defaultList" class="list-unstyled btn-group-vertical together">
                                        <li class="static btn btn-sm btn-black">Default Column Sequence</li>
                                        @For x As Integer = 0 To Model.columns(1).Count - 1
                                            @<li class="btn btn-sm btn-primary">@Model.columns(1)(x)</li>
                                        Next
                                    </ul>
                                </div>
                                <div class="col-md-4 btn-group-vertical">
                                    <button class="btn btn-primary" id="saveDefaultColumnSequence"><u>S</u>ave Selected as Default</button>
                                    <button class="btn btn-primary" id="restoreDefaultColumnSequence"><u>R</u>estore to Current Default</button>
                                    @If (Model.columns(2)(0) = "Open Transactions Temp") Then
                                        @<Button Class="btn btn-warning" id="backToTransactions"><u>B</u>ack to Reprocess Transactions</button>
                                    ElseIf (Model.columns(2)(0) = "ReProcessed") Then
                                        @<Button Class="btn btn-warning" id="backToTransactions"><u>B</u>ack to Reprocessed Transactions</Button>
                                    Else
                                        @<Button Class="btn btn-warning" id="backToTransactions"><u>B</u>ack to @Model.columns(2)(0)</button>
                                    End If
                                </div>
                                <div class="col-md-4 btn-group-vertical">
                                    <button class="btn btn-primary" id="autofillColumns"><u>A</u>utofill Columns</button>
                                    <button class="btn btn-danger" id="delTJColSeq"><u>C</u>lear All</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<!--<script src="~/Scripts/jquery-sortable.js"></script>  was causing problem with hubs for some unknown reason and page works without import (again don't know why)-->
<script src="~/Scripts/jquery-ui.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Column Sequence/ColSequence.js"></script>
<script src="~/Areas/ICSAdmin/Scripts/Column Sequence/sorting.js"></script>