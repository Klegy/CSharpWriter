/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
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
    /// paragraph element
    /// </summary>
    [Serializable()]
    public class RTFDomParagraph : RTFDomElement
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public RTFDomParagraph()
        {
        }

        internal bool TemplateGenerated = false;
        /// <summary>
        /// 是否是临时生成的段落对象
        /// </summary>
        public bool IsTemplateGenerated
        {
            get
            {
                return TemplateGenerated;
            }
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
        public override string InnerText
        {
            get
            {
                return base.InnerText + Environment.NewLine ;
            }
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("Paragraph");
            if (this.Format != null)
            {
                str.Append("(" + this.Format.Align + ")");
                if (this.Format.NumberedList)
                {
                    str.Append("(NumberedList)");
                }
                else if (this.Format.BulletedList)
                {
                    str.Append("(BulletedList)");
                }
            }
            
            return str.ToString();
        }
    }
}
