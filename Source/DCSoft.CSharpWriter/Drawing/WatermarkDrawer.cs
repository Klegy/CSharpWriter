/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
namespace DCSoft.Drawing
{
    /// <summary>
    /// 绘制水印的类型
    /// </summary>
    [Serializable()]
    public class WatermarkDrawer
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public WatermarkDrawer()
        {
        }

        private XFontValue myFont = new XFontValue();
        /// <summary>
        /// 绘制文本使用的字体
        /// </summary>
        public XFontValue Font
        {
            get
            {
                return myFont;
            }
            set
            {
                myFont = value;
            }
        }

        private Color intColor = Color.Black;
        /// <summary>
        /// 颜色值
        /// </summary>
        public Color Color
        {
            get
            {
                return intColor;
            }
            set
            {
                intColor = value;
            }
        }

        private int intalpha = 80;
        /// <summary>
        /// 透明度
        /// </summary>
        public int Alpha
        {
            get 
            { 
                return intalpha; 
            }
            set
            {
                if (value > 255) value = 255;
                if (value < 0) value = 0;
                intalpha = value; 
            }
        }


        private string strText = null;
        /// <summary>
        /// 文本
        /// </summary>
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

        private bool bolRepeat = false;
        /// <summary>
        /// 重复绘制水印
        /// </summary>
        public bool Repeat
        {
            get
            {
                return bolRepeat;
            }
            set
            {
                bolRepeat = value;
            }
        }

        private RectangleF myBounds = RectangleF.Empty ;
        /// <summary>
        /// 绘制水印的区域
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return myBounds ;
            }
            set
            {
                myBounds = value;
            }
        }

        private float fAngle = 0f;
        /// <summary>
        /// 绘制文本的角度
        /// </summary>
        public float Angle
        {
            get
            {
                return fAngle;
            }
            set
            {
                fAngle = value;
            }
        }

        private PointF PeripheralPoint(PointF pCenter, float r ,float angleDegree)
        {
            float xCenter = pCenter.X;
            float yCenter = pCenter.Y;
            angleDegree = angleDegree % 360;
            double angleRadian = angleDegree * Math.PI / 180;//将角度转换为弧度
            double tan = Math.Tan(angleRadian);
            double x = r / Math.Sqrt(1+ tan * tan);
            double y = x * tan;
            if (angleDegree > 90 && angleDegree < 270)
            {
                return new PointF(xCenter - (float)x, yCenter - (float)y);
            }
            else
            {
                return new PointF(xCenter + (float)x, yCenter + (float)y);
            }
        }

        private void DrawOne(Graphics graphics, Rectangle clipRectangle, float x ,float y)
        {
            SizeF strSize = graphics.MeasureString(this.Text, this.Font.Value);
            RectangleF strRec = new RectangleF(x,y, strSize.Width,strSize.Height);
            //graphics.DrawRectangle(Pens.Black, Rectangle.Round(strRec));

            PointF pCenter = new PointF(strRec.Left + strRec.Width / 2, strRec.Top + strRec.Height / 2);
            float r = (float)Math.Sqrt(strRec.Width * strRec.Width + strRec.Height * strRec.Height) / 2;
            float angle = (float)(Math.Atan(strRec.Height / strRec.Width) * (180 / Math.PI));

            PointF p1 = PeripheralPoint(pCenter, r, this.Angle + 180 + angle);
            PointF p2 = PeripheralPoint(pCenter, r, this.Angle - angle);
            PointF p3 = PeripheralPoint(pCenter, r, this.Angle + angle);
            PointF p4 = PeripheralPoint(pCenter, r, this.Angle + 180 - angle);

            GraphicsPath path = new GraphicsPath();
            path.AddLines(new PointF[] { p1, p2, p3, p4 });

            RectangleF newRec = path.GetBounds();
            //graphics.DrawRectangle(Pens.Green, Rectangle.Round(newRec));

            if (newRec.IntersectsWith(clipRectangle))
            {
                graphics.TranslateTransform(p1.X, p1.Y);
                graphics.RotateTransform(this.Angle);
                using (SolidBrush b = new SolidBrush(Color.FromArgb(this.Alpha, this.Color)))
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                    graphics.DrawString(this.Text, this.Font.Value, b, 0, 0, stringFormat);
                }
                graphics.ResetTransform();
            }

        }

        public void Draw(Graphics graphics, Rectangle clipRectangle)
        {
            int padding = 30;
            SizeF strSize = graphics.MeasureString(this.Text, this.Font.Value);

            float x = this.Bounds.Left + this.Bounds.Width / 2 - strSize.Width / 2;
            float y = this.Bounds.Top + this.Bounds.Height / 2 - strSize.Height / 2;
            if (this.Repeat)
            {
                for (float xx = padding; xx < this.Bounds.Right; xx += padding + strSize.Width)
                {
                    for (float yy = padding; yy < this.Bounds.Bottom; yy += padding + strSize.Height)
                    {
                        DrawOne(graphics, clipRectangle, xx, yy);
                    }
                }
            }
            else
            {
                DrawOne(graphics, clipRectangle, x, y);
            }           

        }
    }//public class WatermarkDrawer
}
