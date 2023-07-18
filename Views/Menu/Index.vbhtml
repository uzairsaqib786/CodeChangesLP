<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Main Menu"
    'Order Apps in Alpha Order
    Dim Apps = PickPro_Web.Config.AppLicenses.ToList().OrderBy(Function(a)
                                                                       Return a.Value.Info.DisplayName
                                                               End Function)
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h2 class="panel-title">Apps</h2>
                </div>
                <div class="panel-body">
                    @For Each availApp In Apps
                        If availApp.Value.isLicenseValid AndAlso PickPro_Web.Config.getWSAppPermission(Session("WSID"), availApp.Key) Then
                        @<div class="col-md-6">
                            <a href="/@availApp.Value.Info.URL" class="btn btn-xl btn-primary btn-block bottom-spacer">@availApp.Value.Info.DisplayName</a>
                        </div>
                        End If
                    Next
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    var count = 0;
    setInterval(function () {
        var choices = ['ಠ_ಠ', '(ﾉ◕ヮ◕)ﾉ', '( ͡° ͜ʖ ͡°)', '(ಠ ͜ʖ ಠ)'];
        var which = parseInt(Math.random() * (choices.length));
        count++;
        console.log(choices[which] + ' you have been idle on this page for ' + (count * 2) + ' minutes.');
    }, 2000 * 60); // two minutes
</script>