<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="p_parameterInfo.aspx.cs" Inherits="Declare_p_parameterInfo" %>

<%@ Register Src="../Home/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_menu" runat="Server">
    <uc1:Menu runat="server" ID="Menu" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_head" runat="Server">
    <title>参数信息</title>
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
    <script type="text/javascript" src="js/p_parameterInfo.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>
    <style type="text/css"></style>
    <script type="text/javascript"></script>

    <script type="text/html" id="rowTool">
        <a class="layui-btn layui-btn-mxj layui-btn-xs layui-icon" lay-event="xiangqing"><i class="layui-icon">&#xe642;</i>编辑</a>

    </script>
    <script type="text/html" id="tableTool">
        <div class="layui-btn-container">
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add" type="button" id="btnAdd" ><i class="layui-icon">&#xe654;</i>新增</button>
            <button class="layui-btn layui-btn-sm layui-btn-danger" type="button" id="btnDelete" lay-event="Delete" ><i class="layui-icon">&#xe640;</i>删除</button>
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
                        <table id="Main" lay-filter="Main"></table>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="layui-col-md9">
            <div class="layui-row grid-demo">
                <fieldset class="layui-elem-field layui-field-title" style="margin-top: 30px;">
                    <legend>参数信息</legend>
                </fieldset>
                <table id="Result" lay-filter="Result"></table>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="MasterPage_outbody" runat="Server">
    <div id="div_maininfo" style="display: none">
        <div class="layui-tab" lay-filter="demo">
            <ul class="layui-tab-title">
                <li lay-id="0" class="layui-this">参数信息</li>
            </ul>
            <div class="layui-tab-content">
                <div class="layui-tab-item layui-show" id="div_Main">
                    <fieldset class="layui-elem-field" id="MainInfo">
                        <legend style="font-size: 16px;">详情</legend>
                        <form class="layui-form " lay-filter="Main" id="info" name="info">
                            <div class="layui-field-box">
                                <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add2" type="button" id="btnAdd2"> <i class="layui-icon">&#xe654;</i>新建</button>
                                <button lay-submit lay-filter="save" class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button" id="btnUpdate" ><i class="layui-icon">&#xe605;</i>保存</button>

                                <input type="hidden" name="id" id="" value="" />
                            </div>

                            <div class="layui-form-item" pane>
                                <label class="layui-form-label">参数名称</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="Details"  id="Details" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                <label class="layui-form-label">备注</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="Remark" id="Remark" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                </div>
                            </div>
                        </form>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>