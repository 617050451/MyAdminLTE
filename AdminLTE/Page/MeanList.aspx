<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MeanList.aspx.cs" Inherits="AdminLTE.Admin.Aspx.MeanList" validateRequest="false" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>菜单管理</title>
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
    <link href="../../Script/AdminLTE-2.4.2/dist/css/bootstrapValidator.min.css" rel="stylesheet" />
    <link href="../../Script/AdminLTE-2.4.2/dist/css/bootstrapValidator.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="content" style="margin-top: -13px;">
            <div data-resple="iswhere" class="box box-solid">
                <div class="box-header with-border">
                    <h3 class="box-title">高级查询</h3>
                    <div class="box-tools">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i data-resple="isplus" class=""></i>
                        </button>
                    </div>
                </div>
                <div class="box box-danger">
                    <div class="box-body" id="SelectWhereFrom">
                         <asp:Literal ID="ltlStrWhere" runat="server" Text=""></asp:Literal> 
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary">
                        <div class="box-body">	
                            <div id="ltlbnts"  class="pull-left" style="height:24px;" >
                                 <asp:Literal ID="ltlbnt" runat="server" Text=""></asp:Literal>
                            </div>
                            <div id="ltlSum" class="pull-right"  style="height:24px;">
                            </div>
                            <table id="example" class="table table-bordered table-hover">
                                <asp:Literal ID="ltlhead" runat="server" Text=""></asp:Literal> 
                            </table>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
            <!-- /.row -->
        </section>
        <asp:HiddenField ID="IsPlus" runat="server" />
        <asp:HiddenField ID="IsWhere" runat="server" />
        <asp:HiddenField ID="IsChoice" runat="server" />
        <asp:HiddenField ID="ColumnsJson" runat="server" />
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
    <!-- AdminLTE for demo purposes -->
    <script src="../../Script/AdminLTE-2.4.2/dist/js/demo.js"></script>
    <!-- bootstrap datepicker -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.zh-CN.js"></script>
    <!-- page script -->
    <script src="../../Script/AdminLTE-2.4.2/dist/js/bootstrapValidator.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/dist/js/bootstrapValidator.js"></script>
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
	<link href="../../Script/js/cyfs.css" rel="stylesheet" />
    <script src="../../Script/js/cyfs.js"></script>
</body>
</html>

