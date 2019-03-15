using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
namespace Shop.Bussiness
{
    public class UpDate
    {
        /// <summary>
        /// 更新type表
        /// 
        /// </summary>
        public void UpdateType()
        {
            //============================================
            //获取服务端数据
            List<Lebi_Type> apis;
            try
            {
                string res = Shop.LebiAPI.Service.Instanse.API("CheckType", "");
                JavaScriptSerializer jss = new JavaScriptSerializer();
                apis = jss.Deserialize<List<Lebi_Type>>(res);

            }
            catch (Exception)
            {
                apis = new List<Lebi_Type>();
            }
            Lebi_Type model;
            foreach (Lebi_Type api in apis)
            {
                model = B_Lebi_Type.GetModel(api.id);
                if (model == null)
                {
                    B_Lebi_Type.Add(api);
                    continue;
                }
                if (model.Name == api.Name && model.Sort == api.Sort && model.Class == api.Class)
                {
                    continue;
                }
                B_Lebi_Type.Update(api);
            }
        }
        #region .net方式解压缩-用于升级程序，部署更新文件

        public static void DecompressFile(string fileName, string dirPath)
        {
            DecompressFile(fileName, dirPath, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dirPath"></param>
        /// <param name="jump">跳过的文件</param>
        public static void DecompressFile(string fileName, string dirPath, string jump)
        {

            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            fileName = ServerPath + fileName;
            using (Stream source = File.OpenRead(fileName))
            {
                using (Stream destination = new MemoryStream())
                {
                    using (GZipStream input = new GZipStream(source, CompressionMode.Decompress, true))
                    {
                        byte[] bytes = new byte[4096];
                        int n;
                        while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            destination.Write(bytes, 0, n);
                        }
                    }
                    destination.Flush();
                    destination.Position = 0;
                    DeSerializeFiles(destination, dirPath, ServerPath, jump);
                }
            }
        }
        private static void DeSerializeFiles(Stream s, string dirPath, string ServerPath, string jump)
        {
            string adminpath = RequestTool.GetConfigKey("AdminPath").ToLower().TrimEnd('/');
            string supplierpath = RequestTool.GetConfigKey("SupplierPath").ToLower().TrimEnd('/');
            bool plug_supplier = false;
            bool plug_agent = false;
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            {
                plug_supplier = true;
            }
            //if (Shop.LebiAPI.Service.Instanse.Check("plugin_agent"))
            //{
            //    plug_supplier = true;
            //}

            string[] arr = new string[0];
            if (jump != "")
            {
                jump = jump.ToLower().Replace(@"\", "/").Replace("\r", "");
                arr = jump.Split('\n');
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].IndexOf("/admin/") == 0)
                {
                    arr[i] = arr[i].Remove(0, 6);
                    arr[i] = adminpath + "/" + arr[i];
                }
                if (arr[i].IndexOf("/supplier/") == 0)
                {
                    arr[i] = arr[i].Remove(0, 9);
                    arr[i] = supplierpath + "/" + arr[i];
                }
            }
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(s);

            foreach (ZipUtil.SerializeFileInfo f in list)
            {
                string fpath = f.FilePath;
                if (!plug_supplier && fpath.Contains("/supplier"))
                {
                    continue;
                }
                //if (!plug_agent && fpath.Contains("admin/agent"))
                //{
                //    continue;
                //}
                if (fpath.IndexOf("/admin") == 0)
                {
                    fpath = fpath.Remove(0, 6);
                    fpath = adminpath + "/" + fpath;
                }
                if (fpath.IndexOf("/supplier") == 0)
                {
                    fpath = fpath.Remove(0, 9);
                    fpath = supplierpath + "/" + fpath;
                }

                string vnewName = dirPath + "/" + fpath + Path.GetFileName(f.FileName);
                vnewName = vnewName.ToLower().Replace("//", "/").Replace("//", "/");

                if (arr.Contains(vnewName))
                {
                    continue;
                }

                string newPath = ServerPath + dirPath + "/" + fpath;
                newPath = newPath.Replace("//", "/");
                string newName = newPath + Path.GetFileName(f.FileName);

                if (!Directory.Exists(newPath))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(newPath);
                }
                using (FileStream fs = new FileStream(newName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(f.FileBuffer, 0, f.FileBuffer.Length);
                    fs.Close();
                }
            }
        }
        #endregion
    }

}

