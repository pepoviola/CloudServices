﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="detalles_vm.aspx.vb" Inherits="UI.detalles_vm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
          .sidebar-nav {
            padding: 9px 0px;
        }
        .data_sep {
            padding: 0px 5px;
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
                   <%-- <div class="span9">
                        <div class="console" id="pluginPanel"></div>
                                                
                    </div>--%>
                     <div class="span3">
                            <div class="well sidebar-nav">
                                <ul class="nav nav-list">
                                    <li class="nav-header">Menu</li>
                                    <li class="active"><a href="#"><% =translate("menu_home")%></a></li>
                                    <li><a href="/cloud/sg/sg.aspx"><% =translate("menu_sg")%></a></li>                                    
                                </ul>
                            </div>
                        </div>
                    <div class="span9">
                        <div class="well">
                            <h4 class=""><%=translate("li_info")%></h4>
                            <div class="row-fluid">
                            <div class="span4">
                                <ul class="unstyled">
                                    
                                    <%If Not VM Is Nothing Then%>
                                    <li><strong><%=translate("li_nombre")%></strong>:<span class="data_sep"> <%=VM.vmNombre%></span></li>
                                    <li><strong><%=translate("li_memoria")%></strong>:<span class="data_sep"><%=VM.Memoria%> GB</span></li>
                                    <li><strong><%=translate("li_qcpu")%></strong>:<span class="data_sep"><%=VM.Qcpu%></span></li>
                                    
                             </ul>
                            </div>

                            <div class="span4">
                                <ul class="unstyled">
                                    <li><strong><%=translate("li_adicionales")%></strong>
                                        <ul>
                                            <% For Each addon As BE.BEServicioAdicional In VM.Srv_adicionales%>
                                                <li><%=addon.Nombre%></li>
                                            <%Next%>                                                                                       
                                        </ul>
                                    </li>
                                    </ul>
                            </div>
                            <div class="span3">
                                <ul class="unstyled">
                                    <li><strong><%=translate("menu_sg")%></strong>
                                        <ul>
                                            <% For Each addon As BE.BEServicioAdicional In VM.Srv_adicionales%>
                                                <li><%=addon.Nombre%></li>
                                            <%Next%>                                                                                       
                                        </ul>
                                    </li>
                                    </ul>

                            </div>
                            <%--        <li class="nav-header"><%=translate("li_acciones")%></li>
                                    </ul>
                                    <a class="btn btn-primary" onclick="connect_vm()">connect</a>
                                    <span class="help-inline">Ctrl+Alt to release</span>--%>
                            </div>
                            <hr />
                            <div class="row-fluid">
                                    <a class="btn btn-primary segs" data-action="reboot" data-vmid="<%=VM.Id%>" >Config SG</a>                                
                                    <a class="btn btn-warning actions" data-action="reboot" data-vmid="<%=VM.Id%>" >Reboot</a>
                                    <a class="btn btn-danger actions" data-action="reset" data-vmid="<%=VM.Id%>">Reset</a>
                                    <% End If%>                  
                            </div>        
                            
                         <!--</div>-->
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
    <script>
    $(document).ready(function(){
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
    </script>
</asp:Content>
