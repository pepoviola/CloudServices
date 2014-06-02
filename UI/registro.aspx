<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="registro.aspx.vb" Inherits="UI.registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title>CloudServices</title>
    <!-- Bootstrap core CSS -->
    <link href="/content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/content/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="content/css/login/login.css" rel="stylesheet" />
    <style>
        .no-modal-body {
            padding-top:10px;
        }
    </style>
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
				<a class="brand" href="#">CloudServices</a>	

		    </div>
		</div>
	</div>
    <div class="container">
	    <div class="wrap">
            <div class="row" id="alertas">              
                <%  If Session("_msg_dv_err") <> "" Then%>
                <div  id="Div1" class="alert alert-error"><a class="close" data-dismiss="alert">×</a>
                    <% =Session("_msg_dv_err")%>
                </div>  

                <%  End If %>       
            </div>
            <div class="row-fluid">
                <div class="span10 offset1 well">
                            <!-- modal create -->
    <div id="modal_new_user" class="">
        <form class="form-horizontal" id="form_new_user">
        <div class="modal-header">
            <!--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>-->
            <h3><%=translate("lbl_registrarse_cliente")%></h3>
        </div>
        <div class="no-modal-body">
          
            <div class="control-group">
                <label class="control-label" for="username"><% =translate("username")%></label>
                <div class="controls">
                        <input type="text" name="username" id="username" maxlength="25" pattern=".{4,25}" required title="4 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

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
                <label class="control-label" for="email"><% =translate("email")%></label>
                <div class="controls">
                        <input type="text" name="email" id="email" placeholder="" maxlength="150" />
                </div>
            </div>
            <div class="control-group">
      
                        <label class="control-label" for="dir"><% =translate("direccion")%></label>
                        <div class="controls form-inline">
                            <label class="" for="dir"><%=translate("calle")%>
                                <input type="text" class="input-small" name="calle" id="calle" placeholder="" maxlength="150" />
                            </label>
                            <label class="" for="dir"><%=translate("numero")%>
                                <input type="number" class="input-mini" name="numero" id="numero"  maxlength="6" />
                            </label>
                                                    
<!--
                        <label class="" for="dir"><%=translate("piso")%>
                            <input type="text" class="input-mini" name="piso" id="piso" placeholder="" maxlength="4" />
                        </label>
                        <label class="" for="dir"><%=translate("dto")%>
                            <input type="text" class="input-mini" name="dto" id="dto" placeholder="" maxlength="4" />
                        </label>
                     
     -->
</div>  
                    </div>
    
           <div class="control-group">
                <label class="control-label" for="localidad"><% =translate("localidad")%></label>
                <div class="controls">
                        <input type="text" name="localidad" id="localidad" placeholder="" maxlength="150" />
                </div>
            </div>
            
            <div class="control-group">
                <label class="control-label" for="idioma"><% =translate("idioma")%></label>
                <div class="controls">
                    <select name="idioma">
                      <% For Each i As BE.Idioma In lista_idioma %>
                        <option value="<% =i.Id%>"><% =i.Codigo%></option>              
                      <% Next%>
                    </select>                        
                </div>
            </div>
            <!-- end commons -->
            <!-- Es persona o empresa -->
         <!--   <div class="control-group">
                <label class="control-label" for="tipo_cli"><% =translate("Seleccione")%></label>
                <div class="controls">
                    <select name="tipo_cli" id="tipo_cli">
                        <option value="p"><%=translate("persona")%></option>
                        <option value="e"><%=translate("empresa")%></option>                      
                    </select>                        
                </div>
            </div>
            -->
            <!-- fin es persona o empresa -->
            <div class="control-group" id="ctrl-nombre">
                <label class="control-label" for="nombre"><% =translate("nombre")%></label>
                <div class="controls">
                        <input type="text" name="nombre" id="nombre" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group" id="ctrl-apellido">
                <label class="control-label" for="apellido"><% =translate("apellido")%></label>
                <div class="controls">
                        <input type="text" name="apellido" id="apellido" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
           
          <!--
            <div class="control-group" id="ctrl-razon_social">
                <label class="control-label" for="razon_socila"><% =translate("razon_social")%></label>
                <div class="controls">
                        <input type="text" name="razon_social" id="razon_social" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group" id="ctrl-cuit">
                <label class="control-label" for="cuit" ><% =translate("cuit")%></label>
                <div class="controls">
                        <input type="text" name="cuit" id="cuit" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
            -->
        </div>
            </form>
        <div class="modal-footer">
           <!-- <a href="#" class="btn" data-dismiss="modal" aria-hidden="true"><%=translate("btn_close")%></a>-->
            <button type="submit" class="btn btn-primary" data-action="create" id="user_create"><%=translate("btn_save")%></button>
        </div>
    <!-- end form -->            
    
    </div>
    <!-- .end  create -->

                </div>
            </div>
        </div>
    </div>

    <script src="scripts/jquery-2.1.0.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="/scripts/jquery.validate.js"></script>
    <script>
        var persona_fields = ["nombre", "apellido"];
        var empresa_fields = ["razon_social", "cuit"];

        var deshabilitar = function (t) {
            
            if (t == "p") {
                $.each(empresa_fields, function (k, field) {
                    $('#ctrl-' + field).hide();
                });
                $.each(persona_fields, function (k, field) {
                    $('#ctrl-' + field).show();
                });
            }
            else {
                $.each(persona_fields, function (k, field) {
                    $('#ctrl-' + field).hide();
                });
                $.each(empresa_fields, function (k, field) {
                    $('#ctrl-' + field).show();
                });
            }
            
        }

        // mensajes
        var messages = {
            username: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            password: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            repeat_password: {
                required: "<%=translate("campo_requerido")%>",
                equalTo: "<%=translate("las_claves_deben_coincidir")%>"
            },
            nombre: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            apellido: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            email: {
                required: "<%=translate("campo_requerido")%>",
                email: "<%=translate("debe_ser_email_valido")%>"
            },
            calle: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            numero: {
                required: "<%=translate("campo_requerido")%>",
                number: "<%=translate("solo_numeros")%>"
            },


        };

        // VALIDACIONES
        var validate_new = function () {
            $("#form_new_user").validate({
                debug: true,
                rules: {
                    username: {
                        required: true,
                        minlength: 4,
                        maxlength: 25
                    },
                    password: {
                        required: true,
                        minlength: 6,
                        maxlength: 50,
                    },
                    repeat_password: {
                        required: true,
                        equalTo: "#password",
                    },
                    nombre: {
                        required: true,
                        minlength: 2,
                        maxlength: 50,
                    },
                    apellido: {
                        required: true,
                        minlength: 2,
                        maxlength: 50,
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    calle: {
                        required: true,
                        minlength: 2,
                        maxlength: 50,
                    },
                    numero: {
                        required: true,
                        number: true
                    }
                },
                messages: messages
            });
        };

        $(document).ready(function () {

            // logic for fields
            // always use DRY way!

            // first show as person only
            //deshabilitar($('#tipo_cli').val());

            // on change
            //$('#tipo_cli').change(function (ev) {
            //    deshabilitar($(this).val());
            //});

            // CREATE
            $('#user_create').click(function (ev) {
                ev.preventDefault();
                // instancio validacion
                validate_new();
                if ($("#form_new_user").valid()) {
                    $.post('/Admin/clientes/add_cliente.ashx', $('#form_new_user').serialize(), function (res) {
                        // if the session expired reload the page to go to login form
                        if (res.status == undefined) location.reload();

                        var alert_type = (res.status == 200) ? "info" : "error";
                        var div_alert = '<div class="alert alert-' + alert_type + '">'
                            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                            + '<div class="alert-msg">' + res.msg + '</div></div>';

                        //remove if there any
                        $('.alert').remove();
                        //remove modal
                        //$("#modal_new_user").modal("hide");
                        $('#alertas').prepend(div_alert);

                        // continue
                        if (res.status == "200") {
                            // it's new so reload the page in 1 sec
                            setTimeout(function () { location.href = "/login.aspx"; }, 2000);
                        }
                    });
                } // del if valid
            }); // end 

        });

    </script>
</body>
</html>
