/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DCSoft.RTF
{
    /// <summary>
    /// 页眉元素
    /// </summary>
    [Serializable()]
    public class RTFDomHeader : RTFDomElement
    {
        private HeaderFooterStyle _Style = HeaderFooterStyle.AllPages;
        /// <summary>
        /// 页眉页脚样式
        /// </summary>
        [System.ComponentModel.DefaultValue( HeaderFooterStyle.AllPages )]
        public HeaderFooterStyle Style
        {
            get
            {
                return _Style; 
            }
            set
            {
                _Style = value; 
            }
        }
        public override string ToString()
        {
            return "Header " + this.Style;
        }

        /// <summary>
        /// 判断元素是否有实际内容
        /// </summary>
        public bool HasContentElement
        {
            get
            {
                return RTFUtil.HasContentElement(this);
            }
        }
    }

    /// <summary>
    /// 页脚元素
    /// </summary>
    [Serializable()]
    public class RTFDomFooter : RTFDomElement
    {
        private HeaderFooterStyle _Style = HeaderFooterStyle.AllPages;
        /// <summary>
        /// 页眉页脚样式
        /// </summary>
        [System.ComponentModel.DefaultValue(HeaderFooterStyle.AllPages)]
        public HeaderFooterStyle Style
        {
            get
            {
                return _Style;
            }
            set
            {
                _Style = value;
            }
        }


        /// <summary>
        /// 判断元素是否有实际内容
        /// </summary>
        public bool HasContentElement
        {
            get
            {
                return RTFUtil.HasContentElement(this);
            }
        }

        public override string ToString()
        {
            return "Footer " + this.Style;
        }
    }

    public enum HeaderFooterStyle
    {
        AllPages ,
        LeftPages ,
        RightPages ,
        FirstPage
    }
}
