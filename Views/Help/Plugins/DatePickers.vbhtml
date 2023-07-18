<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Date Pickers
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                Date pickers are available to any date type field.  They allow you to choose a date to use, but you may also type in the field to achieve the same result, so long as the formats are compatible.
            </div>
            <div class="col-md-12">
                The field below is an example of a date picker.  Click in the field to open the date picker interface.
            </div>
            <div class="col-md-3">
                <input type="text" class="date-picker form-control" />
            </div>
            <div class="col-md-12">
                The date picker will appear with the current date selected. <br />
                <img src="~/images/Help/DatePicker/DatePicker.png" alt="Opened date picker" />
            </div>
            <div class="col-md-12">
                In order to select a date using the picker:
                <ul>
                    <li>Click inside the input box to launch the picker.</li>
                    <li>
                        Year:
                        <ol>
                            <li>Click the month and year combination in order to expand the list of months.  Clicking this area again will cause it to expand further.</li>
                            <li>Click the arrows on either side of the bar you clicked in order to navigate further away from the selected year.</li>
                        </ol>
                    </li>
                    <li>
                        Month:
                        <ol>
                            <li>From the opening area of the date picker click the month and year.</li>
                            <li>Click the desired month from the list.</li>
                        </ol>
                    </li>
                    <li>
                        Day:
                        <ol>
                            <li>From the opening area of the date picker click the day desired, or navigate the months/years until you find the day desired.</li>
                        </ol>
                    </li>
                </ul>
                In order to pick a date without using the date picker you may type in the text area as you normally would.  The date must be recognizable as a date.  The safest format is MM/DD/YYYY 
                where M is month, D is day and Y is year.  If you need to add a time to this date you can type the time in the format: HH:MM:SS where H is hour, M is minute and S is second.  
                Most fields will truncate the time, but some can use it.  Other formats may function normally, but some may have unexpected results.  Sticking with the aforementioned formats is recommended.
            </div>
        </div>
    </div>
</div>
<script>
    // need this because the date picker plugin loaded globally to initialize won't be executed in this page.
    $(document).ready(function () {
        $('.date-picker').datetimepicker({
            pickTime: false,
            minDate: '01/01/1900'
        });
    });
</script>