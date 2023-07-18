<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->


<div class="row">
    <div class=" col-md-12">
        <div class="panel-group" id="GeneralAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#GeneralGlobalConfig" data-parent="GeneralAccordion">
                        <h3 class="panel-title">
                            Configure Workstation
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="body-panel collapse accordion-toggle" id="GeneralGlobalConfig">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row" style="padding-top:20px; padding-left: 30px; padding-bottom:20px;">
                                <font size="+1">
                                    A workstation must be configured via the Global Config in order for it to access the FlowRack Replenishment app.
                                </font>
                            </div>
                        </div>
                    </div>

                </div>
            </div>


            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#GeneralWSPref" data-parent="GeneralAccordion">
                        <h3 class="panel-title">
                            Set Workstation Zone
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="body-panel collapse accordion-toggle" id="GeneralWSPref">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row" style="padding-top:20px; padding-left: 30px;">
                                <font size="+1">
                                    <strong>1.  </strong>
                                    First you must go to Admin/preferences and select the Workstation Preferences
                                    tab in the top left corner. At the bottom of Workstation Settings you can select
                                    the carton flow zone you are working in.  The zones that are displayed come
                                    from the Location Zones table where Carton Flow is true.
                                </font>
                            </div>
                            <div class="row" style="padding-left: 30px; padding-top:20px; padding-bottom:20px;">
                                <font size="+1">
                                    <strong>2.  </strong>
                                    Once you select the zone you are in, the Carton Flow ID field is updated in the Workstation Preferences table.
                                </font>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#GeneralFRR" data-parent="GeneralAccordion">
                        <h3 class="panel-title">
                            FlowRack Replenish
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="body-panel collapse accordion-toggle" id="GeneralFRR">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row" style="padding-top:20px; padding-left: 30px;">
                                <font size="+1">
                                    <strong>1.  </strong>
                                     When you open the FlowRack Replenish module your current Carton Flow Zone
                                     is selected from the Workstation Preferences table and displayed to you in the top left corner.
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px;">
                                <font size="+1">
                                    <strong>2.  </strong>
                                     When you scan an Item Number, Supplier Item ID or Scan Code, the program attempts to find that item number.
                                     If the Item Number is found it populates the Scan Item Field.
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px;">
                                <font size="+1">
                                    <strong>3.  </strong>
                                    If an Item is found in your workstation's carton flow zone all the locations in that zone where the item exists are displayed.  
                                    Any open locations in that zone are displayed at the bottom and marked as “Open”.
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px;">
                                <font size="+1">
                                    <strong>4.  </strong>
                                    Any Items not already in the zone must have a Primary OR Secondary pick location as Carton Flow, 
                                    otherwise that item will not be allowed to be put away to the carton flow.
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px;">
                                <font size="+1">
                                    <strong>5.  </strong>
                                    If the Item is not found in your zone all open locations are displayed.  
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px;">
                                <font size="+1">
                                    <strong>6.  </strong>
                                    Once a location is selected the quantity Box is displayed.  
                                    After you enter the quantity to put in that location a submit button is displayed.
                                </font>
                            </div>
                            <div class="row" style="padding-top:20px; padding-left:30px; padding-bottom:30px;">
                                <font size="+1">
                                    <strong>7.  </strong>
                                    The submit button press will update that Inventory Map location. 
                                </font>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>