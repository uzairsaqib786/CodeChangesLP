<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="BatchManModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="BatchMan_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="modal-title" id="BatchMan_label"><strong>Batch Manager</strong></h4>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-8 col-md-offset-2">
                                <h3 id="BatchMan_Message" class="text-center"></h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="BatchMan_OK">Ok</button>
                        <button type="button" id="BatchMan_Close" class="btn btn-default" onclick="BatchManModal_OKClick = false" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/CategoryModal.js"></script>
