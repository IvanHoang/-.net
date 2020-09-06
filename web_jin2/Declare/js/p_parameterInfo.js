
var handlerURL = "ashx/p_parameterHandler.ashx";

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

    var gloableTypeID = 0;

    ///类型
    var types = [];


    // 表格渲染
    var Result_Active = {
        reload: function () {

            table.reload("Result", {
                page: {
                    pageIndex: 1, //第一页开始
                    curr: 1
                },
                where: {
                    ope: "getList",
                    TypeID: gloableTypeID,
                    t_: (new Date()).valueOf(),
                }
            });
        },
        loadMain: function () {
            //表格渲染
            table.render({
                id: 'Main',
                elem: '#Main',
                url: handlerURL + "",
                height: 'full-140',
                fill: true,
                where: {
                    ope: "getMainList",
                    t_: (new Date()).valueOf(),
                },
                request: {
                    pageName: 'pageIndex', //页码的参数名称，默认：page
                    limitName: 'pageSize', //每页数据量的参数名，默认：limit
                },
                cols: [[
                    { type: 'numbers', title: '序号', width: 60, fixed: true },
                    { field: 'id', title: 'id', hide: true },
                    {
                        field: 'TypeName', title: '参数类型', fixed: true, templet: function (d) {
                            return '<a href="#" style="color: blue;">' + d.TypeName + '</a>';
                        }
                    },
                ]],
                done: function (res) {   //返回数据执行回调函数
                    types = res.data;
                    console.log(types);
                    layer.close(index);    //返回数据关闭loading
                }
            });
        },
        loadDetail: function (typeID) {
            //表格渲染
            table.render({
                id: 'Result',
                elem: '#Result',
                url: handlerURL + "",
                page: true,
                height: 'full-140',
                fill: true,
                toolbar: '#tableTool',//表头工具
                where: {
                    ope: "getList",
                    TypeID: typeID,
                    t_: (new Date()).valueOf(),
                },
                request: {
                    pageName: 'pageIndex', //页码的参数名称，默认：page
                    limitName: 'pageSize', //每页数据量的参数名，默认：limit
                },
                cols: [[
                    { field: 'Row', title: '序号', width: 60, fixed: true },
                    { checkbox: true, fixed: true },
                    { field: 'id', title: 'id', hide: true },
                    { field: 'id', title: '操作', toolbar: '#rowTool', width: 100 },//行内工具
                    {
                        field: 'TypeID', title: '参数类型', templet: function (d) {
                            return getTypeName(d.TypeID);
                        }
                    },
                    { field: 'Details', title: '参数名称' },
                    { field: 'inPep', title: '创建人', sort: true, width: 120 },
                    { field: 'Remark', title: '备注', sort: true },
                    { field: 'inDate', title: '创建日期', templet: "<div>{{layui.util.toDateString(d.inDate, 'yyyy-MM-dd')}}</div>", sort: true, width: 160 }
                ]],
                limits: [10, 50, 100, 999999],
                done: function (res) {   //返回数据执行回调函数
                    layer.close(index);    //返回数据关闭loading
                }
            });
        }
    }

    // 渲染主体表格
    Result_Active.loadMain();

    // 点击行
    table.on('row(Main)', function (row) {
        console.log(row)
        var typeID = row.data.TypeID;
        gloableTypeID = typeID;
        Result_Active.loadDetail(typeID);
    });

    // 点击行按钮
    table.on('tool(Result)', function (obj) {
        var rowData = obj.data;
        var event = obj.event;
        switch (event) {
            case "xiangqing":
                formHelper.clear('Main').edit('Main', rowData);
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
                var checkData = checkStatus.data;
                if (checkData.length <= 0) {
                    layer.msg('未选中数据');
                    return;
                }
                layer.confirm("您确定要删除吗？", function () {
                    for (var key in checkData) {
                        table.DeleteSingleGoods(checkData[key].id);
                    }
                });
                break;
        }
    });


    //监听新增
    $("#btnAdd2").on('click', function () {
        clearMain();
        $("#TypeID").val(gloableTypeName);
    });
    // 表单提交
    form.on('submit(save)', function (data) {
        var goods = data.field;
        goods.id = goods.id == "" ? 0 : goods.id;
        goods.TypeID = gloableTypeID;
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
                    form.val('p_parameterDetail', data.data);
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

    // 删除一行数据
    table.DeleteSingleGoods = function (id) {
        $.ajax({
            type: "POST",
            url: handlerURL,
            data: { time: new Date(), ope: 'del', id: id, t_: (new Date()).valueOf() },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    layer.msg('删除成功！');
                    Result_Active.reload();
                }
                else {
                    layer.alert('删除失败!');
                }
            }
        });
    }




    var getTypeName = function (typeId) {
        for (var i = 0; i < types.length; i++) {
            var type = types[i];
            if (type.TypeID == typeId) {
                return type.TypeName;
            }
        }
        return "未知名称";
    }

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
        $("#info").find("input").each(function () {
            this.value = "";
        });

    }
});