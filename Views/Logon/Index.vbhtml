<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@modeltype List(Of String)
@Code
    ViewData("Title") = "Logon"
    If Model.Count = 0 Then
        Model.Add("")
        Model.Add("")
        Model.Add("")
        Model.Add("")
        Model.Add("")
    End If
End Code
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div id="Errors" class="col-md-12">
                    @If ViewData.ContainsKey("Error") Then
                        @<div class="alert alert-warning alert-dismissible" role="alert">
                            <button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <strong>Error:</strong> @ViewData("Error")
                        </div>
                    End If
                    <div id="logon_alert">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h1 style="color:#428bca;font-size:44px;" class="text-center"><i>PickPro WCS</i></h1>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3 class="text-center">@Model(0)</h3>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="text-center">@Model(1)</h4>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="text-center">@Model(2), @Model(3)</h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <form method="post" action="/Logon/" role="form" id="LogonForm" autocomplete="off">
                                                        <div class="form-group">
                                                            <label for="username">Username</label>
                                                            <input type="text" class="form-control" id="username" name="username" placeholder="Enter Username" maxlength="100" autocomplete="off">
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="password">Password</label>
                                                            <input type="password" class="form-control" id="password" name="password" placeholder="Password" maxlength="50" autocomplete="off">
                                                        </div>
                                                        <input type="hidden" name="ReturnUrl" value="@ViewData("ReturnURL")" />
                                                        <button type="button" id="LogonSubmit" class="btn btn-primary pull-right">Logon</button>
                                                    </form>

                                                    @If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Or (PickPro_Web.Config.getSecurityEnvironment.Contains("AD") AndAlso PickPro_Web.Config.getSecurityEnvironment = "ADLDS") Then
                                                        @<a data-toggle="modal" data-target="#myModal" style="margin-right:5px;" Class="" id="changePass">Change Password</a>
                                                    End If

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            @If Model(4) <> "NO EXTENSION FOUND" Then
                                @<div class="row">
                                    <div class="text-center col-md-12">
                                        <img src="/images/CompanyLogo/logo@(Model(4))" alt="Company Logo" />
                                    </div>
                                </div>
                            End If
                        </div>
                    </div>
                    <div class="row">
                        <p class="text-center">DB Connection: <a data-toggle="modal" data-target="#Config" onclick="$('#cancelConfig').show()">@Session("ConnectionString")</a></p>
                        @If PickPro_Web.GlobalFunctions.Testing Then
                            @<p Class="text-center">TEST MODE</p>
                        End If
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Modal -->
@If Not PickPro_Web.Config.getSecurityEnvironment.Contains("AD") Or (PickPro_Web.Config.getSecurityEnvironment.Contains("AD") AndAlso PickPro_Web.Config.getSecurityEnvironment = "ADLDS") Then
    @<div class="modal fade" id="myModal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width:900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" id="myModalLabel">Password Change</h4>
                </div>
                <div class="modal-body">
                    <div id="changepass_alert">
                    </div>
                    <div class="form-group">
                        <label>Username</label>
                        <input type="text" class="form-control" id="user_passwordChange" placeholder="Enter Username" maxlength="50">
                    </div>
                    <div class="form-group">
                        <label>Old Password</label>
                        <input type="password" class="form-control" id="password_passwordChange" placeholder="Old Password" maxlength="16">
                    </div>
                    <div class="form-group">
                        <label>New Password</label>
                        <input type="password" class="form-control" id="newPassword_passwordChange" placeholder="New Password" maxlength="16">
                    </div>
                    <div class="form-group">
                        <label>Confirm New Password</label>
                        <input type="password" class="form-control" id="newPassword2_passwordChange" placeholder="New Password" maxlength="16">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button id="passwordChange_submit" type="button" class="btn btn-warning">Save Password</button>
                </div>
            </div>
        </div>
    </div>
End If
<div class="modal fade" id="Config" tabindex="-1" role="dialog" data-backdrop="static" aria-labelledby="ConfigLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="ConfigLabel">Select a Database Connection</h4>
            </div>
            <div class="modal-body">
                <ul>
                    @For Each connection In TempData("Connections")
                        @<li><button class="btn btn-primary connectionName" style="margin-bottom:5px;">@connection.name</button></li>
                    Next
                </ul>
            </div>
            <div class="modal-footer">
                @If Not TempData("Conn") = False Then
                    @<button id="cancelConfig" class="btn btn-default" hidden="hidden" data-dismiss="modal">Cancel</button>
                End If
                <button type="button" class="btn btn-primary" onclick="document.getElementById('dbConfigForm').submit()" style="display:none;">Submit</button>
            </div>
        </div>
    </div>
</div>
@If TempData.ContainsKey("Conn") And TempData("Conn") = False Then
    @<script>
         $(document).ready(function () {
             $('#Config').modal({ keyboard: false });
         })
    </script>
End If
<script src="~/Scripts/Logon/Logon.js"></script>
<script src="~/Scripts/Global/autocompleteoff.js"></script>