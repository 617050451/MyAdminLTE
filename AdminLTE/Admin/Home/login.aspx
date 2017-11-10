<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AdminLTE.Admin.Home.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>AdminLTE 2 | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/dist/css/AdminLTE.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="../../Script/AdminLTE-2.4.2/plugins/iCheck/square/blue.css" />
    <link href="../../Script/AdminLTE-2.4.2/dist/css/bootstrapValidator.min.css" rel="stylesheet" />
    <link href="../../Script/AdminLTE-2.4.2/dist/css/bootstrapValidator.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition login-page" style="height:80%;">
    <div class="login-box">
        <div class="login-logo">
            <a href="javascript:void(0)"><b>Family</b>Chen</a>
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <p class="login-box-msg">Please Enter The Family Chen After Login</p>
            <div class="form-group has-feedback">
                <input id="userid" type="email" name="userid" class="form-control" placeholder="账号" />
                <span class="glyphicon glyphicon-cloud form-control-feedback"></span>
            </div>
            <div class="form-group has-feedback">
                <input id="password" type="password" name="password" class="form-control" placeholder="密码" />
                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <%--       <div class="checkbox icheck">
                            <label>
                                <input type="checkbox">
                                Remember Me
           
                            </label>
                        </div>--%>
                </div>
                <!-- /.col -->
                <div class="col-xs-4">
                    <button type="button" class="btn btn-primary btn-block btn-flat"  data-loading-text="Sign In ..." onclick="signinOnclick(this)">Sign In</button>
                </div>
                <!-- /.col -->
            </div>
        </div>
        <!-- /.login-box-body -->
        <div id="error" style="margin-top:10px;"></div>
    </div>
    <!-- /.login-box -->
    <!-- jQuery 3 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../../Script/AdminLTE-2.4.2/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="../../Script/AdminLTE-2.4.2/plugins/iCheck/icheck.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/dist/js/bootstrapValidator.min.js"></script>
    <script src="../../Script/AdminLTE-2.4.2/dist/js/bootstrapValidator.js"></script>
    <script>
        //绑定确定事件
        $(document).keypress(function (e) {
            var eCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            if (eCode == 13) {
                signinOnclick();
            }
        });
        //登录事件
        function signinOnclick(bnt) {
            bntLoading(bnt);
            var email = $("#email").val();
            var password = $("#password").val();
            if (email == "" || password == "") {
                funerrorMes('账号或错误不能为空');
                bntCloseLoading(bnt);
                return false;
            }
            if (email == "admin" && password == "123") {
                $(location).attr('href', '/admin/home/index.aspx');
            } else {
                funerrorMes('账号或错误！请进行一些更改。');
                bntCloseLoading(bnt);
                return false;
            }
        }
        //错误提示
        function funerrorMes(error) {
            $("#error").html('<div id="error" class="alert alert-danger alert-dismissable"><button type="button" class="close" data-dismiss="alert"aria-hidden="true">&times;</button>' + error + '</div>');

        }
        function bntLoading(btn) {
            $(btn).button('loading');
        }
        function bntCloseLoading(btn) {
            $(btn).button('reset');
        }
    </script>
</body>
</html>
