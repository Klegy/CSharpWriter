/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;
using System.Drawing ;
using System.ComponentModel ;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 背景图案样式
    /// </summary>
    //[DCSoft.Common.DocumentComment()]
    [System.ComponentModel.Editor(
        "DCSoft.WinForms.Design.XBrushStyleConstEditor", 
        typeof(System.Drawing.Design.UITypeEditor))]
    public enum XBrushStyleConst
    {
        /// <summary>
        /// 对象被禁止
        /// </summary>
        Disabled = -2 ,
        /// <summary>
        /// 背景不启用图案,使用纯色填充
        /// </summary>
        Solid = - 1,
        ///// <summary>
        ///// 指定阴影样式 System.Drawing.Drawing2D.HatchStyle.Horizontal。</summary>
        ///// </summary>
        //Min = System.Drawing.Drawing2D.HatchStyle.Min ,

        /// <summary>
        /// 水平线的图案。
        /// </summary>
        Horizontal = System.Drawing.Drawing2D.HatchStyle.Horizontal,

        ///<summary>垂直线的图案。</summary>
        Vertical = System.Drawing.Drawing2D.HatchStyle.Vertical,

        ///<summary>从左上到右下的对角线的线条图案。</summary>
        ForwardDiagonal = System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal,

        ///<summary>从右上到左下的对角线的线条图案。</summary>
        BackwardDiagonal = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal,

        /////<summary>指定阴影样式 System.Drawing.Drawing2D.HatchStyle.SolidDiamond。</summary>
        //Max = System.Drawing.Drawing2D.HatchStyle.Max ,

        ///<summary>指定交叉的水平线和垂直线。</summary>
        Cross = System.Drawing.Drawing2D.HatchStyle.Cross,

        /////<summary>指定阴影样式 System.Drawing.Drawing2D.HatchStyle.Cross。</summary>
        //LargeGrid = System.Drawing.Drawing2D.HatchStyle.LargeGrid ,

        ///<summary>交叉对角线的图案。</summary>
        DiagonalCross = System.Drawing.Drawing2D.HatchStyle.DiagonalCross,

        ///<summary>指定 5% 阴影。前景色与背景色的比例为 5:100。</summary>
        Percent05 = System.Drawing.Drawing2D.HatchStyle.Percent05,

        ///<summary>指定 10% 阴影。前景色与背景色的比例为 10:100。</summary>
        Percent10 = System.Drawing.Drawing2D.HatchStyle.Percent10,

        ///<summary>指定 20% 阴影。前景色与背景色的比例为 20:100。</summary>
        Percent20 = System.Drawing.Drawing2D.HatchStyle.Percent20,

        ///<summary>指定 25% 阴影。前景色与背景色的比例为 25:100。</summary>
        Percent25 = System.Drawing.Drawing2D.HatchStyle.Percent25,

        ///<summary>指定 30% 阴影。前景色与背景色的比例为 30:100。</summary>
        Percent30 = System.Drawing.Drawing2D.HatchStyle.Percent30,

        ///<summary>指定 40% 阴影。前景色与背景色的比例为 40:100。</summary>
        Percent40 = System.Drawing.Drawing2D.HatchStyle.Percent40,

        ///<summary>指定 50% 阴影。前景色与背景色的比例为 50:100。</summary>
        Percent50 = System.Drawing.Drawing2D.HatchStyle.Percent50,

        ///<summary>指定 60% 阴影。前景色与背景色的比例为 60:100。</summary>
        Percent60 = System.Drawing.Drawing2D.HatchStyle.Percent60,

        ///<summary>指定 70% 阴影。前景色与背景色的比例为 70:100。</summary>
        Percent70 = System.Drawing.Drawing2D.HatchStyle.Percent70,

        ///<summary>指定 75% 阴影。前景色与背景色的比例为 75:100。</summary>
        Percent75 = System.Drawing.Drawing2D.HatchStyle.Percent75,

        ///<summary>指定 80% 阴影。前景色与背景色的比例为 80:100。</summary>
        Percent80 = System.Drawing.Drawing2D.HatchStyle.Percent80,

        ///<summary>指定 90% 阴影。前景色与背景色的比例为 90:100。</summary>
        Percent90 = System.Drawing.Drawing2D.HatchStyle.Percent90,

        LightDownwardDiagonal = System.Drawing.Drawing2D.HatchStyle.LightDownwardDiagonal,

        ///<summary>指定从顶点到底点向左倾斜的对角线，其两边夹角比 System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal
        ///小 50%，但这些直线不是锯齿消除的。</summary>
        LightUpwardDiagonal = System.Drawing.Drawing2D.HatchStyle.LightUpwardDiagonal,

        ///<summary>指定从顶点到底点向右倾斜的对角线，其两边夹角比 System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal
        ///小 50%，宽度是其两倍。此阴影图案不是锯齿消除的。</summary>
        DarkDownwardDiagonal = System.Drawing.Drawing2D.HatchStyle.DarkDownwardDiagonal,

        ///<summary>指定从顶点到底点向左倾斜的对角线，其两边夹角比 System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal
        ///小 50%，宽度是其两倍，但这些直线不是锯齿消除的。</summary>
        DarkUpwardDiagonal = System.Drawing.Drawing2D.HatchStyle.DarkUpwardDiagonal,

        ///<summary>指定从顶点到底点向右倾斜的对角线，其间距与阴影样式 System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal
        ///相同，宽度是其三倍，但它们不是锯齿消除的。</summary>
        WideDownwardDiagonal = System.Drawing.Drawing2D.HatchStyle.WideDownwardDiagonal,

        ///<summary>指定从顶点到底点向左倾斜的对角线，其间距与阴影样式 System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal
        ///相同，宽度是其三倍，但它们不是锯齿消除的。</summary>
        WideUpwardDiagonal = System.Drawing.Drawing2D.HatchStyle.WideUpwardDiagonal,

        ///<summary>指定垂直线的两边夹角比 System.Drawing.Drawing2D.HatchStyle.Vertical 小 50%。</summary>
        LightVertical = System.Drawing.Drawing2D.HatchStyle.LightVertical,

        ///<summary>指定水平线，其两边夹角比 System.Drawing.Drawing2D.HatchStyle.Horizontal 小 50%。</summary>
        LightHorizontal = System.Drawing.Drawing2D.HatchStyle.LightHorizontal,

        ///<summary>指定垂直线的两边夹角比阴影样式 System.Drawing.Drawing2D.HatchStyle.Vertical 小 75%（或者比 System.Drawing.Drawing2D.HatchStyle.LightVertical
        ///小 25%）。</summary>
        NarrowVertical = System.Drawing.Drawing2D.HatchStyle.NarrowVertical,

        ///<summary>指定水平线的两边夹角比阴影样式 System.Drawing.Drawing2D.HatchStyle.Horizontal 小 75%（或者比
        ///System.Drawing.Drawing2D.HatchStyle.LightHorizontal 小 25%）。</summary>
        NarrowHorizontal = System.Drawing.Drawing2D.HatchStyle.NarrowHorizontal,

        ///<summary>指定垂直线的两边夹角比 System.Drawing.Drawing2D.HatchStyle.Vertical 小 50% 并且宽度是其两倍。</summary>
        DarkVertical = System.Drawing.Drawing2D.HatchStyle.DarkVertical,

        ///<summary>指定水平线的两边夹角比 System.Drawing.Drawing2D.HatchStyle.Horizontal 小 50% 并且宽度是 System.Drawing.Drawing2D.HatchStyle.Horizontal
        ///的两倍。</summary>
        DarkHorizontal = System.Drawing.Drawing2D.HatchStyle.DarkHorizontal,

        ///<summary>指定虚线对角线，这些对角线从顶点到底点向右倾斜。</summary>
        DashedDownwardDiagonal = System.Drawing.Drawing2D.HatchStyle.DashedDownwardDiagonal,

        ///<summary>指定虚线对角线，这些对角线从顶点到底点向左倾斜。</summary>
        DashedUpwardDiagonal = System.Drawing.Drawing2D.HatchStyle.DashedUpwardDiagonal,

        ///<summary>指定虚线水平线。</summary>
        DashedHorizontal = System.Drawing.Drawing2D.HatchStyle.DashedHorizontal,

        ///<summary>指定虚线垂直线。</summary>
        DashedVertical = System.Drawing.Drawing2D.HatchStyle.DashedVertical,

        ///<summary>指定带有五彩纸屑外观的阴影。</summary>
        SmallConfetti = System.Drawing.Drawing2D.HatchStyle.SmallConfetti,

        ///<summary>指定具有五彩纸屑外观的阴影，并且它是由比 System.Drawing.Drawing2D.HatchStyle.SmallConfetti 更大的片构成的。</summary>
        LargeConfetti = System.Drawing.Drawing2D.HatchStyle.LargeConfetti,

        ///<summary>指定由 Z 字形构成的水平线。</summary>
        ZigZag = System.Drawing.Drawing2D.HatchStyle.ZigZag,

        ///<summary>指定由代字号“~”构成的水平线。</summary>
        Wave = System.Drawing.Drawing2D.HatchStyle.Wave,

        ///<summary>指定具有分层砖块外观的阴影，它从顶点到底点向左倾斜。</summary>
        DiagonalBrick = System.Drawing.Drawing2D.HatchStyle.DiagonalBrick,

        ///<summary>指定具有水平分层砖块外观的阴影。</summary>
        HorizontalBrick = System.Drawing.Drawing2D.HatchStyle.HorizontalBrick,

        ///<summary>指定具有织物外观的阴影。</summary>
        Weave = System.Drawing.Drawing2D.HatchStyle.Weave,

        ///<summary>指定具有格子花呢材料外观的阴影。</summary>
        Plaid = System.Drawing.Drawing2D.HatchStyle.Plaid,

        ///<summary>指定具有草皮层外观的阴影。</summary>
        Divot = System.Drawing.Drawing2D.HatchStyle.Divot,

        ///<summary>指定互相交叉的水平线和垂直线，每一直线都是由点构成的。</summary>
        DottedGrid = System.Drawing.Drawing2D.HatchStyle.DottedGrid,

        ///<summary>指定互相交叉的正向对角线和反向对角线，每一对角线都是由点构成的。</summary>
        DottedDiamond = System.Drawing.Drawing2D.HatchStyle.DottedDiamond,

        ///<summary>指定带有对角分层鹅卵石外观的阴影，它从顶点到底点向右倾斜。</summary>
        Shingle = System.Drawing.Drawing2D.HatchStyle.Shingle,

        ///<summary>指定具有格架外观的阴影。</summary>
        Trellis = System.Drawing.Drawing2D.HatchStyle.Trellis,

        ///<summary>指定具有球体彼此相邻放置的外观的阴影。</summary>
        Sphere = System.Drawing.Drawing2D.HatchStyle.Sphere,

        ///<summary>指定互相交叉的水平线和垂直线，其两边夹角比阴影样式 System.Drawing.Drawing2D.HatchStyle.Cross 小 50%。</summary>
        SmallGrid = System.Drawing.Drawing2D.HatchStyle.SmallGrid,

        ///<summary>指定带有棋盘外观的阴影。</summary>
        SmallCheckerBoard = System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard,

        ///<summary>指定具有棋盘外观的阴影，棋盘所具有的方格大小是 System.Drawing.Drawing2D.HatchStyle.SmallCheckerBoard
        ///大小的两倍。</summary>
        LargeCheckerBoard = System.Drawing.Drawing2D.HatchStyle.LargeCheckerBoard,

        ///<summary>指定互相交叉的正向对角线和反向对角线，但这些对角线不是锯齿消除的。</summary>
        OutlinedDiamond = System.Drawing.Drawing2D.HatchStyle.OutlinedDiamond,

        ///<summary>指定具有对角放置的棋盘外观的阴影。</summary>
        SolidDiamond = System.Drawing.Drawing2D.HatchStyle.SolidDiamond,
        /// <summary>
        /// 指定从右上到左下的渐变。  
        /// </summary>
        GradientBackwardDiagonal = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal + 1000 ,
        /// <summary>
        /// 指定从左上到右下的渐变。  
        /// </summary>
        GradientForwardDiagonal = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal + 1000 ,
        /// <summary>
        /// 指定从左到右的渐变。  
        /// </summary>
        GradientHorizontal = System.Drawing.Drawing2D.LinearGradientMode.Horizontal+1000,
        /// <summary>
        /// 指定从上到下的渐变。 
        /// </summary>
        GradientVertical = System.Drawing.Drawing2D.LinearGradientMode.Vertical+1000
    }

    /// <summary>
    /// 画刷样式对象，本对象可以参与XML序列化和反序列化。
    /// </summary>
    //[DCSoft.Common.DocumentComment()]
    [Serializable()]
    [System.ComponentModel.TypeConverter(typeof(XBrushStyleTypeConverter))]
    [System.ComponentModel.Editor(
        "DCSoft.WinForms.Design.XBrushStyleEditor",
        typeof(System.Drawing.Design.UITypeEditor))]
    public class XBrushStyle : ICloneable, IDisposable
    {
        public const int GradientBase = 1000 ;

        /// <summary>
        /// 初始化对象
        /// </summary>
        public XBrushStyle()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">背景颜色</param>
        public XBrushStyle(Color color)
        {
            intColor = color;
        }

        private Color intColor = Color.Transparent;
        /// <summary>
        /// 背景色
        /// </summary>
        [System.ComponentModel.DefaultValue(typeof(Color), "Transparent")]
        [System.Xml.Serialization.XmlIgnore()]
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

        /// <summary>
        /// 画刷颜色文本值
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement("Color")]
        [DefaultValue( "Transparent")]
        public string ColorString
        {
            get
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertToString(intColor);
            }
            set
            {
                intColor = (System.Drawing.Color)System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertFromString(value);
            }
        }

        private Color intColor2 = Color.Black;
        /// <summary>
        /// 第二背景色，用于充当网格背景色或渐变色
        /// </summary>
        [System.ComponentModel.DefaultValue(typeof(Color), "Black")]
        [System.Xml.Serialization.XmlIgnore()]
        public Color Color2
        {
            get 
            {
                return intColor2; 
            }
            set
            {
                intColor2 = value; 
            }
        }

        /// <summary>
        /// 画笔颜色文本值
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlElement("Color2")]
        [DefaultValue("Black")]
        public string Color2String
        {
            get
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertToString(intColor2);
            }
            set
            {
                intColor2 = (System.Drawing.Color)System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertFromString(value);
            }
        }

        private XBrushStyleConst intStyle = XBrushStyleConst.Solid;
        /// <summary>
        /// 背景图案样式
        /// </summary>
        [System.ComponentModel.DefaultValue(XBrushStyleConst.Solid )]
        public XBrushStyleConst Style
        {
            get
            {
                return intStyle;
            }
            set
            {
                intStyle = value;
            }
        }

        private XImageValue myImage = null;
        /// <summary>
        /// 背景图片
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public XImageValue Image
        {
            get
            {
                return myImage;
            }
            set
            {
                myImage = value;
            }
        }

        private bool bolRepeat = false;
        /// <summary>
        /// 是否重复绘制背景图片
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
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

        private float fOffsetX = 0f;
        /// <summary>
        /// 背景图片横向偏移量
        /// </summary>
        [System.ComponentModel.DefaultValue( 0f)]
        public float OffsetX
        {
            get
            {
                return fOffsetX;
            }
            set
            {
                fOffsetX = value;
            }
        }

        private float fOffsetY = 0f;
        /// <summary>
        /// 背景图片纵向偏移量
        /// </summary>
        [System.ComponentModel.DefaultValue(0f)]
        public float OffsetY
        {
            get
            {
                return fOffsetY;
            }
            set
            {
                fOffsetY = value;
            }
        }

        


        /// <summary>
        /// 对象是否有内容可以创建画刷对象
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool HasContent
        {
            get
            {
                return intStyle != XBrushStyleConst.Disabled;
            }
        }


        /// <summary>
        /// 是否是纯色画刷
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsSolid
        {
            get
            {
                if (intStyle == XBrushStyleConst.Disabled)
                    return false;
                if (myImage != null && myImage.Value != null)
                    return false;
                return intStyle == XBrushStyleConst.Solid ;
            }
        }

        /// <summary>
        /// 创建填充背景使用的画刷对象
        /// </summary>
        /// <returns>创建的画刷对象</returns>
        public Brush CreateBrush()
        {
            return CreateBrush(0, 0, 100, 100, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 创建填充背景的画刷对象
        /// </summary>
        /// <param name="rect">要填充的矩形区域</param>
        /// <param name="unit">图形单位</param>
        /// <returns>创建的画刷对象</returns>
        public Brush CreateBrush(Rectangle rect, GraphicsUnit unit)
        {
            return CreateBrush(rect.Left, rect.Top, rect.Width, rect.Height, unit);
        }

        /// <summary>
        /// 创建填充背景的画刷对象
        /// </summary>
        /// <param name="rect">要填充的矩形区域</param>
        /// <returns>创建的画刷对象</returns>
        public Brush CreateBrush(Rectangle rect )
        {
            return CreateBrush(rect.Left, rect.Top, rect.Width, rect.Height, GraphicsUnit.Pixel );
        }

        /// <summary>
        /// 创建填充背景的画刷对象
        /// </summary>
        /// <param name="rect">要填充的矩形区域</param>
        /// <returns>创建的画刷对象</returns>
        public Brush CreateBrush(RectangleF rect)
        {
            return CreateBrush(rect.Left, rect.Top, rect.Width, rect.Height, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 创建填充背景的画刷对象
        /// </summary>
        /// <param name="rect">要填充的矩形区域</param>
        /// <param name="unit">图形单位</param>
        /// <returns>创建的画刷对象</returns>
        public Brush CreateBrush(RectangleF rect, GraphicsUnit unit)
        {
            return CreateBrush(rect.Left, rect.Top, rect.Width, rect.Height, unit);
        }

        /// <summary>
        /// 创建填充背景使用的画刷对象
        /// </summary>
        /// <param name="left">要绘制背景区域的左端坐标</param>
        /// <param name="top">要绘制背景区域的顶端坐标</param>
        /// <param name="width">要绘制背景区域的宽度</param>
        /// <param name="height">要绘制背景区域的高度</param>
        /// <param name="unit">绘制图形使用的单位</param>
        /// <returns>创建的画刷对象</returns>
        /// <remarks>
        /// 若设置了背景图片则创建图片样式的画刷对象，若设置了图案则创建带图案的画刷对象，
        /// 若设置了渐变设置则创建带渐变的画刷对象，否则创建纯色画刷对象。
        /// </remarks>
        public Brush CreateBrush(
            float left,
            float top,
            float width,
            float height,
            System.Drawing.GraphicsUnit unit)
        {
            if (intStyle == XBrushStyleConst.Disabled)
                return null;

            if (myImage != null && myImage.Value != null)
            {
                System.Drawing.TextureBrush brush = new TextureBrush(myImage.Value);
                if (bolRepeat)
                    brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                else
                    brush.WrapMode = System.Drawing.Drawing2D.WrapMode.Clamp;
                float rate = (float)GraphicsUnitConvert.GetRate(
                    unit,
                    System.Drawing.GraphicsUnit.Pixel);
                //brush.Transform.Translate(fOffsetX, fOffsetY);
                brush.TranslateTransform(fOffsetX, fOffsetY);
                brush.ScaleTransform(rate, rate);
                return brush;
            }
            if (intStyle == XBrushStyleConst.Solid)
            {
                return new SolidBrush(intColor);
            }
            else
            {
                if ( ( int ) intStyle < 1000)
                {
                    System.Drawing.Drawing2D.HatchStyle style = (System.Drawing.Drawing2D.HatchStyle)intStyle;
                    return new System.Drawing.Drawing2D.HatchBrush(
                        (System.Drawing.Drawing2D.HatchStyle)intStyle,
                        intColor,
                        intColor2 );
                }
                else
                {
                    return new System.Drawing.Drawing2D.LinearGradientBrush(
                        new RectangleF(left, top, width, height),
                        intColor,
                        intColor2,
                        (System.Drawing.Drawing2D.LinearGradientMode) ( intStyle - 1000 ));
                }
            }
            //return new SolidBrush(intColor);
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (intStyle == XBrushStyleConst.Disabled)
                return "Disabled";

            if (myImage != null && myImage.Value != null)
            {
                string str = "";
                if (bolRepeat)
                    str = " Repeat ";
                str = str + myImage.ToString();
                return str;
            }
            else if (intStyle == XBrushStyleConst.Solid)
            {
                return ColorTranslator.ToHtml(intColor);
            }
            else
            {
                return intStyle.ToString()
                    + " " + System.Drawing.ColorTranslator.ToHtml(intColor)
                    + "->" + System.Drawing.ColorTranslator.ToHtml(intColor2 );
            }
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XBrushStyle style = new XBrushStyle();
            style.bolRepeat = this.bolRepeat;
            style.intColor = this.intColor;
            style.intColor2 = this.intColor2;
            style.intStyle = this.intStyle;
            style.fOffsetX = this.fOffsetX;
            style.fOffsetY = this.fOffsetY;
            if (myImage != null)
                style.myImage = myImage.Clone();
            return style;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XBrushStyle Clone()
        {
            return (XBrushStyle)((System.ICloneable)this).Clone();
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            if (myImage != null)
            {
                myImage.Dispose();
                myImage = null;
            }
        }
    }

    public class XBrushStyleTypeConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext context,
            object Value,
            Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XBrushStyle), attributes).Sort( new string[]{"Style" , "Color","Color2","Image" , "Repeat" });
        }
    }
}
