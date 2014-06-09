<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CloudServices.Master" CodeBehind="repos.aspx.vb" Inherits="UI.repos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .separador {
            margin-right: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
       <div class="row-fluid">
        <div class="span10 offset1">
            <header>
                <div class="well">
                    <h2><strong><% =translate("welcome_msg_reportes")%></strong></h2>
                    <h5><% =translate("seleccione_el_reporte")%></h5>
                    <hr />
                    <div class="form-inline">
                        <label class="inline" for="repo_tipo"><% =translate("reporte_por")%>
                            <select name="repo_tipo" id="repo_tipo">                            
                                <option value="pesos"><% =translate("ventas")%></option>
                                <option value="q_ventas"><% =translate("q_ventas")%></option>
                                <option value="q_ventas_por"><% =translate("q_ventas_por")%></option>                                          
                                </select>    
                        </label>
                        <span class="separador"></span>
                        <button  class="btn btn-primary" data-action="generar" id="generar_repo"><%=translate("btn_generar")%></button>
                    </div>
                </div>
            </header>
            <section>
                
                    
               
                <div id="container" style="width:100%; height:400px;"></div>            
            </section>
          </div>
        </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="js_block" runat="server">
    <script src="/scripts/highcharts/highcharts.js"></script>
    <script>

        var repo_proyeccion = function (repo) {
            var categories = [];
            var data_real = [];
            var data_proy = [];
            $.each(repo.Cuerpo, function (key, obj) {
                categories.push(key);
                data_real.push((parseFloat(obj.real)) ? parseFloat(obj.real) : null);
                data_proy.push((parseFloat(obj.proy)) ? parseFloat(obj.proy) : null);
            });

            // genero el gráfico
            $('#container').highcharts({
                chart: {
                    type: 'spline'
                },
                title: {
                    text: repo.Titulo
                },
                subtitle: {
                    text: repo.Footer
                },
                xAxis: {

                    categories : categories
                },
                yAxis: {
                    title: {
                        text: 'Ventas($)'
                    },
                    labels: {
                        formatter: function () {                            
                            return '$ '+ this.value
                        }
                    }
                },
                tooltip: {
                    crosshairs: true,
                    shared: true
                },
                plotOptions: {
                    spline: {
                        marker: {
                            radius: 4,
                            lineColor: '#666666',
                            lineWidth: 1
                        }
                    }
                },
                series: [{
                    name: 'Real',
                    marker: {
                        symbol: 'square'
                    },
                    data: data_real
                }, {
                    name: 'Proyectado',
                    marker: {
                        symbol: 'diamond'
                    },
                    data:  data_proy
                }]
            });
        };




        var repo_q_ventas = function (repo) {
            var categories = [];
            var data_real = [];
            var data_proy = [];
            $.each(repo.Cuerpo, function (key, obj) {
                categories.push(key);
                data_real.push((parseFloat(obj.real)) ? parseFloat(obj.real) : null);
                data_proy.push((parseFloat(obj.proy)) ? parseFloat(obj.proy) : null);
            });

            // genero el gráfico
            $('#container').highcharts({
                chart: {
                    type: 'spline'
                },
                title: {
                    text: repo.Titulo
                },
                subtitle: {
                    text: repo.Footer
                },
                xAxis: {

                    categories: categories
                },
                yAxis: {
                    title: {
                        text: 'Cantidad de servicios'
                    },
                    labels: {
                        formatter: function () {
                            return this.value
                        }
                    }
                },
                tooltip: {
                    crosshairs: true,
                    shared: true
                },
                plotOptions: {
                    spline: {
                        marker: {
                            radius: 4,
                            lineColor: '#666666',
                            lineWidth: 1
                        }
                    }
                },
                series: [{
                    name: 'Real',
                    marker: {
                        symbol: 'square'
                    },
                    data: data_real
                }, {
                    name: 'Proyectado',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: data_proy
                }]
            });
        };




        var debug, debug2;
        var repo_q_ventas_por = function (repo) {
            debug = repo;
            var categories = [];
            var servicios = {
                BECloudServerBasic: [],
                BECloudServerAdvance: [],
                BECloudServerPro: [],
                BEBackupService: [],
                BESnapshot: []
            };
            debug2 = servicios;
            $.each(repo.Cuerpo, function (key, obj) {
                categories.push(key);
                $.each(servicios, function (key, value) {
                    servicios[key].push( parseInt(obj[key],10));
                });

            });

            // genero el gráfico
            $('#container').highcharts({
                chart: {
                    type: 'spline'
                },
                title: {
                    text: repo.Titulo
                },
                subtitle: {
                    text: repo.Footer
                },
                xAxis: {

                    categories: categories
                },
                yAxis: {
                    title: {
                        text: 'Cantidad de servicios'
                    },
                    labels: {
                        formatter: function () {
                            return this.value
                        }
                    }
                },
                tooltip: {
                    crosshairs: true,
                    shared: true
                },
                plotOptions: {
                    spline: {
                        marker: {
                            radius: 4,
                            lineColor: '#666666',
                            lineWidth: 1
                        }
                    }
                },
                series: [{
                    name: 'BECloudServerBasic',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerBasic
                }, {
                    name: 'BECloudServerAdvance',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerAdvance
                }, {
                    name: 'BECloudServerPro',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerPro
                }, {
                    name: 'BEBackupService',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BEBackupService
                }, {
                    name: 'BESnapshot',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BESnapshot
                }]
            });
        };
   
        var typos_handlers = {
            "pesos": repo_proyeccion,
            "q_ventas": repo_q_ventas,
            "q_ventas_por": repo_q_ventas_por
        }



        // binding
        $('#generar_repo').click(function (ev) {
            ev.preventDefault();
            var type = $('#repo_tipo').val();
            $.post('/Admin/reportes/repo_data.ashx', { type: type }, function (res) {
                //console.log(res);
                // if the session expired reload the page to go to login form
                if (res.status == undefined) {
                    location.reload();
                }
                else {
                    //limpio el container
                    $('#container').html("");
                    var alert_type = (res.status == 200) ? "info" : "error";
                    var div_alert = '<div class="alert alert-' + alert_type + '">'
                            + '<button type="button" class="close" data-dismiss="alert">&times;</button>'
                            + '<div class="alert-msg">' + res.msg + '</div></div>';
                    //remove if there any
                    $('.alert').remove();
                    $('section').prepend(div_alert);

                    // if ok add the graph
                    if (res.status == 200) {
                        // repo_proyeccion(res.repo)
                        typos_handlers[type](res.repo);
                    }
                }
                
            }, "json");
        });
    </script>
</asp:Content>
