﻿<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2021-->

<div class="modal fade" id="BlossomModal" tabindex="-1" data-backdrop="static" role="dialog" aria-hidden="true">
    <div class="modal-dialog" style="width:80%">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#428bca; color:white">
                <h4 class="modal-title" id="BlossomModalTitle"></h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-4">
                        <input type="text" style="font-size:x-large;" class="form-control" id="BlossToteIDScan" placeholder="New Tote ID" />
                    </div>
                    <div class="col-md-4">
                        <Button Class="btn btn-success" style="font-size:x-large" id="SubmitBlossom" disabled="disabled">Submit Blossom</Button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div Class="table-responsive">
                            <table Class="table table-striped text-center">
                                <thead>
                                    <tr>
                                        <th style="font-size:x-large;text-align:center">Item Number</th>
                                        <th style="font-size:x-large;text-align:center">Trans Quantity</th>
                                        <th style="font-size:x-large;text-align:center">Picked Quantity</th>
                                        <th style="font-size:x-large;text-align:center" id="QtyColTitle"></th>
                                    </tr>
                                </thead>
                                <tbody id="blossomdata"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" id="BlossomModalDismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Markout/Scripts/Markout/BlossomModal.js"></script>
<script src="~/Areas/Markout/Scripts/Markout/Markout.js"></script>
