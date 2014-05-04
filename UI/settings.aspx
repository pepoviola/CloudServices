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
                <label class="control-label" for="username"><% =translate("password")%></label>
                <div class="controls">
                        <input type="password" name="password" id="Text2" maxlength="50" pattern=".{6,50}" required title="6 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="username"><% =translate("repeat_password")%></label>
                <div class="controls">
                        <input type="password" name="repeat_password" id="Text3" maxlength="50" pattern=".{6,50}" required title="6 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="nombre"><% =translate("nombre")%></label>
                <div class="controls">
                        <input type="text" name="nombre" id="Text4" value="<%=user_session.Nombre%>" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="apellido"><% =translate("apellido")%></label>
                <div class="controls">
                        <input type="text" name="apellido" id="Text5" value="<% =user_session.Apellido%>" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
          
            <div class="control-group">
                <label class="control-label" for="email"><% =translate("email")%></label>
                <div class="controls">
                        <input type="text" name="email" value="<%=user_session.Email %>" id="Text6" placeholder="" maxlength="150" />
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="idioma"><% =translate("idioma")%></label>
                <div class="controls">
                    <select name="idioma">
                <% For Each i As BE.Idioma In lista_idioma %>
                        <%If i.Id = Session("lang") Then%>
                            <option value="<% =i.Id%>" selected="selected"><% =i.Codigo%></option>              
                        <%Else %>
                            <option value="<% =i.Id%>"><% =i.Codigo%></option>
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
        // SAVE EDITED USER
        $('#save_edited_user').click(function (ev) {
            ev.preventDefault();
            // validar el form
            $('#form_edit_user').submit();

            //console.log($('#form_edit_user').serialize());
            //$.post('/Admin/usuario/mod_usuario.ashx', $('#form_edit_user').serialize(), function (res) {
            //    // if the session expired reload the page to go to login form
            //    if (res.status == undefined) location.reload();

            //    var alert_type = (res.status == 200) ? "info" : "error";
            //    var div_alert = '<div class="alert alert-' + alert_type + '">'
            //            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
            //            + '<div class="alert-msg">' + res.msg + '</div></div>';

            //    //remove if there any
            //    $('.alert').remove();
            //    //remove modal
            //    $("#modal_edit_user").modal("hide");
            //    $('section').prepend(div_alert);


            //    // continue
            //    if (res.status == "200") {
            //        // it's new so reload the page in 1 sec
            //        setTimeout(function () { location.reload(); }, 1000);
            //    }
            //});
        });
    </script>
</asp:Content>
