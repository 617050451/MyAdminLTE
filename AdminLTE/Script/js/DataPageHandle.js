var listconifg = {};
var oLanguage = {
    "sProcessing": "加载中...",
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
$(function () {
    listconifg = {
        "isplus": 0,
        "iswhere": 0,
        "ischoice": 0,
        "where": "",
        "dom": "t<'row'<'#id.col-xs-2 table-l'l><'#id.col-xs-3'i><'#id.col-xs-6 table-p'p>>r",
        "aoColumnDefs": [{ "bSortable": false, "aTargets": [0] }],
        "aaSorting": [[1,"asc"]],
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
        "columns": [],
        "oLanguage": oLanguage,
        "url":"/Ajax/GetData.ashx",
        "getData": function () {
            table = $('#example').dataTable({
                "dom": listconifg.dom,
                "aoColumnDefs": listconifg.aoColumnDefs,
                "aaSorting": listconifg.aaSorting,
                "lengthChange": listconifg.lengthChange,
                "autoWidth": listconifg.autoWidth,
                "aLengthMenu": listconifg.aLengthMenu,
                //当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
                "pagingType": listconifg.pagingType,//详细分页组，可以支持直接跳转到某页  
                "deferRender": listconifg.deferRender,
                "processing": listconifg.processing,  //隐藏加载提示,自行处理
                "serverSide": listconifg.serverSide,  //启用服务器端分页
                "searching": listconifg.searching,  //禁用原生搜索
                "orderMulti": listconifg.orderMulti,  //启用多列排序
                "columns": listconifg.columns,
                "oLanguage": listconifg.oLanguage,
                "where": listconifg.where,
                ajax: function (data, callback, settings) {                    
                    //封装请求参数
                    var param = {};
                    param.gettype = "GetDataList";
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码;
                    param.where = data.where;
                    param.order = data.order;
                    //ajax请求数据
                    $.ajax({
                        type: "GET",
                        url: listconifg.url,
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
                                }, 50);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            // 状态码
                            console.log(XMLHttpRequest.status);
                            // 状态
                            console.log(XMLHttpRequest.readyState);
                            // 错误信息   
                            console.log(textStatus);
                            layer.msg("系统繁忙，请稍等.....");
                        }
                    });
                }
            });
        }
    }
})


















function loadding(txt, obj) {
    $(obj).attr('disabled', true);
    loadIndex = layer.msg(txt, {
        icon: 16, shade: 0.01, time: 0, end: function () {
            $(obj).attr('disabled', false);
        }
    });
}
function loadClose() {
    layer.close(loadIndex);
}
//获取url参数
function getQueryString(key) {
    var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
    var result = window.location.search.substr(1).match(reg);
    return result ? decodeURIComponent(result[2]) : null;
}
//取当前页面名称(带后缀名)
function GetPageName() {
    var strUrl = location.href;
    var arrUrl = strUrl.split("/");
    var strPage = arrUrl[arrUrl.length - 1];
    return strPage;
}
