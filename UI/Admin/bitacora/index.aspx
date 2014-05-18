<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="index.aspx.vb" Inherits="UI.index" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_bitacora")%></div>
            </header>
            <section>
              
                <div>
                    <div class="form-inline">

					        <label for="bita_filer_by_user"><% =translate("bita_filtro_usuario")%>
                                <input type="text" class="input-medium search-query" name="bita_filtro_usuario" id="bita_filtro_usuario"  placeholder="<% =translate("username")%>" />
					        </label>

					        <label  for="bita_filer_by_user"><% =translate("bita_filtro_categoria")%>
                                <input type="text" class="input-medium search-query" name="bita_filtro_categoria" id="bita_filtro_categoria"  placeholder="<% =translate("categoria")%>" />
					        </label>							    
                            <button type="button" id="filtrar" class="btn"><i class=" icon-search"></i> <% =translate("filtrar") %></button> 
					</div>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_usuario")%></th>
                                <th><%=translate("th_categoria")%></th>
                                <th><%=translate("Fecha")%></th>
                                <th><%=translate("Descripcion")%></th>
                            </tr>
                        </thead>
                        <tbody>
                   <%--         <% For Each entrada As BE.Bitacora In lista_bita%>
                                <tr class="bita_row" id="<% =entrada.Id %>">
                                    <td><% =entrada.Usuario.Username%></td>
                                    <td><% =entrada.Categoria%></td>
                                    <td><% =entrada.Fecha%></td>
                                    <td><% =entrada.Descripcion%></td>
                                </tr>                            
                            <% Next%>                         --%>   
                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script type="text/javascript">
    // make active Admin tab
        $('.active').removeClass('active');
        $('.menu_admin').addClass('active');

        var prittifyTime = function (digit) {
            return (parseInt(digit,10) < 10 ) ? "0"+digit : digit
        };
        // Extends Date prototype
        Date.prototype.myCustomFormat = function (format) {
            // formatos aceptados
            // dd/mm/yyyy HH:MM:SS
            // mm/dd/yyyy HH:MM:SS
            var day = prittifyTime(this.getDate());
            var month = prittifyTime(this.getMonth());
            var fullyear = prittifyTime(this.getFullYear());

            var hr = prittifyTime(this.getHours());
            var mins = prittifyTime(this.getMinutes());
            var secs = prittifyTime(this.getSeconds());

            var fulldate = "";
            if (format == "mm/dd/yyyy") {
                fulldate += month + "/" + day + "/" + fullyear
            }
            else {
                fulldate += day + "/" + month + "/" + fullyear
            }

            fulldate += " " + hr + ":" + mins + ":" + secs;
            return fulldate
        };
        // fill the table
        var fillTable = function (data) {
            $.each(data.rows, function (k, v) {
                //console.log(v);
                var tr = $('<tr>').addClass('bita_row').attr("id", v.Id);
                var td = $('<td>').html(v.Usuario.Username);
                tr.append(td)
                tr.append($('<td>').html(v.Categoria))
                tr.append($('<td>').html(new Date(parseInt(v.Fecha.match("[0-9]+")[0])).myCustomFormat( data.date_format ) ) );
                tr.append($('<td>').html(v.Descripcion));
                $('tbody').append(tr);

            });
        };

        $(document).ready(function () {
            // load without search terms
            postdata = {
                bita_filtro_usuario: $('#bita_filtro_usuario').val(),
                bita_filtro_categoria: $('#bita_filtro_categoria').val()
            }
            $.post('/Admin/bitacora/filtrar.ashx', postdata, function (res) {
                if (res.status == 500) {
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                    + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                    + '<div class="alert-msg">' + res.msg + '</div></div>';

                    //remove if there any
                    $('.alert').remove();
                    $('section').prepend(div_alert);
                }
                else {
                    //borro la tabla
                    $('.bita_row').remove();
                    fillTable(res);
                }
            });

            // actions
            $('#filtrar').click(function (ev) {
                ev.preventDefault();
                // post to filter
                postdata = {
                    bita_filtro_usuario : $('#bita_filtro_usuario').val(),
                    bita_filtro_categoria : $('#bita_filtro_categoria').val()
                }
                $.post('/Admin/bitacora/filtrar.ashx', postdata, function (res) {
                    // if the session expired reload the page to go to login form
                    if (res.status == undefined) location.reload();

                    if (res.status == 500) {
                        var div_alert = '<div class="alert alert-' + alert_type + '">'
                        + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                        + '<div class="alert-msg">' + res.msg + '</div></div>';

                        //remove if there any
                        $('.alert').remove();                        
                        
                        $('section').prepend(div_alert);
                    }
                    else {
                        //borro la tabla
                        $('.bita_row').remove();
                        fillTable(res);
                    }
                                        
                } );
            });
        });
        </script>
</asp:Content>
