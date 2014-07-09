<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="home_partial.aspx.vb" Inherits="UI.home_partial" %>

    <% For Each s As BE.BECloudServer In Servicios_contratados
                                Dim total_linea As Double = s.Precio
                                
                         %>

                            <tr>
                               <td class="servicioNombre">
                                    <div >
                                    <span class=""><%=s.Nombre%> &nbsp;</span>
                                    <% If s.Estado = 1 Then %>
                                    <span class="pull-right separar">
                                       <span class="spin"></span>
                                    </span>
                                    <% Else %> 
                                    <span class="pull-right">
                                     <button class="btn btn-mini btn-danger baja-srv" title="baja" data-sid="<%=s.Id %>" data-qaddons="<%=s.Srv_adicionales.Count%>" data-type="<%=s.Codigo%>">
                                         <i class="icon-trash icon-white"></i>
                                     </button>
                                     </span>
                                    <% End If%> 
                                    </div>
                                </td>
                                <td> 
                                    <% If s.Estado = 1 Then %>                           
                                        <a href="#" class="tip" data-toggle="tooltip" title="creando"><%=s.vmNombre%></a>                                                                        
                                    <%Else%>
                                        <a href="vm_details.aspx?id=<%=s.Id%>"><%=s.vmNombre%></a>
                                    <%End If%>                                   
                                        
                                </td>
                                <td class="adicionales-width">
                                    <%For Each a As BE.BEServicioAdicional In s.Srv_adicionales%>
                                    <% total_linea += a.Precio%>
                                    <div class="addon-srv">
                                      <span class="">
                                        <%=a.Nombre%>
                                      </span>
                                     
                                            <span class="pull-right separar">
                                            <% If s.Estado = 1 Then%>
                                                <span class="spin"></span>
                                            <% Else%>
                                            <button class="btn btn-mini btn-danger baja-srv ladda-button" title="baja" data-sid ="<%=a.Id %>" data-type="<%=a.Codigo%>" data-style="expand-right" data-size="l">
                                            <span class="ladda-label"><i class="icon-trash icon-white"></i></span>
                                            </button>                                       
                                        </span>
                                    <%End If%>
                                       
                                        </div>

   
                                    <% Next%>
                                </td>
                                <!--<td><span class="get_ip pull-right" id="ip-<%=s.Id%>"></span></td>-->
                                <td><span>$ &nbsp;&nbsp;</span><span class="pull-right"><%= total_linea %></span></td>
                            </tr>    
                        <%Next%>
