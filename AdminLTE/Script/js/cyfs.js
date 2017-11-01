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
        //全选
        function OnCheckboxSelectAll(){
            if ($("#selectAll").prop("checked")) {
                $("input[name='checkboxGuid']").prop("checked", 'true');//全选 
            } else {
                $("input[name='checkboxGuid']").prop("checked", '');//取消全选 
            }
        }