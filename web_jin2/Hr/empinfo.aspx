<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="empinfo.aspx.cs" Inherits="Hr_empinfo" %>

<%@ Register Src="../Home/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_menu" runat="Server">
    <uc1:Menu runat="server" ID="Menu" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_head" runat="Server">
    <title>人员设置</title>
    <style>
        .layui-form-item {
            margin: 0px 0px 5px 0px;
        }

        form .layui-form-label {
            width: 120px;
        }

        form .layui-input-block {
            margin-left: 150px;
            width: 890px;
        }
    </style>
    <script type="text/javascript" src="js/empinfo.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>
    <style type="text/css"></style>
    <script type="text/javascript"></script>
    <script type="text/html" id="rowTool">
        <a class="layui-btn layui-btn-mxj layui-btn-xs layui-icon" lay-event="xiangqing" <%=Command.SetPermissions.ReturnPower("ryqx-rysz-bj","","人员权限","人员设置","编辑") %>><i class="layui-icon">&#xe642;</i>编辑</a>
      
    </script>
    <script type="text/html" id="tableTool">
        <div class="layui-btn-container">
              <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add" type="button" id="btnAdd" <%=Command.SetPermissions.ReturnPower("ryqx-rysz-xz","","人员权限","人员设置","新增") %>><i class="layui-icon">&#xe654;</i>新增</button>
              <button class="layui-btn layui-btn-sm layui-btn-danger"  id="btnDelete" type="button" lay-event="Delete" <%=Command.SetPermissions.ReturnPower("ryqx-rysz-lz","","人员权限","人员设置","离职") %>><i class="layui-icon">&#xe640;</i>离职</button>
        </div>
    </script>
  
   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_body" runat="Server">

    <div class="layui-row layui-col-space5">
        <div class="layui-col-md3">
            <div class="layui-row grid-demo">

                <div class="layui-form layui-form-pane" style="padding: 5px;">

                    <fieldset class="layui-elem-field">
                        <legend style="font-size: 16px;">查询条件</legend>
                        <div class="layui-field-box">

                            <div class="layui-form-item">
                                <label class="layui-form-label">员工姓名</label>
                                <div class="layui-input-block">
                                    <input type="text" name="title" lay-verify="title" id="secName" autocomplete="off" placeholder="" class="layui-input" />
                                </div>
                            </div>
                          
                            <div class="layui-form-item">
                                <button class="layui-btn layui-btn-mxj layui-btn-fluid layui-icon" id="btnSearch"><i class="layui-icon">&#xe615;</i>搜索</button>
                                <input type="hidden" id="hid" value="" />
                               
                               
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="layui-col-md9">
            <div class="layui-row grid-demo">
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                    <legend>人员设置</legend>
                </fieldset>
                <table id="Result" lay-filter="Result"></table>
            </div>
        </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MasterPage_outbody" runat="Server">
    <div id="div_maininfo">
        <div class="layui-tab" lay-filter="demo">
            <ul class="layui-tab-title">
                <li lay-id="0" class="layui-this">人员设置</li>
               
            </ul>
            <div class="layui-tab-content">
                <div class="layui-tab-item layui-show" id="div_Main">
                    <fieldset class="layui-elem-field" id="MainInfo">
                        <legend style="font-size: 16px;">人员设置</legend>
                        <form class="layui-form" action="">
                            <div class="layui-field-box">
                                  <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon"   type="button"   id="btnUpdate" <%=Command.SetPermissions.ReturnPower("ryqx-rysz-bc","","人员权限","人员设置","保存") %>><i class="layui-icon">&#xe605;</i>保存</button>
                                <input type="hidden" id="btnUpdate1" value="<%=btnUpdate %>" />
                                  </div>
                                <div class="layui-form-item" pane>
                                    <label class="layui-form-label"  >系统用户名</label>
                                    <div class="layui-input-inline">
                                        <input type="text" name="title" lay-verify="required" id="txtUSname" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                    <label class="layui-form-label"  >员工姓名</label>
                                    <div class="layui-input-inline">
                                        <input type="text" name="title" lay-verify="required" id="txtName" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                     <label class="layui-form-label"  >性别</label>
                                     <div class="layui-input-inline">
                                 
                                         <select name="title" lay-verify="required" lay-search="" id="ddlsex">
                                            <option value="男" selected="selected">男</option>
                                            <option value="女">女</option>
                                      
                                        </select>
                                              
                                    </div>
                                   
                                </div>
                                <div class="layui-form-item">
                                      <label class="layui-form-label"  >初始密码</label>
                                    <div class="layui-input-inline">
                                        <input   name="title" lay-verify="required" id="txtpassword" type="password"  placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                     <label class="layui-form-label"  >电话</label>
                                    <div class="layui-input-inline">
                                        <input type="text" name="title" lay-verify="required" id="txttel" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                     <label class="layui-form-label"  >邮箱</label>
                                    <div class="layui-input-inline">
                                        <input type="text" name="title" lay-verify="required" id="txtemail" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                 
                                 

                                </div>
                                <div class="layui-form-item">
                                    <label class="layui-form-label"  >地址</label>
                                    <div class="layui-input-block">
                                        <input type="text" name="title"  id="txtAddress" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                 
                              
                                     </div>
                                
                        </form>
                    </fieldset>
                </div>
          
   
            </div>
        </div>
  
</asp:Content>


