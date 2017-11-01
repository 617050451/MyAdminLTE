<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MeanList.aspx.cs" Inherits="AdminLTE.Admin.Aspx.MeanList" validateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <link href="../../Script/js/cyfs.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <section class="content" style="margin-top: -13px;">
            <div class="box box-solid collapsed-box">
                <div class="box-header with-border">
                    <h3 class="box-title">高级查询</h3>
                    <div class="box-tools">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                            <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
                <div class="box box-danger">
                    <div class="box-body" id="selectWhere">
                         <asp:Literal ID="ltlStrWhere" runat="server" Text=""></asp:Literal> 
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary">
                        <%--                        <div class="box-header">
                            <h3 class="box-title">title</h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <div class="box-body">	
						    <asp:Literal ID="ltlbnt" runat="server" Text=""></asp:Literal>
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
    <script src="../../Script/js/cyfs.js"></script>
    <script> 
        var columnsJson = <%=columnsJson%>;
        $(document).ready(function () {
            getJsonData("getDate");
            //Date picker
            $('input[data-type=datepicker]').datepicker({
                language: 'zh-CN',
                autoclose: true,
                todayHighlight: true,
                format: 'yyyy-mm-dd'
            });
        });
        //获取数据
        var table;
        function getJsonData(type) {
            if (type == 'select') {
                table.fnClearTable(false);  //清空数据.fnClearTable();//清空数据
                table.fnDestroy(); //还原初始化了的datatable  
            }
            table = $('#example').dataTable({
                "dom": "t<'row'<'#id.col-xs-2 table-l'l><'#id.col-xs-3'i><'#id.col-xs-6 table-p'p>>r",
				"aoColumnDefs": [{ "bSortable": false, "aTargets": [0]}],
				"aaSorting": [[1, "asc"]],
                "lengthChange": true,
                "autoWidth": false,
                "aLengthMenu": [25, 50, 100, 200],
                //当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
                "pagingType": "full_numbers",//详细分页组，可以支持直接跳转到某页  
                "deferRender": true,
                "processing": true,  //隐藏加载提示,自行处理
                "serverSide": true,  //启用服务器端分页
                "searching": false,  //禁用原生搜索
                "orderMulti": true,  //启用多列排序
                "columns": columnsJson,
                "oLanguage": oLanguage,
                ajax: function (data, callback, settings) {
                    var values = $('#selectWhere').find('input,select').serializeArray();
                    //封装请求参数
                    var param = {};
                    param.gettype = "getDate";
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码;
                    param.values = JSON.stringify(values);
                    //ajax请求数据
                    $.ajax({
                        type: "GET",
                        url: pageName(),
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        async: false,
                        success: function (result) {
                            if (result != false) {
                                //setTimeout仅为测试延迟效果
                                setTimeout(function () {
                                    //封装返回数据
                                    var returnData = {};
                                    returnData.draw = data.draw;//这里直接自行返回了draw计数器,应该由后台返回
                                    returnData.recordsTotal = result.total;//返回数据全部记录
                                    returnData.recordsFiltered = result.total;//后台不实现过滤功能，每次查询均视作全部结果
                                    returnData.data = result.data;//返回的数据列表
                                    //调用DataTables提供的callback方法，代表数据已封装完成并传回DataTables进行渲染
                                    //此时的数据需确保正确无误，异常判断应在执行此回调前自行处理完毕
                                    callback(returnData);
                                }, 200);
                            }
                        }
                    });
                }
            });
        }
        //全选
        function OnCheckboxSelectAll(obj) {
            if ($(obj).text() == "全选") {
                $(obj).text("取消全选");
                $("input[name='checkboxGuid']").prop("checked", 'true');//全选 
            } else {
                $(obj).text("全选");
                $("input[name='checkboxGuid']").prop("checked", '');//取消全选 
            }
        }
    </script>
</body>
</html>
