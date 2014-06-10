<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cambiar_clave.aspx.vb" Inherits="UI.cambiar_clave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>CloudServices</title>
    <!-- Bootstrap core CSS -->
    <link href="/content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/content/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="/content/css/login/login.css" rel="stylesheet"/>
    <link href="/content/css/customs.css" rel="stylesheet" />
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

            <% If Valido Then%>
            <div class="well">
                <form id="recuperar" class="form-horizontal">
                    <legend><%=translate("recuperar_passwd")%></legend>
                      <div class="control-group">
                        <label class="control-label" for="username"><% =translate("password")%></label>
                        <div class="controls">
                            <input type="password" name="password" id="password" maxlength="50" pattern=".{6,50}" required title="6 <%=translate("x_caracteres_requeridos") %>"/>
                        </div>
                    </div>

                    <div class="control-group">
                        <label class="control-label" for="username"><% =translate("repeat_password")%></label>
                        <div class="controls">
                                <input type="password" name="repeat_password" id="repeat_password" maxlength="50" pattern=".{6,50}" required title="6 <%=translate("x_caracteres_requeridos") %>"/>
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
 
            <% Else%>
            <div class="alert alert-error">
                <h4><%=translate("codigo_no_valido")%></h4>
            </div>
            <%End If%>

         </div>
     </div>
      
        <%If Valido Then %>
        <script src="scripts/jquery-2.1.0.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/login/login.js"></script>
    <script src="/scripts/jquery.validate.js"></script>
                   <script>
                       var messages = {
                           password: {
                               required: "<%=translate("campo_requerido")%>",
                        minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                        maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
                    },
                    repeat_password: {
                        required: "<%=translate("campo_requerido")%>",
                        equalTo: "<%=translate("las_claves_deben_coincidir")%>"
                    }
                };
                var validar = function () {
                    $("#recuperar").validate({
                        debug: true,
                        rules: {
                            password: {
                                required: true,
                                minlength: 6,
                                maxlength: 50,
                            },
                            repeat_password: {
                                required: true,
                                equalTo: "#password",
                            }
                        },
                        messages: messages
                    });
                };

                $(document).ready(function () {
                    $('#regenerar').click(function (ev) {
                        ev.preventDefault();
                        validar()
                        if ($("#recuperar").valid()) {
                            $.post('/cambiar_clave.ashx', { ucode: "<%=Ucode%>", password: $('#password').val() }, function (res) {
                                var alert_type = (res.status == 200) ? "info" : "error";
                                // informo
                                var div_alert = '<div class="alert alert-' + alert_type + '">'
                                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                                $('.alert').remove();
                                $('.well').prepend(div_alert);

                                if (res.status == 200) {
                                    setTimeout(function () {
                                        location.href = "/"
                                    }, 2000);
                                }
                            });
                        }
                    });
                });




            </script>      
          
        <%End If%>
</body>
</html>
