<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Select Work"
    ViewData("PageName") = "&nbsp; | &nbsp; Select Work"
End Code

<div class="container-fluid">
    <input type="hidden" id="PrintDirect" value="@Model.WSPrefs("Print Direct").ToString().ToLower()" />
    @html.partial("CountPartial")
    <div class="row">
        <div class="col-md-8">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Ready to Select for Processing</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <button type="button" class="btn btn-primary" id="Assign">Auto Assign Work Batches</button>
                            <button type="button" class="btn btn-primary" id="SelectNext" style="display:none;">Select Next @Model.WSPrefs("Default Pick Batch")</button>
                        </div>
                    </div>
                   <div class="row top-spacer">
                       <div class="col-md-12">
                           <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="UnselectedTable">
                               <thead>
                                   <tr>
                                       <td>Type</td>
                                       <td name="WorkType">Batch Pick ID</td>
                                       <td>Req. Date</td>
                                       <td>Priority</td>
                                       <td>Lines</td>
                                       <td>Assigned To</td>
                                       <td>Detail</td>
                                   </tr>
                               </thead>
                               <tbody></tbody>
                           </table>
                       </div>
                   </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Selected for Processing</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                <ul style="margin-top:-10px;" role="menu" class="dropdown-menu">
                                    <li>
                                        <a href="#" id="PrintWork" class="Print-Report">Print Work List</a>
                                        <a href="#" id="PrintItem" class="Print-Report">Print Item Label</a>
                                        <a href="#" id="PrintTote" class="Print-Report">Print Tote Label</a>
                                    </li>
                                </ul>
                            </div>
                            <button type="button" class="btn btn-warning" data-toggle="tooltip" data-placement="top" data-original-title="Clear Selected & Refresh Orders" id="Refresh"><span class="glyphicon glyphicon-refresh"></span></button>
                            <button type="button" class="btn btn-danger" id="ClearBatch">Clear Batches</button>
                            @If Model.OrganizeWork Then
                                @<button type="button" class="btn btn-primary" id="AssignToMe">Assign Selected to Specified Worker</button>
                            Else
                                @<button type="button" class="btn btn-primary" id="AssignToMe">Assign Selected to Me</button>
                            End If
                            <button type="button" class="btn btn-primary" id="AssignBatch" style="display:none;">Assign Batch ID</button>
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-12">
                            <table class="table table-bordered table-condensed table-striped" style="background-color:white;" id="SelectedTable">
                                <thead>
                                    <tr>
                                        <td>Type</td>
                                        <td name="WorkType">Batch Pick ID</td>
                                        <td>Req. Date</td>
                                        <td>Priority</td>
                                        <td>Lines</td>
                                        <td>Assigned To</td>
                                        <td>Detail</td>
                                    </tr>
                                </thead>
                                <tbody></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/FilterMenu/FilterMenu.js"></script>
<script src="~/Areas/WorkManager/Scripts/SelectWork/SelectWork.js"></script>
@Html.Partial("~/Areas/WorkManager/Views/Modals/AssignWork.vbhtml")
@Html.Partial("~/Areas/WorkManager/Views/Modals/TransactionDetail.vbhtml")