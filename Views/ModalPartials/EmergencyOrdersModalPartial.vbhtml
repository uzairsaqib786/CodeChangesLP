<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="emergency_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="emergency_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="emergency_label">Emergency Orders</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="padding-bottom:5px;">
                    <div class="col-md-12">
                        There are Emergency Order(s) to be picked from this workstation's zone(s).  Please return to the order selection screen and complete the emergency transactions.  
                        This alert will continue to appear until the Emergency Transactions are completed or the Emergency field is unchecked for the transactions.
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Carousel(s):</label>
                        <div id="emergency_car">-</div>
                    </div>
                    <div class="col-md-6">
                        <label>Zone(s):</label>
                        <div id="emergency_zone">-</div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="emergency_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="emergency_submit">Go To Order Selection Screen</button>
            </div>
        </div>
    </div>
</div>
