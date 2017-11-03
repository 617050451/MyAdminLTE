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
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css" />
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
                <table class="table" style="margin:0px;margin-top:-2px;border-left:1px solid #ddd;border-right:1px solid #ddd;">
                    <tbody>
                        <tr class="pull-right">
                            <td style="border: none;">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-info" onclick="bntSaveClick()">保存</button>
                                    <button type="button" class="btn btn-info">自动排序</button>
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
                            <th>显示转换</th>
                            <th>是否显示</th>
                            <th>排序</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="ltlTable" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
        </section>
    </form>
    <!-- jQuery 3 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="../../Script/js/cyfs.js"></script>
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <script> 
        function bntSaveClick() {
            var values = $("#example").find("input,select").serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "setDate";
            param.values = JSON.stringify(values);
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
    </script>
</body>
</html>
