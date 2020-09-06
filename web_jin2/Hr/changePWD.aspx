<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="changePWD.aspx.cs" Inherits="Hr_changePWD" %>


<%@ Register Src="../Home/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_menu" runat="Server">
    <uc1:Menu runat="server" ID="Menu" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_head" runat="Server">
   
  
   
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MasterPage_body" runat="Server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MasterPage_outbody" runat="Server">
      <div id="div_maininfo">
     <title>修改密码</title>
   <style type="text/css">
          .layui-form-item {
            margin: 0px 0px 10px 0px;
        }

        form .layui-form-label {
            width: 120px;
        }

        form .layui-input-block {
            margin-left: 150px;
            width: 890px;
        }
       form .layui-form-item {
            margin-left: 100px;
             }
       </style>
    <script type="text/javascript" src="js/changePWD.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>

    <script type="text/javascript"></script>
      <fieldset class="layui-elem-field" id="MainInfo">
                        <legend style="font-size: 16px;">修改密码</legend>
                        <form class="layui-form" action="">
                            <div class="layui-field-box">
                                
                                <div class="layui-form-item">
                                    <label class="layui-form-label"  >原密码</label>
                                    <div class="layui-input-inline">
                                        <input type="password" name="title" lay-verify="required" id="txtOldPWD" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                 
                                </div>
                                 
                                <div class="layui-form-item">
                                   
                                  

                                     <label class="layui-form-label"  >新密码</label>
                                    <div class="layui-input-inline">
                                        <input type="password" name="title" lay-verify="required" id="txtNewPWQ1" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                  </div>
                                  <div class="layui-form-item">
                                   
                                  

                                     <label class="layui-form-label"  >确认新密码</label>
                                    <div class="layui-input-inline">
                                        <input type="password" name="title" lay-verify="required" id="txtNewPWQ2" placeholder="" autocomplete="off" class="layui-input" />
                                    </div>
                                  </div>
                                     
                               
                                 <div class="layui-form-item">
                                       <label class="layui-form-label"  ></label>
                              <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon"  type="button"    id="btnChange"><i class="layui-icon">&#xe605;</i>确定修改</button>
                                       </div>
                                  </div>
                        </form>
                    </fieldset>
            </div>
</asp:Content>


