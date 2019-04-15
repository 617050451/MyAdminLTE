<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AdminLTE.Shop.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
    <title>购物车</title>
    <link rel="stylesheet" href="/shop/css/common.css" />
    <style type="text/css">
        html {
            font-size: 10px;
            font-family: "Microsoft YaHei";
        }

        * {
            padding: 0;
            margin: 0;
            color: #000;
        }

        img {
            width: 100%;
        }

        .header {
            width: 100%;
        }
        /*底部样式*/

        footer {
            width: 100%;
            height: 5rem;
            display: flex;
            justify-content: space-between;
            position: fixed;
            bottom: 0;
            background: #fff;
            border-top: 0.1rem solid #e9e9e9;
            font-size: 1.6rem;
            align-items: center;
            z-index: 101;
        }

            footer .count_num {
                width: 3.6rem;
                height: 3.6rem;
                margin: 0 1.4rem;
                background: url("img/cart.png") no-repeat;
                background-size: contain;
                position: relative;
            }

                footer .count_num span {
                    position: absolute;
                    top: -0.2rem;
                    right: -0.2rem;
                    background: #000;
                    color: #fff;
                    border-radius: 2rem;
                    width: 1.2rem;
                    height: 1.2rem;
                    text-align: center;
                    line-height: 1.2rem;
                    font-size: 1.2rem;
                    padding: 0.2rem;
                }

            footer .num_price {
                display: flex;
                justify-content: flex-end;
                align-items: center;
            }

            footer .submit {
                padding: 0.6rem 1.8rem;
                background: #ffce5a;
                border-radius: 0.4rem;
                margin-right: 4%;
            }

            footer .price::before {
                content: '合计：'
            }

            footer .price::after {
                content: '元'
            }
        /*左侧列表样式*/

        .content {
            display: flex;
            justify-content: flex-start;
            flex-wrap: nowrap;
        }

        .type_list {
            width: 20%;
        }

            .type_list li {
                background: #f2f2f2;
                font-size: 1.2rem;
                padding: 0.6rem 0;
                height: 2.2rem;
                line-height: 2.2rem;
                text-align: center;
                position: relative;
            }

                .type_list li span {
                    position: absolute;
                    left: 0;
                    top: 1.2rem;
                    height: 1.2rem;
                    width: 0.4rem;
                    background: #f2f2f2;
                }

                .type_list li.select span {
                    background: #f85a62;
                }

                .type_list li.select {
                    font-weight: 800;
                    background: #fff;
                }
        /*右侧列表样式*/

        .item_list {
            width: 80%;
            overflow-y: scroll;
            border-right: 0.1rem solid #e9e9e9;
            border-left: 0.1rem solid #e9e9e9;
        }

            .item_list .item_type {
                font-size: 1.6rem;
                font-weight: 800;
                margin: 1rem;
            }

            .item_list li {
                font-size: 1.6rem;
                font-weight: 800;
                display: flex;
                justify-content: space-between;
                margin: 2rem 0;
                align-items: flex-end;
                flex-wrap: nowrap;
            }

                .item_list li div {
                    height: 6rem;
                    overflow: hidden;
                }

            .item_list .item_img {
                width: 30%;
                display: flex;
                justify-content: center;
            }

                .item_list .item_img img {
                    height: 100%;
                    width: auto;
                    margin: 0 auto;
                }

            .item_list .item_content {
                width: 40%;
            }

        .item_title {
            font-size: 1.4rem;
            font-weight: 800;
        }

        .item_activity {
            font-size: 1.2rem;
            border: 0.1rem solid #e74040;
            border-radius: 1.6rem;
            color: #e74040;
            padding: 0rem 0.2rem;
            width: 5rem;
            text-align: center;
            height: 1.4rem;
            line-height: 1.4rem;
            margin: 0.4rem 0;
        }

        .item_price span {
            font-size: 1.4rem;
            color: #e74040;
            font-weight: 800;
        }

            .item_price span::before {
                content: "¥";
                font-size: 1.2rem;
            }

        .item_price s {
            font-size: 1.2rem;
            color: #999999;
            margin-left: 0.6rem;
        }

            .item_price s::before {
                content: "¥";
                font-size: 1.2rem;
            }

        .item_list li .calc {
            width: 20%;
            display: flex;
            justify-content: flex-end;
            align-items: flex-end;
            flex-wrap: nowrap;
            margin-right: 2%;
        }

            .item_list li .calc p {
                width: 1.6rem;
                height: 1.6rem;
                margin: 0 1%;
                border-radius: 1.8rem;
                overflow: hidden;
                text-align: center;
                line-height: 1.6rem;
                display: none;
            }

            .item_list li .calc .item_sub {
                background: #000 url("img/03.png") no-repeat;
                background-size: contain;
                display: none;
            }

            .item_list li .calc .item_add {
                background: #000 url("img/02.png") no-repeat;
                background-size: contain;
                display: block;
            }

        .content .item_list li .select p {
            display: block;
        }
        /*购物车列表*/

        .select_list_parent {
            position: fixed;
            z-index: 100;
            top: 100%;
            width: 100%;
            background: #fff;
            transition: all 1s;
        }

        .select_num {
            width: 96%;
            margin: 0 auto;
            padding: 0.4rem;
            border-bottom: 0.1rem solid #e9e9e9;
        }

        .select_list {
            width: 96%;
            margin: 0 auto;
        }

            .select_list li {
                display: flex;
                justify-content: flex-start;
                margin: 0.4rem 0;
            }

                .select_list li .name {
                    width: 50%;
                }

                .select_list li .price {
                    width: 20%;
                    font-size: 1.4rem;
                }

                    .select_list li .price::before {
                        content: '¥'
                    }

                .select_list li .calc {
                    width: 30%;
                    display: flex;
                    justify-content: flex-end;
                    align-items: center;
                    flex-wrap: nowrap;
                    margin-right: 2%;
                }

                    .select_list li .calc p {
                        width: 1.6rem;
                        height: 1.6rem;
                        margin: 0 1%;
                        border-radius: 1.8rem;
                        overflow: hidden;
                        text-align: center;
                        line-height: 1.6rem;
                        font-size: 1.6rem;
                    }

                    .select_list li .calc .item_sub {
                        background: #000 url("img/03.png") no-repeat;
                        background-size: contain;
                    }

                    .select_list li .calc .item_add {
                        background: #000 url("img/02.png") no-repeat;
                        background-size: contain;
                    }
.form-group {
    margin-bottom: 15px;
}
label {
    display: inline-block;
    max-width: 100%;
    margin-bottom: 5px;
    font-weight: 700;
font-family: "Helvetica Neue",Helvetica,Arial,sans-serif;
    font-size: 14px;
    line-height: 1.42857143;
    color: #333;
}
.form-control {
    display: block;
    width: 100%;
    height: 25px;
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}
    </style>
</head>
<body>
    <!--<div style="background: #2a2a2a; height: 1rem;"></div>-->
    <div class="header">
        <img src="img/header.png" />
    </div>
    <div class="content">
        <ul class="type_list">
            <li class="select"><span></span>扫码买单</li>
            <li><span></span>商品管理</li>
        </ul>
        <ul class="item_list">
            <div class="item_type" data-type="扫码买单">已选择商品</div>
            <div class="item_type" style="display:none;" data-type="商品管理">
                <p class="addproduct" style="padding: 0.6rem 1.8rem;background: #009f95;border-radius: 0.4rem;width:70px;color:white;">添加商品</p>
            </div>
        </ul>
    </div>
    <footer>
        <div class="num_price">
            <div class="count_num" data-type="0"><span>0</span></div>
            <p class="price">0</p>
        </div>
        <p class="submit">已买单</p>
    </footer>
    <div class="layershowinfo" style="display:none;">
        <div style="margin:0 auto;text-align:center;">
             <div class="form-group"></div>
            <div class="form-group">
                <input type="text" class="form-control" id="code" placeholder="请输入商品条码" />
            </div>
            <div class="form-group">
                <input type="text" class="form-control" id="name" placeholder="请输入商品名称" />
            </div>
            <div class="form-group">
                <input type="text" class="form-control" id="price" placeholder="请输入商品单价" />
            </div>
             <div class="form-group" style="margin:0 auto;text-align:center;">
                  <p class="saveproduct" style="height:30px;line-height:30px;font-size:20px;padding: 0.6rem 1.8rem;background: #009f95;border-radius: 0.4rem;width:70px;color:white;display:inline-block;" onclick="saveProdectClick()">保存</p>
            </div>
        </div>
    </div>
</body>
<script src="js/jquery.min.js"></script>
<script src="js/jquery.fly.min.js"></script>
<script src="../../Script/layer-v3.1.0/layer/layer.js"></script>
<script src="../Script/js/DataPageHandle.js"></script>
<script src="http://res.wx.qq.com/open/js/jweixin-1.4.0.js"></script>
<script>
    var productlist = [];
    var configJSON = JSON.parse('<%=WxConfigJson%>');
    wx.config({
        debug: false,
        appId: configJSON.appId,
        timestamp: configJSON.timeStamp,
        nonceStr: configJSON.nonceStr,
        signature: configJSON.signature,
        jsApiList: [
            'checkJsApi',
            'scanQRCode',
        ]
    });
    wx.ready(function () {

    })
    $(".type_list li").click(function () {
        var title = $(this).text();
        $(".item_type").hide();
        $(".item_list div[data-type='" + title + "']").show();
    })
    function EventScanQRCode(ac) {
        wx.scanQRCode({
            // 默认为0，扫描结果由微信处理，1则直接返回扫描结果
            needResult: 1,
            desc: 'scanQRCode desc',
            success: function (res) {
                var reStr = res.resultStr;
                //商品条形码，取","后面的
                if (reStr.indexOf(",") >= 0) {
                    var tempArray = reStr.split(',');
                    reStr = tempArray[1];
                }
                ac(reStr);
            }
        });
    }
    function FindProduct(code) {
        var bl = false;
        for (var i = 0; i < productlist.length; i++) {
            if (productlist[i].Code == code) {
                bl = true;
                break;
            }
        }
        if (bl == false) {
            layer.msg("未查询到此商品！");
            return false;
        }
        if ($(".item_list li[data-code='" + code + "']").length > 0)
            $(".item_list li:last .item_add").click();
        else {
            var strhtml = '<li data-code="' + code + '"><div class="item_img"><img src="#" /></div>';
            strhtml += '<div class="item_content">'
            strhtml += '<p class="item_title">' + productlist[i].Name + '</p>';
            strhtml += '<p class="item_activity">限时特价</p>';
            strhtml += '<p class="item_price"><span>' + productlist[i].Price + '</span></p></div>';
            strhtml += '<div class="calc"><p class="item_sub" onclick="num_sub(this)"></p>';
            strhtml += '<p class="item_num">0</p> <p class="item_add" onclick="num_add(this)"></p></div></li>';
            $(".item_list").append(strhtml);
            $(".item_list li:last .item_add").click();
        }
    }
    function saveProdectClick() {
        var code = $(".layui-layer #code").val();
        var name = $(".layui-layer #name").val();
        var price = $(".layui-layer #price").val();
        var param = {};
        param.gettype = "SaveProdect";
        param.code = code;
        param.name = name;
        param.price = price;
        //ajax请求数据
        $.ajax({
            type: "GET",
            url: "index.aspx",
            cache: false,  //禁用缓存
            data: param,  //传入组装的参数
            dataType: "text",
            async: false,
            success: function (result) {
                if (result == 1) {
                    layer.msg("添加成功", { icon: 1, time: 1000 }, function () {
                        layer.closeAll();
                    });
                } else {
                    layer.msg("添加失敗");
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
    $(".addproduct").click(function () {
        EventScanQRCode(function (code) {
            //页面层
            layer.open({
                type: 1,
                skin: 'layui-layer-rim', //加上边框
                area: ['80%', '40%'], //宽高
                content: $(".layershowinfo").html(),
                success: function () {
                    $(".layui-layer #code").val(code);
                }
            });
        })
    })
    $(function () {
        var param = {};
        param.gettype = "GetDataList";
        param.option = 4;
        param.limit = 500;//页面显示记录条数，在页面显示每页显示多少项的时候
        param.start = 1;//开始的记录序号
        param.page = 1;//当前页码;
        //ajax请求数据
        $.ajax({
            type: "GET",
            url: "/Ajax/GetData.ashx",
            cache: false,  //禁用缓存
            data: param,  //传入组装的参数
            dataType: "json",
            async: false,
            success: function (result) {
                if (result != false) {
                    productlist = result.data;
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
    })
    $(".header").click(function () {
        EventScanQRCode(FindProduct)
    });
    $(".submit").click(function () {
        window.location.reload();
    })
    var header = $(".header").height();
    var footer = $(".footer").height();
    var window11 = $(window).height();
    window11 = window11 - header - 40;
    var offset = $('.num_price').offset();
    //    设置产品列表可视区域高度
    $(".item_list").css('height', window11 + 'px');
    //    左侧产品类型选择
    $('.type_list li').click(function () {
        $('.type_list li').removeClass('select');
        $(this).addClass('select');
    });
    $('.item_add').click(function (event) {
        //        alert('sdf');
        var thisItem = $(this);
        var flyer = thisItem.clone().css({
            "background": "#000",
            "width": "20px",
            "height": "20px",
            "border-radius": "20px"
        });
        flyer.fly({
            start: {
                width: 20,
                height: 20,
                left: event.pageX - 20,
                top: event.pageY - 20
            },
            end: {
                left: offset.left + 40,
                top: offset.top + 0,
                width: 20,
                height: 20
            },
            onEnd: function () {

                this.destory();
            }, //      autoPlay: false,//是否直接运动,默认true
            speed: 1.1, //越大越快，默认1.2
            vertex_Rtop: 100, //运动轨迹最高点top值，默认20
            onEnd: function () {
                flyer.remove();
            } //结束回调
        });
    });
    $(".count_num").click(function () {
        var this_type = parseInt($(this).attr('data-type'));
        if (this_type == 0) {
            var height = parseInt($(".select_list_parent").height());
            $(".select_list_parent").css('top', 'calc(100% - ' + (height + 60) + 'px)');
            $(this).attr('data-type', '1')
        } else {
            $(".select_list_parent").css('top', '100%');
            $(this).attr('data-type', '0')
        }
    });
    var count_num = 0;
    var parice = 0;
    var this_parice;
    var this_num;
    function num_add(on_this) {
        //总数和总价格
        count_num++;
        $('.count_num span').html(count_num);
        this_parice = $(on_this).parent().parent().children('.item_content').children('.item_price').children('span').text();
        this_parice = parseFloat(this_parice);
        parice += this_parice;
        $('.price').html(parice.toFixed(2));
        //当前商品数量
        this_num = $(on_this).parent().parent().children('.calc').children('.item_num');
        var get_this_num = parseInt(this_num.text()) + 1;
        this_num.html(get_this_num);
        this_num.parent().addClass('select');
    }
    function num_sub(on_this) {
        //总数和总价格
        count_num--;
        $('.count_num span').html(count_num);
        this_parice = $(on_this).parent().parent().children('.item_content').children('.item_price').children('span').text();
        this_parice = parseFloat(this_parice);
        parice -= this_parice;
        $('.price').html(parice.toFixed(2));
        //        当前商品数量
        this_num = $(on_this).parent().parent().children('.calc').children('.item_num');
        var get_this_num = parseInt(this_num.text()) - 1;
        this_num.html(get_this_num);
        if (get_this_num == 0) {
            this_num.parent().removeClass('select');
        }
    }
</script>
</html>