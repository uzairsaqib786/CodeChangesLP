<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout = nothing
End Code
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Typeaheads (Input Suggestions)
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        Typeaheads are suggestions that are tied to user input and a particular input box.  They are similar to a traditional dropdown as the one below.  They differ in that 
                        there are many more options and none of them are typically required to match.  For example:  The user is making an adjustment to inventory stock.  Reasons for this adjustment 
                        would be suggested by a typeahead when the user was prompted for the reason for the adjustment.  The suggestions would include choices like "Stock quantity was found to be incorrect 
                        during a recount of the item at this location." or something similar, making it easier for the user to click a suggestion instead of typing in the entire statement.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        The example below uses US states for each of the inputs.  On the left is a normal dropdown, in the middle is the typeahead and on the right is a standard input.  The middle input will
                        match against known values and suggest ones that are close to the value it contains.  Try typing the beginning of a state name in the typeahead box.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-3">
                        <label>Traditional Dropdown:</label>
                        <select class="form-control" id="TraditionalDropDown"></select>
                    </div>
                    <div class="col-md-3 col-md-offset-1">
                        <label>Input With Typeahead:</label>
                        <input id="TypeaheadExample" type="text" class="form-control typeahead" placeholder="Text to match against" />
                    </div>
                    <div class="col-md-3 col-md-offset-1">
                        <label>Normal Input Without Typeahead:</label>
                        <input type="text" class="form-control" placeholder="Example Text" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Help/TypeaheadExample.js"></script>