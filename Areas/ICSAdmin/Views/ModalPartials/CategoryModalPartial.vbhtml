<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="category_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="category_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <div class="row">
                    <div class="col-md-11">
                        <h4 class="modal-title" id="category_label">Categories - Add, Delete, Edit and Set.</h4>
                    </div>
                    <div class="col-md-1">
                        <button id="print_categories" type="button" data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary Print-Report" data-original-title="Print Categories"><span class="glyphicon glyphicon-print"></span></button>
                    </div>
                </div>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div id="category_alerts">

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-5">
                                <label>Category</label>
                            </div>
                            <div class="col-md-4">
                                <label>Sub Category</label>
                            </div>
                            <div class="col-md-offset-2 col-md-1">
                                <button type="button" class="btn btn-primary" id="category_add"><span class="glyphicon glyphicon-plus"></span></button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="category_container">

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-2 col-md-offset-10">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="category_dismiss">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/CategoryModal.js"></script>
