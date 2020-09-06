using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

    public static class Download
    {

        /// <summary>
        /// 弹出下载文件
        /// </summary>
        /// <param name="s_path"></param>
        public static void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        /// <summary>
        /// 弹出下载文件
        /// </summary>
        /// <param name="s_path"></param>
        public static void downloadfilezip(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            string filename = System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
            //HttpContext.Current.Response.ContentType = "application/ms-download";
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            //HttpContext.Current.Response.AddHeader("Content-Type", "application/x-zip-compressed");
            //HttpContext.Current.Response.AddHeader("Content-Type", "application/zip");
            //HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            //HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //HttpContext.Current.Response.WriteFile(file.FullName);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.End();


            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            //HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            //HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //HttpContext.Current.Response.WriteFile(file.FullName);
            //HttpContext.Current.Response.Flush();
            ////HttpContext.Current.Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();



            HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
            HttpContext.Current.Response.TransmitFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
