<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="dvs.aspx.vb" Inherits="UI.dvs" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form class="form-horizontal">
           <div class="control-group">
                <label class="control-label" for="username"><% =translate("nombre_de_la_tabla")%></label>
                <div class="controls">
                        <input type="text" name="table_name" id="table_name" maxlength="50" />
                </div>
            </div>
        <button type="submit" class="btn btn-primary" id="ejecutar_dvh"><%=translate("ejecutar_dvh")%></button>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        $('#ejecutar_dvh').click(function (ev) {
            ev.preventDefault();
            $.post('/Admin/dvManager/generate_dv.ashx', { table_name: $("#table_name").val() }, function (res) {
                if (res.status == undefined) location.reload();
                var alert_type = (res.status == 200) ? "info" : "error";
                var div_alert = '<div class="alert alert-' + alert_type + '">'
                    + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                    + '<div class="alert-msg">' + res.msg + '</div></div>';

                //remove if there any
                $('.alert').remove();

                $('.container-fluid').prepend(div_alert);
            }, "json");
            });
    </script>
</asp:Content>
