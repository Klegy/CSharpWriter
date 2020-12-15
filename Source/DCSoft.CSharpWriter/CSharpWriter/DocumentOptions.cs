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
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.CSharpWriter.Security;
using System.Configuration;
using System.Reflection;

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文档选项对象
    /// </summary>
    /// <remarks>
    /// 编写 袁永福
    /// </remarks>
    [Serializable()]
    [TypeConverter(typeof( CommonTypeConverter))]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DocumentOptions : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentOptions()
        {
        }

        /// <summary>
        /// 从配置文件中加载系统配置
        /// </summary>
        public void LoadConfig()
        {
            System.Collections.Specialized.NameValueCollection settings
                = ConfigurationManager.AppSettings;
            if (settings == null)
            {
                return;
            }
            string prefix = "DCSoft.CSharpWriter.Options.";
            foreach (string key in settings.AllKeys)
            {
                string key2 = key.Trim().ToLower();
                if (key2.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    continue;
                }
                string v = settings[key];
                try
                {

                    key2 = key2.Substring(prefix.Length);
                    int index = key2.IndexOf(".");
                    if (index > 0)
                    {
                        string sectionName = key2.Substring(0, index).Trim();
                        string itemName = key2.Substring(index + 1).Trim();
                        System.Reflection.PropertyInfo p1 = this.GetType().GetProperty(
                            sectionName,
                            BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (p1 != null)
                        {
                            object option = p1.GetValue(this, null);
                            if (option != null)
                            {
                                PropertyInfo p2 = option.GetType().GetProperty(
                                    itemName,
                                    BindingFlags.Instance | BindingFlags.IgnoreCase);
                                if (p2 != null)
                                {
                                    TypeConverter tc = TypeDescriptor.GetConverter(p2.PropertyType);
                                    if (tc.CanConvertFrom(typeof(string)))
                                    {
                                        object newValue = tc.ConvertFrom(v);
                                        p2.SetValue(option, newValue, null);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ext)
                {
                    System.Diagnostics.Debug.WriteLine(key + "=" + v + ":" + ext.Message);
                }

            }//foreach
        }
   

        public static void WriteName()
        {
            foreach (System.Reflection.PropertyInfo p in typeof(DocumentOptions).GetProperties())
            {
                foreach (System.Reflection.PropertyInfo p2 in p.PropertyType.GetProperties())
                {
                    System.Console.WriteLine(
                        "case \"dcsoft.writer.options." + p.Name.ToLower() + "." + p2.Name.ToLower() + "\":\r\n this." + p.Name + "." + p2.Name + "=1; \r\nbreak;");
                }
            }

            foreach (System.Reflection.PropertyInfo p in typeof(DocumentOptions).GetProperties())
            {
                foreach (System.Reflection.PropertyInfo p2 in p.PropertyType.GetProperties())
                {
                    System.Console.WriteLine(
                        "<add key=\"DCSoft.CSharpWriter.Options." + p.Name + "." + p2.Name  + "\" value=\"" + p2.PropertyType.Name + "\" />");
                }
            }
        }

        private DocumentSecurityOptions _SecurityOptions = new DocumentSecurityOptions();
        /// <summary>
        /// 安全和权限设置信息对象
        /// </summary>
        public DocumentSecurityOptions SecurityOptions
        {
            get
            {
                if (_SecurityOptions == null)
                {
                    _SecurityOptions = new DocumentSecurityOptions();
                }
                return _SecurityOptions; 
            }
            set
            {
                _SecurityOptions = value; 
            }
        }

        private DocumentViewOptions _ViewOptions = new DocumentViewOptions();
        /// <summary>
        /// 视图选项
        /// </summary>
        public DocumentViewOptions ViewOptions
        {
            get
            {
                if (_ViewOptions == null)
                {
                    _ViewOptions = new DocumentViewOptions();
                }
                return _ViewOptions; 
            }
            set
            {
                _ViewOptions = value; 
            }
        }

        private DocumentBehaviorOptions _BehaviorOptions = new DocumentBehaviorOptions();
        /// <summary>
        /// 行为选项
        /// </summary>
        public DocumentBehaviorOptions BehaviorOptions
        {
            get
            {
                if (_BehaviorOptions == null)
                {
                    _BehaviorOptions = new DocumentBehaviorOptions();
                }
                return _BehaviorOptions; 
            }
            set
            {
                _BehaviorOptions = value; 
            }
        }

        private DocumentEditOptions _EditOptions = new DocumentEditOptions();
        /// <summary>
        /// 编辑选项
        /// </summary>
        public DocumentEditOptions EditOptions
        {
            get
            {
                if (_EditOptions == null)
                {
                    _EditOptions = new DocumentEditOptions();
                }
                return _EditOptions; 
            }
            set 
            {
                _EditOptions = value; 
            }
        }

        

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DocumentOptions opt = new DocumentOptions();
            if (this._EditOptions != null)
            {
                opt._EditOptions = this._EditOptions.Clone();
            }
            if (this._ViewOptions != null)
            {
                opt._ViewOptions = this._ViewOptions.Clone();
            }
            if (this._BehaviorOptions != null)
            {
                opt._BehaviorOptions = this._BehaviorOptions.Clone();
            }
            if (this._SecurityOptions != null)
            {
                opt._SecurityOptions = this._SecurityOptions.Clone();
            }
            return opt;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentOptions Clone()
        {
            return (DocumentOptions)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// DocumentOptions类型配套的类型转换器
    /// </summary>
    public class DocumentOptionsTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }
    }

}
