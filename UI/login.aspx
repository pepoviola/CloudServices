﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="UI.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CloudServices</title>
    <!-- Bootstrap core CSS -->
    <link href="/content/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="/content/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link href="/content/css/login/login.css" rel="stylesheet"/>
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
        <% If Session("_login_err") > 0 Then %>
        <div  id="aler_div_error" class="alert alert-error"><a class="close" data-dismiss="alert">×</a>
            <% =translate("login_error_acceso")%>
        </div>  
        <%  ElseIf Session("_msg_dv_err") <> "" Then%>
        <div  id="Div1" class="alert alert-error"><a class="close" data-dismiss="alert">×</a>
            <% =Session("_msg_dv_err")%>
        </div>  

        <%  End If %>
   
        <div  id="alert_div_complete" class="alert alert-error hide"><a class="close" data-dismiss="alert">×</a>
            <% =translate("login_error_complete")%>            
        </div>
        
       
    </div>
    <div class="row">
             <div class="control-group pull-right">
                <!--<label class="control-label" for="idioma"><% =translate("idioma")%></label>-->
                <div class="controls">
                    <select name="idioma" id="cambia_idioma">
                <% For Each i As BE.Idioma In lista_idioma %>
                        <%If i.Id = Session("lang") Then%>
                            <option value="<%=i.Id%>" selected="selected"><%=i.Codigo%></option>              
                        <%Else %>
                            <option value="<%=i.Id%>"><%=i.Codigo%></option>
                      <% 
                      End If
                  Next
                  %>
                    </select>                        
                </div>
            </div>
    </div>
        <%If sistemaEnError Then %>
    <div class="row">
        <div class="span6 offset3 well">
            <div class="modal-header">
                <h3><%=translate("sistema_mantenimiento")%></h3>
            </div>
            <br />
            <p><%=translate("disculpe_molestias")%></p>
            
        </div>
    </div>                
          <%Else%>
    <!-- sistema ok -->
    <div class="row">
        <div class="span6 offset3" id="form-login">
            <form class="form-horizontal well" runat="server">
                <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="isEmpty" ControlToValidate="txt_login_username"></asp:CustomValidator>--%>
                <fieldset>
                    <legend> <%  =translate("login_form_header")%> <%--<asp:label runat="server" ID="login_form_header"></asp:label>--%>   </legend>
                    <div class="control-group">
                        <div class="control-label">
                            <label><%  =translate("login_form_username")%><%--<asp:label runat="server" id="login_form_username"></asp:Label>--%></label>
                        </div>
                        <div class="controls">
                            <%--<input type="text" name="username" id="username"  class="input-large" />--%>
                            <%--<asp:TextBox runat="server" ID="txt_login_username" CssClass="input-large"></asp:TextBox>--%>
                            <input type="text" name="txt_login_username" id="txt_login_username" class="input-large" />
                        </div>
                    </div>

                    <div class="control-group">
                        <div class="control-label">
                            <label>
                                <%--<asp:Label runat="server" ID="login_form_password"></asp:Label>--%>
                                <% =translate("login_form_password")%>
                            </label>
                        </div>
                        <div class="controls">
                            <%--<input type="password" name="passwd" id="passwd"  class="input-large" />--%>
                            <%--<asp:TextBox runat="server" ID="txt_login_passwd" TextMode="Password" CssClass="input-large"></asp:TextBox>--%>
                            <input type="password" name="txt_login_passwd" id="txt_login_passwd" class="input-large" />
                        </div>
                    </div>

                    <div class="control-group">
                        <div class="controls" style="margin-left:25%">

                            <%--<asp:Button runat="server" id="login_submit" CssClass="btn btn-primary button-loading"/>--%>
                            <button type="submit" id="login_submit" name="login_submit" class="btn btn-primary" ><% =translate("login_submit")%></button>
                            <a href="/recuperar.aspx"  id="lost_passwd"  class="btn btn-warning" ><% =translate("lost_passwd")%></a>
                           <%--<button type="submit" id="submit" class="btn btn-primary button-loading" data-loading-text="Loading...">Sign in</button>--%>

                        <%--<button type="button" id="olvido" class="btn btn-secondary button-loading" data-loading-text="Loading...">Olvidó la clave...</button>--%>

                        </div>
                    </div>
                    
                </fieldset>
            </form>
            <div class="registro">
                <p><%=translate("no_tenes_cuenta_aun")%> <strong><a href="registro.aspx"><%=translate("registrate_ahora")%></a> </strong></p>
            </div>
        </div>
    </div>
      <%  End If%>
        <%=translate("idioma_detectado")%>: <%=HttpContext.Current.Request.UserLanguages(0) %>
</div>
        
    </div>
    <script src="scripts/jquery-2.1.0.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/login/login.js"></script>
    <script>
        var idioma_actual;
        $(document).ready(function () {
            idioma_actual = $('#cambia_idioma').val();
            $('#cambia_idioma').change(function (ev) {
                //alert($(this).val());
                var nuevo_idioma = $(this).val();
                if (idioma_actual != nuevo_idioma) {
                    location.href = "http://localhost:49399/login.aspx?lang=" + nuevo_idioma;
                }
            });
        });
    </script>
</body>
</html>
