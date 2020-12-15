/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;


namespace DCSoft.Data
{
    /// <summary>
    /// 数据格式化对象
    /// </summary>
    [Serializable()]
    [System.ComponentModel.TypeConverter(typeof(ValueFormaterTypeConverter))]
    [System.ComponentModel.Editor( 
        typeof(DCSoft.Data.WinForms.Design.ValueFormaterUIEditor) ,
        typeof( System.Drawing.Design.UITypeEditor ))]
    public class ValueFormater : System.ICloneable
    {
        public static ValueFormater ParseOldFormat(string format)
        {
            ValueFormater formater = new ValueFormater();
            if (format == null)
                return new ValueFormater();
            int index = format.IndexOf(":");
            if (index > 0)
            {
                string strStyle = format.Substring(0, index).Trim();
                try 
                {
                    formater.Style = ( ValueFormatStyle ) Enum.Parse( typeof( ValueFormatStyle ) , strStyle );
                }
                finally{}
                if (formater.Style != ValueFormatStyle.None)
                {
                    formater.Format = format.Substring(index + 1).Trim();
                    if (formater.Format == "chinese")
                    {
                        formater.Format = "BigChinese";
                    }
                }
            }
            else
            {
                formater.Style = (ValueFormatStyle)Enum.Parse(typeof(ValueFormatStyle), format);
            }
            return formater;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValueFormater()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="style">类型</param>
        /// <param name="format">格式</param>
        public ValueFormater(ValueFormatStyle style, string format)
        {
            intStyle = style;
            strFormat = format;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="style">类型</param>
        /// <param name="format">格式</param>
        /// <param name="naNText">数据空时的文本</param>
        public ValueFormater(ValueFormatStyle style, string format, string naNText)
        {
            intStyle = style;
            strFormat = format;
            _NoneText = NoneText;
        }

        private ValueFormatStyle intStyle = ValueFormatStyle.None;
        /// <summary>
        /// 数据源格式化样式
        /// </summary>
        [System.ComponentModel.DefaultValue(ValueFormatStyle.None)]
        public ValueFormatStyle Style
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

        private string strFormat = null;
        /// <summary>
        /// 格式化字符串
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Format
        {
            get
            {
                return strFormat;
            }
            set
            {
                strFormat = value;
            }
        }

        private string _NoneText = null;
        /// <summary>
        /// 对于非数字显示的文本
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string NoneText
        {
            get
            {
                return _NoneText;
            }
            set
            {
                _NoneText = value;
            }
        }

        /// <summary>
        /// 对象没有任何有效设置
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (this.intStyle != ValueFormatStyle.None)
                {
                    return false;
                }
                if (this.strFormat != null && strFormat.Length > 0)
                {
                    return false;
                }
                if (this._NoneText != null && this._NoneText.Length > 0)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// 解释字符串设置对象数据
        /// </summary>
        /// <param name="strText">字符串</param>
        public void Parse(string strText)
        {
            intStyle = ValueFormatStyle.None;
            strFormat = null;
            _NoneText = null;

            if ( HasContent( strText ))
            {
                string[] items = strText.Split(',');
                if (items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        string txt = items[0].Trim();
                        int index = txt.IndexOf("=");
                        if (index >= 0)
                        {
                            string name = txt.Substring(0, index).Trim();
                            txt = txt.Substring(index + 1).Trim();
                            if (string.Compare(name, "format", true) == 0 )
                            {
                                strFormat = txt;
                            }
                            else if (string.Compare(name, "nonetext", true) == 0 )
                            {
                                _NoneText = txt;
                            }
                        }
                        else
                        {
                            bool find = false;
                            foreach (string name in Enum.GetNames(typeof(ValueFormatStyle)))
                            {
                                if (string.Compare(name, txt, true) == 0)
                                {
                                    intStyle = (ValueFormatStyle)Enum.Parse(typeof(ValueFormatStyle), name);
                                    find = true;
                                    break;
                                }
                            }
                            if (find == false)
                            {
                                strFormat = txt; 
                            }
                        }
                    }
                }//if
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            ValueFormater formater = (ValueFormater)this.MemberwiseClone();
            return formater;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ValueFormater Clone()
        {
            return (ValueFormater)((System.ICloneable)this).Clone();
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(intStyle.ToString());
            if (intStyle != ValueFormatStyle.None)
            {
                if (HasContent(strFormat))
                {
                    str.Append("," + strFormat);
                }
                //if (HasContent(strParameter1) || HasContent(strParameter2))
                //{
                //    str.Append("(" + strParameter1 + "," + strParameter2 + ")");
                //}
                
                if (HasContent(_NoneText))
                {
                    str.Append(",NoneText=" + _NoneText);
                }
            }
            return str.ToString();
        }

        internal static bool HasContent(string txt)
        {
            return txt != null && txt.Trim().Length > 0;
        }

        /// <summary>
        /// 执行数据格式转换,生成转换后的文本
        /// </summary>
        /// <param name="Value">原始数据</param>
        /// <returns>格式化后生成的文本</returns>
        public string Execute(object Value)
        {
            if (intStyle == ValueFormatStyle.None)
            {
                if (Value == null || DBNull.Value.Equals(Value))
                {
                    return null;
                }
                else
                {
                    return Convert.ToString( Value );
                }
            }
            else
            {
                return Execute(
                    this.Style,
                    this.Format,
                    this.NoneText,
                    Value);
            }
        }

        private static bool IsNumberTypeFast(object value)
        {
            return (value is byte
                || value is sbyte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal);
        }

        public static string Execute(
            ValueFormatStyle style,
            string format,
            string noneText,
            object Value)
        {
            if (Value == null || DBNull.Value.Equals(Value))
            {
                // 数据为空
                return noneText;
            }
            if (format == null)
            {
                format = "";
            }
            switch (style)
            {
                case ValueFormatStyle.None:
                    {
                        // 无任何格式转换
                        return Convert.ToString(Value);
                    }
                case ValueFormatStyle.Currency:
                    {
                        // 货币类型
                        decimal dec = 0;
                        bool convertFlag = false;
                        if (Value is string)
                        {
                            string txt = (string)Value;
                            if (HasContent( txt ))
                            {
                                try
                                {
                                    convertFlag = decimal.TryParse(txt, out dec);
                                }
                                catch
                                {
                                }
                            }
                        }
                        else if (IsNumberTypeFast(Value) || Value is bool)
                        {
                            dec = Convert.ToDecimal(Value);
                            convertFlag = true;
                        }
                        if (convertFlag)
                        {
                            if (HasContent(format))
                            {
                                return dec.ToString(format);
                            }
                            else
                            {
                                return dec.ToString();
                            }
                        }
                        else
                        {
                            return noneText;
                        }
                        //break;
                    }
                case ValueFormatStyle.DateTime:
                    {
                        DateTime dtm = DateTime.MinValue;
                        bool convertFlag = false;
                        if (Value is DateTime)
                        {
                            dtm = (DateTime)Value;
                            convertFlag = true;
                        }
                        else if (Value is string )
                        {
                            string txt = (string)Value;
                            if( HasContent( txt ))
                            {
                                convertFlag = DateTime.TryParse(txt, out dtm);
                            }
                        }
                        else if (IsNumberTypeFast(Value))
                        {
                            dtm = new DateTime(Convert.ToInt64(Value));
                            convertFlag = true;
                        }
                        else
                        {
                            dtm = Convert.ToDateTime(Value);
                            convertFlag = true;
                        }
                        if ( convertFlag )
                        {
                            if (HasContent(format))
                            {
                                return dtm.ToString( format );
                            }
                            else
                            {
                                return dtm.ToLongDateString();
                            }
                        }
                        else
                        {
                            return noneText ;
                        }
                    }
                case ValueFormatStyle.Numeric:
                    {
                        double dbl = 0;
                        bool flag = false;
                        if (Value is string)
                        {
                            string txt = (string)Value;
                            if (HasContent(txt))
                            {
                                flag = double.TryParse(txt, out dbl);
                            }
                        }
                        else if (IsNumberTypeFast(Value))
                        {
                            dbl = Convert.ToDouble(Value);
                            flag = true;
                        }
                        else
                        {
                            try
                            {
                                dbl = Convert.ToDouble(Value);
                                flag = true;
                            }
                            catch
                            {
                            }
                        }
                        if (flag && double.IsNaN(dbl) == false)
                        {
                            if (HasContent(format))
                            {
                                return dbl.ToString(format);
                            }
                            else
                            {
                                return dbl.ToString();
                            }
                        }
                        else
                        {
                            return noneText;
                        }
                        //break;
                    }
                case ValueFormatStyle.Percent:
                    {
                        double dbl = 0;
                        int dig = 0;
                        int rate = 100;
                        if (Value is string)
                        {
                            string txt = (string)Value;
                            if (HasContent(txt))
                            {
                                if (txt.IndexOf("%") > 0)
                                {
                                    rate = 1;
                                    txt = txt.Replace("%", "");
                                }
                                if (double.TryParse(txt, out dbl) == false)
                                {
                                    dbl = double.NaN;
                                }
                            }
                            else
                            {
                                return noneText;
                            }
                        }
                        else if (IsNumberTypeFast(Value))
                        {
                            dbl = Convert.ToDouble(Value);
                        }
                        else
                        {
                            try
                            {
                                dbl = Convert.ToDouble(Value);
                            }
                            catch
                            {
                                return noneText;
                            }
                        }
                        if (Int32.TryParse(format, out dig) == false)
                        {
                            dig = 0;
                        }
                        if (dig < 0)
                        {
                            dig = 0;
                        }
                        if (double.IsNaN(dbl) == false)
                        {
                            dbl = Math.Round(dbl * rate, dig);
                            if (dig == 0)
                            {
                                return dbl.ToString() + "%";
                            }
                            else
                            {
                                return dbl.ToString("0." + new string('0', dig)) + "%";
                            }
                        }
                        else
                        {
                            return noneText;
                        }
                    }
                case ValueFormatStyle.SpecifyLength:
                    {
                        int specifyLength = 0;
                        string txt = Convert.ToString(Value);
                        if (Int32.TryParse(format, out specifyLength))
                        {
                            if (specifyLength > 0)
                            {
                                int len = 0;
                                foreach (char c in txt)
                                {
                                    if (c > 255)
                                        len += 2;
                                    else
                                        len++;
                                }
                                if (len < specifyLength)
                                {
                                    return Value + new string(' ', specifyLength - len);
                                }
                            }
                        }
                        return txt ;
                    }
                case ValueFormatStyle.String:
                    {
                        string txt = Convert.ToString(Value);
                        if (HasContent(txt) == false )
                        {
                            return txt;
                        }
                        if ( HasContent( format ) == false )
                        {
                            return txt;
                        }
                        format = format.Trim();
                        if (string.Compare(format, "trim", true) == 0)
                        {
                            return txt.Trim();
                        }
                        //else if (string.Compare(format, "normalizespace", true) == 0)
                        //{
                        //    return string.StringFormatHelper.NormalizeSpace(Value);
                        //}
                        //else if (string.Compare(format, "htmltext", true) == 0)
                        //{
                        //    HTMLDocument doc = new HTMLDocument();
                        //    doc.LoadHTML(Value);
                        //    string txt = doc.InnerText;
                        //    if (txt != null)
                        //        txt = txt.Trim();
                        //    return txt;
                        //}
                        else if (string.Compare(format, "HtmlEncode", true) == 0)
                        {
                            return System.Web.HttpUtility.HtmlEncode(txt);
                        }
                        else if (string.Compare(format, "HtmlDecode", true) == 0)
                        {
                            return System.Web.HttpUtility.HtmlDecode(txt);
                        }
                        else if (string.Compare(format, "UrlEncode", true) == 0)
                        {
                            return System.Web.HttpUtility.UrlEncode(txt);
                        }
                        else if (string.Compare(format, "UrlDecode", true) == 0)
                        {
                            return System.Web.HttpUtility.UrlDecode(txt);
                        }
                        else if (string.Compare(format, "HtmlAttributeEncode", true) == 0)
                        {
                            return System.Web.HttpUtility.HtmlAttributeEncode(txt);
                        }
                        else if (string.Compare(format, "lower", true) == 0)
                        {
                            return txt.ToLower();
                        }
                        else if (string.Compare(format, "upper", true) == 0)
                        {
                            return txt.ToUpper();
                        }

                        format = format.ToLower();
                        if (format.StartsWith("left"))
                        {
                            int index = format.IndexOf(",");
                            if (index > 0)
                            {
                                string left = format.Substring(index + 1);
                                int len = 0;
                                if (Int32.TryParse(left, out len))
                                {
                                    if (len > 0 && txt.Length > len)
                                    {
                                        return txt.Substring(0, len);
                                    }
                                }
                            }
                            return txt;
                        }
                        if (format.StartsWith("right"))
                        {
                            int index = format.IndexOf(",");
                            if (index > 0)
                            {
                                string right = format.Substring(index + 1);
                                int len = 0;
                                if (Int32.TryParse(right, out len))
                                {
                                    if (len > 0 && txt.Length > len)
                                    {
                                        return txt.Substring(txt.Length - len - 1, len);
                                    }
                                }
                            }
                            return txt;
                        }
                        //					else if( string.Compare( Format , "groupsplit" , true ) == 0 )
                        //					{
                        //						int GroupSize = Convert.ToInt32( 
                        //						Value = Value.Trim();
                        //
                        //					}

                        return txt;
                    }
                case ValueFormatStyle.Boolean:
                    {
                        if (format == null)
                        {
                            return Convert.ToString( Value );
                        }
                        int index = format.IndexOf(",");
                        string trueStr = format;
                        string falseStr = "";
                        if (index >= 0)
                        {
                            trueStr = format.Substring(0, index);
                            falseStr = format.Substring(index + 1);
                        }
                        bool bol = false;
                        bool flag = false;
                        if (Value is bool)
                        {
                            bol = (bool)Value;
                            flag = true;
                        }
                        else if (Value is string)
                        {
                            flag = Boolean.TryParse((string)Value, out bol);
                        }
                        else
                        {
                            try
                            {
                                bol = Convert.ToBoolean(Value);
                                flag = true;
                            }
                            catch
                            {
                                return noneText;
                            }
                        }
                        if (bol)
                        {
                            return trueStr;
                        }
                        else
                        {
                            return falseStr;
                        }
                        //return Value ;
                    }
            }
            return Convert.ToString( Value );
        }
    }

    /// <summary>
    /// 数据源格式类型
    /// </summary>
    public enum ValueFormatStyle
    {
        /// <summary>
        /// 无样式
        /// </summary>
        None,
        /// <summary>
        /// 数字
        /// </summary>
        Numeric,
        /// <summary>
        /// 货币
        /// </summary>
        Currency,
        /// <summary>
        /// 时间日期
        /// </summary>
        DateTime,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 固定文本长度
        /// </summary>
        SpecifyLength,
        /// <summary>
        /// 布尔类型
        /// </summary>
        Boolean,
        /// <summary>
        /// 百分比
        /// </summary>
        Percent,
    }

    public class ValueFormaterTypeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals(typeof(string)))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value)
        {
            if (Value is string)
            {
                ValueFormater format = new ValueFormater();
                format.Parse((string)Value);
                return format;
            }
            return base.ConvertFrom(context, culture, Value);
        }
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(
            ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture,
            object Value,
            Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                ValueFormater format = (ValueFormater)Value;
                System.Reflection.ConstructorInfo ctor = null;
                if (format.Style == ValueFormatStyle.None && format.Format == null && format.NoneText == null)
                {
                    ctor = typeof(ValueFormater).GetConstructor(new Type[] { });
                    return new InstanceDescriptor(ctor, new object[] { } , true);
                }
                else
                {
                    ctor = typeof(ValueFormater).GetConstructor(
                        new Type[] { typeof(ValueFormatStyle), typeof(string), typeof(string) });
                    return new InstanceDescriptor(ctor, new object[] { format.Style, format.Format, format.NoneText } , true);
                }
                //InstanceDescriptor desc = new InstanceDescriptor(
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }

        public override System.ComponentModel.PropertyDescriptorCollection GetProperties(
            System.ComponentModel.ITypeDescriptorContext context,
            object Value,
            Attribute[] attributes)
        {
            return System.ComponentModel.TypeDescriptor.GetProperties(
                typeof(ValueFormater), attributes).Sort(new string[] {
                    "Style", 
                    "Format" , 
                    "NoneText" });
        }
        public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

         
    }
}