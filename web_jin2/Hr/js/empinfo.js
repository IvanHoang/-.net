
var handlerURL = "ashx/empinfo.ashx";
//===========================================================================
//表头

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


    //表格渲染
    table.render({
        id: 'Result',
        elem: '#Result',
        url: handlerURL + "",
        page: true,
        height: 'full-140',
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
             { field: 'Row', title: '序号', width: 60, fixed: true },
            { checkbox: true, fixed: true },
            { field: 'id', title: 'id', hide: true },
            {  title: '操作', toolbar: '#rowTool', width: 100 },//行内工具
     
            { field: 'Name', title: '员工姓名', sort: true, width: 100 },
            { field: 'USname', title: '系统用户名', sort: true, width: 150 },
        
               { field: 'sex', title: '性别', sort: true, width: 100 },
            { field: 'tel', title: '电话', sort: true, width: 120 },
            { field: 'email', title: '邮箱', sort: true, width: 200 },
            { field: 'Address', title: '地址', sort: true, width: 280 },
            { field: 'inpep', title: '创建人', sort: true, width: 135 },
            { field: 'indate', title: '创建时间', sort: true, width: 135 },
        ]],
        limits: [10, 50, 100, 999999],
        done: function (res) {   //返回数据执行回调函数
            layer.close(index);    //返回数据关闭loading

        }
    });
    //监听行单击事件（单击事件为：rowDouble）
    table.on('row(Result)', function (obj) {
        var data = obj.data;

        var ishide = $("#MainInfo").css("display") == "none";
        $("#hid").val(data.id);

        //$("#MainInfo").show();
        //if (ishide) {
        //    table.reload("Result", {
        //        height: 'full-286', //高度最大化减去差值
        //        page: {
        //            pageIndex: 1 //第一页开始
        //        },
        //        where: {
        //            ope: "getList",
        //            t_: (new Date()).valueOf(),
        //        }
        //    });
        //}

        //infoopen();

        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    });
    table.on('tool(Result)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        var data = obj.data; //获得当前行数据
        var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
        var tr = obj.tr; //获得当前行 tr 的DOM对象
        switch (layEvent) {
            case "xiangqing":
                clearMain();
                editMain(data.id);
                break;
        }
    });
    function editMain(id) {
        layui.jquery.ajax({
            type: "post",
            url: handlerURL,//请求地址已在另一处设置
            data: { ope: 'getModel', id: id, t_: (new Date()).valueOf() },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data_) {
                if (data_.count > 0) {
                    var model = data_.dt[0];
                    $('#hid').val(model.id);
                    $('#txtUSname').val(model.USname);
                    $('#txtName').val(model.Name);
                    $('#ddlsex').val(model.sex);
                    $('#txtpassword').val(model.password);
                    $('#txttel').val(model.tel);
                    $('#txtemail').val(model.email);
                    $('#txtAddress').val(model.Address);
                
                    layui.form.render('select');

                    layer.closeAll('loading');
                    //打开
                    infoopen();
                } else {
                    if (data_.msg != null && data_.msg != "") {
                        layer.alert(data_.msg);
                    }
                    else {
                        layer.alert("未能获取详情!");
                    }
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.closeAll('loading');
            }
        });
    }
    //头工具栏事件
    table.on('toolbar(Result)', function (obj) {
        var checkStatus = table.checkStatus(obj.config.id);
        switch (obj.event) {

            case "Add":
                clearMain();
                infoopen();
                break;
            case 'Delete':
                var checkStatus = table.checkStatus(obj.config.id);
                if (checkStatus.data.length == 0) {
                    layer.alert('请先选择要离职的数据行！');
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
                layer.confirm("确定要离职吗？", function () {
                    $.ajax({
                        type: "POST",
                        url: handlerURL,
                        data: { ope: 'del', codeId: codeId, t_: (new Date()).valueOf() },
                        dataType: "json",
                        beforeSend: function () {
                            layer.load(2);
                        },
                        success: function (data) {
                            layer.closeAll('loading');
                            if (data.count > 0) {
                                layer.alert('已离职！');
                                location.reload(true);
                            }
                            else {
                                layer.alert('离职失败!');
                            }
                        }
                    })
                })
        };
    });



    //表格重载
    var Result_Active = {
        reload: function () {

            var Name = $('#secName').val();
            table.reload("Result", {
                page: {
                    pageIndex: 1, //第一页开始
                    curr: 1
                },
                where: {
                    ope: "getList", Name: Name,
                    t_: (new Date()).valueOf(),
                }
            })
            //监听上传
            //upload_active();
        }
    };

    //监听查询
    $("#btnSearch").on('click', function () {
       
        Result_Active.reload();
    });
 
    //监听保存
    $("#btnUpdate").on('click', function () {

        var id = $('#hid').val();
        var USname = $('#txtUSname').val();
        var Name = $('#txtName').val();
        var sex = $('#ddlsex').val();
        var password = $('#txtpassword').val();
        var tel = $('#txttel').val();
        var email = $('#txtemail').val();
        var Address = $('#txtAddress').val();
        layui.jquery.ajax({
            type: "post",
            url: handlerURL,//请求地址已在另一处设置
            data: {
                ope: 'save', id: id, t_: (new Date()).valueOf(),
                USname: USname, Name: Name,
                sex: sex, password: password,
                tel: tel, email: email, Address: Address,
            },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');
                if (data.count > 0) {
                    //editMain(data.count);
                    Result_Active.reload();
                }
                else {
                    if (data.msg != null && data.msg != "") {
                        layer.alert(data.msg);
                    }
                    else {
                        layer.alert('保存失败!');
                    }
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.closeAll('loading');
            }
        });
    });
    //打开详情
    function infoopen() {
        var maininfo = layer.open({
            type: 1,
            title: '人员信息',
            content: $('#div_maininfo'),
            maxmin: false,
            area: ['1220px', '620px'], //宽高
        });
        //layer.full(maininfo);
        //element.tabChange('demo', 0);
        //ResultD_Active.reload();

    }
    function clearMain() {
        $('#hid').val('');
        $('#txtUSname').val('');
        $('#txtName').val('');
        $('#ddlsex').val('男');
        $('#txtpassword').val('');
        $('#txttel').val('');
        $('#txtemail').val('');
        $('#txtAddress').val('');

        layui.form.render('select');
    }



    $(function () {

        setTimeout(function () {

            if ($("#btnUpdate1").val() == "style=") {
                $("#btnUpdate").hide();
            }


        }, 1000);

    })













});
