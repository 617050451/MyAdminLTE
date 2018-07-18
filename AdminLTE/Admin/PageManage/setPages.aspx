<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPages.aspx.cs" Inherits="AdminLTE.Admin.Temp.SetPages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>页面编辑</title>
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
                <div class="col-xs-12">
                    <div class="box box-primary">
                        <div class="box-body" style="line-height:32px;">
                            <label style="float:left;">选择页面：</label>
                            <div class="col-xs-6">
                                <select class="form-control" style="float:left;" onchange="SelectTable(this)" id="selecttable">
                                    <%=OptionList %>
                                </select>
                            </div>
                            <label style="float:left;">搜索：</label>
                            <div class="col-xs-4" style="float:left;">
                                <input type="text" class="form-control" placeholder="页面名称" oninput="OnInput (event)" onpropertychange="OnPropChanged (event)"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <ul class="nav nav-tabs" id="selectnav">
                                <li class="active"><a href="javascript:void(0)" data-src="SetList.aspx">显示页面</a></li>
                                <li><a href="javascript:void(0)" data-src="SetInsert.aspx">新增页面</a></li>
                                <li><a href="javascript:void(0)" data-src="SetUpdate.aspx">修改页面</a></li>
                            </ul>
                            <iframe id="ifmSetPage"  style="width:100%;height:auto;border:none;min-height:800px;"></iframe>
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
    <script src="../../Script/js/cyfs.js"></script>
    <script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
    <script>
        var itemidguid = "";
        $(document).ready(function () {
            $(".box-body ul li a").click(function () {
                $(".box-body ul li").removeClass("active");
                $(this).parent().addClass("active");
                var itemid = $("#selecttable").val();
                if (itemid == null || itemid.lenght < 10) {
                    if (itemidguid == null || itemidguid.lenght < 10) {
                        layer.msg('请选择页面', function () {
                            return false;
                        });
                    }
                } else {
                    itemidguid = itemid;
                }
                $("#ifmSetPage").prop("src", $(this).attr("data-src") + "?ItemGUID=" + itemidguid);
            });
            $(".box-body ul li a:eq(0)").click();
        });

        function OnInput(event) {
            var text = event.target.value;
            if (text == "") {
                $("#selecttable option").show();
            } else {
                $("#selecttable option").each(function () {
                    var value = $(this).attr("page-title");
                    var name = $(this).attr("page-name");
                    if (value.isLike(text, 0) || name.isLike(text, 0)) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            }
            var options = $("#selecttable option");
            options.first().attr("selected", true);
        }
        function OnPropChanged (event) {
            if (event.propertyName.toLowerCase () == "value") {
                alert ("The new content: " + event.srcElement.value);
            }
        }
        function SelectTable(obj) {
          $(".box-body ul li a:eq(0)").click();
        }
    </script>
</body>
</html>
