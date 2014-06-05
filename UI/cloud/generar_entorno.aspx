<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="generar_entorno.aspx.vb" Inherits="UI.generar_entorno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
       <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("indicaciones_generar")%></div>
            </header>
            <section>
                <div class="row-fluid">
                    <div class="span6 ">
                        <div class="form-horizontal well">
                            <div class="modal-header">           
                                <h3><%=translate("lbl_generar_entorno")%></h3>
                            </div>
                            <br />
                            <div class="no-modal-body">
                                <form id="wizard">
                                <div class="control-group">
                                    <label class="control-label" for="visitas"><%=translate("lbl_visitas_diarias")%></label>
                                    <div class="controls">
                                        <input type="number" name="visitas" id="visitas" required="required" title="<%=translate("campo_numerico") %>"/>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for=""><%=translate("Servicios")%></label>
                                    <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="db" id="db" value="db" /> Base de datos
                                    </label>
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="email" id="email" value="email" /> Email
                                    </label>                                    
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="ha">HA</label>
                                    <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="ha" id="ha" value="db" /> Habilitar
                                    </label>                                   
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="">Addons</label>
                                    <div class="controls">
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="bkp" id="bkp" value="bkp" /> Bkp
                                    </label>
                                    <label class="checkbox inline">
                                        <input type="checkbox" name="snap" id="snap" value="snap" /> Snap
                                    </label>
                                    </div>
                                </div>
                                    </form>
                            </div>
                            
                           <!-- end body -->
                             <div class="modal-footer">           
                                 <button type="submit" class="btn btn-primary" data-action="create" id="btn_generar"><%=translate("btn_generar")%></button>
                            </div>
                            <!-- end footer -->
                        </div>                        
                    </div>

                    <div class="span6 hide" id="ov_table">
                      <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_servicio")%></th>
                                <th><%=translate("th_adicionales")%></th>
                                <th><%=translate("th_costo_mensual") %></th>
                            </tr>
                        </thead>
                        <tbody>
                         
                        </tbody>
                        <tfoot>
                            <tr>
                                <td><%=translate("td_total")%></td>
                                <td></td>
                                <td ><span>$ &nbsp;&nbsp;</span><span class="pull-right" id="costo_total"></span></td>
                            </tr>
                        </tfoot>
                     </table>

                        </div>
                </div>
            </section>
        </div>
        </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>

        var actualizar_total = function (total) {
            $('#costo_total').html(total);
        }

        var servicios;
        $("#btn_generar").click(function (ev) {
            $.post('/cloud/generar.ashx', $('form').serialize(), function (res) {
                $('#ov_table').show(100);
                servicios = JSON.parse(res.s);
                $('tbody').children().remove();
                var costo_total = 0;
                $.each(servicios, function (k, server) {
                    // armo la tabla
                    var costo_linea = 0;
                    costo_linea += server.Precio;
                    var tr = $("<tr>");
                    tr.append($("<td>").html(server.Nombre));
                    tr.append($("<td>").html(server.Descripcion));
                    var td_add = $("<td>");
                    if (server.Srv_adicionales) {
                        $.each(server.Srv_adicionales, function (k, addon) {
                            td_add.append($("<span>").html(addon.Nombre));
                            costo_linea += parseFloat(addon.Precio);
                        });
                    }                    
                    //tr.append(td_add);
                    tr.append($("<td>").html('<span>$ &nbsp;&nbsp;</span><span class="pull-right" >' + costo_linea + '</span>'));
                    $('tbody').append(tr);
                    costo_total += costo_linea;
                    
                });
                actualizar_total(costo_total);
            });
        });
    </script>
    
</asp:Content>
