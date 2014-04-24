<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="idiomas.aspx.vb" Inherits="UI.idiomas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_idioma") %></div>
            </header>
            <section>
              
                <div>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modal_idioma">
                            <i class="icon-plus icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    <br />
                    <br />
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>Codigo</th>
                                <th>descripcion</th>
                                <th>acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each l As BE.Idioma In langs %>
                                <tr id="idlang-<% =l.Id%>">
                                    <td><% =l.Codigo %></td>
                                    <td><% =l.Descripcion %></td>
                                    <td>
                                        <a href="#" class="btn btn-primary idioma_edit" data-idlang="<% =l.Id %>" data-codelang="<%=l.Codigo%>"><i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
                                        <a href="#" class="btn btn-danger idioma_delete" data-idlang="<% =l.Id %>"><i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>
                                    </td>
                                </tr>                            
                            <% Next%>                            
                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>

    <!-- modal -->
    <div id="modal_idioma" class="modal hide fade">
        <form class="form-horizontal" action="add_idioma.aspx" method="post">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3>Agregar Idioma</h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="idioma_code"><% =translate("idioma_code")%></label>
                <div class="controls">
                        <input type="text" name="idioma_code" id="idioma_code" placeholder="<% =Session("lang_code")%>" />
                </div>
             </div>
            <div class="control-group">
                <label class="control-label" for="idioma_descripcion"><% =translate("idioma_descripcion")%></label>
                <div class="controls">
                        <input type="text" name="idioma_descripcion" id="idioma_descripcion" placeholder="" />
                </div>
             </div>
              <!-- create dynamic form -->
          <% For Each t As BE.Tag In tags %>
            <div class="control-group">
                <label class="control-label" for="<% =t.id%>"><% =t.Codigo %></label>
                <div class="controls">
                        <input type="text" name="<% =t.Id%>" id="<% =t.Codigo%>" placeholder="<% =t.Leyenda%>" />
                </div>
             </div>
          <% Next%>
          

        </div>
        <div class="modal-footer">
            <a href="#" class="btn">Close</a>
            <button type="submit" class="btn btn-primary" data-action="create" id="idioma_create">save</button>
        </div>
    <!-- end form -->            
    </form>
    </div>

    <!-- .end modal -->
</asp:Content>
<asp:Content runat="server" ID="js" ContentPlaceHolderID="js_block">
    <script>
        $(document).ready(function () {
            //binding actions

            // edit idioma
            $('idioma_edit').click(function (ev) {
                ev.preventDefault();
                //get the tags and fill the form

            });
            // delete idioma
            $('.idioma_delete').click(function (ev) {
                ev.preventDefault();
                //console.log(ev.target.dataset.idlang);
                
                // make post
                $.post('/Admin/idioma/del_idioma.ashx', { idlang: ev.target.dataset.idlang }, function (data) {
                    console.log(data);
                    if (data.status == 200) {
                        // remove row                        
                        $('#idlang-' + data.idlang).remove();
                    }
                    else {
                        //show error
                        var div_alert = '<div class="alert alert-error">'
                                + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                                + '<div class="alert-msg">'+ data.msg+'</div></div>';

                        //remove if there any
                        $('.alert').remove();
                        $('section').prepend(div_alert);
                    }
                });

            });

        });

    </script>
</asp:Content>
