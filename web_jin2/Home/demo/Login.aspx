<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="home_Login" %>
<%@ Import Namespace="Command" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../../layui/css/layui.css" rel="stylesheet" media="all" />
    <link href="../../layui/style.css" rel="stylesheet" />
    <script src="../../layui/layui.js"></script>
    <style type="text/css">
          .layui-form-item {
            margin: 0px 0px 3px 0px;
        }

        form .layui-form-label {
            width: 120px;
        }

        form .layui-input-block {
            margin-left: 100px;
            width: 300px;
        }
      
    </style>
    <script type="text/javascript" src="login.js"></script>
</head>
<body style="background-color: #95cdf2; overflow: hidden; background-size: cover;">
    <div class="page-header" style="width: 100%; border-bottom: 0px">
        <div class="row" style="height: 100px;">

            <div   class="layui-field-item">
                <h2 style="margin-left: 10px; margin-top:30px; font-size: 300%; float: left; color: white;">DEMO</h2>
                    </div>
             <div class="layui-form-item">
               
                   <h2 style="margin-right:15%; margin-top:100px; font-size: 200%; float: right; color: white;">  仓储系统</h2>
            </div>
        </div>

    </div>
 
    <div style="background-size: 100%; background: url(../../image/title2.jpg) no-repeat; height: 500px; width: 100%;">
       
        <div>
            <form id="form1" runat="server" style="margin-left: 60%; margin-top: 15%;">
                <div class="form-horizontal">
                    
                    <div class="form-group">
                      <div class="layui-field-box">
                            
                            <div class="layui-form-item">
                                <div class="layui-input-block">
                                    <input type="text"   lay-verify="title" id="UserName" name="UserName" autocomplete="off" placeholder="请输入用户名" class="layui-input" />
                                </div>
                            </div>
                        </div>
                         <div class="layui-field-box">
                            <div class="layui-form-item">
                                <div class="layui-input-block">
                                    <input   lay-verify="title" type="password" id="pwd" name="password" autocomplete="off" placeholder="请输入密码" class="layui-input"  />
                                </div>
                            </div>
                        </div>

                    <div class="layui-field-box">
                        <label class="layui-form-label"></label>
                          <div class="layui-input-block">
                            <button type="button" class="layui-btn layui-btn-mxj  layui-btn-fluid"  id="btnCheckIn"><i class="layui-icon">&#xe679;</i>登录</button>
                        </div>
                    </div>
   </div>
                </div>
            </form>
        </div>

    </div>
 

    <footer class="footer" style="position: fixed; bottom: 0;">
        <div class="container " style="height: 50px;margin-left: 330px; margin-top: -10px;color: white;">
            <p class="text-muted">
                <center> Copyright  2020 . All Rights Reserved.</center>
            </p>
        </div>
    </footer>
</body>
</html>
