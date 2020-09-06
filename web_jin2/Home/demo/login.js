
var handlerURL = "../loginHandler.ashx";
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
    form.val(layer.closeAll('loading'));



    //监听登陆
    $("#btnCheckIn").on('click', function () {
        var UserName = $('#UserName').val();
        var Password = $('#pwd').val();


        if (UserName == "") {
            layer.alert("请输入用户名!");
            return false;
        }
        if (Password == "") {
            layer.alert("请输入密码!");
            return false;
        }
        layui.jquery.ajax({
            type: "post",
            url: handlerURL,//请求地址已在另一处设置
            data: { ope: 'CheckIn', UserName: UserName, Password: Password, t_: (new Date()).valueOf() },
            dataType: "json",
            beforeSend: function () {
                layer.load(2);
            },
            success: function (data_) {
                if (data_.count > 0) {
                    location.href = "../index.aspx";
                } else {
                    layer.alert("用户名和密码无效!");
                    layer.closeAll('loading');
                }
            }
        });
    });
    //监听回车 
    document.onkeydown = function (e) { // 回车提交表单
        var theEvent = window.event || e; 
        var code = theEvent.keyCode || theEvent.which; 
        if (code == 13) { 
            $("#btnCheckIn").click(); // #btnCheckIn 是你 提交按钮的ID
        } 
    }  
    
});
