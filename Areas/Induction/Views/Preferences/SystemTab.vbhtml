<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row top-spacer">
                    <div class="col-md-6">
                        @If Model.Prefs.UseDefFilter Then
                            @<input type="radio" data-toggle="tooltip" title="Sets if custom filter or zone is to be used in pick batch manager" name="BatchBy" id="UseDefFilter" checked="checked" /> @<strong>Use Default Filter</strong>
                        Else
                            @<input type="radio" data-toggle="tooltip" title="Sets if custom filter or zone is to be used in pick batch manager" name="BatchBy" id="UseDefFilter" /> @<strong>Use Default Filter</strong>
                        End If
                    </div>
                    <div class="col-md-6">
                        @If Model.Prefs.UseDefZone Then
                            @<input type="radio" data-toggle="tooltip" title="Sets if custom filter or zone is to be used in pick batch manager" name="BatchBy" id="UseDefZone" checked="checked" /> @<strong>Use Default Zone</strong>
                        Else
                            @<input type="radio" data-toggle="tooltip" title="Sets if custom filter or zone is to be used in pick batch manager" name="BatchBy" id="UseDefZone" /> @<strong>Use Default Zone</strong>
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Default Batch Qty: </b><input type="text" data-toggle="tooltip" title="Number of totes per batch" id="DefBatchQty" oninput="setNumeric($(this))" class="form-control" value="@Model.Prefs.PickBatchQuant" />
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Default Cells: </b><input type="text" data-toggle="tooltip" title="Number of put away transactions allowed per tote" id="DefCells" oninput="setNumeric($(this))" class="form-control" value="@Model.Prefs.DefCells" />
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Shorting Methodology: </b>
                        <select id="ShortMethod" class="form-control">
                            @select Case Model.Prefs.ShortMehtod
                                Case "Complete"
                                    @<option value="Complete" selected>Complete Short</option>
                                    @<option value="Markout">Send to Markout</option>
                                    @<option value="Deallocate">Split and Deallocate</option>
                                Case "Markout"
                                    @<option value="Complete">Complete Short</option>
                                    @<option value="Markout" selected>Send to Markout</option>
                                    @<option value="Deallocate">Split and Deallocate</option>
                                Case "Deallocate"
                                    @<option value="Complete">Complete Short</option>
                                    @<option value="Markout">Send to Markout</option>
                                    @<option value="Deallocate" selected>Split and Deallocate</option>
                            End Select
                        </select>
                    </div>
                    <div Class="col-md-12 top-spacer">
                        @If Model.Prefs.SelIfOne Then
                            @<b>Select if One: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if transaction select screen is opened if only one transaction exists. If Checked, will auto select an existing transaction if it is the last one" checked="checked" id="SelIfOne" />
                        Else
                            @<b>Select if One: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if transaction select screen is opened if only one transaction exists. If Checked, will auto select an existing transaction if it is the last one" id="SelIfOne" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.ValTotes Then
                            @<b>Validate Totes: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if Tote IDs need to be validated" checked="checked" id="ValTotes" />
                        Else
                            @<b>Validate Totes: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if Tote IDs need to be validated" id="ValTotes" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoForReplen Then
                            @<b>Auto Forward Replenish: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Auto Forward Replenishments will be displayed" checked="checked" id="AutoForwardReplen" />
                        Else
                            @<b>Auto Forward Replenish: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if Auto Forward Replenishments will be displayed" id="AutoForwardReplen" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.CreateItemMast Then
                            @<b>Create Item Master: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if new items can be created if they do not exist from put away batch manager" checked="checked" id="CreateItemMaster" />
                        Else
                            @<b>Create Item Master: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if new items can be created if they do not exist from put away batch manager" id="CreateItemMaster" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.SAPLocTrans Then
                            @<b>SAP Location Transactions: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if SAP location transactions are created" checked="checked" id="SAPLocTrans" />
                        Else
                            @<b>SAP Location Transactions: </b>@<input type="checkbox" data-toggle="tooltip" title="Toggles if SAP location transactions are created" id="SAPLocTrans" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.StripScan Then
                            @<b>Strip Scan: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if characters are to be removed from scans" checked="checked" id="StripScan" />
                        Else
                            @<b>Strip Scan: </b>@<input type="checkbox" data-toggle="tooltip" title="Controls if characters are to be removed from scans" id="StripScan" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.StripScan Then
                            @<div class="row" id="StripScanRow">
                                <div class="col-md-6">
                                    <strong>Strip Side:</strong>
                                    <select class="form-control" data-toggle="tooltip" title="Remove characters from left or right side of scan" id="StripSide">
                                        @code
                                        Dim StripSideList As List(Of String) = New List(Of String) From {"Left", _
                                             "Right"}
                                        For Each side In StripSideList
                                        If Model.Prefs.StripSide = side Then
                                            @<option value="@side" selected>@side</option>
                                        Else
                                            @<option value="@side">@side</option>
                                        End If
                                        Next
                                        End Code
                                    </select>

                                </div>
                                <div class="col-md-6">
                                    <b>Strip Number: </b><input type="text" data-toggle="tooltip" title="Number of characters to remove" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int.max)" id="StripNumber" class="form-control" value="@Model.Prefs.StripNum" />
                                </div>
                            </div>
                        Else
                            @<div class="row" id="StripScanRow" hidden>
                                <div class="col-md-6">
                                    <strong>Strip Side:</strong>
                                    <select class="form-control" data-toggle="tooltip" title="Remove characters from left or right side of scan" id="StripSide">
                                        @code
                                        Dim StripSideList As String() = {"Left", "Right"}
                                        For Each side In StripSideList
                                        If Model.Prefs.StripSide.ToString().ToLower() = side.ToLower() Then
                                        @<option value="@side" selected>@side</option>
                                        Else
                                        @<option value="@side">@side</option>
                                        End If
                                        Next
                                        End Code
                                    </select>
                                </div>
                                <div class="col-md-6">
                                    <b>Strip Number: </b><input type="text" data-toggle="tooltip" title="Number of characters to remove" oninput="setNumericInRange($(this), 0, SqlLimits.numerics.int.max)" id="StripNumber" class="form-control" value="@Model.Prefs.StripNum" />
                                </div>
                            </div>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>