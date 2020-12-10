/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Collections ;
using System.Collections.Generic ;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 画笔样式信息对象,本对象可以参与XML序列化和反序列化
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(XPenStyleTypeConverter))]
    [System.ComponentModel.Editor( typeof( XPenStyleTypeEditor ) , typeof( UITypeEditor ))]
    [Serializable()]
    [System.ComponentModel.ToolboxItem(false)]
    public class XPenStyle : System.ICloneable , IComponent
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XPenStyle()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        public XPenStyle(Color color)
        {
            intColor = color;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="width">线条宽度</param>
        public XPenStyle(Color color, float width)
        {
            this.intColor = color;
            this.fWidth = width;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="width">线条宽度</param>
        /// <param name="dashStyle">虚线样式</param>
        public XPenStyle(Color color, float width, DashStyle dashStyle)
        {
            intColor = color;
            fWidth = width;
            intDashStyle = DashStyle;
        }

        private System.Drawing.Color intColor = System.Drawing.Color.Black;
        /// <summary>
        /// 画笔颜色
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue( typeof( System.Drawing.Color ) , "Black" )]
        [System.Xml.Serialization.XmlIgnore()]
        public System.Drawing.Color Color
        {
            get 
            {
                return intColor; 
            }
            set
            {
                intColor = value;
                _Value = null;
            }
        }

        /// <summary>
        /// 画笔颜色文本值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement("Color")]
        [DefaultValue("Black")]
        public string ColorString
        {
            get
            {
                return System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertToString(intColor);
            }
            set
            {
                intColor = ( System.Drawing.Color ) System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Color)).ConvertFromString(value);
                _Value = null;
            }
        }

        private float fWidth = 1f;
        /// <summary>
        /// 线条宽度
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(1f)]
        public float Width
        {
            get 
            {
                return fWidth; 
            }
            set
            {
                fWidth = value;
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.DashStyle intDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
        /// <summary>
        /// 线条虚线样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.DashStyle.Solid)]
        [System.ComponentModel.Editor( "DCSoft.Editor.DashStyleEditor"  , typeof( System.Drawing.Design.UITypeEditor ))]
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get 
            {
                return intDashStyle; 
            }
            set 
            {
                if (value != System.Drawing.Drawing2D.DashStyle.Custom)
                {
                    intDashStyle = value;
                    _Value = null;
                }
            }
        }

        private System.Drawing.Drawing2D.DashCap intDashCap = System.Drawing.Drawing2D.DashCap.Flat;
        /// <summary>
        /// 虚线帽样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.DashCap.Flat )]
        [System.ComponentModel.Editor( "DCSoft.Editor.DashCapEditor" , typeof( System.Drawing.Design.UITypeEditor ))]
        public System.Drawing.Drawing2D.DashCap DashCap
        {
            get
            {
                return intDashCap; 
            }
            set
            {
                intDashCap = value;
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.LineJoin intLineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
        /// <summary>
        /// 线段连接处样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineJoin.Bevel )]
        public System.Drawing.Drawing2D.LineJoin LineJoin
        {
            get 
            {
                return intLineJoin; 
            }
            set
            {
                intLineJoin = value;
                _Value = null;
            }
        }

        private System.Drawing.Drawing2D.LineCap intStartCap = System.Drawing.Drawing2D.LineCap.Flat;
        /// <summary>
        /// 线段起点箭头样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineCap.Flat)]
        [System.ComponentModel.Editor("DCSoft.Editor.LineCapEditor", typeof(System.Drawing.Design.UITypeEditor))]
        public System.Drawing.Drawing2D.LineCap StartCap
        {
            get
            {
                return intStartCap; 
            }
            set
            {
                if (value != System.Drawing.Drawing2D.LineCap.Custom)
                {
                    intStartCap = value;
                    _Value = null;
                }
            }
        }

        private System.Drawing.Drawing2D.LineCap intEndCap = System.Drawing.Drawing2D.LineCap.Flat;
        /// <summary>
        /// 线段终点箭头样式
        /// </summary>
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(System.Drawing.Drawing2D.LineCap.Flat)]
        [System.ComponentModel.Editor("DCSoft.Editor.LineCapEditor", typeof(System.Drawing.Design.UITypeEditor))]
        public System.Drawing.Drawing2D.LineCap EndCap
        {
            get 
            {
                return intEndCap; 
            }
            set
            {
                if (value != System.Drawing.Drawing2D.LineCap.Custom)
                {
                    intEndCap = value;
                    _Value = null;
                }
            }
        }

        private float _MiterLimit = 10f;
        [DefaultValue( 10f)]
        public float MiterLimit
        {
            get
            {
                return _MiterLimit; 
            }
            set
            {
                if (_MiterLimit != value)
                {
                    _MiterLimit = value;
                    _Value = null;
                }
            }
        }

        private PenAlignment _Alignment = PenAlignment.Center;
        [DefaultValue( PenAlignment.Center )]
        public PenAlignment Alignment
        {
            get 
            {
                return _Alignment; 
            }
            set
            {
                _Alignment = value; 
            }
        }
        private static Dictionary<string, Pen> _Buffer = new Dictionary<string, Pen>();

        private Pen _Value = null;
        /// <summary>
        /// 画笔对象
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public Pen Value
        {
            get
            {
                if (_Value == null)
                {
                    string key = this.Color.ToArgb().ToString()+ " "
                        + this.Width + " "
                        + this.DashCap + " "
                        + this.DashStyle + " "
                        + this.StartCap + " " 
                        + this.EndCap + " "
                        + this.MiterLimit + " " 
                        + this.Alignment ;
                    if (_Buffer.ContainsKey(key))
                    {
                        _Value = _Buffer[key];
                    }
                    else
                    {
                        if (_Buffer.Count > 100)
                        {
                            // 缓存的画笔太多，清空掉
                            foreach (Pen p2 in _Buffer.Values)
                            {
                                p2.Dispose();
                            }
                            _Buffer.Clear();
                        }
                        Pen p = this.CreatePen();
                        _Buffer[key] = p;
                        _Value = p;
                    }
                }
                return _Value; 
            }
        }

        //public void DrawPath(System.Drawing.Graphics g, System.Drawing.Drawing2D.GraphicsPath path)
        //{
        //    using (System.Drawing.Pen p = this.CreatePen())
        //    {
        //        g.DrawPath(p, path);
        //    }
        //}

        /// <summary>
        /// 根据设置创建画笔对象
        /// </summary>
        /// <returns>创建的画笔对象</returns>
        public System.Drawing.Pen CreatePen()
        {
            System.Drawing.Pen p = new System.Drawing.Pen(this.Color, this.Width);
            p.DashStyle = this.DashStyle;
            p.DashCap = this.DashCap;
            p.LineJoin = this.LineJoin;
            p.StartCap = this.StartCap;
            p.EndCap = this.EndCap;
            p.MiterLimit = this.MiterLimit;
            p.Alignment = this.Alignment;
            return p;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            XPenStyle style = (XPenStyle)this.MemberwiseClone();
            return style;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XPenStyle Clone()
        {
            return (XPenStyle)((System.ICloneable)this).Clone();
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            System.Drawing.ColorConverter cc = new System.Drawing.ColorConverter();
            System.Text.StringBuilder str = new StringBuilder();
            str.Append(intDashStyle.ToString());
            str.Append(" " + System.Drawing.ColorTranslator.ToHtml(intColor));
            str.Append(" " + fWidth);
            if (this.MiterLimit != 10f)
            {
                str.Append(" MiterLimit:" + this.MiterLimit);
            }
            if (this.Alignment != PenAlignment.Center)
            {
                str.Append(" " + this.Alignment);
            }
            return str.ToString();
        }

        #region IComponent 成员

        /// <summary>
        /// 对象销毁事件
        /// </summary>
        public event EventHandler Disposed = null;

        private ISite mySite = null;
        /// <summary>
        /// 对象站点
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get
            {
                return mySite;
            }
            set
            {
                mySite = value;
            }
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            if (Disposed != null)
            {
                Disposed(this, new EventArgs());
            }
        }

        #endregion
    }

    /// <summary>
    /// 用于XPenStyle类型的数据编辑器
    /// </summary>
    public class XPenStyleTypeEditor : UITypeEditor
    {
        /// <summary>
        /// 获得编辑样式
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>编辑样式</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.None;
        }

        /// <summary>
        /// 是否自定义绘制数据
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>支持自定义绘制数据</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        /// <summary>
        /// 自定义绘制数据
        /// </summary>
        /// <param name="e">事件参数</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            XPenStyle style = e.Value as XPenStyle;
            System.Drawing.Color c = System.Drawing.Color.Black;
            if (style != null)
            {
                c = style.Color;
            }
            using (System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(c))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
        }
    }
    /// <summary>
    /// 用于XPenStyle的类型转换器
    /// </summary>
    public class XPenStyleTypeConverter : TypeConverter
    {
        /// <summary>
        /// 获得对象属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="value">数值</param>
        /// <param name="attributes">特性</param>
        /// <returns>获得的属性列表</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(XPenStyle), attributes);
        }
        /// <summary>
        /// 是否支持获得对象属性
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns>支持获得对象数据</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
