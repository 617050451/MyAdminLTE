<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataList.aspx.cs" Inherits="Cases_baseprint_HomePage_DataList" validateRequest="false" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../../Tools/AdminLTE-2.3.11/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" /><%--table必要样式--%>
    <link href="../../../Tools/AdminLTE-2.3.11/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" /><%--table必要样式--%>
    <link href="../../../Tools/AdminLTE-2.3.11/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" /><%--table必要样式--%>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <section class="content" style="margin-top:-13px;">
      <div class="row">
        <div class="col-xs-12">
        <div class="box">
<%--            <div class="box-header">
              <h3 class="box-title"></h3>
            </div>--%>
            <!-- /.box-header -->
            <div class="box-body">
                <table id="example" class="table table-bordered table-hover" >
                    <thead>
                        <tr>    
                            <th><input id="checkboxQs" name="checkboxQs" type="checkbox" onclick="OnCheckboxClock()"/></th>                     
                            <th>部门名称</th>
                            <th>奖扣对象</th>
                            <th>工号</th>
                            <th>奖扣时间</th>
                            <th>主题</th>
                            <th>事件</th>
                            <th>B分</th>
                            <th>初审人</th>
                            <th>终审人</th>
                            <th>记录人</th>
                            <th>打印状态</th>
                            <th>事件描述</th>
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
    <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
    <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
    <asp:HiddenField ID="QuantifyRecordId" runat="server" Value="" />
    <asp:HiddenField ID="DepartmentId" runat="server" Value="" />
    <asp:HiddenField ID="Empid" runat="server"  Value="" />
    <asp:HiddenField ID="Prizes" runat="server"  Value="" />
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" style="visibility:hidden"/>
    </form>
<%--    <script src="../../../Tools/Js/jquery-1.8.2.min.js"></script>--%>
    <script src="../../../Tools/AdminLTE-2.3.11/plugins/jQuery/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script src="../../../Tools/AdminLTE-2.3.11/bootstrap/js/bootstrap.min.js" type="text/javascript"></script><%--table必备js--%>
    <script src="../../../Tools/AdminLTE-2.3.11/plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script><%--table必备js--%>
    <script src="../../../Tools/AdminLTE-2.3.11/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script><%--table必备js--%>
    <script> 
        var table;
        var columns = [
                 { "data": "QuantifyRecordId", render: function (data, type, row) { return "<input  name='checkboxQ' type='checkbox' class='table-checkable' onclick='OnCheckboxOnSelectValue()' value='" + data + "'/>"; } },
                 { "data": "Department" },
                 { "data": "EmpName" },
                 { "data": "EmpNum" },
                 { "data": "QuantifyDate" },
                 { "data": "QuantifyName" },
                 { "data": "EventName" },
                 { "data": "BScore" },
                 { "data": "AttnBy" },
                 { "data": "AuditBy" },
                 { "data": "CreateBy" },
                 { "data": "IsPrint", render: function (data, type, row) { return data == 0 ? "未打印" : "已打印" } },
                 { "data": "EventDetail" }
        ];
        var oLanguage = {    // 语言设置  
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
        $(document).ready(function () {
            getJsonDataT();
        });
        //获取数据
        function getJsonDataT() {
            table = $('#example').dataTable({
                "dom": "t<'row'<'#id.col-xs-6'l>><'row'<'#id.col-xs-5'i><'#id.col-xs-7'p>>r",
                "lengthChange": true,
                "autoWidth": false,
                "aLengthMenu": [25, 50, 100, 200],
                //当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
                //"pagingType": "full_numbers",//详细分页组，可以支持直接跳转到某页  
                "deferRender": true,
                "processing": true,  //隐藏加载提示,自行处理
                "serverSide": true,  //启用服务器端分页
                "searching": false,  //禁用原生搜索
                "orderMulti": true,  //启用多列排序
                "columns": columns,
                "oLanguage": oLanguage,
                ajax: function (data, callback, settings) {
                    //封装请求参数
                    var param = {};
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码;
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
    </script>
</body>
</html>
