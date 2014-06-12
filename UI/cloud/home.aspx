﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="home.aspx.vb" Inherits="UI.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <%-- <link href="/content/css/whhg-font/css/whhg.css" rel="stylesheet" />--%>
    <style>
        .addon-srv {
            position: relative;
            width: 45%;            
            float: left;
        }
        .separar {
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
 
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_cliente_servicios")%></div>
            </header>
            <section>
                <% If msg_err <> "" Then%>
                <div  id="aler_div_error" class="alert alert-error"><a class="close" data-dismiss="alert">×</a>
                    <% =msg_err%>
                </div>  
                <%End If%>
                <div class="row-fluid">                    
                </div>
                <div>                    
                    <div class="pull-right">
                       <div class="btn-group">
                            <a class="btn dropdown-toggle btn-success" data-toggle="dropdown" href="#">
                                <i class="whhg-icon-shoppingcartalt"></i> <% =translate("btn_new")%>
                                <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li>
                                    <a href="/cloud/add_services.aspx" >
                                        <i class="whhg-icon-addtocart"></i> <% =translate("seleccion_manual")%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/cloud/generar_entorno.aspx">
                                        <i class="whhg-icon-wizardalt"></i> <% =translate("generar_entorno")%>
                                    </a>
                                </li>                                
                            </ul>
                        </div>
                        <!--
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modal_new" data-action="add">
                            <i class="icon-shoppingcartalt icon-white"></i> <% =translate("btn_new")%>
                        </a>
                        -->
                    </div>
                    <br /><br />
                    <%=Servicios_contratados.Count %>
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_servicio")%></th>
                                <th><%=translate("th_adicionales")%></th>
                                <th><%=translate("th_costo_mensual") %></th>
                            </tr>
                        </thead>
                        <tbody>
                        <% For Each s As BE.BECloudServer In Servicios_contratados
                                Dim total_linea As Double = s.Precio
                                
                         %>

                            <tr>
                                <td><%=s.Nombre%>
                                     <span class="pull-right">
                                     <button class="btn btn-mini btn-danger baja-srv" title="baja" data-sid ="<%=s.Id %>">
                                         <i class="icon-trash icon-white"></i>
                                     </button>
                                     </span>
                                </td>
                                <td>
                                    <%For Each a As BE.BEServicioAdicional In s.Srv_adicionales%>
                                    <% total_linea += a.Precio%>
                                    <div class="addon-srv">
                                      <span class="">
                                        <%=a.Nombre%>
                                      </span>
                                      <span class="pull-right separar">
                                            <button class="btn btn-mini btn-danger baja-srv" title="baja" data-sid ="<%=a.Id %>">
                                            <i class="icon-trash icon-white"></i>
                                            </button>                                       
                                    </span>
                                        </div>

   
                                    <% Next%>
                                </td>
                                <td><span>$ &nbsp;&nbsp;</span><span class="pull-right"><%= total_linea %></span></td>
                            </tr>    
                        <%Next%>
                        </tbody>
                     </table>
                </div>
                    
                    <br />
                    <br />
            </section>
         </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        $(document).ready(function() {
            
            $('.baja-srv').click(function (ev) {
                ev.preventDefault();
                var sid = $(this).data('sid')
                $.confirm({
                    text: "<%=translate("confirme_accion")%>",
                    confirmButton: "<%=translate("Si")%>",
                    cancelButton: "<%=translate("Cancelar")%>",
                    confirm: function () {
                        alert(sid);
                    }

                });
            });
        });
        
    </script>
</asp:Content>
