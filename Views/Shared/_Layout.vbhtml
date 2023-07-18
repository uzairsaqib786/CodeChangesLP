<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
    Dim Apps = PickPro_Web.Config.AppLicenses.ToList().OrderBy(Function(a)
                                                                       Return a.Value.Info.DisplayName
                                                               End Function)
End Code
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta name="mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link rel="icon" href="~/Content/favicon.ico">
    <link rel="shortcut icon" href="~/Content/ST.ico" />
    <link href="~/Content/jquery.contextMenu.css" rel="stylesheet" />
    <title>PickPro @ViewData("App") @ViewData("Title")</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/jquery")
    <script src="/Scripts/bootstrap.js"></script>
    <script src="~/Scripts/FilterMenu/jquery.ui.position.js"></script>
    <script src="~/Scripts/FilterMenu/jquery.contextMenu.js"></script>
    <script src="~/Scripts/jquery.signalR-2.2.0.min.js"></script>
    <script src="~/signalr/hubs"></script>
    <script src="~/Scripts/Global/KeyOverride.js"></script>
    <script src="/Scripts/Global/GlobalHub.js"></script>
    <script src="/Scripts/Modals/GlobalModal.js"></script>
    <script src="~/Scripts/Global/IdleTimer.js"></script>
    <script src="~/Scripts/Global/TestPrint.js"></script>

    <script src="~/Scripts/handlebars.js"></script>
    <script src="~/Scripts/typeahead.bundle.js"></script>
    <script src="~/Scripts/moment.min.js"></script>
    <script src="~/Scripts/bootstrap-datetimepicker.js"></script>
    <script src="~/Scripts/jquery.numpad.js"></script>
    <script src="~/Scripts/scotttech.numpad.js"></script>

    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/jquery-ui.css" rel="stylesheet" />
    <link href="~/Content/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" />
    <link href="~/Content/PickProCustom.css" rel="stylesheet" />
    <link href="~/Content/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="~/Content/typeahead-style.css" rel="stylesheet" />
    <link rel="stylesheet" href="~/Content/jquery.numpad.css">
    <link rel="stylesheet" href="~/Content/scotttech.numpad.css" />
</head>
<body class="bootstrap-body body-fixed">
    <div class="navbar navbar-thin navbar-grey navbar-fixed-top" role="navigation">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <ul class="nav navbar-nav" style="float:left;margin:0;">
                    <li id="appMenu" class="dropdown">
                        @If ViewData("popup") Then
                            @<a onclick="window.opener.location.href = '/'; window.close();" style="height:45px;width:45px;background-image:url('/Content/Scotttech.png')" href="#" class="dropdown-toggle" data-toggle="dropdown"></a>
                        ElseIf ViewData("override_back") <> "" Then
                            @<a onclick="@ViewData("override_back")" style="height:45px;width:45px;background-image:url('/Content/Scotttech.png')" href="#" class="dropdown-toggle" data-toggle="dropdown"></a>
                        Else
                            @<a id="goToAppMenu" style="height:45px;width:45px;background-image:url('/Content/Scotttech.png')" href="#" class="dropdown-toggle" data-toggle="dropdown"></a>
                        End If
                        @If Request.IsAuthenticated Then
                            @<ul class="dropdown-menu" role="menu">
                                @For Each availApp In Apps
                                If availApp.Value.isLicenseValid AndAlso PickPro_Web.Config.getWSAppPermission(Session("WSID"), availApp.Key) Then
                                    @<li>
                                        @If ViewData("popup") Then
                                            @<a onclick="window.opener.location.href='/@availApp.Value.Info.URL'; window.close();"><span class="glyphicon text-primary glyphicon-th-large"></span><strong>@availApp.Value.Info.DisplayName</strong></a>
                                        ElseIf ViewData("override_back") <> "" Then
                                            @<a onclick="@ViewData("override_back")"><span class="glyphicon text-primary glyphicon-th-large"></span><strong>@availApp.Value.Info.DisplayName</strong></a>
                                        Else
                                            @<a href="/@availApp.Value.Info.URL"><span class="glyphicon text-primary glyphicon-th-large"></span><strong>@availApp.Value.Info.DisplayName</strong></a>
                                        End If
                                    </li>
                                End If
                                Next
                            </ul>
                        End If
                    </li>
                </ul>
                @If ViewData("popup") Then
                    @<a id="showTest" class="navbar-brand pull-left" style="padding-left:20px;" onclick="window.opener.location.href='/@ViewData("AppHome")';window.close();">PickPro @ViewData("App")</a>
                ElseIf ViewData("override_back") <> "" Then
                    @<a id="showTest" class="navbar-brand pull-left" style="padding-left:20px;" onclick="@ViewData("override_back")">PickPro @ViewData("App")</a>
                Else
                    @<a id="showTest" class="navbar-brand pull-left" style="padding-left:20px;" href="/@ViewData("AppHome")">PickPro @ViewData("App")</a>
                End If
                <p id="PageName" class="navbar-PageName pull-left hidden-xs">@Html.Raw(ViewData("PageName"))</p>
            </div>
            <div class="navbar-collapse collapse">
                @If Request.IsAuthenticated And Not ViewData.ContainsKey("CONFIG") Then
                    @<form class="navbar-form navbar-right">
                        @If ViewData("popup") Then
                            @<a id="Back" onclick="window.close()" class="btn btn-primary">Back</a>
                        ElseIf ViewData("override_back") <> "" Then
                            @<a id="Back" onclick="@ViewData("override_back")" class="btn btn-primary">Back</a>
                        Else
                            @<a id="Back" onclick="history.back()" class="btn btn-primary">Back</a>
                        End If
                    </form>
                    @*@<ul class="nav navbar-right navbar-nav">
                            @If ViewData("popup") Then
                                @<li data-toggle="tooltip" data-placement="bottom" title="Back"><a onclick="window.close()"><span class="fa fa-undo"></span></a></li>
                            ElseIf ViewData("override_back") <> "" Then
                                @<li data-toggle="tooltip" data-placement="bottom" title="Back"><a onclick="@ViewData("override_back")"><span class="fa fa-undo"></span></a></li>
                            Else
                                @<li data-toggle="tooltip" data-placement="bottom" title="Back"><a onclick="history.back()"><span class="fa fa-undo"></span></a></li>
                            End If
                        </ul>*@
                    @<ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                            <a style="max-height:45px;" href="#" class="dropdown-toggle" data-toggle="dropdown">@User.Identity.Name<span class="caret"></span></a>
                            <ul class="dropdown-menu" role="menu">
                                <li><a href="#" style="font-size:18px;">Support Website</a></li>
                                @RenderSection("HelpLink", False)
                                <li><a style="font-size:18px;">Database: <strong class="text-success">@Session("ConnectionString")</strong></a></li>
                                @If PickPro_Web.STEService.ServiceStatus() Then
                                    @<li><a style="font-size:18px;">STE <strong style="font-size: 18px;color: green;" id="STEServiceOnline">Online</strong></a></li>
                                Else
                                    @<li><a style="font-size:18px;">STE <strong style="font-size: 18px;color: red;" id="STEServiceOnline">Offline</strong></a></li>
                                End If
                                @If PickPro_Web.PrintService.PrintServiceRunning Then
                                    @<li><a style="font-size:18px;">Printing <strong style="font-size: 18px;color: green;" id="PrintServiceOnline">Online</strong></a></li>
                                Else
                                    @<li><a style="font-size:18px;">Printing <strong style="font-size: 18px;color: red;" id="PrintServiceOnline">Offline</strong></a></li>
                                End If
                                <li><a id="PrinterConfig" style="font-size:18px;">Printer Setup</a></li>
                                <li><a id="TestPrintList" style="font-size:18px;">Test Print Lists</a></li>
                                <li><a id="TestPrintLabel" style="font-size:18px;">Test Print Labels</a></li>
                                @code
                                    'If Session("Work") = "Start Work" Then
                                    ' @<li><a href="#" id="startWork" style="font-size:18px;color:green;">Start Work</a></li>
                                    '          Else
                                    ' @<li><a href="#" id="startWork" style="font-size:18px;color:red;">End Work</a></li>
                                    'End If
                                End Code
                                <li><a href="/Logon/Logout" style="font-size:18px;color:red;">Log-out</a></li>
                                <li>@RenderSection("VersionDetails", False)</li>
                            </ul>
                        </li>
                    </ul>
                    @RenderSection("DropDownMenu", False)

                                    End If

            </div><!--/.nav-collapse -->
        </div>
    </div>
    <div id="print-alert">
    </div>
    @If TempData.ContainsKey("Error") Then
        @<div>
            <div class="alert alert-warning alert-dismissible" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <strong>@TempData("Error")</strong>
            </div>
        </div>
    End If
    @Html.Partial("MessageModal")
    @Html.Partial("PrintConfigModal")
    @Html.Partial("LLPreviewModal")
    @Html.Partial("PromptModal")
    @RenderBody()
    <div id="1.0.0" data-version="3/4/16">
        <input type="hidden" id="currentUser" value="@User.Identity.Name" />
        <input type="hidden" id="WSID" value="@Session("WSID")" />
        <input type="hidden" id="idlePopTime" value="@Session("IdlePopTime")" />
        <input type="hidden" id="idleShutTime" value="@Session("IdleShutTime")" />
        <input type="hidden" id="ReportPrinter" value="@Session("ReportPrinter")" />
        <input type="hidden" id="LabelPrinter" value="@Session("LabelPrinter")" />
        <input type="hidden" id="ConnectionName" value="@Session("ConnectionString")" />
        <input type="hidden" id="isAdmin" value="@Session("Admin")" />
        <input type="hidden" id="PageUUID" value="@ViewData("PageUUID")" />
        @RenderSection("AppName", False)
    </div>
    <script>
        $(document).ready(function () {
            document.body.style.zoom = '100%';
            //This code can be added to any page to prevent the user from leaving the page without confirming beforehand
            //$(window).on('beforeunload', function(e) {
            //    return 'Whatever Message we want to display';
            //});
        })
    </script>
    <script src="~/Scripts/FilterMenu/FilterMenu.js"></script>
    <script src="~/Scripts/FilterMenu/FilterMenuNonTable.js"></script>
    <script src="~/Scripts/Global/GlobalPostInitialization.js"></script>
</body>
</html>