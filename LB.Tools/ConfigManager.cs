using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows;
namespace LB.Tools
{
    public enum ConfigurationFile
    {
        AppConfig = 1,
        WebConfig = 2
    }
    public class ConfigManager
    {
        public ConfigManager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 对[appSettings]节点依据Key值读取到Value值，返回字符串
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="key">要读取的Key值</param>
        /// <returns>返回Value值的字符串</returns>
        public static string ReadValueByKey(ConfigurationFile configurationFile, string key)
        {
            string value = string.Empty;
            string filename = string.Empty;
            if (configurationFile.ToString() == ConfigurationFile.AppConfig.ToString())
            {
                //filename = System.Windows.Forms.Application.ExecutablePath + ".config";
            }
            else
            {
                filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//appSettings"); //得到[appSettings]节点 
                                                                  ////得到[appSettings]节点中关于Key的子节点
            XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
            if (element != null)
            {
                value = element.GetAttribute("value");
            }
            return value;
        }
        /// <summary>
        /// 对[connectionStrings]节点依据name值读取到connectionString值，返回字符串
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="name">要读取的name值</param>
        /// <returns>返回connectionString值的字符串</returns>
        public static string ReadConnectionStringByName(string name)
        {
            string connectionString = string.Empty;
            string filename = string.Empty;

            filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";

            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//connectionStrings"); //得到[appSettings]节点 
                                                                        ////得到[connectionString]节点中关于name的子节点
            XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + name + "']");
            if (element != null)
            {
                connectionString = element.GetAttribute("connectionString");
            }
            return connectionString;
        }
        /// <summary>
        /// 更新或新增[appSettings]节点的子节点值，存在则更新子节点Value,不存在则新增子节点，返回成功与否布尔值
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="key">子节点Key值</param>
        /// <param name="value">子节点value值</param>
        /// <returns>返回成功与否布尔值</returns>
        public static bool UpdateOrCreateAppSetting(ConfigurationFile configurationFile, string key, string value)
        {
            bool isSuccess = false;
            string filename = string.Empty;
            if (configurationFile.ToString() == ConfigurationFile.AppConfig.ToString())
            {
                //filename = System.Windows.Forms.Application.ExecutablePath + ".config";
            }
            else
            {
                filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//appSettings"); //得到[appSettings]节点 
            try
            {
                ////得到[appSettings]节点中关于Key的子节点
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (element != null)
                {
                    //存在则更新子节点Value
                    element.SetAttribute("value", value);
                }
                else
                {
                    //不存在则新增子节点
                    XmlElement subElement = doc.CreateElement("add");
                    subElement.SetAttribute("key", key);
                    subElement.SetAttribute("value", value);
                    node.AppendChild(subElement);
                }
                //保存至配置文件(方式一)
                using (XmlTextWriter xmlwriter = new XmlTextWriter(filename, null))
                {
                    xmlwriter.Formatting = Formatting.Indented;
                    doc.WriteTo(xmlwriter);
                    xmlwriter.Flush();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;
        }
        /// <summary>
        /// 更新或新增[connectionStrings]节点的子节点值，存在则更新子节点,不存在则新增子节点，返回成功与否布尔值
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="name">子节点name值</param>
        /// <param name="connectionString">子节点connectionString值</param>
        /// <param name="providerName">子节点providerName值</param>
        /// <returns>返回成功与否布尔值</returns>
        public static bool UpdateOrCreateConnectionString(ConfigurationFile configurationFile, string name, string connectionString, string providerName)
        {
            bool isSuccess = false;
            string filename = string.Empty;
            if (configurationFile.ToString() == ConfigurationFile.AppConfig.ToString())
            {
                //filename = System.Windows.Forms.Application.ExecutablePath + ".config";
            }
            else
            {
                filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//connectionStrings"); //得到[connectionStrings]节点 
            try
            {
                ////得到[connectionStrings]节点中关于Name的子节点
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + name + "']");
                if (element != null)
                {
                    //存在则更新子节点
                    element.SetAttribute("connectionString", connectionString);
                    element.SetAttribute("providerName", providerName);
                }
                else
                {
                    //不存在则新增子节点
                    XmlElement subElement = doc.CreateElement("add");
                    subElement.SetAttribute("name", name);
                    subElement.SetAttribute("connectionString", connectionString);
                    subElement.SetAttribute("providerName", providerName);
                    node.AppendChild(subElement);
                }
                //保存至配置文件(方式二)
                doc.Save(filename);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }
            return isSuccess;
        }
        /// <summary>
        /// 删除[appSettings]节点中包含Key值的子节点，返回成功与否布尔值
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="key">要删除的子节点Key值</param>
        /// <returns>返回成功与否布尔值</returns>
        public static bool DeleteByKey(ConfigurationFile configurationFile, string key)
        {
            bool isSuccess = false;
            string filename = string.Empty;
            if (configurationFile.ToString() == ConfigurationFile.AppConfig.ToString())
            {
                //filename = System.Windows.Forms.Application.ExecutablePath + ".config";
            }
            else
            {
                filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//appSettings"); //得到[appSettings]节点 
                                                                  ////得到[appSettings]节点中关于Key的子节点
            XmlElement element = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
            if (element != null)
            {
                //存在则删除子节点
                element.ParentNode.RemoveChild(element);
            }
            else
            {
                //不存在
            }
            try
            {
                //保存至配置文件(方式一)
                using (XmlTextWriter xmlwriter = new XmlTextWriter(filename, null))
                {
                    xmlwriter.Formatting = Formatting.Indented;
                    doc.WriteTo(xmlwriter);
                    xmlwriter.Flush();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        /// <summary>
        /// 删除[connectionStrings]节点中包含name值的子节点，返回成功与否布尔值
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="name">要删除的子节点name值</param>
        /// <returns>返回成功与否布尔值</returns>
        public static bool DeleteByName(ConfigurationFile configurationFile, string name)
        {
            bool isSuccess = false;
            string filename = string.Empty;
            if (configurationFile.ToString() == ConfigurationFile.AppConfig.ToString())
            {
                //filename = System.Windows.Forms.Application.ExecutablePath + ".config";
            }
            else
            {
                filename = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件 
            XmlNode node = doc.SelectSingleNode("//connectionStrings"); //得到[connectionStrings]节点 
                                                                        ////得到[connectionStrings]节点中关于Name的子节点
            XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + name + "']");
            if (element != null)
            {
                //存在则删除子节点
                node.RemoveChild(element);
            }
            else
            {
                //不存在
            }
            try
            {
                //保存至配置文件(方式二)
                doc.Save(filename);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
