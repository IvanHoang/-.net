using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// XmlHelper 的摘要说明
/// </summary>
public static class XmlHelper
{
    public static string ConvertDataTableToXML(DataTable xmlDS)
    {
        MemoryStream stream = null;
        XmlTextWriter writer = null;
        try
        {
            stream = new MemoryStream();
            writer = new XmlTextWriter(stream, Encoding.UTF8);
            xmlDS.WriteXml(writer);
            int count = (int)stream.Length;
            byte[] arr = new byte[count];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(arr, 0, count);
            UTF8Encoding utf = new UTF8Encoding();
            return utf.GetString(arr).Trim();
        }
        catch
        {
            return String.Empty;
        }
        finally
        {
            if (writer != null) writer.Close();
        }
    }
    #region 序列化与反序列化
    /// <summary>
    /// 从XML文件中反序列化对象
    /// </summary>
    public static T Deserialize<T>(string path)
    {
        T result = default(T);
        //xml来源可能是外部文件，也可能是从其他系统获得
        using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            XmlSerializer xmlSearializer = new XmlSerializer(typeof(T));
            result = (T)xmlSearializer.Deserialize(file);
            file.Close();
        }
        return result;
    }
    public static T Deserialize<T>(string xml, string xmlRootName = "Root")
    {

        T result = default(T);

        using (StringReader sr = new StringReader(xml))
        {

            XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?

                new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));

            result = (T)xmlSerializer.Deserialize(sr);

        }

        return result;

    }
    #endregion
}