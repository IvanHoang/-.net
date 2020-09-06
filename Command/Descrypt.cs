using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Descrypt 的摘要说明
/// </summary>  
public class Descrypt
{
    public Descrypt()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 对字符串进行加密
    /// </summary>
    /// <param name="sourceString"></param>
    /// <returns></returns>
    public static string Encrypt(string sourceString)
    {
        //key和iv必须为8位
        string key = "20150313";
        string iv = "12345678";
        try
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);

            byte[] btIV = Encoding.UTF8.GetBytes(iv);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (
                        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV),
                            CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);

                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return sourceString;
                }
            }
        }
        catch
        {
        }

        return "DES加密出错";
    }

    /// <summary>
    /// 对字符串进行解密
    /// </summary>
    /// <param name="encryptedString"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string Decrypt(string encryptedString)
    {
        //key和iv必须为8位
        string key = "20150313";
        string iv = "12345678";

        byte[] btKey = Encoding.UTF8.GetBytes(key);

        byte[] btIV = Encoding.UTF8.GetBytes(iv);

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        using (MemoryStream ms = new MemoryStream())
        {
            byte[] inData = Convert.FromBase64String(encryptedString);
            try
            {
                using (
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write)
                    )
                {
                    cs.Write(inData, 0, inData.Length);

                    cs.FlushFinalBlock();
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return encryptedString;
            }
        }
    }
}