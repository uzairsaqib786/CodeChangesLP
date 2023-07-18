﻿// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

var $autocomplete;
if (typeof jQuery != "undefined") {
    $autocomplete = jQuery
} else {
    if (typeof $jq != "undefined") {
        $autocomplete = $jq
    }
}
if (typeof $autocomplete != "undefined") {
    $autocomplete(document).ready(function () {
        if (typeof window.disableAutocomplete === "function") {
            disableAutocomplete()
        }
    });
    $autocomplete(document).ajaxStop(function () {
        disableAutocomplete()
    })
}
var disableAutocomplete = function () {
    $autocomplete("input[type=password]:visible").each(function () {
        if (!($autocomplete(this).css("left") == "-1000px") && $autocomplete(this).prev("input[type=password][aria-hidden=true]").length === 0) {
            $autocomplete(this).before('<input type="password" style="position:absolute;left:-1000px" value="a" aria-hidden="true" tabindex="-1"/><input type="password" style="position:absolute;left:-1000px" value="b" aria-hidden="true" tabindex="-1"/><input type="password" style="position:absolute;left:-1000px" value="c" aria-hidden="true" tabindex="-1"/><input type="password" style="position:absolute;left:-1000px" value="d" aria-hidden="true" tabindex="-1"/>')
        }
    })
}
;