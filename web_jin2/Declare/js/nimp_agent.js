var handlerURL = "ashx/nimp_agentHandler.ashx";

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
            var agentNames = $('#agentNames').val();
            var agentAllNames = $('#agentAllNames').val();
            table.reload("Result", {
                page: {
                    pageIndex: 1, //第一页开始
                    curr: 1
                },
                where: {
                    ope: "getList",
                    agentNames: agentNames,
                    agentAllNames: agentAllNames,
                    orderby: "id",
                    paixun:"desc",
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
                    orderby: "id",
                    paixu:"desc",
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
                    { fixed: 'id', title: '操作', toolbar: '#rowTool', width: 100 },//行内工具
                    { field: 'agentAllName', title: '客户名称', sort: true },
                    { field: 'AgentName', title: '客户简称', sort: true },
                    { field: 'contactPep', title: '联系人' },
                    { field: 'contactTel', title: '联系电话' },
                    { field: 'ypep', title: '业务联系人', hide: true },
                    { field: 'ytel', title: '业务联系电话', hide: true },
                    { field: 'cpep', title: '财务联系人', hide: true },
                    { field: 'ctel', title: '财务联系电话', hide: true },
                    { field: 'address', title: '地址' },
                    { field: 'tel', title: '电话', hide: true},
                    { field: 'userName', title: '平台用户名', hide: true },
                    { field: 'pwd', title: '平台密码', hide: true },
                    { field: 'fax', title: '传真', hide: true},
                    { field: 'bank', title: '开户行',hide: true },
                    { field: 'bank_ads', title: '开户行地址', hide: true},
                    { field: 'bank_tel', title: '开户行电话',hide: true },
                    { field: 'bank_account', title: '开户行账号',hide:true },
                    { field: 'invoiceNo', title: '税号', hide: true},
                    { field: 'in_pep', title: '录入人' },
                    { field: 'in_date', title: '录入日期', templet: "<div>{{layui.util.toDateString(d.CreateDate, 'yyyy-MM-dd')}}</div>", sort: true },
                ]],
                limits: [10, 50, 100, 999999],
                done: function (res) {   //返回数据执行回调函数
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

    // 点击行按钮
    table.on('tool(Result)', function (obj) {
        var rowData = obj.data;
        var event = obj.event;
        switch (event) {
            case "xiangqing":
                formHelper.clear('nimp_agent').edit('nimp_agent', rowData);
                break;
            default:
        }
    });

    // 点击表格工具栏
    table.on("toolbar(Result)", function (obj) {
        switch (obj.event) {
            case "Add":
                clearMain();
                formHelper.mainOpenAdd('div_maininfo', '添加用户信息', 1220, 620, false);
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

    $("#btnSearch").on('click', function () {
        Result_Active.reload();
    });

    //监听新增
    $("#btnAdd2").on('click', function () {
        clearMain();
    });

    // 表单提交
    form.on('submit(save)', function (data) {
        var nimp_agent = data.field;
        nimp_agent.id = (nimp_agent.id == null || nimp_agent.id == "") ? "0" : nimp_agent.id;
        $.ajax({
            type: "POST",
            url: handlerURL,
            data: { time: new Date(), ope: 'save', obj: JSON.stringify(nimp_agent) },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    form.val('nimp_agent', data.data);
                    layer.msg('操作成功！');
                    //$("#nimp_agent")[0].reset();
                    Result_Active.reload();
                    $("#id").val(data.id);
                } else if (data.count == -1) {
                    layer.msg(data.msg);
                }
                else {
                    layer.alert('操作失败，客户简称或客户全称已存在!');
                }
            },
            error: function (error) {
                layer.closeAll('loading');
                console.log(error);
                layer.alert("操作失败，客户简称或客户名称不能重复");
            }
        });
    });




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
            title: '客户信息',
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
        $("#nimp_agent").find("input").each(function () {
            this.value = "";
        });

    }
});