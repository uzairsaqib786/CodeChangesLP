<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Boolean
@Code
    ViewData("Title") = "Generate Certificates"
    ViewData("PageName") = "&nbsp; | &nbsp; Certificates"
End Code
<div class="container-fluid">
    <div class="row">
        <div id="certContainer" class="col-md-4 col-md-offset-4">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Workstation Certificates</h3>
                </div>
                <div class="panel-body">
                    <label id="certMessage">Your Server certificate will begin downloading in <strong id="ServerCount">5</strong>...</label>
                    <button id="serverContinue" hidden="hidden" class="btn btn-primary" onclick="startClientCertificates()">Continue</button>
                    <div style="margin-top:10px;" hidden="hidden" id="serverCertSteps">
                        <div class="col-md-3">
                            <label>Click the Install Certificate Button</label>
                            <a href="#" class="thumbnail">
                                <img src="~/images/Help/Certificates/cert1.PNG" alt="CertGuide1" />
                            </a>
                        </div>
                        <div class="col-md-3">
                            <label>Click the Browse Button to find the Certificate Store</label>
                            <a href="#" class="thumbnail">
                                <img src="~/images/Help/Certificates/cert3.PNG" alt="CertGuide2" />
                            </a>
                        </div>
                        <div class="col-md-3">
                            <label>Select Trusted Root Certification Authorities</label>
                            <a href="#" class="thumbnail">
                                <img src="~/images/Help/Certificates/cert4.PNG" alt="CertGuide3" />
                            </a>
                        </div>
                        <div class="col-md-3">
                            <label>Complete the Certificate Installation</label>
                            <a href="#" class="thumbnail">
                                <img src="~/images/Help/Certificates/cert6.PNG" alt="CertGuide4" />
                            </a>
                        </div>
                    </div>
                    <div hidden="hidden" id="clientCertInputs">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>PC Friendly Name</label>
                                <input id="certName" type="text" class="form-control" placeholder="Friendly Name" maxlength="50" />
                            </div>
                            <button hidden="hidden" class="btn btn-primary" onclick="generateCertificate()"><strong id="generateText">Generate</strong><strong id="generateButton"></strong></button>
                        </div>
                    </div>
                    <div style="margin-top:10px;" hidden="hidden" id="clientCertSteps">
                        <div class="row">

                            <div class="col-md-3">
                                <label>Click Next to Begin Installing the Certificate</label>
                                <a href="#" class="thumbnail">
                                    <img src="~/images/Help/Certificates/ClientCertStep1.PNG" alt="CertGuide1" />
                                </a>
                            </div>
                            <div class="col-md-3">
                                <label>Keep the Default Certificate location and Click Continue</label>
                                <a href="#" class="thumbnail">
                                    <img src="~/images/Help/Certificates/ClientCertStep2.PNG" alt="CertGuide2" />
                                </a>
                            </div>
                            <div class="col-md-3">
                                <label>The Password for this certificate is <strong>pickpro</strong></label>
                                <a href="#" class="thumbnail">
                                    <img src="~/images/Help/Certificates/ClientCertStep3.PNG" alt="CertGuide3" />
                                </a>
                            </div>
                            <div class="col-md-3">
                                <label>Keep default setting of "Automatically select the certificate store"</label>
                                <a href="#" class="thumbnail">
                                    <img src="~/images/Help/Certificates/ClientCertStep4.PNG" alt="CertGuide4" />
                                </a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>Click Finish</label>
                                <a href="#" class="thumbnail">
                                    <img src="~/images/Help/Certificates/ClientCertStep5.PNG" alt="CertGuide5" />
                                </a>
                            </div>
                        </div>
                    </div>
                    <div style="margin-top:10px;" hidden="hidden" id="androidCertSteps">
                        <h2>Refresh your browser to connect the Application</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input id="isAndroid" type="hidden" value="@Model.ToString" />
</div>
<script src="~/Scripts/Certificates/Certificates.js"></script>
