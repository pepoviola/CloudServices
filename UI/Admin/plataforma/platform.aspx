<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="platform.aspx.vb" Inherits="UI.platform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_msg_plataforma")%></div>
            </header>
            <section>
              <% If Not String.IsNullOrEmpty(getErr) Then%>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=getErr%></div>
                     </div>
                  <%End If%>
                <div>
                    <%If writeAccess Then%>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modalForm" data-action="add">
                            <i class="icon-hdd icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    <%End If%>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_hostname")%></th>
                                <th><%=translate("th_memoria")%></th>
                                <th><%=translate("th_qcpu")%></th>
                                <% If writeAccess Then%>
                                <th><%=translate("th_acciones")%></th>
                                <%End If%>                                
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each s As BE.BEServerPlataforma In listaServers%>
                                <tr id="idServer-<% =s.Id%>">
                                    <td><% =s.Hostname%></td>
                                    <td><% =s.Memoria%></td>
                                    <td><% =s.Q_cpu%></td>
                                    <%If writeAccess Then%>
                                    <td>                                        
                                         <a href="#" class="btn btn-primary server_edit" data-sid="<% =s.Id%>" data-hostname="<% =s.Hostname%>" data-mem="<% =s.Memoria%>" data-qcpu="<% =s.Q_cpu%>">
                                             <i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%>
                                         </a>
                                        <a href="#" class="btn btn-danger server_delete" data-sid="<% =s.Id%>"  data-hostname="<% =s.Hostname%>" ><i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>                                        
                                    </td>
                                    <%End If%>
                                </tr>                            
                            <% Next%>                            
                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>


      <!-- modal create -->
    <div id="modalForm" class="modal hide fade">
        <form class="form-horizontal" id="form_create_server">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("h3_agregar_platform_server")%></h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="hostname"><% =translate("th_hostname")%></label>
                <div class="controls">
                        <input type="text" name="hostname" id="hostname" placeholder="" maxlength="50"/>
                </div>
             </div>
            <div class="control-group">
                <label class="control-label" for="memoria"><% =translate("th_memoria")%></label>
                <div class="controls">
                        <input type="text" name="memoria" id="memoria" placeholder="" />
                </div>
             </div>
             <div class="control-group">
                <label class="control-label" for="qcpu"><% =translate("th_qcpu")%></label>
                <div class="controls">
                        <input type="text" name="qcpu" id="qcpu" placeholder="" />
                </div>
             </div>
          
                
        </div>
            </form>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true">Close</a>
            <button type="submit" class="btn btn-primary" data-action="create" id="server_create">save</button>
        </div>
    <!-- end form -->            

    </div>
    <!-- .end modal create -->


         <!-- modal edit -->
    <div id="modalEdit" class="modal hide fade">
        <form class="form-horizontal" id="form_edit_server">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("h3_editar_platform_server")%></h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="hostname"><% =translate("th_hostname")%></label>
                <div class="controls">
                        <input type="hidden" name="sid" id="sid-edit" />
                        <input type="text" name="hostname" id="hostname-edit" placeholder="" />
                </div>
             </div>
            <div class="control-group">
                <label class="control-label" for="memoria"><% =translate("th_memoria")%></label>
                <div class="controls">
                        <input type="text" name="memoria" id="memoria-edit" placeholder="" />
                </div>
             </div>
             <div class="control-group">
                <label class="control-label" for="qcpu"><% =translate("th_qcpu")%></label>
                <div class="controls">
                        <input type="text" name="qcpu" id="qcpu-edit" placeholder="" />
                </div>
             </div>
          
                
        </div>
            </form>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true">Close</a>
            <button type="submit" class="btn btn-primary" data-action="save" id="save-server">save</button>
        </div>
    <!-- end form -->            

    </div>
    <!-- .end modal create -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script src="/scripts/cloud/customs_adm.js"></script>
    <script>
        // mensajes
        var messages = {
            hostname: {
                required: "<%=translate("campo_requerido")%>",
                minlength: $.validator.format("<%=translate("al_menos")%> {0} <%=translate("x_caracteres_requeridos")%>"),
                maxlength: $.validator.format("<%=translate("como_maximo")%> {0} <%=translate("x_caracteres")%>")
            },
            memoria: {
                required: "<%=translate("campo_requerido")%>",
                number: "<%=translate("solo_numeros")%>"
            },
            qcpu: {
                required: "<%=translate("campo_requerido")%>",
                number: "<%=translate("solo_numeros")%>"
            }
        };

        // VALIDACIONES
        var validate_new = function () {
            $("#form_create_server").validate({
                debug :true,
                rules: {
                    hostname: {
                        required :true,
                        minlength: 4,
                        maxlength: 50
                    },
                    memoria: {
                        required: true,
                        number: true
                    },
                    qcpu: {
                        required: true,
                        number: true
                    },
                },
                messages: messages
            })
        };

        var validate_edit = function () {
            $("#form_edit_server").validate({
                debug: true,
                rules: {
                    hostname: {
                        required: true,
                        minlength: 4,
                        maxlength: 150
                    },
                    memoria: {
                        required: true,
                        number: true
                    },
                    qcpu: {
                        required: true,
                        number: true
                    },
                },
                messages: messages
            })
        };


        // CREATE
        $('#server_create').click(function (ev) {
            ev.preventDefault();
            // instancio validacion
            validate_new();
            if ($("#form_create_server").valid()) {
                //console.log($('#form_new_user').serialize());
                $.post('/Admin/plataforma/add_server.ashx', $('#form_create_server').serialize(), function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();

                    var alert_type = (res.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    //remove modal
                    $("#modalForm").modal("hide");
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
        $('.server_delete').click(function (ev) {
            ev.preventDefault();
            var sid = $(this).data('sid');
            var hostname = $(this).data('hostname');
            $.confirm({
                text: "<%=translate("confirme_accion")%>",
                confirmButton: "<%=translate("Si")%>",
                cancelButton: "<%=translate("Cancelar")%>",
                confirm: function () {
                    $.post('/Admin/plataforma/del_server.ashx', { sid: sid, hostname: hostname }, function (res) {
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
                            $('#idServer-' + sid).remove();
                        }
                    });
                }
            }); // confirm
        });

        // edit

        $('.server_edit').click(function (ev) {
            ev.preventDefault();
            var sid = $(this).data('sid');
            var hostname = $(this).data('hostname');
            var mem = $(this).data('mem');
            var qcpu = $(this).data('qcpu');

            $('#sid-edit').val(sid);
            $('#hostname-edit').val(hostname);
            $('#memoria-edit').val(mem);
            $('#qcpu-edit').val(qcpu);

            $('#modalEdit').modal('show');
        });


        // SAVE
        $('#save-server').click(function (ev) {
            ev.preventDefault();
            // instancio validacion
            validate_edit();
            if ($("#form_edit_server").valid()) {
                //console.log($('#form_new_user').serialize());
                $.post('/Admin/plataforma/save_server.ashx', $('#form_edit_server').serialize(), function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();

                    var alert_type = (res.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    //remove modal
                    $("#modalEdit").modal("hide");
                    $('section').prepend(div_alert);

                    // continue
                    if (res.status == "200") {
                        // it's new so reload the page in 1 sec
                        setTimeout(function () { location.reload(); }, 1000);
                    }
                });
            } // del if valid
        }); // end 

    </script>

</asp:Content>
