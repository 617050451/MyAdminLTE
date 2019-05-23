<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetInsert.aspx.cs" Inherits="AdminLTE.Admin.PageManage.SetInsert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/bower_components/Ionicons/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="/Script/AdminLTE-2.4.2/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
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
                    <div class="box-body" style="line-height: 32px;">
                        <label style="float: left;">排版列数：</label>
                        <div class="col-xs-1">
                            <select class="form-control" style="float: left;" onchange="SetCulomnNum(this)">
                                <option value="col-xs-12">1列</option>
                                <option value="col-xs-6">2列</option>
                                <option value="col-xs-4">3列</option>
                                <option value="col-xs-3">4列</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="box-header ui-sortable-handle" style="cursor: move;">
                    <i class="ion ion-clipboard"></i>
                    <h3 class="box-title">字段列表</h3>
                </div>
                <div class="box-body">
                    <ul class="todo-list ui-sortable">
                        <li>
                            <span class="handle ui-sortable-handle">
                                <i class="fa fa-ellipsis-v"></i>
                                <i class="fa fa-ellipsis-v"></i>
                            </span>
                            <input type="checkbox" name="FieldKey" value="GUID" checked="checked" />
                            <span class="text" style="min-width: 120px;">GUID</span>
                            <input type="text" name="FieldText" value="GUID" />
                            <select style="height: 26px;" onchange="SetCulomnNum(this)">
                                <option value="0" selected="selected">不合并</option>
                                <option value="1">合并1列</option>
                                <option value="2">合并2列</option>
                                <option value="3">合并3列</option>
                                <option value="4">合并4列</option>
                            </select>
                        </li>
                        <li>
                            <span class="handle ui-sortable-handle">
                                <i class="fa fa-ellipsis-v"></i>
                                <i class="fa fa-ellipsis-v"></i>
                            </span>
                            <input type="checkbox" name="FieldKey" value="No" checked="checked" />
                            <span class="text" style="min-width: 120px;">No</span>
                            <input type="text" name="No" value="用户编号" />
                            <select style="height: 26px;" onchange="SetCulomnNum(this)">
                                <option value="0" selected="selected">不合并</option>
                                <option value="1">合并1列</option>
                                <option value="2">合并2列</option>
                                <option value="3">合并3列</option>
                                <option value="4">合并4列</option>
                            </select>
                        </li>
                        <li>
                            <span class="handle ui-sortable-handle">
                                <i class="fa fa-ellipsis-v"></i>
                                <i class="fa fa-ellipsis-v"></i>
                            </span>
                            <input type="checkbox" name="FieldKey" value="Account" checked="checked" />
                            <span class="text" style="min-width: 120px;">Account</span>
                            <input type="text" name="Account" value="用户账号" />
                            <select style="height: 26px;" onchange="SetCulomnNum(this)">
                                <option value="0" selected="selected">不合并</option>
                                <option value="1">合并1列</option>
                                <option value="2">合并2列</option>
                                <option value="3">合并3列</option>
                                <option value="4">合并4列</option>
                            </select>
                        </li>
                        <li>
                            <span class="handle ui-sortable-handle">
                                <i class="fa fa-ellipsis-v"></i>
                                <i class="fa fa-ellipsis-v"></i>
                            </span>
                            <input type="checkbox" name="FieldKey" value="Name" checked="checked" />
                            <span class="text" style="min-width: 120px;">Name</span>
                            <input type="text" name="Name" value="用户姓名" />
                            <select style="height: 26px;" onchange="SetCulomnNum(this)">
                                <option value="0" selected="selected">不合并</option>
                                <option value="1">合并1列</option>
                                <option value="2">合并2列</option>
                                <option value="3">合并3列</option>
                                <option value="4">合并4列</option>
                            </select>
                        </li>
                    </ul>
                </div>
                <div class="box-footer clearfix no-border">
                    <button type="button" class="btn btn-default pull-right"><i class="fa fa-plus"></i>Add item</button>
                </div>
            </div>
        </section>
    </form>
    <!-- jQuery 3 -->
    <script src="/Script/AdminLTE-2.4.2/bower_components/jquery/dist/jquery.min.js"></script>
    <script src="/Script/AdminLTE-2.4.2/bower_components/jquery-ui/jquery-ui.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="/Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- DataTables -->
    <script src="/Script/AdminLTE-2.4.2/bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="/Script/AdminLTE-2.4.2/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <!-- SlimScroll -->
    <script src="/Script/AdminLTE-2.4.2/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <!-- FastClick -->
    <script src="/Script/AdminLTE-2.4.2/bower_components/fastclick/lib/fastclick.js"></script>
    <!-- AdminLTE App -->
    <script src="/Script/AdminLTE-2.4.2/dist/js/adminlte.min.js"></script>
    <script src="/Script/js/cyfs.js"></script>
    <script src="/Script/layer-v3.1.0/layer/layer.js"></script>
    <script>
        // jQuery UI sortable for the todo list
        $('.todo-list').sortable({
            placeholder: 'sort-highlight',
            handle: '.handle',
            forcePlaceholderSize: true,
            zIndex: 999999
        });
        function SetCulomnNum(obj) {
            $(".todo-list li").removeAttr("class");
            $(".todo-list li").addClass($(obj).val());
        }
    </script>
</body>
