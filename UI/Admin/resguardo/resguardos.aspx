<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="resguardos.aspx.vb" Inherits="UI.resguardos" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_msg_resguardo")%></div>
            </header>
            <section>
              <% If Not String.IsNullOrEmpty(Me.Request.QueryString("err")) Then %>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=translate(Me.Request.QueryString.Get("err"))%></div>
                     </div>
                  <%End If%>
                <div>
                    <%If bkpAccess Then%>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" id="make_bkp" data-action="add">
                            <i class="icon-lock icon-white"></i> <% =translate("btn_bkp")%>
                        </a>
                    </div>
                    <%End If%>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_archivo")%></th>
                                <th><%=translate("th_fecha")%></th>
                                <% If restoreAccess Then%>
                                <th><%=translate("th_restore")%></th>
                                <%End If%>                                
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each b As BE.BEBackup In lista_bkps%>
                                <tr id="idlang-<% =b.Id%>">
                                    <td><% =b.Filename%></td>
                                    <td><% =b.Fecha%></td>
                                    <%If restoreAccess Then%>
                                    <td>                                        
                                        <a href="#" class="btn btn-danger restore" data-idbkp="<% =b.Id%>"  data-file="<%=b.Filename%>"><i class="icon-file icon-white"></i> <% =translate("btn_restore")%></a>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        // make active Admin tab
        $('.active').removeClass('active');
        $('.menu_admin').addClass('active');

        $('#make_bkp').click(function (ev) {
            ev.preventDefault();
            $.confirm({
                text: "<%=translate("confirme_accion")%>",
                confirmButton: "<%=translate("Si")%>",
                cancelButton: "<%=translate("Cancelar")%>",
                confirm: function () {
                    // insert backdrop
                    $('<div class="modal-backdrop"></div>').appendTo(document.body);
                    //make!
                    var ie_fix = new Date();
                    $.get('/Admin/resguardo/make_bkp.ashx',{"ie_fix":ie_fix}, function (res) {
                        // remove backdrop
                        $(".modal-backdrop").remove();
                        if (res.status == undefined) location.reload();

                        var alert_type = (res.status == 200) ? "info" : "error";
                        var div_alert = '<div class="alert alert-' + alert_type + '">'
                            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                            + '<div class="alert-msg">' + res.msg + '</div></div>';

                        //remove if there any
                        $('.alert').remove();
                        $('section').prepend(div_alert);

                        // continue
                        if (res.status == "200") {
                            // it's new so reload the page 1 second after
                            setTimeout(function () { location.reload() }, 1000);;
                        }
                    });
                }
            });
        });

        // restore
        $('.restore').click(function (ev) {
            ev.preventDefault();
            var bkp_name = $(this).data('file');
            $.confirm({
                text: "<%=translate("confirme_accion")%>",
                confirmButton: "<%=translate("Si")%>",
                cancelButton: "<%=translate("Cancelar")%>",
                confirm: function () {
                    // insert backdrop
                    $('<div class="modal-backdrop"></div>').appendTo(document.body);
                    //make!

                    $.post('/Admin/resguardo/restore.ashx', { bkp_name: bkp_name }, function (res) {
                        // remove backdrop
                        $(".modal-backdrop").remove();
                        if (res.status == undefined) {
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

                            if (res.status == "200") {
                                // it's new so reload the page 1 second after
                                setTimeout(function () { location.reload() }, 1000);;
                            }
                        }
                    });
                }
            });
        });
    </script>
    
</asp:Content>
