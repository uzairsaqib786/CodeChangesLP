<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="EmployeeAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#EmployeeOverview" data-parent="#EmployeeAccordion">
                        <h3 class="panel-title">
                            Overview
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="EmployeeOverview">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This page is the <strong>Employees</strong> page. It displays an employee's information.
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <img src="/images/Help/Employees/InitialScreen.png" alt="Employees Load Screen" usemap="#employeesmap" style="width: 85%"/>
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="EmployeesOverviewAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" data-target="#panelEmployeeTab">
                                                    <h3 class="panel-title">
                                                        1 | Employees Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelEmployeeTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Employees Tab</strong>. This tab displays information about the selected employee.
                                                                <div class="row" style="padding-top:5px">
                                                                    <div class="col-md-12">
                                                                        <img src="/images/Help/Employees/EmployeesTab.png" alt="Employees Load Screen" usemap="#employeestabmap"style="width: 85%" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="EmployeesTabAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#EmployeesTabAccordion" data-target="#EmployeesTab_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="EmployeesTab_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons displayed.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Add New Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save Changes:</strong> Saves any changes made to the selected employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Save Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the employees report
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/PrintButt.png" alt="Employees Print Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete Employee:</strong> Deletes the selected employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteEmployeeButt.png" alt="Employees Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Clear:</strong> Clears the selected employee value in order for a new employee to be selected
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/ClearButt.png" alt="Employees Clear Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#EmployeesTabAccordion" data-target="#EmployeesTab_2">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="EmployeesTab_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information displayed for an employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Info:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Active Employee:</strong> Identifies if the employee is currently active</li>
                                                                                        <li><strong>First Name:</strong> The employee's first name</li>
                                                                                        <li><strong>MI:</strong> The employee's middle initial</li>
                                                                                        <li><strong>Last Name:</strong> The employee's last name</li>
                                                                                        <li><strong>Username:</strong> The employee's username that is used for logon</li>
                                                                                        <li><strong>Password:</strong> The employee's password that is used for logon</li>
                                                                                        <li><strong>Access Level:</strong> The employee's access level</li>
                                                                                        <li><strong>Group:</strong> The group that the selected employee belongs to</li>
                                                                                        <li><strong>Email Address:</strong> The employee's email address</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#EmployeesTabAccordion" data-target="#EmployeesTab_3">
                                                                                <h3 class="panel-title">
                                                                                    1 | Bulk Pro Settings
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="EmployeesTab_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12">
                                                                                            This panel discusses the <strong>Bulk Pro Settings</strong> tab. Below are the three different fields displayed
                                                                                            in this tab.
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <img src="/images/Help/Employees/BPSettTabs.png" alt="Employees BP Settings Tabs" usemap="#bpsettingsmap">
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="padding-top:5px;">
                                                                                        <div class="col-md-12">
                                                                                            <div class="panel-group" id="BPAccordion">
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#BP_1" data-parent="#BPAccordion">
                                                                                                            <h3 class="panel-title">
                                                                                                                1 | Zones
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="BP_1">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the <strong>Zones</strong> section. This displays all zones that are associated with the employee.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/Employees/BPZone.png" alt="Employees BP Zone Sections" style="width: 50%">
                                                                                                            </div>
                                                                                                        </div>

                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new zone for the employee
                                                                                                                        <ul>
                                                                                                                            <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees BP Zone Add New Button"></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Save Changes:</strong> Saves any edits made to the corresponding row (zone)
                                                                                                                        <ul>
                                                                                                                            <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees BP Zone Save Button"></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes the respective row (zone) from the employee
                                                                                                                        <ul>
                                                                                                                            <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees BP Zone Delete Button"></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Zone:</strong> The zones that are assigned to this employee</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#BP_2" data-parent="#BPAccordion">
                                                                                                            <h3 class="panel-title">
                                                                                                                2 | Locations
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="BP_2">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the <strong>Locations</strong> section. It displays the starting and corresponding ending locations
                                                                                                                assigned to the employee.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/Employees/BPLocations.png" style="width: 50%" alt="Employees BP Locations Section">
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Buttons:</strong>
                                                                                                                <ul>
                                                                                                                    <li>
                                                                                                                        <strong>Add New:</strong> Adds a new start and end location to the employee
                                                                                                                        <ul>
                                                                                                                            <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees BP Locations Add New Button"></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                    <li>
                                                                                                                        <strong>Delete:</strong> Deletes corresponding start and end location from the employee
                                                                                                                        <ul>
                                                                                                                            <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees BP Locations Delete Button"></li>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Start Location:</strong> The starting location assigned to this employee</li>
                                                                                                                    <li><strong>End Location:</strong> The end location assigned to a specific starting location</li>
                                                                                                                </ul>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="panel panel-info">
                                                                                                    <div class="panel-heading">
                                                                                                        <a data-toggle="collapse" data-target="#BP_3" data-parent="#BPAccordion">
                                                                                                            <h3 class="panel-title">
                                                                                                                3 | Orders
                                                                                                                <span class="accordion-caret-down"></span>
                                                                                                            </h3>
                                                                                                        </a>
                                                                                                    </div>
                                                                                                    <div class="panel-body collapse accordion-toggle" id="BP_3">
                                                                                                        <div class="row">
                                                                                                            <div class="col-md-12">
                                                                                                                This panel discusses the <strong>Orders</strong> section. This displays the maximum handheld orders
                                                                                                                for the employee.
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <img src="/images/Help/Employees/BPOrders.png" alt="Employees BP Orders Section">
                                                                                                            </div>
                                                                                                        </div>
                                                                                                        <div class="row" style="padding-top:5px;">
                                                                                                            <div class="col-md-12">
                                                                                                                <strong>Information:</strong>
                                                                                                                <ul>
                                                                                                                    <li><strong>Maximum Handheld Orders:</strong> The maximum handheld orders that this employee is allowed to do</li>
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
                                                                            <a data-target="#EmployeesTab_4" data-toggle="collapse" data-parent="#EmployeesTabAccordion">
                                                                                <h3 class="panel-title">
                                                                                    2 | Employee Pick Levels
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="EmployeesTab_4">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the <strong>Employee Pick Levels</strong> tab. This displays the pick level, start, and end shelf
                                                                                    for an employee.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="padding-top:5px;">
                                                                                <div class="col-md-12">
                                                                                    <img src="/images/Help/Employees/EmployeePickLevels.png" style="width: 50%" alt="Employees Employee Pick Levels Tab">
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="padding-top:5px;">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new pick level for the employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Employee Pick Levels Add New Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save Changes:</strong> Saves any changes made to the designated row (pick level)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Employee Pick Levels Save Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete:</strong> Deletes the designated pick level from the employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Employee Pick Levels Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Pick Level:</strong> The level that the corresponding start and end shelf are assigned to for this employee</li>
                                                                                        <li><strong>Start Shelf:</strong> The starting shelf for this employee's pick level</li>
                                                                                        <li><strong>End Shelf:</strong> The ending shelf for this employee's pick level</li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-target="#EmployeesTab_5" data-toggle="collapse" data-parent="#EmployeesTabAccordion">
                                                                                <h3 class="panel-title">
                                                                                    3 | Functions Allowed
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="EmployeesTab_5">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the <strong>Functions Allowed</strong> tab. This tab displays all functions that are allowed
                                                                                    for the selected employee.
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="padding-top:5px;">
                                                                                <div class="col-md-12">
                                                                                    <img src="/images/Help/Employees/FunctionsAllowed.png" style="width: 50%" alt="Employees Functions Allowed Tab">
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" style="padding-top:5px;">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add All:</strong> This will add all admin functions if the employee is an admin, and add all non-admin
                                                                                            functions if the employee is not an admin.
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddAllButt.png" alt="Employees Functions Allowed Add All Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Add New:</strong> Adds a new function to the employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Functions Allowed Add Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete:</strong> Deletes the designated function from the employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Functions Allowed Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Function:</strong> The function that this employee is assigned</li>
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
                                                <a data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" data-target="#panelGroupsTab">
                                                    <h3 class="panel-title">
                                                        2 | Groups Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelGroupsTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Groups</strong> tab which allows a group's functions to be edited.
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/Employees/GroupTab.png" style="width: 85%" alt="Employees Groups Tab">
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="GroupsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#GroupsAccordion" data-target="#Groups_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="Groups_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons displayed on the page
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Add New Group:</strong> Adds a new group that employees can be associated with
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Groups Add New Group Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Save Changes:</strong> Saves any changes made to the selected group
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Groups Save Changes Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the group report for either the selected group or all groups
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/PrintButt.png" alt="Employees Groups Print Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Delete</strong> Deletes the selected group unless it is one of the following groups administrator, bulk operator,
                                                                                            car/off operator, carousel operator, fork truck operator, pickpro admin, supervisor
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Groups Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Clear:</strong> Clears the selected group in order to selected a new group
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/ClearButt.png" alt="Employees Groups Clear Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Update Employees In Group:</strong> Updates all the employees in the group to have the updated group functions
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/UpdateEmployeesButt.png" alt="Employees Groups Update Emloyees Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Add All Functions:</strong> Adds all functions from the unassigned side (list) to the assigned side (list)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddAllFunctionsButt.png" alt="Employees Groups Add All Functions Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Remove All Functions:</strong> Moves all functions from the assigned side (list) to the unassigned side (list)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/RemoveAllFunctionsButt.png" alt="Employees Groups Remove All Functions Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Functions:</strong> Each function is able to clicked to be added to the opposite list, or clicked and dragged
                                                                                            between the two sides
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#GroupsAccordion" data-target="#Groups_2">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="Groups_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information displayed in the <strong>Groups</strong> tab
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Group:</strong> The selected group whose functions are able to be edited</li>
                                                                                        <li><strong>Unassigned Employee Functions:</strong> Functions not assigned to the selected group</li>
                                                                                        <li><strong>Assigned Employee Functions:</strong> Functions assigned to the selected group</li>
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
                                                <a data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" data-target="#StatTab">
                                                    <h3 class="panel-title">
                                                        3 | Statistics Tab
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="StatTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the <strong>Statistics</strong> tab. This tab displays the work information for all
                                                                selected employees within the date range
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/Employees/StatsTab.png" style="width: 85%" alt="Employees Statistics Tab">
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="StatsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#StatsAccordion" data-target="#Stats_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Buttons
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="Stats_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the buttons displayed on the page
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Buttons:</strong>
                                                                                    <ul>
                                                                                        <li>
                                                                                            <strong>Set Date To Today:</strong> Set the dates values to the current date
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SetDateButt.png" alt="Employees Statistics Tab Set Date To Today Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Select All:</strong> Selects all employees for the table information
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SelectAllButt.png" alt="Employees Statistics Tab Select All Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Clear Selected:</strong> Clears all selected employees
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/ClearselectedButt.png" alt="Employees Statistics Tab Clear Selected Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Print:</strong> Prints the information in the table either by employee or date
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/PrintButt.png" alt="Employees Statistics Tab Print Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#StatsAccordion" data-target="#Stats_2">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="Stats_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the information that is displayed
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Start Date:</strong> The start date of the date range for records to be shown</li>
                                                                                        <li><strong>End Date:</strong> The end date of the date range for records to be shown</li>
                                                                                        <li>
                                                                                            <strong>Employees:</strong> A list of all employees, that filter the table information to only records
                                                                                            for selected employees
                                                                                        </li>
                                                                                        <li>
                                                                                            <strong>Statistics:</strong> A data table containing all records of work done by the selected employees
                                                                                            within the given date range. For more information on data tables see the <strong>Data Tables</strong> section
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
                                                <a data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" data-target="#panelButtonAccessTab">
                                                    <h3 class="panel-title">
                                                        4 | Button Access
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelButtonAccessTab">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This Panel discusses the <strong>Button Access</strong> tab. This displays all controls that are able to be assigned
                                                                along with their function, and if the control is admin level.
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <img src="/images/Help/Employees/ButtonAccessTab.png" style="width: 85%" alt="Employees Button Access Tab">
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="ButtonAccessAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#ButtonAccessAccordion" data-target="#BAccess_1">
                                                                                <h3 class="panel-title">
                                                                                    Displayed Information
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BAccess_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses the displayed information
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Displayed Information:</strong>
                                                                                    <ul>
                                                                                        <li><strong>Control Name:</strong> The control that is able to be assigned to different employees and groups</li>
                                                                                        <li><strong>Function:</strong> The functionality that this control allows</li>
                                                                                        <li>
                                                                                            <strong>Admin Level:</strong> Checkbox that tells if the control is an admin level control. Able to be
                                                                                            clicked to change this
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#EmployeeAccordion" data-target="#EmployeeTabFunction">
                        <h3 class="panel-title">
                            Employee Tab Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="EmployeeTabFunction">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the functionality that is associated with the <strong>Employees</strong> tab
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="EmployeeFunctionsAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#EFunctions_1" data-parent="#EmployeeFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Selecting an Employee
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to select an employee
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Begin typing the desired employee's last name into the <strong>Employee Lookup</strong> (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/EmployeeLookup.png" alt="Employees Employees Tab Employee Lookup"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                When characters are entered a dropdown (shown below) appears with all employees whose last name begins with the
                                                                entered characters. Once you see the desired employee, click on their name in the drop down to select them
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/EmployeeLookupTypeahead.png" alt="Employees Employees Employee Lookup Type Ahead"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the desired employee is selected all the information will be displayed for that employee</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_2">
                                                    <h3 class="panel-title">
                                                        Editing and Saving an Employee's Information
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to edit and save changes made to an employee
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have an employee selected</li>
                                                            <li>Edit any of the valid fields and input new valid terms</li>
                                                            <li>
                                                                When all the desired fields are done being edited press the <strong>Save</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Employees Tab Save Changes Button"></li>
                                                                </ul>
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_3">
                                                    <h3 class="panel-title">
                                                        Adding a New Employee
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to add a new employee
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the <strong>Add New Employee</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Employees Add New Employee Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once pressed a pop up (shown below) will appear. Fill out all the fields for the new employee
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/AddNewEmployeeModal.png" style="width: 50%" alt="Employees Employees Add New Employee Modal"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once all the fields are filled out press the <strong>Submit</strong> button (shown below) to add the new employee
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SubmitButt.png" alt="Employees Employees Add New Employee Modal Submit Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the <strong>Submit</strong> is pressed the employee is added, and can be selected in order to edit any other information</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_4">
                                                    <h3 class="panel-title">
                                                        Printing the Employee Report
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_4">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to print the employee report
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the <strong>Print</strong> button shown below
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/PrintButt.png" alt="Employees Employees Print Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>The report will now print to the designated printer</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_5">
                                                    <h3 class="panel-title">
                                                        Delete an Employee
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_5">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to delete an employee
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the employee to delete</li>
                                                            <li>
                                                                Press the <strong>Delete Employee</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/DeleteEmployeeButt.png" alt="Employees Delete Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Press <strong>Okay</strong> on the pop up (shown below) to delete the employee
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/DeleteEmployeePop.png" alt="Employees Delete Modal"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once <strong>Okay</strong> is pressed the employee is deleted</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#EFunctions_6" data-parent="#EmployeeFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Clearing the Page
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_6">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to clear the employee information
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have an employee selected</li>
                                                            <li>
                                                                Press the <strong>Clear</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/ClearButt.png" alt="Employees Clear Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the clear button is pressed the selected employee and their information are emptied. <strong>Clearing an employee does not delete them</strong></li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_7">
                                                    <h3 class="panel-title">
                                                        BulkPro Settings Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_7">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This page contains the functions within the <strong>BulkPro Settings</strong> tab.
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="BPFunctionsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_1">
                                                                                <h3 class="panel-title">
                                                                                    Adding a New Zone
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to add a new zone within the <strong>Zone</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Have an employee selected</li>
                                                                                        <li>Under the <strong>Bulk Pro Settings</strong> tab have zone selected</li>
                                                                                        <li>
                                                                                            Press the <strong>Add New</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees BP Zones Add New Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once the <strong>Add New</strong> button is pressed a new row (shown below) will appear. Fill out this row with
                                                                                            the desired value
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewZoneRow.png" style="width: 40%" alt="Employees BP Zones Add New Row"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Press the <strong>Save</strong> button (shown below) to save the new zone for the selected employee
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees BP Zones Save Changes Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_2">
                                                                                <h3 class="panel-title">
                                                                                    Edit and Save an Existing Zone
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to edit and save an existing zone for an employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Have an employee selected that has zones</li>
                                                                                        <li>Edit the <strong>Zone</strong> field to the desire value</li>
                                                                                        <li>
                                                                                            Press the <strong>Save</strong> button (shown below) to save the changes
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees BP Zones Save Changes Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_3">
                                                                                <h3 class="panel-title">
                                                                                    Delete a Zone
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses deleting a zone form the employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee with existing zones</li>
                                                                                        <li>
                                                                                            Press on the <strong>Delete</strong> button (shown below) designated for the desired zone to be deleted
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees BP Zone Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once the <strong>Delete</strong> button is pressed a pop up (shown below) appears. To continue deleting press <strong>Okay</strong>
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteZonePopUp.png" alt="Employees BP Zone Delete Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once <strong>Okay</strong> is pressed the zone is deleted from the selected employee</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_4">
                                                                                <h3 class="panel-title">
                                                                                    Adding  a New Location
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_4">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses adding a new location on the <strong>Locations</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select the desired employee</li>
                                                                                        <li>Click on the <strong>Locations</strong> section</li>
                                                                                        <li>
                                                                                            Press the <strong>Add New</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees BP Locations Add New Location Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once the add new button is pressed a pop up will appear (shown below). In this pop up are two typeaheads one for start location and
                                                                                            one for end location. The data within the typeahead for the end location depends on the value of the start location.
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/LocationsAddNewPopUp.png" style="width: 40%" alt="Employees BP Locations Add New Location Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Use the start location typeahead (shown below) to pick a start location
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/StartLocationTypeahead.png" style="width: 40%" alt="Employees BP Locations Start Location Typeahead"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Use the end location typeahead (shown below) to pick an end location
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/EndLocationTypeAhead.png" style="width: 40%" alt="Employees BP Locations End Location Typeahead"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once a start and end location are picked press the <strong>Submit</strong> Button, and the new location is added for the
                                                                                            selected employee
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_5">
                                                                                <h3 class="panel-title">
                                                                                    Editing an Existing Location
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_5">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to edit an already existing location for an employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee who has existing locations</li>
                                                                                        <li>Click on either the <strong>Start Location</strong> or <strong>End Location</strong> fields</li>
                                                                                        <li>
                                                                                            When any of these fields are clicked the locations pop up (shown below) appears with the current start and end locations filled in.
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/LocationsAddNewPopUp.png" style="width: 40%" alt="Employees BP Locations Add New Location Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Using the typeahead for the start location (shown below) select the new desired start location.
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/StartLocationTypeahead.png" style="width: 40%" alt="Employees BP Locations Start Location Typeahead"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Using the typeahead for end location (shown below) select an end location. Remember the end location <strong>Depends</strong>
                                                                                            on the start location
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/EndLocationTypeAhead.png" style="width: 40%" alt="Employees BP Locations End Location Typeahead"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once the new start and end locations are set, click the <strong>Submit</strong> button and the changes will be saved and applied</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_6">
                                                                                <h3 class="panel-title">
                                                                                    Deleting a Location
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_6">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses deleting a location from the <strong>Locations</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee that has locations assigned to them</li>
                                                                                        <li>Go under the <strong>Locations</strong> section</li>
                                                                                        <li>
                                                                                            Click on the designated <strong>Delete</strong> button (shown below) for the desired location set to be deleted
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees BP Locations Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once the desired <strong>Delete</strong> button is pressed, a pop up (shown below) will appear. To continue with the
                                                                                            delete press <strong>Okay</strong>
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/LocationsDeletePopUp.png" alt="Employees BP Locations Delete Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once <strong>Okay</strong> is pressed the location is deleted from the selected employee</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#BPFunctionsAccordion" data-target="#BPFunctions_7">
                                                                                <h3 class="panel-title">
                                                                                    Editing Maximum Handheld Orders
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="BPFunctions_7">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to edit the <strong>Maximum Handheld Orders</strong> field under the <strong>Orders</strong> section
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select the desired employee</li>
                                                                                        <li>Go under the <strong>Orders</strong> section</li>
                                                                                        <li>Enter the new value for the Maximum Handheld Orders</li>
                                                                                        <li>
                                                                                            One the desired value is entered press the <strong>Save</strong> button (shown below) found under
                                                                                            <strong>Employee Information</strong>
                                                                                        </li>
                                                                                    </ol>
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
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_8">
                                                    <h3 class="panel-title">
                                                        Employee Pick Levels Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_8">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the functionality under the <strong>Employee Pick Levels</strong> section
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="PLFunctionsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#PLFunctionsAccordion" data-target="#PLFunctions_1">
                                                                                <h3 class="panel-title">
                                                                                    Add a New Pick Level
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="PLFunctions_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses adding a new pick level for the selected employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select the desired employee to edit</li>
                                                                                        <li>Select the <strong>Employee Pick Levels</strong> Tab</li>
                                                                                        <li>
                                                                                            Under this tab press the <strong>Add New</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Employee Pick Levels Add New Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Upon pressing the <strong>Add New</strong> button a new empty row will appear. Fill out the
                                                                                            start and end shelf for this row
                                                                                        </li>
                                                                                        <li>
                                                                                            Once these fields are filled out with valid values press the <strong>Save Button</strong> (shown below)
                                                                                            designated for the new row
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Employee Pick Levels Save Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>With the <strong>Save</strong> button pressed the new pick level is added for the selected employee</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#PLFunctionsAccordion" data-target="#PLFunctions_2">
                                                                                <h3 class="panel-title">
                                                                                    Editing a Current Pick Level
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="PLFunctions_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to edit an already existing pick level for an employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee that has pick levels assigned to them</li>
                                                                                        <li>Edit the <strong>Start</strong> and <strong>End Shelf's</strong> values to the new desired values</li>
                                                                                        <li>
                                                                                            Press the <strong>Save</strong> button (shown below) designated for the edited row to save the changes made
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Employee Pick Levels Save Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#PLFunctionsAccordion" data-target="#PLFunctions_3">
                                                                                <h3 class="panel-title">
                                                                                    Deleting a Pick Level
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="PLFunctions_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses deleting a pick level for an employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee with pick levels already existing</li>
                                                                                        <li>
                                                                                            Under the <strong>Employee Pick Levels</strong> tab click on the <strong>Delete</strong> button (shown below)
                                                                                            designated to the desired pick level to be deleted
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Employee Pick Levels Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Once this button is pressed, a pop up (shown below) appears. To continue with the delete press <strong>Okay</strong>
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/PickLevelDeletePopUp.png" alt="Employees Employee Pick Levels Delete Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once <strong>Okay</strong> is pressed, the desired pick level is removed from the selected employee</li>
                                                                                    </ol>
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
                                                <a data-toggle="collapse" data-parent="#EmployeeFunctionsAccordion" data-target="#EFunctions_9">
                                                    <h3 class="panel-title">
                                                        Functions Allowed Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="EFunctions_9">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                This panel discusses the functions within the <strong>Functions Allowed</strong> Tab
                                                            </div>
                                                        </div>
                                                        <div class="row" style="padding-top:5px;">
                                                            <div class="col-md-12">
                                                                <div class="panel-group" id="FAFunctionsAccordion">
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#FAFunctionsAccordion" data-target="#FAFunctions_1">
                                                                                <h3 class="panel-title">
                                                                                    Adding a Single Function
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="FAFunctions_1">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how to add a single function to the selected employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select the desired employee to add functions to</li>
                                                                                        <li>
                                                                                            Under the <strong>Functions Allowed</strong> tab press the <strong>Add</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Functions Allowed Add Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            When this button is pressed a pop up (shown below) appears. Using the typeahead (shown below) select the
                                                                                            desired function
                                                                                            <ul>
                                                                                                <li>
                                                                                                    <strong>Modal:</strong>
                                                                                                    <ul>
                                                                                                        <li><img src="/images/Help/Employees/ControlNameModal.png"style="width: 50%" alt="Employees Functions Allowed Add Modal"></li>
                                                                                                    </ul>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <strong>Typeahead:</strong>
                                                                                                    <ul>
                                                                                                        <li><img src="/images/Help/Employees/ControlNameTypeahead.png" style="width: 50%" alt="Employees Functions Allowed Add Typeahead"></li>
                                                                                                    </ul>
                                                                                                </li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once the desired function is selected press the <strong>Submit</strong> button to add this function to the selected employee</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#FAFunctionsAccordion" data-target="#FAFunctions_2">
                                                                                <h3 class="panel-title">
                                                                                    Adding All Functions (Using Add All Button)
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="FAFunctions_2">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses how the to add all functions and how it works
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>How it works:</strong>
                                                                                    <ul>
                                                                                        <li>If an employee's <strong>Access Level</strong> is <strong>Administrator</strong> then they receive all functions (controls)</li>
                                                                                        <li>
                                                                                            If an employee's <strong>Access Level</strong> is <strong>not Administrator</strong> then they receive all functions(controls)
                                                                                            which do not have the <strong>Admin Level</strong> checked
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select the desired employee</li>
                                                                                        <li>
                                                                                            Under the <strong>Functions Allowed</strong> section press the <strong>Add All</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/AddAllButt.png" alt="Employees Functions Allowed Add All Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once this button is pressed all functions depending on the employee's <strong>Access Level</strong> (see above) are added</li>
                                                                                    </ol>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="panel panel-info">
                                                                        <div class="panel-heading">
                                                                            <a data-toggle="collapse" data-parent="#FAFunctionsAccordion" data-target="#FAFunctions_3">
                                                                                <h3 class="panel-title">
                                                                                    Deleting a Function
                                                                                    <span class="accordion-caret-down"></span>
                                                                                </h3>
                                                                            </a>
                                                                        </div>
                                                                        <div class="panel-body collapse accordion-toggle" id="FAFunctions_3">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    This panel discusses removing a function from the selected employee
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <strong>Steps:</strong>
                                                                                    <ol>
                                                                                        <li>Select an employee that has functions assigned to them</li>
                                                                                        <li>
                                                                                            Under the <strong>Functions Allowed</strong> tab press the <strong>Delete</strong> button (shown below)
                                                                                            designated to the function
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Functions Allowed Delete Button"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>
                                                                                            Upon clicking this button a pop up (shown below) appears. To continue with the delete press
                                                                                            <strong>Okay</strong> on the pop up
                                                                                            <ul>
                                                                                                <li><img src="/images/Help/Employees/FunctionsAllowedDeletePopUp.png" alt="Employees Functions Allowed Delete Pop Up"></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once <strong>Okay</strong> is pressed the desired function is removed from the selected employee</li>
                                                                                    </ol>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#GroupTabFunction" data-parent="#EmployeeAccordion">
                        <h3 class="panel-title">
                            Groups Tab Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="GroupTabFunction">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the functionality within the <strong>Groups</strong> tab
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="GroupFunctionsAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-target="#GFunctions_1" data-toggle="collapse" data-parent="#GroupFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Adding a New Group
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to add a new group
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Under the <strong>Groups</strong> tab press the <strong>Add New Group</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/AddNewButt.png" alt="Employees Groups Add New Group Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once this button is pressed a pop up (shown below) appears. Enter the desired new group name under the
                                                                <strong>New Group Name</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/InsGroupsPopUp.png" style="width: 50%" alt="Employees Groups Add New Group Pop Up"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once the new group name is entered press the <strong>Submit</strong> button and the new group will be created</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_2">
                                                    <h3 class="panel-title">
                                                        Selecting a Group
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to select an existing group
                                                        <span class="accordion-caret-down"></span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Under the <strong>Employee Group Management</strong> section click on the <strong>Select Employee Group</strong> field</li>
                                                            <li>
                                                                Once clicked a dropdown (shown below) appears. In this dropdown click on the desired group
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SelectGroupTypeahead.png" alt="Employees Groups Select Group Typeahead"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once clicked the desired group's functions are displayed and the group's name now fills the group field</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_3">
                                                    <h3 class="panel-title">
                                                        Edit and Save Assigned Function Changes
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses assigning and unassigning functions and saving the changes
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired group to be edited</li>
                                                            <li>
                                                                Under the <strong>Assigned Functions</strong> sections are two lists: one for unassigned and the other for assigned functions.
                                                                By click and dragging or clicking these functions are able to be switched between the two lists.
                                                            </li>
                                                            <li>
                                                                Once all functions are in their desired place for the group press the <strong>Save</strong> button (shown below) under the
                                                                <strong>Employee Group Management</strong> section
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Groups Save Changes Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once the <strong>Save</strong> button is pressed, all assigned functions are then assigned to the group and all unassigned functions
                                                                are unassigned from the group
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_4">
                                                    <h3 class="panel-title">
                                                        Printing the Group Report
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_4">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to print the group report. The group report has two options print a single group or all groups.
                                                        Both prints steps are shown below
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps To Print Single Group Report:</strong>
                                                        <ol>
                                                            <li>Select the desired group that is to be printed</li>
                                                            <li>
                                                                Click on the print button (shown below) and select the option <strong>Print Selected Group</strong> from the dropdown
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/GroupPrintDropDown.png" alt="Employees Groups Print Dropdown"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once selected the report will be printed only containing the selected group</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps to Print All Group</strong>
                                                        <ol>
                                                            <li>
                                                                Click on the print button (shown below) and select the option <strong>Print All Groups</strong> from the dropdown
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/GroupPrintDropDown.png" alt="Employees Groups Print Dropdown"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once selected the report will be printed containing all groups</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_5">
                                                    <h3 class="panel-title">
                                                        Deleting a Group
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_5">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to delete a group. If the group is one of the following it is unable to be deleted:
                                                        <ul>
                                                            <li>Administrator</li>
                                                            <li>Bulk Operator</li>
                                                            <li>Car/Off Operator</li>
                                                            <li>Carousel Operator</li>
                                                            <li>Fork Truck Operator</li>
                                                            <li>PickPro Admin</li>
                                                            <li>Supervisor</li>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps</strong>
                                                        <ol>
                                                            <li>Select the desired group to be deleted</li>
                                                            <li>
                                                                Click the <strong>Delete</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/DeleteButt.png" alt="Employees Groups Delete Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once this button is pressed a pop up (shown below) appears. To continue with the delete press <strong>Okay</strong>
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/DeleteGroupPopUp.png" alt="Employees Groups Delete Pop Up"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once <strong>Okay</strong> is pressed the group is deleted</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#GFunctions_6" data-parent="#GroupFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Clear Selected Group Field
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_6">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses clearing the selected group field
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select a group</li>
                                                            <li>
                                                                Press the <strong>Clear</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/ClearSelectedButt.png" alt="Employees Statistics Tab Clear Selected Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Once this button is pressed all information on the screen is removed and the selected group field is restored to the empty value.
                                                                <strong>Clearing</strong> does not delete the group
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-target="#GFunctions_7" data-parent="#GroupFunctionsAccordion">
                                                    <h3 class="panel-title">
                                                        Updating Employees in a Group
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_7">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses hot to update all employees within a group to have the functions of that group
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired group</li>
                                                            <li>
                                                                Press the <strong>Update Employees in Group</strong> button (shown below) to update all employees within the selected group
                                                                and assign them any functions that are assigned to the group
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/UpdateEmployeesButt.png" alt="Employees Update Employees in Group Button"></li>
                                                                </ul>
                                                            </li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_8">
                                                    <h3 class="panel-title">
                                                        Add All Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_8">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to assign all unassigned functions
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired group</li>
                                                            <li>
                                                                Press the <strong>Add All Functions</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/AddAllFunctionsButt.png" alt="Employees Groups Add All Functions Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                All functions should now be under the assigned list. To save this press the <strong>Save</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Groups Save Changes Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>All functions are now assigned to this group</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#GroupFunctionsAccordion" data-target="#GFunctions_9">
                                                    <h3 class="panel-title">
                                                        Remove All Functions
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="GFunctions_9">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to remove all assigned functions from a group
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired group</li>
                                                            <li>
                                                                Press the <strong>Remove All Functions</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/RemoveAllFunctionsButt.png" alt="Employees Groups Remove All Functions Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                All functions should now be under the unassigned list. To save this press the <strong>Save</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SaveChangesButt.png" alt="Employees Groups Save Changes Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>All functions are now unassigned and removed from this group</li>
                                                        </ol>
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
                    <a data-toggle="collapse" data-parent="#EmployeeAccordion" data-target="#StatTabFunction">
                        <h3 class="panel-title">
                            Statistics Tab Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="StatTabFunction">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the various functions within the <strong>Statistics</strong> tab
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px;">
                                <div class="col-md-12">
                                    <div class="panel-group" id="StatTabFunctionsAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#StatTabFunctionsAccordion" data-target="#StatFunction_1">
                                                    <h3 class="panel-title">
                                                        Populating the Statistics Table
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="StatFunction_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to populate the <strong>Statistics Table</strong> with data
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Use the <strong>Start</strong> and <strong>End Date</strong> filters to set the desired date range. <strong>
                                                                    If the wrong date range is set the desired
                                                                    statistic(s) may not appear.
                                                                </strong> For more information on see the <strong>Date Picker</strong> section
                                                            </li>
                                                            <li>Once the date range is set, select the employees to display by clicking (for a single employee) or by clicking and dragging (for multiple employees)</li>
                                                            <li>The table will now populate with any statistics within the date range for the selected employees</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#StatTabFunctionsAccordion" data-target="#StatFunction_2">
                                                    <h3 class="panel-title">
                                                        Set Date To Today
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body accordion-toggle collapse" id="StatFunction_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses resetting the date filters to today's date
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the <strong>Set Date to Today</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SetDateButt.png" alt="Employees Statistics Tab Set Date To Today Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>After this button is pressed both date filters are set to the current date</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#StatTabFunctionsAccordion" data-target="#StatFunction_3">
                                                    <h3 class="panel-title">
                                                        Select All Employees
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="StatFunction_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to select all employees for the table display
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>
                                                                Press the <strong>Select All</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/SelectAllButt.png" alt="Employees Statistics Tab Select All Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Upon being hit all employees in the list should be highlighted, showing that they are selected</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#StatTabFunctionsAccordion" data-target="#StatFunction_4">
                                                    <h3 class="panel-title">
                                                        Clear All Selected Employees
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="StatFunction_4">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to clear all selected employees
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Have employees selected</li>
                                                            <li>
                                                                Press the <strong>Clear Selected</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/ClearselectedButt.png" alt="Employees Statistics Tab Clear Selected Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>Once this button is pressed all selected employees are unselected and the table is cleared as no employees are selected</li>
                                                        </ol>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#StatTabFunctionsAccordion" data-target="#StatFunction_5">
                                                    <h3 class="panel-title">
                                                        Printing The Statistics Report
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body accordion-toggle collapse" id="StatFunction_5">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses printing the employee statistics report
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Select the desired date range and employees</li>
                                                            <li>
                                                                Press the <strong>Print</strong> button (shown below)
                                                                <ul>
                                                                    <li><img src="/images/Help/Employees/PrintButt.png" alt="Employees Statistics Tab Print Button"></li>
                                                                </ul>
                                                            </li>
                                                            <li>
                                                                Select the desired design of the report. For a report by individual employees select the <strong>Print By Users</strong> option.
                                                                For a report by each day select the <strong>Print By Date</strong> option
                                                            </li>
                                                            <li>Once on these options are selected, the desired report is sent to the printer</li>
                                                        </ol>
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
                    <a data-toggle="collapse" data-target="#ButtonTabFunction" data-parent="#EmployeeAccordion">
                        <h3 class="panel-title">
                            Button Access Tab Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="ButtonTabFunction">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    This panel discusses the functionality within the <strong>Button Access</strong> tab
                                </div>
                            </div>
                            <div class="row" style="padding-top:5px">
                                <div class="col-md-12">
                                    <div class="panel-group" id="ButtonFunctionsAccordion">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#ButtonFunctionsAccordion" data-target="#BAFunction_1">
                                                    <h3 class="panel-title">
                                                        Setting the Admin Level of a Control
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body accordion-toggle collapse" id="BAFunction_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        This panel discusses how to change the <strong>Admin Level</strong> field of a control
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <strong>Steps:</strong>
                                                        <ol>
                                                            <li>Find the desired control to switch the <strong>Admin Level</strong> for</li>
                                                            <li>
                                                                Click on the <strong>Admin Level</strong> checkbox to set the desired value. Checked implies that it is an admin control,
                                                                unchecked implies it is not.
                                                            </li>
                                                            <li>This will autosave, on any changes made</li>
                                                        </ol>
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
        </div>
    </div>
</div>
<map name="employeesmap">
    <area shape="rect" coords="6, 1, 79, 40" data-target="#panelEmployeeTab" data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" />
    <area shape="rect" coords="87, 2, 149, 36" data-target="#panelGroupsTab" data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" />
    <area shape="rect" coords="148, 3, 216, 138" data-target="#StatTab" data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" />
    <area shape="rect" coords="219, 5, 316, 37" data-target="#panelButtonAccessTab" data-toggle="collapse" data-parent="#EmployeesOverviewAccordion" />
</map>
<map name="employeestabmap">
    <area shape="rect" coords="303, 6, 388, 33" data-target="#EmployeesTab_3" data-toggle="collapse" data-parent="#EmployeesTabAccordion" />
    <area shape="rect" coords="392, 6, 497, 30" data-target="#EmployeesTab_4" data-toggle="collapse" data-parent="#EmployeesTabAccordion" />
    <area shape="rect" coords="502, 6, 596, 28" data-target="#EmployeesTab_5" data-toggle="collapse" data-parent="#EmployeesTabAccordion" />
</map>
<map name="bpsettingsmap">
    <area shape="rect" coords="10, 14, 336, 92" data-target="#BP_1" data-toggle="collapse" data-parent="#BPAccordion" />
    <area shape="rect" coords="22, 97, 328, 155" data-target="#BP_2" data-toggle="collapse" data-parent="BPAccordion" />
    <area shape="rect" coords="35, 164, 318, 219" data-target="#BP_3" data-toggle="collapse" data-parent="BPAccordion" />
</map>