<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="AdminLTE.Admin.Temp.AddPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>项目页面管理</title>
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
                            <div id="ltlbnts" class="pull-left" style="height: 24px;">
                                <asp:Literal ID="ltlbnt" runat="server" Text=""></asp:Literal>
                            </div>
                            <table id="example" class="table table-bordered table-hover">
                                <thead>
                                    <tr role="row">
                                        <th class="sorting_asc" tabindex="0" aria-controls="example" rowspan="1" colspan="1" aria-sort="ascending" aria-label="页面编号: activate to sort column descending">页面编号</th>
                                        <th class="sorting" tabindex="1" aria-controls="example" rowspan="1" colspan="1" aria-label="页面说明: activate to sort column ascending">页面说明</th>
                                        <th class="sorting" tabindex="2" aria-controls="example" rowspan="1" colspan="1" aria-label="页面名称: activate to sort column ascending">页面名称</th>
                                        <th class="sorting" tabindex="3" aria-controls="example" rowspan="1" colspan="1" aria-label="操作: ">操作</th>
                                    </tr>
                                </thead>
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
        <div id="LayerOpenHtml" class="hidden">
            <div id="LayerOpenHtmlFrom">
                <div class="form-group" style="display: -webkit-box; margin-top: 10px;">
                    <label for="Title" class="col-sm-2 control-label text-right" style="padding: 0px; line-height: 32px;">标题：</label>
                    <div class="col-sm-10">
                        <input type="text" name="Title" class="form-control" placeholder="标题" value="" />
                    </div>
                </div>
                <div class="form-group" style="display: -webkit-box;">
                    <label for="FileName" class="col-sm-2 control-label text-right" style="padding: 0px; line-height: 32px;">页面名称：</label>
                    <div class="col-sm-10">
                        <input type="text" name="FileName" class="form-control" placeholder="页面名称" value="" />
                    </div>
                </div>
                <div class="form-group" style="display: -webkit-box; margin-top: 10px;">
                    <label for="TableName" class="col-sm-2 control-label text-right" style="padding: 0px; line-height: 32px;">数据表：</label>
                    <div class="col-sm-10">
                        <input type="text" name="TableName" class="form-control" placeholder="数据表" value="" />
                    </div>
                </div>
                <div class="form-group" style="display: -webkit-box;">
                    <label for="SQL" class="col-sm-2 control-label text-right" style="padding: 0px;">SQL：</label>
                    <div class="col-sm-10">
                        <textarea name="" class="form-control" placeholder="SQL" rows="3"></textarea>
                    </div>
                </div>
                <div class="form-group" style="display: -webkit-box;">
                    <label for="Note" class="col-sm-2 control-label text-right" style="padding: 0px;">备注：</label>
                    <div class="col-sm-10">
                        <textarea name="Note" class="form-control" placeholder="备注" rows="3"></textarea>
                    </div>
                </div>
                <div class="form-group text-center">
                    <button type="button" class="btn btn-success" onclick="BntSaveTable()">保　存</button>
                </div>
                <div class="hidden">
                    <label id="Gettype"></label>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="IsPlus" runat="server"  Value="0"/>
        <asp:HiddenField ID="IsWhere" runat="server" Value="0"/>
        <asp:HiddenField ID="IsChoice" runat="server" Value="0"/>
        <asp:HiddenField ID="ColumnsJson" runat="server" Value="[{&quot;data&quot;: &quot;GUID&quot;},{&quot;data&quot;: &quot;Title&quot;},{&quot;data&quot;: &quot;FileName&quot;},{&quot;data&quot;: &quot;ItemID&quot;, render: function (data, type, row) { return &quot;<button name='UpdateItemID' type = 'button' class='btn btn-warning  btn-xs' value='&quot; + data + &quot;'>修　改</button>&amp;nbsp;<button name = 'DeleteItemID' type = 'button' class='btn btn-danger  btn-xs' value='&quot; + data + &quot;'>删　除</button>&amp;nbsp;&quot;}}]"/>
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
    <link href="../../Script/js/cyfs.css" rel="stylesheet" />
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <script>
        var Index;
        var GetType;
        function LayerOpenHtml(title, gettype,value) {
            var showHtml = $("#LayerOpenHtml").html();
            //页面层
            Index = layer.open({
                type: 1,
                title: title,
                skin: 'layui-layer-rim', //加上边框
                area: ['620px', '450px'], //宽高
                content: showHtml,
                success: function () {
                    GetType = gettype;
                    ChoiceValue = value;
                    if (ChoiceValue != undefined) {
                        var param = {};
                        param.Gettype = 'GetDataView';
                        param.ChoiceValue = ChoiceValue;
                        $.ajax({
                            type: "GET",
                            url: pageName(),
                            cache: false,  //禁用缓存
                            data: param,  //传入组装的参数
                            dataType: "json",
                            async: false,
                            success: function (result) {
                                if (result != null && result != "") {
                                    var json = result[0]; 
                                    $.each(json, function (key, val) {
                                        $(".layui-layer [name=" + key + "]").val(val);
                                    });
                                } else {
                                    layer.msg("系统繁忙，请稍等.....");
                                }
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                layer.msg("系统繁忙，请稍等.....");
                            }
                        });
                    }
                }
            });
        }
        
        function BntSaveTable() {
            var FromValues = $(".layui-layer #LayerOpenHtmlFrom").find(fromchildren).serializeArray();
            var param = {};
            param.Gettype = GetType
            param.ChoiceValue = ChoiceValue;
            param.FromValues = GetFromJson(FromValues);
            return false;
            //ajax请求数据
            $.ajax({
                type: "GET",
                url: pageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result != null && result[0].code == "100") {
                        //setTimeout仅为测试延迟效果
                        setTimeout(function () {
                            layer.msg('保存成功', {
                                icon: 1, time: 1500, end: function () {
                                    layer.close(index);
                                    location.reload();
                                }
                            });
                        }, 50);
                    } else {
                        layer.msg("系统繁忙，请稍等.....");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("系统繁忙，请稍等.....");
                }
            });
        }

        function GetDataSuccess() {
            setTimeout(function () {
                $("button[name=UpdateItemID]").unbind("click");
                $("button[name=UpdateItemID]").click(function () {
                    LayerOpenHtml('修改页面', $(this).val());
                })
                $("button[name=DeleteItemID]").click(function () {
                    alert("删除1：" + $(this).val());
                })
            }, 50);
        }
    </script>
</body>
</html>
