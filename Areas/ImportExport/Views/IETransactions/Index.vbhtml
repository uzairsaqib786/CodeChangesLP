<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(of string, object)
@Code
    ViewData("Title") = "Transactions"
End Code
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Filters
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label>Data Source</label>
                            <select class="form-control" id="TableSelect">
                                @For Each table As String In {"Archive Transaction History", "Import Transactions", "Import Transactions History", "Export Transactions"}
                                    @<option @(IIf(table.ToLower() = Model("Init Table").ToLower(), "selected", ""))>@table</option>
                                Next
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label>Start Date</label>
                            <input type="text" class="form-control date-picker" id="SDate" value="@Model("Min Date")" />
                        </div>
                        <div class="col-md-3">
                            <label>End Date</label>
                            <input type="text" class="form-control date-picker" id="EDate" value="@DateTime.Now.ToString("MM/dd/yyyy")" />
                        </div>
                        <div class="col-md-3">
                            <label style="visibility:hidden;" class="btn-block">Reset</label>
                            <button class="btn btn-primary" onclick="$('#SDate,#EDate').val('@(DateTime.Now.ToString("MM/dd/yyyy"))')">Reset to Today's Date</button>
                            @Code
                                Dim display As String = IIf(Model("Init Table").ToLower() = "import transactions" Or Model("Init Table").ToLower() = "export transactions", "", "style=display:none;")
                                Dim type As String = IIf(Model("Init Table").ToLower() = "import transactions", "Import", "Export")
                            End Code
                            <button class="btn btn-danger" id="DeleteTrans" @display>Delete @type Transactions</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            @Code
                Dim importCols As New List(Of String) From {"Transaction Type", "Import Date", "Order Number", "Item Number", "Description", "Transaction Quantity", "Priority", "Required Date",
                        "Line Number", "Serial Number", "Lot Number", "Expiration Date", "Batch Pick ID",
                        "Unit of Measure", "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7",
                        "User Field8", "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID",
                        "Zone", "Warehouse", "Import By", "Import Filename", "Notes", "Emergency"}
                Dim otherCols As New List(Of String) From {"Transaction Type", "Import Date", "Order Number", "Item Number", "Description", "Transaction Quantity", "Priority", "Required Date",
                        "Line Number", "Serial Number", "Lot Number", "Expiration Date", "Completed Quantity", "Completed Date", "Completed By", "Batch Pick ID",
                        "Unit of Measure", "User Field1", "User Field2", "User Field3", "User Field4", "User Field5", "User Field6", "User Field7",
                        "User Field8", "User Field9", "User Field10", "Revision", "Tote ID", "Tote Number", "Cell", "Host Transaction ID", "ID",
                        "Zone", "Carousel", "Row", "Shelf", "Bin", "Warehouse", "Inv Map ID", "Import By", "Import Filename", "Notes", "Emergency",
                        "Master Record", "Master Record ID", "Export Batch ID", "Export Date", "Export File Name", "Exported By", "Status Code"}
                Dim tables As New Dictionary(Of String, List(Of String)) From {{"Import Transactions", importCols}, _
                                                                               {"Import Transactions History", importCols}, _
                                                                               {"Export Transactions", otherCols}, _
                                                                               {"Archive Transaction History", otherCols}}
                Dim FieldTypes As New Dictionary(Of String, String) From {{"Archive_ID", "Number"}, {"TH_ID", "Number"}, {"ID", "Number"}, {"Import Date", "Date"}, {"Import By", "Text"}, _
                        {"Import Filename", "Text"}, {"Transaction Type", "Text"}, {"Order Number", "Text"}, {"Priority", "Number"}, {"Item Number", "Text"}, {"Revision", "Text"}, _
                        {"Lot Number", "Text"}, {"Expiration Date", "Date"}, {"Serial Number", "Text"}, {"Description", "Text"}, {"Transaction Quantity", "Number"}, {"Location", "Text"}, _
                        {"Warehouse", "Text"}, {"Zone", "Text"}, {"Carousel", "Text"}, {"Row", "Text"}, {"Shelf", "Text"}, {"Bin", "Text"}, {"Completed Date", "Date"}, _
                        {"Completed By", "Text"}, {"Completed Quantity", "Number"}, {"Batch Pick ID", "Text"}, {"Notes", "Text"}, {"Export File Name", "Text"}, {"Export Date", "Date"}, _
                        {"Exported By", "Text"}, {"Export Batch ID", "Text"}, {"Line Number", "Number"}, {"Line Sequence", "Number"}, {"Table Type", "Text"}, {"User Field1", "Text"}, _
                        {"User Field2", "Text"}, {"User Field3", "Text"}, {"User Field4", "Text"}, {"User Field5", "Text"}, {"User Field6", "Text"}, {"User Field7", "Text"}, _
                        {"User Field8", "Text"}, {"User Field9", "Text"}, {"User Field10", "Text"}, {"Unit of Measure", "Text"}, {"Required Date", "Date"}, {"Status Code", "Text"}, _
                        {"Master Record", "Bool"}, {"Master Record ID", "Number"}, {"Inv Map ID", "Number"}, {"Label", "Bool"}, {"Tote ID", "Text"}, {"Tote Number", "Number"}, _
                        {"Cell", "Text"}, {"Host Transaction ID", "Text"}, {"Emergency", "Bool"}, {"Archive Date", "Date"}, {"Import_ID", "Text"}, {"Transfer Status", "Text"}, _
                        {"Status Message", "Text"}, {"ImpHist_ID", "Number"}, {"Export_ID", "Number"}, {"Induction Location", "Text"}, {"Induction Date", "Date"}, {"Induction By", "Text"}, _
                        {"OT ID", "Number"}, {"Date Stamp", "Date"}, {"FIFO Date", "Date"}}
            End Code
            @For Each t As String In tables.Keys
                @<div data-table-div="@t" @(IIf(t.ToLower() <> Model("Init Table").ToLower(), "style=display:none;", ""))>
                    <table class="table table-bordered table-striped table-hover table-condensed" data-table-name="@t" style="background-color:white;">
                        <thead>
                            <tr>
                                @For Each s As String In tables(t)
                                    Dim fieldType As String = ""
                                    Try
                                        fieldType = FieldTypes(s)
                                    Catch ex As Exception
                                        fieldType = "Text"
                                    End Try
                                        @<th data-field-type="@fieldType">@s</th>
                                Next
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            Next
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Areas/ImportExport/Scripts/Transactions/DataTable.js"></script>
<script src="~/Areas/ImportExport/Scripts/Transactions/Transactions.js"></script>