<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AdminLTE.Admin.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Family Chen </title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/skins/_all-skins.min.css" />
    <!-- Morris chart -->
    <%--<link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/morris.js/morris.css"/>--%>
    <!-- jvectormap -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/jvectormap/jquery-jvectormap.css" />
    <!-- Date Picker -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Daterange picker -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-daterangepicker/daterangepicker.css" />
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <div class="wrapper">
        <header class="main-header">
            <!-- Logo -->
            <a href="javascript:void(0)" class="logo">
                <!-- mini logo for sidebar mini 50x50 pixels -->
                <span class="logo-mini"><b>C</b>FL</span>
                <!-- logo for regular state and mobile devices -->
                <span class="logo-lg"><b>Chen</b>Family</span>
            </a>
            <!-- Header Navbar: style can be found in header.less -->
            <nav class="navbar navbar-static-top">
                <!-- Sidebar toggle button-->
                <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
                    <span class="sr-only">Toggle navigation</span>
                </a>
                <div class="navbar-custom-menu">
                    <ul class="nav navbar-nav">
                        <li class="dropdown user user-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <img src="<%=imgurl %>" class="user-image" alt="User Image" />
                                <span class="hidden-xs"><%=username %></span>
                            </a>
                            <ul class="dropdown-menu">
                                <!-- User image -->
                                <li class="user-header">
                                    <img src="<%=imgurl %>" class="img-circle" alt="User Image"/>
                                    <p><%=username %>
                                     <small>11/16 2017</small>
                                    </p>
                                </li>
                                <!-- Menu Footer-->
                                <li class="user-footer">
                                    <div class="pull-right">
                                        <a href="login.aspx" class="btn btn-default btn-flat">注销</a>
                                    </div>
                                    <div class="pull-right">
                                        <a href="login.aspx" class="btn btn-default btn-flat">修改密码</a>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <!-- Control Sidebar Toggle Button -->
                        <li>
                            <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
        <!-- Left side column. contains the logo and sidebar -->
        <aside class="main-sidebar">
            <!-- sidebar: style can be found in sidebar.less -->
            <section class="sidebar">
                <ul class="sidebar-menu" data-widget="tree">
                </ul>
            </section>
            <!-- /.sidebar -->
        </aside>
        <!-- Content Wrapper. Contains page content -->
        <div class="content-wrapper">
            <section class="content-header">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs" id="tabnav">
                        <li class="active" menu-moid="1000"><a href="javascript:setPage(1000)">首页</a></li>
                    </ul>
                    <section class="content" menu-type="nav-tabs" menu-moid="1000">
                    </section>
                </div>
                <!-- /.box-body -->
            </section>
        </div>
        <!-- /.content-wrapper -->
        <footer class="main-footer" style="margin-top:-20px;">
            <div class="pull-right hidden-xs">
                <b>Version</b> 5.2.0
            </div>
            <strong>Wedding Day &copy; 2016/04/17</strong>
        </footer>
        <!-- Control Sidebar -->
        <aside class="control-sidebar control-sidebar-dark">
            <!-- Create the tabs -->
            <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
                <li><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <!-- Home tab content -->
                <div class="tab-pane" id="control-sidebar-home-tab">
                    <h3 class="control-sidebar-heading">DMS</h3>
                    <ul class="control-sidebar-menu">
                        <li>
                            <a href='https://dms-net.aliyun.com/?host=qds108295464.my3w.com&port=1433&dbType=SQLServer&userName=qds108295464' target="_blank">
                                <i class="menu-icon fa fa-eyedropper bg-red"></i>
                                <div class="menu-info">
                                    <h4 class="control-sidebar-subheading">My DMS</h4>
                                    <p>qds108295464.my3w.com</p>
                                </div>
                            </a>
                        </li>
                    </ul>
                    <h3 class="control-sidebar-heading">后台</h3>
                    <ul class="control-sidebar-menu">
                        <li>
                            <a href='https://dms-net.aliyun.com/?host=qds108295464.my3w.com&port=1433&dbType=SQLServer&userName=qds108295464' target="_blank">
                                <i class="menu-icon fa fa-eyedropper bg-red"></i>
                                <div class="menu-info">
                                    <h4 class="control-sidebar-subheading">My DMS</h4>
                                    <p>qds108295464.my3w.com</p>
                                </div>
                            </a>
                        </li>
                    </ul>
                    <!-- /.control-sidebar-menu -->
                </div>
                <!-- /.tab-pane -->
                <!-- Stats tab content -->
                <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
                <!-- /.tab-pane -->
            </div>
        </aside>
        <!-- /.control-sidebar -->
        <!-- Add the sidebar's background. This div must be placed
       immediately after the control sidebar -->
        <div class="control-sidebar-bg"></div>
    </div>
    <!-- ./wrapper -->
    <!-- jQuery 3 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- jQuery UI 1.11.4 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery-ui/jquery-ui.min.js"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script>
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- Morris.js charts -->
    <%--<script src="../../Script/AdminLTE-2.4.2/bower_components/raphael/raphael.min.js"></script>
<script src="../../Script/AdminLTE-2.4.2/bower_components/morris.js/morris.min.js"></script>--%>
    <!-- Sparkline -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js"></script>
    <!-- jvectormap -->
    <script src="../../Script/AdminLTE-2.4.2/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
    <!-- jQuery Knob Chart -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery-knob/dist/jquery.knob.min.js"></script>
    <!-- daterangepicker -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/moment/min/moment.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
    <!-- datepicker -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="../../Script/AdminLTE-2.4.2/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <!-- Slimscroll -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/fastclick/lib/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="../../Script/AdminLTE-2.4.2/dist/js/adminlte.min.js"></script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <%--<script src="../../Script/AdminLTE-2.4.2/dist/js/pages/dashboard.js"></script>--%>
    <!-- AdminLTE for demo purposes -->
    <script src="../../Script/AdminLTE-2.4.2/dist/js/demo.js"></script>
    <script type="text/javascript">
        $(window).on('load', function () {
            menuInit();
        });
        //加载菜单
        function menuInit() {
            var meanhtml = '<%=strJson%>';
            $(".sidebar-menu").append(meanhtml);
            //绑定菜单点击事件
            $(".sidebar-menu li ul li a").click(function () {
                mainMenuClickFunc(this);
            })
            //默认点击首页
            loadwel(1000, "WelcomePage.aspx");
        }
        //菜单点击之后，加载页面（切换效果）
        function mainMenuClickFunc(param) {
            var moid = $(param).attr("menu-moid");
            var text = $(param).attr("menu-text");
            var controller = $(param).attr("menu-controller");
            $("#tabnav li").removeClass("active");
            var index = $("#tabnav li[menu-moid='" + moid + "']").length;
            if (index > 0) {
                $("#tabnav li[menu-moid='" + moid + "']").addClass("active");
                $(".content[menu-moid='" + moid + "']").show();
            } else {
                $(".content[menu-type='nav-tabs']").hide();
                $("#tabnav").append("<li class=\"active\" menu-moid=\"" + moid + "\"><a href=\"javascript:setPage(" + moid + ")\" >" + text + "</a></li>");
                $("#tabnav").parent().append("<section class=\"content\" menu-type=\"nav-tabs\" menu-moid=\"" + moid + "\"></section>");
                $.ajax({
                    url: controller,
                    success: function (d) {
                        var html = $(d);
                        $(".content[menu-moid='" + moid + "']").html(html);
                    }
                });
            }
        }
        //page跳转
        function setPage(moid) {
            $("#tabnav li").removeClass("active");
            $("#tabnav li[menu-moid='" + moid + "']").addClass("active");
            $(".content[menu-type='nav-tabs']").hide();
            $(".content[menu-moid='" + moid + "']").show();
        }
        //加载首页
        function loadwel(moid, controller) {
            $.ajax({
                url: controller,
                success: function (d) {
                    var html = $(d);
                    $(".content[menu-moid='" + moid + "']").html(html);
                }
            });
        }
    </script>
</body>
</html>
