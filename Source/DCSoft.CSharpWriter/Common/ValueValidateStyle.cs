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
using System.ComponentModel;

namespace DCSoft.Common
{
    
    /// <summary>
    /// 文本验证样式对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    [TypeConverter( typeof( ValueValidateStyleTypeConverter ))]
    [Editor( "DCSoft.Common.ValueValidateStyleEditor" , typeof( System.Drawing.Design.UITypeEditor ))]
    public class ValueValidateStyle : System.ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ValueValidateStyle()
        {
        }

        private string strValueName = null;
        /// <summary>
        /// 数据的名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.ComponentModel.Category("Design")]
        public string ValueName
        {
            get
            {
                return strValueName; 
            }
            set
            {
                strValueName = value; 
            }
        }

        private object objValue = null;
        /// <summary>
        /// 数值
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public object Value
        {
            get
            {
                return objValue; 
            }
            set
            {
                objValue = value; 
            }
        }

        private bool bolRequired = false;
        /// <summary>
        /// 必填数据
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        [System.ComponentModel.Category("Format")]
        public bool Required
        {
            get
            {
                return bolRequired; 
            }
            set
            {
                bolRequired = value; 
            }
        }

        private ValueTypeStyle intValueType = ValueTypeStyle.Text;
        /// <summary>
        /// 数值类型
        /// </summary>
        [System.ComponentModel.DefaultValue( ValueTypeStyle.Text )]
        [System.ComponentModel.Category("Format")]
        public ValueTypeStyle ValueType
        {
            get
            {
                return intValueType; 
            }
            set
            {
                intValueType = value; 
            }
        }

        private int intMaxLength = 0;
        /// <summary>
        /// 最大文本长度
        /// </summary>
        [System.ComponentModel.DefaultValue( 0 )]
        [System.ComponentModel.Category("Format")]
        public int MaxLength
        {
            get 
            {
                return intMaxLength; 
            }
            set
            {
                intMaxLength = value; 
            }
        }

        private int intMinLength = 0;
        /// <summary>
        /// 最小文本长度
        /// </summary>
        [System.ComponentModel.DefaultValue( 0 )]
        [System.ComponentModel.Category("Format")]
        public int MinLength
        {
            get
            {
                return intMinLength; 
            }
            set
            {
                intMinLength = value; 
            }
        }

        private bool _CheckMaxValue = false;
        /// <summary>
        /// 检查数值或者日期值的最大值
        /// </summary>
        [DefaultValue( false )]
        [Category("Format")]
        public bool CheckMaxValue
        {
            get
            {
                return _CheckMaxValue; 
            }
            set
            {
                _CheckMaxValue = value; 
            }
        }

        private bool _CheckMinValue = false;
        /// <summary>
        /// 检查数值或者日期值的最小值
        /// </summary>
        [DefaultValue( false )]
        [Category("Format")]
        public bool CheckMinValue
        {
            get
            {
                return _CheckMinValue; 
            }
            set
            {
                _CheckMinValue = value; 
            }
        }

        private double dblMaxValue = 0.0;
        /// <summary>
        /// 数值最大值
        /// </summary>
        [System.ComponentModel.DefaultValue( 0.0 )]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        [System.ComponentModel.Category("Format")]
        public double MaxValue
        {
            get 
            {
                return dblMaxValue; 
            }
            set
            {
                dblMaxValue = value; 
            }
        }

        private double dblMinValue = 0.0;
        /// <summary>
        /// 数值最小值
        /// </summary>
        [System.ComponentModel.DefaultValue( 0.0 )]
        [System.ComponentModel.RefreshProperties( System.ComponentModel.RefreshProperties.All )]
        [System.ComponentModel.Category("Format")]
        public double MinValue
        {
            get
            {
                return dblMinValue; 
            }
            set
            {
                dblMinValue = value; 
            }
        }

        public readonly static DateTime NullDateTime = new DateTime(1980, 1, 1, 0, 0, 0);

        private DateTime dtmDateTimeMaxValue = NullDateTime;
        /// <summary>
        /// 最大时间日期值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        [System.ComponentModel.Category("Format")]
        public DateTime DateTimeMaxValue
        {
            get
            {
                return dtmDateTimeMaxValue;
            }
            set
            {
                dtmDateTimeMaxValue = value;
            }
        }

        //[System.ComponentModel.Browsable( false )]
        //[System.Xml.Serialization.XmlElement()]
        //[System.ComponentModel.DefaultValue("1980-1-1 0:00:00")]
        //public string DateTimeMaxValueString
        //{
        //    get
        //    {
        //        return dtmDateTimeMaxValue.ToString();
        //    }
        //    set
        //    {
        //        dtmDateTimeMaxValue = DateTime.Parse(value);
        //    }
        //}

        private DateTime dtmDateTimeMinValue = NullDateTime;
        /// <summary>
        /// 最小时间日期值
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        [System.ComponentModel.Category("Format")]
        public DateTime DateTimeMinValue
        {
            get
            {
                return dtmDateTimeMinValue;
            }
            set
            {
                dtmDateTimeMinValue = value;
            }
        }

        //[System.ComponentModel.Browsable( false )]
        //[System.Xml.Serialization.XmlElement()]
        //[System.ComponentModel.DefaultValue("1980-1-1 0:00:00")]
        //public string DateTimeMinValueString
        //{
        //    get
        //    {
        //        return dtmDateTimeMinValue.ToString();
        //    }
        //    set
        //    {
        //        dtmDateTimeMinValue = DateTime.Parse(value);
        //    }
        //}

        private string strRange = null;
        /// <summary>
        /// 数据取值范围
        /// </summary>
        [System.ComponentModel.Category("Format")]
        [System.ComponentModel.DefaultValue(null)]
        public string Range
        {
            get
            {
                return strRange; 
            }
            set 
            {
                strRange = value; 
            }
        }

        private string strCustomMessage = null;
        /// <summary>
        /// 自定义提示信息
        /// </summary>
        [System.ComponentModel.Category("Format")]
        [System.ComponentModel.DefaultValue( null )]
        public string CustomMessage
        {
            get
            {
                return strCustomMessage; 
            }
            set
            {
                strCustomMessage = value; 
            }
        }

        private int _ContentVersion = -1;
        /// <summary>
        /// 进行数据校验时的相关的内容版本号
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public int ContentVersion
        {
            get
            {
                return _ContentVersion; 
            }
            set
            {
                _ContentVersion = value; 
            }
        }

        /// <summary>
        /// 对象没有任何有效设置
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public bool IsEmpty
        {
            get
            {
                if (this.bolRequired)
                {
                    return false;
                }
                if (intValueType == ValueTypeStyle.Text)
                {
                    if (this.CheckMaxValue || this.CheckMinValue)
                    {
                        return false;
                    }
                }
                else
                {
                    return false ;
                }
                if (this.strRange != null && strRange.Trim().Length > 0)
                {
                    return false;
                }
                if (this.strValueName != null && strValueName.Trim().Length > 0)
                {
                    return false;
                }
                if (strCustomMessage != null && strCustomMessage.Trim().Length > 0)
                {
                    return false;
                }
                return true;
            }
        }

        private string strMessage = null;
        /// <summary>
        /// 验证结果
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public string Message
        {
            get 
            {
                return strMessage; 
            }
            set
            {
                strMessage = value; 
            }
        }

        /// <summary>
        /// 进行数据验证
        /// </summary>
        /// <returns>验证是否通过</returns>
        public bool Validate()
        {
            strMessage = null;
            string cm = this.CustomMessage;

            if (cm == null || cm.Trim().Length == 0)
            {
                cm = null;
            }
            bool isNullValue = false;
            if (objValue == null || DBNull.Value.Equals(objValue))
            {
                isNullValue = true;
            }
            else
            {
                string txt = Convert.ToString(objValue);
                if (string.IsNullOrEmpty(txt))
                {
                    isNullValue = true;
                }
            }
            if (this.Required)
            {
                if (isNullValue)
                {
                    strMessage = cm != null ? cm : ValueValidateStyleStrings.ForbidEmpty;
                    return false;
                }
            }
            else
            {
                if (isNullValue)
                {
                    // 数据为空而且允许为空,则不进行后续判断
                    return true;
                }
            }
            if (intValueType == ValueTypeStyle.Text)
            {
                string txt = Convert.ToString(objValue);
                if ( this.CheckMaxValue && intMaxLength > 0)
                {
                    if (txt != null && txt.Length > this.MaxLength)
                    {
                        strMessage = cm != null ? cm : string.Format(ValueValidateStyleStrings.MoreThanMaxLength_Length, intMaxLength);
                        return false;
                    }
                }
                if ( this.CheckMinValue && this.MinLength > 0)
                {
                    if (txt != null && txt.Length < intMinLength)
                    {
                        strMessage = cm != null ? cm : string.Format(ValueValidateStyleStrings.LessThanMinLength_Length, intMinLength);
                        return false;
                    }
                }
                if (strRange != null && strRange.Length > 0)
                {
                    bool find = true ;
                    string[] items = strRange.Split(new char[] { ',' });
                    foreach (string item in items)
                    {
                        find = false;
                        if (string.Compare(txt, item.Trim(), true) == 0)
                        {
                            find = true ;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.ExcludeRange_Range , strRange );
                        return false;
                    }
                }
            }
            else if (intValueType == ValueTypeStyle.Numeric 
                || intValueType == ValueTypeStyle.Integer )
            {
                double v = double.NaN;
                if (objValue is Int32 || objValue is float || objValue is double)
                {
                    v = (double)objValue;
                }
                else
                {
                    bool result = false;
                    if (intValueType == ValueTypeStyle.Numeric)
                    {
                        result = double.TryParse(Convert.ToString(objValue), out v);
                        if (result == false)
                        {
                            strMessage = cm != null ? cm : ValueValidateStyleStrings.MustNumeric;
                            return false;
                        }
                    }
                    else
                    {
                        int v2 = int.MinValue;
                        result = ValueTypeHelper.TryParseInt32(Convert.ToString(objValue), out v2);
                        if (result)
                        {
                            v = v2;
                        }
                        else
                        {
                            strMessage = cm != null ? cm : ValueValidateStyleStrings.MustInteger ;
                            return false;
                        }
                    }
                    
                }
                if ( this.CheckMaxValue && double.IsNaN(dblMaxValue) == false)
                {
                    if (v > dblMaxValue)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.MoreThanMaxValue_Value ,dblMaxValue);
                        return false;
                    }
                }
                if ( this.CheckMinValue && double.IsNaN(dblMinValue) == false)
                {
                    if (v < dblMinValue)
                    {
                        strMessage = cm != null ? cm : string.Format(ValueValidateStyleStrings.LessThanMinValue_Value, dblMinValue);
                        return false;
                    }
                }
                if (strRange != null && strRange.Length > 0)
                {
                    bool find = true ;
                    string[] items = strRange.Split(new char[] { ',' });
                    foreach (string item in items)
                    {
                        find = false;
                        int index = item.IndexOf('-');
                        if (index > 0)
                        {
                            double min = 0;
                            double max = 0;
                            if (double.TryParse(item.Substring(0, index), out min)
                                && double.TryParse(item.Substring(index + 1), out max))
                            {
                                if (v >= min && v <= max)
                                {
                                    find = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            double v2 = double.NaN ;
                            if (double.TryParse(item, out v2))
                            {
                                if (v2 == v)
                                {
                                    find = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (find == false)
                    {
                        strMessage = cm != null ? cm : string.Format(ValueValidateStyleStrings.ExcludeRange_Range, strRange);
                        return false;
                    }
                }
            }
            else if (intValueType == ValueTypeStyle.Date)
            {
                DateTime dtm = DateTime.MinValue ;
                bool result = DateTime.TryParse(Convert.ToString(objValue), out dtm);
                if (result == false)
                {
                    strMessage = cm != null ? cm : ValueValidateStyleStrings.MustDateType;
                    return false;
                }
                dtm = dtm.Date;
                if ( this.CheckMaxValue )
                {
                    DateTime max = dtmDateTimeMaxValue.Date;
                    if (dtm > max)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.MoreThanMaxValue_Value , max.ToString("yyyy-MM-dd"));
                        return false;
                    }
                }
                if ( this.CheckMinValue )
                {
                    DateTime min = dtmDateTimeMinValue.Date ;
                    if (dtm < min)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.LessThanMinValue_Value , min.ToString("yyyy-MM-dd"));
                        return false;
                    }
                }
            }
            else if (intValueType == ValueTypeStyle.Time)
            {
                TimeSpan dtm = TimeSpan.Zero;
                bool result = TimeSpan.TryParse(Convert.ToString(objValue), out dtm);
                if (result == false)
                {
                    strMessage = cm != null ? cm : ValueValidateStyleStrings.MustTimeType ;
                    return false;
                }
                if ( this.CheckMaxValue )
                {
                    TimeSpan max = dtmDateTimeMaxValue.TimeOfDay;
                    if (dtm > max)
                    {
                        strMessage = cm != null ? cm : string.Format(ValueValidateStyleStrings.MoreThanMaxValue_Value, max);
                        return false;
                    }
                }
                if ( this.CheckMinValue )
                {
                    TimeSpan min = dtmDateTimeMinValue.TimeOfDay;
                    if (dtm < min)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.LessThanMinValue_Value , min );
                        return false;
                    }
                }
            }
            else if (intValueType == ValueTypeStyle.DateTime)
            {
                DateTime dtm = DateTime.MinValue;
                bool result = DateTime.TryParse(Convert.ToString(objValue), out dtm);
                if (result == false)
                {
                    strMessage = cm != null ? cm : ValueValidateStyleStrings.MustDateTimeType;
                    return false;
                }
                if ( this.CheckMaxValue )
                {
                    DateTime max = dtmDateTimeMaxValue;
                    if (dtm > max)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.MoreThanMaxValue_Value , max.ToString("yyyy-MM-dd HH:mm:ss"));
                        return false;
                    }
                }
                if ( this.CheckMinValue )
                {
                    DateTime min = dtmDateTimeMinValue;
                    if (dtm < min)
                    {
                        strMessage = cm != null ? cm : string.Format( ValueValidateStyleStrings.LessThanMinValue_Value , min.ToString("yyyy-MM-dd HH:mm:ss"));
                        return false;
                    }
                }
            }
            return true;
        }
        public override string ToString()
        {
            return "Type:" + intValueType.ToString();
        }
        public string ToStyleString()
        {
            StringBuilder myStr = new StringBuilder();
            if( strValueName != null && strValueName.Trim().Length > 0 )
            {
                AddItem( myStr , "ValueName" , strValueName );
            }
            if (strCustomMessage != null && strCustomMessage.Trim().Length > 0)
            {
                AddItem(myStr, "CustomMessage", strCustomMessage.Trim());
            }
            if (bolRequired)
            {
                AddItem(myStr, "Required", "true");
            }
            if (intValueType != ValueTypeStyle.Text)
            {
                AddItem(myStr, "ValueType", intValueType.ToString());
            }
            if (intValueType == ValueTypeStyle.Text)
            {
                if ( this.CheckMaxValue && this.MaxLength > 0)
                {
                    AddItem(myStr, "MaxLength", this.MaxLength.ToString());
                }
                if ( this.CheckMinValue && this.MinLength > 0)
                {
                    AddItem(myStr, "MinLength", this.MinLength.ToString());
                }
            }
            if (intValueType == ValueTypeStyle.Numeric
                || intValueType == ValueTypeStyle.Integer)
            {
                if ( this.CheckMaxValue && double.IsNaN(dblMaxValue) == false)
                {
                    AddItem(myStr, "MaxValue", dblMaxValue.ToString());
                }
                if ( this.CheckMinValue && double.IsNaN(dblMinValue) == false)
                {
                    AddItem(myStr, "MinValue", dblMinValue.ToString());
                }
            }
            if (intValueType == ValueTypeStyle.Date
                || intValueType == ValueTypeStyle.Time
                || intValueType == ValueTypeStyle.DateTime)
            {
                if (dtmDateTimeMaxValue != DateTime.MinValue)
                {
                    if (this.CheckMaxValue)
                    {
                        AddItem(myStr, "DateTimeMaxValue", dtmDateTimeMaxValue.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    if (this.CheckMinValue)
                    {
                        AddItem(myStr, "DateTimeMaxValue", dtmDateTimeMinValue.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            if (strRange != null && strRange.Trim().Length > 0)
            {
                AddItem(myStr, "Range", strRange.Trim());
            }
            return myStr.ToString();
        }

        private void AddItem(StringBuilder str, string name, string Value)
        {
            if (str.Length > 0)
            {
                str.Append(",");
            }
            str.Append(name + ":" + Value);
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            ValueValidateStyle style = (ValueValidateStyle)this.MemberwiseClone();
            return style;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ValueValidateStyle Clone()
        {
            return (ValueValidateStyle)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum ValueTypeStyle
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text ,
        /// <summary>
        /// 整数
        /// </summary>
        Integer,
        /// <summary>
        /// 数值
        /// </summary>
        Numeric ,
        /// <summary>
        /// 日期
        /// </summary>
        Date ,
        /// <summary>
        /// 时间
        /// </summary>
        Time ,
        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime 
    }

    public class ValueValidateStyleTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(ValueValidateStyle), attributes);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
