<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<!--<form id="live-search" action="" class="styled" method="post">
    <fieldset>
        <input type="text" class="text-input" id="filter" value="" />
        <!--<span id="filter-count"></span>-->
   <!--</fieldset>
</form>-->
<div class="PageDiv">
<!--<div class="row">-->
    <div class="col-md-12">
        <div class="panel-group" id="PickToteSetupAccordion">
           <div class="panel panel-info">
               <div class="panel-heading">
                   <a data-toggle="collapse" data-parent="#PickToteSetupAccordion" data-target="#PickToteSetupOverview">
                       <h3 class="panel-title">
                           Overview
                           <span class="accordion-caret-down"></span>
                       </h3>
                   </a>
               </div>
               <div class="panel-body collapse accordion-toggle" id="PickToteSetupOverview">
                   <div class="row">
                       <div class="col-md-12">
                           This page is the <strong>Pick Tote Setup</strong> page. It gets pick transactions and assigns them to tote ids and a batch id
                       </div>
                   </div>
                   <div class="row" style="padding-top:5px;">
                       <div class="col-md-5">
                           <img src="/Areas/Induction/Images/HelpPickToteSetup/PickToteSetupPage1New.png" style="width: 100%" alt="Pick Tote Setup" usemap="#picktotesetupmap" />
                        </div>
                       <div class="col-md-7">
                           <img src="/Areas/Induction/Images/HelpPickToteSetup/PickToteSetupPage2New.png" style="width: 100%" alt="Pick Tote Setup" usemap="#picktotesetupmap" />
                       </div>
                   </div>
                   <div class="row" style="padding-top:5px;">
                       <div class="col-md-12">
                           <div class="panel-group" id="PickToteSetupInfo">
                               <div class="panel panel-info">
                                   <div class="panel-heading">
                                       <a data-toggle="collapse" data-parent="#PickToteSetupInfo" data-target="#PickToteSetupInfo_1">
                                           <h3 class="panel-title">
                                               1 | Batch Setup
                                               <span class="accordion-caret-down"></span>
                                           </h3>
                                       </a>
                                   </div>
                                   <div class="panel-body collapse accordion-toggle" id="PickToteSetupInfo_1">
                                       <div class="row">
                                           <div class="col-md-12">
                                               <div class="panel-group" id="BatchSetupInfo">
                                                   <div class="panel panel-info">
                                                       <div class="panel-heading">
                                                           <a data-toggle="collapse" data-parent="#BatchSetupInfo" data-target="#BatchSetupInfo_1">
                                                               <h3 class="panel-title">
                                                                   Information
                                                                   <span class="accordion-caret-down"></span>
                                                               </h3>
                                                           </a>
                                                       </div>
                                                       <div class="panel-body collapse accordion-toggle" id="BatchSetupInfo_1">
                                                           <div class="row">
                                                               <div class="col-md-12">
                                                                   <strong>Information:</strong>
                                                                   <ul>
                                                                       <li><strong>Batch ID:</strong> The batch id for this current tote setup</li>
                                                                       <li><strong>Mixed Zones:</strong> The number of pick order numbers able to be inducted that are on zone that is both a carousel and off carousel</li>
                                                                       <li><strong>Carousel:</strong> The number of pick order numbers able to be inducted that are on a carousel zone</li>
                                                                       <li><strong>Off-Carousel:</strong> The number of pick order numbers able to be inducted that are on a off-carousel zone</li>
                                                                   </ul>
                                                               </div>
                                                           </div>
                                                       </div>
                                                   </div>
                                                   <div class="panel panel-info">
                                                       <div class="panel-heading">
                                                           <a data-toggle="collapse" data-parent="#BatchSetupInfo" data-target="#BatchSetupInfo_2">
                                                               <h3 class="panel-title">
                                                                   Buttons
                                                                   <span class="accordion-caret-down"></span>
                                                               </h3>
                                                           </a>
                                                       </div>
                                                       <div class="panel-body collapse accordion-toggle" id="BatchSetupInfo_2">
                                                           <div class="row">
                                                               <div class="col-md-12">
                                                                   <strong>Buttons:</strong>
                                                                   <ul>
                                                                       <li><strong>New Batch with ID:</strong> Creates a new batch with the next batch id value
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/NewBatchIDButt.png" alt="New Batch with ID Button"/></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>New Batch:</strong> Creates a new batch without using the next batch id (the batch id textbox is cleared)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/NewBatchButt.png" alt="New Batch Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>Pick Batch Manager:</strong> Opens the Pick Batch Manager modal
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManButt.png" alt="Pick Batch Manager Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>Refresh Order Counts:</strong> Refreshes the count info for the number of Mixed, Carousel, and Off-Carousel zones
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/RefreshCountButt.png" alt="Refresh Count Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>Print:</strong> Prints the desired report or label
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/BatchPrintButt.png" alt="Batch Print Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>Process:</strong> Inducts the current tote setup and creates a pick batch
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ProcessButt.png" alt="Proccess Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li>
                                                                           <strong>Fill All Empty Tote IDs:</strong> Fills all empty tote ids in the Tote Setup with the next available Tote ID numbers
                                                                           <ul>
                                                                               <li><img src="/Areas/Induction/Images/HelpPickToteSetup/FillAllEmptyToteIDsButt.png" alt="Fill All Empty Tote IDs Button" /></li>
                                                                           </ul>
                                                                       </li>
                                                                       <li>
                                                                           <strong>Fill Next Empty Tote ID:</strong> Fills the next empty tote id in the Tote Setup with the next available Tote ID number
                                                                           <ul>
                                                                               <li><img src="/Areas/Induction/Images/HelpPickToteSetup/FillNextEmptyToteIDButt.png" alt="Fill Next Empty Tote ID Button" /></li>
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
                               <div class="panel panel-info">
                                   <div class="panel-heading">
                                       <a data-toggle="collapse" data-parent="#PickToteSetupInfo" data-target="#PickToteSetupInfo_2">
                                           <h3 class="panel-title">
                                               2 | Tote Setup
                                               <span class="acordion-caret-down"></span>
                                           </h3>
                                       </a>
                                   </div>
                                   <div class="panel-body collapse accordion-toggle" id="PickToteSetupInfo_2">
                                       <div class="row">
                                           <div class="col-md-12">
                                               <div class="panel-group" id="ToteSetupInfo">
                                                   <div class="panel panel-info">
                                                       <div class="panel-heading">
                                                           <a data-toggle="collapse" data-parent="#ToteSetupInfo" data-target="#ToteSetupInfo_1">
                                                               <h3 class="panel-title">
                                                                   Information
                                                                   <span class="accordion-caret-down"></span>
                                                               </h3>
                                                           </a>
                                                       </div>
                                                       <div class="panel-body collapse accordion-toggle" id="ToteSetupInfo_1">
                                                           <div class="row">
                                                               <div class="col-md-12">
                                                                   <strong>Information:</strong>
                                                                   <ul>
                                                                       <li><strong>Position:</strong> The location in the batch setup this tote is located</li>
                                                                       <li><strong>Tote ID:</strong> The tote id assigned to this position</li>
                                                                       <li><strong>Order Number:</strong> The order number assigned to the tote id</li>
                                                                   </ul>
                                                               </div>
                                                           </div>
                                                       </div>
                                                   </div>
                                                   <div class="panel panel-info">
                                                       <div class="panel-heading">
                                                           <a data-toggle="collapse" data-parent="#ToteSetupInfo" data-target="#ToteSetupInfo_2">
                                                               <h3 class="panel-title">
                                                                   Buttons
                                                                   <span class="accordion-caret-down"></span>
                                                               </h3>
                                                           </a>
                                                       </div>
                                                       <div class="panel-body collapse accordion-toggle" id="ToteSetupInfo_2">
                                                           <div class="row">
                                                               <div class="col-md-12">
                                                                   <strong>Buttons:</strong>
                                                                   <ul>
                                                                       <li><strong>Clear:</strong> Clears the tote id and order number
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Tote Clear Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>Print:</strong> Prints the item label for the desired position
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/TotePrintButt.png" alt="Tote Print Button" /></li>
                                                                            </ul>
                                                                       </li>
                                                                       <li><strong>View Order:</strong> Opens the order status page with the desired order number filled in
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewOrderButt.png" alt="View Order Button" /></li>
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
                   </div>
               </div>
           </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#PickToteSetupAccordion" data-target="#PickToteSetupFunctionAccordion">
                        <h3 class="panel-title">
                            Functionality
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionAccordion">
                    <div class="row">
                        <div class="col-md-12">
                            This panel discusses the functionality within the <strong>Pick Tote Setup</strong> page
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-group" id="PickToteSetupFunctionInfo">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_1">
                                            <h3 class="panel-title">
                                                Populating the Page
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="PopulatePickToteSetup">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PopulatePickToteSetup" data-target="#PopulatePickToteSetup_1">
                                                                <h3 class="panel-title">
                                                                    Autofilling in Data
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PopulatePickToteSetup_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Within the preferences page there are 6 preferences (shown along with a description below) which control autofilling the page. 
                                                                            The combination of these determines what data will get auto filled for page. Use the descriptions below to determine the 
                                                                           values for the each of them.
                                                                           <ul>
                                                                               <li><strong>Auto Select Pick Orders:</strong> Will autofill only if the pick batch manager preference is unchecked. 
                                                                                   Will use the pick order sort in order to get the correct order for the order numbers and then will fill in orders
                                                                                   that meet the checked zone criteria (mixed, carousel, or off-carousel).
                                                                               </li>
                                                                               <li><strong>Pick Order Sort:</strong> How to sort the orders when using autofills</li>
                                                                               <li><strong>Auto Pick Tote ID's:</strong> Will autofill each position's tote id with the next tote id value. 
                                                                                   This will always happen if it is enabled
                                                                               </li>
                                                                               <li><strong>Use Pick Batch Manager:</strong> Tells if the default filter or zone will auto fill orders. 
                                                                                   If none are enabled the modal will open
                                                                               </li>
                                                                               <li><strong>Use Default Filter:</strong> Signifies that the default filter will be used. 
                                                                                   Only applies if the Use Pick Batch Manager preference is enabled.
                                                                               </li>
                                                                               <li><strong>Use Default Zone:</strong> Signifies that the default zone will be used.
                                                                                   Only applies if the Use Pick Batch Manager preference is enabled.
                                                                               </li>
                                                                           </ul>
                                                                        </li>
                                                                        <li>Once the preferences are setup, go to the Pick Tote Setup page  and
                                                                            enter the desired batch id into the <strong>Batch ID</strong> textbox
                                                                        </li>
                                                                        <li>Once entered click off of the textbox and the page will autofill with data if it exists</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PopulatePickToteSetup" data-target="#PopulatePickToteSetup_2">
                                                                <h3 class="panel-title">
                                                                    Using the Pick Batch Manager Modal
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PopulatePickToteSetup_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Tote Setup page opened</li>
                                                                        <li>Enter in the desired batch id</li>
                                                                        <li>Once entered click off it in order to trigger the autofills. 
                                                                            This does not impact the pick batch manager modal
                                                                        </li>
                                                                        <li>When ready press the <strong>Pick Batch Manager</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManButt.png" alt="Pick Batch Manager Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed the modal will open to the section that is set as default (filter or zone), 
                                                                            or if non are selected it will open on the filter section. Use the radio buttons in the top left
                                                                            to change the section that is displayed.
                                                                        </li>
                                                                        <li>Once in the modal go to either the zone or filter section and follow steps below:
                                                                            <ul>
                                                                                <li>For Filter:
                                                                                    <ol>
                                                                                        <li>Use the Select Saved Filter typeahead in order to select the desired filter</li>
                                                                                        <li>Once selected the all valid order numbers that fulfill this filter are 
                                                                                            shown within the batch results section, under the Order Number column
                                                                                        </li>
                                                                                    </ol>
                                                                                </li>
                                                                                <li>For Zone:
                                                                                    <ol>
                                                                                        <li>Find the desired zone to fill the order number table</li>
                                                                                        <li>Press the corresponding <strong>View</strong> button (shown below) to show the 
                                                                                            Batch Results tab and populate the order number list
                                                                                            <ul>
                                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ZoneViewButt.png" alt="Pick Batch Manager Zone View Button" /></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ol>
                                                                                </li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once one of the paths are completed, either select an order by clicking on it within the list of order numbers
                                                                            and press the <strong>Select Order</strong> button (shown below) to select only the highlighted order number, or simply
                                                                            press the <strong>Select All</strong> button (shown below) to select all order numbers from the list
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectOrderButt.png" alt="Pick Batch Manager Select Order Button" /></li>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectAllButt.png" alt="Pick Batch Manager Select All Button" /></li>
                                                                            </ul> 
                                                                        </li>
                                                                        <li>Once selected the order number will be populated into one of the tote ids. 
                                                                            The modal also stays open to select more or perform different operations
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PopulatePickToteSetup" data-target="#PopulatePickToteSetup_3">
                                                                <h3 class="panel-title">
                                                                    Manual Entry
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PopulatePickToteSetup_3">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Tote Setup page opened</li>
                                                                        <li>Enter in the desired data for tote ids, order numbers, and the batch id. 
                                                                            For manual entry the batch id does not need to be filled out first 
                                                                            (highly recommend to fill it first though so that autofilling does not overwrite any values) 
                                                                            and the order number entered has to be a valid order number 
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
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_2">
                                            <h3 class="panel-title">
                                                Creating a New Batch
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup page open</li>
                                                    <li>Press either the <strong>New Batch with ID</strong> or <strong>New Batch</strong> button (both shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/NewBatchIDButt.png" alt="New Batch with ID Button" /></li>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/NewBatchButt.png" alt="New Batch Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>When pressed a pop up (shown below) appears. Asking if you want to create a new batch. To continue press <strong>OK</strong>
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/NewBatchPopUpNew.png" style="width: 45%" alt="New Batch Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>When either one is pressed a pop up (shown below) appears asking if you want to keep the current configuration. 
                                                        To keep the current configuration press <strong>OK</strong>. Otherwise it will clear the setup and only fill in the 
                                                        Batch ID if the <strong>New Batch with ID</strong> button was pressed
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ConfigPopUpNew.png" style="width: 45%" alt="New Batch Config Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_3">
                                            <h3 class="panel-title">
                                                Opening the Pick Batch Modal
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup screen open</li>
                                                    <li>Fill in the batch id field, and click off of it in order to trigger auto fills</li>
                                                    <li>When ready press the <strong>Pick Batch Manager</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManButt.png" alt="Pick Batch Manager Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the modal will open</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_4">
                                            <h3 class="panel-title">
                                                Refreshing the Counts
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup page open</li>
                                                    <li>Press the <strong>Refresh Order Counts</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/RefreshCountButt.png" alt="Refresh Count Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed all the count data is recalculated and displayed</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_5">
                                            <h3 class="panel-title">
                                                Printing
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_5">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="PrintInfo">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PrintInfo" data-target="#PrintInfo_1">
                                                                <h3 class="panel-title">
                                                                    Batch Setup Print
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PrintInfo_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Tote Setup page opened</li>
                                                                        <li>Fill in the batch id and at least one tote id field. This enables all the print functions in this section</li>
                                                                        <li>Press the Print button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/BatchPrintButt.png" alt="Batch Print Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed a dropdown listing out the different print options is shown. Press the desired print option.</li>
                                                                        <li>Once pressed the desired report is either printed or previewed depending on the Print Directly to Printer preference</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PrintInfo" data-target="#PrintInfo_2">
                                                                <h3 class="panel-title">
                                                                    Tote Setup Print
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PrintInfo_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Tote Setup page open</li>
                                                                        <li>Fill in the desired tote id and order number for the position that is going to be printed</li>
                                                                        <li>Press the Print button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/TotePrintButt.png" alt="Tote Print Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the desired report is either printed or previewed depending on the Print Directly to Printer preference</li>
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
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_6">
                                            <h3 class="panel-title">
                                                Process Current Setup
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_6">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup page opened</li>
                                                    <li>Fill out the page to the desired setup</li>
                                                    <li>Press the <strong>Process</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ProcessButt.png" alt="Proccess Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed a pop up (shown below) appears. To continue processing press <strong>OK</strong>
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ProcessPopUpNew.png" style="width: 45%" alt="Process Pop Up" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once confirmed the current setup is processed</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_7">
                                            <h3 class="panel-title">
                                                Clear Position
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_7">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup page opened</li>
                                                    <li>Press the <strong>Clear</strong> button (shown below) designated to the position that is to be cleared
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Tote Clear Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the designated row's tote id and order number fields are emptied</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickToteSetupFunctionInfo" data-target="#PickToteSetupFunctionInfo_8">
                                            <h3 class="panel-title">
                                                View Order
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickToteSetupFunctionInfo_8">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <strong>Steps:</strong>
                                                <ol>
                                                    <li>Have the Pick Tote Setup page opened</li>
                                                    <li>Fill in the desired position with a valid order number</li>
                                                    <li>Once filled press the <strong>View Order</strong> button (shown below)
                                                        <ul>
                                                            <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewOrderButt.png" alt="View Order Button" /></li>
                                                        </ul>
                                                    </li>
                                                    <li>Once pressed the Order Status page is opened with the desired order number's info being displayed</li>
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
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#PickToteSetupAccordion" data-target="#PickBatchManagerModal">
                        <h3 class="panel-title">
                            Pick Batch Manager Modal
                            <span class="accordion-caret-down"></span>
                        </h3>
                    </a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="PickBatchManagerModal">
                    <div class="row">
                        <div class="col-md-12">
                            This is the <strong>Pick Batch Manager</strong> modal. It allows for a user to find order numbers based on filters or zones and select them
                        </div>
                    </div>
                    <div class="row" style="padding-top:5px;">
                        <div class="col-md-12">
                            <div class="panel-group" id="PickBatchManagerAccordion">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickBatchManagerAccordion" data-target="#PickBatchManagerOverview">
                                            <h3 class="panel-title">
                                                Overview
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickBatchManagerOverview">
                                        <!-- Origional code commented out
                                            <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManRadio.png" alt="Pick Batch Manager Radio Buttons" usemap="#pickbatchmanradiomap" />
                                            </div>
                                        </div>
                                            -->
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-6">
                                                <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManFilterSelectNew.png" style="width: 70%" alt="Pick Batch Manager Filter Radio Buttons" usemap="#pickbatchmanradiomap1" />
                                            </div>
                                            <div class="col-md-6">
                                                <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManZoneSelectNew.png" style="width: 70%" alt="Pick Batch Manager Zone Radio Buttons" usemap="#pickbatchmanradiomap2" />
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="PickBatchManagerInfo">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerInfo" data-target="#PickBatchManagerInfo_1">
                                                                <h3 class="panel-title">
                                                                    1 | Filters Section
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerInfo_1">
                                                            <div class="row">
                                                                <!--div class="col-md-12">
                                                                    <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManFilters.png" alt="Pick Batch Manager Filters Page" usemap="#pickbatchmanfiltersmap" />
                                                                </div>-->
                                                                <!--20170206 - JMP - Changed Code to make it so all images are on different lines all the time.<div class="row">-->
                                                                    <!--20170206 - JMP - Changed Code to make it so all images are on different lines all the time.<div class="col-md-4">-->
                                                                <div>
                                                                    <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManFiltersNew1.png" style="width: 80%" alt="Pick Batch Manager Filters Page" data-parent="#PickBatchManagerInfo_1" data-target="#FilterInfo_1" />
                                                                </div><br />
                                                                <!--20170206 - JMP - Changed Code to make it so all images are on different lines all the time.<div class="col-md-5">-->
                                                                    <div>
                                                                        <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManFiltersNew2.png" style="width: 100%" alt="Pick Batch Manager Filters Page" data-parent="#PickBatchManagerInfo_1" data-target="#FilterInfo_2" />
                                                                    </div><br />
                                                                <!--20170206 - JMP - Changed Code to make it so all images are on different lines all the time.<div class="col-md-3">-->
                                                                    <div>
                                                                        <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManFiltersNew3.png" style="width: 50%" alt="Pick Batch Manager Filters Page" data-parent="#PickBatchManagerInfo_1" data-target="#FilterInfo_3" />
                                                                    </div>
                                                                    <!--20170206 - JMP - Changed Code to make it so all images are on different lines all the time.</div>-->
                                                                </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="FilterInfo">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#FilterInfo" data-target="#FilterInfo_1">
                                                                                    <h3 class="panel-title">
                                                                                        1 | Filter Functions 
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="FilterInfo_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Select Saved Filter:</strong> Typeahead to select the filter to view and/or edit</li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Buttons:</strong>
                                                                                        <ul>
                                                                                            <li>
                                                                                                <strong>Rename Filter</strong> button: Opens pop up to rename selected filter
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/RenameFilterButt.png" alt="Rename Filter Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>View Default</strong> button: Views the default filter
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewDefButt.png" alt="View Default Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Set Default</strong> button: Sets the selected filter as the default
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SetDefButt.png" alt="Set Default Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Clear Default</strong> button: Unmarks the default filter
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ClearDefButt.png" alt="Clear Default Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Delete Filter</strong> button: Deletes the selected filter
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Delete Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Add New Filter</strong> button: Adds a new filter
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewButt.png" alt="Add New Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#FilterInfo" data-target="#FilterInfo_2">
                                                                                    <h3 class="panel-title">
                                                                                        2 | Filter
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="FilterInfo_2">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Sequence:</strong> The sequence that the individual filters will be executed in. The order does not impact the result</li>
                                                                                            <li><strong>Field:</strong> The column to filter on</li>
                                                                                            <li><strong>Criteria:</strong> The condition that is going to be applied</li>
                                                                                            <li><strong>Value:</strong> The value that the condition will apply with</li>
                                                                                            <li><strong>And/Or:</strong> Whether the next filter is going to be inclusive(AND) or exclusive(OR) with the current filter.
                                                                                                And is 1 and 1 is true, while 1 and 0 is false. Or is 1 or 1 is true, 1 or 0 is true, while 0 or 0 is false.
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Buttons:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Save Filter Row</strong> button: Saves the current filter row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SaveButt.png" alt="Save Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li><strong>Delete Filter Row</strong> button: Deletes the filter row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Tote Clear Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li><strong>Add New Filter Row</strong> button: Adds a new filter row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewButt.png" alt="Add New Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#FilterInfo" data-target="#FilterInfo_3">
                                                                                    <h3 class="panel-title">
                                                                                        3 | Order By
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="FilterInfo_3">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information</strong>
                                                                                        <ul>
                                                                                            <li><strong>Sequence:</strong> The sequence that each order by will be added. 
                                                                                                Sequence order does impact the order that the data will be returned.
                                                                                            </li>
                                                                                            <li><strong>Field:</strong> The column to order by on</li>
                                                                                            <li><strong>Sort Order:</strong> The order for ordering the corresponding column by</li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Buttons:</strong>
                                                                                        <ul>
                                                                                            <li>
                                                                                                <strong>Save Order By Row</strong> button: Saves the current order by row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SaveButt.png" alt="Save Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Delete Order By Row</strong> button: Deletes the order by row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Tote Clear Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li>
                                                                                                <strong>Add New Order By Row</strong> button: Adds a new order by row
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewButt.png" alt="Add New Button" /></li>
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
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerInfo" data-target="#PickBatchManagerInfo_2">
                                                                <h3 class="panel-title">
                                                                    2 | Zones Section
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerInfo_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/Induction/Images/HelpPickToteSetup/PickBatchManZones.png" style="width: 100%" alt="Pick Batch Manager Zones" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="ZoneInfo">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#ZoneInfo" data-target="#ZoneInfo_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="ZoneInfo_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Zone:</strong> The starting zone for the batch</li>
                                                                                            <li><strong>Batch Type:</strong> Tells whether all orders are in the zone or if they start in the zone</li>
                                                                                            <li><strong>Total Orders:</strong> The total number of order numbers for this zone and batch type</li>
                                                                                            <li><strong>Total Locations:</strong> The total number locations within this zone and batch type</li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Buttons:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Default</strong> button: Marks this zone and batch type as default
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ZoneDefButt.png" alt="Pick Batch Manager Zones Default Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li><strong>View</strong> button: Populates the order number list on the <strong>Batch Results</strong> 
                                                                                                tab will all order numbers for this zone and batch type
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ZoneViewButt.png" alt="Pick Batch Manager Zones Viwe Button" /></li>
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
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerInfo" data-target="#PickBatchManagerInfo_3">
                                                                <h3 class="panel-title">
                                                                    3 | Batch Results
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerInfo_3">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <img src="/Areas/Induction/Images/HelpPickToteSetup/BatchResultsNew.png" style="width: 100%" alt="Pick Batch Manager Batch Results" />
                                                                </div>
                                                            </div>
                                                            <div class="row" style="padding-top:5px;">
                                                                <div class="col-md-12">
                                                                    <div class="panel-group" id="BatchResultsInfo">
                                                                        <div class="panel panel-info">
                                                                            <div class="panel-heading">
                                                                                <a data-toggle="collapse" data-parent="#BatchResultsInfo" data-target="#BatchResultsInfo_1">
                                                                                    <h3 class="panel-title">
                                                                                        Information
                                                                                        <span class="accordion-caret-down"></span>
                                                                                    </h3>
                                                                                </a>
                                                                            </div>
                                                                            <div class="panel-body collapse accordion-toggle" id="BatchResultsInfo_1">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Information:</strong>
                                                                                        <ul>
                                                                                            <li><strong>Orders Table:</strong> Table filled with order numbers from a filter or from viewing a zone</li>
                                                                                            <li><strong>Transactions Table:</strong> Table filled with transactions that contain the selected order number from the Orders Table</li>
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <strong>Buttons:</strong>
                                                                                        <ul>
                                                                                            <li><strong>View All Order Lines</strong> radio button: Views all transactions for every order number in the Orders Table or just the orders selected
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewAllOrdersRadio.png" alt="Pick Batch Manager Bacth Results View All Orders Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li><strong>Select All</strong> button: Fills each order number into a tote id's order number field
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectAllButt.png" alt="Pick Batch Manager Bacth Results Select All Orders Button" /></li>
                                                                                                </ul>
                                                                                            </li>
                                                                                            <li><strong>Select Order</strong> button: Fills one tote id's order number field with the selected order number
                                                                                                <ul>
                                                                                                    <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectOrderButt.png" alt="Pick Batch Manager Bacth Results Select Order Button" /></li>
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
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#PickBatchManagerAccordion" data-target="#PickBatchManagerFunctions">
                                            <h3 class="panel-title">
                                                Functionality
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctions">
                                        <div class="row">
                                            <div class="col-md-12">
                                                This panel discusses the functionality found within the <strong>Pick Batch Manager</strong> modal
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel-group" id="PickBatchManagerFunctionInfo">
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_1">
                                                                <h3 class="panel-title">
                                                                    Selecting a Filter
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_1">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Click on the Select Saved Filter textbox</li>
                                                                        <li>When clicked a dropdown menu appears below the textbox. It displays some of the existing filters.
                                                                            If you see the desired filter click on it in the menu. Otherwise being typing the desired filter name
                                                                            until you see the filter in the menu. Once you see it click on it
                                                                        </li>
                                                                        <li>Once clicked the page will populate with the filter's data</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_2">
                                                                <h3 class="panel-title">
                                                                    Renaming a Filter
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_2">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter to rename</li>
                                                                        <li>Once selected press the <strong>Rename Filter</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/RenameFilterButt.png" alt="Rename Filter Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once clicked a pop up (shown below) appears. Within here enter the new filter name
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/RenamePopUp.png" alt="Rename Filter Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once the new name is entered press <strong>OK</strong></li>
                                                                        <li>Once pressed the filter is renamed to the new value</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_3">
                                                                <h3 class="panel-title">
                                                                    View Default Filter
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_3">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Have a default filter</li>
                                                                        <li>Press the <strong>View Default</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewDefButt.png" alt="View Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the default printer's information is shown</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_4">
                                                                <h3 class="panel-title">
                                                                    Set Filter as Default
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_4">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Press the <strong>Set Default</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SetDefButt.png" alt="Set Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed a pop up (shown below) appears, confirming to mark this filter as the default one. 
                                                                            To continue press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/MarkDefPopUp.png" alt="Mark Default Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When continued, another pop up (shown below) appears. This one is to confirm 
                                                                            that you want to unmark the filter that is currently marked as the default one.
                                                                            To continue press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ClearDefPopUp.png" alt="Clear Default Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once confirmed the selected filter is marked as the default filter</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_5">
                                                                <h3 class="panel-title">
                                                                    Clear Default
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_5">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open. You do <strong>not</strong> need to select the select the default filter to clear it</li>
                                                                        <li>Press the <strong>Clear Default</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ClearDefButt.png" alt="Clear Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed a pop up (shown below) appears confirming to clear the default. 
                                                                            To continue press <strong>Ok</strong>.
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ClearDefPopUp.png" alt="Clear Default Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the default filter is unmarked</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_6">
                                                                <h3 class="panel-title">
                                                                    Delete Filter
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_6">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Press the <strong>Delete Filter</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Delete Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed a pop up (shown below) appears. This is confirming to delete the selected filter. 
                                                                            To continue press <strong>OK</strong>
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/DelFilterPopUp.png" alt="Delete Filter Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed the filter is deleted</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_7">
                                                                <h3 class="panel-title">
                                                                    Add New Filter
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_7">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Press the <strong>Add New</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewButt.png" alt="Add New Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed a pop up (shown below) appears. Within it enter the new filter name
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewPopUp.png" alt="Add New Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When the filter name is entered press <strong>OK</strong></li>
                                                                        <li>When pressed the page will populate with the new filter. 
                                                                            A new filter is row automatically created. Set this filter row to its
                                                                            desired values and press the <strong>Save</strong> button (shown below).
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SaveButt.png" alt="Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed the filter is officially saved and the new filter row is added</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_8">
                                                                <h3 class="panel-title">
                                                                    Add New Filter or Order By Row
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_8">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Press the <strong>Add New</strong> button (shown below) for either the filter or order by
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/AddNewButt.png" alt="Add New Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed a new row is added to the desired section. Within this row input the desired information</li>
                                                                        <li>Once the filter and/or order by is row's information is to the desired result press the <strong>Save</strong>
                                                                            button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SaveButt.png" alt="Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed the row is saved for the corresponding section</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_9">
                                                                <h3 class="panel-title">
                                                                    Save Changes to Filter or Order By Row
                                                                    <span class="accordion-caret-down"></span> 
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_9">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Within either section change any of the existing rows data</li>
                                                                        <li>When finished press the <strong>Save</strong> button (shown below) assigned to the row that was changed
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SaveButt.png" alt="Save Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed the row's new data is saved</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_10">
                                                                <h3 class="panel-title">
                                                                    Delete Filter or Order By Row
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_10">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Press the <strong>Delete</strong> button (shown below) assigned to the filter or order by row 
                                                                            that's going to be deleted
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ToteClearButt.png" alt="Tote Clear Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed a pop up (shown below) appears. To continue with the delete press <strong>OK</strong>.
                                                                            Notice: this is the pop up for the filter. The order by pop up is same except for the wording in the it
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/DelFilterRowPopUp.png" alt="Delete Filter Row Pop Up" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>When pressed the row is deleted</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_11">
                                                                <h3 class="panel-title">
                                                                    Set Zone as Default
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_11">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Zones section open</li>
                                                                        <li>Click on the <strong>Default</strong> button (shown below) for the desired zone and batch type
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ZoneDefButt.png" alt="Pick Batch Manager Zones Default Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once clicked the desired zone and batch type row is outlined in green marking it as the default</li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_12">
                                                                <h3 class="panel-title">
                                                                    View Order Numbers for Filter or Zone
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_12">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps for Filter:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Filter section open</li>
                                                                        <li>Select the desired filter</li>
                                                                        <li>Once selected the Orders Table on the <strong>Batch Results</strong> tab will populate 
                                                                            with order numbers that fit the filter
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps for Zone:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Zone section open</li>
                                                                        <li>Press the <strong>View</strong> button (shown below) for the desired 
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ZoneViewButt.png" alt="Pick Batch Manager Zones Viwe Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once pressed Orders Table on the <strong>Batch Results</strong> tab will populate
                                                                            with order numbers that are part of the viewed zone and batch type
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_13">
                                                                <h3 class="panel-title">
                                                                    Populate Transactions Table
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_13">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Batch Results tab open</li>
                                                                        <li>Have Order Numbers in the Orders Table</li>
                                                                        <li>Either click on the desired order number within the Orders Table, or click
                                                                            the <strong>View All Order Lines</strong> button (shown below)
                                                                            <ul>
                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/ViewAllOrdersButt.png" alt="Pick Batch Manager Bacth Results View All Orders Button" /></li>
                                                                            </ul>
                                                                        </li>
                                                                        <li>Once clicked the Transactions Table will populate with transactions for the selected order number, 
                                                                            or all order numbers
                                                                        </li>
                                                                    </ol>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="panel panel-info">
                                                        <div class="panel-heading">
                                                            <a data-toggle="collapse" data-parent="#PickBatchManagerFunctionInfo" data-target="#PickBatchManagerFunctionInfo_14">
                                                                <h3 class="panel-title">
                                                                    Select Order Number(s)
                                                                    <span class="accordion-caret-down"></span>
                                                                </h3>
                                                            </a>
                                                        </div>
                                                        <div class="panel-body collapse accordion-toggle" id="PickBatchManagerFunctionInfo_14">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <strong>Steps:</strong>
                                                                    <ol>
                                                                        <li>Have the Pick Batch Manager Modal open</li>
                                                                        <li>Have the Batch Results tab open</li>
                                                                        <li>Have Order Numbers in the Orders Table</li>
                                                                        <li>Perform one of the following actions:
                                                                            <ul>
                                                                                <li>To Select All:
                                                                                    <ol>
                                                                                        <li>Press the <strong>Select All</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectAllButt.png" alt="Pick Batch Manager Bacth Results Select All Orders Button" /></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once pressed all order numbers in the list are added to each tote id. 
                                                                                            Note: There may be more order numbers than tote ids available or vice versa
                                                                                        </li>
                                                                                    </ol>
                                                                                </li>
                                                                                <li>To Select a Single Order Number:
                                                                                    <ol>
                                                                                        <li>Click on the desired order number in the Orders Table</li>
                                                                                        <li>Press the <strong>Select Order</strong> button (shown below)
                                                                                            <ul>
                                                                                                <li><img src="/Areas/Induction/Images/HelpPickToteSetup/SelectOrderButt.png" alt="Pick Batch Manager Bacth Results Select Order Button" /></li>
                                                                                            </ul>
                                                                                        </li>
                                                                                        <li>Once pressed the selected order number is filled in a tote id</li>
                                                                                    </ol>
                                                                                </li>
                                                                            </ul>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="picktotesetupmap">
    <area shape="rect" coords="2, 0, 562, 209" data-target="#PickToteSetupInfo_1" data-toggle="collapse" data-parent="#PickToteSetupInfo" />
    <area shape="rect" coords="568, 1, 1352, 131" data-target="#PickToteSetupInfo_2" data-toggle="collapse" data-parent="#PickToteSetupInfo" />
</map>
<map name="pickbatchmanradiomap1">
    <area shape="rect" coords="0, 0, 133, 108" data-target="#PickBatchManagerInfo_1" data-toggle="collapse" data-parent="#PickBatchManagerInfo" />
    <area shape="rect" coords="133, 0, 256, 108" data-target="#PickBatchManagerInfo_3" data-toggle="collapse" data-parent="#PickBatchManagerInfo" />
</map>
<map name="pickbatchmanradiomap2">
    <area shape="rect" coords="0, 0, 140, 104" data-target="#PickBatchManagerInfo_2" data-toggle="collapse" data-parent="#PickBatchManagerInfo" />
    <area shape="rect" coords="140, 0, 262, 104" data-target="#PickBatchManagerInfo_3" data-toggle="collapse" data-parent="#PickBatchManagerInfo" />
</map>
<map name="pickbatchmanfiltersmap">
    <area shape="rect" coords="0, 0, 1313, 88" data-target="#FilterInfo_1" data-toggle="collapse" data-parent="#FilterInfo" />
    <area shape="rect" coords="1, 93, 870, 186" data-target="#FilterInfo_2" data-toggle="collapse" data-parent="#FilterInfo" />
    <area shape="rect" coords="888, 95, 1313, 184" data-target="#FilterInfo_3" data-toggle="collapse" data-parent="#FilterInfo" />
</map>
<script>
        $(document).ready(function () {
            $("#filter").keyup(function () {

                // Retrieve the input field text and reset the count to zero
                var filter = $(this).val(), count = 0;

                // Loop through the comment list
                //$(".PageDiv li").each(function () {
                $(".PageDiv div").each(function () {

                    // If the list item does not contain the text phrase fade it out
                    if ($(this).text().search(new RegExp(filter, "i")) < 0) {
                        $(this).fadeOut();

                        // Show the list item if the phrase matches and increase the count by 1
                    } else {
                        $(this).show();
                        count++;
                    }
                });

                // Update the count
                var numberItems = count;
                $("#filter-count").text("Number of Comments = " + count);
            });
        });
</script>