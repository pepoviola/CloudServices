
$('#login_submit').click(function(ev) {
    // check if are filled username and password
    // DRY
    var valid = true;
    ctrls = ["txt_login_username", "txt_login_passwd"];
    $.each(ctrls, function (k, v) {
        if ($("#" + v).val() == "") { valid = false; $("#" + v).css({"border-color":"red"}) }
    });

    if (!(valid)) {
        ev.preventDefault();
        // if there is an error before hide first
        $('#alert_div').hide();
        $('#alert_div_complete').show();

    }

    //if ($('#txt_login_username').val() == "" || $('#txt_login_passwd').val() == "" ) {
    //    ev.preventDefault();
    //    // if there is an error before hide first
    //    $('#alert_div').hide();
    //    $('#alert_div_complete').show();
    //}
    
})