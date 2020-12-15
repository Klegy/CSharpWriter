using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 文档参数对象
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DocumentParameter: ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentParameter()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">参数名称</param>
        public DocumentParameter(string name)
        {
            strName = name;
        }

        private string strName = null;
        /// <summary>
        /// 参数名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.ComponentModel.Category("Design")]
        public string Name
        {
            get
            {
                return strName; 
            }
            set
            {
                strName = value; 
            }
        }

        private string strDescription = null;
        /// <summary>
        /// 参数说明
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.ComponentModel.Category("Design")]
        public string Description
        {
            get
            {
                return strDescription; 
            }
            set
            {
                strDescription = value; 
            }
        }

        private DocumentParameterValueType intValueType = DocumentParameterValueType.Object;
        /// <summary>
        /// 数据类型
        /// </summary>
        [System.ComponentModel.DefaultValue(DocumentParameterValueType.Object)]
        [System.ComponentModel.Category("Data")]
        public DocumentParameterValueType ValueType
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

        private string strSourceColumn = null;
        /// <summary>
        /// 参数值来源栏目名称
        /// </summary>
        public string SourceColumn
        {
            get
            {
                return strSourceColumn; 
            }
            set
            {
                strSourceColumn = value; 
            }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public Type ValueDataType
        {
            get
            {
                switch (intValueType)
                {
                    case DocumentParameterValueType.Text:
                        return typeof(string);
                    case DocumentParameterValueType.Boolean:
                        return typeof(bool);
                    case DocumentParameterValueType.Date:
                        return typeof(DateTime);
                    case DocumentParameterValueType.Time:
                        return typeof(TimeSpan);
                    case DocumentParameterValueType.DateTime:
                        return typeof(DateTime);
                    case DocumentParameterValueType.Numeric:
                        return typeof(double);
                    case DocumentParameterValueType.Binary:
                        return typeof(byte[]);
                    case DocumentParameterValueType.Object:
                        return typeof(object);
                }
                return typeof(string);
            }
        }

        private string strDefaultValue = null;
        /// <summary>
        /// 默认值
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        [System.ComponentModel.Category("Data")]
        public string DefaultValue
        {
            get
            {
                return strDefaultValue; 
            }
            set
            {
                strDefaultValue = value; 
            }
        }

        private object objValue = null;
        /// <summary>
        /// 参数值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public object Value
        {
            get
            {
                if (this.ValueType == DocumentParameterValueType.Object)
                    return objValue;
                if (objValue == null && strDefaultValue != null)
                {
                    object v = ValueTypeHelper.GetDefaultValue( this.ValueDataType ) ;
                    ValueTypeHelper.TryConvertTo(strDefaultValue, this.ValueDataType, ref v);
                    return v ;
                }
                else
                {
                    return objValue;
                }
            }
            set
            {
                try
                {
                    if (value == null || DBNull.Value.Equals(value))
                    {
                        objValue = ValueTypeHelper.GetDefaultValue(this.ValueDataType);
                        if (objValue == null)
                        {
                            objValue = DBNull.Value;
                        }
                    }
                    else
                    {
                        objValue = ValueTypeHelper.ConvertTo(value, this.ValueDataType);
                        if (intValueType == DocumentParameterValueType.Date)
                        {
                            DateTime dtm = Convert.ToDateTime(objValue);
                            objValue = dtm.Date;
                        }
                    }
                }
                catch (Exception ext )
                {
                    throw new Exception( string.Format(
                        WriterStrings.BadParameterValueType_Name_Type_Value,
                        strName,
                        intValueType,
                        Value ) , ext );
                }
            }
        }
        /// <summary>
        /// 获得字符串值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement()]
        public string StringValue
        {
            get
            {
                object v = this.Value;
                if (v == null || DBNull.Value.Equals(v))
                {
                    return this.DefaultValue;
                }
                else
                {
                    return Convert.ToString(v);
                }
            }
        }

        /// <summary>
        /// 用于序列化/反序列化的用户设置的参数值的属性值
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlElement()]
        public string SerializeStringValue
        {
            get
            {
                if (objValue == null || DBNull.Value.Equals(objValue))
                {
                    return this.DefaultValue;
                }
                else
                {
                    return this.StringValue;
                }
            }
            set
            {
                this.Value = value;
            }
        }

        ///// <summary>
        ///// 根据文本设置参数值
        ///// </summary>
        ///// <param name="strValue">文本值</param>
        //public void SetValue(string strValue)
        //{
        //    try
        //    {
        //        switch (intValueType)
        //        {
        //            case ParameterValueType.Text:
        //                {
        //                    objValue = strValue;
        //                }
        //                break;
        //            case ParameterValueType.Numeric:
        //                {
        //                    objValue = double.Parse(strValue);
        //                }
        //                break;
        //            case ParameterValueType.Date:
        //                {
        //                    DateTime dtm = DateTime.Parse(strValue);
        //                    objValue = dtm.Date;
        //                }
        //                break;
        //            case ParameterValueType.Time:
        //                {
        //                    objValue = TimeSpan.Parse(strValue);
        //                    break;
        //                }
        //            case ParameterValueType.DateTime:
        //                objValue = DateTime.Parse(strValue);
        //                break;
        //        }
        //    }
        //    catch (Exception )
        //    {
        //        throw new Exception( string.Format(
        //            DataSourceDomStrings.Instance.BadParameterValueType_Name_Type_Value ,
        //            strName ,
        //            intValueType ,
        //            strValue ));
        //    }
        //}
        /// <summary>
        /// 尝试根据文本值设置参数值,不触发异常
        /// </summary>
        /// <param name="strValue">文本值</param>
        /// <returns>操作是否成功</returns>
        public bool TrySetValue(string strValue)
        {
            return ValueTypeHelper.TryConvertTo(strValue, this.ValueDataType, ref objValue);
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return strName + "=" + objValue;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            DocumentParameter p = (DocumentParameter)this.MemberwiseClone();
            return p;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentParameter Clone()
        {
            return (DocumentParameter)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// 文档参数列表
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.ComVisible( true )]
    public class DocumentParameterCollection : List<DocumentParameter>, System.Collections.IDictionary , ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentParameterCollection()
        {
        }

        public void CopytValuesTo(DocumentParameterCollection ps)
        {
            if (ps != null)
            {
                foreach (DocumentParameter p in this)
                {
                    ps.SetValue(p.Name, p.Value);
                }
            }
        }
        //private int _ContentVersion = 0;
        ///// <summary>
        ///// 对象内容版本，对象数据的任何修改都会导致该版本号增加
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //public int ContentVersion
        //{
        //    get
        //    {
        //        return _ContentVersion; 
        //    }
        //}
         

        /// <summary>
        /// 获得指定名称的参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>获得的参数对象</returns>
        public DocumentParameter this[string name]
        {
            get
            {
                foreach (DocumentParameter p in this)
                {
                    if (string.Compare(p.Name, name, true) == 0)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 获得指定名称的参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数值</returns>
        public object GetValue(string name)
        {
            DocumentParameter p = this[name];
            if (p == null)
            {
                return null;
            }
            else
            {
                return p.Value;
            }
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="Value">参数值</param>
        public void SetValue(string name, object Value)
        {
            DocumentParameter p = this[name];
            if (p == null)
            {
                p = new DocumentParameter();
                p.Name = name;
                p.Value = Value;
                this.Add(p);
            }
            else
            {
                p.Value = Value;
            }
        }

        

        public void SetXmlValue(string name, string xmlText)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            if (string.IsNullOrEmpty(xmlText))
            {
                doc.LoadXml("<a/>");
            }
            else
            {
                try
                {
                    doc.LoadXml(xmlText);
                }
                catch (Exception ext)
                {
                    System.Diagnostics.Debug.WriteLine(ext.Message);
                    doc.LoadXml("<a/>");
                }
            }
            this.SetValue(name, doc.DocumentElement);
        }

        public string GetXmlValue(string name)
        {
            object v = GetValue(name);
            if (v is System.Xml.XmlNode)
            {
                return ((System.Xml.XmlNode)v).OuterXml;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断是否存在指定名称的参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns>是否有指定名称的参数</returns>
        public bool Contains(string name)
        {
            return this[name] != null;
        }

        /// <summary>
        /// 删除指定名称的参数
        /// </summary>
        /// <param name="name">参数名</param>
        public void Remove(string name)
        {
            DocumentParameter p = this[name];
            if (p != null)
            {
                this.Remove(p);
            }
        }

        /// <summary>
        /// 所有参数名称
        /// </summary>
        public string[] Names
        {
            get
            {
                string[] names = new string[this.Count];
                for (int iCount = 0; iCount < this.Count; iCount++)
                {
                    names[iCount] = this[iCount].Name;
                }
                return names;
            }
        }

        #region IDictionary 成员

        void System.Collections.IDictionary.Add(object key, object value)
        {
            SetValue(Convert.ToString(key), value);
        }

        void System.Collections.IDictionary.Clear()
        {
            this.Clear();
        }

        bool System.Collections.IDictionary.Contains(object key)
        {
            return Contains(Convert.ToString(key));
        }

        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        bool System.Collections.IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool System.Collections.IDictionary.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        System.Collections.ICollection System.Collections.IDictionary.Keys
        {
            get
            {
                string[] names = new string[this.Count];
                for (int iCount = 0; iCount < this.Count; iCount++)
                {
                    names[iCount] = this[iCount].Name;
                }
                return names;
            }
        }

        void System.Collections.IDictionary.Remove(object key)
        {
            DocumentParameter p = this[Convert.ToString(key)];
            if (p != null)
            {
                this.Remove(p);
            }
        }

        System.Collections.ICollection System.Collections.IDictionary.Values
        {
            get
            {
                object[] values = new object[this.Count];
                for (int iCount = 0; iCount < this.Count; iCount++)
                {
                    values[iCount] = this[iCount].Value;
                }
                return values;
            }
        }

        object System.Collections.IDictionary.this[object key]
        {
            get
            {
                return GetValue(Convert.ToString(key));
            }
            set
            {
                SetValue(Convert.ToString(key), value);
            }
        }

        #endregion

        #region ICollection 成员

        void System.Collections.ICollection.CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        int System.Collections.ICollection.Count
        {
            get { return this.Count; }
        }

        bool System.Collections.ICollection.IsSynchronized
        {
            get { return false; }
        }

        object System.Collections.ICollection.SyncRoot
        {
            get { return null; }
        }

        #endregion

        //#region IEnumerable 成员

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentParameterCollection Clone()
        {
            return (DocumentParameterCollection)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            DocumentParameterCollection ps = new DocumentParameterCollection();
            foreach (DocumentParameter p in this)
            {
                ps.Add(p.Clone());
            }
            return ps;
        }
    }

    public enum DocumentParameterValueType
    {
        /// <summary>
        /// 纯文本
        /// </summary>
        Text,
        /// <summary>
        /// 布尔类型值
        /// </summary>
        Boolean,
        /// <summary>
        /// 数值
        /// </summary>
        Numeric,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 时间
        /// </summary>
        Time,
        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 二进制数据
        /// </summary>
        Binary,
        /// <summary>
        /// 对象类型
        /// </summary>
        Object
    }
}
