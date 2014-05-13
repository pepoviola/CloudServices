<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="dvs.aspx.vb" Inherits="UI.dvs" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <form class="form-horizontal">
           <div class="control-group">
                <label class="control-label" for="username"><% =translate("nombre_tabla")%></label>
                <div class="controls">
                        <input type="text" name="table_name" id="table_name" maxlength="50" />
                </div>
            </div>
        <button type="submit" class="btn btn-primary" id="ejecutar_dv"><%=translate("ejecutar") %></button>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        $('#ejecutar_dv').click(function (ev) {
            ev.preventDefault();
        });
    </script>
</asp:Content>
