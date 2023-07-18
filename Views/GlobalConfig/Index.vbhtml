<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Global Configuration"
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
                                            <h1 style="color:#428bca;font-size:44px;" class="text-center"><i>PickPro Configuration</i></h1>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-offset-3">
                                        <form method="post" action="/GlobalConfig" role="form" id="LogonForm">
                                            <div class="form-group">
                                                <label for="username">Username</label>
                                                <input type="text" class="form-control" id="username" name="username" placeholder="Enter Username" maxlength="50">
                                            </div>
                                            <div class="form-group">
                                                <label for="password">Password</label>
                                                <input type="password" class="form-control" id="password" name="password" placeholder="Password" maxlength="50">
                                            </div>
                                            <button type="button" class="btn btn-primary pull-right" id="LogonSubmit">Submit</button>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/GlobalConfig/Logon.js"></script>
<script src="~/Scripts/Global/autocompleteoff.js"></script>
@If TempData.ContainsKey("Conn") And TempData("Conn") = False Then
    @<div class="modal fade" id="Config" tabindex="-1" role="dialog" data-backdrop="static" aria-labelledby="ConfigLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="ConfigLabel">Set Config Database</h4>
                </div>
                <div class="modal-body">
                    <form id="dbConfigForm" method="post" action="/GlobalConfig/setConfigDB">
                        @Html.AntiForgeryToken()
                        <div class="form-group">
                            <label>Server Name</label>
                            <input type="text" name="ServerName" placeholder="Sever Name" class="form-control" value="" />
                        </div>
                        <div class="form-group">
                            <label>Database Name</label>
                            <input type="text" name="DBName" placeholder="DB Name" class="form-control" value="" />
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button id="SubmitConfig" type="button" class="btn btn-primary" onclick="document.getElementById('dbConfigForm').submit()">Submit</button>
                </div>
            </div>
        </div>
    </div>
    @<script>
        $(document).ready(function () {
            $('#Config').modal({ keyboard: false });

            $('input[name="ServerName"], input[name="DBName"]').on('input', function () {
                var server = $('input[name="ServerName"]').val();
                var DB = $('input[name="DBName"]').val();
                if (server == '' || DB == '') {
                    $('#SubmitConfig').attr('disabled', 'disabled')
                }
                else {
                    $('#SubmitConfig').removeAttr('disabled')
                }
            })
        });
    </script>
End If