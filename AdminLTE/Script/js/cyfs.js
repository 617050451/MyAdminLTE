var hide_guid = "";
var tableguid = getQueryString("tableguid");
//获取url参数
function getQueryString(key) {
            var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
            var result = window.location.search.substr(1).match(reg);
            return result ? decodeURIComponent(result[2]) : null;
        }
// 语言设置  
var oLanguage = {
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
//取当前页面名称(带后缀名)
function pageName() {
            var strUrl = location.href;
            var arrUrl = strUrl.split("/");
            var strPage = arrUrl[arrUrl.length - 1];
            return strPage;
        }
//注册事件 全选，删除
function getDataAfter() {
    $("#selectAll").click(function () {
        if ($(this).prop("checked")) {
            $("input[name='checkboxItemID']").prop("checked", 'true');//全选 
        } else {
            $("input[name='checkboxItemID']").prop("checked", '');//取消全选 
        }
        OnCheckboxOnSelectValue();
    })
    $('input:checkbox[name=checkboxItemID]').click(function () {
        OnCheckboxOnSelectValue();
    })
    $('#deleteItemID').click(function () {
        deleteItemID();
    })
}
//选中的值
function OnCheckboxOnSelectValue() {
    var guidValues = "";
    var checkboxChecked = $('input:checkbox[name=checkboxItemID]:checked');
    var checkbox = $('input:checkbox[name=checkboxItemID]');
    if (checkboxChecked.length == checkbox.length && checkboxChecked.length > 0) {
        $("#selectAll").prop("checked", 'true');
    } else {
        $("#selectAll").prop("checked", '');
    }
    $(checkboxChecked).each(function (i) {
        if (i > 0)
            guidValues += ",";
        guidValues += "'" + $(this).val() + "'";
    });
    hide_guid = guidValues;
}
//删除BY GUID
function deleteItemID() {
            if (hide_guid == "") {
                layer.msg('请选择需要删除的数据！');
            } else {
                //询问框
                layer.confirm('你确定要删除当前选择的数据？', {
                    title: '删除',
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    var param = {};
                    param.gettype = "bntOperation";
                    param.values = hide_guid;
                    //ajax请求数据
                    $.ajax({
                        type: "GET",
                        url: pageName(),
                        data: param,
                        cache: false,  //禁用缓存
                        dataType: "text",
                        async: false,
                        success: function (result) {
                            if (result == "True") {
                                layer.msg('操作成功！', {
                                    icon: 1, end: function () {
                                        location.reload();
                                    }
                                });
                            } else {
                                layer.msg('操作失败！', {
                                    icon: 2
                                });
                            }
                        }
                    });
                });
            }
        }
var loadIndex;
function loadding(obj) {
            loadIndex = layer.msg(obj, {
                icon: 16, shade: 0.01,time: 0
            });
        }
function loadClose(obj) {
            layer.close(obj);
}

//登录
//登录事件
function signinOnclick() {
    var bnt = $("button[name=bntSave]");
    bntLoading(bnt);
    var userid = $("#userid").val();
    var password = $("#password").val();
    if (userid == "" || password == "") {
        funerrorMes('账号或错误不能为空');
        bntCloseLoading(bnt);
        return false;
    }
    //封装请求参数
    var param = {};
    param.gettype = "login";
    param.userid = userid;
    param.password = password;
    //ajax请求数据
    $.ajax({
        type: "GET",
        url: pageName(),
        cache: false,  //禁用缓存
        data: param,  //传入组装的参数
        dataType: "text",
        async: false,
        success: function (result) {
            if (result == "True")
                $(location).attr('href', 'Index.aspx');
            else {
                funerrorMes('账号或错误！请进行一些更改。');
                bntCloseLoading(bnt);
                return false;
            }
        }
    });
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

//加载数据
var isplus = $("#IsPlus").val();
var iswhere = $("#IsWhere").val();
if (isplus == "1") {
    $("div[data-resple='iswhere']").addClass("collapsed-box");
    $("i[data-resple='isplus']").addClass("fa fa-plus");
} else {
    $("i[data-resple='isplus']").addClass("fa fa-minus");
}
if (iswhere == "0")
    $("div[data-resple='iswhere']").addClass("hidden");
var columnsJson = eval("(" + $("#ColumnsJson").val() + ")");
$(document).ready(function () {
    setTimeout(getJsonData("getDate"), 50);
    //Date picker
    $('input[data-type=datepicker]').datepicker({
        language: 'zh-CN',
        autoclose: true,
        todayHighlight: true,
        format: 'yyyy-mm-dd'
    });
});
//获取数据
var table;
function getJsonData(type) {
    if (type == 'select') {
        table.fnClearTable(false);  //清空数据.fnClearTable();//清空数据
        table.fnDestroy(); //还原初始化了的datatable  
    }
    table = $('#example').dataTable({
        "dom": "t<'row'<'#id.col-xs-2 table-l'l><'#id.col-xs-3'i><'#id.col-xs-6 table-p'p>>r",
        "aoColumnDefs": [{ "bSortable": false, "aTargets": [0] }],
        "aaSorting": [[1, "asc"]],
        "lengthChange": true,
        "autoWidth": false,
        "aLengthMenu": [25, 50, 100, 200],
        //当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
        "pagingType": "full_numbers",//详细分页组，可以支持直接跳转到某页  
        "deferRender": true,
        "processing": true,  //隐藏加载提示,自行处理
        "serverSide": true,  //启用服务器端分页
        "searching": false,  //禁用原生搜索
        "orderMulti": true,  //启用多列排序
        "columns": columnsJson,
        "oLanguage": oLanguage,
        ajax: function (data, callback, settings) {
            var values = $('#selectWhere').find('input,select').serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "getDate";
            param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
            param.start = data.start;//开始的记录序号
            param.page = (data.start / data.length) + 1;//当前页码;
            param.values = JSON.stringify(values);
            param.order = columnsJson[data.order[0].column]['data'];
            param.orderDir = data.order[0].dir;
            //ajax请求数据
            $.ajax({
                type: "GET",
                url: pageName(),
                cache: false,  //禁用缓存
                data: param,  //传入组装的参数
                dataType: "json",
                async: false,
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
                            $("#ltlSum").html(result.sumHtml);
                            getDataAfter();
                        }, 200);
                    }
                }
            });
        }
    });
}