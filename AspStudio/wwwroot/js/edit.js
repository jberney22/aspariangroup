// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    BindControls();
    $("#EagleBidPrice").blur(function () {
        var amt = parseFloat(this.value);
        $(this).val(amt.toFixed(2));
        $('#EagleBidSales').val($("#EagleBidPrice").val());
    });

    $("#AcceptedDate").blur(function () {
        $('#EagleBidSales').val($("#EagleBidPrice").val());
    });
    $("#CompetitorPrice").blur(function () {
        var amt = parseFloat(this.value);
        $(this).val(amt.toFixed(2));
    });

    // chkBoxMobilization
    $('#chkBoxMobilization').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {
            $('#txtMobilization').removeAttr('disabled');
            $('#dtMobilization').removeAttr('disabled');
        }
        else {


            $('#txtMobilization').attr('disabled', 'disabled');
            $('#dtMobilization').attr('disabled', 'disabled');

        }

    });


    $("#txtMobilization").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtMobilization').val(new Date().toLocaleDateString('en-CA'));
        }
    });

    // chkBoxMobilization end

    // Prep 1/4 
    $('#chkBoxPrep14').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {

            $('#txtPrep14').removeAttr('disabled');
            $('#dtPrep14').removeAttr('disabled');
        }
        else {
            $('#txtPrep14').attr('disabled', 'disabled');
            $('#dtPrep14').attr('disabled', 'disabled');

        }

    });
    $("#txtPrep14").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtPrep14').val(new Date().toLocaleDateString('en-CA'));
        }
    });

    // Prep 1/4 end

    // Prep 1/2
    $('#chkBoxPrep12').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {

            $('#txtPrep12').removeAttr('disabled');
            $('#dtPrep12').removeAttr('disabled');
        }
        else {
            $('#txtPrep12').attr('disabled', 'disabled');
            $('#dtPrep12').attr('disabled', 'disabled');

        }

    });

    $("#txtPrep12").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtPrep12').val(new Date().toLocaleDateString('en-CA'));
        }
    });
    // Prep 1/2 end
    // Prep Done
    $('#chkBoxPrepDone').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {

            $('#txtPrepDone').removeAttr('disabled');
            $('#dtPrepDone').removeAttr('disabled');
        }
        else {
            $('#txtPrepDone').attr('disabled', 'disabled');
            $('#dtPrepDone').attr('disabled', 'disabled');

        }

    });
    $("#txtPrepDone").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtPrepDone').val(new Date().toLocaleDateString('en-CA'));
        }
    });
    // Prep Done end

    // Removal 1/2
    $('#chkBoxRemoval12').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {

            $('#txtRemoval12').removeAttr('disabled');
            $('#dtRemoval12').removeAttr('disabled');
        }
        else {
            $('#txtRemoval12').attr('disabled', 'disabled');
            $('#dtRemoval12').attr('disabled', 'disabled');

        }

    });
    $("#txtRemoval12").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtRemoval12').val(new Date().toLocaleDateString('en-CA'));
        }
    });
    //Removal 1/2 end

    // Demo done
    $('#chkBoxDemoDone').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {

            $('#txtDemoDone').removeAttr('disabled');
            $('#dtDemoDone').removeAttr('disabled');
        }
        else {
            $('#txtDemoDone').attr('disabled', 'disabled');
            $('#dtDemoDone').attr('disabled', 'disabled');

        }

    });
    $("#txtDemoDone").blur(function () {
        var mobilization = $(this).val();
        if (mobilization != '') {
            $('#dtDemoDone').val(new Date().toLocaleDateString('en-CA'));
        }
    });
    // Demo done end


    // Inventory Return
    $('#chkBoxInventory').change(function () {
        var isChecked = $(this).is(':checked');

        if (isChecked) {
            $('#dtInventory').removeAttr('disabled');
        }
        else {
            $('#dtInventory').attr('disabled', 'disabled');

        }

    });

    // Inventory Return end

});

function BindControls() {
    $("#clientName").autocomplete({
        source: function (request, response) {

            var val = request.term;
            $.ajax({
                // url: "Customer/GetCustomerList?name=" + val,
                url: "@Url.Action("GetCustomerList", "Customer")",
                type: "GET",
                data: "name=" + val,
                success: function (data) {

                    response($.map(data, function (item) {
                        return { value: item }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 2   // MINIMUM 1 CHARACTER TO START WITH.
    });
}
