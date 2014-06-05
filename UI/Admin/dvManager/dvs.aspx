<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="dvs.aspx.vb" Inherits="UI.dvs" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

      <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_msg_dvs")%></div>
            </header>
            <section>
                <div class="alert alert-info">
                    <button type="submit" class="btn btn-primary" id="ejecutar_dv"><%=translate("ejecutar_dvh")%></button>
                </div>
            </section>
            </div>
          </div>
    <!-- 
        <form class="form-horizontal">
           <div class="control-group">
                <label class="control-label" for="username"><% =translate("nombre_de_la_tabla")%></label>
                <div class="controls">
       -->
                     <!-- "Usuario", "Bitacora", "Familia" -->
         <!--
                        <select name="table_name" id="table_name">
                        <option value="Usuario">Usuario</option>
                        <option value="Bitacora">Bitacora</option>
                        <option value="Familia">Familia</option>
                    </select>
            -->
                        <!--<input type="text" name="table_name" id="table_name" maxlength="50" />-->
               <!--
                 </div>
            </div>
        <button type="submit" class="btn btn-primary" id="ejecutar_dv"><%=translate("ejecutar_dvh")%></button>
    -->
        <!--<button type="submit" class="btn btn-primary" id="ejecutar_dvh"><%=translate("ejecutar_dvh")%></button>
        <button type="submit" class="btn btn-primary" id="ejecutar_dvv"><%=translate("ejecutar_dvv")%></button>-->
    <!--</form>
    -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        $('#ejecutar_dv').click(function (ev) {
            ev.preventDefault();
            //$.post('/Admin/dvManager/generate_dv.ashx', { table_name: $("#table_name").val() }, function (res) {
            $.post('/Admin/dvManager/generate_dv.ashx',  function (res) {
                if (res.status == undefined) location.reload();
                var alert_type = (res.status == 200) ? "info" : "error";
                var div_alert = '<div class="alert alert-' + alert_type + '">'
                    + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                    + '<div class="alert-msg">' + res.msg + '</div></div>';
                //remove if there any
                $('.alert').remove();
                $('.container-fluid').prepend(div_alert);

                if (res.status == 200) {
                    setTimeout(function () { location.reload(true) }, 2000);
                }
            }, "json");
            });


        // old methods
        //
        //$('#ejecutar_dvv').click(function (ev) {
        //    ev.preventDefault();
        //    $.post('/Admin/dvManager/generate_dv.ashx', { table_name: $("#table_name").val(), type: "dvv" }, function (res) {
        //        if (res.status == undefined) location.reload();
        //        var alert_type = (res.status == 200) ? "info" : "error";
        //        var div_alert = '<div class="alert alert-' + alert_type + '">'
        //            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
        //            + '<div class="alert-msg">' + res.msg + '</div></div>';
        //        //remove if there any
        //        $('.alert').remove();
        //        $('.container-fluid').prepend(div_alert);
        //    }, "json");
        //});

    </script>
</asp:Content>
