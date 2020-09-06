
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

    //表单初始赋值
    form.val(
      layer.closeAll('loading'));



   





    //监听保存
    $("#btnChange").on('click', function () {


        var OldPWD = $('#txtOldPWD').val();
        var NewPWQ1 = $('#txtNewPWQ1').val();
        var NewPWQ2 = $('#txtNewPWQ2').val();
        if (OldPWD == "") {
            layer.alert("请输入原密码!");
            return false;
        }
        if (NewPWQ1 == "" || NewPWQ2=="") {
            layer.alert("新密码需要输入2次!");
            return false;
        }
        if (NewPWQ1 != NewPWQ2) {
            layer.alert("2次新密码输入不一致，请重新输入!");
            return false;
        }
       
        layui.jquery.ajax({
            type: "post",
            url: handlerURL,//请求地址已在另一处设置
            data: {
                ope: 'change',  t_: (new Date()).valueOf(),
                OldPWD: OldPWD, NewPWQ1: NewPWQ1,
                NewPWQ2: NewPWQ2,
            },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data) {
                layer.closeAll('loading');

                if (data.count > 0) {
                    layer.alert('修改成功!');
                }
                else {
                    if (data.msg != null && data.msg != "") {
                        layer.alert(data.msg);
                    }
                    else {
                        layer.alert('修改失败!');
                    }
                }
            }
        });
    });



});
