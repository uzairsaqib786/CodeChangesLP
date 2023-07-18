<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype PickPro_Web.CycleCountModel
@Code
    Dim colName = ""
End Code

<div class="modal fade" id="FieldMapModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="FieldMapModal" aria-hidden="true">
    <div class="modal-dialog" style="width:1200px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="FieldMapLabel">Audit File Field Mapping</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <label><b>Backup File Path:</b></label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-10">
                        <input type="text" class="form-control input-sm" id="FieldMapBackup" value="@Model.ModalData.Backup" maxlength="50" /><br>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="TransDataTable">
                            <thead>
                                <tr>
                                    <th>Transaction Type</th>
                                    <th>Import File Path</th>
                                    <th>Import File Extension</th>
                                    <th>Active</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    @For x As Integer = 0 To Model.ModalData.TransData.Count() - 1
                                        If x = 1 Then
                                        @<td><input type="text" class="form-control input-sm" id="FieldMapFilePath" value="@Model.ModalData.TransData(x)" maxlength="50" /></td>
                                        ElseIf x = 2 Then
                                        @<td><input type="text" class="form-control input-sm" id="FieldMapFileExten" value="@Model.ModalData.TransData(x)" maxlength="50" /></td>
                                        ElseIf x = 3 Then
                                            If Model.ModalData.TransData(x) = "True" Then
                                        @<td><input type="checkbox" id="FieldMapActive" checked /></td>
                                            Else
                                        @<td><input type="checkbox" id="FieldMapActive" /></td>
                                            End If
                                        Else
                                        @<td>@Model.ModalData.TransData(x)</td>
                                        End If
                                    Next
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-md-12">
                        <table class="table table-bordered table-striped table-condensed" style="background-color:white;" id="FieldMapTable">
                            <thead>
                                <tr>
                                    <th>Pick Pro Field Name</th>
                                    <th>Start Position</th>
                                    <th>Field Length</th>
                                    <th>End Position</th>
                                    <th>Pad Field from Left</th>
                                    <th>Field Type</th>
                                    <th>Import Format</th>
                                </tr>
                            </thead>
                            <tbody>
                                @For Each row As List(Of String) In Model.ModalData.TableInfo
                                    @<tr>
                                        @For x As Integer = 0 To row.Count() - 1
                                        colName = row(0).Replace(" ", "")
                                        If x = 0 Then
                                            @<td>@row(x)</td>
                                        ElseIf x = 1 Then
                                            @<td><input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control input-sm field-map-input" id="@(colName & "_start")" value="@row(x)" data-id="@("#" & colName)" /></td>
                                        ElseIf x = 2 Then
                                            @<td><input type="text" oninput="setNumericInRange($(this), SqlLimits.numerics.int)" class="form-control input-sm field-map-input" id="@(colName & "_length")" value="@row(x)" data-id="@("#" & colName)" /></td>
                                        ElseIf x = 3 Then
                                            @<td><input type="text" class="form-control input-sm" id="@(colName & "_end")" value="@row(x)" data-id="@("#" & colName)" disabled /></td>
                                        ElseIf x = 4 Then
                                            If row(x) = "False" Then
                                            @<td><input type="checkbox" id="@(colName & "_pad")" /></td>
                                            Else
                                            @<td><input type="checkbox" id="@(colName & "_pad")" checked /></td>
                                            End If
                                        ElseIf x = 5 Then
                                            @<td>@row(x)</td>
                                        Else
                                            @<td><input maxlength="50" type="text" class="form-control input-sm" id="@(colName & "_format")" /></td>
                                        End If
                                        Next
                                    </tr>
                                Next
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" data-dismiss="modal" id="FieldMapModalSave">Save</button>
                <button type="button" class="btn btn-default" data-dismiss="modal" id="scan_dismiss">Close</button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/FieldMapModal.js"></script>