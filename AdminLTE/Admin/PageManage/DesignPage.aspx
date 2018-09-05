<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DesignPage.aspx.cs" Inherits="AdminLTE.Admin.PageManage.DesignPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>显示页面编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/Ionicons/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
    folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/skins/_all-skins.min.css" />
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body>
    <form id="FromPage">
        <section class="content">
            <div class="box box-success" style="width: 20%">
                <div class="box-header with-border">
                    <h3 class="box-title">基础控件</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div id="external-events">
                        <div class="external-event bg-green ui-draggable ui-draggable-handle" data-coltype="text" style="position: relative;">文本框</div>
                        <div class="external-event bg-green ui-draggable ui-draggable-handle" style="position: relative;">Go home</div>
                        <div class="external-event bg-green ui-draggable ui-draggable-handle" style="position: relative;">Do homework</div>
                        <div class="external-event bg-green ui-draggable ui-draggable-handle" style="position: relative;">Work on UI design</div>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <div class="box box-info" style="width: 77%; min-height: 600px; height: 95%; position: absolute; top: 15px; right: 15px;">
            </div>
        </section>
    </form>
    <!-- jQuery 3 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- DataTables -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/fastclick/lib/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="../../Script/AdminLTE-2.4.2/dist/js/adminlte.min.js"></script>
    <!-- fullCalendar -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/moment/moment.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/bower_components/fullcalendar/dist/fullcalendar.min.js"></script>
    <!-- LAYER -->
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <script>        $(function () {
            init_events($('#external-events div.external-event'));            function init_events(ele) {
                ele.each(function () {
                    var $othis = $(this);
                    $othis.mousedown(function (e) {
                        var coltype = $othis.data("coltype");
                        var colName = $othis.text();
                        $("#external-events").append('<div class="external-event bg-green ui-draggable ui-draggable-handle colmousemove" data-coltype="' + coltype + '" style="position: relative;">' + colName + '</div>');
                        var $ithis = $("#external-events .colmousemove");
                        var isMove = true;
                        var div_x = $ithis.offset().left;
                        var div_y = $ithis.offset().top;
                        $ithis.css({ "left": div_x, "top": div_y, "z-index": 1070 });
                        $(document).mousemove(function (e) {
                            if (isMove) {
                                var obj = $ithis;
                                $ithis.css({ "left": e.pageX - div_x, "top": e.pageY - div_y });
                            }
                        }).mouseup(
                            function () {
                                isMove = false;
                                $ithis.removeClass("colmousemove");
                            });
                    });
                })
            }
        });    </script>
</body>
</html>

