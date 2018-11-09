<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetList.aspx.cs" Inherits="AdminLTE.Admin.SetList" ValidateRequest="false" %>

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
    <!-- Google Font /UplaodImg/my.jpg-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body>
    <form id="FromPage">
        <section class="content">
            <div class="row">
                <table id="tableInfo" class="table" style="margin: 0px; margin-top: -3px; border-left: 1px solid #ddd; border-right: 1px solid #ddd;">
                    <tbody>
                        <tr>
                            <td>
                                <h4 style="margin: 5px;">
                                    <a href="javasrcpt:void(0)"><span class="label label-warning" onclick="showTableInfoHtml()"><%=TableModel.Title %><%=TableModel.FileName %></span></a>
                                    <a href="javasrcpt:void(0)"><span class="label label-primary" onclick="showTableInfoHtmlSum()">显示设置</span></a>
                                    <a href="javasrcpt:void(0)"><span class="label label-primary" onclick="showFragmentCodeHtml()">片段代码</span></a>
                                    <a href="javascript:PagePreview('<%=TableModel.Title %>','/Page/<%=TableModel.FileName %>.html','<%=TableModel.TableID %>')">
                                    <span class="label label-primary">页面预览</span></a>
                                </h4>
                            </td>
                            <td>
                                <select name="IsChoice" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsChoice==1?"selected='selected'":"" %> value="1">有选择</option>
                                    <option <%=TableModel.IsChoice==0?"selected='selected'":"" %> value="0">无选择</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsInsert" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsInsert==1?"selected='selected'":"" %> value="1">有添加</option>
                                    <option <%=TableModel.IsInsert==0?"selected='selected'":"" %> value="0">无添加</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsUpdate" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsUpdate==1?"selected='selected'":"" %> value="1">有修改</option>
                                    <option <%=TableModel.IsUpdate==0?"selected='selected'":"" %> value="0">无修改</option>
                                </select></td>
                            <td>
                                <select name="IsDelete" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsDelete==1?"selected='selected'":"" %> value="1">有删除</option>
                                    <option <%=TableModel.IsDelete==0?"selected='selected'":"" %> value="0">无删除</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsWhere" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsWhere==1?"selected='selected'":"" %> value="1">有查询</option>
                                    <option <%=TableModel.IsWhere==0?"selected='selected'":"" %> value="0">无查询</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsPlus" class="form-control select2 select2-hidden-accessible">
                                    <option <%=TableModel.IsPlus==1?"selected='selected'":"" %> value="1">折叠</option>
                                    <option <%=TableModel.IsPlus==0?"selected='selected'":"" %> value="0">展开</option>
                                </select>
                            </td>
                            <td style="width: 240px; border: none;">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info" onclick="bntOrderClick()">自动排序</button>
                                    <button type="button" class="btn btn-info" onclick="showMoreButtonsHtml()">更多按钮</button>
                                    <button type="button" class="btn btn-info" onclick="bntSaveClick(this)">保&nbsp;存</button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table id="example" class="table table-bordered table-hover">
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
                        <%=GetSetListHtml(TableFielModelList) %>
                    </tbody>
                </table>
            </div>
        </section>
        <div id="setTableInfo" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="form-group">
                    <label for="title" class="control-label">标题</label>
                    <input type="text" name="Title" class="form-control" placeholder="标题" value="<%=TableModel.Title %>" />
                </div>
                <div class="form-group">
                    <label for="FileName" class="control-label">页面名称</label>
                    <input type="text" name="FileName" class="form-control" placeholder="页面名称" value="<%=TableModel.FileName %>" />
                </div>
                <div class="form-group">
                    <label for="TableType" class="control-label">数据类型</label>
                    <select class="form-control" name="TableType"> 
                        <option <%=TableModel.TableType==1?"selected='selected'":"" %> value="1">数据库表</option>
                        <option <%=TableModel.TableType==2?"selected='selected'":"" %> value="2">XML数据表</option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="TableName" class="control-label">数据对象</label>
                    <textarea name="TableName" class="form-control" placeholder="数据对象" rows="1"><%=TableModel.TableName%></textarea>
                </div>
                <div class="form-group">
                    <label for="SQL" class="control-label">数据集合</label>
                    <textarea name="SQL" class="form-control" placeholder="SQL" rows="3"><%=TableModel.SQL%></textarea>
                </div>
                <div class="form-group">
                    <label for="Note" class="control-label">备注</label>
                    <textarea name="Note" class="form-control" placeholder="备注" rows="2"><%=TableModel.Note%></textarea>
                </div>
                <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclick()">保存</button>
            </div>
        </div>
        <div id="setTableInfoSum" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="text-center">
                    <p style="margin-top: 3px;"><span style="color: blue;">聚合显示例子(说明)</span></p>
                    <div class="text-left">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <td>
                                    <label class="radio-inline">JSON数据：[{"type":"COUNT","key":"GUID","title":"数据总条数："}]</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="radio-inline">SQL表达式：[{"type":"SQL","key":"select sg(1)","title":"数据总条数："}]</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="radio-inline">单引号属于特殊字符，sg(条件)='条件' 必须是JSON数据格式</label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <textarea name="PredefinedSQL" class="form-control" placeholder="聚合显示" rows="4"><%=TableModel.PredefinedSQL%></textarea>
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclickSum()" style="margin-top: 5px;">保存</button>
                </div>
            </div>
        </div>
        <div id="FragmentCode" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="text-center">
                    <p style="margin-top: 3px;"><span style="color: blue;">顶部片段代码</span></p>
                    <textarea name="TopHead" class="form-control" placeholder="顶部片段代码" rows="10"></textarea>
                     <p style="margin-top: 3px;"><span style="color: blue;">底部片段HTML</span></p>
                    <textarea name="BottomHtml" class="form-control" placeholder="底部片段HTML" rows="10"></textarea>
                    <p style="margin-top: 3px;"><span style="color: blue;">底部片段代码</span></p>
                    <textarea name="BottomScript" class="form-control" placeholder="底部片段代码" rows="10"></textarea>
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoFragmentCodeOnclick()" style="margin-top: 5px;">保存</button>
                </div>
            </div>
        </div>        
        <div id="MoreButtons" class="text-left hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="form-group col-sm-6" style="margin-bottom: 5px;">
                    <label for="BntName" class="control-label">按钮名称</label>
                    <input type="text" name="BntName" class="form-control" placeholder="按钮名称" value="" />
                </div>
                <div class="form-group col-sm-6" style="margin-bottom: 5px;">
                    <label for="BntAction" class="control-label">执行动作</label>
                    <select class="form-control" name="BntAction">
                        <option value="1">弹层（页面）</option>
                        <option value="2">新开（页面）</option>
                        <option value="3">脚本</option>
                        <option value="4">SQL</option>
                    </select>
                </div>
                <div class="form-group col-sm-12" style="margin-bottom: 5px;">
                    <label for="ConfirmText" class="control-label">询问框内容</label>
                    <input type="text" name="ConfirmText" class="form-control" placeholder="询问框内容" />
                </div>
                <div class="form-group col-sm-12" style="margin-bottom: 5px;">
                    <label for="BntActionContent" class="control-label">执行内容</label>
                    <textarea name="BntActionContent" class="form-control" placeholder="执行内容" rows="3"></textarea>
                </div>
                <div class="form-group col-sm-6" style="margin-bottom: 5px;">
                    <label for="Widths" class="control-label">弹层宽度</label>
                    <input type="text" name="Widths" class="form-control" placeholder="弹层宽度" />
                </div>
                <div class="form-group col-sm-6" style="margin-bottom: 5px;">
                    <label for="Heigths" class="control-label">弹层高度</label>
                    <input type="text" name="Heigths" class="form-control" placeholder="弹层高度" />
                </div>
                <div class="form-group col-sm-6">
                    <label for="BntActionCondition" class="control-label" style="margin-top: 5px;">执行条件</label>
                    <select class="form-control">
                        <option value="1">执行</option>
                        <option value="2">动态（前台判断）</option>
                        <option value="3">动态（后台判断）</option>
                    </select>
                </div>
                <div class="form-group col-sm-6">
                    <label for="BntIsEnable" class="control-label">是否启用</label>
                    <select class="form-control" name="BntIsEnable">
                        <option value="1">启用</option>
                        <option value="0">禁用</option>
                    </select>
                </div>
                 <div class="form-group col-sm-12" style="margin-bottom: 5px;">                  
                    <textarea name="BntActionCondition" class="form-control" placeholder="执行条件" rows="3"></textarea>
                </div>
                <div class="col-sm-12">
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclick()">保存</button>
                </div>
            </div>
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
    <script id="TopHeadModel" type="text/html"><%=TableBll.GetFragmentCodeModel("TopHead")%></script>
    <script id="BottomHtmlModel" type="text/html"><%=TableBll.GetFragmentCodeModel("BottomHtml")%></script>
    <script id="BottomScriptModel" type="text/html"><%=TableBll.GetFragmentCodeModel("BottomScript")%></script>
    <script> 
        $(function () {
             $(document).bind("contextmenu", function (e) {
                top.$(".contextmenu").hide();
                return false;
            }); 
            $("select[name=SelectType]").change(function () {
                if ($(this).val() == 2 || $(this).val() == 4) {
                    $(this).parent().next("a").removeClass("hidden")
                }
                else
                    $(this).parent().next("a").addClass("hidden");

            });
            $("select[name = FieldDataType]").change(function () {
                if ($(this).val() > 1)
                    $(this).parent().next("a").removeClass("hidden")
                else
                    $(this).parent().next("a").addClass("hidden");
            });
        })
        function bntSaveClick(obj) {
            loadding('正在保存，请稍等...', obj);
            var values = $("#example").find("input,select").serializeArray();
            var tableInfo = $("#tableInfo").find("select").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "SetData";
            param.values = JSON.stringify(values);
            param.tableInfo = JSON.stringify(tableInfo);
            $.ajax({
                type: "post",
                url: GetPageName(),
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
                    } else {
                        loadClose();
                        layer.msg('操作失败！', {
                            icon: 2, time: 1500, end: function () {
                            }
                        });
                    }
                }
            });
        }
        function bntSaveTableInfoFragmentCodeOnclick() {
            loadding('正在保存，请稍等...');
            var settableinfo = $(".layui-layer-content").find("input,textarea").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "SetFragmentCode";
            param.settableinfo = JSON.stringify(settableinfo);
            $.ajax({
                type: "post",
                url: GetPageName(),
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
                    } else {
                        loadClose();
                        layer.msg('操作失败！', {
                            icon: 2, time: 1500, end: function () {
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
            param.gettype = "SetTableData";
            param.settableinfo = JSON.stringify(settableinfo);
            $.ajax({
                type: "post",
                url: GetPageName(),
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
                    } else {
                        loadClose();
                        layer.msg('操作失败！', {
                            icon: 2, time: 1500, end: function () {
                            }
                        });
                    }
                }
            });
        }
        function bntSaveTableInfoOnclickSum() {
            loadding('正在保存，请稍等...');
            var settableinfo = $(".layui-layer-content").find("input,textarea").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "SetTableData";
            param.settableinfo = JSON.stringify(settableinfo);
            $.ajax({
                type: "post",
                url: GetPageName(),
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
        function showMoreButtonsHtml() {
            var showHtml = $("#MoreButtons").html();
            //页面层
            layer.open({
                type: 1,
                title: '更多按钮',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '552px'], //宽高
                content: showHtml,
                offset: ['45px']
            });
        }
        function showFragmentCodeHtml() {
            var showHtml = $("#FragmentCode").html();
            //页面层
            layer.open({
                type: 1,
                title: '更多按钮',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '645px'], //宽高
                content: showHtml,
                offset: ['45px'],
                success: function () {
                    $(".layui-layer [name=TopHead]").text($("#TopHeadModel").html());
                    $(".layui-layer [name=BottomHtml]").text($("#BottomHtmlModel").html());
                     $(".layui-layer [name=BottomScript]").text($("#BottomScriptModel").html());
                }
            });
        }
        function showTableInfoHtml() {
            var showHtml = $("#setTableInfo").html();
            //页面层
            layer.open({
                type: 1,
                title: '数据设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '600px'], //宽高
                content: showHtml,
                offset: ['45px']
            });
        }
        function showTableInfoHtmlSum() {
            var showHtml = $("#setTableInfoSum").html();
            //页面层
            layer.open({
                type: 1,
                title: '显示设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '368px'], //宽高
                content: showHtml,
                offset: ['45px']
            });
        }
        var showData;
        var showIndex = 0;
        function setSelectData(obj) {
            showData = $(obj).parent().find("[name=SelectData]");
            var values = $(showData).val();
            var type = $(obj).parent().find("[name=SelectType]").val();
            var showHtml = "<div class=\"form-group text-center\" style=\"margin:5px;\">";
            if (type == 2) {
                showHtml += "<p><span style=\"color: blue;\">JSON数据：[{\"key\":\"启用\",\"value\":\"1\"},{\"key\":\"不启用\",value:\"0\"}]</span></p> ";
                showHtml += "<p><span style=\"color: blue;\">SQL表达式：[{\"key\":\"SQL\",\"value\":\"select sg(启用),1\"}]</span></p>";
                showHtml += "<p><span style=\"color: blue;\">单引号属于特殊字符，sg(启用)='启用'</span> <span style=\"color: red;\"> 必须是JSON数据格式</p>";
                showHtml += "<p><textarea class=\"form-control\" placeholder=\"查询条件\" rows=\"4\">" + values + "</textarea></p>";
            }
            else if (type == 4) {
                showHtml += "<div class=\"form-group text-left\">";
                showHtml += "<table class=\"table table-bordered table-hover\">";
                showHtml += "<tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"   value=\"yearM\" " + (values == "yearM" ? "checked" : "") + " >时间格式：2018-05</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"date\" " + (values == "date" ? "checked" : "") + ">日期格式：2018-05-22</label></td>";
                showHtml += "</tr><tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time1\" " + (values == "time1" ? "checked" : "") + " >时间格式：2018-05-22 15:33</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time2\" " + (values == "time2" ? "checked" : "") + " >时间格式：2018-05-22 15:33:36</label></td>";
                showHtml += "</tr></table>";
                showHtml += "</div>";
            }
            showHtml += "<button type=\"button\" class=\"btn btn-success btn-block\" style=\"margin-top:10px;\" onclick=\"parentFunSetSelectData()\">保　存</button></div>";
            //页面层
            showIndex = layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['540px', '328px'], //宽高
                content: showHtml
            });
        }
        function parentFunSetSelectData() {
            var values = $(".layui-layer textarea").val();
            $(showData).val(values);
            layer.close(showIndex);
        }
        function setFieldData(obj) {
            showData = $(obj).parent().find("[name=FieldData]");
            var FieldDataTypeValue = $(obj).parent().find("[name=FieldDataType]").val();
            var values = $(showData).val();
            var showHtml = "<div class=\"form-group text-center\" >";
            if (FieldDataTypeValue == "2") {
                showHtml += "<p><span style=\"color: blue;\">固定转换(一般例子)：</span>data == 1?\"启用\":\"禁用\"　　data：</span>最终显示的值</p><p><span style=\"color: blue;\">特殊例子：</span><span>\"<标签>\" + data + \"<标签>\"　　也可以是JQ语句&nbsp;alert(data);</span><p/><p> <span style=\"color: blue;\">row：</span>当前行所有字段的对象（row.[字段] 字段名必须一致）（不执行二次转换）</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"10\">" + values + "</textarea>";
            } else if (FieldDataTypeValue == "3") {
                showHtml += "<p><span style=\"color: blue;\">动态转换(例子)：</span>select [字段] from [表名] where  [字段] =sg(row.[列表所有的字段])</p><p> <span style=\"color: blue;\">&nbsp;row：</span>当前行所有字段的对象（row.[字段] 字段名必须一致）（异步请求转换）单引号统一使用sg(值)</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"11\">" + values + "</textarea>";
            }
            else if (FieldDataTypeValue == "4") {
                showHtml += "<div class=\"form-group text-left\">";
                showHtml += "<table class=\"table table-bordered table-hover\">";
                showHtml += "<tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"   value=\"yearM\" " + (values == "yearM" ? "checked" : "") + " >时间格式：2018-05</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"yearMzw\" " + (values == "yearMzw" ? "checked" : "") + ">日期格式：2018年05月</label></td>";
                showHtml += "</tr><tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"date\" " + (values == "date" ? "checked" : "") + ">日期格式：2018-05-22</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"datezw\" " + (values == "datezw" ? "checked" : "") + ">日期格式：2018年05月22日</label></td>";
                showHtml += "</tr><tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time1\" " + (values == "time1" ? "checked" : "") + " >时间格式：2018-05-22 15:33</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time1zw\" " + (values == "timezw" ? "checked" : "") + " >时间格式：2018年05月日 15时33分</label></td>";
                showHtml += "</tr><tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time2\" " + (values == "time2" ? "checked" : "") + " >时间格式：2018-05-22 15:33:36</label></td>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time2zw\" " + (values == "timezw" ? "checked" : "") + " >时间格式：2018年05月日 15时33分36秒</label></td>";
                showHtml += "</tr><tr>";
                showHtml += "<td><label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"img\" " + (values == "img" ? "checked" : "") + " >图片组</label></td>";
                showHtml += "</tr></table>";
                showHtml += "</div>";
            }
            showHtml += "<button type=\"button\" class=\"btn btn-success btn-block\" style=\"margin-top:10px;\" onclick=\"parentFunSetSelectDataZh()\">保　存</button></div>";
            //页面层
            showIndex = layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['720px', '420px'], //宽高
                content: showHtml
            });
        }
        function parentFunSetSelectDataZh() {
            var type = $(".layui-layer input[name=FieldData]").attr("type");
            var values = "";
            if (type == "radio")
                values = $(".layui-layer input[name=FieldData]:checked").val();
            else
                values = $(".layui-layer textarea[name=FieldData]").val();
            $(showData).val(values);
            layer.close(showIndex);
        }
        function bntOrderClick() {
            loadding('正在处理，请稍等...');
            var param = {};
            param.gettype = "SetOrder";
            $.ajax({
                type: "post",
                url: GetPageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "text",
                success: function (result) {
                    debugger;
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
        //页面预览
        function PagePreview(title, url, moid) {
            $(top.TopPagePreview(title, url, moid))
        }
    </script>
</body>
</html>
