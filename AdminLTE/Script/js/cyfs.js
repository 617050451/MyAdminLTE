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
        function getDataAfter(urls) {
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
        var TimeObjectUtil;
        /**
         * @title 时间工具类
         * @note 本类一律违规验证返回false
         * @author {boonyachengdu@gmail.com}
         * @date 2013-07-01
         * @formatter "2013-07-01 00:00:00" , "2013-07-01"
         */
        TimeObjectUtil = {
            /**
             * 获取当前时间毫秒数
             */
            getCurrentMsTime: function () {
                var myDate = new Date();
                return myDate.getTime();
            },
            /**
             * 毫秒转时间格式
             */
            longMsTimeConvertToDateTime: function (time) {
                var myDate = new Date(time);
                return this.formatterDateTime(myDate);
            },
            /**
             * 时间格式转毫秒
             */
            dateToLongMsTime: function (date) {
                var myDate = new Date(date);
                return myDate.getTime();
            },
            /**
             * 格式化日期（不含时间）
             */
            formatterDate: function (date) {
                var datetime = date.getFullYear()
                    + "-"// "年"
                    + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                        + (date.getMonth() + 1))
                    + "-"// "月"
                    + (date.getDate() < 10 ? "0" + date.getDate() : date
                        .getDate());
                return datetime;
            },
            /**
             * 格式化日期（含时间"00:00:00"）
             */
            formatterDate2: function (date) {
                var datetime = date.getFullYear()
                    + "-"// "年"
                    + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                        + (date.getMonth() + 1))
                    + "-"// "月"
                    + (date.getDate() < 10 ? "0" + date.getDate() : date
                        .getDate()) + " " + "00:00:00";
                return datetime;
            },
            /**
             * 格式化去日期（含时间）
             */
            formatterDateTime: function (date) {
                var datetime = date.getFullYear()
                    + "-"// "年"
                    + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : "0"
                        + (date.getMonth() + 1))
                    + "-"// "月"
                    + (date.getDate() < 10 ? "0" + date.getDate() : date
                        .getDate())
                    + " "
                    + (date.getHours() < 10 ? "0" + date.getHours() : date
                        .getHours())
                    + ":"
                    + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date
                        .getMinutes())
                    + ":"
                    + (date.getSeconds() < 10 ? "0" + date.getSeconds() : date
                        .getSeconds());
                return datetime;
            },
            /**
             * 时间比较{结束时间大于开始时间}
             */
            compareDateEndTimeGTStartTime: function (startTime, endTime) {
                return ((new Date(endTime.replace(/-/g, "/"))) > (new Date(
                    startTime.replace(/-/g, "/"))));
            },
            /**
             * 验证开始时间合理性{开始时间不能小于当前时间{X}个月}
             */
            compareRightStartTime: function (month, startTime) {
                var now = formatterDayAndTime(new Date());
                var sms = new Date(startTime.replace(/-/g, "/"));
                var ems = new Date(now.replace(/-/g, "/"));
                var tDayms = month * 30 * 24 * 60 * 60 * 1000;
                var dvalue = ems - sms;
                if (dvalue > tDayms) {
                    return false;
                }
                return true;
            },
            /**
             * 验证开始时间合理性{结束时间不能小于当前时间{X}个月}
             */
            compareRightEndTime: function (month, endTime) {
                var now = formatterDayAndTime(new Date());
                var sms = new Date(now.replace(/-/g, "/"));
                var ems = new Date(endTime.replace(/-/g, "/"));
                var tDayms = month * 30 * 24 * 60 * 60 * 1000;
                var dvalue = sms - ems;
                if (dvalue > tDayms) {
                    return false;
                }
                return true;
            },
            /**
             * 验证开始时间合理性{结束时间与开始时间的间隔不能大于{X}个月}
             */
            compareEndTimeGTStartTime: function (month, startTime, endTime) {
                var sms = new Date(startTime.replace(/-/g, "/"));
                var ems = new Date(endTime.replace(/-/g, "/"));
                var tDayms = month * 30 * 24 * 60 * 60 * 1000;
                var dvalue = ems - sms;
                if (dvalue > tDayms) {
                    return false;
                }
                return true;
            },
            /**
             * 获取最近几天[开始时间和结束时间值,时间往前推算]
             */
            getRecentDaysDateTime: function (day) {
                var daymsTime = day * 24 * 60 * 60 * 1000;
                var yesterDatsmsTime = this.getCurrentMsTime() - daymsTime;
                var startTime = this.longMsTimeConvertToDateTime(yesterDatsmsTime);
                var pastDate = this.formatterDate2(new Date(startTime));
                var nowDate = this.formatterDate2(new Date());
                var obj = {
                    startTime: pastDate,
                    endTime: nowDate
                };
                return obj;
            },
            /**
             * 获取今天[开始时间和结束时间值]
             */
            getTodayDateTime: function () {
                var daymsTime = 24 * 60 * 60 * 1000;
                var tomorrowDatsmsTime = this.getCurrentMsTime() + daymsTime;
                var currentTime = this.longMsTimeConvertToDateTime(this.getCurrentMsTime());
                var termorrowTime = this.longMsTimeConvertToDateTime(tomorrowDatsmsTime);
                var nowDate = this.formatterDate2(new Date(currentTime));
                var tomorrowDate = this.formatterDate2(new Date(termorrowTime));
                var obj = {
                    startTime: nowDate,
                    endTime: tomorrowDate
                };
                return obj;
            },
            /**
             * 获取明天[开始时间和结束时间值]
             */
            getTomorrowDateTime: function () {
                var daymsTime = 24 * 60 * 60 * 1000;
                var tomorrowDatsmsTime = this.getCurrentMsTime() + daymsTime;
                var termorrowTime = this.longMsTimeConvertToDateTime(tomorrowDatsmsTime);
                var theDayAfterTomorrowDatsmsTime = this.getCurrentMsTime() + (2 * daymsTime);
                var theDayAfterTomorrowTime = this.longMsTimeConvertToDateTime(theDayAfterTomorrowDatsmsTime);
                var pastDate = this.formatterDate2(new Date(termorrowTime));
                var nowDate = this.formatterDate2(new Date(theDayAfterTomorrowTime));
                var obj = {
                    startTime: pastDate,
                    endTime: nowDate
                };
                return obj;
            }
        };