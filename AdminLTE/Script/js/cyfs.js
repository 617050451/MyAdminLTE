

var tableguid = getQueryString("tableguid");
var PageConfig = {};
PageConfig.IsPlus = 0;
PageConfig.IsWhere = 0;
PageConfig.IsChoice = 0;
PageConfig.Columns = [];
PageConfig.Fromchildren = "input,textarea,select";
function getJsonData(type, pageName, option) {
    if (PageConfig.Columns == undefined || PageConfig.Columns.length == 0)
        return false;
    if (pageName == undefined || pageName == "")
        pageName = GetPageName();
    if (type == 'select') {
        table.fnClearTable(false);  //清空数据.fnClearTable();//清空数据
        table.fnDestroy(); //还原初始化了的datatable  
    } else {
        SetHtml();
    }
    var aTargets = [];
    aTargets[0] = PageConfig.IsChoice - 1;
    if (PageConfig.Columns[PageConfig.Columns.length - 1].data == "ItemID")
        aTargets[1] = PageConfig.Columns.length - 1;
    table = $('#example').dataTable({
        "dom": "t<'row'<'#id.col-xs-2 table-l'l><'#id.col-xs-3'i><'#id.col-xs-6 table-p'p>>r",
        "aoColumnDefs": [{ "bSortable": false, "aTargets": aTargets }],
        "aaSorting": [[PageConfig.IsChoice, "asc"]],
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
        "columns": PageConfig.Columns,
        "oLanguage": oLanguage,
        ajax: function (data, callback, settings) {
            var WhereValues = $('#SelectWhereFrom').find(PageConfig.Fromchildren).serializeArray();
            //封装请求参数
            var param = {};
            param.gettype = "GetDataList";
            param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
            param.start = data.start;//开始的记录序号
            param.page = (data.start / data.length) + 1;//当前页码;
            param.WhereValues = JSON.stringify(WhereValues);
            param.order = PageConfig.Columns[data.order[0].column]['data'];
            param.orderDir = data.order[0].dir;
            //ajax请求数据
            $.ajax({
                type: "GET",
                url: pageName,
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
                            if (type != 'select') {
                                GetDataAfter(result.data, option);                               
                            }
                            loadClose();
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
function SetHtml() {
    if (PageConfig.IsPlus == 1) {
        $("div[data-resple='iswhere']").addClass("collapsed-box");
        $("i[data-resple='isplus']").addClass("fa fa-plus");
    } else {
        $("div[data-resple='iswhere']").removeClass("collapsed-box");
        $("i[data-resple='isplus']").addClass("fa fa-minus");
    }
    if (PageConfig.IsWhere == 0)
        $("div[data-resple='iswhere']").addClass("hidden");
    else
        $("div[data-resple='iswhere']").removeClass("hidden");
}
//获取url参数
function getQueryString(key) {
    var reg = new RegExp("(^|&)" + key + "=([^&]*)(&|$)");
    var result = window.location.search.substr(1).match(reg);
    return result ? decodeURIComponent(result[2]) : null;
}
// 语言设置  
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
//取当前页面名称(带后缀名)
function GetPageName() {
    var strUrl = location.href;
    var arrUrl = strUrl.split("/");
    var strPage = arrUrl[arrUrl.length - 1];
    return strPage;
}
//注册事件 全选，删除
function GetDataAfter(redata, option) {
    option = option || {}
    //注册按钮事件
    $(function () {
        var data = redata
            , $body = $('body')
            , commend = {
                find: function (arr, ev) {
                    var array = new Array();
                    for (var i = 0; i < arr.length; i++) {
                        var item = arr[i];
                        if (ev(item)) {
                            array.push(item);
                        }
                    }
                    return array;
                }
            }
            , clickList = {
                Select: option.Select || function (sender) {
                    loadding('正在查询，请稍等...');
                    getJsonData('select');
                },
                MoreBntClick: option.MoreBntClick || function (sender) {
                    loadding('处理中，请稍等...');
                    var ItemID = sender.val();
                    var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                    alert("更多按钮测试：" + ItemID);
                }
                , InsertItemID: option.InsertItemID || function (sender) {
                    loadding('处理中，请稍等...');
                    alert("新增");
                }
                , UpdateItemID: option.UpdateItemID || function (sender) {
                    loadding('处理中，请稍等...');
                    var ItemID = sender.val();
                    var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                    alert("修改：" + ItemID);
                }
                , DeleteItemID: option.DeleteItemID || function (sender) {
                    loadding('处理中，请稍等...');
                    var ItemID = sender.val();
                    var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                    DeleteItemID(OnCheckboxOnSelectValue());
                }
                , CheckBoxItemID: option.CheckBoxItemID || function (sender) {
                    loadding('处理中，请稍等...');
                    var ItemID = sender.val();
                    var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                    var values = OnCheckboxOnClick();
                    console.log(values);
                }, SelectAll: option.SelectAll || function (sender) {
                    loadding('处理中，请稍等...');
                    if ($(sender).prop("checked")) {
                        $("input[name='CheckBoxItemID']").prop("checked", 'true');//全选 
                    } else {
                        $("input[name='CheckBoxItemID']").prop("checked", '');//取消全选 
                    }
                    var values = OnCheckboxOnSelectValue();
                    console.log(values);
                }, ShowImg: option.ShowImg || function (sender) {//放大显示图片集
                    ShowImgUrl(sender);
                }
            };
        $body.on('click', '*[bnt-click]', function () {
            var othis = $(this)
                , attrEvent = othis.attr('bnt-click');
            clickList[attrEvent] && clickList[attrEvent].call(this, othis);
        });
    })
    if (jQuery.isFunction(GetDataSuccess)) {
        GetDataSuccess();
    }
}
//选中的值
function OnCheckboxOnClick() {
    var guidValues = "";
    var checkboxChecked = $('input:checkbox[name=CheckBoxItemID]:checked');
    var checkbox = $('input:checkbox[name=CheckBoxItemID]');
    if (checkboxChecked.length == checkbox.length && checkboxChecked.length > 0) {
        $("input:checkbox[name=SelectAll]").prop("checked", 'true');
    } else {
        $("input:checkbox[name=SelectAll]").prop("checked", '');
    }
    $(checkboxChecked).each(function (i) {
        if (i > 0)
            guidValues += ",";
        guidValues += "'" + $(this).val() + "'";
    });
    return guidValues;
}
function OnCheckboxOnSelectValue() {
    var guidValues = "";
    var checkboxChecked = $('input:checkbox[name=CheckBoxItemID]:checked');
    $(checkboxChecked).each(function (i) {
        if (i > 0)
            guidValues += ",";
        guidValues += "'" + $(this).val() + "'";
    });
    return guidValues;
}
//删除BY GUID
function DeleteItemID(ChoiceValue) {
    if (ChoiceValue == "") {
        layer.msg('请选择需要删除的数据！');
    } else {
        //询问框
        layer.confirm('你确定要删除当前选择的数据？', {
            title: '删除',
            btn: ['确定', '取消'] //按钮
        }, function () {
            var param = {};
            param.gettype = "BntOperation";
            param.ChoiceValue = ChoiceValue;
            //ajax请求数据
            $.ajax({
                type: "GET",
                url: GetPageName(),
                data: param,
                cache: false,  //禁用缓存
                dataType: "text",
                async: false,
                success: function (result) {
                    if (result == "False") {
                        console.log(result);
                        layer.msg('操作失败！', {
                            icon: 2
                        });
                    } else {
                        layer.msg('操作成功！', {
                            icon: 1, time: 1500, end: function () {
                                location.reload();
                            }
                        });
                    }
                }
            });
        });
    }
}
var loadIndex;
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
        url: GetPageName(),
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
function GetFromJson(obj) {
    var m = [], idata;
    $.each(obj, function (i, field) {
        // 由于会出现"双引号字符会导致接下来的数据打包失败，故此对元素内容进行encodeURI编码  
        // 后台PHP采用urldecode()函数还原数据  
        m.push('"' + field.name + '":"' + encodeURI(field.value) + '"');
    });
    idata = '{' + m.join(',') + '}';
    // 按字符 idata 转换成 JSON 格式  
    return idata;
}
//转换请求
function GetFieldKeyValue(Row, FieldKey, envent) {
    var param = Row;
    param.gettype = "GetFieldKeyValue";
    param.FieldKey = FieldKey;
    //ajax请求数据
    $.ajax({
        type: "GET",
        url: GetPageName(),
        cache: true,  //禁用缓存
        data: param,  //传入组装的参数
        dataType: "text",
        async: false,
        success: envent,
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
//时间格式化
Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1, //月份   
        "d+": this.getDate(), //日   
        "H+": this.getHours(), //小时   
        "m+": this.getMinutes(), //分   
        "s+": this.getSeconds(), //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds() //毫秒   
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
///为字符串添加模糊比较的方法
String.prototype.isLike = function (exp/*类似于SQL中的模糊查询字符串*/, i/*是否区分大小写*/) {
    var str = this;
    i = i == null ? false : i;
    if (exp.constructor == String) {
        /*首先将表达式中的‘_’替换成‘.’，但是‘[_]’表示对‘_’的转义，所以做特殊处理*/
        var s = exp.replace(/_/g, function (m, i) {
            if (i == 0 || i == exp.length - 1) {
                return ".";
            }
            else {
                if (exp.charAt(i - 1) == "[" && exp.charAt(i + 1) == "]") {
                    return m;
                }
                return ".";
            }
        });
        /*将表达式中的‘%’替换成‘.’，但是‘[%]’表示对‘%’的转义，所以做特殊处理*/
        s = s.replace(/%/g, function (m, i) {
            if (i == 0 || i == s.length - 1) {
                return ".*";
            }
            else {
                if (s.charAt(i - 1) == "[" && s.charAt(i + 1) == "]") {
                    return m;
                }
                return ".*";
            }
        });
        /*将表达式中的‘[_]’、‘[%]’分别替换为‘_’、‘%’*/
        s = s.replace(/\[_\]/g, "_").replace(/\[%\]/g, "%");
        /*对表达式处理完后构造一个新的正则表达式，用以判断当前字符串是否和给定的表达式相似*/
        var regex = new RegExp("^" + s, i ? "" : "i");
        return regex.test(this);
    }
    return false;
};
function SetDateTime(data, fmt) {
    // 转换日期格式
    var date = new Date(data.replace(/-/g, '/').replace('T', ' '));
    return date.Format(fmt);
}
//选中行
$(function () {
    $("#example tr").click(function () {
        $("#example tr").css("background-color", "");
        $(this).css("background-color", "#eee");
    })
    $("#example tr").mouseover(function () {
        $("#example tr").css("background-color", "");
        $(this).css("background-color", "#eee");
    });
    $("#example tr").mouseout(function () {
        $("#example tr").css("background-color", "");
        $(this).css("background-color", "#eee");
    });
});
//设置图片
function SetImgUrl(row, data, endfun) {
    var srcArr = new Array();
    srcArr = data.split(',');
    var redata = "";
    for (var i = 0; i < srcArr.length; i++) {
        redata += "<img width='40' bnt-click='ShowImg' img-group='" + row.ItemID + "' img-start='" + i + "'  src='" + srcArr[i] + "' \" />";
    }
    endfun(redata);
}
//显示图片
function ShowImgUrl(obj) {
    var srcjson = new Array();
    $("img[img-group='" + $(obj).attr("img-group") + "']").each(function () {
        srcjson.push({ "src": $(this).attr("src") });
    })
    var json = {
        "title": "", //相册标题
        "id": 123, //相册id
        "start": $(obj).attr("img-start"), //初始显示的图片序号，默认0
        "data": srcjson
    }
    layer.photos({
        photos: json //格式见API文档手册页
        , anim: 5 //0-6的选择，指定弹出图片动画类型，默认随机
    });
}

