using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

using System.IO;

public class PDFHelper
{
    public bool CreatePDF(string path, string filename, DataSet ds)
    {
        bool flag = false;

        //利用 iText 五步创建一个 PDF 文件：helloword。 
        //第一步，创建一个 iTextSharp.text.Document 对象的实例： 
        Document document = new Document();
        //第二步，为该 Document 创建一个 Writer 实例： 
        PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
        //第三步，打开当前 Document 
        document.Open();
        //第四步，为当前 Document 添加内容： 
        document.Add(new Paragraph("Hello World"));

        //第五步，关闭 Document 
        document.Close();
        //完
        return flag;
    }
    public static void Html2Pdf(string html, string filename)
    {
        using (Stream fs = new FileStream(filename, FileMode.Create))
        {
            using (Document doc = new Document(PageSize.A4))
            {

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                using (StringReader sr = new StringReader(html))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                }

                doc.Close();

            }
        }
    }
    public static void Html2Pdf(List<string> htmls, string filename, out string msg)
    {
        msg = "";
        using (Stream fs = new FileStream(filename, FileMode.Create))
        {
            using (Document doc = new Document(PageSize.A4))
            {

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                int x = 1;
                foreach (string html in htmls)
                {
                    if (x > 1)
                    {
                        doc.NewPage();
                    }
                    //using (StringReader sr = new StringReader(html))
                    //{
                    //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
                    //}
                    try
                    {
                        byte[] data = Encoding.UTF8.GetBytes(html);
                        using (MemoryStream msInput = new MemoryStream(data))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message.ToString();
                    }
                    x++;
                }
                doc.Close();

            }
        }
    }

}

