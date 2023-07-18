<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <a data-target="#GCApps" data-toggle="collapse" data-parent="#GCParent">
            <h3 class="panel-title">
                Workstation Apps <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse" id="GCApps">
        <div class="row">
            <div class="col-md-12">
                <strong>Workstation Apps</strong> is an area where you may register workstations with the application and customize a few features to optimize your PickPro experience.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <img src="~/images/Help/GlobalConfig/workstations.png" alt="Workstation Apps page" style="width: 60%"/>
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                The first time any workstation is connected to PickPro the operator will be prompted to download and install a certificate in order to certify that the workstation is licensed and allowed to connect to the application.
                Once the client certificate has been installed the workstation may be customized from this page.  Workstations may be limited to a certain number of applications that the site has access to or
                a default application may be set.
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                This ability is particularly helpful when:
                <ul>
                    <li>Certain workstations are only going to be using a single application.</li>
                    <li>Certain workstations should load an application by default instead of allowing the user to choose which application they will use.</li>
                    <li>Certain workstations should not have access to some applications offered.</li>
                </ul>
                A simple example of this is that a workstation in the induction area that does not do consolidation simply should not have access to consolidation.  The default application for such a workstation would likely be induction.  If a workstation will be used primarily 
                by supervisors this may differ because they may need to see what is being inducted alongside consolidation.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                In order to assign applications to a workstation:
                <ol>
                    <li>Identify the workstation in the left hand list by the friendly name of the workstation and click the entry.</li>
                    <li>Check the appropriate applications.</li>
                    <li>Set a default app (optional) by clicking the radio button.  You may also clear the default app by clicking the <button class="btn btn-primary">Clear Default App</button> button.</li>
                    <li>Each change will be automatically saved as the changes are made.</li>
                </ol>
            </div>
        </div>
    </div>
</div>
