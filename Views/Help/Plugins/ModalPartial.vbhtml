<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Modals (Pop-ups)
                    </h3>
                </div>
                <div class="panel-body" id="ModalOverview">
                    <div class="row">
                        <div class="col-md-12">
                            Modals are pop-ups that allow data entry or selection in a more convenient or contextually relevant way.
                        </div>
                    </div>
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    Modals are commonly launched in PickPro by buttons or input boxes with the expandable icon on or next to them.
                                </div>
                            </div>
                            <div class="row" style="padding-top:15px;">
                                <div class="col-md-4">
                                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                                        <label class="control-label">Example Modal Input Box</label>
                                        <input type="text" readonly="readonly" class="form-control modal-launch-style" id="ExampleModalInput">
                                        <i class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top:15px;">
                                <div class="col-md-4">
                                    <button id="ExampleModalButton" type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Click Me!">
                                        Example Modal Button
                                        <span class="glyphicon glyphicon-resize-full"></span>
                                    </button>
                                </div>
                            </div>
                            <div class="row" style="padding-top:15px;">
                                <div class="col-md-12">
                                    Clicking either of the examples will launch a modal with example inputs.  Modals will also appear when there is an emergency order in the user's zone and carousel 
                                    according to their workstation's pick zones.  The emergency modal will persist until the emergency orders are taken care of.  They can be dismissed, but they will 
                                    reappear on a timer or when new emergency orders are entered.
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    The first enabled text input will get focus when a modal opens.
                                </div>
                            </div>
                            <div class="row top-spacer">
                                <div class="col-md-12">
                                    <img src="~/images/Help/Modals/uom.png" style="width: 50%" alt="Unit of Measure modal" />
                                </div>
                                <div class="col-md-12">
                                    The image above is the <strong>Unit of Measure</strong> modal which can be seen on the <strong>Inventory Master</strong> screen and several other areas.  It allows you to choose a specific Unit of Measure for a contextually related 
                                    field.  The field that you clicked to launch such a modal is usually the one that will be completed by the modal.  
                                    
                                    <ul>
                                        <li>Clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button> button will allow you to set the Unit of Measure.</li>
                                        <li>Clicking the <button type="button" class="btn btn-danger" data-toggle="tooltip" data-placement="top" data-original-title="Delete"><span class="glyphicon glyphicon-remove"></span></button> button will allow you to delete a saved unit of measure.</li>
                                        <li>Editing the unit of measure and then clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> button will save the changes to the unit of measure and allow you to set it.</li>
                                    </ul>  
                                    
                                    Many areas of the application have modals that function similarly to the <strong>Unit of Measure modal</strong>.  Check individual pages for details on those modals and how to use them.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="example_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="example_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="example_label">Example Modal - Edit</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        Clicking <strong>Submit</strong> will close the modal and enter the example text into the example modal text box.  This is how modals allow the user to set values.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        Clicking <strong>Close</strong> will dismiss the modal without altering the value of the input text box or button.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <label>Example Input Label</label>
                        <input type="text" class="form-control" placeholder="Example Text Here" />
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="example_dismiss">Close</button>
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="example_submit">Submit</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Help/ModalExample.js"></script>