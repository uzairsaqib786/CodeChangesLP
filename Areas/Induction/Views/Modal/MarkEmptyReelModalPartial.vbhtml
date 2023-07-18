<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="MarkEmptyReelModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="MarkEmptyReelModal" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-6">
                        <h4 class="modal-title" id="MarkEmptyReelModal_label">Mark Reels as Empty</h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Scan Serial Number:</label>
                        <input type="text" id="EmptySerialNumberScan" autofocus class="form-control" />
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <label>Last Scanned Serial Number:</label>
                                <input type="text" id="LastSerialNumberScan" disabled="disabled" class="form-control" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="alert alert-danger" id="SerialNumberScanAlert" style="display:none" role="alert"></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Scanned Serial Numbers:</label>
                        <div style="overflow-y:scroll;max-height:600px;">
                            <table id="EmptyReelSerialNumbersTable" style="background-color:white;" class="table table-bordered table-condensed table-striped">
                                <thead>
                                    <tr>
                                        <td>Serial Number</td>
                                    </tr>
                                </thead>
                                <tbody id="EmptyReelSerialNumbersTableBody"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" id="MarkReelsAsEmptyButt">Mark Reels as Empty</button>
                <button type="button" class="btn btn-default" data-dismiss="modal" id="MarkEmptyReelModalDismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/Modal/MarkEmptyReelModal.js"></script>
