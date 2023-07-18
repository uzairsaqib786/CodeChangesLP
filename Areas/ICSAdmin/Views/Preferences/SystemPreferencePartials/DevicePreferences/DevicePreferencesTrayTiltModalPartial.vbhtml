<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="tray_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="tray_label" aria-hidden="true">
    <div class="modal-dialog" style="width:700px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="tray_label">Shuttle Tray Tilt</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3">
                        <label>Zone</label>
                    </div>
                    <div class="col-md-3">
                        <label>Carousel</label>
                    </div>
                    <div class="col-md-3">
                        <label>Tray</label>
                    </div>
                    <div class="col-md-3">
                        <label>Tilt</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="tray_container">
                        <div class="row tray-container">
                            <div class="col-md-3">
                                <span class="label label-default tray-zone"></span>
                            </div>
                            <div class="col-md-3">
                                <span class="label label-default tray-car"></span>
                            </div>
                            <div class="col-md-3">
                                <span class="label label-default tray"></span>
                            </div>
                            <div class="col-md-3">
                                <div class="toggles toggle-modern tray-tilt"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="tray_all">Set All On</button>
                    </div>
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary" id="tray_alloff">Set All Off</button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" id="tray_dismiss">Go Back</button>
            </div>
        </div>
    </div>
</div>