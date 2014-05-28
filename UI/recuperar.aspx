<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="recuperar.aspx.vb" Inherits="UI.recuperar" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CloudServices</title>
    <!-- Bootstrap core CSS -->
    <link href="content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="content/css/login/login.css" rel="stylesheet"/>
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container"> 
				<!-- .btn-navbar is used as the toggle for collapsed navbar content -->
				<a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</a>
				<a class="brand" href="/">CloudServices</a>	

		    </div>
		</div>
	</div>
    <div class="container">
	<div class="wrap">
    <div class="row" id="alertas">
  
        <div  id="alert_div_complete" class="alert alert-error hide"><a class="close" data-dismiss="alert">×</a>
                      
        </div>
        
       
    </div>
    <div class="row">
        <div class="span8 offset1" id="form-regenerate">

            <div class="well">
                <form id="signup" class="form-horizontal">
                    <legend><%=translate("recuperar_passwd")%></legend>
                    <div class="control-group">
                        <label class="control-label"><span id="" class=""><%=translate("email") %></span></label>
                        <div class="controls">
                            <div class="input-prepend">
                                <span class="add-on"><i class="icon-user"></i></span>
                                <input type="email" class="input-xlarge" id="email_to" name="email_to"  />
                            </div>
                        </div>
                    </div>

                    <div class="control-group">
                        <label class="control-label"></label>
                        <div class="controls">
                            <button type="submit" id="regenerar" class="btn btn-success" ><%=translate("recuperar")%></button> 
                        </div>    
                    </div>
               </form>
            </div>
          
        </div>
    </div>
     
</div>
        
    </div>
    <script src="scripts/jquery-2.1.0.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/login/login.js"></script>
    <script>
        var _validEmail = function (value) {

            // http://docs.jquery.com/Plugins/Validation/Methods/email
            var matches = value.match(/((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))+/i);
            if (matches === null) return false;

            return matches[0];
        };

        $(document).ready(function () {
            $('#regenerar').click(function (ev) {
                ev.preventDefault();
                // valido el mail
                if (_validEmail($('#email_to').val())) {
                    alert_type = "info";
                    msg = "<%=translate("se_envio_el_mail")%>";
                }
                else {
                    alert_type = "error";
                    msg = "<%=translate("el_mail_no_es_valido")%>"
                }

                // informo
                var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + msg + '</div></div>';

                $('.well').prepend(div_alert);
            });
        });
    </script>
</body>
</html>