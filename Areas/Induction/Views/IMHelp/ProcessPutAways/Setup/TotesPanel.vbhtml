<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <a data-toggle="collapse" data-parent="#ProcessPutToteInnerAccordion" data-target="#ProcessPutTote3Accordion">
            <h3 class="panel-title">
                3 | Tote Setup
                <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse accordion-toggle" id="ProcessPutTote3Accordion">
        <div class="row">
            <div class="col-md-12">
                This panel refers to the <strong>Totes</strong> panel on the Process Put Aways screen.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                The image seen below is the <strong>Totes</strong> modal.  It allows users to print a range of tote labels, assign a tote to the current batch or add, edit, print or delete a managed tote.
                <img src="~/Areas/Induction/Images/HelpProcPutBatches/managetotes.png" style="width:95%" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                The Position field of all totes included in the current batch cannot be changed.  Tote IDs should therefore be assigned to the appropriate position and not the other way around.
                In order to assign a Tote ID to a position in the current batch:
                <ol>
                    <li>Choose the position you wish to assign to the tote.</li>
                    <li>
                        Click the corresponding textbox (seen below)
                        <ul>
                            <li>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                                            <input type="text" class="form-control modal-launch-style" name="tote-id"  value="" readonly="readonly">
                                            <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li>Choose a tote from the modal that appears or enter a new tote.</li>
                    <li>
                        Depending on your site's preferences you may have the choice of using a "managed" or "unmanaged" tote exclusively.
                        <ul>
                            <li>A "managed" tote is registered in PickPro's database and has predefined properties.</li>
                            <li>An "unmanaged" tote is not registered in PickPro's database and in some cases may be used for tasks other than induction as well.</li>
                            <li>Managed totes are always listed, but depending on preferences unmanaged totes may be disallowed.</li>
                            <li>Managed totes are located in the area labeled 2 in the image above.</li>
                            <li>Unmanaged totes are located in the area labeled 1 in the image above.</li>
                        </ul>
                    </li>
                    <li>
                        Choose the method by which you will assign the tote.
                        <ul>
                            <li>
                                If you wish to use a managed, existing tote:
                                <ul>
                                    <li>Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button> button.</li>
                                    <li>Note:  If there is an allocated label present and no set button (<span class="label label-default">Allocated</span>) the tote is already in use and cannot be assigned.</li>
                                </ul>
                            </li>
                            <li>If you wish to use a managed tote that is not registered yet follow the directions below to create the tote first and then return to this process.</li>
                            <li>
                                If you wish to use an unmanaged tote:
                                <ul>
                                    <li>Type the new tote ID in the <strong>Unmanaged Tote ID</strong> textbox and click the <button class="btn btn-primary"><span class="glyphicon glyphicon-edit"></span></button> button.</li>
                                    <li>If the <strong>Unmanaged Tote ID</strong> textbox is not present you need to change your preferences to allow the use of unregistered totes before you can assign an unmanaged tote.</li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                In order to create a new managed tote:
                <ol>
                    <li>
                        Open the <strong>Totes</strong> modal by clicking the assign tote textbox corresponding to the position you wish to place the tote.
                        <ul>
                            <li>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                                            <input type="text" class="form-control modal-launch-style" name="tote-id"  value="" readonly="readonly">
                                            <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li>
                        Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button> button to add a new tote to the list.
                    </li>
                    <li>Scroll to the new tote in the totes list.</li>
                    <li>Enter the new managed tote's ID and the number of cells it contains in the appropriate textboxes.</li>
                    <li>Click the <button class="btn btn-primary save-tote"><span class="glyphicon glyphicon-floppy-disk"></span></button></li>
                    <li>If there is a duplicate tote it will not be saved and you will be warned of the potential copy.  Saving will not succeed if there are duplicate managed totes.</li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                In order to print a tote label:
                <ol>
                    <li>
                        Choose whether you desire a single tote label or wish to have a tote label for each tote in the current batch.
                        <ul>
                            <li>
                                If you wish to print a single tote label:
                                <ul>
                                    <li>Method 1:  Click the <button type="button" data-toggle="tooltip" data-original-title="Print Tote Label" data-placement="top" class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> button in the <strong>Totes</strong> panel next to the corresponding Tote ID.</li>
                                    <li>
                                        Method 2:
                                        <ol>
                                            <li>
                                                Click the tote textbox.
                                                <ul>
                                                    <li>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                                    <input type="text" class="form-control modal-launch-style" name="tote-id" value="" readonly="readonly">
                                                                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>
                                                Click the <button type="button" data-toggle="tooltip" data-original-title="Print Tote Label" data-placement="top" class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> button in the <strong>Totes</strong> modal next to the corresponding Tote ID.
                                            </li>
                                        </ol>
                                    </li>
                                </ul>
                            </li>
                            <li>
                                If you wish to print a range of tote labels:
                                <ul>
                                    <li>
                                        Method 1: (This method allows you to specify a range of managed totes and does not matter which batch is currently selected.)
                                        <ol>
                                            <li>
                                                Click the tote textbox to launch the <strong>Totes</strong> modal.
                                                <ul>
                                                    <li>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="form-group has-feedback" style="margin-bottom:0px;">
                                                                    <input type="text" class="form-control modal-launch-style" name="tote-id"  value="" readonly="readonly">
                                                                    <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>Enter the beginning tote of the range into the <strong>From Tote ID</strong> textbox.</li>
                                            <li>Enter the end tote of the range into the <strong>To Tote ID</strong> textbox.</li>
                                            <li>Click the <button class="btn btn-primary">Print Range</button> button.</li>
                                        </ol>
                                    </li>
                                    <li>
                                        Method 2:  (This method prints every tote label AND location label for the current batch and is only available to a "processed" batch.)
                                        <ol>
                                            <li>Click the <button class="btn btn-primary">Print Tote / Location Labels</button> button.</li>
                                        </ol>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                In order to delete a managed tote:
                <ol>
                    <li>
                        Launch the <strong>Totes</strong> modal by clicking
                        <ul>
                            <li>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group has-feedback" style="margin-bottom:0px;">
                                            <input type="text" class="form-control modal-launch-style" name="tote-id"  value="" readonly="readonly">
                                            <i style="top:0px;" class="glyphicon glyphicon-resize-full form-control-feedback modal-launch-style"></i>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ul>.
                    </li>
                    <li>Click the <button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button.</li>
                    <li>Click OK in the prompt to continue the deletion.  Click Cancel to cancel the deletion.</li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                In order to clear the tote and number of cells from a position:
                <ol>
                    <li>Identify the position you desire to clear.</li>
                    <li>Click the <button type="button" class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button in the <strong>Totes</strong> panel.</li>
                    <li>The number of cells for the position clicked will reset to the default number of cells in a tote.</li>
                </ol>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                In order to automatically assign tote IDs to the totes in each position:
                <ul>
                    <li>If you wish to assign a single tote ID click the <button type="button" class="btn btn-primary">Assign Next ID</button> button next to the appropriate tote.</li>
                    <li>If you wish to assign IDs to every tote in the current batch click the <button type="button" class="btn btn-primary">Assign all IDs</button> button.</li>
                </ul>
            </div>
        </div>
    </div>
</div>