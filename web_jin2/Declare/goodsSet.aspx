<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="goodsSet.aspx.cs" Inherits="Declare_goodsSet" %>

<%@ Register Src="../Home/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterPage_menu" runat="Server">
    <uc1:Menu runat="server" ID="Menu" />

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterPage_head" runat="Server">
    <title>商品信息</title>
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
    <script type="text/javascript" src="js/goodsSet.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>
    <style type="text/css"></style>
    <script type="text/javascript"></script>

    <script type="text/html" id="rowTool">
        <a class="layui-btn layui-btn-mxj layui-btn-xs layui-icon" lay-event="xiangqing"><i class="layui-icon">&#xe642;</i>编辑</a>
    </script>
    <script type="text/html" id="tableTool">
        <div class="layui-btn-container">
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add" type="button" id="btnAdd"><i class="layui-icon">&#xe654;</i>新增</button>
            <button class="layui-btn layui-btn-sm layui-btn-danger" type="button" id="btnDelete" lay-event="Delete"><i class="layui-icon">&#xe640;</i>删除</button>
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" id="btndown" type="button" onclick="location.href='../Files/Template/应收报价模板.xls?t_=<%=DateTime.Now.ToString("HHmmssfff") %>'"><i class="layui-icon">&#xe601;</i>模板下载</button>
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button" lay-event="upGoods" id="upGoods"><i class="layui-icon">&#xe67c;</i>上传商品信息</button>
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
                                <label class="layui-form-label">货物型号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="SKUs" id="SKUs" autocomplete="off" placeholder="请输入货物型号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">品名</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="GoodsNames" id="GoodsNames" autocomplete="off" placeholder="请输入品名" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">客户简称</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="AgentNames" id="AgentNames" autocomplete="off" placeholder="请输入客户简称" class="layui-input" />
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
                    <legend>商品信息</legend>
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
                <li lay-id="0" class="layui-this">表头信息</li>
            </ul>
            <div class="layui-tab-content">
                <div class="layui-tab-item layui-show" id="div_Main">
                    <fieldset class="layui-elem-field" id="MainInfo">
                        <legend style="font-size: 16px;">详情</legend>
                        <form class="layui-form" lay-filter="goodsSet" id="goods" name="goods">
                            <div class="layui-field-box">
                                <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add2" type="button" id="btnAdd2"><i class="layui-icon">&#xe654;</i>新建</button>
                                <button lay-submit lay-filter="save" class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button" id="btnUpdate"><i class="layui-icon">&#xe605;</i>保存</button>
                                <input type="hidden" name="title" id="" value="" />
                            </div>
                            <div class="layui-form-item" pane>

                                <label class="layui-form-label">客户简称</label>
                                <div class="layui-input-inline">
                                    <select name="AgentName" id="AgentName" lay-verify="selected" lay-search="" lay-filter="AgentName"></select>
                                </div>
                                <label class="layui-form-label">货物型号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="SKU" id="SKU" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                <label class="layui-form-label">品名</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="GoodsName" id="GoodsName" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item" pane>

                                <label class="layui-form-label">HSCode</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="HSCode" id="HSCode" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                <label class="layui-form-label">规格型号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="gdsSpcfModelDesc" id="gdsSpcfModelDesc" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                <label class="layui-form-label">申报单位</label>
                                <div class="layui-input-inline">
                                    <select name="dclUnitcd" id="dclUnitcd" lay-verify="selected" lay-search=""></select>
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">法定单位</label>
                                <div class="layui-input-inline" pane>
                                    <select name="lawfUnitcd" id="lawfUnitcd" lay-verify="selected" lay-search=""></select>
                                </div>
                                <label class="layui-form-label">立方</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="Volume" id="Volume" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                                <label class="layui-form-label">净重</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="netWt" id="netWt" placeholder="" autocomplete="off" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item" pane>
                                <label class="layui-form-label">原产国</label>
                                <div class="layui-input-inline">
                                    <select name="Origin" id="Origin" lay-verify="selected" lay-search=""></select>
                                </div>

                                <%--                                <label class="layui-form-label">部门</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="department" id="department" lay-verify="required" placeholder="" autocomplete="off" class="layui-input" />
                                </div>--%>
                            </div>
                </div>
                </form>
                    </fieldset>
            </div>
        </div>
    </div>
    <script>  
</script>



</asp:Content>

