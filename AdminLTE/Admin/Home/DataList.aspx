<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataList.aspx.cs" Inherits="Cases_baseprint_HomePage_DataList" validateRequest="false" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/css/bootstrap.min.css"/>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/font-awesome/css/font-awesome.min.css"/>
  <!-- Ionicons -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/Ionicons/css/ionicons.min.css"/>
  <!-- DataTables -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css"/>
  <!-- Theme style -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css"/>
  <!-- bootstrap datepicker -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
  <!-- AdminLTE Skins. Choose a skin from the css/skins
  folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/skins/_all-skins.min.css"/>
  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
  <style>
        table {
            table-layout: fixed;
        }
        td {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }
        tr:hover td {
            height: auto;
            white-space: normal;
        }
      .table-l {
          width:190px;
          margin-top:5px;
      }
      .table-p {
          float:right;
      }
      .table-s {
          margin-bottom:5px;
      }
      .table-label {
          width:auto;padding-top:6px;
      }
    </style>
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
                            <div class="col-lg-2 col-xs-5 table-s">   
                                  <label  class="col-xs control-label table-label">菜单名称</label>
                                  <input type="text" name="MeanName" data-i="1" class="form-control" placeholder="请输入菜单名称" />                             
                            </div>                            
                            <div class="col-lg-2 col-xs-5 table-s">
                                <label  class="col-xs control-label table-label">创建时间</label>
                                <input type="text" name="CreateTime" data-i="2" class="form-control pull-right" data-type="datepicker"  placeholder="请选择创建时间" />
                            </div>                          
                            <div class="col-lg-2 col-xs-5 table-s">
                                <label  class="col-xs control-label table-label">菜单样式</label>
                                <select name="MeanClass" data-i="3" class="form-control select2 select2-hidden-accessible" style="width: 100%;" tabindex="-1" aria-hidden="true" >
                                    <option selected="selected" value="" >请选择（菜单样式）</option>
                                    <option value="folder">folder</option>
                                    <option  value="dashboard">dashboard</option>
                                    <option  value="table">table</option>
                                </select>
                            </div>
                           <%-- 查询--%>
                            <div class="col-sm-1 table-p" style="margin-top:30px;">
                                <button type="button" class="btn btn-danger pull-right btn-block btn-primary" onclick="getJsonData('select')">查询</button>
                            </div>
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
                            <table id="example" class="table table-bordered table-hover">
                               
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
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.zh-CN.js"></script>
    <!-- page script -->
    <script> 
        var theadHtml = "";
        var columns = [];
        var listColumn = '<%=listColumn%>';
        $(document).ready(function () {
            setheadHtml();
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
                "columns": columns,
                "oLanguage": oLanguage,
                ajax: function (data, callback, settings) {                    
                    var values = $('#selectWhere').find('input,select').serializeArray();                    
                    //封装请求参数
                    var param = {};
                    param.gettype = "getDate";
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码;
                    param.values = JSON.stringify(values); ;
                    //ajax请求数据
                    $.ajax({
                        type: "GET",
                        url: "DataList.aspx",
                        cache: false,  //禁用缓存
                        data: param,  //传入组装的参数
                        dataType: "json",
                        //async: false,
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
        //获取url参数        
        function getQueryString(key) {
            var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
            var result = window.location.search.substr(1).match(reg);
            return result ? decodeURIComponent(result[2]) : null;
        }
        // 语言设置  
        var oLanguage = {  
            "sProcessing": "处理中...",
            "sLengthMenu": "每页显示 _MENU_ 条记录",
            "sZeroRecords": "抱歉， 没有找到",
            "sInfo": "从 _START_ 到 _END_ /共 _TOTAL_ 条数据",
            "sInfoEmpty": "",
            "sInfoFiltered": "(从 _MAX_ 条数据中检索)",
            "sZeroRecords": "没有检索到数据",
            "sSearch": "检索:",
            "oPaginate": {
                "sFirst": "首页",
                "sPrevious": "前一页",
                "sNext": "后一页",
                "sLast": "尾页"
            }
        }
        //设置tablethead or column
        function setheadHtml() {
            var list = listColumn.split(',');
            if (list.length > 0) {
                var theadHtml = "<thead><tr>";
                for (var i = 0; i < list.length; i++) {
                    var listc = list[i].split('|');
                    columns[i] = { "data": listc[0] };
                    theadHtml += '<th>' + listc[1] + '</th>';
                }
                theadHtml += "</tr></thead>";
                $("#example").append(theadHtml);
            }                              
        }
    </script>
</body>
</html>
