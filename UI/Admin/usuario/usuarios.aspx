<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="usuarios.aspx.vb" Inherits="UI.usuarios" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_msg_usuarios")%></div>
            </header>
            <section>
              <% If Not String.IsNullOrEmpty(Me.Request.QueryString("err")) Then %>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=translate(Me.Request.QueryString.Get("err"))%></div>
                     </div>
                  <%End If%>
                <% If Not String.IsNullOrEmpty(errores) Then%>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=errores%></div>
                     </div>
                  <%End If%>
                <div>
                    <%If writeAccess Then%>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modal_new_user" data-action="add">
                            <i class="icon-user icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    <%End If%>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_username")%></th>
                                <th><%=translate("th_nombre")%></th>
                                <th><%=translate("th_apellido")%></th>
                                <th><%=translate("th_email")%></th>
                                <th><%=translate("th_patente")%></th>
                                <th><%=translate("th_idioma")%></th>
                                <%If writeAccess Then%>
                                <th><%=translate("th_acciones")%></th>                                                                
                                <%End If%>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each user As BE.BEUsuario In lista_usuarios%>
                            <% If Not user.Patente.codigo = 47 Then%>
                                <tr id="uid-<% =user.Id%>">
                                    <td><% =user.Username%></td>
                                    <td><% =user.Nombre%></td>
                                    <td><% =user.Apellido%></td>
                                    <td><% =user.Email%></td>
                                    <td><%=user.Patente.descripcion %></td>
                                    <td><%=user.Idioma.Codigo %></td>
                                    <%If writeAccess Then%>
                                    <td>                                        
                                      <a href="#" class="btn btn-primary user_edit" data-uid="<% =user.Id%>" data-username="<% =user.Username%>">  <i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
                                      <a href="#" class="btn btn-danger user_delete" data-uid="<% =user.Id%>" data-username="<% =user.Username%>">  <i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>                                                
                                    </td>
                                    <%End If%>
                                </tr> 
                            <%End If%>                           
                            <% Next%>                            
                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>

        <!-- modal create -->
    <div id="modal_new_user" class="modal hide fade">
        <form class="form-horizontal" id="form_new_user">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("lbl_agregar_usuario")%></h3>
        </div>
        <div class="modal-body">
          
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
                <label class="control-label" for="nombre"><% =translate("nombre")%></label>
                <div class="controls">
                        <input type="text" name="nombre" id="nombre" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="apellido"><% =translate("apellido")%></label>
                <div class="controls">
                        <input type="text" name="apellido" id="apellido" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
          
            <div class="control-group">
                <label class="control-label" for="email"><% =translate("email")%></label>
                <div class="controls">
                        <input type="text" name="email" id="email" placeholder="" maxlength="150" />
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

            <div class="control-group">
                <label class="control-label" for="flia"><% =translate("familia")%></label>
                <div class="controls">
                    <select name="flia">
                      <% For Each f As BE.BEPatenteBasica In lista_flia%>
                        <% If f.Nativo = 1 Then %>
                            <option value="<% =f.codigo%>"><% =f.descripcion%></option>                      
                        <%End If%>                        
                      <% Next%>
                    </select>                        
                </div>
            </div>

        </div>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true"><%=translate("btn_close")%></a>
            <button type="submit" class="btn btn-primary" data-action="create" id="user_create"><%=translate("btn_save")%></button>
        </div>
    <!-- end form -->            
    </form>
    </div>
    <!-- .end modal create -->

    <!-- modal edit -->
    <div id="modal_edit_user" class="modal hide fade">
        <form class="form-horizontal" id="form_edit_user">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("lbl_editar_usuario")%></h3>
        </div>
        <div class="modal-body">
             <input type="hidden" name="uid"/>
            <div class="control-group">
                <label class="control-label" for="username"><% =translate("username")%></label>
                <div class="controls">
                        <input type="text" name="username" id="edit_username" maxlength="25"  readonly="true"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="username"><% =translate("password")%></label>
                <div class="controls">
                        <input type="password" name="password" id="edit_password" "/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="username"><% =translate("repeat_password")%></label>
                <div class="controls">
                        <input type="password" name="repeat_password" id="edit_repeat_password"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="nombre"><% =translate("nombre")%></label>
                <div class="controls">
                        <input type="text" name="nombre" id="edit_nombre" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>

            <div class="control-group">
                <label class="control-label" for="apellido"><% =translate("apellido")%></label>
                <div class="controls">
                        <input type="text" name="apellido" id="edit_apellido" placeholder="" maxlength="50" pattern=".{2,50}" required title="2 <%=translate("x_caracteres_requeridos") %>"/>
                </div>
            </div>
          
            <div class="control-group">
                <label class="control-label" for="email"><% =translate("email")%></label>
                <div class="controls">
                        <input type="text" name="email" id="edit_email" placeholder="" maxlength="150" />
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

            <div class="control-group">
                <label class="control-label" for="flia"><% =translate("familia")%></label>
                <div class="controls">
                    <select name="flia">
                      <% For Each f As BE.BEPatenteBasica In lista_flia%>
                        <% If f.Nativo = 1 Then %>
                            <option value="<% =f.codigo%>"><% =f.descripcion%></option>                      
                        <%End If%>                        
                      <% Next%>
                    </select>                        
                </div>
            </div>

        </div>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true"><%=translate("btn_close")%></a>
            <button type="submit" class="btn btn-primary" data-action="edit" id="save_edited_user"><%=translate("btn_save") %></button>
        </div>
    <!-- end form -->            
    </form>
    </div>
    <!-- .end modal edit -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        // make active Admin tab
        $('.active').removeClass('active');
        $('.menu_admin').addClass('active');

        // mensajes
        var messages =  {                   
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
                            email : "<%=translate("debe_ser_email_valido")%>"
                    }
                }

        // VALIDACIONES
        var validate_new = function () {
            $("#form_new_user").validate({
                debug :true,
                rules: {
                    username: {
                        required :true,
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
                        email:  true
                    }
                },
                messages : messages                
            });
        };

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
        // end validaciones
        // binding actions

        // CREATE
        $('#user_create').click(function (ev) {
            ev.preventDefault();
            // instancio validacion
            validate_new();
            if ($("#form_new_user").valid()) {
                //console.log($('#form_new_user').serialize());
                $.post('/Admin/usuario/add_usuario.ashx', $('#form_new_user').serialize(), function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();

                    var alert_type = (res.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    //remove modal
                    $("#modal_new_user").modal("hide");
                    $('section').prepend(div_alert);

                    // continue
                    if (res.status == "200") {
                        // it's new so reload the page in 1 sec
                        setTimeout(function () { location.reload(); }, 1000);
                    }
                });
            } // del if valid
        }); // end 

        //DELETE
        $('.user_delete').click(function (ev) {
            ev.preventDefault();
            var uid = $(this).data('uid');
            var uname = $(this).data('username');
            $.confirm({
                text: "<%=translate("confirme_accion")%>",
                confirmButton: "<%=translate("Si")%>",
                cancelButton: "<%=translate("Cancelar")%>",
                confirm: function () {
                    $.post('/Admin/usuario/del_usuario.ashx', { uid: uid, username:uname }, function (res) {
                        // if the session expired reload the page to go to login form
                        if (res.status == undefined) location.reload();

                        var alert_type = (res.status == 200) ? "info" : "error";
                        var div_alert = '<div class="alert alert-' + alert_type + '">'
                                + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                                + '<div class="alert-msg">' + res.msg + '</div></div>';

                        //remove if there any
                        $('.alert').remove();
                        $('section').prepend(div_alert);

                        if (res.status == "200") {
                            // remove the row
                            $('#uid-' + uid).remove();
                        }
                    });
                }
            }); // confirm
        });

        // EDIT
        $('.user_edit').click(function (ev) {
            ev.preventDefault();
            var uid = $(this).data('uid');
            var uname = $(this).data('username');
            // get the data
            $.post('/Admin/usuario/filtra_usuarios.ashx', { username: uname }, function (res) {
                // if the session expired reload the page to go to login form
                if (res.status == undefined) location.reload();
                if (res.status == "200") {
                    // complete the form
                    parsed_user = JSON.parse(res.lista)[0];

                    $('#form_edit_user input[name="uid"]').val(parsed_user.Id);
                    $('#form_edit_user input[name="username"]').val(parsed_user.Username);
                    $('#form_edit_user input[name="nombre"]').val(parsed_user.Nombre);
                    $('#form_edit_user input[name="apellido"]').val(parsed_user.Apellido);
                    $('#form_edit_user input[name="email"]').val(parsed_user.Email);
                    $('#form_edit_user select[name="idioma"] option[value="' + parsed_user.Idioma.Id + '"]').attr("selected", true)
                         
                    $('#form_edit_user select[name="flia"] option[value="' + parsed_user.Patente.codigo + '"]').attr("selected", true)
                    
                    // show the form
                    $('#modal_edit_user').modal("show");

                }
                else {
                    // show the error
                    var div_alert = '<div class="alert alert-error">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    $('section').prepend(div_alert);
                }
               
            }, "json");
        } );

        // SAVE EDITED USER
        $('#save_edited_user').click(function (ev) {
            ev.preventDefault();

            // vaido
            validate_edit();

            if ($("#form_edit_user").valid()) {
                $.post('/Admin/usuario/mod_usuario.ashx', $('#form_edit_user').serialize(), function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();

                    var alert_type = (res.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    //remove modal
                    $("#modal_edit_user").modal("hide");
                    $('section').prepend(div_alert);


                    // continue
                    if (res.status == "200") {
                        // it's new so reload the page in 1 sec
                        setTimeout(function () { location.reload(); }, 1000);
                    }
                });
            }// del if valid
        }); // end


    </script>
</asp:Content>
