<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype string
<div class="modal fade" id="logo_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="logo_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h3 class="modal-title">
                    Current Company Logo
                </h3>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <strong>Note:</strong> If the image is not the most recently uploaded please try refreshing the page by using the key combination:  CTRL + F5.
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        @If Model <> "NO EXTENSION FOUND" Then
                            @<div id="modal_logo" style="overflow-x:scroll;"><img src="./images/CompanyLogo/logo@(Model)" /></div>
                        Else
                            @<div id="modal_logo">No company logo found!</div>
                        End If
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="logo_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>