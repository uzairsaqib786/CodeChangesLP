<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info" style="margin-bottom:10px;">
            <div class="panel-heading">
                <a data-toggle="collapse" data-target="#SRC_0">
                    <h3 class="panel-title">
                        Overview
                        <span class="accordion-caret-down"></span>
                    </h3>
                </a>
            </div>
            <div class="panel-body collapse accordion-toggle" id="SRC_0">
                <div class="row">
                    <div class="col-md-12">
                        The <strong>Current Orders</strong> section of System Replenishment allows users to see, delete and print current replenishments that have not yet been completed.
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <img src="~/images/Help/SystemReplenishment/sysreplen_current.PNG" style="width: 85%"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-group" id="SRC_Accordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#SRC_1" data-parent="#SRC_Accordion">
                        <h3 class="panel-title">
                            1 | Print Current Replenishment Orders
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRC_1">
                    <div class="row">
                        <div class="col-md-12">
                            <ol>
                                <li>
                                    Click the print button dropdown. 
                                    <div class="btn-group">
                                        <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="printReport" class="Print-Report">Print Orders</a></li>
                                            <li><a id="printLabel" class="Print-Label">Print Labels</a></li>
                                        </ul>
                                    </div>
                                </li>
                                <li>Click the print dropdown entry desired for either printing orders or labels.</li>
                            </ol>
                            <strong>Note: </strong> If there is no specified printer for reports or for labels the corresponding print button will be disabled.  Clicking it while disabled will cause
                            an alert to warn the user.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#SRC_2" data-parent="#SRC_Accordion">
                        <h3 class="panel-title">
                            2 | Delete Current Replenishment Orders
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="SRC_2">
                    <div class="row">
                        <div class="col-md-12">
                            <ol>
                                <li>
                                    Click the delete dropdown button 
                                    <div class="btn-group">
                                        <button id="delete" class="btn btn-danger dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-trash"></span> <span class="caret"></span></button>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a id="deleteAll">Delete All Orders</a></li>
                                            <li><a id="deleteShown">Delete Shown Orders</a></li>
                                            <li><a id="deleteRange">Delete Range</a></li>
                                        </ul>
                                    </div>
                                </li>
                                <li>Click the desired delete type from the dropdown.</li>
                            </ol>
                            <div class="row">
                                <div class="col-md-12">
                                    <strong>Delete Shown Orders</strong> will delete records as filtered by the search feature on the right side.  So if the Zone column is being searched for "02" the delete button will
                                    delete all current replenishments from Zone 02.
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <strong>Delete Range</strong> will reveal a modal (pop-up), which will prompt the user to select a column to delete by and a starting value and ending value for 
                                    that column.  Example: Pick Location is the selected column.  0530508A2 is in the begin location textbox.  0530610A4 is the end location.  Any location evaluated
                                    alphanumerically to be in between those two locations will be selected to be deleted.
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>