<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="ToteManagerAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ToteManagerAccordion" data-target="#ToteManagerOverview">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ToteManagerOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This page is the <strong>Tote Manager</strong> page. It is used for managing tote ids.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <img src="~/Areas/Induction/Images/HelpToteMan/ToteManModal.png" style="width:35%" alt="Pick Tote Setup" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="ToteManInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManInfo" data-target="#ToteManInfo_1">
                                            <h3 class="panel-title">
                                                Information
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Information:</strong>
                                                <ul>
                                                    <li><strong>Tote ID:</strong> A created tote id</li>
                                                    <li><strong>Cell:</strong> The number of cells within the corresponding tote id</li>
                                                    <li><strong>From Tote ID Print:</strong> Where to start when printing the tote id range</li>
                                                    <li><strong>To Tote ID Print:</strong> Where to end when printing the tote id range </li>
                                                    <li><strong>Allocated:</strong> Not a text field displays when tote can't be altered because it's currently allocated within the system</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManInfo" data-target="#ToteManInfo_2">
                                            <h3 class="panel-title">
                                                Buttons
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li><strong>Delete:</strong> Deletes the corresponding tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/DeleteButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Print:</strong> Prints the corresponding tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/PrintButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Save:</strong> Saves any changes to the corresponding tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/SaveButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Add New Tote:</strong> Adds a new tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/AddButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li><strong>Print Range:</strong> Prints a report for the desired range of tote ids
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/PrintRangeButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ToteManagerAccordion" data-target="#ToteManagerFuncs">
                        <h3 class="panel-title">
                            Functions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ToteManagerFuncs">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses he functionality within the <strong>Tote Manager</strong> modal
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="ToteManFuncs">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManFuncs" data-target="#ToteManFuncs_1">
                                            <h3 class="panel-title">
                                                Printing Tote ID Range
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManFuncs_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Tote Manager modal displayed</li>
                                                    <li>Set the starting tote id</li>
                                                    <li>Set the ending tote id</li>
                                                    <li>Press the <strong>Print Range</strong> button
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/PrintRangeButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the report is now printed or preivewed depending on the print direct preference</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManFuncs" data-target="#ToteManFuncs_2">
                                            <h3 class="panel-title">
                                                Add New Tote ID
                                                <span class="accordion-caret-down">
                                                </span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManFuncs_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Tote Manager modal displayed</li>
                                                    <li>Press the <strong>Add New Tote</strong> button
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/AddButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Locate the new empty tote id row</li>
                                                    <li>Fill in the tote id and cell fields</li>
                                                    <li>Press the <strong>Save</strong> button
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/SaveButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the new tote id is saved and added</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManFuncs" data-target="#ToteManFuncs_3">
                                            <h3 class="panel-title">
                                                Delete a Tote ID
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManFuncs_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Tote Manager modal displayed</li>
                                                    <li>Press the <strong>Delete</strong> button assigned to the tote id that's going ot be deleted
                                                       <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/DeleteButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed a pop up appears. To contiue deleting the tote id press <strong>OK</strong>
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/DeleteModal.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once presssed the tote id is deleted</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManFuncs" data-target="#ToteManFuncs_4">
                                            <h3 class="panel-title">
                                                Print a Single Tote ID
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManFuncs_4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Tote Manger modal displayed</li>
                                                    <li>Press the <strong>Print</strong> button assigned to the desired tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/PrintButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the report is printed or previewed depending on the print direct preference</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ToteManFuncs" data-target="#ToteManFuncs_5">
                                            <h3 class="panel-title">
                                                Editing a Tote ID
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ToteManFuncs_5">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Tote Manager modal displayed</li>
                                                    <li>Adjust the desired tote id's information</li>
                                                    <li>When all changes are made press the <strong>Save</strong> button assigned to the edited tote id
                                                        <ul>
                                                            <li><img src="~/Areas/Induction/Images/HelpToteMan/SaveButt.png" alt="Pick Tote Setup" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed all changes are saved for the tote id</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>