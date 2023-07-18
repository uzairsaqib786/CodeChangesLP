<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="EventLogDetailModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel">Event Log Entry Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Event ID:</label><span id="modalEventID" class="pull-right"></span>
                    </div>
                    <div class="col-md-6">
                        <label>Event Type:</label><span id="modalEventType" class="pull-right"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Event Location:</label><span id="modalEventLocation" class="pull-right"></span>
                    </div>
                    <div class="col-md-6">
                        <label>Event Code:</label><span id="modalEventCode" class="pull-right"></span>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label>Username:</label><span id="modalUser" class="pull-right"></span>
                    </div>
                    <div class="col-md-6">
                        <label>Date:</label><span id="modalDate" class="pull-right"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Transaction ID:</label><span id="modalTransID" class="pull-right"></span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Message:</label>
                        <textarea readonly rows="3" class="form-control no-horizontal readonly-cursor" id="modalMessage"></textarea>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Notes:</label>
                        <textarea readonly rows="5" class="form-control no-horizontal readonly-cursor" id="modalNotes"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary" id="modalPrint" data-dismiss="modal">Print Event</button>
                <button type="button" class="btn btn-danger" id="modalDelete">Delete Event</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/EventLogDetailModal.js"></script>