<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="EventLogAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ELOverview" data-parent="#EventLogAccordion"><h3 class="panel-title">Overview <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body accordion-toggle collapse" id="ELOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Event Log</strong>.  It displays logged information relevant to PickPro functionality and interaction with the data.  
                                    The black outlined boxes can be clicked to open a detailed overview of the particular area below the image.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/EventLog/load.png" alt="Event Log load screen" style="width: 85%" usemap="#EventLogMap" />
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ELOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ELOverviewAccordion" data-target="#panelEL_1"><h3 class="panel-title">1 | Filters <span class="accordion-caret-down"></span></h3></a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelEL_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Filters:</strong>
                                                        <ul>
                                                            <li>Start Date:  Filters any Event Log entries before its value out of the resulting recordset.</li>
                                                            <li>End Date:  Filters any Event Log entries after its value out of the resulting recordset.</li>
                                                            <li>Ignore Date Range:  Nullifies the effect of <strong>Start Date</strong> and <strong>End Date</strong> fields.</li>
                                                            <li>Message:  Allows filtering of specific messages in Message column - e.g. Logon</li>
                                                            <li>Event Location:  Allows Event Location column to be filtered on - e.g. Workstation #1</li>
                                                            <li>Name Stamp:  Allows Username column to be filtered on - e.g. joe1</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row" style="padding-top:5px;">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li><strong>Clear Filters</strong>:  Empties or resets all filters.  Alternatively the <strong>C</strong> key can be pressed while not in an input text box.</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-target="#panelEL_2" data-parent="#ELOverviewAccordion" data-toggle="collapse"><h3 class="panel-title">2 | Event Log Table <span class="accordion-caret-down"></span></h3></a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelEL_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The <strong>Event Log</strong> table contains data relevant to use of PickPro.  For more detailed information see <strong>Tables</strong>.
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li><strong>Print <span class="caret"></span></strong>:  Allows printing of a highlighted entry or a range of entries based on filters.  Prompts the user for confirmation of a range or selected entry.  Alternatively the <strong>I</strong> key can be pressed to activate the button when not in an input text box.</li>
                                                            <li><strong>Export Range</strong>:  Allows the table's currently selected data to be copied into an Excel spreadsheet.  Alternatively the <strong>E</strong> key can be pressed to export a range of entries.</li>
                                                            <li><strong>Delete Range</strong>:  Allows the user to delete a range of entries based on filters.  Prompts the user before deleting records.  Alternatively the <strong>D</strong> key can be pressed to delete a range of entries.</li>
                                                            <li><strong>Refresh Table</strong>:  Refreshes the table with any newly added data which matches the filters.  Alternatively the <strong>R</strong> key can be pressed while not in an input text box.</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Unique Features:</strong>
                                                        <ul>
                                                            <li>
                                                                Row highlighting - Click a row and it will turn <span class="activeRow" style="color:white;">blue.</span>  This allows you to print or delete the selected item or expand a modal.  (see <strong>Modals</strong> for details.)
                                                                <ul>
                                                                    <li>
                                                                        <img src="/images/Help/EventLog/elhighlight.png" style="width: 50%" alt="Event Log row highlighted" />
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Modal detail view - Click on a highlighted row and a modal will pop up giving a more detailed view of the data.
                                                                <ul>
                                                                    <li>
                                                                        <img src="/images/Help/EventLog/elmodal.png" style="width: 50%" alt="Event Log modal detailed view" />
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
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELMessage">
                        <h3 class="panel-title">
                            Log Message Types <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELMessage">
                    <div class="row">
                        <div class="col-md-12">
                            <ul>
                                <li>Allocations - e.g. Assigning items to a particular location.</li>
                                <li>Changes to preferences - e.g. Changing the report printer for a particular workstation.</li>
                                <li>Changes to tables by automated events - e.g. <strong>Transaction History</strong> being purged to keep the database lightweight.</li>
                                <li>
                                    Deletions by users
                                    <ul>
                                        <li>Items</li>
                                        <li>Kits</li>
                                        <li>Locations</li>
                                        <li>Batch IDs</li>
                                        <li>Scan Codes</li>
                                    </ul>
                                </li>
                                <li>Errors</li>
                                <li>Exports - e.g. Exporting the <strong>Event Log</strong> to an Excel spreadsheet.</li>
                                <li>Logons</li>
                                <li>Setup Information - e.g. Existing locations or stock being added to a new or updated management system.</li>
                                <li>(Un-)Quarantines
                                    <ul>
                                        <li>Items</li>
                                        <li>Locations</li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELData"><h3 class="panel-title">Selecting data <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELData">
                    <div class="row">
                        <div class="col-md-12">
                            Populating the Event Log table with data starts with the filters.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Choose a date range to get data for.  See <strong>DatePickers</strong> for details.  You may also check the checkbox titled <strong>Ignore Date Range</strong> to make these filters optional.</li>
                                <li>Optional:  Choose a message to filter on.  E.g. User logged onto Workstation #1</li>
                                <li>Optional:  Choose an Event Location filter.  E.g. Workstation #1</li>
                                <li>Optional:  Choose an Event Code filter.  E.g. PickPro</li>
                                <li>Optional:  Choose an Event Type filter.  E.g. Logon</li>
                                <li>Optional:  Choose a Username filter. E.g. UserABC</li>
                            </ol>
                            Note: The table will refresh with new data everytime a new filter or a different filter is applied or removed.
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            Applying the filters over again after a period of inactivity will refresh the table's data with any changes.
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELClear"><h3 class="panel-title">Clear Event Log filters <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELClear">
                    <div class="row">
                        <div class="col-md-12">
                            There are two methods to clear the Event Log's filters.  Each filter can be manually reset or a shortcut can be used.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Click the <strong>Clear Filters</strong> button or press the <strong>C</strong> key when not in an input text box or datepicker.
                                    <ul>
                                        <li><img src="/images/Help/EventLog/elclearbutton.png" alt="Clear Filters button" /></li>
                                    </ul>
                                </li>
                            </ol>
                            <strong>Results:</strong>
                            <ul>
                                <li>Start and End Date filters will be reset to the current date.</li>
                                <li>Message, Event Location and Name Stamp filters are emptied.</li>
                                <li>The Event Log table is refreshed.</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELRefresh"><h3 class="panel-title">Refresh Event Log table <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELRefresh">
                    <div class="row">
                        <div class="col-md-12">
                            Refreshing the Event Log table can be useful when the page has been left open for an extended period.  If new data has been added to the log refreshing the table will display any new data.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Click the <strong>Refresh Table</strong> button or press the <strong>R</strong> key when not in an input text box.
                                    <ul>
                                        <li>
                                            <img src="/images/Help/EventLog/elrefresh.png" alt="Refresh Table button" />
                                        </li>
                                    </ul>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELPrint"><h3 class="panel-title">Print one or more entries <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELPrint">
                    <div class="row">
                        <div class="col-md-12">
                            There are multiple methods of printing an entry or a range of entries from the Event Log.  These reports will be printed to the workstation's report printer.  See <strong>Preferences</strong> for details.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="ELPrintAccordion">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ELPrintAccordion" data-target="#ELPrintSelected"><h3 class="panel-title">Print a selected entry <span class="accordion-caret-down"></span></h3></a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ELPrintSelected">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h3>Method #1</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click a row to highlight it.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elhighlight.png" style="width: 50%" alt="Highlighted row in Event Log" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Click the <strong>Print</strong> button dropdown.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elprint.png" alt="Event Log Print button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Click the <strong>Print Selected</strong> button (#1) from the dropdown.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elprint_expanded.png" alt="Event Log Print dropdown expanded" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>The selected entry will be printed to the workstation's report printer.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:15px;">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h3>Method #2</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click a row to highlight it.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elhighlight.png" style="width: 50%" alt="Event Log selected row" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Click the highlighted row to launch a modal detail view.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elmodal.png" style="width: 50%" alt="Event Log modal detail view" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Click the <strong>Print Event</strong> button.
                                                                <ul>
                                                                    <li><img src="/images/Help/EventLog/elmodalprint.png" alt="Event Log modal Print Event button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li>The selected event will print to the workstation's report printer (see <strong>Preferences</strong> for details.)</li>
                                                            <li>The modal detail view will close.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#ELPrintAccordion" data-target="#ELPrintRange"><h3 class="panel-title">Print a range of entries <span class="accordion-caret-down"></span></h3></a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="ELPrintRange">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>
                                                        Ensure that the filters are correct for what you want to print.  What is displayed is what will be printed.
                                                    </li>
                                                    <li>
                                                        Click the <strong>Print</strong> button dropdown.
                                                        <ul>
                                                            <li><img src="/images/Help/EventLog/elprint.png" alt="Event Log Print button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        Click the <strong>Print Range</strong> button from the dropdown. (#2)
                                                        <ul>
                                                            <li><img src="/images/Help/EventLog/elprint_expanded.png" alt="Event Log Print Range Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        The range of Event Log entries will be printed to the workstation's report printer.  (see <strong>Preferences</strong> for details.)
                                                    </li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EventLogAccordion" data-target="#ELExport"><h3 class="panel-title">Export a range of entries to an Excel spreadsheet <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELExport">
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>
                                    Ensure that the filters are correct for what you want to export.  What is displayed is what will be exported.
                                </li>
                                <li>
                                    Click the <strong>Export</strong> button.
                                    <ul>
                                        <li>
                                            <img src="/images/Help/EventLog/elexport.png" alt="Event Log Export button" />
                                        </li>
                                    </ul>
                                </li>
                                <li>The Excel file will be generated.</li>
                                <li>The file will be downloaded to the workstation's download folder.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-target="#ELDelete" data-toggle="collapse" data-parent="#EventLogAccordion">
                        <h3 class="panel-title">
                            Delete a single entry or a range of entries <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ELDelete">
                    <div class="panel-group" id="ELDeleteAccordion">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#ELDeleteAccordion" data-target="#ELDeleteRange"><h3 class="panel-title">Delete a range <span class="accordion-caret-down"></span></h3></a>
                            </div>
                            <div class="panel-body accordion-toggle collapse" id="ELDeleteRange">
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>Steps:</strong>
                                        <ol>
                                            <li>Ensure the filters are correct for what you want to delete.  What is displayed in the table is what will be deleted.</li>
                                            <li>
                                                Click the <strong>Delete Range</strong> button.
                                                <ul>
                                                    <li>
                                                        <img src="/images/Help/EventLog/eldelete.png" alt="Event Log Delete Range button" />
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>
                                                Confirm or deny the deletion.
                                                <ul>
                                                    <li>
                                                        <img src="/images/Help/EventLog/eldelete_confirm.png" alt="Event Log Delete Range confirmation" />
                                                    </li>
                                                    <li>If <strong>Cancel</strong> is clicked the entries will not be deleted and the deletion attempt will end.</li>
                                                </ul>
                                            </li>
                                            <li>If <strong>OK</strong> was clicked the entries are deleted.</li>
                                            <li>The table is refreshed with any new data.</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#ELDeleteAccordion" data-target="#ELDeleteSelected"><h3 class="panel-title">Delete a selected entry <span class="accordion-caret-down"></span></h3></a>
                            </div>
                            <div class="panel-body accordion-toggle collapse" id="ELDeleteSelected">
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>Steps:</strong>
                                        <ol>
                                            <li>
                                                Click the row to be deleted.
                                                <ul>
                                                    <li><img src="/images/Help/EventLog/elhighlight.png" style="width: 40%" alt="Event Log highlight row" /></li>
                                                </ul>
                                            </li>
                                            <li>
                                                Click the highlighted row to launch a modal detail view.
                                                <ul>
                                                    <li><img src="/images/Help/EventLog/elmodal.png" style="width: 40%" alt="Event Log modal detail view" /></li>
                                                </ul>
                                            </li>
                                            <li>
                                                Click the <strong>Delete Event</strong> button.
                                                <ul>
                                                    <li><img src="/images/Help/EventLog/elmodal_delete.png" alt="Event Log modal Delete Event button" /></li>
                                                </ul>
                                            </li>
                                            <li>
                                                Confirm the deletion by clicking the <strong>OK</strong> button. (#1)
                                                <ul>
                                                    <li>
                                                        <img src="/images/Help/EventLog/elmodal_delete_confirm.png" alt="Event Log delete entry confirmation" />
                                                    </li>
                                                    <li>
                                                        If the <strong>Cancel</strong> button is clicked the deletion will halt and you will be returned to the modal.
                                                    </li>
                                                </ul>
                                            </li>
                                            <li>If the <strong>OK</strong> button was clicked the entry will be deleted.</li>
                                            <li>The modal detail view for the deleted entry will exit.</li>
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

<map name="EventLogMap">
    <area shape="rect" data-parent="#ELOverviewAccordion" data-target="#panelEL_1" data-toggle="collapse" coords="5, 34, 224, 430" />
    <area shape="rect" data-parent="#ELOverviewAccordion" data-target="#panelEL_2" data-toggle="collapse" coords="233, 37, 1220, 290" />
</map>