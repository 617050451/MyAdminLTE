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
        "lengthChange": true,
        "autoWidth": false,
        "aLengthMenu": [25, 50, 100, 200],
        //当处理大数据时，延迟渲染数据，有效提高Datatables处理能力 
        "pagingType": "full_numbers",//详细分页组，可以支持直接跳转到某页  
        "deferRender": true,
        "processing": false,  //隐藏加载提示,自行处理
        "serverSide": true,  //启用服务器端分页
        "searching": false,  //禁用原生搜索
        "orderMulti": true,  //启用多列排序
        "columns": [],
        "oLanguage": oLanguage,
        "url": "/Ajax/GetData.ashx",
        "fromchildren": "input,textarea,select",
        "getData": function (option, funaggregate) {
            var aoColumnDefs = [];
            if (listconifg.ischoice == 1)
                aoColumnDefs.push(0);
            if (listconifg.columns[listconifg.columns.length - 1].data == "ItemID")
                aoColumnDefs.push(listconifg.columns.length - 1);
            table = $('#example').dataTable({
                "dom": listconifg.dom,
                "aoColumnDefs": [{ "bSortable": false, "aTargets": aoColumnDefs }],
                "aaSorting": [[listconifg.ischoice, "asc"]],
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
                    loadding('正在查询，请稍等...');
                    var where = $('#selectwherefrom').find(listconifg.fromchildren).serializeArray();
                    //封装请求参数
                    var param = {};
                    param.gettype = "GetDataList";
                    param.option = option;
                    param.limit = data.length;//页面显示记录条数，在页面显示每页显示多少项的时候
                    param.start = data.start;//开始的记录序号
                    param.page = (data.start / data.length) + 1;//当前页码;
                    param.where = JSON.stringify(where);
                    param.order = JSON.stringify(data.order);
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
                                    $("#ltlSum").html(result.sumHtml);
                                    GetDataAfter(result.data, option, funaggregate);
                                }, 10);
                            }
                            loadClose();
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
        },
        "setTable": function () {
            //设置
            if (listconifg.isplus == 1) {
                $("div[data-resple='iswhere']").addClass("collapsed-box");
                $("i[data-resple='isplus']").addClass("fa fa-plus");
            } else {
                $("div[data-resple='iswhere']").removeClass("collapsed-box");
                $("i[data-resple='isplus']").addClass("fa fa-minus");
            }
            if (listconifg.iswhere == 0)
                $("div[data-resple='iswhere']").addClass("hidden");
            else
                $("div[data-resple='iswhere']").removeClass("hidden");
            if ($("#ltlbnts").html() == "")
                $("#ltlbnts").hide();
            if ($("#ltlSum").html() == "")
                $("#ltlSum").hide();
        }
    }
    //注册事件 全选，删除
    function GetDataAfter(redata, option, funaggregate) {
        funaggregate = funaggregate || {}
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
                    Select: funaggregate.Select || function (sender) {
                        table.fnClearTable(false);  //清空数据.fnClearTable();//清空数据
                        table.fnDestroy(); //还原初始化了的datatable  
                        listconifg.getData(option);
                    },
                    MoreBntClick: funaggregate.MoreBntClick || function (sender) {
                        var ItemID = sender.val();
                        if (ItemID == "") {
                            layer.msg("请选择需要操作的记录！");
                            loadClose();
                            return false;
                        }
                        loadding('处理中，请稍等...');
                        var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                        alert("更多按钮测试：" + ItemID);
                        loadClose();
                    }
                    , InsertItemID: funaggregate.InsertItemID || function (sender) {
                        loadding('处理中，请稍等...');
                        alert("新增");
                        loadClose();
                    }
                    , UpdateItemID: funaggregate.UpdateItemID || function (sender) {
                        var ItemID = sender.val();
                        if (ItemID == "") {
                            layer.msg("请选择需要操作的记录！");
                            loadClose();
                            return false;
                        }
                        loadding('处理中，请稍等...');
                        var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                        alert("修改：" + ItemID);
                        loadClose();
                    }
                    , DeleteItemID: funaggregate.DeleteItemID || function (sender) {
                        var ItemID = sender.val();
                        if (ItemID == "") {
                            ItemID = OnCheckboxOnSelectValue();
                            if (ItemID == "") {
                                layer.msg("请选择需要操作的记录！");
                                loadClose();
                                return false;
                            }
                        }
                        loadding('处理中，请稍等...');
                        var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                        DeleteItemID(option, ItemID);
                        loadClose();
                    }
                    , CheckBoxItemID: funaggregate.CheckBoxItemID || function (sender) {
                        loadding('处理中，请稍等...');
                        var ItemID = sender.val();
                        var currdata = commend.find(data, function (s) { return s.ItemID === ItemID; });
                        var values = OnCheckboxOnClick();
                        loadClose();
                    }, SelectAll: funaggregate.SelectAll || function (sender) {
                        loadding('处理中，请稍等...');
                        if ($(sender).prop("checked")) {
                            $("input[name='CheckBoxItemID']").prop("checked", 'true');//全选 
                        } else {
                            $("input[name='CheckBoxItemID']").prop("checked", '');//取消全选 
                        }
                        var values = OnCheckboxOnSelectValue();
                        loadClose();
                    }, ShowImg: funaggregate.ShowImg || function (sender) {//放大显示图片集
                        loadding('加载中，请稍等...');
                        ShowImgUrl(sender);
                        loadClose();
                    }, SEOFieldKey: funaggregate.SEOFieldKey || function (sender) {//综合查询
                        var value = $(sender).val();
                        if (value.length > 0) {
                            var valuelist = $(sender).attr("bnt-value").split(',');
                            $("#example tbody tr").each(function () {
                                for (var i = 0; i < valuelist.length; i++) {
                                    $(this).hide();
                                    var item = valuelist[i];
                                    var index = $("#example th[filedkey=" + item + "]").index();
                                    if ($(this).find("td:eq(" + index + ")").filter(":contains('" + value + "')").length > 0)
                                    {
                                        $(this).show();
                                        break;
                                    }
                                }
                            })                           
                        } else {
                            $('#example tbody tr').show();
                        }
                    }, ShowColumn: funaggregate.ShowColumn || function (sender) {
                        loadding('加载中，请稍等...');
                        var showHtml = "<div><table class='table table-bordered table-hover dataTable no-footer'>";
                        $(this).parent("th").siblings("th").each(function (index) {
                            showHtml += "<tr><td><input type='checkbox' checked='checked' data-index=" + index + "/> " + $(this).text() + "</td></tr>";
                        });
                        showHtml += "</table></div>";
                        //页面层
                        layer.open({
                            type: 1,
                            id: "columninfo",
                            title: "设置显示列",
                            shade: false,
                            skin: 'layui-layer-rim', //加上边框
                            area: ['200px'], //宽高
                            offset: 'rt',
                            content: showHtml,
                            success: function () {
                                $("#columninfo input[type='checkbox']").change(function () {
                                    var index = $(this).data("index");
                                    if ($(this).is(':checked')) {
                                        $("#example tr th:nth-child(" + (index + 1) + ")").show();
                                        $("#example tr td:nth-child(" + (index + 1) + ")").show();
                                    } else {
                                        $("#example tr th:nth-child(" + (index + 1) + ")").hide();
                                        $("#example tr td:nth-child(" + (index + 1) + ")").hide();
                                    }
                                });
                            }
                        });
                        loadClose();
                    }
                };           
            $('body *[bnt-click]').unbind("click");  
            $('body *[bnt-click]').bind('click', function () {
                var othis = $(this)
                    , attrEvent = othis.attr('bnt-click');
                clickList[attrEvent] && clickList[attrEvent].call(this, othis);
            });
            $('body *[bnt-keyup]').unbind("keyup");
            $('body *[bnt-keyup]').bind('keyup', function () {
                var othis = $(this)
                    , attrEvent = othis.attr('bnt-keyup');
                clickList[attrEvent] && clickList[attrEvent].call(this, othis);
            });
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
                $("#example .showBntA").mouseover(function (f) {
                    var xx = f.pageX;
                    var yy = f.pageY;
                    $(this).nextAll(".showBntDiv").css({ "left": (xx - 110) + "px", "top": (yy - 60) + "px" });
                    $(this).nextAll(".showBntDiv").removeClass("hide");
                });
                $("#example .showBntDiv").mouseleave(function () {
                    $(this).addClass("hide");
                });
            });
        })
        if (jQuery.isFunction("GetDataSuccess")) {
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
    //删除选中的数据
    function DeleteItemID(option, ChoiceValue) {
        if (ChoiceValue == "") {
            layer.msg('请选择需要删除的数据！');
        } else {
            //询问框
            layer.confirm('你确定要删除当前选择的数据？', {
                title: '温馨提示',
                btn: ['确定', '取消'] //按钮
            }, function () {
                var param = {};
                param.gettype = "BntDeleteItemID";
                param.choicevalue = ChoiceValue;
                param.option = option;
                //ajax请求数据
                $.ajax({
                    type: "GET",
                    url: listconifg.url,
                    data: param,
                    cache: false,  //禁用缓存
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        if (result.code === 1) {
                            layer.msg('操作成功！', {
                                icon: 1, time: 1500, end: function () {
                                    location.reload();
                                }
                            });
                        } else {
                            layer.msg(result.msg, {
                                icon: 2, end: function () {
                                    location.reload();
                                }
                            });
                        }
                    }
                });
            });
        }
    }
})
var loadIndex = 0;
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
// 转换日期格式
function SetDateTime(data, fmt) {
    if (data != null && data.length > 0) {
        var date = new Date(data.replace(/-/g, '/').replace('T', ' '));
        return date.Format(fmt);
    } else {
        return "";
    }
}
//转换请求
function GetFieldKeyValue(Row, FieldKey, envent) {
    var param = Row;
    param.gettype = "GetFieldKeyValue";
    param.FieldKey = FieldKey;
    //ajax请求数据
    $.ajax({
        type: "GET",
        url: listconifg.url,
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
//设置图片
function SetImgUrl(row, data, endfun) {
    if (data != null && data.length > 0) {
        var srcArr = data.split(',');
        var redata = "";
        for (var i = 0; i < srcArr.length; i++) {
            redata += "<img width='27' bnt-click='ShowImg' img-group='" + row.ItemID + "' img-start='" + i + "'  src='" + srcArr[i] + "' \" />";
        }
        endfun(redata);
    } else {
        endfun("");
    }
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
//编码json数据
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
jQuery.escapeHTML = function (a) {
    a = "" + a;
    return a.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&apos;");;
}
jQuery.unescapeHTML = function (a) {
    a = "" + a;
    return a.replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&amp;/g, "&").replace(/&quot;/g, '"').replace(/&apos;/g, "'");
}

jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') {
        options = options || {};
        if (value === null) {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString();
        }
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else {
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
}