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
using DCSoft.CSharpWriter.Dom;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Script
{
    /// <summary>
    /// 表达式对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class DomExpression
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomExpression()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 对象名称
        /// </summary>
        [DefaultValue( null )]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private string _Expression = null;
        /// <summary>
        /// 表达式文本
        /// </summary>
        [DefaultValue( null)]
        public string Expression
        {
            get
            {
                return _Expression; 
            }
            set
            {
                _Expression = value;
                _ReferencedElements = null;
                _ReferencedElementsRefreshed = false;
            }
        }

        private bool _ReferencedElementsRefreshed = false;

        internal bool ReferencedElementsRefreshed
        {
            get
            {
                return _ReferencedElementsRefreshed; 
            }
            set
            {
                _ReferencedElementsRefreshed = value; 
            }
        }

        [NonSerialized]
        private DomElementList _ReferencedElements = null;
        /// <summary>
        /// 表达式中引用的文档元素对象
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public DomElementList ReferencedElements
        {
            get
            {
                return _ReferencedElements; 
            }
            set
            {
                _ReferencedElements = value; 
            }
        }

        private DomExpressionType _Type = DomExpressionType.Simple;
        /// <summary>
        /// 简单的表达式格式
        /// </summary>
        [DefaultValue( DomExpressionType.Simple )]
        public DomExpressionType Type
        {
            get
            {
                return _Type; 
            }
            set
            {
                _Type = value; 
            }
        }
    }

    /// <summary>
    /// 表达式类型
    /// </summary>
    public enum DomExpressionType
    {
        /// <summary>
        /// 简单的表达式类型
        /// </summary>
        Simple ,
        /// <summary>
        /// 脚本表达式
        /// </summary>
        Script,
        /// <summary>
        ///  自定义表达式
        /// </summary>
        Custom
    }
}
