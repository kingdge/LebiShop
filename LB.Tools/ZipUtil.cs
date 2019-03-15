using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;

namespace LB.Tools
{
    public class ZipUtil
    {
        #region winarar方式压缩/解压缩
        /// <summary>
        /// 是否安装了Winrar
        /// </summary>
        /// <returns></returns>
        static public bool Exists()
        {
            RegistryKey the_Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            return !string.IsNullOrEmpty(the_Reg.GetValue("").ToString());
        }

        /// <summary>
        /// 利用 WinRAR 进行压缩
        /// 使用示例 QsqLib.Util.WinRar.RAR("c:/aaa/ReortUDP.txt", "c:/", "fff.rar");
        /// </summary>
        /// <param name="path">将要被压缩的文件夹（绝对路径）</param>
        /// <param name="rarPath">压缩后的 .rar 的存放目录（绝对路径）</param>
        /// <param name="rarName">压缩文件的名称（包括后缀）</param>
        /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>
        public static bool RAR(string path, string rarPath, string rarName)
        {
            if (!Exists())
            {
                throw new Exception("主机没有安装Winrar");
            }
            path = path.Replace("/", @"\").Replace(@"\\", @"\") + @"\";
            rarPath = rarPath.Replace("/", @"\").Replace(@"\\", @"\") + @"\";
            bool flag = false;
            string rarexe;       //WinRAR.exe 的完整路径
            RegistryKey regkey;  //注册表键
            Object regvalue;     //键值
            string cmd;          //WinRAR 命令参数
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //update by 冯岩 2010-08-02 修复Windows Server 2008服务器无法运行的BUG
                if (regkey == null)
                    regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\WinRAR\shell\open\command");

                regvalue = regkey.GetValue("");  // 键值为 "d:\Program Files\WinRAR\WinRAR.exe" "%1"
                rarexe = regvalue.ToString();
                regkey.Close();
                rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe

                // Directory.CreateDirectory(path);
                //压缩命令，相当于在要压缩的文件夹(path)上点右键->WinRAR->添加到压缩文件->输入压缩文件名(rarName)
                cmd = string.Format("a  -ep1 {0} {1} -r",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;                          //设置命令参数
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit(); //无限期等待进程 winrar.exe 退出
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        /// <summary>
        /// 利用 WinRAR 进行解压缩
        /// 调用示例：QsqLib.Util.WinRar.UnRAR("c:/", "c:/", "ex090620.rar");
        /// </summary>
        /// <param name="path">文件解压路径（绝对）</param>
        /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>
        /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>
        /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>
        public static bool UnRAR(string path, string rarPath, string rarName)
        {
            if (!Exists())
            {
                throw new Exception("主机没有安装Winrar");
            }

            bool flag = false;
            string rarexe;
            RegistryKey regkey;
            Object regvalue;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                //update by 冯岩 2010-08-02 修复Windows Server 2008服务器无法运行的BUG
                if (regkey == null)
                    regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\WinRAR\shell\open\command");

                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();
                rarexe = rarexe.Substring(1, rarexe.Length - 7);

                Directory.CreateDirectory(path);
                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹
                cmd = string.Format("x {0} {1} -y",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }

        #endregion
        #region .net方式压缩/解压缩-用于升级程序，部署更新文件


        private static ArrayList GetFilesList(string ServerPath, string dirPath, ArrayList list)
        {
            string path = ServerPath + dirPath;
            DirectoryInfo mydir = new DirectoryInfo(path);
            FileInfo[] files = mydir.GetFiles();
            foreach (FileInfo f in files)
            {
                byte[] destBuffer = File.ReadAllBytes(f.FullName);
                SerializeFileInfo sfi = new SerializeFileInfo(dirPath, f.Name, destBuffer);
                list.Add(sfi);
            }

            DirectoryInfo[] dirs = mydir.GetDirectories();
            foreach (DirectoryInfo d in dirs)
            {
                list = GetFilesList(ServerPath, dirPath + "/" + d.Name + "/", list);
            }
            return list;
        }
        public static void Compress(string dirPath, string fileName)
        {
            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            fileName = ServerPath + fileName;
            ServerPath = ServerPath + dirPath;
            ArrayList list = new ArrayList();
            list = GetFilesList(ServerPath, "", list);
            IFormatter formatter = new BinaryFormatter();
            using (Stream s = new MemoryStream())
            {
                formatter.Serialize(s, list);
                s.Position = 0;
                CreateCompressFile(s, fileName);
            }
        }
        private static void CreateCompressFile(Stream source, string destinationName)
        {
            using (Stream destination = new FileStream(destinationName, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream output = new GZipStream(destination, CompressionMode.Compress))
                {
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = source.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        output.Write(bytes, 0, n);
                    }
                }
            }
        }


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
            //string adminpath = RequestTool.GetConfigKey("AdminPath").ToLower().TrimEnd('/');
            //string supplierpath = RequestTool.GetConfigKey("SupplierPath").ToLower().TrimEnd('/');
            string[] arr = new string[0];
            if (jump != "")
            {
                jump = jump.ToLower().Replace(@"\", "/").Replace("\r", "");
                arr = jump.Split('\n');
            }
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    if (arr[i].IndexOf("/admin/") == 0)
            //    {
            //        arr[i] = arr[i].Remove(0, 6);
            //        arr[i] = adminpath + "/" + arr[i];
            //    }
            //    if (arr[i].IndexOf("/supplier/") == 0)
            //    {
            //        arr[i] = arr[i].Remove(0, 9);
            //        arr[i] = supplierpath + "/" + arr[i];
            //    }
            //}
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(s);

            foreach (SerializeFileInfo f in list)
            {
                string fpath = f.FilePath;
                //if (fpath.IndexOf("/admin") == 0)
                //{
                //    fpath = fpath.Remove(0, 6);
                //    fpath = adminpath + "/" + fpath;
                //}
                //if (fpath.IndexOf("/supplier") == 0)
                //{
                //    fpath = fpath.Remove(0, 9);
                //    fpath = supplierpath + "/" + fpath;
                //}
                string vnewName = dirPath + "/" + fpath + Path.GetFileName(f.FileName);
                //vnewName = vnewName.ToLower().Replace("//", "/").Replace("//", "/");
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


        [Serializable]
       public class SerializeFileInfo
        {
            public SerializeFileInfo(string path, string name, byte[] buffer)
            {
                fileName = name;
                fileBuffer = buffer;
                filePath = path;
            }
            string filePath;
            public string FilePath
            {
                get
                {
                    return filePath;
                }
            }
            string fileName;
            public string FileName
            {
                get
                {
                    return fileName;
                }
            }

            byte[] fileBuffer;
            public byte[] FileBuffer
            {
                get
                {
                    return fileBuffer;
                }
            }
        }

        #endregion
        #region 加压解压方法
        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹夹路径</param>
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>
        /// <param name="err">出错信息</param>
        /// <returns>是否压缩成功</returns>
        public static bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "要压缩的文件夹不能为空！";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "要压缩的文件夹不存在！";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋.zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("\\"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 功能：解压zip格式的文件。
        /// </summary>
        /// <param name="zipFilePath">压缩文件路径</param>
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>
        /// <param name="err">出错信息</param>
        /// <returns>解压是否成功</returns>
        public static bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath == string.Empty)
            {
                err = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFilePath))
            {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("\\"))
                unZipDir += "\\";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("\\"))
                            directoryName += "\\";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }//while
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//解压结束
        #endregion
    }
}
