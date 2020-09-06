<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="goodsinfo.aspx.cs" Inherits="Declare_goodsinfo" %>

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
    <script type="text/javascript" src="js/goodsinfo.js?t_=<%=DateTime.Now.ToString("HHmmssfff") %>"></script>
    <style type="text/css"></style>
    <script type="text/javascript"></script>

    <script type="text/html" id="rowTool">
        <a class="layui-btn layui-btn-mxj layui-btn-xs layui-icon" lay-event="xiangqing"><i class="layui-icon">&#xe642;</i>编辑</a>

    </script>
    <script type="text/html" id="tableTool">
        <div class="layui-btn-container">
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add" type="button"><i class="layui-icon">&#xe654;</i>新增</button>
            <button class="layui-btn layui-btn-sm layui-btn-danger" type="button" lay-event="Delete"><i class="layui-icon">&#xe640;</i>删除</button>
        </div>
    </script>
    <script type="text/html" id="rowTool_detail">
        <a class="layui-btn layui-btn-mxj layui-btn-xs layui-icon" lay-event="resultxiangqing"><i class="layui-icon">&#xe642;</i>编辑</a>

    </script>

    <script type="text/html" id="tableTool_detail">
        <div class="layui-btn-container">
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="detail_add" type="button"><i class="layui-icon">&#xe654;</i>新增</button>
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" id="btndown" type="button" onclick="location.href='../Files/Template/入库模版.xlsx?t_=<%=DateTime.Now.ToString("HHmmssfff") %>'"><i class="layui-icon">&#xe601;</i>模板下载</button>
            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button" lay-event="upGoods" id="upGoods"><i class="layui-icon">&#xe67c;</i>上传商品信息</button>
            <button class="layui-btn layui-btn-sm layui-btn-danger" type="button" lay-event="detail_delete"><i class="layui-icon">&#xe640;</i>删除</button>
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
                                <label class="layui-form-label">业务流水号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="jobnumbers" id="jobnumbers" autocomplete="off" placeholder="请输入业务流水号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">QD号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="bondInvtNos" id="bondInvtNos" autocomplete="off" placeholder="请输入QD号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">客户简称</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="agentNames" id="agentNames" autocomplete="off" placeholder="请输入客户简称" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">报关单号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="entryNos" id="entryNos" autocomplete="off" placeholder="请输入报关单号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">提单号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="BL_Nos" id="BL_Nos" autocomplete="off" placeholder="请输入提单号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">客户自编号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="customNOs" id="customNOs" autocomplete="off" placeholder="请输入客户自编号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">发票号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="invoiceNos" id="invoiceNos" autocomplete="off" placeholder="请输入发票号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">订单号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="ordernos" id="ordernos" autocomplete="off" placeholder="请输入订单号" class="layui-input" />
                                </div>
                            </div>
                            <div class="layui-form-item">
                                <label class="layui-form-label">运单号</label>
                                <div class="layui-input-inline">
                                    <input type="text" name="transportNos" id="transportNos" autocomplete="off" placeholder="请输入运单号" class="layui-input" />
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
                    <legend>入库信息</legend>
                </fieldset>
                <table id="Result" lay-filter="Result"></table>
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="MasterPage_outbody" runat="Server">
    <div id="div_maininfo">

        <div class="layui-tab" lay-filter="tab1">
            <ul class="layui-tab-title" id="navList">
                <li id="main" lay-id="0" class="layui-this">表头</li>
                <li id="detail" lay-id="1">表体</li>
            </ul>



            <div class="layui-tab-content">
                <div class="layui-tab-item layui-show" id="div_Main">

                    <form class="layui-form" lay-filter="main">

                        <div class="layui-field-box">
                            <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="Add2" type="button" id="btnAdd2"><i class="layui-icon">&#xe654;</i>新建</button>
                            <button lay-submit lay-filter="save" class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button" id="btnUpdate"><i class="layui-icon">&#xe605;</i>保存</button>
                            <input type="hidden" name="id" />

                        </div>


                        <div class="layui-form-item">
                            <label class="layui-form-label required">业务流水号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="jobnumber" id="jobnumber" placeholder="系统自动生成" autocomplete="off" class="layui-input" disabled />
                            </div>
                            <label class="layui-form-label">客户简称</label>
                            <div class="layui-input-inline">
                                <select name="agentName" id="agentName" lay-verify="selected" lay-search=""></select>
                            </div>
                            <label class="layui-form-label">QD号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="bondInvtNo" id="bondInvtNo" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                        </div>

                        <div class="layui-form-item">
                            <label class="layui-form-label">报关单号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="entryNo" id="entryNo" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                            <label class="layui-form-label">运输工具</label>
                            <div class="layui-input-inline">
                                <select name="transport" id="transport" lay-verify="selected" lay-search=""></select>
                            </div>
                            <label class="layui-form-label">提单号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="BL_No" id="BL_No" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                        </div>
                        <div class="layui-form-item">
                            <label class="layui-form-label">客户自编号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="customNO" id="customNO" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                            <label class="layui-form-label">预计入库日期</label>
                            <div class="layui-input-inline">
                                <input type="text" name="planDate" id="planDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input date" />
                            </div>

                            <label class="layui-form-label">货物状态</label>
                            <div class="layui-input-inline">
                                <select name="status" id="status" lay-filter="status">
                                    <option value="10">入库通知</option>
                                    <%--                                    <option value="11">上架确认</option>
                                    <option value="12">已入库</option>
                                    <option value="20">出库通知</option>
                                    <option value="21">提货单</option>
                                    <option value="22">出库确认</option>
                                    <option value="23">已出库</option>--%>
                                </select>

                            </div>
                        </div>
                        <div class="layui-form-item" pane>
                            <label class="layui-form-label">出入库日期</label>
                            <div class="layui-input-inline">
                                <input type="text" name="inoutdate" id="inoutdate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input date" />
                            </div>
                            <label class="layui-form-label">客服中心流水号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="c_jobumber" id="c_jobumber" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                            <label class="layui-form-label">发票号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="invoiceNo" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                        </div>
                        <div class="layui-form-item" pane>
                            <label class="layui-form-label">运单号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="transportNo" id="transportNo" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                            <label class="layui-form-label">订单号</label>
                            <div class="layui-input-inline">
                                <input type="text" name="orderno" id="orderno" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                            <label class="layui-form-label">备注</label>
                            <div class="layui-input-inline">
                                <input type="text" name="remark" id="remark" placeholder="" autocomplete="off" class="layui-input" />
                            </div>
                        </div>

                    </form>
                </div>

                <div class="layui-tab-item">
                    <div class="layui-col">
                        <table id="detail_result" lay-filter="detail_result"></table>
                    </div>
                </div>

            </div>

        </div>


    </div>






    <div id="div_detailInfo">

        <form class="layui-form" lay-filter="detail">

            <div class="layui-field-box">
                <button class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" lay-event="detail_clear" type="button" id="detail_clear"><i class="layui-icon">&#xe654;</i>新建</button>
                <button lay-submit lay-filter="detail_save" class="layui-btn layui-btn-mxj layui-btn-sm layui-icon" type="button"><i class="layui-icon">&#xe605;</i>保存</button>
                <input type="hidden" name="id" />

            </div>

            <div class="layui-form-item">

                <label class="layui-form-label">备案序号</label>
                <div class="layui-input-inline">
                    <input type="text" name="putrecSeqno" id="putrecSeqno" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">货物型号</label>
                <div class="layui-input-inline">
                    <select name="SKU" id="SKU" lay-verify="selected" lay-filter="SKU"></select>
                </div>
                <label class="layui-form-label">币种</label>
                <div class="layui-input-inline">
                    <select name="curr" id="curr" lay-verify="selected" lay-search="" lay-filter="curr"></select>
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">品名</label>
                <div class="layui-input-inline">
                    <input type="text" name="GoodsName" id="GoodsName" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">HSCode</label>
                <div class="layui-input-inline">
                    <input type="text" name="HSCode" id="HSCode" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">规格型号</label>
                <div class="layui-input-inline">
                    <input type="text" name="gdsSpcfModelDesc" id="gdsSpcfModelDesc" placeholder="" autocomplete="off" class="layui-input" />
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">申报数量</label>
                <div class="layui-input-inline">
                    <input type="text" name="dcl_QTY" id="dcl_QTY" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">申报单位</label>
                <div class="layui-input-inline">
                    <input type="text" name="dclUnitcd" id="dclUnitcd" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">法定数量</label>
                <div class="layui-input-inline">
                    <input type="text" name="law_QTY" id="law_QTY" placeholder="" autocomplete="off" class="layui-input" />
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">法定单位</label>
                <div class="layui-input-inline">
                    <input type="text" name="lawfUnitcd" id="lawfUnitcd" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">立方</label>
                <div class="layui-input-inline">
                    <input type="text" name="Volume" id="Volume" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">毛重</label>
                <div class="layui-input-inline">
                    <input type="text" name="grossWt" id="grossWt" placeholder="" autocomplete="off" class="layui-input" />
                </div>
            </div>
            <div class="layui-form-item">
                <label class="layui-form-label">净重</label>
                <div class="layui-input-inline">
                    <input type="text" name="netWt" id="netWt" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">原产国</label>
                <div class="layui-input-inline">
                    <select name="Origin" id="Origin" lay-verify="selected" lay-filter="Origin"></select>
                </div>
                <label class="layui-form-label">批号</label>
                <div class="layui-input-inline">
                    <input type="text" name="batch" id="batch" placeholder="" autocomplete="off" class="layui-input" />
                </div>
            </div>
            <div class="layui-form-item" pane>
                <label class="layui-form-label">序列号</label>
                <div class="layui-input-inline">
                    <input type="text" name="LPN" id="LPN" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">生产日期</label>
                <div class="layui-input-inline">
                    <input type="text" name="productionDate" id="productionDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input date" />
                </div>
                <label class="layui-form-label">有效期</label>
                <div class="layui-input-inline">
                    <input type="text" name="validDate" id="validDate" placeholder="yyyy-MM-dd" autocomplete="off" class="layui-input date" />
                </div>
            </div>
            <div class="layui-form-item">
                <%--                <label class="layui-form-label">库位</label>
                <div class="layui-input-inline">
                    <input type="text" name="Location" id="Location" placeholder="" autocomplete="off" class="layui-input" />
                </div>--%>
                <label class="layui-form-label">单价</label>
                <div class="layui-input-inline">
                    <input type="text" name="unitprice" id="unitprice" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">总金额</label>
                <div class="layui-input-inline">
                    <input type="text" name="totalamount" id="totalamount" placeholder="" autocomplete="off" class="layui-input" />
                </div>
                <label class="layui-form-label">货物状态</label>
                <div class="layui-input-inline">
                    <select name="goodsStatus" id="goodsStatus" lay-verify="selected" lay-search="" lay-filter="goodsStatus">
                        <option value="1">正常</option>
                        <option value="0">结束</option>
                    </select>
                </div>
            </div>
            <%--            <div class="layui-form-item">

                <label class="layui-form-label">货物状态</label>
                <div class="layui-input-inline">
                    <select name="goodsStatus" id="goodsStatus" lay-verify="selected" lay-search="" lay-filter="goodsStatus">
                        <option value="1">正常</option>
                        <option value="0">结束</option>
                    </select>
                </div>
            </div>--%>
        </form>



    </div>



</asp:Content>



