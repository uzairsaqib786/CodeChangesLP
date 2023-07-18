<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype String
@Code
    ViewData("Title") = "Event Log"
    ViewData("PageName") = "&nbsp; | &nbsp; Event Log"
    Layout = PickPro_Web.GlobalFunctions.chooseLayoutFile(Model)
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="col-md-2">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="checkbox" style="margin-top:10px">
                                            <label><input id="dateIgnore" type='checkbox'/>Ignore Date Range</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <input type="text" class="form-control date-picker" id="sDateFilterEL" maxlength="50" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>End Date</label>
                                            <input type="text" class="form-control date-picker" id="eDateFilterEL" maxlength="50" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Message</label>
                                            <input type="text" class="form-control" id="messageFilterEL" maxlength="255" placeholder="Message" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Event Location</label>
                                            <input type="text" class="form-control" id="eLocationEL" maxlength="255" placeholder="Event Location" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Event Code</label>
                                            <input type="text" class="form-control" id="EventCode" maxlength="255" placeholder="Event Code" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Event Type</label>
                                            <input type="text" class="form-control" id="EventType" maxlength="255" placeholder="Event Type" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Username</label>
                                            <input type="text" class="form-control" id="nStampEL" maxlength="50" placeholder="Username" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <button id="clearFilters" class="btn btn-primary btn-block"><u>C</u>lear Filters</button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-10">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label># Entries</label>
                                        <select id="pageLength" class="form-control" style="width:auto; display:inline;">
                                            <option>10</option>
                                            <option>15</option>
                                            <option selected ="selected">20</option>
                                            <option>25</option>
                                            <option>50</option>
                                            <option>100</option>
                                        </select>
                                        <div class="btn-group" style="padding-right:5px;">
                                            <button id="printButton" type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                            <ul class="dropdown-menu" role="menu">
                                                <li><a id="printSelected" class="Print-Report">Print Selected</a></li>
                                                <li><a id="printRange" class="Print-Report">Print Range (by date)</a></li>
                                            </ul>
                                        </div>
                                        <button id="refreshTable" data-toggle="tooltip" title="Refresh Table" class="btn btn-primary"><span class="glyphicon glyphicon-refresh"></span></button>
                                        <button id="exportRange" class="btn btn-primary"><u>E</u>xport Range</button>
                                        @If Model = "Admin" Then
                                            @<button id="deleteRange" class="btn btn-danger"><u>D</u>elete Range</button>
                                        End If
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <table style="background-color:white;" id="eventTable" class="table table-hover table-bordered table-condensed table-striped">
                                            <thead>
                                                <tr>
                                                    <th style="max-width: 135px; min-width:135px;">Date</th>
                                                    <th style="max-width: 300px; min-width: 300px;">Message</th>
                                                    <th style="max-width: 135px; min-width:135px;">Event Code</th>
                                                    <th style="max-width: 150px; min-width:150px;">Username</th>
                                                    <th style="max-width: 190px; min-width:190px;">Event Type</th>
                                                    <th style="max-width: 130px;">Event Location</th>
                                                    <th style="max-width: 300px; min-width: 300px;">Notes</th>
                                                    <th style="max-width: 60px; min-width:160px;">Trans. ID</th>
                                                    <!-- not shown -->
                                                    <th>Event ID</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <select hidden id="EventLogCols">
        <option value="Date">Date</option>
        <option value="Message">Message</option>
        <option value="Event Code">Event Code</option>
        <option value="Username">Username</option>
        <option value="Event Type">Event Type</option>
        <option value="Event Location">Event Location</option>
        <option value="Notes">Notes</option>
        <option value="Trans. ID">Trans. ID</option>
    </select>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/Event Log/EventLog.js"></script>
<script src="~/Scripts/Event Log/EventLogFilters.js"></script>
<script src="~/Scripts/Event Log/EventLogHub.js"></script>
<script src="~/Scripts/Event Log/EventLogShortcuts.js"></script>
@Html.Partial("~/Views/EventLog/EventLogDetailModal.vbhtml")