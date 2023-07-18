<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
@Code
    ViewData("Title") = "Preferences"
    ViewData("PageName") = "&nbsp; | &nbsp; Preferences"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Preferences
                    </h3>
                </div>
                <div class="panel-body" id="OMPrefPanel">
                    <div class="row">
                        <div class="col-md-2">
                            <strong>Maximum Orders to Retrieve: </strong><input type="text" id="MaxOrdersRet" oninput="setNumeric($(this))" class="form-control" value="@Model.Prefs.MaxOrders" />
                        </div>
                    </div>
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-3">
                            @If Model.Prefs.AllowInProc Then
                                @<b>Allow Release for In-Process Orders: </b>@<input type="checkbox" checked="checked" id="allowInProcOrders" />
                            Else
                                @<b>Allow Release for In-Process Orders: </b>@<input type="checkbox" id="allowInProcOrders" />
                            End If
                            
                        </div>
                        <div class="col-md-3">
                            @If Model.Prefs.AllowPartRel Then
                                @<b>Allow Release of Individual Order Lines: </b>@<input type="checkbox" checked="checked" id="allowIndivdOrders" />
                            Else
                                @<b>Allow Release of Individual Order Lines: </b>@<input type="checkbox" id="allowIndivdOrders" />
                            End If
                            
                        </div>
                        <div class="col-md-3">
                            @If Model.Prefs.DefUserFields Then
                                @<b>Use Default User Fields: </b>@<input type="checkbox" checked="checked" id="DefUserFields" />
                            Else
                                @<b>Use Default User Fields: </b>@<input type="checkbox" id="DefUserFields" />
                            End If
                        </div>
                        <div class="col-md-3">
                            @If Model.Prefs.PrintDirectly Then
                                @<label>Print Directly? <input type="checkbox" checked="checked" id="PrintDirect" /></label>
                            Else
                                @<label>Print Directly? <input type="checkbox" id="PrintDirect" /></label>
                            End If
                        </div>
                    </div>
                    <div class="row" style="padding-top:15px;">
                        <div class="col-md-6">
                            <strong>Enter the location of the custom Reports application: </strong><input type="text" id="CustReportsApp" class="form-control" value="@Model.Prefs.CustomReport"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <strong>Enter the location of the custom application to open from the Admin Menu: </strong><input type="text" id="CustReportsMenuApp" class="form-control"value="@Model.Prefs.CustomAdmin" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <strong>Enter the text for the custom application button on the Admin Menu: </strong><input type="text" id="CustReportsMenuText" class="form-control" value="@Model.Prefs.CustomAdminText"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/OrderManager/Scripts/Preferences/Preferences.js"></script>