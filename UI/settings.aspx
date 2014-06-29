<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="settings.aspx.vb" Inherits="UI.settings" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
       <% If Not String.IsNullOrEmpty(msg) Then%>
                     <div class="alert alert-<%=msg_type%>">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=msg%></div>
                     </div>
       <%End If%>
     <div id="settings_form" class="well" >
        <form class="form-horizontal" id="form_edit_user" method="post" action="settings.aspx">
        <div class="">            
            <h3><%=translate("lbl_configuraciones")%></h3>
        </div>
        <div class="modal-body">
            <div class="control-group">
                <label class="control-label" for="username"><%=translate("password")%></label>
                <div class="controls">
                        <input type="password" name="password" id="edit_password" maxlength="16" pattern=".{6,16}"  title="6 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="username"><% =translate("repeat_password")%></label>
                <div class="controls">
                        <input type="password" name="repeat_password" id="edit_repeat_password" maxlength="16" pattern=".{6,16}"  title="6 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="nombre"><% =translate("nombre")%></label>
                <div class="controls">
                        <input type="text" name="nombre" id="edit_nombre" value="<%=user_session.Nombre%>" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="apellido"><% =translate("apellido")%></label>
                <div class="controls">
                        <input type="text" name="apellido" id="edit_apellido" value="<% =user_session.Apellido%>" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
          
            <div class="control-group">
                <label class="control-label" for="email"><% =translate("email")%></label>
                <div class="controls">
                        <input type="text" name="email" value="<%=user_session.Email %>" id="edit_email" placeholder="" maxlength="100" />
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="idioma"><% =translate("idioma")%></label>
                <div class="controls">
                    <select name="idioma">
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
        <div class="form-actions">
            <button type="submit" class="btn btn-primary" data-action="edit" id="save_edited_user"><%=translate("btn_save") %></button>
        </div>
    <!-- end form -->            
    </form>
    </div>
    <!-- .end modal edit -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>

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
                    }
        }

        var validate_edit = function () {
            $("#form_edit_user").validate({
                debug: true,
                rules: {
                    repeat_password: {
                        equalTo: "#edit_password",
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
                    }
                },
                messages: messages
            });

        };

        // SAVE EDITED USER
        $('#save_edited_user').click(function (ev) {
            ev.preventDefault();
            // validar el form
            validate_edit();
            if ($("#form_edit_user").valid()) {
                //submit the form
                var f = document.getElementById("form_edit_user")
                f.submit();
            }          
        });
    </script>
</asp:Content>
