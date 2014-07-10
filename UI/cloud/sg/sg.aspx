<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="sg.aspx.vb" Inherits="UI.sg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .regla{
           margin-bottom:5px;
        }
        .labelin {
            margin-left: 3px;
            margin-right: 3px;
        }
            .sidebar-nav {
            padding: 9px 0px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">

           <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well"><% =translate("welcome_msg_sec_group")%></div>
            </header>
            <section>
             
                <div>
                    
                    <div class="pull-right">
                        <a href="#" class="btn btn-success" data-toggle="modal" data-target="#modalForm" data-action="add">
                            <i class="icon-fire icon-white"></i> <% =translate("btn_new")%>
                        </a>
                    </div>
                    
                    <br />
                    <br />
                    <div class="row-fluid">
                        <div class="span3">
                            <div class="well sidebar-nav">
                                <ul class="nav nav-list">
                                    <li class="nav-header">Menu</li>
                                    <li ><a href="/cloud/home.aspx"><% =translate("menu_home")%></a></li>
                                    <li class="active"><a href="#"><% =translate("menu_sg")%></a></li>                                    
                                </ul>
                            </div>
                        </div>
                    <div class="span9">
                    <table class="table table-bordered table-hover">
                        <thead>
                            <tr>
                                <th><%=translate("th_sec_nombre")%></th>
                                <th><%=translate("th_fecha")%></th>                                
                                <th><%=translate("th_acciones")%></th>
                                
                            </tr>
                        </thead>
                        <tbody>
                            <% For Each s As BE.BEGrupoSeguridad In sgs%>
                                <tr id="idServer-<% =s.Id%>">
                                    <td><% =s.Nombre%></td>
                                    <td><% =s.FechaIn%></td>
                                    
                                    
                                    <td>                                        
                                         <a href="#" class="btn btn-primary edit" data-sid="<% =s.Id%>" data-name="<%=s.Nombre %>">
                                             <i class="icon-pencil icon-white"></i> <% =translate("btn_edit")%>
                                         </a>
                                        <a href="#" class="btn btn-danger delete" data-sid="<% =s.Id%>"   ><i class="icon-trash icon-white"></i> <% =translate("btn_delete")%></a>                                        
                                    </td>
                                    
                                </tr>                            
                            <% Next%>                            
                        </tbody>                    
                    </table>
                        </div>
                        </div>
                </div>
            </section>            
        </div>
    </div>


      <!-- modal create -->
    <div id="modalForm" class="modal hide fade">
        <form class="form-horizontal" id="form_create">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("h3_agregar_grupo_seguridad")%></h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="name"><% =translate("th_sec_nombre")%></label>
                <div class="controls">
                        <input type="text" name="name" id="name_create" placeholder="" maxlength="50"/>
                </div>
             </div>
            
                <h6 class=""><% =translate("th_reglas")%></h6>
            
            <div class="reglas">
                <div class="form-inline regla" id="regla-0">
                    <label for="origen"><% =translate("origen")%></label>
                    <input type="text" class="input-medium" name="origen" id="origen-0" placeholder="ALL / ip"  maxlength="50"/>
                    <label for="destino"><% =translate("destino")%></label>
                    <input type="text" class="input-mini" name="destino" id="destino-0"  placeholder="1-65535" maxlength="50"/>
                    <label for="origen"><% =translate("regla")%></label>
                    <select name="action_to" class="input-small">
                        <option value="aceptar"><% =translate("aceptar")%></option>
                        <option value="rechazar"><% =translate("rechazar")%></option>
                    </select>                    
                    <a href="#" class="add-rule btn btn-success" onclick="addRuleForm(1)" data-next="1" id="btn-0">+</a>
                </div>
            </div>
                
                
                
                
                
        </div>
            </form>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true">Close</a>
            <button type="submit" class="btn btn-primary" data-action="create" id="sg_create">save</button>
        </div>
    <!-- end form -->            

    </div>
    <!-- .end modal create -->


         <!-- modal edit -->
    <div id="modalEdit" class="modal hide fade">
        <form class="form-horizontal" id="edit-form">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h3><%=translate("h3_editar_grupo_seguridad")%></h3>
        </div>
        <div class="modal-body">
          
            <div class="control-group">
                <label class="control-label" for="hostname"><% =translate("th_sec_nombre")%></label>
                <div class="controls">
                        <input type="hidden" name="sid" id="sid-edit" />
                        <input type="text" name="name" id="name-edit" placeholder="" disabled="disabled"/>
                </div>
             </div>
             <h6 class=""><% =translate("th_reglas")%></h6>
            
            <div class="edit-reglas"></div>

      
          
                
        </div>
            </form>
        <div class="modal-footer">
            <a href="#" class="btn" data-dismiss="modal" aria-hidden="true">Close</a>
            <button type="submit" class="btn btn-primary" data-action="save" id="save-sg">save</button>
        </div>
    <!-- end form -->            

    </div>
    <!-- .end modal create -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">

    <script>
        // serializo

        var serializado = <%=sgs_serializado%>;

        // validar
        var validarNombre = function (f) {
            if ($(f).val().trim().length < 1) {
                return false
            }
            return true;
            
        };
        var validarOrigen = function (f) {
            var val = $(f).val();
            if (val.toUpperCase() === "ALL") {
                return true;
            }
            var ip = /^(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9]{2}|[0-9]{2}|[0-9])){3}$/;
            return val.match(ip);                        
        };
        var validarDestino = function (f) {
            var val = $(f).val();
            if (val.toUpperCase() === "ALL") {
                return true;
            }
            var valido = true;
            $.each(val.split('-'), function (k, v) {
                var n = parseInt(v, 10);
                if (! (n > 0 && n < 65536) ) {
                    valido = false
                }
            });
            return valido;
        };

        var validaciones = {
            "name": validarNombre,
            "origen": validarOrigen,
            "destino":validarDestino
        }

        var validarEdit = function (formId) {
            var valido = true;
            var fields = $(formId);

            $.each($(formId), function (k, v) {
                $(v).css("border-color", "#ccc");
                var tipo = $(v).attr("name");
                if( tipo != "sid"){
                 
                  
                    if (!(validaciones[tipo](v))) {                        
                        $(v).css("border-color","red")
                        valido = false
                    }
                }                
            });
            return valido;
        }

        var validar = function (formId) {
            var valido = true;
            $.each($(formId), function (k, v) {
                $(v).css("border-color", "#ccc");
                var tipo = $(v).attr("name");
                if( tipo != "sid"){
                    if (!(validaciones[tipo](v))) {
                        $(v).css("border-color","red")
                        valido = false
                    }
                }                
            });
            return valido;
        }


        var createFormRule = function (id) {
            next = parseInt(id, 10) + 1;
            var template = '<div class="form-inline regla" id="regla-'+ id +'">';
            template += '      <label class="labelin" for="origen">Origen </label>';
            template += '<input type="text" class="input-medium" name="origen" id="origen-' + id + '" placeholder="ALL / ip"  maxlength="50"/>'
            template += '<label class="labelin" for="destino">Destino </label>'
            template += '<input type="text" class="input-mini" name="destino" id="destino-' + id + '"  placeholder="1-65535" maxlength="50"/>'
            template += '<label class="labelin" for="origen">Regla </label>'
            template += '<select name="action_to" class="input-small">'
            template += '<option value="aceptar">aceptar</option>'
            template += '   <option value="rechazar">rechazar</option>'
            template += '</select>                    '
            template += '<a href="#" class="add-rule btn btn-success" onclick="addRuleForm('+next+')" data-next="' +next + '" id="btn-' + id + '">+</a>'
            template += '</div>'

            return template;
        };

        var createEditRule = function (id) {
            next = parseInt(id, 10) + 1;
            var template = '<div class="form-inline regla" id="edit-regla-'+ id +'">';
            template += '      <label class="labelin" for="origen">Origen </label>';
            template += '<input type="text" class="input-medium" name="origen" id="origen-' + id + '" placeholder="ALL / ip"  maxlength="50"/>'
            template += '<label class="labelin" for="destino">Destino </label>'
            template += '<input type="text" class="input-mini" name="destino" id="destino-' + id + '"  placeholder="1-65535" maxlength="50"/>'
            template += '<label class="labelin" for="origen">Regla </label>'
            template += '<select name="action_to" class="input-small">'
            template += '<option value="aceptar">aceptar</option>'
            template += '   <option value="rechazar">rechazar</option>'
            template += '</select>                    '
            //template += '<a href="#" class="add-rule btn btn-success" onclick="addRuleEdit('+next+')" data-next="' +next + '" id="edit-btn-' + id + '">+</a>'
            template += '<a href="#" class="add-rule btn btn-danger" onclick="removeEditRule('+id+')">-</a>'
            template += '</div>'

            return template;
        };

   

        function removeRule(id) {
            $('#regla-' + id).remove();
        }

        function removeEditRule(id) {
            $('#edit-regla-' + id).remove();
        }
        function addRuleForm(next) {
            var anterior = parseInt(next) -1;
            var f = createFormRule(next);
            $('#btn-' + anterior).removeClass("btn-success").addClass("btn-danger").html("-");
            $('#btn-' + anterior).attr("onclick","removeRule("+anterior+")")
            $('.reglas').append(f);
        };

        function addRuleEdit(next) {
            //var anterior = parseInt(next) -1;
            var f = createEditRule(next);
            var next = parseInt(next,10) + 1;
            //$('#edit-btn-' + anterior).removeClass("btn-success").addClass("btn-danger").html("-");
            //$('#edit-btn-' + anterior).attr("onclick","removeEditRule("+anterior+")")
            $('#edit-add-btn').attr( "onclick", "addRuleEdit("+ next +")");
            $('.edit-reglas').append(f);
        };
        
        function Regla(origen, dest, regla) {
            return {
                Origen : origen,
                PtoDestino : dest,
                Regla : regla
            }
        }

        $(document).ready(function () {

            // create edit
            $('.edit').click(function(ev){
                ev.preventDefault();
                // busco el fw
                var sid = $(this).data('sid');
                var fw;
                $.each(serializado,function(k,v){
                    if(v.Id == sid){
                        fw = v;
                    }
                });

                // creo el modal
                //console.log(fw);
                $('#sid-edit').val(sid);
                $('#name-edit').val(fw.Nombre);
                $(".edit-reglas").children().remove();
                var last = 0;
                
                $.each(fw.Reglas,function(k,r){
                    var template = '<div class="form-inline regla" id="edit-regla-'+ r.Id +'">';
                    template += '      <label class="labelin" for="origen">Origen </label>';
                    template += '<input type="text" class="input-medium" name="origen" id="origen-' + r.Id + '" placeholder="ALL / ip"  maxlength="50" value="'+r.Origen+'"/>'
                    template += '<label class="labelin" for="destino">Destino </label>'
                    template += '<input type="text" class="input-mini" name="destino" id="destino-' + r.Id + '"  placeholder="1-65535" maxlength="50" value="'+r.PtoDestino+'" />'
                    template += '<label class="labelin" for="origen">Regla </label>'
                    template += '<select name="action_to" class="input-small">'
                    if(r.Regla == "aceptar"){
                        template += '<option value="aceptar" selected>aceptar</option>'
                        template += '   <option value="rechazar">rechazar</option>'
                    }
                    else{
                        template += '<option value="aceptar" >aceptar</option>'
                        template += '   <option value="rechazar" selected>rechazar</option>'
                    }
                    template += '</select>                    '
                    template += '<a href="#" class="add-rule btn btn-danger" onclick="removeEditRule('+r.Id+')">-</a>'
                    template += '</div>'

                    $(".edit-reglas").append(template);
                    last = r.Id;
                });
                
                var next = parseInt(last,10)+1;
                var addBtn = '<a href="#" class="add-rule btn btn-success" onclick="addRuleEdit('+next+')" data-next="' +next + '" id="edit-add-btn">+</a>'
                $(".edit-reglas").prepend(addBtn);
               
                //$(".edit-reglas").append(createEditRule(parseInt(last,10)+1));
                //muestro
                $('#modalEdit').modal("show");            
            });


            // save
            $("#save-sg").click(function(ev){
                ev.preventDefault();
                if (validar('#edit-form input')) {
                
                    var reglas = [];
                    $.each($('#edit-form .regla'), function (k, v) {
                        var orig = $(v).find("input[name='origen']").val()
                        var dest = $(v).find("input[name='destino']").val()
                        var act = $(v).find("select").val()
                        var rule = Regla(orig, dest, act);
                        reglas.push(rule);
                    });
                   
                    $.post('/cloud/sg/save_fw.ashx', {name:$('#name-edit').val(),sid:$('#sid-edit').val(),reglas: JSON.stringify(reglas)}, function (res) {
                        //console.log(res);
                        $('#modalEdit').modal("hide");
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
                                setTimeout(function () { location.href = "/cloud/sg/sg.aspx"; }, 1000);
                            }
                        }
                    });
                }
                else {
                    alert("<%=translate("msg_validar")%>");
                };

            });
            
            // create
            $('#sg_create').click(function (ev) {
                ev.preventDefault();
                if (validar('#form_create input')) {
                    //post
                    var reglas = [];
                    $.each($('#form_create .regla'), function (k, v) {
                        var orig = $(v).find("input[name='origen']").val()
                        var dest = $(v).find("input[name='destino']").val()
                        var act = $(v).find("select").val()
                        var rule = Regla(orig, dest, act);
                        reglas.push(rule);
                    });
                    var fw = {
                        name: "demo",
                        reglas :reglas
                    }
                    $.post('/cloud/sg/add_fw.ashx', {name:$('#name_create').val(),reglas: JSON.stringify(reglas)}, function (res) {
                        //console.log(res);
                        $('#modalForm').modal("hide");
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
                                setTimeout(function () { location.href = "/cloud/sg/sg.aspx"; }, 2000);
                            }
                        }
                    });
                }
                else {
                    alert("<%=translate("msg_validar")%>");
                };
            });
        });

    </script>
</asp:Content>
