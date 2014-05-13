<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="idiomas.aspx.vb" Inherits="UI.idiomas" %>
<%@ MasterType VirtualPath="~/CloudServices.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_mesg_idioma") %></div>
            </header>
            <section>
              <% If Not String.IsNullOrEmpty(Me.Request.QueryString("err")) Then %>
                     <div class="alert alert-error">
                         <button type="button" class="close" data-dismiss="alert">&times;</button>
                           <div class="alert-msg"><%=translate(Me.Request.QueryString.Get("err"))%></div>
                     </div>
                  <%End If%>
                <div>
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modal_idioma" data-action="add">
                            <i class="icon-flag icon-white"></i> <% =translate("btn_new")%>
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
                                        <a href="#" class="btn btn-primary idioma_edit" data-idlang="<% =l.Id %>" data-codelang="<%=l.Codigo%>" data-descripcionlang="<% =l.Descripcion %>"><i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%></a>
                                        <a href="#" class="btn btn-danger idioma_delete" data-idlang="<% =l.Id %>"  data-codelang="<%=l.Codigo%>"><i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>
                                    </td>
                                </tr>                            
                            <% Next%>                            
                        </tbody>                    
                    </table>
                </div>
            </section>            
        </div>
    </div>
    
    <!-- modal create -->
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
    <!-- .end modal create -->

        <!-- modal edit -->
    <div id="modal_idioma_edit" class="modal hide fade">
        <form class="form-horizontal" id="edit_idioma_form" >
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3>Editar Idioma</h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="idioma_code"><% =translate("lbl_idioma_code")%></label>
                <div class="controls">
                        
                        <input type="text" name="idioma_code" id="edit_idioma_code" placeholder="" value="" readonly/>
                </div>
             </div>
            <div class="control-group">
                <label class="control-label" for="idioma_descripcion"><% =translate("lbl_idioma_descripcion")%></label>
                <div class="controls">
                        <input type="text" name="idioma_descripcion" id="edit_idioma_descripcion" placeholder="" disabled />
                </div>
             </div>
              <!-- create dynamic form -->
          <% For Each t As BE.Tag In tags %>
            <div class="control-group">
                <label class="control-label" for="<% =t.id%>"><% =t.Codigo %></label>
                <div class="controls">
                        <input type="text" name="<% =t.Id%>" id="edit_<% =t.Codigo %>" placeholder="" />
                </div>
             </div>
          <% Next%>
          

        </div>
        <div class="modal-footer">
            <a href="#" class="btn">Close</a>
            <button type="submit" class="btn btn-primary" data-action="edit" id="edit_idioma_save" >save</button>
        </div>
    <!-- end form -->            
    </form>
    </div>
    <!-- .end modal edit -->

</asp:Content>
<asp:Content runat="server" ID="js" ContentPlaceHolderID="js_block">
    <script>
        $(document).ready(function () {
            //binding actions

            // edit idioma
            $('.idioma_edit').click(function (ev) {
                ev.preventDefault();
                //set id /code/ descripcion the DRY way
                var ids = [ 'code', 'descripcion'];
                $.each(ids, function (k, v) {
                    //console.log($(ev.target).data(v + 'lang'));
                    $('#edit_idioma_' + v).val($(ev.target).data(v+'lang'));
                });
                
                //get the tags and fill the form
                $.get('/Admin/idioma/tags.ashx?codelang=' + $(this).data("codelang"), function (data) {
                    //console.log(data);
                    $.each(data.tags, function (k, v) {
                        //console.log(v);
                        $('#edit_' + v.Codigo).val(v.Leyenda);
                    });
                });

                // show the modal
                $('#modal_idioma_edit').modal("show");
            });

            // save edited idioma
            $('#edit_idioma_save').click(function (ev) {
                ev.preventDefault();
                //console.log($('#edit_idioma_form').serialize());
                //post
                $.post('/Admin/idioma/tags.ashx', $('#edit_idioma_form').serialize(), function (data) {
                    //console.log(data);
                    $('#modal_idioma_edit').modal("hide");
                    var alert_type = (data.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                            + '<div class="alert-msg">' + data.msg + '</div></div>';
                    //remove if there any
                    $('.alert').remove();
                    $('section').prepend(div_alert);

                });
            });
            // delete idioma
            $('.idioma_delete').click(function (ev) {
                ev.preventDefault();
                
                // make post
                $.post('/Admin/idioma/del_idioma.ashx', { idlang: $(this).data("idlang"), codelang: $(this).data("codelang") },
                   function (data) {
                       console.log(data);
                       var alert_type = (data.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                            + '<div class="alert-msg">' + data.msg + '</div></div>';

                    if (data.status == 200) {
                        // remove row                        
                        $('#idlang-' + data.idlang).remove();
                    }
                        //remove if there any
                        $('.alert').remove();
                        $('section').prepend(div_alert);
                });

            });

        });

    </script>
</asp:Content>
