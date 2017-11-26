<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setList.aspx.cs" Inherits="AdminLTE.Admin.Temp.setList" %>

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
            <div class="row">
                <table id="tableInfo" class="table" style="margin:0px;margin-top:-3px;border-left:1px solid #ddd;border-right:1px solid #ddd;">
                    <tbody>
                        <tr>
                            <td>
                                <h4 style="margin: 0px;">
                                    <button type="button" title="数据设置" class="btn btn-warning" onclick="showTableInfoHtml()">数据设置</button>
                                    <button type="button" title="显示设置" class="btn btn-warning" onclick="showTableInfoHtml1()">显示设置</button>
                                    &nbsp;<span class="label label-success"><%=tableInfo.Rows[0]["title"].ToString() %></span>
                                    <span class="label label-success"><%=tableInfo.Rows[0]["FileName"].ToString() %></span>
                                    <span class="label label-success"><%=tableInfo.Rows[0]["TableName"].ToString() %></span></h4>
                            </td>
                            <td>
                                <select name="choice" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["choice"].ToString()=="1"?"selected='selected'":"" %> value="1">有选择</option>
                                    <option <%=tableInfo.Rows[0]["choice"].ToString()=="0"?"selected='selected'":"" %> value="0">无选择</option>
                                </select>
                            </td>
                            <td>
                                <select name="insert" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["insert"].ToString()=="1"?"selected='selected'":"" %> value="1">有添加</option>
                                    <option <%=tableInfo.Rows[0]["insert"].ToString()=="0"?"selected='selected'":"" %> value="0">无添加</option>
                                </select>
                            </td>
                            <td>
                                <select name="update" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["update"].ToString()=="1"?"selected='selected'":"" %> value="1">有修改</option>
                                    <option <%=tableInfo.Rows[0]["update"].ToString()=="0"?"selected='selected'":"" %>  value="0">无修改</option>
                                </select></td>
                            <td>
                                <select name="delete" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["delete"].ToString()=="1"?"selected='selected'":"" %>  value="1">有删除</option>
                                    <option <%=tableInfo.Rows[0]["delete"].ToString()=="0"?"selected='selected'":"" %>  value="0">无删除</option>
                                </select>
                            </td>
                            <td>
                                <select name="strwhere" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["strwhere"].ToString()=="1"?"selected='selected'":"" %>  value="1">有查询</option>
                                    <option <%=tableInfo.Rows[0]["strwhere"].ToString()=="0"?"selected='selected'":"" %>  value="0">无查询</option>
                                </select>
                            </td>
                             <td>
                                <select name="Plus" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableInfo.Rows[0]["Plus"].ToString()=="1"?"selected='selected'":"" %>  value="1">折叠</option>
                                    <option <%=tableInfo.Rows[0]["Plus"].ToString()=="0"?"selected='selected'":"" %>  value="0">展开</option>
                                </select>
                            </td>
                            <td style="border: none;">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info">自动排序</button>  
                                    <button type="button" class="btn btn-info" onclick="bntSaveClick()">保&nbsp;存</button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table id="example" class="table table-bordered table-hover" >
                    <thead>
                        <tr>
                            <th>字段称</th>
                            <th>显示名称</th>
                            <th>数据呈现</th>
                            <th>是否启用</th>
                            <th>查询条件</th>
                            <th>排序</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlTable" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </section>
        <div id="setTableInfo" class="hidden">
            <table class="table" style="border: 1px solid #ddd; text-align: right">
                <tbody>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="title" class="col-sm-3 control-label">标题：</label>
                                <div class="col-sm-9">
                                    <input type="text" name="title" class="form-control" placeholder="标题" value="<%=tableInfo.Rows[0]["title"].ToString() %>" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="FileName" class="col-sm-3 control-label">页面名称：</label>
                                <div class="col-sm-9">
                                    <input type="text" name="FileName" class="form-control" placeholder="页面名称" value="<%=tableInfo.Rows[0]["FileName"].ToString() %>" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="TableName" class="col-sm-3 control-label">操作表：</label>
                                <div class="col-sm-9">
                                    <input type="text" name="TableName" class="form-control" placeholder="操作表" value="<%=tableInfo.Rows[0]["TableName"].ToString() %>" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="Note" class="col-sm-3 control-label">备注：</label>
                                <div class="col-sm-9">
                                    <textarea name="Note" class="form-control" placeholder="备注" rows="3"><%=tableInfo.Rows[0]["Note"].ToString()%></textarea>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="text-center">
                        <td>
                            <button type="button" class="btn btn-success" onclick="bntSaveTableInfoOnclick()">保存</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="setTableInfo1" class="hidden">
            <table class="table" style="border: 1px solid #ddd; text-align: right">
                <tbody>
                    <tr>
                        <td>
                            <div class="form-group">
                                <label for="CountData" class="col-sm-3 control-label">聚合显示：</label>
                                <div class="col-sm-9">
                                    <textarea name="CountData" class="form-control" placeholder="备注" rows="3"><%=tableInfo.Rows[0]["CountData"].ToString()%></textarea>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr class="text-center">
                        <td>
                            <button type="button" class="btn btn-success" onclick="bntSaveTableInfoOnclick1()">保存</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
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
    <script src="../../Script/js/cyfs.js"></script>
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <script> 
        $(function () {
            $("select[name=SelectType]").change(function () {
                if ($(this).val() > 0) {
                    $(this).parent().next("a").removeClass("hidden")
                }
                else
                    $(this).parent().next("a").addClass("hidden");

            })         
        })
        function bntSaveClick() {
            loadding('正在保存，请稍等...');
            var values = $("#example").find("input,select").serializeArray();
            var tableInfo = $("#tableInfo").find("select").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "setData";
            param.values = JSON.stringify(values);
            param.tableguid = tableguid;
            param.tableInfo = JSON.stringify(tableInfo);   
            $.ajax({
                type: "post",
                url: pageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "text",
                success: function (result) {
                    if (result == "True") {
                        layer.msg('操作成功！', {
                            icon: 1, time: 1500, end: function () {
                                location.reload();
                            }
                        });
                    }
                }
            });
        }
        function bntSaveTableInfoOnclick() {
            loadding('正在保存，请稍等...');
            var settableinfo = $(".layui-layer-content").find("input,textarea").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "setTableData";
            param.tableguid = tableguid;
            param.settableinfo = JSON.stringify(settableinfo);
            $.ajax({
                type: "post",
                url: pageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "text",
                success: function (result) {
                    if (result == "True") {
                        layer.msg('操作成功！', {
                            icon: 1, time: 1500, end: function () {
                                location.reload();
                            }
                        });
                    }
                }
            });
        }
        function bntSaveTableInfoOnclick1() {
            loadding('正在保存，请稍等...');
            var settableinfo = $(".layui-layer-content").find("input,textarea").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "setTableData";
            param.tableguid = tableguid;
            param.settableinfo = JSON.stringify(settableinfo);
            $.ajax({
                type: "post",
                url: pageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "text",
                success: function (result) {
                    if (result == "True") {
                        layer.msg('操作成功！', {
                            icon: 1, time: 1500, end: function () {
                                location.reload();
                            }
                        });
                    }
                }
            });
        }
        function showTableInfoHtml() {
            var showHtml = $("#setTableInfo").html();
            //页面层
            layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['620px', '370px'], //宽高
                content: showHtml
            });
        }
        function showTableInfoHtml1() {
            var showHtml = $("#setTableInfo1").html();
            //页面层
            layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['620px', '370px'], //宽高
                content: showHtml
            });
        }
        var showData;
        var showIndex = 0;
        function setSelectData(obj) {
            showData = $(obj).parent().find("[name=SelectData]");
            var values = $(showData).val();
            var showHtml = "<div class=\"form-group text-center\">";
            showHtml += "<textarea name=\"selectData\" class=\"form-control\" placeholder=\"查询条件\" rows=\"6\">" + values + "</textarea><button type=\"button\" class=\"btn btn-success\" onclick=\"parentFunSetSelectData()\">保存</button></div>";
            //页面层
            showIndex = layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['420px', '240px'], //宽高
                content: showHtml
            });
        }
        function parentFunSetSelectData()
        {
            var values = $(".layui-layer textarea").val();
            $(showData).val(values);
            layer.close(showIndex);
        }
        function setFieldData(obj) {
            showData = $(obj).parent().find("[name=FieldData]");
            var values = $(showData).val();
            var showHtml = "<div class=\"form-group text-center\">";
            showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"6\">" + values + "</textarea><button type=\"button\" class=\"btn btn-success\" onclick=\"parentFunSetSelectData1()\">保存</button></div>";
            //页面层
            showIndex = layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['420px', '240px'], //宽高
                content: showHtml
            });
        }
        function parentFunSetSelectData1() {
            var values = $(".layui-layer textarea").val();
            $(showData).val(values);
            layer.close(showIndex);
        }
    </script>
</body>
</html>
