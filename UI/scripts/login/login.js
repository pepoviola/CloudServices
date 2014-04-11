$(function(){
    var textfield = $("input[name='user']");
    var passfield =  $("input[name='password']")
            $('button[type="submit"]').click(function(e) {
                e.preventDefault();
                //little validation just to check username
                if (textfield.val() == "" || passfield.val() == "" ) {
                    //remove success mesage replaced with error message
                    $("#output").removeClass(' alert alert-success');
                    $("#output").addClass("alert alert-danger animated fadeInUp").html("sorry enter a username / password ");
                }

            });
});