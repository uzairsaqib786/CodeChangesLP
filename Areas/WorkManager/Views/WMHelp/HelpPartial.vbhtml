<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
    Layout = Nothing
End Code
<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Help Home
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                This is the Help page.  Contained are guides to using Pick Pro sorted by page (Event Log, Inventory Map, etc).  Each page has an overview and various other tabs with more details about individual pages and how they work.
            </div>
        </div>
        <div class="row" style="padding-top:5px;">
            <div class="col-md-12">
                <h3>Navigation between Help pages</h3>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <strong>Steps:</strong>
                <ol>
                    <li>Identify the page you need help with.  This is at the top left in the gray menu bar.  This page is Help because it is labeled <strong>Pick Pro | Help</strong></li>
                    <li>Identify what you need help with.  Example:  "I need help changing my shipping carrier."</li>
                    <li>
                        Use the navigation bar on the left to navigate to the appropriate view.  (Our example would be found in the <strong>Carrier Modal</strong> within the <strong>Preferences</strong> view.)
                    </li>
                    <li>
                        Use the collapsible panels to find your specific view or problem.  The panel headings can be clicked to expand them or collapse them.
                        <ul>
                            <li>
                                <div class="panel-group">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <a data-toggle="collapse" data-target="#help-collapse">
                                                <h3 class="panel-title">
                                                    View/Page Title (Click me!) <span class="accordion-caret-down"></span>
                                                </h3>
                                            </a>
                                        </div>
                                        <div class="panel-body collapse accordion-toggle" id="help-collapse">
                                            This is where helpful instructions for a particular view will be.
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li>Navigate within the page's panel to find your specific issue.  Larger views will have more collapsible panels with more information in them, so be sure to look through any inside.</li>
                    <li>If you cannot find a solution to your issue within the documentation for the specific view be sure to see any other related help pages, like <strong>Staging Locations</strong>.</li>
                    <li>If you still cannot find a solution to your issue contact a supervisor or ScottTech for support.</li>
                </ol>
            </div>
        </div>
    </div>
</div>
