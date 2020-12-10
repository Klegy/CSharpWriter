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
using DCSoft.CSharpWriter.RTF;
namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 分页元素
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [System.Xml.Serialization.XmlType("XPageBreak")]
    public class DomPageBreakElement : DomElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomPageBreakElement()
        {
            this.Height = 20;
        }

        public override void WriteRTF(RTFContentWriter writer)
        {
            writer.WriteKeyword("page");
        }

        private bool _Handled = false;
        /// <summary>
        /// 是否处理过了分页
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        internal bool Handled
        {
            get
            {
                return _Handled; 
            }
            set
            {
                _Handled = value; 
            }
        }
    }
}
