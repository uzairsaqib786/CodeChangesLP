<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
<div class="row">
    <div class="col-md-12">
        <div id="reprocedTableSize" class="col-md-12">
            <div class="row" style="margin-left:10px; margin-bottom:0">
                <div class="col-md-2" style="margin-top:20px">
                    <label># Entries</label>
                    <select id="pageLength5" class="form-control" style="width:auto; display:inline;">
                        <option>10</option>
                        <option>15</option>
                        <option>20</option>
                        <option>25</option>
                        <option>50</option>
                        <option selected="selected">100</option>
                    </select>
                    <button id="setDefaultReproced" type="button" data-toggle="tooltip" data-placement="top" title="Set Column Sequence" class="btn btn-primary">
                        <span class="glyphicon glyphicon-list"></span>
                    </button>
                </div>
                <div class="col-md-5 pull-right" style="margin-top:20px;" id="searchStringTypeAhead">
                    <label class="pull-right">
                        Search
                        <select id="selection5" class="form-control" style="display:inline;width:auto;" aria-controls="example">
                            <option value=" "> </option>
                            @For f As Integer = 0 To Model.reprocessedCols.Count - 1
                                If Model.itemNumber <> "" And Model.reprocessedCols(f) = "Item Number" Then
                                    @<option selected="selected" value="@Model.reprocessedCols(f)">@Model.reprocessedCols(f)</option>
                                Else
                                    @<option value="@Model.reprocessedCols(f)">@Model.reprocessedCols(f)</option>
                                End If
                            Next
                        </select>
                        By
                        <input id="searchString5" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" value="@Model.itemNumber" />
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="reprocedTransTable" style="background-color:white;" class="table table-bordered table-striped table-condensed datatable table-hover">
                        <thead>
                            <tr>
                                @For x As Integer = 0 To Model.reprocessedCols.Count - 1
                                    If (Model.reprocessedCols(x).Contains("Number")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Number", "#")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Transaction")) Then
                                        Dim str = Model.reprocessedCols(x).Replace("Transaction", "Trans")
                                        If (str.Contains("Quantity")) Then
                                            str = str.Replace("Quantity", "Qty")
                                        End If
                                        @<th>@str</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Filename")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Filename", "File")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("File Name")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("File Name", "File")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Unit of Measure")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Unit of Measure", "UOM")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Required")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Required", "Req")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Sequence")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Sequence", "Seq")</th>
                                    ElseIf (Model.reprocessedCols(x).Contains("Quantity")) Then
                                        @<th>@Model.reprocessedCols(x).Replace("Quantity", "Qty")</th>
                                    Else
                                        @<th>@Model.reprocessedCols(x)</th>
                                    End If
                                Next
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>
