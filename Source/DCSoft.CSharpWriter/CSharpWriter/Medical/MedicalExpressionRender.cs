using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Medical
{
    /// <summary>
    /// 医学表达式图形呈现器
    /// </summary>
    public class MedicalExpressionRender
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public MedicalExpressionRender()
        { 
        }

        private int _SourceVersion = -1;
        /// <summary>
        /// 数据来源版本号
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public int SourceVersion
        {
            get
            {
                return _SourceVersion; 
            }
            set
            {
                _SourceVersion = value; 
            }
        }

        private XFontValue _Font = new XFontValue();
        /// <summary>
        /// 字体
        /// </summary>
        public XFontValue Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        private Color _ForeColor = Color.Black;
        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        private string _Value1 = null;
        /// <summary>
        /// 数值1
        /// </summary>
        public string Value1
        {
            get { return _Value1; }
            set { _Value1 = value; }
        }

        private string _Value2 = null;
        /// <summary>
        /// 数值2
        /// </summary>
        public string Value2
        {
            get { return _Value2; }
            set { _Value2 = value; }
        }

        private string _Value3 = null;
        /// <summary>
        /// 数值3
        /// </summary>
        public string Value3
        {
            get { return _Value3; }
            set { _Value3 = value; }
        }

        private string _Value4 = null;
        /// <summary>
        /// 数值4
        /// </summary>
        public string Value4
        {
            get { return _Value4; }
            set { _Value4 = value; }
        }

        private MedicalExpressionStyle _Style = MedicalExpressionStyle.FourValues;
        /// <summary>
        /// 表达式样式
        /// </summary>
        public MedicalExpressionStyle Style
        {
            get { return _Style; }
            set { _Style = value; }
        }
         
        /// <summary>
        /// 计算合适的大小
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <returns>对象的大小</returns>
        public SizeF GetPreferredSize(Graphics g)
        {
            return InnerRender(g, RectangleF.Empty, true);
        }

        /// <summary>
        /// 绘制对象
        /// </summary>
        /// <param name="g">画布对象</param>
        /// <param name="bounds">对象边界</param>
        public void Render(Graphics g, RectangleF bounds)
        {
            InnerRender(g, bounds, false);
        }

        /// <summary>
        /// 在指定边界呈现表达式图形
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="bounds">边界</param>
        private SizeF InnerRender(Graphics g, RectangleF bounds, bool getPreferredSize)
        {
            float step = (float)GraphicsUnitConvert.Convert(1.0, GraphicsUnit.Pixel, g.PageUnit);
            if (this.Style == MedicalExpressionStyle.FourValues)
            {
                SizeF s1 = GetSize(this.Value1, g);
                SizeF s2 = GetSize(this.Value2, g);
                SizeF s3 = GetSize(this.Value3, g);
                SizeF s4 = GetSize(this.Value4, g);
                SizeF preferredSize = SizeF.Empty;
                preferredSize.Width = s1.Width + Math.Max(s2.Width, s3.Width) + s4.Width;
                preferredSize.Height = (s2.Height + s3.Height) * 1.1f;
                if (getPreferredSize)
                {
                    // 计算内容大小
                    return preferredSize;
                }
                RectangleF clientBounds = new RectangleF(
                    bounds.Left + (bounds.Width - preferredSize.Width) / 2,
                    bounds.Top + (bounds.Height - preferredSize.Height) / 2,
                    preferredSize.Width,
                    preferredSize.Height);
                using (SolidBrush b = new SolidBrush(this.ForeColor))
                {
                    if (string.IsNullOrEmpty(this.Value1) == false)
                    {
                        g.DrawString(
                            this.Value1,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Left,
                                clientBounds.Top,
                                s1.Width,
                                clientBounds.Height),
                            TextFormat);
                    }
                    if (string.IsNullOrEmpty(this.Value2) == false)
                    {
                        g.DrawString(
                            this.Value2,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Left + s1.Width,
                                clientBounds.Top  + step * 2 ,
                                Math.Max(s2.Width, s3.Width),
                                s2.Height ),
                            TextFormat);
                    }
                    if (string.IsNullOrEmpty(this.Value3) == false)
                    {
                        g.DrawString(
                            this.Value3,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Left + s1.Width,
                                clientBounds.Bottom - s3.Height,
                                Math.Max(s2.Width, s3.Width),
                                s3.Height),
                            TextFormat);
                    }
                    if (string.IsNullOrEmpty(this.Value4) == false)
                    {
                        g.DrawString(
                            this.Value4,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Right - s4.Width,
                                clientBounds.Top,
                                s4.Width,
                                clientBounds.Height),
                            TextFormat);
                    }
                    using (Pen p = new Pen(this.ForeColor))
                    {
                        g.DrawLine(
                            p,
                            clientBounds.Left + s1.Width,
                            clientBounds.Top + clientBounds.Height / 2,
                            clientBounds.Right - s4.Width,
                            clientBounds.Top + clientBounds.Height / 2);
                    }
                }//using

                return preferredSize;
            }
            else if (this.Style == MedicalExpressionStyle.ThreeValues)
            {
                SizeF s1 = GetSize(this.Value1 + "/", g);
                SizeF s2 = GetSize(this.Value2, g);
                SizeF s3 = GetSize(this.Value3, g);
                SizeF preferredSize = SizeF.Empty;
                preferredSize.Width = s1.Width + Math.Max(s2.Width, s3.Width);
                preferredSize.Height = (s2.Height + s3.Height) * 1.1f;
                if (getPreferredSize)
                {
                    // 计算内容大小
                    return preferredSize;
                }
                RectangleF clientBounds = new RectangleF(
                    bounds.Left + (bounds.Width - preferredSize.Width) / 2,
                    bounds.Top + (bounds.Height - preferredSize.Height) / 2,
                    preferredSize.Width,
                    preferredSize.Height);
                using (SolidBrush b = new SolidBrush(this.ForeColor))
                {
                    g.DrawString(
                        this.Value1 + "/",
                        this.Font.Value,
                        b,
                        new RectangleF(
                            clientBounds.Left,
                            clientBounds.Top,
                            s1.Width,
                            clientBounds.Height),
                        TextFormat);

                    if (string.IsNullOrEmpty(this.Value2) == false)
                    {
                        g.DrawString(
                            this.Value2,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Left + s1.Width,
                                clientBounds.Top + step * 2 ,
                                Math.Max(s2.Width, s3.Width),
                                s2.Height),
                            TextFormat);
                    }
                    if (string.IsNullOrEmpty(this.Value3) == false)
                    {
                        g.DrawString(
                            this.Value3,
                            this.Font.Value,
                            b,
                            new RectangleF(
                                clientBounds.Left + s1.Width,
                                clientBounds.Bottom - s3.Height,
                                Math.Max(s2.Width, s3.Width),
                                s3.Height),
                            TextFormat);
                    }

                    using (Pen p = new Pen(this.ForeColor))
                    {
                        g.DrawLine(
                            p,
                            clientBounds.Left + s1.Width,
                            clientBounds.Top + clientBounds.Height / 2,
                            clientBounds.Right,
                            clientBounds.Top + clientBounds.Height / 2);
                    }
                }
                return preferredSize;
            }

            return SizeF.Empty;
        }



        private SizeF GetSize(string text, Graphics g)
        {
            SizeF result = SizeF.Empty;
            if (text == null || text.Length == 0 )
            {
                result = new SizeF(
                     GraphicsUnitConvert.Convert(10, GraphicsUnit.Pixel, g.PageUnit),
                    this.Font.GetHeight(g));
            }
            else
            {
                result = g.MeasureString(text, this.Font.Value, 100000, TextFormat);
            }
            result.Height = this.Font.GetHeight( g ) + GraphicsUnitConvert.Convert( 4 , GraphicsUnit.Pixel , g.PageUnit );
            return result;
        }


        private static StringFormat _TextFormat = null;
        private static StringFormat TextFormat
        {
            get
            {
                if (_TextFormat == null)
                {
                    _TextFormat = new StringFormat();
                    _TextFormat.Alignment = StringAlignment.Center;
                    _TextFormat.LineAlignment = StringAlignment.Center   ;
                    _TextFormat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip ;
                }
                return _TextFormat;
            }
        }

    }

    /// <summary>
    /// 医学表达式的样式
    /// </summary>
    public enum MedicalExpressionStyle
    {
        /// <summary>
        /// 4个数值的样式
        /// </summary>
        /// <remarks>
        ///                  Value2
        /// 样式为   Value1 -------- Value4
        ///                  Value3
        /// </remarks>
        FourValues,
        /// <summary>
        /// 3个数值的样式
        /// </summary>
        /// <remarks>
        ///                    Value2
        /// 样式为   Value1 / --------
        ///                    Value3
        /// </remarks>
        ThreeValues
    }
}
