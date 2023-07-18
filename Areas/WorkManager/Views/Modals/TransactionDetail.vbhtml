<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="TransDetail_Modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="TransDetail_Label" aria-hidden="true">
    <div class="modal-dialog" style="width:99%;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="panel-title" id="TransDetail_Label">Assign Work</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="TransDetailTable">
                            <thead>
                                <tr>
                                    <td><strong>Trans. Type</strong></td>
                                    <td><strong>Batch Pick ID</strong></td>
                                    <td><strong>Order Number</strong></td>
                                    <td><strong>Line No.</strong></td>
                                    <td><strong>@Model.Alias.ItemNumber</strong></td>
                                    <td><strong>Location</strong></td>
                                    <td><strong>Description</strong></td>
                                    <td><strong>Required Date</strong></td>
                                    <td><strong>Priority</strong></td>
                                    <td><strong>U of M</strong></td>
                                    <td><strong>Lot No.</strong></td>
                                    <td><strong>Exp. Date</strong></td>
                                    <td><strong>Serial No.</strong></td>
                                    <td><strong>Revision</strong></td>
                                    <td><strong>Trans. Qty</strong></td>
                                    <td><strong>Tote ID</strong></td>
                                    <td><strong>Tote No.</strong></td>
                                    <td><strong>Whse</strong></td>
                                    <td><strong>Zone</strong></td>
                                    <td><strong>Carousel</strong></td>
                                    <td><strong>Row</strong></td>
                                    <td><strong>Shelf</strong></td>
                                    <td><strong>Bin</strong></td>
                                    <td><strong>Inv Map ID</strong></td>
                                    <td><strong>Notes</strong></td>
                                    <td><strong>Label</strong></td>
                                    <td><strong>@Model.Alias.UserFields(0)</strong></td>
                                    <td><strong>@Model.Alias.UserFields(1)</strong></td>
                                    @For x As Integer = 3 To 10
                                        @<td><strong>User Field @x</strong></td>
                                    Next
                                    <td><strong>Cell</strong></td>
                                    <td><strong>Host Trans. ID</strong></td>
                                    <td><strong>Emergency</strong></td>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/WorkManager/Scripts/Modals/TransDetail.js"></script> 