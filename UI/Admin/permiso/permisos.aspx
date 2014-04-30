<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="permisos.aspx.vb" Inherits="UI.permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <style type="text/css">
    
        .patentes_nodes { width:500px }
        
        .ul_root { list-style:none; }
    
        .li_item { padding-top:10px }
        
        .ul_root, .li_item, .div_item { float:left ; margin-left: 5px;}
    
        .ul_root, .li_item { width:100% ; padding-left: 5px;   }
    
        .expand { width:15px;height:15px; }
    
        .collapse { width:15px;height:15px;display:none }
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
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modal_flia" data-action="add">
                            <i class="icon-plus icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Patente</th>
                                <th>acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each patente As BE.BEPatenteBasica In listaPatentes %>
                                <tr id="codpat-<% =patente.codigo%>">
                                    <td><% =patente.descripcion%></td>
                                    <td>
                                        <% If patente.Nativo = 1 Then %>
                                            <a href="#" class="btn btn-primary patente_edit" data-codpat="<% =patente.codigo%>"> <i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
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
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="flia_code"><% =translate("flia_code")%></label>
                <div class="controls">
                        <input type="text" name="flia_code" id="flia_code"  />
                </div>
             </div>
            <div class="patentes_nodes">
                <ul class='ul_root'>
                <%For Each p As BE.BEPatenteBasica In listaPatentes
                    Response.Write(create_nodes(p))
                    Next
                %>
                </ul>
            </div>
        </div>
        <div class="modal-footer">
            <a href="#" class="btn"><%=translate("btn_cerrar") %></a>
            <button type="submit" class="btn btn-primary" data-action="create" id="flia_create"><%=translate("btn_guardar")%></button>
        </div>
        </form>
       </div>
    <!-- end create modal -->
    <!-- end modals -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        // manage checkbox
        $(".expand").click(function () {
            $(this).toggle();
            $(this).next().toggle();
            $(this).parent().parent().children().last().toggle();
        });
        $(".collapse").click(function () {
            $(this).toggle();
            $(this).prev().toggle();
            $(this).parent().parent().children().last().toggle();
        });

        $("input[type='checkbox']").click(function () {
            if ($(this).prop("checked") == false) {
                $(this).parent().parent().find("input[type='checkbox']").each(function () {
                    $(this).prop("checked",false);
                });
            }
            else {
                $(this).parent().parent().find("input[type='checkbox']").each(function () {
                    $(this).prop("checked", true);
                });
            }
        });
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
        } );

    </script>

</asp:Content>
