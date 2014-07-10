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
                <div class="well"><% =translate("indicaciones_seleccione")%></div>
            </header>
            <section>
                <div class="row-fluid">
                    <% For Each p As BE.BEServicioBase In lista_srv%> 
                    <div class="span4 producto">
                        <h4><%=p.Nombre%></h4>
                        <p><%=p.Descripcion%></p>
                        <span class="precio">$ <%=p.Precio%></span>
                        <div class="pull-right">
                            <button class="btn btn-success" data-codigo="<%=p.Codigo %>" id="srv-<%=p.Id%>"><%=translate("agregar") %></button>                                                        
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
                    <br />
                <button class="btn btn-success" id="generar_ov"><%=translate("btn_contratar")%></button>
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

        var Servicio = function (id, codigo, precio) {
            this.Id = id;
            this.Codigo = codigo;
            this.Precio = precio;
            this.Srv_adicionales = [];
        }

        var generar_ov = function () {

        };

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
            var tr = $("<tr class='line-" + line + "'>");
            var td = $("<td>").append($("<input>").attr({"type":"hidden","data-codigo": srv.Codigo,"data-srvid": srv.Id, "data-precio": srv.Precio}));
                td.append(srv.Nombre);
            tr.append(td);
            var inline_form = '<div class="form-inline">';
            $.each(addons, function (k, addon) {
                inline_form += '<label class="checkbox inline"><input type="checkbox" onChange="procesar(this)" id="'+line+'-'+addon.Codigo+'" data-srvid="'+addon.Id+'" data-line="'+line+'" data-codigo="'+ addon.Codigo +'"data-precio="' + addon.Precio + '"/>' + addon.Nombre + ' </label>';
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

            $('#generar_ov').click(function (ev) {
                // validate and make a post
                var trs = $('tbody tr');
                var servicios = []
                $.each(trs, function (k, tr) {
                    // recorro los trs y voy creando los servicios

                    // encuentro el server y creo el objeto contenedor
                    var srv_temp = $(tr).find("input[type='hidden']");
                    var Srv = new Servicio($(srv_temp).data("srvid"), $(srv_temp).data("codigo"), $(srv_temp).data("precio"))
                    // busco si tiene adicionales
                    $.each($(tr).find("input[type='checkbox']"), function (k, chbox) {
                        if ($(chbox).prop("checked")) {
                            // genero el adicional y se lo agrego a Srv
                            var addSrv = new Servicio($(chbox).data("srvid"), $(chbox).data("codigo"), $(chbox).data("precio"))
                            Srv.Srv_adicionales.push(addSrv)
                        }
                    } );
                    servicios.push( Srv );
                });
                // not make empty post
                if (servicios.length > 0) {
                    $.post('/cloud/add_ov.ashx', { ov: JSON.stringify(servicios) }, function (res) {
                        //console.log(res);

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

                            // continue
                            if (res.status == "200") {
                                
                                // it's new so reload the page in 2 sec
                                setTimeout(function () { location.href = "/cloud/home.aspx"; }, 2000);
                            }
                        }
                        
                    });
                }
                
                console.log(JSON.stringify(servicios))
            });

            actualizar_total();
        });
    </script>
</asp:Content>
