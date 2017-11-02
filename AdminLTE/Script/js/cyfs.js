        var hide_guid = "";
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
                    $("input[name='checkboxGuid']").prop("checked", 'true');//全选 
                } else {
                    $("input[name='checkboxGuid']").prop("checked", '');//取消全选 
                }
                OnCheckboxOnSelectValue();
            })
            $('input:checkbox[name=checkboxGuid]').click(function () {
                OnCheckboxOnSelectValue(); 
            })
            $('#deleteGUID').click(function () {
                deleteGUID();
            })
        }
        //选中的值
        function OnCheckboxOnSelectValue() {
            var guidValues = "";
            var checkboxChecked = $('input:checkbox[name=checkboxGuid]:checked');
            var checkbox = $('input:checkbox[name=checkboxGuid]');
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
        function deleteGUID() {
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