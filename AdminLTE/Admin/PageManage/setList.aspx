<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetList.aspx.cs" Inherits="AdminLTE.Admin.SetList" validateRequest="false" %>

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
    <form id="FromPage">
        <section class="content" style="margin-top: -13px;">
            <div class="row">
                <table id="tableInfo" class="table" style="margin:0px;margin-top:-3px;border-left:1px solid #ddd;border-right:1px solid #ddd;">
                    <tbody>
                        <tr>
                            <td>
                                <h4 style="margin: 0px;">
                                    <button type="button" title="数据设置" class="btn btn-warning" onclick="showTableInfoHtml()">数据设置</button>
                                    <button type="button" title="显示设置" class="btn btn-warning" onclick="showTableInfoHtmlSum()">显示设置</button>
                                    <span class="label label-success"><%=tableModel.TableModel.FileName %></span>
                                    <span class="label label-success"><%=tableModel.TableModel.TableName %></span>
                                    <a href="javascript:PagePreview('<%=tableModel.TableModel.Title %>','/Page/<%=tableModel.TableModel.FileName %>.aspx','<%=tableModel.TableModel.GUID %>')">
                                    <span class="label label-primary">页面预览</span></a>
                                </h4>
                            </td>
                            <td>
                                <select name="IsChoice" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsChoice==1?"selected='selected'":"" %> value="1">有选择</option>
                                    <option <%=tableModel.TableModel.IsChoice==0?"selected='selected'":"" %> value="0">无选择</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsInsert" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsInsert==1?"selected='selected'":"" %> value="1">有添加</option>
                                    <option <%=tableModel.TableModel.IsInsert==0?"selected='selected'":"" %> value="0">无添加</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsUpdate" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsUpdate==1?"selected='selected'":"" %> value="1">有修改</option>
                                    <option <%=tableModel.TableModel.IsUpdate==0?"selected='selected'":"" %>  value="0">无修改</option>
                                </select></td>
                            <td>
                                <select name="IsDelete" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsDelete==1?"selected='selected'":"" %>  value="1">有删除</option>
                                    <option <%=tableModel.TableModel.IsDelete==0?"selected='selected'":"" %>  value="0">无删除</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsWhere" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsWhere==1?"selected='selected'":"" %>  value="1">有查询</option>
                                    <option <%=tableModel.TableModel.IsWhere==0?"selected='selected'":"" %>  value="0">无查询</option>
                                </select>
                            </td>
                            <td>
                                <select name="IsPlus" class="form-control select2 select2-hidden-accessible">
                                    <option <%=tableModel.TableModel.IsPlus==1?"selected='selected'":"" %>  value="1">折叠</option>
                                    <option <%=tableModel.TableModel.IsPlus==0?"selected='selected'":"" %>  value="0">展开</option>
                                </select>
                            </td>
                            <td style="width:240px;border: none;">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info" onclick="bntOrderClick()">自动排序</button>  
                                    <button type="button" class="btn btn-info">更多按钮</button>  
                                    <button type="button" class="btn btn-info" onclick="bntSaveClick(this)">保&nbsp;存</button>
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
                        <%=GetSetListHtml(ItemGUID) %>
                    </tbody>
                </table>
            </div>
        </section>
        <div id="setTableInfo" class="hidden">
            <div class="col-sm-12" style="margin-top:5px;">
                <div class="form-group">
                    <label for="title" class="control-label">标题</label>
                    <input type="text" name="Title" class="form-control" placeholder="标题" value="<%=tableModel.TableModel.Title %>" />
                </div>
                <div class="form-group">
                    <label for="FileName" class="control-label">页面名称</label>
                    <input type="text" name="FileName" class="form-control" placeholder="页面名称" value="<%=tableModel.TableModel.FileName %>" />
                </div>
                <div class="form-group">
                    <label for="TableName" class="control-label">SQL</label>
                    <textarea name="SQL" class="form-control" placeholder="数据" rows="3"><%=tableModel.TableModel.SQL%></textarea>
                </div>
                <div class="form-group">
                    <label for="TableName" class="control-label">操作表</label>
                    <textarea name="TableName" class="form-control" placeholder="数据" rows="1"><%=tableModel.TableModel.TableName%></textarea>
                </div>
                <div class="form-group">
                    <label for="Note" class="control-label">备注</label>
                    <textarea name="Note" class="form-control" placeholder="备注" rows="3"><%=tableModel.TableModel.Note%></textarea>
                </div>
                <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclick()">保存</button>
            </div>
        </div>
        <div id="setTableInfoSum" class="hidden">
            <div class="form-group text-center">
                <p style="margin-top:3px;"><span style="color: blue;">聚合显示例子(一般例子)：</span>记录总条数：{count(guid)}（条）</p>
                <textarea name="CountData" class="form-control" placeholder="聚合显示" rows="10"><%=tableModel.TableModel.CountData%></textarea>
                <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclickSum()" style="margin-top:5px;">保存</button>
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
    <script> 
        $(function () {
            $("select[name=SelectType]").change(function () {
                if ($(this).val() > 0) {
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
            param.tableguid = tableguid;
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
            param.tableguid = tableguid;
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
        function bntSaveTableInfoOnclickSum() {
            loadding('正在保存，请稍等...');
            var settableinfo = $(".layui-layer-content").find("input,textarea").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "SetTableData";
            param.tableguid = tableguid;
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
        function showTableInfoHtml() {
            var showHtml = $("#setTableInfo").html();
            //页面层
            layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['780px', '545px'], //宽高
                content: showHtml
            });
        }
        function showTableInfoHtmlSum() {
            var showHtml = $("#setTableInfoSum").html();
            //页面层
            layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['620px', '355px'], //宽高
                content: showHtml
            });
        }
        var showData;
        var showIndex = 0;
        function setSelectData(obj) {
            showData = $(obj).parent().find("[name=SelectData]");
            var values = $(showData).val();
            var showHtml = "<div class=\"form-group text-center\" style=\"margin:5px;\">";
            showHtml += "<p><span style=\"color: blue;\">支持SQL表达式</p>";
            showHtml += "<textarea name=\"selectData\" class=\"form-control\" placeholder=\"查询条件\" rows=\"8\">" + values + "</textarea><button type=\"button\" style=\"margin-top:5px;\" class=\"btn btn-success btn-block\" onclick=\"parentFunSetSelectData()\">保存</button></div>";
            //页面层
            showIndex = layer.open({
                type: 1,
                title: '参数设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['540px', '310px'], //宽高
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
            var FieldDataTypeValue = $(obj).parent().find("[name=FieldDataType]").val();
            var values = $(showData).val();
            var showHtml = "<div class=\"form-group text-center\" >";
            if (FieldDataTypeValue == "2") {
                showHtml += "<p><span style=\"color: blue;\">固定前台转换(一般例子)：</span>data == 1?\"启用\":\"禁用\"　　data：</span>最终显示的值</p><p><span style=\"color: blue;\">特殊例子：</span><span>\"<标签>\" + data + \"<标签>\"　　也可以是JQ语句&nbsp;alert(data);</span><p/><p> <span style=\"color: blue;\">row：</span>当前行所有字段的对象（row.[字段] 字段名必须一致）（不执行二次转换）</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"10\">" + values + "</textarea>";
            } else if (FieldDataTypeValue == "3") {
                showHtml += "<p><span style=\"color: blue;\">动态前台转换(例子)：</span>select [字段] from [表名] where  [字段] =\"row.[列表所有的字段]\"</p><p> <span style=\"color: blue;\">&nbsp;row：</span>当前行所有字段的对象（row.[字段] 字段名必须一致）（异步请求转换）</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"11\">" + values + "</textarea>";
            }
            else if (FieldDataTypeValue == "4") {
                showHtml += "<div class=\"form-group text-left\" >";
                showHtml += "<label class=\"radio-inline\" style=\"margin-left: 10px;\"><input type=\"radio\" name=\"FieldData\"   value=\"yearM\" " + (values == "yearM" ? "checked" : "") + " >时间格式：2018-05</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"yearMzw\" " + (values == "yearMzw" ? "checked" : "") + ">日期格式：2018年05月</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"date\" " + (values == "date" ? "checked" : "") + ">日期格式：2018-05-22</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\"   name=\"FieldData\"  value=\"datezw\" " + (values == "datezw" ? "checked" : "") + ">日期格式：2018年05月22日</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time1\" " + (values == "time" ? "checked" : "") + " >时间格式：2018-05-22 15:33</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time1zw\" " + (values == "timezw" ? "checked" : "") + " >时间格式：2018年05月日 15时33分</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time2\" " + (values == "time" ? "checked" : "") + " >时间格式：2018-05-22 15:33:36</label>";
                showHtml += "<label class=\"radio-inline\"><input type=\"radio\" name=\"FieldData\"    value=\"time2zw\" " + (values == "timezw" ? "checked" : "") + " >时间格式：2018年05月日 15时33分36秒</label>";
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
