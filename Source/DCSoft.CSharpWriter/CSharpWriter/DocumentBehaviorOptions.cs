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

namespace DCSoft.CSharpWriter
{
    /// <summary>
    /// 文档行为设置
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [TypeConverter(typeof(CommonTypeConverter))]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DocumentBehaviorOptions : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentBehaviorOptions()
        {
        }

        private bool _DesignMode = false;
        /// <summary>
        /// 编辑器是否处于设计模式。
        /// </summary>
        [DefaultValue( false )]
        public bool DesignMode
        {
            get 
            {
                return _DesignMode; 
            }
            set
            {
                _DesignMode = value; 
            }
        }

        private bool _DebugMode = false;
        /// <summary>
        /// 系统是否处于调试模式。若为true，则系统处于调试模式，系统会输出一些调试文本信息。
        /// 默认为false。
        /// </summary>
        [DefaultValue( false )]
        public bool DebugMode
        {
            get
            {
                return _DebugMode; 
            }
            set
            {
                _DebugMode = value; 
            }
        }

        private bool _EnableExpression = true;
        /// <summary>
        /// 是否允许表达式。如果为false，则系统不执行表达式，级联模板功能也无法运行。
        /// 默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool EnableExpression
        {
            get
            {
                return _EnableExpression; 
            }
            set
            {
                _EnableExpression = value; 
            }
        }

        private bool _Printable = true ;
        /// <summary>
        /// 文档能否打印。若为false则文档不能打印。默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool Printable
        {
            get
            {
                return _Printable; 
            }
            set
            {
                _Printable = value; 
            }
        }

        //private bool _Readonly = false;
        ///// <summary>
        ///// 文档只读
        ///// </summary>
        //[DefaultValue( false )]
        //public bool Readonly
        //{
        //    get { return _Readonly; }
        //    set { _Readonly = value; }
        //}

        private bool _EnableScript = true;
        /// <summary>
        /// 允许VBA宏脚本。默认为true。
        /// </summary>
        [DefaultValue( true )]
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

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DocumentBehaviorOptions opt = (DocumentBehaviorOptions)this.MemberwiseClone();
            return opt;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentBehaviorOptions Clone()
        {
            return (DocumentBehaviorOptions)((ICloneable)this).Clone();
        }
    }
}
