<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="home.aspx.vb" Inherits="UI.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <%-- <link href="/content/css/whhg-font/css/whhg.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
 
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_cliente_servicios")%></div>
            </header>
            <section>
                <div class="row-fluid">
                    
                </div>
                <div>                    
                    <div class="pull-right">
                       <div class="btn-group">
                            <a class="btn dropdown-toggle btn-success" data-toggle="dropdown" href="#">
                                <i class="icon-shoppingcartalt icon-white"></i> <% =translate("btn_new")%>
                                <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li>
                                    <a href="/cloud/add_services.aspx" >
                                        <i class="icon-addtocart icon-white"></i> <% =translate("seleccion_manual")%>
                                    </a>
                                </li>
                                <li>
                                    <a href="/cloud/generar_entorno.aspx">
                                        <i class="icon-wizardalt icon-white"></i> <% =translate("generar_entorno")%>
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
                        <% For Each s As BE.BECloudServer In Servicios_contratados %>
                            <tr>
                                <td><%=s.Nombre%></td>
                                <td>
                                    <%For Each a As BE.BEServicioAdicional In s.Srv_adicionales%>
                                    <span class="addon-srv"> <%=a.Nombre%></span>
                                    <% Next%>
                                </td>
                                <td></td>
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
</asp:Content>
