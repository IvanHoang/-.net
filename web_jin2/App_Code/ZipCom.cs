using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

/// <summary>
///Zip 的摘要说明
/// </summary>
public class ZipCom
{
    public ZipCom()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>    
    /// 生成压缩文件    
    /// </summary>   
    /// 
    /// <param name="strZipPath">生成的zip文件的路径</param>    
    /// <param name="strZipTopDirectoryPath">源文件的上级目录</param>    
    /// <param name="intZipLevel">T压缩等级</param>    
    /// <param name="strPassword">压缩包解压密码</param>    
    /// <param name="filesOrDirectoriesPaths">源文件路径</param>    
    /// <returns></returns>    
    public bool Zip(string strZipPath, string strZipTopDirectoryPath, int intZipLevel, string strPassword, string[] filesOrDirectoriesPaths)    
    {        
        try        
        {            
            List<string> AllFilesPath = new List<string>();      
            if (filesOrDirectoriesPaths.Length > 0) 
                // get all files path          
            {              
                for (int i = 0; i < filesOrDirectoriesPaths.Length; i++)    
                {                 
                    if (File.Exists(filesOrDirectoriesPaths[i]))   
                    {                    
                        AllFilesPath.Add(filesOrDirectoriesPaths[i]);   
                    }                  
                    else if (Directory.Exists(filesOrDirectoriesPaths[i]))     
                    {                     
                        GetDirectoryFiles(filesOrDirectoriesPaths[i], AllFilesPath);     
                    }           
                }          
            }         
            if (AllFilesPath.Count > 0)       
            {              
                ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(strZipPath));   
                zipOutputStream.SetLevel(intZipLevel);       
                zipOutputStream.Password = strPassword;      
                for (int i = 0; i < AllFilesPath.Count; i++)      
                {                   
                    string strFile = AllFilesPath[i].ToString();      
                    try               
                    {               
                        if (strFile.Substring(strFile.Length - 1) == "") //folder       
                        {                          
                            string strFileName = strFile.Replace(strZipTopDirectoryPath, "");  
                            if (strFileName.StartsWith(""))         
                            {                            
                                strFileName = strFileName.Substring(1);        
                            }                   
                            ZipEntry entry = new ZipEntry(strFileName);     
                            entry.DateTime = DateTime.Now;     
                            zipOutputStream.PutNextEntry(entry);    
                        }               
                        else //file        
                        {             
                            FileStream fs = File.OpenRead(strFile);           
                            byte[] buffer = new byte[fs.Length];         
                            fs.Read(buffer, 0, buffer.Length);      
                            string strFileName = strFile.Replace(strZipTopDirectoryPath, "");  
                            if (strFileName.StartsWith(""))       
                            {                        
                                strFileName = strFileName.Substring(0);   
                            }                        
                            ZipEntry entry = new ZipEntry(strFileName);    
                            entry.DateTime = DateTime.Now;           
                            zipOutputStream.PutNextEntry(entry);    
                            zipOutputStream.Write(buffer, 0, buffer.Length);    
                            fs.Close();               
                            fs.Dispose();           
                        }                 
                    }               
                    catch         
                    {              
                        continue;        
                    }            
                }             
                zipOutputStream.Finish();      
                zipOutputStream.Close();     
                return true;        
            }          
            else        
            {             
                return false;     
            }     
        }    
        catch     
        {        
            return false;   
        }   
    }

    /// <summary>    
    /// Gets the directory files.    
    /// </summary>    
    /// <param name="strParentDirectoryPath">源文件路径</param>    
    /// <param name="AllFilesPath">所有文件路径</param>    
    private void GetDirectoryFiles(string strParentDirectoryPath, List<string> AllFilesPath)   
    {       
        string[] files = Directory.GetFiles(strParentDirectoryPath);     
        for (int i = 0; i < files.Length; i++)      
        {         
            AllFilesPath.Add(files[i]);  
        }       
        string[] directorys = Directory.GetDirectories(strParentDirectoryPath);     
        for (int i = 0; i < directorys.Length; i++)      
        {         
            GetDirectoryFiles(directorys[i], AllFilesPath);      
        }      
        if (files.Length == 0 && directorys.Length == 0) //empty folder   
        {          
            AllFilesPath.Add(strParentDirectoryPath);   
        }   
    }

    public bool FileCopy(string source, string destination)
    {
        //复制文件
        bool ret = false;
        System.IO.FileInfo file_s = new System.IO.FileInfo(source);
        System.IO.FileInfo file_d = new System.IO.FileInfo(destination);
        string Direct = destination.Substring(0, destination.LastIndexOf("\\"));
        if (file_s.Exists)
        {
            if (!file_d.Exists)
            {
                //判断文件夹是否存在,若不存在则创建
                if (Directory.Exists(Direct) == false)
                {
                    Directory.CreateDirectory(Direct);
                }
                file_s.CopyTo(destination);
                ret = true;
            }
        }
        return ret;
    }

}