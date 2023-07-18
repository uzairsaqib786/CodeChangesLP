<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.TransactionsModel
@code
    Dim dateReturn As DateTime
    Dim edateReturn As DateTime
    Dim sreturnString As String = ""
    Dim ereturnString As String = ""
    If Model.defaultValues.Count() > 0 And Model.toShow = "Transaction History" Then
        If (Model.defaultValues(0) <> "") Then
            dateReturn = DateTime.Parse(Model.defaultValues(0))
            sreturnString = dateReturn.ToString("MM/dd/yyyy")
        Else
            sreturnString = Now.AddYears(-50).ToString("MM/dd/yyyy")
        End If
        If Model.defaultValues(1) <> "" Then
            edateReturn = DateTime.Parse(Model.defaultValues(1))
            ereturnString = edateReturn.ToString("MM/dd/yyyy")
        Else
            sreturnString = Now.ToString("MM/dd/yyyy")
        End If
    End If
    sreturnString = Now.AddYears(-50).ToString("MM/dd/yyyy")
    ereturnString = Now.ToString("MM/dd/yyyy")
End Code
    <div id="TransView">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading clearfix">
                        <h4 class="panel-title pull-left">Filters</h4>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Completed Date (Start)</label>
                                    <input type="text" Class="form-control input-sm date-picker" id="sDateFilterTrans" value="@sreturnString" maxlength="50" />
                                </div>
                            </div>
                            <div Class="col-md-4">
                                <div Class="form-group">
                                    <Label>End Date</label>
                                    <input type = "text" Class="form-control input-sm date-picker" id="eDateFilterTrans" value="@ereturnString" maxlength="50" />
                                </div>
                            </div>
                            <div Class="col-md-4">
                                <div Class="form-group">
                                    <Label> Order Number</label>
                                    <div id = "transTypeAhead" Class="form-group">
                                        <!--Textbox for the order number input-->
                                        @If Model.toShow = "Transaction History" Then
                                            @<input type = "text" Class="form-control input-sm typeahead" id="orderNumberFilterTrans" placeholder="Order Number" value="@Model.defaultValues(2)" maxlength="50">
                                        Else
                                            @<input type = "text" Class="form-control input-sm typeahead" id="orderNumberFilterTrans" placeholder="Order Number" maxlength="50">
                                        End If
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div Class="row">
                            <div Class="col-md-4">
                                <div Class="form-group">
                                    <Button Class="btn btn-primary pull-left" id="resetDateTrans">Reset to <u>T</u>oday's Date</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Added select for searching by column -->
        <div Class="row">
            <div Class="col-xs-6">
                <Label># Entries</label>
                <select id = "pageLength3" Class="form-control" style="width:auto; display:inline;">
                    <option>10</Option>
                    <option>15</Option>
                    <option>20</Option>
                    <option>25</Option>
                    <option>50</Option>
                    <option>100</Option>
                </select>
                <Button id = "setDefaultHist" type="button" data-toggle="tooltip" data-placement="top" title="Set Column Sequence" Class="btn btn-primary">
                    <span Class="glyphicon glyphicon-list"></span>
                </button>
            </div>
            <div Class="col-xs-6" id="searchStringTypeAhead">
                <Label Class="pull-right">
                    Search
                    <select id = "selection3" Class="form-control " style="display:inline;width:auto;" aria-controls="example">
                        <option value = " " > </option>
                        @For f As Integer = 0 To Model.transColumns.Count - 1
                            If Model.itemNumber <> "" And Model.transColumns(f) = "Item Number" Then
                                @<option selected="selected" value="@Model.transColumns(f)">@Model.transColumns(f)</option>
                            ElseIf Model.Location <> "" And Model.transColumns(f) = "Location" Then
                                @<option selected="selected" value="@Model.transColumns(f)">@Model.transColumns(f)</option>
                            Else
                            @<option value="@Model.transColumns(f)">@Model.transColumns(f)</option>
                            End If
                        Next
                    </select>
                    By
                    <input id="searchString3" class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" value="@IIf(Model.itemNumber = "", Model.Location, Model.itemNumber)" />
                </label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table id="transHistTable" class="table table-bordered table-striped" cellspacing="0" role="grid" style="background-color:white;">
                    <thead>
                        <tr>
                            @For x As Integer = 0 To Model.transColumns.Count - 1
                                If (Model.transColumns(x).Contains("Number")) Then
                                @<th>@Model.transColumns(x).Replace("Number", "#")</th>
                                ElseIf (Model.transColumns(x).Contains("Transaction")) Then
                                    Dim str = Model.transColumns(x).Replace("Transaction", "Trans")
                                    If (str.Contains("Quantity")) Then
                                        str = str.Replace("Quantity", "Qty")
                                    End If
                                @<th>@str</th>
                                ElseIf (Model.transColumns(x).Contains("Filename")) Then
                                @<th>@Model.transColumns(x).Replace("Filename", "File")</th>
                                ElseIf (Model.transColumns(x).Contains("File Name")) Then
                                @<th>@Model.transColumns(x).Replace("File Name", "File")</th>
                                ElseIf (Model.transColumns(x).Contains("Unit of Measure")) Then
                                @<th>@Model.transColumns(x).Replace("Unit of Measure", "UOM")</th>
                                ElseIf (Model.transColumns(x).Contains("Required")) Then
                                @<th>@Model.transColumns(x).Replace("Required", "Req")</th>
                                ElseIf (Model.transColumns(x).Contains("Sequence")) Then
                                @<th>@Model.transColumns(x).Replace("Sequence", "Seq")</th>
                                ElseIf (Model.transColumns(x).Contains("Quantity")) Then
                                @<th>@Model.transColumns(x).Replace("Quantity", "Qty")</th>
                                Else
                                @<th>@Model.transColumns(x)</th>
                                End If
                            Next
                        </tr>
                    </thead>
                </table>
                <div>
                    <div class="col-md-12">
                        <div class="col-xs-1 col-xs-offset-10">
                            <button class="dataTables_paginate btn btn-secondary" id="page20LeftTH" style="margin-bottom:10px; margin-left:30px; background-color:white; color:#2a6496; border-color:white;"><< 20</button>
                        </div>
                        <div class="col-xs-1">
                            <button class="dataTables_paginate btn btn-secondary pull-right" id="page20RightTH" style="margin-bottom:10px; margin-right:30px; background-color:white; color:#2a6496; border-color:white;">20 >></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
