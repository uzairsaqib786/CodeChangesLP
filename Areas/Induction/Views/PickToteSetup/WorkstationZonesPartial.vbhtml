<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="WSZonesModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="WSZonesModal" aria-hidden="true">
    <div class="modal-dialog" style="width:50%">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="WSZonesModalTitle">
                    Workstation Zones
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-9">
                        <label>Add Zone:</label>
                        <input type="text" class="form-control typeahead" id="TAZone"/>
                    </div>
                    <div class="col-md-3">
                        <button type="button" class="btn btn-danger" id="ClearAllZones" data-toggle="tooltip" data-placement="top">Clear All Zones</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div id="WSZoneContainer">

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-offset-10 col-md-2">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="WSZonesModalDismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/PickToteSetup/WorkstationZonesPartial.js"></script>
