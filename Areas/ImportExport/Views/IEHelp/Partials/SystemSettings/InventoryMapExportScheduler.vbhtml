<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <img src="~/Areas/ImportExport/Images/Help/scheduler.png" alt="Inventory Map Export Scheduler modal" />
    </div>
    <div class="col-md-12">
        The <strong>Inventory Map Export Scheduler</strong> allows the user to define the times that the inventory map will be exported to a file.  Specific days and times may be set on an individual basis.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        The following fields are available:
        <ul>
            <li><strong>Last Export Date/Time</strong> - Indicates the last time that the export process was executed according to the <strong>Event Log</strong>.</li>
            <li>
                Days
                <ul>
                    <li>Monday</li>
                    <li>Tuesday</li>
                    <li>Wednesday</li>
                    <li>Thursday</li>
                    <li>Friday</li>
                    <li>Saturday</li>
                    <li>Sunday</li>
                </ul>
            </li>
            <li>Export Hour - Hint: 24 hour time ("Military time") is a valid input, but it will be converted to the 12 hour style after leaving the input textbox, by changing the value of the input and of the AM/PM control.</li>
            <li>Export Minute</li>
            <li>AM/PM</li>
        </ul>
        The following actions are available:
        <ul>
            <li>
                <button class="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                </button> - Allows the current record's changes to be saved.
            </li>
            <li>
                <button class="btn btn-primary">
                    <span class="glyphicon glyphicon-plus"></span>
                </button> - Allows the user to add a new definition for a new time/day to export the inventory map.
            </li>
            <li>
                <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> - Allows the user to delete a custom definition for an export time/week.
            </li>
        </ul>
    </div>
</div>