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
        <section class="content" style="padding-top: 0px;">
            <div class="row">
                <table id="tableInfo" class="table" style="margin: 0px; border: 1px solid #f4f4f4; border-top: none;">
                    <tbody>
                        <tr>
                            <td style="border-top: none;">
                                <h4 style="padding: 0; margin: 0;">
                                    <a href="javasrcpt:void(0)"><span class="label label-warning" onclick="showTableInfoHtml()" style="margin-top: 7px; display: inline-block; line-height: 1.2;"><%=TableModel.Title %><%=TableModel.FileName %></span></a>
                                    <a href="javasrcpt:void(0)"><span class="label label-primary" onclick="showTableInfoHtmlSum()" style="margin-top: 7px; display: inline-block; line-height: 1.2;">显示设置</span></a>
                                    <a href="javasrcpt:void(0)"><span class="label label-primary" onclick="showFragmentCodeHtml()" style="margin-top: 7px; display: inline-block; line-height: 1.2;">片段代码</span></a>
                                    <a href="javascript:PagePreview('<%=TableModel.Title %>','/Page/<%=TableModel.FileName %>.html','<%=TableModel.TableID %>')">
                                        <span class="label label-primary" style="margin-top: 7px; display: inline-block; line-height: 1.2;">页面预览</span></a>
                                </h4>
                            </td>
                            <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsChoice" value="1" <%=TableModel.IsChoice==1?"checked='checked'":"" %> />
                                        选择
                                    </label>
                                </div>
                            </td>
                            <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsInsert" value="1" <%=TableModel.IsInsert==1?"checked='checked'":"" %> />
                                        添加
                                    </label>
                                </div>
                            </td>
                          <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsUpdate" value="1" <%=TableModel.IsUpdate==1?"checked='checked'":"" %> />
                                        修改
                                    </label>
                                </div>
                            </td>
                            <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsDelete" value="1" <%=TableModel.IsDelete==1?"checked='checked'":"" %> />
                                        删除
                                    </label>
                                </div>
                            </td>
                           <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsWhere" value="1" <%=TableModel.IsWhere==1?"checked='checked'":"" %> />
                                        查询
                                    </label>
                                </div>
                            </td>
                           <td style="padding:8px 0;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" name="IsPlus" value="1" <%=TableModel.IsPlus==1?"checked='checked'":"" %> />
                                        折叠
                                    </label>
                                </div>
                            </td>
                            <td style="text-align: right;">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info" onclick="bntOrderClick()">自动排序</button>
                                    <%--                                    <button type="button" class="btn btn-info" onclick="showMoreButtonsHtml()">更多按钮</button>--%>
                                    <button type="button" class="btn btn-info" onclick="bntSaveClick(this)">保&nbsp;存</button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table id="example" class="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th style="width: 100px;">字段称</th>
                            <th>显示名称</th>
                            <th>数据处理</th>
                            <th>查询条件</th>
                            <th>是否启用</th>
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
                <div class="form-group col-sm-6">
                    <label for="title" class="control-label">标题</label>
                    <input type="text" name="Title" class="form-control" placeholder="标题" value="<%=TableModel.Title %>" />
                </div>
                <div class="form-group col-sm-6">
                    <label for="FileName" class="control-label">页面名称</label>
                    <input type="text" name="FileName" class="form-control" placeholder="页面名称" value="<%=TableModel.FileName %>" />
                </div>
                <div class="form-group col-sm-6">
                    <label for="TableType" class="control-label">数据类型</label>
                    <select class="form-control" name="TableType">
                        <option <%=TableModel.TableType==1?"selected='selected'":"" %> value="1">数据库表</option>
                        <option <%=TableModel.TableType==2?"selected='selected'":"" %> value="2">XML数据表</option>
                    </select>
                </div>
                <div class="form-group col-sm-6">
                    <label for="TableName" class="control-label">数据对象</label>
                    <textarea name="TableName" class="form-control" placeholder="数据对象" rows="1"><%=TableModel.TableName%></textarea>
                </div>
                <div class="form-group col-sm-12">
                    <label for="SQL" class="control-label">数据集合</label>
                    <textarea name="SQL" class="form-control" placeholder="SQL" rows="3"><%=TableModel.SQL%></textarea>
                </div>
                <div class="form-group col-sm-12">
                    <label for="Note" class="control-label">备注</label>
                    <textarea name="Note" class="form-control" placeholder="备注" rows="3"><%=TableModel.Note%></textarea>
                </div>
                <div class="col-sm-12">
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclick()">保存</button>
                </div>
            </div>
        </div>
        <div id="setTableInfoSum" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="text-center">
                    <div class="text-left">
                        <label class="inline">JSON数据：[{"type":"COUNT","key":"GUID","title":"数据总条数："}]<br />
                        </label>
                        <label class="inline">SQL表达式：[{"type":"SQL","key":"select sg(1)","title":"数据总条数："}]<br />
                        </label>
                        <label class="inline">单引号属于特殊字符，sg(条件)='条件' 必须是JSON数据格式</label>
                    </div>
                    <p style="margin-top: 3px;"><span style="color: blue;">聚合显示</span></p>
                    <textarea name="PredefinedSQL" class="form-control" placeholder="聚合显示" rows="4"><%=TableModel.PredefinedSQL%></textarea>
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclickSum()" style="margin-top: 5px;">保存</button>
                </div>
            </div>
        </div>
        <div id="FragmentCode" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="text-center">
                    <p style="margin-top: 3px;"><span style="color: blue;">顶部片段代码</span></p>
                    <textarea name="TopHead" class="form-control" placeholder="顶部片段代码" rows="8"></textarea>
                    <p style="margin-top: 3px;"><span style="color: blue;">底部片段HTML代码</span></p>
                    <textarea name="BottomHtml" class="form-control" placeholder="底部片段代码" rows="8"></textarea>
                    <p style="margin-top: 3px;"><span style="color: blue;">底部片段代码</span></p>
                    <textarea name="BottomScript" class="form-control" placeholder="底部片段代码" rows="8"></textarea>
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoFragmentCodeOnclick()" style="margin-top: 5px; margin-bottom: 5px;">保存</button>
                </div>
            </div>
        </div>
        <div id="MoreButtons" class="text-left hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="form-group col-sm-6">
                    <label for="Name" class="control-label">按钮名称</label>
                    <input type="text" name="BntName" class="form-control" placeholder="按钮名称" value="" />
                </div>
                <div class="form-group col-sm-3">
                    <label for="ButtonType" class="control-label">按钮类型</label>
                    <select class="form-control" name="ButtonType">
                        <option value="1">常用按钮</option>
                        <option value="0">自定义</option>
                    </select>
                </div>
                <div class="form-group col-sm-3">
                    <label for="StatusID" class="control-label">按钮状态</label>
                    <select class="form-control" name="StatusID">
                        <option value="1">启用</option>
                        <option value="0">禁用</option>
                    </select>
                </div>
                <div class="form-group col-sm-12">
                    <label for="ButtonCss" class="control-label">按钮样式</label>
                    <input type="text" name="ButtonCss" class="form-control" placeholder="按钮样式" />
                </div>
                <div class="form-group col-sm-4" style="margin-bottom: 5px;">
                    <label for="ActionType" class="control-label">执行动作</label>
                    <select class="form-control" name="ActionType">
                        <option value="1">弹层（页面）</option>
                        <option value="2">新开（页面）</option>
                        <option value="3">脚本</option>
                        <option value="4">SQL</option>
                    </select>
                </div>
                <div class="form-group col-sm-8">
                    <label for="ActionBeforeContent" class="control-label">执行前内容</label>
                    <input type="text" name="ActionBeforeContent" class="form-control" placeholder="执行前内容" />
                </div>
                <div class="form-group col-sm-12">
                    <label for="ActionContent" class="control-label">执行内容</label>
                    <textarea name="ActionContent" class="form-control" placeholder="执行内容" rows="2"></textarea>
                </div>
                <div class="form-group col-sm-4">
                    <label for="ImplementType" class="control-label">执行类型</label>
                    <select class="form-control" name="ImplementType">
                        <option value="1">执行</option>
                        <option value="2">动态（前台判断）</option>
                        <option value="3">动态（后台判断）</option>
                        <option value="1">不执行</option>
                    </select>
                </div>
                <div class="form-group col-sm-8">
                    <textarea name="ImplementContent" class="form-control" placeholder="执行条件" rows="2" style="margin-top: 5px;"></textarea>
                </div>
                <div class="col-sm-12">
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveTableInfoOnclick()">保存</button>
                </div>
            </div>
        </div>
        <div id="setFieldOther" class="hidden">
            <div class="col-sm-12" style="margin-top: 5px;">
                <div class="form-group col-sm-6">
                    <label for="TextAlign" class="control-label">对齐方式</label><br />
                    <label class="radio-inline">
                        <input type="radio" checked="checked" name="TextAlign" value="left" />左对齐</label>
                    <label class="radio-inline">
                        <input type="radio" name="TextAlign" value="center" />居中对齐</label>
                    <label class="radio-inline">
                        <input type="radio" name="TextAlign" value="right" />右对齐</label>
                </div>
                <div class="form-group col-sm-6">
                    <label for="Width" class="control-label">宽度</label>
                    <input type="text" name="Width" class="form-control" placeholder="宽度" value="" />
                </div>
                <div class="form-group col-sm-12">
                    <label for="OtherCSS" class="control-label">其他样式</label>
                    <textarea name="OtherCSS" class="form-control" placeholder="其他样式" rows="3"></textarea>
                </div>
                <div class="col-sm-12">
                    <button type="button" class="btn btn-success btn-block" onclick="bntSaveFieldOtherOnclick(this)">保存</button>
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
    <script src="../../Script/js/DataPageHandle.js"></script>
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <span id="TopHeadModel" class="hide"><%=TableBll.GetFragmentCodeModel("TopHead")%></span>
    <span id="BottomHtmlModel" class="hide"><%=TableBll.GetFragmentCodeModel("BottomHtml")%></span>
    <span id="BottomScriptModel" class="hide"><%=TableBll.GetFragmentCodeModel("BottomScript")%></span>
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
        //
        var otherG;
        function setFieldOther(othis, obj) {
            otherG = othis;
            var showHtml = $("#setFieldOther").html();
            //页面层
            showIndex = layer.open({
                id: obj,
                type: 1,
                title: '其他设置',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '425px'], //宽高
                content: showHtml,
                offset: ['45px'],
                success: function () {
                    var TextAlign = $(otherG).nextAll("[name=TextAlign]").val();
                    var Width = $(otherG).nextAll("[name=Width]").val();
                    var OtherCSS = $(otherG).nextAll("[name=OtherCSS]").val();
                    if (TextAlign.length > 0)
                        $(".layui-layer #" + obj).find("[name=TextAlign][value=" + TextAlign + "]").prop("checked", "checked");
                    $(".layui-layer #" + obj).find("[name=Width]").val(Width);
                    $(".layui-layer #" + obj).find("[name=OtherCSS]").val(OtherCSS);
                }
            });
        }
        function bntSaveFieldOtherOnclick(othis) {
            var TextAlign = $(othis).parent().siblings().find("[name=TextAlign]:checked").val();
            var Width = $(othis).parent().siblings().find("[name=Width]").val();
            var OtherCSS = $(othis).parent().siblings().find("[name=OtherCSS]").val();
            $(otherG).nextAll("[name=TextAlign]").val(TextAlign);
            $(otherG).nextAll("[name=Width]").val(Width);
            $(otherG).nextAll("[name=OtherCSS]").val(OtherCSS);
            layer.close(showIndex);
        }
        //
        function bntSaveClick(obj) {
            loadding('正在保存，请稍等...', obj);
            var values = $("#example").find("input,select").serializeArray();
            //var tableInfo = $("#tableInfo").find("input[type='checkbox']").serializeArray();
            var tableInfo = [];
            $("#tableInfo").find("input[type='checkbox']").each(function () {
                var name = $(this).prop("name");
                var value = 0;
                if ($(this).is(':checked'))
                    value = 1;
                tableInfo.push({ "name": name, "value": value });
            })
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
                area: ['680px', '495px'], //宽高
                content: showHtml,
                offset: ['45px']
            });
        }
        function showFragmentCodeHtml() {
            var showHtml = $("#FragmentCode").html();
            //页面层
            layer.open({
                type: 1,
                title: '片段代码',
                skin: 'layui-layer-rim', //加上边框
                area: ['680px', '545px'], //宽高
                content: showHtml,
                offset: ['45px'],
                success: function () {
                    $(".layui-layer [name=TopHead]").text($.unescapeHTML($("#TopHeadModel").html()));
                    $(".layui-layer [name=BottomHtml]").text($.unescapeHTML($("#BottomHtmlModel").html()));
                    $(".layui-layer [name=BottomScript]").text($.unescapeHTML($("#BottomScriptModel").html()));
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
                area: ['680px', '475px'], //宽高
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
                area: ['580px', '368px'], //宽高
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
            } else if (FieldDataTypeValue == "3" ) {
                showHtml += "<p><span style=\"color: blue;\">动态转换(例子) 数据表：</span>select [字段] from [表名] where  [字段] =sg(row.[列表所有的字段])</p><p><span style=\"color: blue;\">XML数据表：</span>文件名.xml_字段名_查询字段名(不填默认当前字段名)</p><p> <span style=\"color: blue;\">&nbsp;row：</span>当前行所有字段的对象（row.[字段] 字段名必须一致）（异步请求转换）单引号统一使用sg(值)</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"10\">" + values + "</textarea>";
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
            else if (FieldDataTypeValue == "5") {
                showHtml += "<p> <span style=\"color: blue;\">例子：&nbsp;\"< a href=\"javascript:test(\"+ data +\")\">查看详情 < /a >\"</p><p> <span style=\"color: blue;\">&nbsp;row：</span>自定显示 当前行所有字段的对象（row.[字段] 字段名必须一致）</p>";
                showHtml += "<textarea name=\"FieldData\" class=\"form-control\" placeholder=\"数据呈现\" rows=\"11\">" + values + "</textarea>";
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
