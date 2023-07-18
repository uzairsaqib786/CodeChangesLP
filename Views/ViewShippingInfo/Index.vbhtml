<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "ViewShippingInfo"
    ViewData("PageName") = "&nbsp; | &nbsp; View Shipping Info"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model.app)
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Order Information
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <input type="text" id="shipInfoOrderNum" disabled="disabled" class="form-control" value="@Model.orderNumber" />
                        </div>
                        <div class="col-md-offset-4 col-md-2">
                            <button type="button" class="btn btn-primary" id="packingList">Packing List</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Packing Information
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="packInfoTable">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Item Number</th>
                                        <th>Line Number</th>
                                        <th>Tote ID</th>
                                        <th>Order Quantity</th>
                                        <th>Picked Quantity</th>
                                        <th>Container ID</th>
                                        <th>Ship Quantity</th>
                                        <th>Completed Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each tr As List(Of String) In Model.packTable
                                        @<tr>
                                            @For Each td As String In tr
                                                @<td>@td</td>
                                            Next
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Shipping Information
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="shipInfoTable">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Container ID</th>
                                        <th>Carrier Name</th>
                                        <th>Tracking Number</th>
                                        <th>Freight</th>
                                        <th>Freight1</th>
                                        <th>Freight2</th>
                                        <th>Weight</th>
                                        <th>Length</th>
                                        <th>Width</th>
                                        <th>Height</th>
                                        <th>Cube</th>
                                        <th>Completed Time</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @For Each tr As List(Of String) In Model.shipTable
                                        @<tr>
                                            @For Each td As String In tr
                                                @<td>@td</td>
                                            Next
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/ViewShippingInfo/ViewShippingInfo.js"></script>
