/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 应用程序错误报告者
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class ErrorReporter
    {
        private static string _ReportServiceUrl = null;
        /// <summary>
        /// 错误报告页面地址
        /// </summary>
        public static string ReportServiceUrl
        {
            get
            {
                return _ReportServiceUrl; 
            }
            set
            {
                _ReportServiceUrl = value; 
            }
        }

        public static void ReportError( IWin32Window parentForm , Type sourceType, string userMessage, Exception ext)
        {
            ErrorReportInfo info = CreateInfo(sourceType, userMessage, ext);
            string dm = "Error:" + userMessage ;
            if( ext != null )
            {
                dm = dm + " " + ext.Message ;
            }
            System.Diagnostics.Debug.WriteLine(dm);
            if (System.Environment.UserInteractive)
            {
                using (dlgError dlg = new dlgError())
                {
                    dlg.ReportInfo = info;
                    dlg.ShowDialog(parentForm);
                }
            }
        }

        public static ErrorReportInfo CreateInfo( 
            Type sourceType ,
            string userMessage,
            Exception ext)
        {
            ErrorReportInfo info = new ErrorReportInfo();
            info.UserMessage = userMessage;
            try
            {
                info.ApplicationName = System.Windows.Forms.Application.ProductName;
            }
            catch (Exception ext2)
            {
                info.ApplicationName = ext2.Message;
            }
            try
            {
                info.AppVersion = System.Windows.Forms.Application.ProductVersion;
            }
            catch (Exception e2)
            {
                info.AppVersion = e2.Message;
            }
            try
            {
                info.CommandLine = System.Environment.CommandLine;
            }
            catch (Exception e2)
            {
                info.CommandLine = e2.Message;
            }
            try
            {
                info.AppPath = System.Windows.Forms.Application.ExecutablePath;
            }
            catch (Exception e)
            {
                info.AppPath = e.Message;
            }
            try
            {
                info.OSVersion = System.Environment.OSVersion.ToString();
            }
            catch (Exception e)
            {
                info.OSVersion = e.Message;
            }
            try
            {
                info.RuntimeVersion = System.Environment.Version.ToString();
            }
            catch (Exception e)
            {
                info.RuntimeVersion = e.Message;
            }
            if (ext != null)
            {
                info.ExceptionString = ext.ToString();
                info.SystemMessage = ext.Message;
            }
            if (sourceType != null)
            {
                info.SourceType = sourceType.FullName;
                AssemblyName asmName = new System.Reflection.AssemblyName(sourceType.Assembly.FullName);
                info.SourceModuleName = asmName.Name;
                info.SourceModuleVersion = asmName.Version.ToString();
                info.SourceModuleCodeBase = sourceType.Assembly.CodeBase;
            }
            return info;
        }
    }

    /// <summary>
    /// 错误报告信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class ErrorReportInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ErrorReportInfo()
        {
        }

        private string _ApplicationName = null;
        /// <summary>
        /// 应用程序名称
        /// </summary>
        [DefaultValue( null )]
        public string ApplicationName
        {
            get
            {
                return _ApplicationName; 
            }
            set
            {
                _ApplicationName = value; 
            }
        }

        private string _AppPath = null;
        /// <summary>
        /// 应用程序路径
        /// </summary>
        [DefaultValue(null)]
        public string AppPath
        {
            get { return _AppPath; }
            set { _AppPath = value; }
        }

        private string _DateTime = null;
        /// <summary>
        /// 提交报告时的时间
        /// </summary>
        [DefaultValue(null)]
        public string DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        private string _OSVersion = null;
        /// <summary>
        /// 操作系统版本号
        /// </summary>
        [DefaultValue( null )]
        public string OSVersion
        {
            get { return _OSVersion; }
            set { _OSVersion = value; }
        }

        private string _CommandLine = null;
        /// <summary>
        /// 命令行文本
        /// </summary>
        [DefaultValue(null)]
        public string CommandLine
        {
            get { return _CommandLine; }
            set { _CommandLine = value; }
        }

        private string _AppVersion = null;
        /// <summary>
        /// 应用程序版本号
        /// </summary>
        [DefaultValue(null)]
        public string AppVersion
        {
            get { return _AppVersion; }
            set { _AppVersion = value; }
        }

        private string _SourceType = null;
        /// <summary>
        /// 发生错误的相关类型
        /// </summary>
        [DefaultValue( null )]
        public string SourceType
        {
            get { return _SourceType; }
            set { _SourceType = value; }
        }
        private string _SourceModuleName = null;
        /// <summary>
        /// 出错的程序模块名称
        /// </summary>
        [DefaultValue(null)]
        public string SourceModuleName
        {
            get { return _SourceModuleName; }
            set { _SourceModuleName = value; }
        }

        private string _SourceModuleVersion = null;
        /// <summary>
        /// 出错的程序模块版本号
        /// </summary>
        [DefaultValue(null)]
        public string SourceModuleVersion
        {
            get { return _SourceModuleVersion; }
            set { _SourceModuleVersion = value; }
        }

        private string _SourceModuleCodeBase = null;
        /// <summary>
        /// 出错的程序模块基础代码
        /// </summary>
        [DefaultValue( null )]
        public string SourceModuleCodeBase
        {
            get { return _SourceModuleCodeBase; }
            set { _SourceModuleCodeBase = value; }
        }

        private string _RuntimeVersion = null;
        /// <summary>
        /// .NET运行时版本号
        /// </summary>
        [DefaultValue(null)]
        public string RuntimeVersion
        {
            get { return _RuntimeVersion; }
            set { _RuntimeVersion = value; }
        }

        private string _UserMessage = null;
        /// <summary>
        /// 用户信息
        /// </summary>
        [DefaultValue(null)]
        public string UserMessage
        {
            get { return _UserMessage; }
            set { _UserMessage = value; }
        }

        private string _SystemMessage = null;
        /// <summary>
        /// 系统消息
        /// </summary>
        [DefaultValue(null)]
        public string SystemMessage
        {
            get { return _SystemMessage; }
            set { _SystemMessage = value; }
        }

        private string _ExceptionString = null;
        /// <summary>
        /// 完整的异常信息
        /// </summary>
        [DefaultValue(null)]
        public string ExceptionString
        {
            get { return _ExceptionString; }
            set { _ExceptionString = value; }
        }

        public string ToXMLString()
        {
            StringWriter myStr = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(myStr);
            writer.Indentation = 1;
            writer.IndentChar = '\t';
            writer.Formatting = Formatting.Indented;
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(this.GetType());
            ser.Serialize(writer, this);
            writer.Close();
            return myStr.ToString();
        }
    }
}
