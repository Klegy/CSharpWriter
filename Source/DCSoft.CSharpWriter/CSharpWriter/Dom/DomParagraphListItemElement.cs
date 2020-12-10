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
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Dom
{

    /// <summary>
    /// 段落列表元素
    /// </summary>
    internal class DomParagraphListItemElement : DomElement
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomParagraphListItemElement()
        {
        }



        public override void Draw(DocumentPaintEventArgs args)
        {
            DomContentLine line = this.OwnerLine;
            RectangleF rect = this.AbsBounds;
            if (line.ParagraphListStyle == ParagraphListStyle.BulletedList)
            {
                float size = Math.Min(rect.Width *0.6f , rect.Height * 0.6f);
                RectangleF circleRect = new RectangleF(rect.Left + size / 2 , rect.Top + size / 2  , size , size );
                args.Graphics.FillEllipse(Brushes.Black, circleRect); 
            }
            else if (line.ParagraphListStyle == ParagraphListStyle.NumberedList)
            {
                using (System.Drawing.StringFormat f
                            = new System.Drawing.StringFormat())
                {
                    XFontValue font = this.RuntimeStyle.Font;
                    f.Alignment = System.Drawing.StringAlignment.Near ;
                    f.LineAlignment = System.Drawing.StringAlignment.Center;
                    f.FormatFlags = System.Drawing.StringFormatFlags.NoWrap;
                    args.Graphics.DrawString(
                        line.ParagraphStyleIndex + ".",
                        font.Value,
                        System.Drawing.Brushes.Black,
                        rect,
                        f);
                }
            }
        }
    }
}
