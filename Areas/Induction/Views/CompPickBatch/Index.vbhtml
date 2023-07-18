<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Complete Pick Batch"
    ViewData("PageName") = "| Complete Pick Batch"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Display Batch
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label>Batch ID</label>
                            <input type="text" id="BatchPickID" placeholder="Batch ID" class="form-control" />
                        </div>
                        <div class="col-md-6" id="ToteCol" style="display:none" >
                            <label>Tote ID</label>
                            <input type="text" id="ToteID" placeholder="Tote ID" class="form-control"/>
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-3">
                            <button type="button" class="btn btn-danger btn-block" id="ClearScreen">Clear Screen</button>
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="btn btn-success btn-block" id="CompleteBatch" disabled>Complete Batch</button>
                        </div>
                        <div class="col-md-3">
                            <button type="button" class="btn btn-warning btn-block" id="BlossTote" style="display:none" disabled>Blossom Tote</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Batch Info
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <button type="button" class="btn btn-success btn-block" id="CompleteTrans" disabled>Complete Transaction</button>
                        </div>
                        <div class="col-md-4">
                            <button type="button" class="btn btn-warning btn-block" id="ShortTrans" disabled>Short Transaction</button>
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            <div style="overflow-y:scroll;max-height:600px;">
                                <table id="PickBatchTransTable" style="background-color:white;" class="table table-hover datatable table-bordered table-striped">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Order Number</th>
                                            <th>Tote ID</th>
                                            <th>Item Number</th>
                                            <th>Description</th>
                                            <th>Transaction Quantity</th>
                                            <th>Location</th>
                                            <th>Zone</th>
                                            <th>Carousel</th>
                                            <th>Row</th>
                                            <th>Shelf</th>
                                            <th>Bin</th>
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
    <input type="text" hidden value="@Model.Prefs.ShortMehtod" id="ShortMethod" />
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/Induction/Scripts/CompPickBatch/CompPickBatch.js"></script>
@Html.Partial("~/Areas/Induction/Views/CompPickBatch/ShortTransPartial.vbhtml")
@Html.Partial("~/Areas/Induction/Views/CompPickBatch/BlossomPartial.vbhtml")