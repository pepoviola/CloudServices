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
                                <tr id="idlang-<% =s.Id%>">
                                    <td><% =s.Hostname%></td>
                                    <td><% =s.Memoria%></td>
                                    <td><% =s.Q_cpu%></td>
                                    <%If writeAccess Then%>
                                    <td>                                        
                                         <a href="#" class="btn btn-primary server_edit" data-ids="<% =s.Id%>" ><i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
                                        <a href="#" class="btn btn-danger server_delete" data-ids="<% =s.Id%>" ><i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>                                        
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
                        <input type="text" name="hostname" id="hostname" placeholder="" />
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
                        <input type="text" name="qcpu" id="Text1" placeholder="" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script src="/scripts/cloud/customs_adm.js"></script>
</asp:Content>
