<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="permisos.aspx.vb" Inherits="UI.permisos" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style type="text/css">
        .patentes_nodes { width:500px }        
        .ul_root { list-style:none; }    
        .li_item { padding-top:10px }        
        .ul_root, .li_item, .div_item { float:left ; margin-left: 5px;}    
        .ul_root, .li_item { width:100% ; padding-left: 5px;   }
        .expand_p { width:15px;height:15px; }    
        .collapse_p { width:15px;height:15px;display:none }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_permisos")%></div>
            </header>
            <section>
              <% If Not String.IsNullOrEmpty(Me.Request.QueryString("err")) Then %>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=translate(Me.Request.QueryString.Get("err"))%></div>
                     </div>
                  <%End If%>
                <div>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success"  id="open_create_modal" data-action="add">
                            <i class="icon-plus icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_flia")%></th>
                                <th><%=translate("th_actions")%></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each patente As BE.BEPatenteBasica In listaPatentes %>
                                <tr id="codpat-<% =patente.codigo%>">
                                    <td><% =patente.descripcion%></td>
                                    <td>
                                        <% If patente.Nativo = 1 Then %>
                                            <a href="#" class="btn btn-primary patente_edit" data-codpat="<% =patente.codigo%>" data-despat="<% =patente.descripcion%>"> <i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
                                            <a href="#" class="btn btn-danger patente_delete" data-codpat="<% =patente.codigo%>">  <i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>        
                                        <%End If%>
                                    </td>
                                </tr>
                            <%Next%>

                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>
    <!-- end main -->
    <!-- modals -->

    <!-- create modal -->
       <div id="modal_flia" class="modal hide fade">
        <form class="form-horizontal" id="form_create">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("agregar_Familia_label") %></h3>
        </div>
        <div class="create modal-body">
        </div>
        <div class="modal-footer">
            <a href="#" class="btn cerrar_modal" data-dismiss="modal" ><%=translate("btn_close")%></a>
            <button type="submit" class="btn btn-primary" data-action="create" id="flia_create"> <%=translate("btn_save")%></button>
        </div>
        </form>
       </div> 
    <!-- end create modal -->

    <!-- modify modal -->
      <div id="mod_modal" class="modal hide fade">
        <form class="form-horizontal" id="form_modify">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("modificar_Familia_label")%></h3>
        </div>
        <div class="modify modal-body">
        </div>
        <div class="modal-footer">
            <a href="#" class="btn cerrar_modal" data-dismiss="modal" ><%=translate("btn_close") %></a>
            <button type="submit" class="btn btn-primary" data-action="create" id="flia_save"> <%=translate("btn_save")%></button>
        </div>
        </form>
       </div> 
    <!-- end modify modal -->
    <!-- end modals -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        // manage checkbox
        function biddinchecks() {
            $(".expand_p").click(function () {
                $(this).toggle();
                $(this).next().toggle();
                $(this).parent().parent().children().last().toggle();
            });
            $(".collapse_p").click(function () {
                $(this).toggle();
                $(this).prev().toggle();
                $(this).parent().parent().children().last().toggle();
            });

            $("input[type='checkbox']").click(function () {
                if ($(this).prop("checked") == false) {
                    $(this).parent().parent().find("input[type='checkbox']").each(function () {
                        $(this).prop("checked", false);
                    });
                }
                else {
                    $(this).parent().parent().find("input[type='checkbox']").each(function () {
                        $(this).prop("checked", true);
                    });
                }
            });
        }
       
        //binding actions
        
        // DELETE
        $('.patente_delete').click(function (ev) {
            ev.preventDefault();
            var flia_code = $(this).data('codpat');
            $.post('/Admin/permiso/del_flias.ashx', { flia_code: flia_code }, function (res) {
                // if the session expired reload the page to go to login form
                if (res.status == undefined) location.reload();

                var alert_type = (res.status == 200) ? "info" : "error";
                var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                if (res.status == 200) {
                    // remove row                        
                    $('#codpat-' + flia_code).remove();
                }
                //remove if there any
                $('.alert').remove();
                $('section').prepend(div_alert);
            } );
        });


        // GENERATE CREATE MODAL
        $('#open_create_modal').click(function (ev) {
            ev.preventDefault();
            // get the body
            $.get('/Admin/permiso/flias.ashx?new=on', function (res) {
                if (res.status == undefined) { location.reload(); }
                else{
                    $('.create.modal-body').html(res.modalbody);
                    $('#modal_flia').modal("show");
                }
            }, "json");
        });


        //CREATE

        $('#flia_create').click(function (ev) {
            ev.preventDefault();
            // validate fields
            var valido = true;
            var msg = "<%=translate("flia_form_validation_msg")%>";
            //valido el nombre
            var flia_code = $('#form_create').find('#flia_code');
            if ( flia_code.val().trim() === ""){
                flia_code.css("border-color","red");
                valido = false;
            }
            //valido que haya al menos 1 checkbox
            if ( $('#form_create').find("input[type='checkbox']:checked").length == 0){
                valido = false;
            }
            if (!(valido)) {
                alert(msg);
                return;
            }
            else {
                // serializo el form y lo mando por post
                //console.log($('#form_create').serialize());
                $.post('/Admin/permiso/add_flia.ashx', $('#form_create').serialize(), function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();
                    
                    // continue
                    if (res.status == "200") {
                        // it's new so reload the page
                        location.reload();
                    }
                    else {
                        var alert_type = (res.status == 200) ? "info" : "error";
                        var div_alert = '<div class="alert alert-' + alert_type + '">'
                                + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                                + '<div class="alert-msg">' + res.msg + '</div></div>';

                        //remove if there any
                        $('.alert').remove();
                        $('section').prepend(div_alert);

                    }
                });
            }
        });

        // GENERATE MOD MODAL
        $('.patente_edit').click(function (ev) {
            ev.preventDefault();
            var flia_to = $(this).data('codpat');
            var flia_des = $(this).data('despat');
            $.get('/Admin/permiso/flias.ashx', function (res) {
                if (res.status == undefined) { location.reload(); }
                else{
                    $('.modify.modal-body').html(res.modalbody);
                    // make and input hidden of the id
                    var hidden = $("<input>").attr({ type: "hidden", name: "codpat", value: flia_to });
                    $('#form_modify').prepend(hidden);

                    // check the flia
                    var root = $('.modify.modal-body').find('#chbox-' + flia_to);
                    $('.modify.modal-body').find('#flia_code').val(flia_des).attr("readonly", true);
                    $(root).parent().parent().find("input[type='checkbox']").each(function () {
                        $(this).prop("checked", true);
                    });
                    $('#mod_modal').modal("show");
                }
            }, "json");
        });

        // SEND MOD MODAL
        $('#flia_save').click(function (ev) {
            ev.preventDefault();
            // validate fields
            var valido = true;
            var msg = "<%=translate("flia_form_validation_msg")%>";
                    //valido el nombre
            var flia_code = $('#form_modify').find('#flia_code');
 
                // serializo el form y lo mando por post
            //console.log($('#form_create').serialize());
            $.post('/Admin/permiso/flias.ashx', $('#form_modify').serialize(), function (res) {
                // if the session expired reload the page to go to login form
                if (res.status == undefined) location.reload();


                var alert_type = (res.status == 200) ? "info" : "error";
                var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                $('#mod_modal').modal("hide");
                //remove if there any
                $('.alert').remove();
                $('section').prepend(div_alert);
            });
       });

    </script>

</asp:Content>
