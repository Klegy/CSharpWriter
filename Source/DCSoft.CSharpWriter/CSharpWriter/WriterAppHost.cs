/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Commands;
using DCSoft.CSharpWriter.Controls;
using System.ComponentModel;
using System.ComponentModel.Design;
 
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文本编辑器宿主对象
    /// </summary>
    [System.Runtime.InteropServices.ComVisible ( true )]
    public class WriterAppHost
    {
 
        static WriterAppHost()
        {
 
        }
          


        private static WriterAppHost _Default = null;
        /// <summary>
        /// 默认对象
        /// </summary>
        public static WriterAppHost Default
        {
            get
            {
                if (_Default == null)
                {
                    _Default = new WriterAppHost();
                }
                return _Default;
            }
            set
            {
                _Default = value;
            }
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterAppHost()
        {
            // 添加标准编辑器命令功能模块
            this.CommandContainer.Modules.Add(new WriterCommandModuleBrowse());
            this.CommandContainer.Modules.Add(new WriterCommandModuleEdit());
            this.CommandContainer.Modules.Add(new WriterCommandModuleFile());
            this.CommandContainer.Modules.Add(new WriterCommandModuleFormat());
            this.CommandContainer.Modules.Add(new WriterCommandModuleInsert());
            this.CommandContainer.Modules.Add(new WriterCommandModuleSecurity());
         
            this.CommandContainer.Modules.Add(new WriterCommandModuleTools());
         
            // 设置默认的调试输出对象
            this.Services.Debugger = new WriterDebugger();
            // 设置自己
            this.Services.AddService(typeof(WriterAppHost), this);
            // 设置默认的文件系统对象
            DefaultFileSystem fs = new DefaultFileSystem();
            fs.OpenFileFilter = WriterStrings.XMLFilter;
            fs.SaveFileFilter = WriterStrings.XMLFilter;
            this.FileSystems[WriterConst.FS_KBLibrary] = fs;
            this.FileSystems[WriterConst.FS_KBListItem] = fs;

            fs = new DefaultFileSystem();
            fs.OpenFileFilter = WriterStrings.StdOpenFileFilter;
            fs.SaveFileFilter = WriterStrings.StdSaveFileFilter;
            this.FileSystems[WriterConst.FS_Document] = fs;
        }

        private string _ApplicationDataBaseUrl = null;
        /// <summary>
        /// 应用程序数据基础路径
        /// </summary>
        public string ApplicationDataBaseUrl
        {
            get
            {
                return _ApplicationDataBaseUrl;
            }
            set
            {
                _ApplicationDataBaseUrl = value;
            }
        }
       
        private WriterCommandContainer _CommandContainer = new WriterCommandContainer();
        /// <summary>
        /// 命令容器对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WriterCommandContainer CommandContainer
        {
            get
            {
                if (_CommandContainer == null)
                {
                    _CommandContainer = new WriterCommandContainer();
                }
                return _CommandContainer;
            }
            set
            {
                _CommandContainer = value;
            }
        }
         
        /// <summary>
        /// 创建文档元素属性对象
        /// </summary>
        /// <param name="elementType">文档元素类型</param>
        /// <returns>创建的文档元素属性对象</returns>
        public virtual XTextElementProperties CreateProperties(Type elementType)
        {
            ElementDescriptorAttribute attr = ElementDescriptorAttribute.GetDescriptor(elementType);
            if (attr != null)
            {
                if (attr.PropertiesType != null)
                {
                    return (XTextElementProperties)System.Activator.CreateInstance(attr.PropertiesType);
                }
            }
            return null;


        }


        private WriterServiceContainer _Services = new WriterServiceContainer();
        /// <summary>
        /// 服务器对象容器
        /// </summary>
        public WriterServiceContainer Services
        {
            get
            {
                if (_Services == null)
                {
                    _Services = new WriterServiceContainer();
                }
                return _Services;
            }
            set
            {
                _Services = value;
            }
        }
         
        private FileSystemDictionary _FileSystems = new FileSystemDictionary();
        /// <summary>
        /// 系统使用的文件系统列表
        /// </summary>
        public FileSystemDictionary FileSystems
        {
            get
            {
                return _FileSystems;
            }
            set
            {
                _FileSystems = value;
            }
        }

        private bool _DebugMode = true;
        /// <summary>
        /// 处于调试模式
        /// </summary>
        public bool DebugMode
        {
            get { return _DebugMode; }
            set { _DebugMode = value; }
        }

        /// <summary>
        /// 调试输出对象
        /// </summary>
        internal WriterDebugger Debuger
        {
            get
            {
                if (this.DebugMode)
                {
                    return this.Services.Debugger;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 添加服务对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        [System.Obsolete("仅保留对旧代码的兼容性，不推荐使用，请使用 Services.AddService")]
        public void AddService(Type type, object instance)
        {
            if (instance == null)
            {
                if (this.Services.GetService(type) != null)
                {
                    this.Services.RemoveService(type);
                }
            }
            else
            {
                if (this.Services.GetService(type) == null)
                {
                    this.Services.AddService(type, instance);
                }
                else
                {
                    this.Services.RemoveService(type);
                    this.Services.AddService(type, instance);
                }
            }
        }


        private WriterAppHostConfig _Configs = new WriterAppHostConfig();
        /// <summary>
        /// 配置信息对象
        /// </summary>
        public WriterAppHostConfig Configs
        {
            get
            {
                return _Configs;
            }
            set
            {
                _Configs = value;
            }
        }
    }

    /// <summary>
    /// 应用程序宿主配置信息对象
    /// </summary>
    [Serializable]
    public class WriterAppHostConfig
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WriterAppHostConfig()
        {
        }



        private bool _EnableScript = true;
        /// <summary>
        /// 启用文档脚本
        /// </summary>
        [DefaultValue(true)]
        public bool EnableScript
        {
            get
            {
                return _EnableScript;
            }
            set
            {
                _EnableScript = value;
            }
        }
    }
}
