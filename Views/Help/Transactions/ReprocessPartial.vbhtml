<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="ReprocAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ReprocOverview" data-parent="#ReprocAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse in accordion-toggle" id="ReprocOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Reprocess</strong> Page. It allows users to see what transactions are in reprocess
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/Transactions/Reprocess/InitialScreen.png" style="width: 100%" alt="Reprocess Load Screen" usemap="#reprocmap" />
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ReprocOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ReprocOverviewAccordion" data-target="#ReprocOverview_1">
                                                    <h3 class="panel-title">
                                                        1 | Transactions in Reprocess
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ReprocOverview_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the information displayed within the <strong>Transactions in Reprocess</strong>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="TransReprocDisp">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#TransReprocDisp" data-target="#TransReprocDisp_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="TransReprocDisp_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Records to View:</strong> Tells if either the current reprocesss or reprocess history records are displayed</li>
                                                                                        <li><strong>Reason Filter:</strong> Tells which type of reprocess records are displayed. This is only usable when <strong>Records to View</strong> is set to <strong>Reprocess</strong></li>
                                                                                        <li><strong>Order Number:</strong> Typeahead to filter records in the datatable by the order number field</li>
                                                                                        <li><strong>Item Number:</strong> Typeahead to filter the records in the datatable by the item number field</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-target="#TransReprocDisp_2" data-parent="#TransReprocDisp">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="TransReprocDisp_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons that are displayed within  the <strong>Transactions in Reprocess</strong>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Clear Filters:</strong> Resets all the filters
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <button data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary" data-original-title="Clear Filters">
                                                                                                        <span class="glyphicon glyphicon-remove"></span>
                                                                                                    </button>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the reprocess report
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <div class="btn-group">
                                                                                                        <button type="button" title="Print" class="btn btn-primary">
                                                                                                            <span class="glyphicon glyphicon-print"></span> <span class="caret"></span>
                                                                                                        </button>
                                                                                                    </div>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Preview Print:</strong> Previews the reprocess report
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <div class="btn-group">
                                                                                                        <button type="button" class="btn btn-primary">
                                                                                                            <span class="glyphicon glyphicon-list-alt"></span> <span class="caret"></span>
                                                                                                        </button>
                                                                                                    </div>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete Transactions:</strong> Either deletes all records or replenishments
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <div class="btn-group">
                                                                                                        <button type="button" title="Delete" class="btn btn-danger">
                                                                                                            <span class="glyphicon glyphicon-trash"></span>
                                                                                                            <span class="caret"></span>
                                                                                                        </button>
                                                                                                    </div>
                                                                                                </li>
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
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#ReprocOverview_2" data-parent="#ReprocOverviewAccordion">
                                                    <h3 class="panel-title">
                                                        2 | Datatable
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ReprocOverview_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses the information within the <strong>Datatable</strong> section,
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-12">
                                                        <strong>Information:</strong>
                                                        <ul>
                                                            <li><strong># Entries:</strong> The number of entries per page in the datatable</li>
                                                            <li><strong>Reprocess Datatable:</strong> Displays the records and their information based on the filter data within the <strong>Transactions in Reprocess</strong> section</li>
                                                            <li><strong>Search Dropdown:</strong> Displays the columns that are available to filter the datatable based on the search text</li>
                                                            <li><strong>Search Text:</strong> The value that the search column will use in order to filter the datatable</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li>
                                                                <strong>Column Sequence:</strong> Button to open up the column sequence page where the order of the displayed columns is able to be edited
                                                                <ul>
                                                                    <li>
                                                                        <div class="btn-group">
                                                                            <button type="button" class="btn btn-primary">
                                                                                <span class="glyphicon glyphicon-list"></span>
                                                                            </button>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ReprocOverviewAccordion" data-target="#ReprocOverview_3">
                                                    <h3 class="panel-title">
                                                        3 | Reprocess Choices
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="ReprocOverview_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses the information within the <strong>Reprocess Choices</strong> section
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-12">
                                                        <strong>Information:</strong>
                                                        <ul>
                                                            <li><strong>Reprocess:</strong> Displays how many transactions are selected for reprocess and if the currently selected one is marked for reprocess</li>
                                                            <li><strong>Post as Complete:</strong> Displays how many transactions are selected for post as complete and if the currently selected one is marked as post for complete</li>
                                                            <li><strong>Send to Reprocess History:</strong> Displays how many transactions are selected for send to reprocess history and if the currently selected one is marked as send to reprocess history</li>
                                                            <li><strong>Selected Orders:</strong> Displays how many transactions are marked for each choice and displays the buttons that can mark and unmark all transactions for the selected choice</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li>
                                                                <strong>Post Transactions:</strong> Begins the process for performing the action designated to each transaction that is marked by the desired choice
                                                                <ul>
                                                                    <li>
                                                                        <div class="btn-group">
                                                                            <button class="btn btn-primary btn-block">Post Transactions</button>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Mark All:</strong> Marks all transactions for the desired choice by the <strong>Selected Orders</strong> tab
                                                                <ul>
                                                                    <li>
                                                                        <div class="btn-group">
                                                                            <button type="button" class="btn btn-primary btn-block">Mark All</button>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                <strong>Unmark All:</strong> Unmarks all transactions from the desired choice by the <strong>Selected Orders</strong> tab
                                                                <ul>
                                                                    <li>
                                                                        <div class="btn-group">
                                                                            <button type="button" class="btn btn-primary btn-block">Unmark All</button>
                                                                        </div>
                                                                    </li>
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
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_1">
                        <h3 class="panel-title">
                            Using the Filters
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_1">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to use the filters in order to show the desired content
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Using the <strong>Records to View</strong> button either select <strong>Reprocess</strong> (current) or <strong>History</strong>.
                                    If History is selected <strong>Orders to Post</strong> screen will be removed and replaced with the more columns from the datatable
                                </li>
                                <li>
                                    After selecting the desired source, select the <strong>Reason Filter</strong> option. This will limit transactions displayed by their reasons.
                                    Select <strong>None</strong> if there is no reason, or select <strong>On Hold</strong> if the reason is on hold (this is disabled if viewing history).
                                </li>
                                <li>Using the typeaheads of <strong>Order Number</strong> and/or <strong>Item Number</strong> select the desired value</li>
                                <li>Once these steps are performed the datatable will display the information for the transactions that satisfy the filters</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_3">
                        <h3 class="panel-title">
                            Clearing Filters
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_3">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to clear and reset the filters
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Have the filters set</li>
                                <li>
                                    Press the <strong>Clear</strong> button (shown below)
                                    <ul>
                                        <li>
                                            <button data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary" data-original-title="Clear Filters">
                                                <span class="glyphicon glyphicon-remove"></span>
                                            </button>
                                        </li>
                                    </ul>
                                </li>
                                <li>Once pressed the all the filters are reset to their default values</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_4">
                        <h3 class="panel-title">
                            Printing Reprocess Report
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_4">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to print the reprocess report
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Fill out the filters to their desired values</li>
                                <li>
                                    Once the filters are set press the <strong>Print</strong> button (shown below)
                                    <ul>
                                        <li>
                                            <div class="btn-group">
                                                <button data-toggle="dropdown" class="btn btn-primary dropdown-toggle">
                                                    <span class="glyphicon glyphicon-print"></span> <span class="caret"></span>
                                                </button>
                                                <ul role="menu" class="dropdown-menu">
                                                    <li>
                                                        <a>Print Selected</a>
                                                        <a>Print All Records</a>
                                                        <a>Print By Selected Reason</a>
                                                        <a>Print By Selected Message</a>
                                                        <a>Print By Date/Time</a>
                                                        <a>Print By Item Number</a>
                                                        <a>Print By Order Number</a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    After pressing the print button a dropdown appears (shown below). On the dropdown press <strong>Print All Records</strong>
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/PrintDropDown.png" alt="Reprocess Transactions Print Drop Down" /></li>
                                    </ul>
                                </li>
                                <li>Once pressed the report is sent to the printer assigned to this workstation</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ReprocFuncs_5" data-parent="#ReprocAccordion">
                        <h3 class="panel-title">
                            Deleting All Records
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_5">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to delete all records from re-process
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Press the <strong>Delete</strong> button (shown below)
                                    <ul>
                                        <li>
                                            <div class="btn-group">
                                                <button type="button" data-toggle="dropdown" title="Delete" class="btn btn-danger dropdown-toggle">
                                                    <span class="glyphicon glyphicon-trash"></span>
                                                    <span class="caret"></span>
                                                </button>
                                                <ul role="menu" class="dropdown-menu">
                                                    <li>
                                                        <a>Delete Selected</a>
                                                        <a>Delete All Records</a>
                                                        <a>Delete By Selected Reason</a>
                                                        <a>Delete By Selected Message</a>
                                                        <a>Delete By Date/Time</a>
                                                        <a>Delete By Item Number</a>
                                                        <a>Delete By Order Number</a>
                                                        <a>Delete Replenishments</a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    Once pressed a dropdown (shown below) appears. On the dropdown press the option <strong>Delete All Records</strong>
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/DeleteDropDown.png" alt="Reprocess Transactions Delete Drop Down" /></li>
                                    </ul>
                                </li>
                                <li>
                                    Once the <strong>Delete All Records</strong> option is selected a pop up (shown below) appears. To continue with the delete press <strong>OK</strong>
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/DeletePopUp.png" alt="Reprocess Transactions Delete Pop Up" /></li>
                                    </ul>
                                </li>
                                <li>Once given the ok, all records are deleted</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_6">
                        <h3 class="panel-title">
                            Deleting All Replenishments
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_6">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discuses how to delete all replenishments
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Press the <strong>Delete</strong> button (shown below)
                                    <ul>
                                        <li>
                                            <div class="btn-group">
                                                <button type="button" data-toggle="dropdown" title="Delete" class="btn btn-danger dropdown-toggle">
                                                    <span class="glyphicon glyphicon-trash"></span>
                                                    <span class="caret"></span>
                                                </button>
                                                <ul role="menu" class="dropdown-menu">
                                                    <li>
                                                        <a>Delete Selected</a>
                                                        <a>Delete All Records</a>
                                                        <a>Delete By Selected Reason</a>
                                                        <a>Delete By Selected Message</a>
                                                        <a>Delete By Date/Time</a>
                                                        <a>Delete By Item Number</a>
                                                        <a>Delete By Order Number</a>
                                                        <a>Delete Replenishments</a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    Once pressed a dropdown (shown below) appears. On the dropdown press the option <strong>Delete Replenishments</strong>
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/DeleteDropDown.png" alt="Reprocess Transactions Delete Drop Down" /></li>
                                    </ul>
                                </li>
                                <li>
                                    Once the <strong>Delete Replenishments</strong> option is selected a pop up (shown below) appears. To continue with the delete press <strong>OK</strong>
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/DeleteReplenPopUp.png" alt="Reprocess Transactions Delete Replenishment Pop Up" /></li>
                                    </ul>
                                </li>
                                <li>Once given the ok, all replenishments are deleted</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_7">
                        <h3 class="panel-title">
                            Marking All Transactions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_7">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses how to mark all transactions within the desired panel
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel-group" id="MarkAllPans">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#MarkAllPans" data-target="#MarkAll_1">
                                                    <h3 class="panel-title">
                                                        Reprocess
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="MarkAll_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to mark all transactions for re-process
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>Reprocess</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Mark All</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Mark All</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) appears. To continue press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/ReProcMarkPop.png" alt="Reprocess Transactions Re-Process Panel Mark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are marked for reprocess</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#MarkAllPans" data-target="#MarkAll_2">
                                                    <h3 class="panel-title">
                                                        Complete
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="MarkAll_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to mark all transactions for complete
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>Complete</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Mark All</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Mark All</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) appears. To continue with the operation press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/CompMarkPop.png" alt="Reprocess Transactions History Panel Mark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are marked for complete</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#MarkAllPans" data-target="#MarkAll_3">
                                                    <h3 class="panel-title">
                                                        History
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="MarkAll_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to mark all transactions for history
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>History</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Mark All</strong> button (shown below)
                                                                <ul>
                                                                    <li>
                                                                        <button class="btn btn-primary">Mark All</button>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                When pressed a pop up (shown below) appears. To continue with the action press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/HistMarkPop.png" alt="Reprocess Transactions History Panel Mark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are marked for history</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_8">
                        <h3 class="panel-title">
                            Unmarking All Transactions
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_8">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses how to unmark all transactions for the desired panel
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel-group" id="UnmarkAll">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#UnmarkAll" data-target="#UnmarkAll_1">
                                                    <h3 class="panel-title">
                                                        Reprocess
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="UnmarkAll_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to unmark all transactions for reprocess
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>Reprocess</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Unmark All</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Unmark All</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once the button is pressed a pop up (shown below) appears. To continue press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/ReProcUnMarkPop.png" alt="Reprocess Transactions Re-Process Panel UnMark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are unmarked for reprocess</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#UnmarkAll" data-target="#UnmarkAll_2">
                                                    <h3 class="panel-title">
                                                        Complete
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="UnmarkAll_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to unmark all for complete
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>Complete</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Unmark All</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Unmark All</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) appears. To continue with the operation press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/CompUnMarkPop.png" alt="Reprocess Transactions Complete Panel UnMark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are unmarked for complete</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#UnmarkAll" data-target="#UnmarkAll_3">
                                                    <h3 class="panel-title">
                                                        History
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="UnmarkAll_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to unmark all transactions for history
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have the <strong>History</strong> panel open</li>
                                                            <li>
                                                                Press the <strong>Unmark</strong> button (shown below)
                                                                <ul>
                                                                    <li><button class="btn btn-primary">Unmark All</button></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) appears. To continue the desired operation press <strong>OK</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Transactions/Reprocess/HistUnMarkPop.png" alt="Reprocess Transactions History Panel UnMark Pop Up" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once confirmed all transactions are unmarked for history</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#ReprocAccordion" data-target="#ReprocFuncs_9">
                        <h3 class="panel-title">
                            Editing a Selected Transaction
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ReprocFuncs_9">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to edit a selected transaction within the <strong>Reprocess</strong> page
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li><strong>Using the filters have the desired order within the <strong>Filtered Orders</strong> list</strong></li>
                                <li>
                                    Select the order number. If an order number is selected it will be highlighted as shown below
                                    <ul>
                                        <li><table class="table table-bordered table-striped table-condensed" style="width:250px;"><tbody><tr class="active"><td>RP1234</td></tr></tbody></table></li>
                                    </ul>
                                </li>
                                <li>
                                    Once selected all transactions for that order are displayed within the <strong>Order Table</strong>. Select the desired transaction to edit by clicking on it.
                                    Once clicked the transaction is highlighted in the same blue color and a new section: <strong>Selected Transaction Info</strong> (shown below) appears.
                                    <ul>
                                        <li>
                                            <img src="/images/Help/Transactions/Reprocess/SelTransInfo.png" style="width: 50%" alt="Reprocess Transactions History Selected Transaction Info" />
                                            <ul>
                                                <li>Within this section is a modal which allows the entire reason message to be shown by clicking on the field <strong>Reason Message</strong></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    Within this section press the <strong>Edit This Transaction</strong> button (shown below)
                                    <ul>
                                        <li>
                                            <button class="btn btn-primary">Edit this Transaction <span class="glyphicon glyphicon-resize-full"></span></button>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    Once pressed a modal (shown below) appears. Within the modal edit any of the editable fields to their desired values
                                    <ul>
                                        <li><img src="/images/Help/Transactions/Reprocess/EditTransModal.png" style="width: 50%" alt="Reprocess Transactions History Edit Transaction Modal" /></li>
                                    </ul>
                                </li>
                                <li>
                                    Once all the desired fields are edited press the <strong>Save Changes and Close</strong> button to save all changes and close the modal. To undo all changes
                                    press the <strong>Close without Saving Changes</strong> button to undo all changes and close the modal
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="reprocmap">
    <area shape="rect" coords="0, 0, 1909, 99" data-target="#ReprocOverview_1" data-toggle="collapse" data-parent="#ReprocOverviewAccordion" />
    <area shape="rect" coords="0, 104, 1108, 596" data-target="#ReprocOverview_2" data-toggle="collapse" data-parent="#ReprocOverviewAccordion" />
    <area shape="rect" coords="1110, 103, 1896, 515" data-target="#ReprocOverview_3" data-toggle="collapse" data-parent="#ReprocOverviewAccordion" />
</map>