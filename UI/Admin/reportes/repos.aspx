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
                            <select name="repo_tipo" id="repo_tipo" class="input-xxlarge">                            
                                <option value="pesos"><% =translate("ventas")%></option>
                                <option value="q_ventas"><% =translate("q_ventas")%></option>
                                <option value="q_ventas_por"><% =translate("q_ventas_por")%></option>
                                <option value="uso_servers"><% =translate("uso_server_fisico")%></option>
                                <option value="capacidad"><% =translate("repo_capacidad")%></option>                                           
                                </select>    
                        </label>
                        <span class="separador"></span>
                        <button  class="btn btn-primary ladda-button" data-action="generar" id="generar_repo" data-style="expand-left" data-size="xs" data-spinner-size="20">
                            <span class="ladda-label"><%=translate("btn_generar")%></span>
                        </button>
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
    <script src="/scripts/cloud/customs_adm.js"></script>
    <script src="/scripts/highcharts/highcharts.js"></script>
    <%--<script src="http://code.highcharts.com/highcharts-3d.js"></script>--%>
 
    <script>
        var debug_proy;

        var repo_proyeccion = function (repo) {
            debug_proy = repo;
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
                    floor: 0,
                    title: {
                        text: '<%=translate("Ventas")%>'
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
                    name: '<%=translate("Real")%>',
                    marker: {
                        symbol: 'square'
                    },
                    data: data_real
                }, {
                    name: '<%=translate("Proyectado")%>',
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
                data_proy.push((parseInt(obj.proy,10)) ? parseInt(obj.proy,10) : null);
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
                    floor: 0,
                    title: {
                        text: '<%=translate("Cantidad_de_servicios")%>'
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
                    name: '<%=translate("Real")%>',
                    marker: {
                        symbol: 'square'
                    },
                    data: data_real
                }, {
                    name: '<%=translate("Proyectado")%>',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: data_proy
                }]
            });
        };



        var getTotal = function(valores){
            sum = 0;
            $.each(valores,function(k,v){
                sum += v;
            });
            return sum;
        }

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
                    floor: 0,
                    title: {
                        text: '<%=translate("Cantidad_de_servicios")%>'
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
                    type: 'spline',
                    name: 'BECloudServerBasic',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerBasic
                }, {
                    type: 'spline',
                    name: 'BECloudServerAdvance',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerAdvance
                }, {
                    type: 'spline',
                    name: 'BECloudServerPro',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BECloudServerPro
                }, {
                    type: 'spline',
                    name: 'BEBackupService',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BEBackupService
                }, {
                    type: 'spline',
                    name: 'BESnapshot',
                    marker: {
                        symbol: 'diamond'
                    },
                    data: servicios.BESnapshot
                },
                //pie
                {
                    type: 'pie',
                    name: 'Total',
                    data: [{
                        name: 'BECloudServerBasic',
                        y: getTotal(servicios.BECloudServerBasic),
                        color: Highcharts.getOptions().colors[0]
                    }, {
                        name: 'BECloudServerAdvance',
                        y: getTotal(servicios.BECloudServerAdvance),
                        color: Highcharts.getOptions().colors[1]
                    }, {
                        name: 'BECloudServerPro',
                        y: getTotal(servicios.BECloudServerPro),
                        color: Highcharts.getOptions().colors[2]
                    }, {
                        name: 'BEBackupService',
                        y: getTotal(servicios.BEBackupService),
                        color: Highcharts.getOptions().colors[3]
                    }, {
                        name: 'BESnapshot',
                        y: getTotal(servicios.BESnapshot),
                        color: Highcharts.getOptions().colors[4]
                    }],
                    center: [100, 80],
                    size: 100,
                    showInLegend: false,
                    dataLabels: {
                        enabled: false
                    }

                }]
            });
        };
        var debug_servers
        var repo_uso_servers = function (repo) {
            var categories = [];
            var serie_used = [];
            var serie_free = [];
            debug_servers = repo;

            $.each(repo.Cuerpo, function (key, obj) {
                //console.log(key);
                categories.push(key);
                serie_used.push(parseInt(obj['mem_usada'], 10));
                serie_free.push(parseInt(obj['mem_total'], 10) - parseInt(obj['mem_usada'], 10));
            });

            //// genero el gráfico
            $('#container').highcharts({
                chart: {
                    type: 'column',
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //},
                    //marginTop: 80,
                    //marginRight: 40
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
                    min: 0,
                    title: {
                        text: '%'
                    }
                },
                tooltip: {
                    pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y} GB</b> ({point.percentage:.0f}%)<br/>',
                    shared: true
                },
                plotOptions: {
                    column: {
                        stacking: 'percent'
                    }
                },
                series: [{
                    name: '<%=translate("free")%>',
                    data: serie_free
                }, {
                    name: '<%=translate("used")%>',
                    data: serie_used
                }]
            });

        };
   
        var repo_capacidad = function (repo) {
            var categories = [];
            var serie_used = [];
            var serie_free = [];
            var serie_proy = [];
            debug_servers = repo;

            $.each(repo.Cuerpo, function (key, obj) {
                //console.log(key);
                categories.push(key);
                serie_used.push(parseInt(obj['usada'], 10));
                serie_proy.push(parseInt(obj['proy'], 10));
                serie_free.push(parseInt(obj['libre'], 10));
            });

            //// genero el gráfico
            $('#container').highcharts({
                chart: {
                    type: 'column',
                    //options3d: {
                    //    enabled: true,
                    //    alpha: 15,
                    //    beta: 15,
                    //    depth: 50,
                    //    viewDistance: 25
                    //},
                    //marginTop: 80,
                    //marginRight: 40
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
                    min: 0,
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y} GB</b> ({point.percentage:.0f}%)<br/>',
                    shared: true
                },
                plotOptions: {
                    column: {
                        stacking: 'percent'
                    }
                },
                series: [{
                    name: '<%=translate("free")%>',
                    data: serie_free
                }, {
                    name: '<%=translate("Proyectado")%>',
                    data: serie_proy
                },{
                    name: '<%=translate("used")%>',
                    data: serie_used
                }]
            });

       };


        var typos_handlers = {
            "pesos": repo_proyeccion,
            "q_ventas": repo_q_ventas,
            "q_ventas_por": repo_q_ventas_por,
            "uso_servers": repo_uso_servers,
            "capacidad": repo_capacidad
        }



        // binding
        $('#generar_repo').click(function (ev) {
            ev.preventDefault();
            var type = $('#repo_tipo').val();
            var l = Ladda.create(this);
            l.start();
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
                
            }, "json")
                .always(function () { l.stop(); });
        });
    </script>
</asp:Content>
