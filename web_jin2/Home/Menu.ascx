<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Home_Menu" %>

<div class="layui-header">
    <div class="layui-logo">欢迎您:<%=PubConstant.YongHu %></div>
    <!-- 头部区域（可配合layui已有的水平导航） -->
    <ul class="layui-nav layui-layout-left" lay-filter="menu">

       
        <li class="layui-nav-item" <%=Command.SetPermissions.ReturnPower("ryqx","","人员权限","人员权限","菜单显示") %>>
            <a href="javascript:;">人员权限</a>
            <dl class="layui-nav-child">
                <!-- 二级菜单 -->
             
                <dd <%=Command.SetPermissions.ReturnPower("ryqx-rysz","","人员权限","人员设置","菜单显示") %>><a href="javascript:void(0)">人员设置</a></dd>
             
                <dd <%=Command.SetPermissions.ReturnPower("ryqx-qxsz","","人员权限","权限设置","菜单显示") %>><a href="javascript:void(0)">权限设置</a></dd>

                <dd <%=Command.SetPermissions.ReturnPower("ryqx-cswh","","人员权限","参数维护","菜单显示") %>><a href="javascript:void(0)">参数维护</a></dd>

                <dd <%=Command.SetPermissions.ReturnPower("ryqx-spsz","","人员权限","商品设置","菜单显示") %>><a href="javascript:void(0)">商品设置</a></dd>

                <dd <%=Command.SetPermissions.ReturnPower("ryqx-khdj","","人员权限","客户登记","菜单显示") %>><a href="javascript:void(0)">客户登记</a></dd>

                <dd <%=Command.SetPermissions.ReturnPower("ryqx-sprk","","人员权限","商品入库","菜单显示") %>><a href="javascript:void(0)">商品入库</a></dd>

            </dl>
        </li>
        <li class="layui-nav-item"><a href="javascript:void(0)">修改密码</a></li>
        <li class="layui-nav-item"><a href="javascript:void(0)">退出系统</a></li>
    </ul>
    <ul class="layui-nav layui-layout-right">
        <li class="layui-nav-item" style="float: right;"><a href="javascript:void(0)"><%=PubConstant.companyName%></a>
           
        </li>


    </ul>
</div>

<script>
    //注意：导航 依赖 element 模块，否则无法进行功能性操作
    layui.use(['element', 'layer', 'jquery'], function () {
        var element = layui.element;
        var layer = layui.layer;
        var form = layui.form;
        var $ = layui.jquery;

        //监听导航点击
        element.on('nav(menu)', function (elem) {
            //console.log(elem)
            //layer.msg(elem.text() + '|' + elem.context.tagName);
            if (elem.context.innerText == elem.context.innerHTML) {

                switch (elem.text()) {
                    case "入库通知":
                        window.location.href = '../';
                        break;
                 
                    case "退出系统":
                            window.close();
                        break;
                    case "参数维护":
                        window.location.href = '../Declare/p_parameterInfo.aspx';
                        break;
                    case "商品设置":
                        window.location.href = '../Declare/goodsSet.aspx';
                        break;
                    case "客户登记":
                        window.location.href = '../Declare/nimp_agent.aspx';
                        break;
                    case "商品入库":
                        window.location.href = '../Declare/goodsinfo.aspx';
                }
            }
        });

    });
</script>
