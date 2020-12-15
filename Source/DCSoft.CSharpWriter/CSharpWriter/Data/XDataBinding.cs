using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Data;
using System.Xml;
using System.Collections;


namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 数据源绑定信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class XDataBinding : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDataBinding()
        {
         
        }

        //private char _PathSpliter

        private string _DataSource = null;
        /// <summary>
        /// 数据源名称
        /// </summary>
        [DefaultValue(null)]
        public string DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
            }
        }

        private string _FormatString = null;
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormatString
        {
            get
            {
                return _FormatString; 
            }
            set
            {
                _FormatString = value; 
            }
        }

        private string _BindingPath = null;
        /// <summary>
        /// 绑定路径
        /// </summary>
        [DefaultValue(null)]
        public string BindingPath
        {
            get
            {
                return _BindingPath;
            }
            set
            {
                _BindingPath = value;
            }
        }

        private bool _AutoUpdate = false;
        /// <summary>
        /// 自动更新内容
        /// </summary>
        [DefaultValue( false )]
        public bool AutoUpdate
        {
            get
            {
                return _AutoUpdate; 
            }
            set
            {
                _AutoUpdate = value; 
            }
        }

        private bool _Readonly = true;
        /// <summary>
        /// 只读标记
        /// </summary>
        [DefaultValue(true)]
        public bool Readonly
        {
            get
            {
                return _Readonly;
            }
            set
            {
                _Readonly = value;
            }
        }

        

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XDataBinding Clone()
        {
            return (XDataBinding)((ICloneable)this).Clone();
        }
     
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XDataBinding binding = (XDataBinding)this.MemberwiseClone();
            return binding;
        }

        /// <summary>
        /// 获得 表示对象内容的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            string txt = this.DataSource;
            if (string.IsNullOrEmpty(this.BindingPath) == false)
            {
                txt = txt + "[" + this.BindingPath + "]";
            }
            return txt;
        }
    }

    //public interface IDataBindableElement
    //{
    //    XDataBinding Binding { get; set; }

    //}

    
}
