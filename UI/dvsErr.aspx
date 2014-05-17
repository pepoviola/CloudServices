<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="dvsErr.aspx.vb" Inherits="UI.dvsErr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1 ">
            <div class="alert alert-error alert-block">
                <strong>Errores en Digitos verificadores</strong>
            </div>
        </div>
        <div class="span10 offset1">
            <div class="table-errs">
                <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_tipo")%></th>
                                <th><%=translate("th_tabla")%></th>
                                <th>ID</th>                                
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                </table>
            </div>

       </div>
    </div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script>
        var dvhErrs = <%= Session("dvhErrs")%>
        ;var dvvErrs = <%= Session("dvvErrs")%>

        $(document).ready(function(){
            // primero dvh
            $.each(dvhErrs, function( k, v){ 
                // genero la tabla
                var tr = $('<tr>');
                tr.append( $('<td>').html("DVH") );
                tr.append( $('<td>').html( Object.keys(v)[0] ) );
                tr.append( $('<td>').html( v[ Object.keys(v)[0] ] ) );
                $('tbody').append(tr);
            } );
        });
    </script>
    
</asp:Content>
