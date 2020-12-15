/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DCSoft.Drawing
{
    /// <summary>
    /// 颜色值的封装
    /// </summary>
    /// <remarks>
    /// Color值不能参与XML序列化，本类型就是Color类型的一个封装，使得颜色值能进行XML序列化
    /// 编写 袁永福</remarks>
    [System.ComponentModel.TypeConverter(typeof(XColorValueTypeConverter))]
    [System.ComponentModel.Editor(typeof(XColorValueUIEditor), typeof(UITypeEditor))]
    public class XColorValue : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XColorValue()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="c">颜色值</param>
        public XColorValue(Color c)
        {
            _Value = c;
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="v">表示颜色的字符串</param>
        public XColorValue(string v)
        {
            this.StringValue = v;
        }

        private Color _Value = Color.Empty;
        /// <summary>
        /// 颜色值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Color Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private static TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
        /// <summary>
        /// 颜色值的字符串表达形式
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        //[System.Xml.Serialization.XmlElement("Value")]
        [System.Xml.Serialization.XmlText()]
        public string StringValue
        {
            get
            {
                return converter.ConvertToString(_Value);
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    _Value = Color.Empty;
                }
                else
                {
                    _Value = (Color)converter.ConvertFromString(value);
                }
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XColorValue Clone()
        {
            return (XColorValue)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XColorValue v = new XColorValue();
            v._Value = this._Value;
            return v;
        }
    }

    /// <summary>
    /// 颜色值类型转换器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XColorValueTypeConverter : TypeConverter
    {
        /// <summary>
        /// 判断能否进行指定的类型的数据转换为颜色值
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="sourceType">指定的类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals(typeof(string))
                || sourceType.Equals(typeof(Color))
                || sourceType.Equals(typeof(InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 将指定的数据转换为颜色值
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="value">要转换的数据</param>
        /// <returns>转换所得的颜色值</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null)
            {
                return new XColorValue();
            }
            else if (value is string)
            {
                return new XColorValue((string)value);
            }
            else if (value is Color)
            {
                return new XColorValue((Color)value);
            }
            else if (value is InstanceDescriptor)
            {
                // 转换为实例描述器中，一般用于对象的CodeDOM序列化
                InstanceDescriptor id = (InstanceDescriptor)value;
                if (id.Arguments != null && id.Arguments.Count > 0)
                {
                    System.Collections.IEnumerator enumer = id.Arguments.GetEnumerator();
                    enumer.Reset();
                    enumer.MoveNext();
                    object v = enumer.Current;
                    if (v is string)
                    {
                        return new XColorValue((string)v);
                    }
                    else if (v is Color)
                    {
                        return new XColorValue((Color)v);
                    }
                    else
                    {
                        return new XColorValue();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 判断颜色值能否转换为指定的类型
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="destinationType">指定的数据类型</param>
        /// <returns>能否转换</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(string))
                || destinationType.Equals(typeof(Color))
                || destinationType.Equals(typeof(InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 将颜色值转换为指定的类型的数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="culture">参数</param>
        /// <param name="value">颜色值</param>
        /// <param name="destinationType">指定的数据类型</param>
        /// <returns>转换结果</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            XColorValue xc = value as XColorValue;
            if (destinationType.Equals(typeof(string)))
            {
                return xc == null ? null : xc.StringValue;
            }
            else if (destinationType.Equals(typeof(Color)))
            {
                return xc == null ? Color.Empty : xc.Value;
            }
            else if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                return new InstanceDescriptor(
                    typeof(XColorValue).GetConstructor(new Type[] { typeof(string) }),
                    new object[] { xc.StringValue });
            }
            return null;
        }
    }

    /// <summary>
    /// 颜色值编辑器
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class XColorValueUIEditor : UITypeEditor
    {
        private static UITypeEditor editor = (UITypeEditor)TypeDescriptor.GetEditor(typeof(Color), typeof(UITypeEditor));
        /// <summary>
        /// 获得数据编辑样式
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>采用下拉类别的样式</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (editor == null)
                return UITypeEditorEditStyle.Modal;
            else
                return UITypeEditorEditStyle.DropDown;
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="provider">参数</param>
        /// <param name="value">旧数据</param>
        /// <returns>编辑后的新数据</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (editor != null)
            {
                XColorValue xc = value as XColorValue;
                Color oldColor = xc == null ? Color.Empty : xc.Value;
                Color newColor = (Color)editor.EditValue(context, provider, oldColor);
                if (oldColor.Equals(newColor) == false)
                {
                    return new XColorValue(newColor);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                using (ColorDialog dlg = new ColorDialog())
                {
                    XColorValue xc = value as XColorValue;
                    if (xc != null)
                    {
                        dlg.Color = xc.Value;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            return new XColorValue(dlg.Color);
                        }
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// 支持自定义绘制数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>支持自定义绘制</returns>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// 绘制数据
        /// </summary>
        /// <param name="e">参数</param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            XColorValue color = e.Value as XColorValue;
            if (color == null)
                color = new XColorValue();
            using (SolidBrush b = new SolidBrush(color.Value))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
        }
    }
}
