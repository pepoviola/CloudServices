<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="vm_details.aspx.vb" Inherits="UI.vm_details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .console {
            height:450px;
            border: 1px solid #ccc;

        }
        .confirmation-modal {
            /*position:relative;*/
            width: 300px;
            left: 95%;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>

            </header>
            <section>
                <div class="row-fluid">
                    <div class="span9">
                        <div class="console" id="pluginPanel"></div>
                                                
                    </div>
                    <div class="span3">
                        <div class="well sidebar-nav">
                                <ul class="unstyled">
                                    <li class="nav-header"><%=translate("li_info")%></li>
                                    <%If Not VM Is Nothing Then%>
                                    <li><%=translate("li_nombre")%>: <%=VM.vmNombre%></li>
                                    <li><%=translate("li_memoria")%>:<%=VM.Memoria%> GB</li>
                                    <li><%=translate("li_qcpu")%>:<%=VM.Qcpu%></li>
                                    <li><%=translate("li_adicionales")%>
                                        <ul>
                                            <% For Each addon As BE.BEServicioAdicional In VM.Srv_adicionales%>
                                                <li><%=addon.Nombre%></li>
                                            <%Next%>                                                                                       
                                        </ul>
                                    </li>
                                    
                                    <li class="nav-header"><%=translate("li_acciones")%></li>
                                    </ul>
                                    <a class="btn btn-primary" onclick="connect_vm()">connect</a>
                                    <span class="help-inline">Ctrl+Alt to release</span>
                                    <hr />
                                    <a class="btn btn-warning actions" data-action="reboot" data-vmid="<%=VM.Id%>" >Reboot</a>
                                    <a class="btn btn-danger actions" data-action="reset" data-vmid="<%=VM.Id%>">Reset</a>
                                    <% End If%>                  
                         </div>
                        <div style="overflow: auto;height:50px " id="msgBox"></div>
                    </div>
                </div><!-- row -->
                <br />
                <br />
                <div class="row-fluid">
                    <div class="span12">
                        <table class="table table-bordered table-hover">
                            <thead>
                            <tr>
                                <th><%=translate("th_evento")%></th>
                                <th><%=translate("th_descripcion")%></th>
                                <th><%=translate("th_fecha")%></th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each e As BE.BEEvento In Eventos%>
                            <tr>
                                <td><%=e.Evento%></td>
                                <td><%=e.Descripcion%></td>
                                <td><%=e.Fecha%></td>
                            </tr>
                            <%Next%>
                          </tbody>
                        </table>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script src="/scripts/cloud/vmrc-embed.js"></script>
    <script src="/scripts/cloud/installer.js"></script>
    <script>
        $(document).ready(function () {
            init();

            $('.actions').click(function (ev) {
                ev.preventDefault();
                var _this = this;
                var vmid = $(this).data('vmid');
                var action = $(this).data('action');
                $.confirm({
                    text: "<%=translate("desea_realizar")%> "+action,
                    confirmButton: "<%=translate("Si")%>",
                    cancelButton: "<%=translate("Cancelar")%>",
                    confirm: function () {
                        $.post('/cloud/eventos/actionVM.ashx', { id: vmid, action: action }, function (res) {
                            // if the session expired reload the page to go to login form
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
                                // scroll to top to show
                                $("html, body").animate({ scrollTop: 0 }, "slow");
                            }

                        });
                    }
                });


            });
        });

        function connect_vm() {

            try {
                var ret = vmrc.startup(2, 1, "");
                log('startup returned "' + ret + '"');
            } catch (err) {
                log('startup call failed: ' + err);
            }

            try {
                //console.log('pase');
                //startup(2,1,null);
                var ret = vmrc.connect("190.210.166.138", "", true,
                                 "<%=tt%>", "", "", "<%=mor%>", "", "");
                log('connect succeeded');
            } catch (err) {
                log('connect failed: ' + err);
            }
        }

        function shutdown_vm() {
            shutdown();
            disconnect();
        }
    </script>
</asp:Content>
