<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="InZoneOrderModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="InZoneOrderModal" aria-hidden="true">
    <div class="modal-dialog" style="width:99%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="PickToteManLabel">
                    Orders
                </h4>
            </div>
            <div class="modal-body">
                <div class="row" id="QueryOrderLabel">
                    <div class="col-md-12" style="text-align:center;">
                        <label style="font-size:200%;">Loading Orders<i class="fa fa-circle-o-notch fa-spin"></i></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" id="FillOrders">Fill With Top Orders</button>
                        <button type="button" class="btn btn-danger" id="ClearAll">Un-Select All Orders</button>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-3">
                        <div style="overflow-y:scroll;max-height:600px;">
                            <table id="PickToteOrdersTable" style="background-color:white;" class="table table-bordered table-condensed table-striped">
                                <thead>
                                    <tr>
                                        <td>Order Number</td>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-9" id="SelectTransactions">
                        <div style="overflow-y:scroll;max-height:600px;">
                            <table id="PickToteTransTable" style="background-color:white;" class="table table-bordered table-condensed datatable table-striped">
                                <thead>
                                    <tr>
                                        <th>Order Number</th>
                                        <th>Item Number</th>
                                        <th>Transaction Quantity</th>
                                        <th>Location</th>
                                        <th>Completed Quantity</th>
                                        <th>Description</th>
                                        <th>Import Date</th>
                                        <th>Priority</th>
                                        <th>Required Date</th>
                                        <th>Line Number</th>
                                        <th>Line Sequence</th>
                                        <th>Serial Number</th>
                                        <th>Lot Number</th>
                                        <th>Expiration Date</th>
                                        <th>Completed Date</th>
                                        <th>Completed By</th>
                                        <th>Batch Pick ID</th>
                                        <th>Unit of Measure</th>
                                        <th>User Field1</th>
                                        <th>User Field2</th>
                                        <th>User Field3</th>
                                        <th>User Field4</th>
                                        <th>User Field5</th>
                                        <th>User Field6</th>
                                        <th>User Field7</th>
                                        <th>User Field8</th>
                                        <th>User Field9</th>
                                        <th>User Field10</th>
                                        <th>Revision</th>
                                        <th>Tote ID</th>
                                        <th>Tote Number</th>
                                        <th>Cell</th>
                                        <th>Host Transaction ID</th>
                                        <th>ID</th>
                                        <th>Zone</th>
                                        <th>Carousel</th>
                                        <th>Row</th>
                                        <th>Shelf</th>
                                        <th>Bin</th>
                                        <th>Warehouse</th>
                                        <th>Inv Map ID</th>
                                        <th>Import By</th>
                                        <th>Import Filename</th>
                                        <th>Notes</th>
                                        <th>Emergency</th>
                                        <th>Master Record</th>
                                        <th>Master Record ID</th>
                                        <th>Export Batch ID</th>
                                        <th>Export Date</th>
                                        <th>Exported By</th>
                                        <th>Status Code</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" data-dismiss="modal" id="InZoneModalDismiss">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/InZoneOrdersPartial.js"></script>