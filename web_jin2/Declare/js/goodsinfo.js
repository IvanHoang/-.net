
var handlerURL = "ashx/goodsinfoHandler.ashx";

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
    var laydate = layui.laydate;
    var gloablejobnumber = "";

    ///类型
    var types = [];


    // 表格重载
    var Result_Active = {
        reload: function () {
            var transportNos = $('#transportNos').val();
            var jobnumbers = $('#jobnumbers').val();
            var bondInvtNos = $('#bondInvtNos').val();
            var agentNames = $('#agentNames').val();
            var entryNos = $('#entryNos').val();
            var BL_Nos = $('#BL_Nos').val();
            var customNOs = $('#customNOs').val();
            var invoiceNos = $('#invoiceNos').val();
            var ordernos = $('#ordernos').val();
            table.reload("Result", {
                page: {
                    pageIndex: 1, //第一页开始
                    curr: 1
                },
                where: {
                    ope: "getList",
                    transportNos: transportNos,
                    jobnumbers: jobnumbers,
                    bondInvtNos: bondInvtNos,
                    agentNames: agentNames,
                    entryNos: entryNos,
                    BL_Nos: BL_Nos,
                    customNOs: customNOs,
                    invoiceNos: invoiceNos,
                    ordernos: ordernos,
                    orderby: "id",
                    paixun: "desc",
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
                height: 'full-140',
                fill: true,
                toolbar: '#tableTool',//表头工具
                where: {
                    ope: "getList",
                    orderby: "id",
                    paixun: "desc",
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
                    { fixed: 'id', title: '操作', toolbar: '#rowTool' },//行内工具
                    { field: 'jobnumber', title: '业务流水号', fixed: true },
                    { field: 'agentName', title: '客户简称' },
                    { field: 'bondInvtNo', title: 'QD号' },
                    { field: 'entryNo', title: '报关单号' },
                    { field: 'transport', title: '运输工具', hide: true },
                    { field: 'BL_No', title: '提单号' },
                    { field: 'customNO', title: '客户自编号' },
                    { field: 'planDate', title: '预计入库日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 120, hide: true },
                    { field: 'status', title: '入库状态', hide: true },
                    { field: 'inoutdate', title: '出入库日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 120, hide: true },
                    { field: 'c_jobumber', title: '客服中心流水号', hide: true },
                    { field: 'invoiceNo', title: '发票号' },
                    { field: 'transportNo', title: '运单号' },
                    { field: 'orderno', title: '订单号' },
                    { field: 'status', title: '入库状态', hide: true },
                    { field: 'remark', title: '备注', hide: true },
                    { field: 'inDep', title: '录入部门', hide: true },
                    { field: 'inpep', title: '录入人', hide: true },
                    { field: 'indate', title: '录入日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 120 },
                ]],
                limits: [10, 50, 100, 999999],
                done: function (res) {   //返回数据执行回调函数
                    types = res.data;
                    layer.close(index);    //返回数据关闭loading
                }
            });
        },
        loadDetail: function (jobnumber) {
            table.render({
                id: 'detail_result',
                elem: '#detail_result',
                url: handlerURL + "",
                height: 'full-140',
                fill: true,
                toolbar: '#tableTool_detail',//表头工具
                where: {
                    ope: "getListDetail",
                    jobnumbers: jobnumber,
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
                    { fixed: 'id', title: '操作', toolbar: '#rowTool_detail' },//行内工具
                    { field: 'jobnumber', title: '业务流水号', fixed: true },
                    { field: 'putrecSeqno', title: '备案序号' },
                    { field: 'SKU', title: '货物型号' },
                    { field: 'GoodsName', title: '品名' },
                    { field: 'HSCode', title: 'HSCode' },
                    { field: 'gdsSpcfModelDesc', title: '规格型号' },
                    { field: 'dcl_QTY', title: '申报数量', hide: true, hide: true },
                    { field: 'dclUnitcd', title: '申报单位' },
                    { field: 'law_QTY', title: '法定数量', hide: true },
                    { field: 'lawfUnitcd', title: '法定单位', hide: true },
                    { field: 'Volume', title: '立方' },
                    { field: 'grossWt', title: '毛重', hide: true },
                    { field: 'netWt', title: '净重' },
                    { field: 'Origin', title: '原产国', hide: true },
                    { field: 'batch', title: '批号', hide: true },
                    { field: 'LPN', title: '序列号', hide: true },
                    { field: 'productionDate', title: '生产日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", hide: true },
                    { field: 'validDate', title: '有效期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", hide: true },
                    { field: 'Location', title: '库位', hide: true },
                    { field: 'unitprice', title: '单价', hide: true },
                    { field: 'totalamount', title: '总金额', hide: true },
                    { field: 'curr', title: '币种', hide: true },
                    { field: 'batch', title: '批号', hide: true },
                    { field: 'inDep', title: '录入部门', hide: true },
                    { field: 'goodsStatus', title: '货物状态', hide: true },
                    { field: 'InDate', title: '录入日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 120 },
                ]],
                limits: [10, 50, 100, 999999],
                done: function (res) {   //返回数据执行回调函数
                    types = res.data;
                    layer.close(index);    //返回数据关闭loading
                }
            });
        },
    }

    // 渲染主体表格
    Result_Active.loadResult();

    // 点击行
    table.on('row(Result)', function (obj) {
        // 点击行添加背景色
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });
    table.on('row(detail_result)', function (obj) {
        // 点击行添加背景色
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });

    // 点击行按钮
    table.on('tool(Result)', function (obj) {
        console.log(obj);
        var rowData = obj.data;
        var event = obj.event;

        switch (event) {
            case "xiangqing":
                gloablejobnumber = rowData.jobnumber;
                formHelper.clear('main').edit('main', rowData);
                Result_Active.loadDetail(rowData.jobnumber);
                upload_active();
                break;
            default:
        }
    });
    // 点击行按钮
    table.on('tool(detail_result)', function (obj) {
        console.log(obj);
        var rowData = obj.data;
        var event = obj.event;

        switch (event) {
            case "resultxiangqing":
                cleardetail();
                formHelper.edit('detail', rowData);
                formHelper.mainOpenAdd('div_detailInfo', '添加表体信息', 1220, 620, false);
                Result_Active.reload();
                break;
            default:
        }
    });

    // 点击表格工具栏
    table.on("toolbar(Result)", function (obj) {
        switch (obj.event) {
            case "Add":
                clearMain();
                gloablejobnumber = '';
                formHelper.mainOpenAdd('div_maininfo', '添加商品信息', 1220, 620, false);
                Result_Active.loadDetail("detail");
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
    table.on('toolbar(detail_result)', function (obj) {
        switch (obj.event) {
            case "detail_add":
                cleardetail();
                formHelper.mainOpenAdd('div_detailInfo', '添加表体信息', 1220, 620, false);
                element.tabChange('tab1', 1);
                break;
            case "detail_delete":
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
                        data: { ope: 'deldetail_delete', ids: codeId, t_: (new Date()).valueOf() },
                        dataType: "json",
                        beforeSend: function () {
                            layer.load(2);
                        },
                        success: function (data) {
                            layer.closeAll('loading');
                            if (data.count > 0) {
                                layer.alert('删除成功！');
                                Result_Active.reload();
                            }
                            else {
                                layer.alert('删除失败!');
                            }
                        }
                    })
                })
                break;
        }
    });


    element.on('tab(tab1)', function (data) {
        console.log(data.index);
        if (1 == data.index) {
            if (gloablejobnumber == "") {
                element.tabChange('tab1', 0);
                layer.alert("请先选择一条信息");
            }
        }
    });


    //上传
    // upload_active();
    function upload_active() {
        isUp = true;
        //alert('dd');
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
                    table.reload("detail_result", {
                        page: {
                            pageIndex: 1, //第一页开始
                            curr: 1
                        },
                        where: {
                            ope: "getListDetail",
                            jobnumbers: gloablejobnumber,
                            t_: (new Date()).valueOf(),
                        },
                        done: function () {
                            Result_Active.reload();
                            upload_active();
                        }
                    });
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

    //监听新增
    $("#btnAdd2").on('click', function () {
        clearMain();
        //$("#jobnumber").val(gloablejobnumber);
    });
    $("#detail_clear").on('click', function () {
        cleardetail();
        //$("#jobnumber").val(gloablejobnumber);
    });
    $("#btnSearch").on('click', function () {
        Result_Active.reload();
    });
    // 表单提交
    form.on('submit(save)', function (data) {
        var goods = data.field;
        goods.id = goods.id == "" ? 0 : goods.id;
        goods.detail_id = goods.detail_id == "" ? 0 : goods.detail_id;
        $.ajax({
            type: "POST",
            url: handlerURL,
            data: { time: new Date(), ope: 'save', obj: JSON.stringify(goods) },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    gloablejobnumber = data.jobnumber;
                    $("#jobnumber").val(data.jobnumber);
                    // form.val('nimp_main', data.data);
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
                layer.alert("操作错误");
            }
        });
    });

    //detail_save
    // 表体提交
    form.on('submit(detail_save)', function (data) {
        var form_data = data.field;
        form_data.id = form_data.id == "" ? 0 : form_data.id;
        form_data.jobnumber = gloablejobnumber;
        $.ajax({
            type: "POST",
            url: handlerURL,
            data: { time: new Date(), ope: 'save_detail', obj: JSON.stringify(form_data) },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    layer.msg('操作成功！');
                    $("#id").val(data.count);
                    layer.closeAll('loading');
                    table.reload("detail_result", {
                        page: {
                            pageIndex: 1, //第一页开始
                            curr: 1
                        },
                        where: {
                            ope: "getListDetail",
                            jobnumbers: form_data.jobnumber,
                            t_: (new Date()).valueOf(),
                        }
                    });
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
                layer.alert("操作错误");
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

    LayuiCreateSelect("agentName", "/Declare/ashx/goodsinfoHandler.ashx?ope=kehu", "");
    LayuiCreateSelect("Origin", "/Declare/ashx/goodsinfoHandler.ashx?ope=yuanchanguo", "");
    LayuiCreateSelect("transport", "/Declare/ashx/goodsinfoHandler.ashx?ope=yunshugongju", "");
    LayuiCreateSelect("SKU", "/Declare/ashx/goodsinfoHandler.ashx?ope=huowuxinghao", "");
    LayuiCreateSelect("curr", "/Declare/ashx/goodsinfoHandler.ashx?ope=bizhong", "");

    laydate.render({
        elem: '#inoutdate',
        value: new Date()
    });
    laydate.render({
        elem: '#planDate',
        value: new Date()
    });
    laydate.render({
        elem: '#productionDate',
        value: new Date()
    });
    laydate.render({
        elem: '#validDate',
        value: new Date()
    });


    var formHelper = {
        clear: function (filterName) {
            var data = form.val(filterName);
            for (var key in data) {
                data[key] = "";
            }
            form.val(filterName, data);
            $("#jobnumbers").val('');
            form.render();
            return this;
        },
        edit: function (filterName, data) {
            form.val(filterName, data);
            infoopen();
        },
        openTab: function (id, title, width, height, isFull) {

            var maininfo = layer.open({
                type: 1,
                title: title,
                content: $('#' + id),
                maxmin: false,
                area: [width + 'px', height + 'px'], //宽高
                success: function (layero, index) {
                    element.tabChange('tab1', 0);
                }
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
            },
            success: function () {
                element.tabChange("tab1", 0);
            }
        });
    }
    function clearMain() {
        $("#div_maininfo").find("input", "select").each(function (i, target) {
            if (!$(target).hasClass("date")) {
                $(target).val('');
            }
        });
    }
    function cleardetail() {
        $("#div_detailInfo").find("input", "select").each(function (i, target) {
            if (!$(target).hasClass("date")) {
                $(target).val('');
            }
        });
    }
});