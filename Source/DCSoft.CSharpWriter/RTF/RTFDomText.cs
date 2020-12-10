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
using System.Text;

namespace DCSoft.RTF
{
    /// <summary>
    /// text element
    /// </summary>
    [Serializable()]
    public class RTFDomText : RTFDomElement
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public RTFDomText()
        {
            // text element can not contains any child element
            this.Locked = true;
        }

        private DocumentFormatInfo myFormat = new DocumentFormatInfo();
        /// <summary>
        /// format
        /// </summary>
        public DocumentFormatInfo Format
        {
            get
            {
                return myFormat;
            }
            set
            {
                myFormat = value;
            }
        }

        private string strText = null;
        /// <summary>
        /// text
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Text
        {
            get
            {
                return strText;
            }
            set
            {
                strText = value;
            }
        }
        public override string InnerText
        {
            get
            {
                return strText;
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Text");
            if (this.Format != null)
            {
                if (this.Format.Hidden)
                {
                    str.Append("(Hidden)");
                }
            }
            str.Append(":" + strText);
            return str.ToString();
        }
    }
}
