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
using DCSoft.Drawing;
using System.ComponentModel ;
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 段落格式命令参数对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable ]
    public class ParagraphFormatCommandParameter
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ParagraphFormatCommandParameter()
        {
        }
        
        /// <summary>
        /// 从文档样式中读取设置
        /// </summary>
        /// <param name="style">文档样式</param>
        public void Read(DocumentContentStyle style)
        {
            if (style == null)
            {
                throw new ArgumentNullException("style");
            }
            this.LineSpacing = style.LineSpacing;
            this.LeftIndent = style.LeftIndent;
            this.LineSpacingStyle = style.LineSpacingStyle;
            this.SpacingAfter = style.SpacingAfterParagraph;
            this.SpacingBefore = style.SpacingBeforeParagraph;
            this.FirstLineIndent = style.FirstLineIndent;
        }

        /// <summary>
        /// 将设置写入到文档样式中
        /// </summary>
        /// <param name="style">文档样式</param>
        public void Save(DocumentContentStyle style)
        {
            if (style == null)
            {
                throw new ArgumentNullException("style");
            }
            style.FirstLineIndent = this.FirstLineIndent;
            style.LeftIndent = this.LeftIndent;
            style.LineSpacing = this.LineSpacing;
            style.LineSpacingStyle = this.LineSpacingStyle;
            style.SpacingAfterParagraph = this.SpacingAfter;
            style.SpacingBeforeParagraph = this.SpacingBefore;
        }

        private LineSpacingStyle _LineSpacingStyle = LineSpacingStyle.SpaceSingle;
        /// <summary>
        /// 行间距样式
        /// </summary>
        [DefaultValue( LineSpacingStyle.SpaceSingle )]
        public LineSpacingStyle LineSpacingStyle
        {
            get
            {
                return _LineSpacingStyle; 
            }
            set
            {
                _LineSpacingStyle = value; 
            }
        }

        private float _LineSpacing = 0f;
        /// <summary>
        /// 行间距
        /// </summary>
        [DefaultValue( 0f )]
        public float LineSpacing
        {
            get
            {
                return _LineSpacing; 
            }
            set
            {
                _LineSpacing = value; 
            }
        }

        private float _SpacingBefore = 0f;
        /// <summary>
        /// 段落前间距
        /// </summary>
        [DefaultValue( 0f )]
        public float SpacingBefore
        {
            get
            {
                return _SpacingBefore; 
            }
            set
            {
                _SpacingBefore = value; 
            }
        }

        private float _SpacingAfter = 0f;
        /// <summary>
        /// 段落后间距
        /// </summary>
        [DefaultValue( 0f )]
        public float SpacingAfter
        {
            get
            {
                return _SpacingAfter; 
            }
            set
            {
                _SpacingAfter = value; 
            }
        }

        private float _FirstLineIndent = 0f;
        /// <summary>
        /// 首行缩进量
        /// </summary>
        [DefaultValue( 0f )]
        public float FirstLineIndent
        {
            get
            {
                return _FirstLineIndent; 
            }
            set
            {
                _FirstLineIndent = value; 
            }
        }

        private float _LeftIndent = 0f;
        /// <summary>
        /// 段落左边缩进量
        /// </summary>
        [DefaultValue( 0f )]
        public float LeftIndent
        {
            get
            {
                return _LeftIndent; 
            }
            set
            {
                _LeftIndent = value; 
            }
        }
    }
}
