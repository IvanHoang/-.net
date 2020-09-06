
var handlerURL = "ashx/goodsSetHandler.ashx";

layui.use(['table', 'form', 'layedit', 'laydate', 'element', 'jquery', 'upload'], function () {
    var $ = layui.$;
    var table = layui.table;
    var form = layui.form
        , layer = layui.layer
        , layedit = layui.layedit
        , laydate = layui.laydate;
    var index = layer.load(1);
    var element = layui.element;
    var upload = layui.upload;

    var gloableTypeName = "";



    // 表格重载
    var Result_Active = {
        reload: function () {
            var AgentNames = $('#AgentNames').val();
            var SKUs = $('#SKUs').val();
            var GoodsNames = $('#GoodsNames').val();
            table.reload("Result", {
                page: {
                    pageIndex: 1, //第一页开始
                    curr: 1
                },
                where: {
                    ope: "getList",
                    AgentNames: AgentNames,
                    SKUs: SKUs,
                    GoodsNames: GoodsNames,
                    t_: (new Date()).valueOf(),
                }
            });

        },
        loadResult: function () {
            //表格渲染
            table.render({
                id: 'Result',
                elem: '#Result',
                url: handlerURL + "",
                page: true,
                height: 'full-160',
                fill: true,
                toolbar: '#tableTool',//表头工具
                where: {
                    ope: "getList",
                    t_: (new Date()).valueOf(),
                },
                request: {
                    pageName: 'pageIndex', //页码的参数名称，默认：page
                    limitName: 'pageSize', //每页数据量的参数名，默认：limit
                },
                cols: [[
                    { type: 'numbers', title: '序号', width: 60, fixed: true },
                    { checkbox: true, fixed: true },
                    { field: 'id', title: 'id', hide: true },
                    { fixed: 'id', title: '操作', toolbar: '#rowTool', width: 90 },//行内工具
                    { field: 'HSCode', title: '商品编号', fixed: true, width: 100 },
                    { field: 'AgentName', title: '客户简称',  width:100 },
                    { field: 'SKU', title: '货物型号', sort:true,width:120 },
                    { field: 'GoodsName', title: '品名', width:60 },
                    { field: 'gdsSpcfModelDesc', title: '规格型号', width:100 },
                    { field: 'dclUnitcd', title: '申报单位', width:100},
                    { field: 'lawfUnitcd', title: '法定单位', width: 100},
                    { field: 'Volume', title: '立方', width:60 },
                    { field: 'netWt', title: '净重',width:60 },
                    { field: 'Origin', title: '原产国',  width: 90},
                    { field: 'department', title: '部门',width:60 },
                    { field: 'InPep', title: '录入人',  width: 90},
                    { field: 'InDate', title: '录入日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 120},
                ]],
                limits: [10, 50, 100, 999999],
                done: function (res) {   //返回数据执行回调函数
                    layer.close(index);    //返回数据关闭loading
                }
            });
        }
    }

    // 渲染主体表格
    Result_Active.loadResult();
     //上传
    upload_active();
    function upload_active() {
        isUp = true;
        upload.render({
            elem: '#upGoods',
            accept: 'file',
            acceptMime: 'file/',//file/xls,file/xlsx
            exts: 'xls|xlsx',//配合accept属性，单独无效 //'doc|docx|xls|xlsx'
            multiple: false,//是否允许多文件上传。设置 true即可开启
            url: handlerURL + "?" + "ope=" + "upGoods",
            data: {
                time: new Date(),
                t_: function () {
                    return (new Date()).valueOf();
                },
            },
            before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
                layer.load(2); //上传loading
            },
            done: function (res, index, upload) {
                console.log(res)
                upIds = res.data;
                layer.closeAll('loading'); //关闭loading
                if (res.count > 0) {
                    layer.alert("上传成功！");
                    Result_Active.reload();
                }
                else {
                    if (res.msg != null && res.msg != "") {
                        layer.alert(res.msg);
                    }
                    else {
                        layer.alert("上传失败！");
                    }
                }
            },
            error: function (index, upload) {
                layer.closeAll('loading'); //关闭loading
                layer.alert("上传失败！");
            }
        })
    };

    // 点击行
    table.on('row(Result)', function (obj) {
        // 点击行添加背景色
       obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });

    // 点击行按钮
    table.on('tool(Result)', function (obj) {
        var rowData = obj.data;
        var event = obj.event;

        console.log(rowData);

        switch (event) {
            case "xiangqing":
                formHelper.clear('goodsSet').edit('goodsSet', rowData);
                break;
            default:
        }
    });

    // 点击表格工具栏
    table.on("toolbar(Result)", function (obj) {
        switch (obj.event) {
            case "Add":
                clearMain();
                formHelper.mainOpenAdd('div_maininfo', '添加商品信息', 1220, 620, false);
                break;
            case "Delete":
                var checkStatus = table.checkStatus(obj.config.id);
                if (checkStatus.data.length == 0) {
                    layer.alert('请先选择要删除的数据行！');
                    return;
                }
                var codeId = "";
                for (var i = 0; i < checkStatus.data.length; i++) {
                    if (i == 0) {
                        codeId = checkStatus.data[i].id;
                    }
                    else {
                        codeId += "," + checkStatus.data[i].id;
                    }
                }
                layer.confirm("您确定要删除吗？", function () {
                    $.ajax({
                        type: "POST",
                        url: handlerURL,
                        data: { ope: 'del', ids: codeId, t_: (new Date()).valueOf() },
                        dataType: "json",
                        beforeSend: function () {
                            layer.load(2);
                        },
                        success: function (data) {
                            layer.closeAll('loading');
                            if (data.count > 0) {
                                layer.alert('删除成功！');
                            }
                            else {
                                layer.alert('删除失败!');
                            }
                            Result_Active.reload();
                        }
                    })
                })
                break;
        }
    });

    $("#btnSearch").on('click', function () {
        Result_Active.reload();
    });

    //监听新增
    $("#btnAdd2").on('click', function () {
        clearMain();
    });

    // 表单提交
    form.on('submit(save)', function (data) {
        var goodsSet = data.field;
        goodsSet.id = goodsSet.id == "" ? 0 : goodsSet.id;
        $.ajax({
            type: "POST",
            url: handlerURL,
            data: { time: new Date(), ope: 'save', obj: JSON.stringify(goodsSet) },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    form.val('goodsSet', data.data);
                    layer.msg('操作成功！');
                    Result_Active.reload();
                } else if (data.count == -1) {
                    layer.msg(data.msg);
                }
                else {
                    layer.alert('操作失败!');
                }
            },
            error: function (error) {
                layer.closeAll('loading');
                console.log(error);
                layer.alert("操作错误，客户简称重复！");
            }
        });
    });

    var LayuiCreateSelect = function (selectId, url, value) {//value  设置加载完成时所选定的值
        //数据请求
        $.post(url, function (res) {
            //判断id是否有"#"
            if (selectId.indexOf('#') != 0) {
                selectId = '#' + selectId;
            }
            var data = JSON.parse(res);
            console.log(data);
            $(selectId).empty();//清空该元素
            //创建option
            for (var k in data.data) {
                $(selectId).append("<option value='" + data.data[k].text + "'>" + data.data[k].text + "</option>");
            }
            //使用layui下拉框的必要代码
            layui.use('form', function () {
                var form = layui.form;
                //设置选中值
                if (value != undefined && value != null && value != '') {
                    $(selectId).val(value);
                }
                form.render();//重载表单
            });
        });
    }

    LayuiCreateSelect("AgentName", "/Declare/ashx/goodsSetHandler.ashx?ope=kehu", "");
    LayuiCreateSelect("Origin", "/Declare/ashx/goodsSetHandler.ashx?ope=yuanchanguo", "");
    LayuiCreateSelect("lawfUnitcd", "/Declare/ashx/goodsSetHandler.ashx?ope=fadingdanwei", "");
    LayuiCreateSelect("dclUnitcd", "/Declare/ashx/goodsSetHandler.ashx?ope=shenbaodanwei", "");

    



    var formHelper = {
        clear: function (filterName) {
            var data = form.val(filterName);
            for (var key in data) {
                data[key] = "";
            }
            form.val(filterName, data);
            form.render();
            return this;
        },
        edit: function (filterName, data) {
            form.val(filterName, data);
            infoopen();
        },
        open: function (filterName) {
            infoopen();
        },
        openTab: function (id, title, width, height, isFull) {
            var maininfo = layer.open({
                type: 1,
                title: title,
                content: $('#' + id),
                maxmin: false,
                area: [width + 'px', height + 'px'], //宽高
            });
            if (isFull) {
                layer.full(maininfo);
            }
        },
        mainOpenAdd: function (id, title, width, height, isFull) {
            this.openTab(id, title, width, height, isFull);
        },
    }



    function infoopen() {
        var maininfo = layer.open({
            type: 1,
            title: '商品信息',
            content: $('#div_maininfo'),
            maxmin: false,
            area: ['1220px', '620px'], //宽高
            yes: function (index, layero) {
                Result_Active.reload();
            },
            cancel: function (index, layero) {
                Result_Active.reload();
            }
        });
    }
    function clearMain() {
        $("#goods").find("input").each(function () {
            this.value = "";
        });

    }
});