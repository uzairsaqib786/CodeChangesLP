<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="CarriersModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="CarriersModal" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="CarriersLabel">Carriers- Add, Delete, Edit</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" hidden="hidden" id="CarriersSender" />
                <div class="row">
                    <div class="col-md-12">
                        <div id="CarriersAlerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-9">
                                <label>Carriers</label>
                            </div>
                            <div class="col-md-offset-2 col-md-1">
                                <button type="button" class="btn btn-primary" id="CarriersAdd" data-placement="top" data-original-title="Add New Carrier"><span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="CarriersContainer">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="CarriersDismiss">Close</button>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Consolidation Manager/Scripts/Preferences/CarriersModal.js"></script>
