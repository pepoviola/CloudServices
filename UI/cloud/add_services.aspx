<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="add_services.aspx.vb" Inherits="UI.add_services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .producto {
            border:1px solid #ccc;
            border-radius: 5px;
            /*height: 200px;*/
            background-color: #e6e5e5;
            padding: 15px 10px;
            
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
                <div class="row-fluid">
                    <% For Each p As BE.BEServicioBase In lista_srv%> 
                    <div class="span4 producto">
                        <h4><%=p.Nombre%></h4>
                        <p><%=p.Descripcion%></p>
                        <span class="precio">$ <%=p.Precio%></span>
                        <div class="pull-right">
                            <button class="btn btn-success" data-codigo="<%=p.Codigo %>" id="srv-<%=p.Id%>">Agregar</button>                                                        
                        </div>
                    </div>
                 
                    <% Next%>                   
                </div>

                <!-- adicionales -->
                <br /><br />
                <div class="row-fluid">
                    <% For Each p As BE.BEServicioAdicional In lista_addon%>
                    <div class="span4 producto">
                        <h4><%=p.Nombre%></h4>
                        <p><%=p.Descripcion%></p>
                        <span class="precio">$ <%=p.Precio%></span>                       
                    </div>
                 
                    <% Next%>                   
                </div>


                <div>                    
   
                    <br /><br />
                    <h3>Orden</h3>
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Servicio</th>
                                <th>Adicionales</th>
                                <th>Costo Mensual</th>
                            </tr>
                        </thead>
                        <tbody>
                         
                        </tbody>
                        <tfoot>
                            <tr>
                                <td>Total</td>
                                <td></td>
                                <td ><span>$ &nbsp;&nbsp;</span><span class="pull-right" id="costo_total"></span></td>
                            </tr>
                        </tfoot>
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
        var servicios = JSON.parse('<%=json_srv%>');
        var addons = JSON.parse('<%=json_addon%>');

        //
        total_ov = 0;
        var line = 0;

        var actualizar_total = function () {
            $('#costo_total').html(total_ov);
        }

        var procesar = function (el) {
            var obj = $("#" + el.id);
            if ( obj.prop("checked") ) {
                // sumo el valor de este servicio
                // actualizo la linea
                var total_linea = parseFloat($('#precio-line-' + obj.data("line")).html()) + parseFloat(obj.data("precio"));
                $('#precio-line-' + obj.data("line")).html(total_linea);
                total_ov += parseFloat(obj.data("precio"));
            }
            else {
                // resto el valor de este servicio
                var total_linea = parseFloat($('#precio-line-' + obj.data("line")).html()) - parseFloat(obj.data("precio"));
                $('#precio-line-' + obj.data("line")).html(total_linea);
                total_ov -= parseFloat(obj.data("precio"));
            }
            actualizar_total();
        };
        //
        var agregar_a_ov = function (srv ) {
            var tr = $("<tr class='line-"+line+"'>");
            tr.append($("<td>").html(srv.Nombre));
            var inline_form = '<div class="form-inline">';
            $.each(addons, function (k, addon) {
                inline_form += '<label class="checkbox inline"><input type="checkbox" onChange="procesar(this)" id="'+line+'-'+addon.Codigo+'" data-line="'+line+'" data-codigo="'+ addon.Codigo +'"data-precio="' + addon.Precio + '"/>' + addon.Nombre + ' </label>';
            });
            inline_form += "</div>";
            tr.append( $("<td>").html( inline_form ));
            tr.append($("<td>").html('<span>$ &nbsp;&nbsp;</span><span class="pull-right" id="precio-line-'+line+'">' + srv.Precio + '</span>'));
            $('tbody').append(tr);           
            total_ov += parseFloat(srv.Precio);
            actualizar_total();
            line++;
        };

        $(document).ready(function () {
            // binding
            $.each(servicios, function (k, obj) {
                $('#srv-' + obj.Id).click(function (ev) {
                    agregar_a_ov(obj);
                });

                

            });

            actualizar_total();
        });
    </script>
</asp:Content>
