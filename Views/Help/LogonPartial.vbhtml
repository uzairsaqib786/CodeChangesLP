<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="LogonAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#LogonOverview" data-parent="#LogonAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="LogonOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Logon Page</strong>. It is the first page that gets displayed when the application is started.
                                    From this page an employee is able to login and change their password.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/Logon/InitialScreen.png" alt="Logon Initial Screen" usemap="#logonmap" />
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="LogonOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#LogonOverviewAccordion" data-target="#panelLogon_1">
                                                    <h3 class="panel-title">
                                                        1 | Logon Filters
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelLogon_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Filters:</strong>
                                                        <ol>
                                                            <li><strong>Username:</strong> The username field where the employee will enter their username for logon.</li>
                                                            <li><strong>Password:</strong> The password field where the employee will enter their password for logon.</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#LogonOverviewAccordion" data-target="#panelLogon_2">
                                                    <h3 class="panel-title">
                                                        2 | Logon Page Buttons
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelLogon_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Buttons:</strong>
                                                        <ul>
                                                            <li><strong>Change Password:</strong> This button will open the pop up for an employee to change their password.
                                                                <ul>
                                                                    <li><img src="/images/Help/Logon/ChangePassButt.png" alt="Logon Initial Screen Change Password Button" /></li>
                                                                </ul>
                                                            </li>
                                                            <li><strong>Logon:</strong> Checks the username and password combination, and if they match will log the employee on.
                                                                <ul>
                                                                    <li><img src="/images/Help/Logon/LogonButt.png" alt="Logon Initial Screen Logon Button" /></li>
                                                                </ul>
                                                            </li>
                                                        </ul>
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
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#LogonAccordion" data-target="#LoggingIn">
                        <h3 class="panel-title">
                            How to Login
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="LoggingIn">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses how to log into the application.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Enter the desired username into the <strong>Username</strong> field.</li>
                                <li>Enter the desired password into the <strong>Password</strong> field.</li>
                                <li>Either press the <strong>Logon</strong> button, or the enter button on the keyboard to try and login with the inputted credentials.</li>
                                <li>Upon successful login the user will be brought to the main menu page.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#ChangePassword" data-parent="#LogonAccordion">
                        <h3 class="panel-title">
                            Changing a Password
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ChangePassword">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses changing a password for an employee.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>On the initial load screen press the <strong>Change Password</strong> button (shown below)
                                    <ul>
                                        <li><img src="/images/Help/Logon/ChangePassButt.png" alt="Logon Initial Screen Change Password Button" /></li>
                                    </ul>
                                </li>
                                <li>A pop up (shown below) will then appear. Fill out the fields identifying the username, old password, new password, and confirming the new password.
                                    <ul>
                                        <li><img src="/images/Help/Logon/ChangePasswordModal.png" style="width: 50%" alt="Logon Initial Screen Change Password Modal" /></li>
                                    </ul>
                                </li>
                                <li>Once these fields are completed press the <strong>Save Password</strong> button. To replace the old password with the new one.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="logonmap">
    <area shape="rect" coords="33, 238, 604, 394" data-target="#panelLogon_1" data-toggle="collapse" data-parent="#LogonOverviewAccordion" />
    <area shape="rect" coords="346, 397, 603, 447" data-target="#panelLogon_2" data-toggle="collapse" data-parent="#LogonOverviewAccordion" />
</map>
