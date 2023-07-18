<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
@Code
    ViewData("Title") = "Transaction Journal"
    ViewData("PageName") = "| " & Model.toShow
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.app)
    ViewData("App") = Model.app
End Code
<div class="container-fluid">
    <form action="/Admin/ColumnSequence" method="post" id="transDefaultForm">
        <input name="table" type="hidden" value="Transaction History" />
        <input id="sDateTrans" name="sDateTrans" type="hidden">
        <input id="eDateTrans" name="eDateTrans" type="hidden">
        <input id="orderNumberTrans" name="orderNumberTrans" type="hidden">
        <input name="App" value="@Model.app" type="hidden" />
    </form>
    <form action="/Admin/ColumnSequence" method="post" id="openDefaultForm">
        <input name="table" type="hidden" value="Open Transactions">
        <input name="App" type="hidden" value="@Model.app">
        <input id="sDateOpen" name="sDateOpen" type="hidden">
        <input id="eDateOpen" name="eDateOpen" type="hidden">
        <input id="orderNumberOpen" name="orderNumberOpen" type="hidden">
        <input id="toteIDOpen" name="toteIDOpen" type="hidden">
        <input id="transactType" name="transactType" type="hidden">
        <input id="transactStat" name="transactStat" type="hidden">
    </form>
    <form action="/Admin/ColumnSequence" method="post" id="reprocDefaultForm">
        <input name="table" type="hidden" value="Open Transactions Temp">
        <input name="App" type="hidden" value="@Model.app">
        <input id="RecToView" name="RecToView" type="hidden">
        <input id="ReasFilt" name="ReasFilt" type="hidden">
        <input id="OrderNumTemp" name="OrderNumTemp" type="hidden">
        <input id="ItemNumTemp" name="ItemNumTemp" type="hidden">
    </form>
    <form action="/Admin/ColumnSequence" method="post" id="reprocedDefaultForm">
        <input name="table" type="hidden" value="ReProcessed">
        <input name="App" type="hidden" value="@Model.app">
    </form>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div id="alerts"></div>
                    </div>
                    <ul class="nav nav-tabs" role="tablist">
                        @If PickPro_Web.OrderStatus.isOrderStatusOnly(Model.app) Then
                            @<li class="active"><a id="orderStatusView" href="#orderStatusTab" role="tab" data-toggle="tab">Order Status</a></li>
                        Else
                            @If Model.app <> "IE" Then
                                @If permissions.Contains("Order Status") Then
                                    @If Model.toShow = "Order Status" Then
                                        @<li class="active"><a id="orderStatusView" href="#orderStatusTab" role="tab" data-toggle="tab">Order Status</a></li>
                                    Else
                                        @<li><a id="orderStatusView" href="#orderStatusTab" role="tab" data-toggle="tab">Order Status</a></li>
                                    End If
                                End If
                            End If
                            @If permissions.Contains("Transaction Journal") Or permissions.Contains("Admin Transaction Journal") Then
                                @If Model.toShow = "Open Transactions" Then
                                    @<li class="active"><a id="openView" href="#openTransTab" role="tab" data-toggle="tab">Open Transactions</a></li>
                                Else
                                    @<li><a id="openView" href="#openTransTab" role="tab" data-toggle="tab">Open Transactions</a></li>
                                End If
                                @If Model.toShow = "Transaction History" Then
                                    @<li class="active"><a id="histView" href="#transHistTab" role="tab" data-toggle="tab">Transaction History</a></li>
                                Else
                                    @<li><a id="histView" href="#transHistTab" role="tab" data-toggle="tab">Transaction History</a></li>
                                End If
                                @If Model.app <> "IM" Then
                                    @If Model.toShow = "Reprocess Transactions" Then
                                        @<li class="active"><a id="reprocView" href="#reprocTab" role="tab" data-toggle="tab">Reprocess Transactions</a></li>
                                    Else
                                        @<li><a id="reprocView" href="#reprocTab" role="tab" data-toggle="tab">Reprocess Transactions</a></li>
                                    End If

                                    @If Model.toShow = "Reprocessed" Then
                                        @<li class="active"><a id="reprocedView" href="#reprocedTab" data-toggle="tab" role="tab">Reprocessed Transactions</a></li>
                                    Else
                                        @<li><a id="reprocedView" href="#reprocedTab" data-toggle="tab" role="tab">Reprocessed Transactions</a></li>
                                    End If
                                End If
                            End If
                        End If
                    </ul>
                    <div class="tab-content">
                        @If (Model.toShow = "Order Status" Or PickPro_Web.OrderStatus.isOrderStatusOnly(Model.app)) Then
                            @<div class="tab-pane active" id="orderStatusTab" style="margin-top:5px;">
                                @Html.Partial("OrderStatusPartial", Model)
                            </div>
                        Else
                            @<div class="tab-pane" id="orderStatusTab" style="margin-top:5px;">
                                @Html.Partial("OrderStatusPartial", Model)
                            </div>
                        End If
                        @If Model.toShow = "Open Transactions" Then
                            @<div class="tab-pane active" id="openTransTab" style="margin-top:5px;">
                                @Html.Partial("OpenTransPartial", Model)
                            </div>
                        Else
                            @<div class="tab-pane" id="openTransTab" style="margin-top:5px;">
                                @Html.Partial("OpenTransPartial", Model)
                            </div>
                        End If
                        @If Model.toShow = "Transaction History" Then
                            @<div class="tab-pane active" id="transHistTab" style="margin-top:5px;">
                                @Html.Partial("TransHistoryPartial", Model)
                            </div>
                        Else
                            @<div class="tab-pane" id="transHistTab" style="margin-top:5px;">
                                @Html.Partial("TransHistoryPartial", Model)
                            </div>
                        End If
                        @If Model.toShow = "Reprocess Transactions" Then
                            @<div class="tab-pane active" id="reprocTab" style="margin-top:5px;">
                                @Html.Partial("ReprocessTransPartial", Model)
                            </div>
                        Else
                            @<div class="tab-pane" id="reprocTab" style="margin-top:5px;">
                                @Html.Partial("ReprocessTransPartial", Model)
                            </div>
                        End If
                        @If Model.toShow = "Reprocessed Transactions" Then
                            @<div class="tab-pane active" id="reprocedTab" style="margin-top:5px;">
                                @Html.Partial("ReprocessedTransPartial", Model)
                            </div>
                        Else
                            @<div class="tab-pane" id="reprocedTab" style="margin-top:5px;">
                                @Html.Partial("ReprocessedTransPartial", Model)
                            </div>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<select hidden="hidden" id="selection1">
    <option>Completed Date</option>
</select>
<select hidden="hidden" id="selection2">
    <option>Completed Date</option>
</select>
<select hidden="hidden" id="selection3">
    <option>Completed Date</option>
</select>

<script src="~/Scripts/Transaction Journal/Transactions.js"></script>
<script src="~/Scripts/Transaction Journal/orderStatus.js"></script>
<script src="~/Scripts/Transaction Journal/transactionHistory.js"></script>
<script src="~/Scripts/Transaction Journal/openTransactions.js"></script>
<script src="~/Scripts/Transaction Journal/keyShortcutsTransactions.js"></script>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<!--<script src="~/Scripts/ColReorderWithResize.js"></script>-->
<script src="~/Scripts/Transaction Journal/OpenTransactionsHub.js"></script>
<script src="~/Scripts/Transaction Journal/OrderStatusHub.js"></script>
<script src="~/Scripts/Transaction Journal/Reprocess/ReprocessTransactions.js"></script>
<script src="~/Scripts/Transaction%20Journal/OpenTransFilters.js"></script>
<script src="~/Scripts/Transaction%20Journal/TransHistFilters.js"></script>
<script src="~/Scripts/Transaction%20Journal/OrderStatFilters.js"></script>
<script src="~/Scripts/Transaction Journal/Reprocessed.js"></script>
<script>
    $(document).ready(function () {
        var page = '@Model.toShow'
        if (page == 'Order Status') {
            $('#ordernumFilterOrder').focus().tooltip('hide');
        }
    })
</script>
