// PeakLogix Proprietary and Confidential
// Copyright Peaklogix 2022

function startClientCertificates() {
    $('#certContainer').removeClass('col-md-12').addClass('col-md-4').addClass('col-md-offset-4');
    $('#serverCertSteps, #serverContinue').hide();
    $('#certMessage').text("Enter a Friendly name for your pc, click generate to recieve your client certificate");
    $('#clientCertInputs').show();
};

function generating() {
    var text = $('#generateButton').text();
    if (text.length < 3) {
        $('#generateButton').text(text + ".");
    } else {
        $('#generateButton').text('');
    };
};


function generateCertificate() {
    $('#generateText').text("Generating");
    var generateInterval = setInterval(generating, 400);
    $.ajax({
        url: "/Certificates/GenerateClientCert",
        type: "POST",
        data: {certName: $('#certName').val()},
        success: function (result) {
            clearInterval(generateInterval);
            $('#generateButton').text('');
            if (result == "Success") {
                $('#certMessage').text("Install the client certificate you just received, and restart your web browser to connect to the application");
                $('#certContainer').addClass('col-md-12').removeClass('col-md-4').removeClass('col-md-offset-4');
                $('#clientCertInputs').hide();
                if (isAndroid()) {
                    alert($('#certName').val())
                    window.location.href = '/Certificates?makeCookie=' + $('#certName').val();
                    $('#androidCertSteps').show();
                } else {
                    $('#clientCertSteps').show();
                    window.location.href = '/api/LLExport/GetFile?fileName=client' + $('#certName').val() + 'cert.pfx&WSID=Cert&cert=1';
                };
            } else if (result == "EXISTS") {
                $('#generateText').text("Generate")
                alert("This PC Name has already been registered with the application, try again");
            } else {
                alert("An Error Occured when trying to generate your certificate: " + result);
            };
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("An Error Occured: " + xhr.status);
        }
    });
}

$('#serverContinue').hide();

function isAndroid() {
    return $('#isAndroid').val().toLowerCase() == 'true';
};

$(document).ready(function () {
    var servercertCount = setInterval(function () {
        var val = $('#ServerCount').text() - 1;
        $('#ServerCount').text(val);
        if (val == 0) {
            $('#certContainer').removeClass('col-md-offset-4').removeClass('col-md-4').addClass('col-md-12');
            clearInterval(servercertCount)
            window.location.href = '/rootCA.der.crt';
            $('#certMessage').text("After you have downloaded your server certificate follow these steps and click Continue when finished");
            $('#serverContinue').show();
            $('#serverCertSteps').show();
        };
    }, 1000);

    $('#certName').on('input', function () {
        setNoSpaces($(this));
    });
});