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
using DCSoft.Common;
using System.Data;

namespace DCSoft.DataSourceDom
{
    /// <summary>
    /// 数据源参数对象
    /// </summary>
    [Serializable()]
    public class XParameter : System.ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XParameter()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">参数名称</param>
        public XParameter(string name)
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

        private DataSourceValueType intValueType = DataSourceValueType.Object;
        /// <summary>
        /// 数据类型
        /// </summary>
        [System.ComponentModel.DefaultValue( DataSourceValueType.Object )]
        [System.ComponentModel.Category("Data")]
        public DataSourceValueType ValueType
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
                    case DataSourceValueType.Text :
                        return typeof(string);
                    case DataSourceValueType.Boolean :
                        return typeof(bool);
                    case DataSourceValueType.Date :
                        return typeof(DateTime);
                    case DataSourceValueType.Time :
                        return typeof(TimeSpan);
                    case DataSourceValueType.DateTime :
                        return typeof(DateTime);
                    case DataSourceValueType.Numeric :
                        return typeof(double);
                    case DataSourceValueType.Binary :
                        return typeof(byte[]);
                    case DataSourceValueType.Object :
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
                if (this.ValueType == DataSourceValueType.Object)
                    return objValue;
                if (objValue == null && strDefaultValue != null)
                {
                    object v = ValueTypeHelper.GetDefaultValue( this.ValueDataType ) ;
                    ValueTypeHelper.TryConvertTo( strDefaultValue , this.ValueDataType , ref v );
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
                        if (intValueType == DataSourceValueType.Date)
                        {
                            DateTime dtm = Convert.ToDateTime(objValue);
                            objValue = dtm.Date;
                        }
                    }
                }
                catch (Exception ext )
                {
                    throw new Exception( string.Format(
                        DataSourceStrings.BadParameterValueType_Name_Type_Value,
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
            XParameter p = new XParameter();
            p.strName = strName;
            p.intValueType = intValueType;
            p.objValue = objValue;
            p.strDefaultValue = strDefaultValue;
            p.strDescription = strDescription;
            return p;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XParameter Clone()
        {
            XParameter p = new XParameter();
            p.strName = strName;
            p.intValueType = intValueType;
            p.objValue = objValue;
            p.strDefaultValue = strDefaultValue;
            p.strDescription = strDescription;
            return p;
        }
    }
    /// <summary>
    /// 参数列表对象
    /// </summary>
    [Serializable()]
    [System.ComponentModel.Editor(
        "DCSoft.DataSourceDom.Design.DataSourceParameterListEditor",
        typeof(System.Drawing.Design.UITypeEditor))]
    public class XParameterList : System.Collections.CollectionBase, System.ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XParameterList()
        {
        }
        /// <summary>
        /// 获得指定序号处的参数对象
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>参数对象</returns>
        public XParameter this[int index]
        {
            get
            {
                return (XParameter)this.List[index];
            }
        }
        /// <summary>
        /// 获得指定名称的参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数对象</returns>
        public XParameter this[string name]
        {
            get
            {
                foreach (XParameter p in this)
                {
                    if (string.Compare(p.Name, name, true) == 0)
                        return p;
                }
                return null;
            }
        }
        /// <summary>
        /// 向列表添加新的参数对象
        /// </summary>
        /// <param name="item">参数对象</param>
        /// <returns>新对象在列表中的序号</returns>
        public int Add(XParameter item)
        {
            return this.List.Add(item);
        }

        public void AddRange(XParameterList list )
        {
            AddRange(list , false );
        }
        public void AddRange(XParameterList list , bool setDefaultValue )
        {
            foreach (XParameter p in list)
            {
                XParameter p2 = this[p.Name];
                if (p2 == null)
                {
                    this.List.Add(p.Clone());
                }
                else
                {
                    p2.ValueType = p.ValueType;
                    p2.Value = p.Value;
                    if (setDefaultValue)
                    {
                        p2.DefaultValue = Convert.ToString(p.Value);
                    }
                }
            }
        }

        /// <summary>
        /// 删除指定的参数对象
        /// </summary>
        /// <param name="item">参数对象</param>
        public void Remove(XParameter item)
        {
            this.List.Remove(item);
        }
        /// <summary>
        /// 删除指定名称的参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        public void Remove(string name)
        {
            XParameter p = this[name];
            if (p != null)
            {
                this.List.Remove(p);
            }
        }

        /// <summary>
        /// 获得指定名称的参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>参数值</returns>
        public object GetValue(string name)
        {
            XParameter p = this[name];
            if (p == null)
            {
                return null;
            }
            else
            {
                if (p.Value == null || DBNull.Value.Equals(p.Value))
                {
                    if (p.DefaultValue != null && p.DefaultValue.Trim().Length > 0)
                        return p.DefaultValue;
                }
                return p.Value;
            }
        }

        public string GetStringValue(string name)
        {
            XParameter p = this[name];
            if (p == null)
                return null;
            else
                return p.StringValue;
        }

        /// <summary>
        /// 设置指定名称的参数值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="Value">参数值</param>
        public void SetValue(string name, object Value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            XParameter p = this[name];
            if (p == null)
            {
                p = new XParameter(name);
                p.ValueType = DataSourceValueType.Object;
                this.List.Add(p);
            }
            p.Value = Value;
        }

        public void SetValues(XParameterList parameters)
        {
            if (parameters != null)
            {
                foreach (XParameter p in parameters)
                {
                    XParameter p2 = this[p.Name];
                    if (p2 != null)
                    {
                        //p2.ValueDataType = p.ValueDataType;
                        p2.ValueType = p.ValueType;
                        p2.Value = p.Value;
                    }
                    else
                    {
                        this.List.Add(p.Clone());
                    }
                }
            }
        }

        public void SetValue2(string name, object Value)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            XParameter p = this[name];
            if (p == null)
            {
                p = new XParameter(name);
                p.ValueType = DataSourceValueType.Object;
                this.List.Add(p);
            }
            p.Value = Value;
            p.DefaultValue = Convert.ToString(Value);
        }

        /// <summary>
        /// 判断是否存在指定名称的参数对象
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>是否存在指定名称的参数对象</returns>
        public bool Contains(string name)
        {
            return this[name] != null;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            XParameterList list = new XParameterList();
            foreach (XParameter p in this)
            {
                list.List.Add(p.Clone());
            }
            return list;
        }
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XParameterList Clone()
        {
            XParameterList list = new XParameterList();
            foreach (XParameter p in this)
            {
                list.List.Add(p.Clone());
            }
            return list;
        }

        /// <summary>
        /// 返回参数对象组成的数组
        /// </summary>
        /// <returns>对象数组</returns>
        public XParameter[] ToArray()
        {
            return (XParameter[])this.InnerList.ToArray(typeof(XParameter));
        }
        /// <summary>
        /// 获得所有参数名称组成的数组
        /// </summary>
        /// <returns>名称数组</returns>
        public string[] GetNames()
        {
            string[] names = new string[this.Count];
            for (int iCount = 0; iCount < this.Count; iCount++)
            {
                names[iCount] = ((XParameter)this.List[iCount]).Name;
            }
            return names;
        }
        public override string ToString()
        {
            return this.Count + " parameters";
        }
    }

    /// <summary>
    /// 用于存储数据库命令信息的对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable()]
    public class XDBCommandInfo : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDBCommandInfo()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="commandText">命令文本</param>
        public XDBCommandInfo(string commandText)
        {
            strCommandText = commandText;
        }

        private string strCommandText = null;
        /// <summary>
        /// 命令文本
        /// </summary>
        public string CommandText
        {
            get
            {
                return strCommandText; 
            }
            set
            {
                strCommandText = value; 
            }
        }

        private int intCommandTimeout = 30;
        /// <summary>
        /// 超时时间，单位秒，默认30秒。
        /// </summary>
        [System.ComponentModel.DefaultValue(30)]
        public int CommandTimeout
        {
            get 
            {
                return intCommandTimeout; 
            }
            set
            {
                intCommandTimeout = value; 
            }
        }
        private XParameterList myParameters = new XParameterList();
        /// <summary>
        /// 参数列表
        /// </summary>
        public XParameterList Parameters
        {
            get
            {
                return myParameters; 
            }
            set
            {
                myParameters = value;
            }
        }

        /// <summary>
        /// 根据对象数据创建一个实际使用的数据库命令对象
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        /// <returns>创建的的数据库命令对象</returns>
        public IDbCommand CreateCommand(IDbConnection conn)
        {
            System.Data.IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = this.CommandText;
            cmd.CommandTimeout = this.CommandTimeout;
            foreach (XParameter p in myParameters)
            {
                IDbDataParameter p2 = cmd.CreateParameter();
                if (p.Name != null)
                {
                    p2.ParameterName = p.Name;
                }
                p2.Value = p.Value;
                p2.SourceColumn = p.SourceColumn;
                cmd.Parameters.Add(p2);
            }
            return cmd;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object System.ICloneable.Clone()
        {
            XDBCommandInfo info = new XDBCommandInfo();
            info.intCommandTimeout = this.intCommandTimeout;
            info.myParameters = new XParameterList();
            foreach (XParameter p in myParameters)
            {
                info.myParameters.Add(p.Clone());
            }
            info.strCommandText = this.strCommandText;

            return info;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XDBCommandInfo Clone()
        {
            return ( XDBCommandInfo ) ((System.ICloneable)this).Clone();
        }
    }


    /// <summary>
    /// 使用一个字典来实现的字符串变量提供者对象
    /// </summary>
    public class DataSourceParameterVariableProvider : IVariableProvider
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DataSourceParameterVariableProvider()
        {
            myValues = new XParameterList();
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="vars">字典对象</param>
        public DataSourceParameterVariableProvider(XParameterList  vars)
        {
            myValues = vars;
        }
        private XParameterList myValues = null;
        /// <summary>
        /// 保存数据的字典对象
        /// </summary>
        public XParameterList Values
        {
            get
            {
                return myValues; 
            }
        }
        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="Name">变量名称</param>
        /// <param name="Value">变量值</param>
        public void Set(string Name, string Value)
        {
            myValues.SetValue( Name , Value );
        }
        /// <summary>
        /// 判断是否存在指定名称的变量
        /// </summary>
        /// <param name="Name">变量名称</param>
        /// <returns>是否存在指定名称的变量</returns>
        public bool Exists(string Name)
        {
            return myValues.Contains( Name );
        }

        /// <summary>
        /// 获得指定名称的变量值
        /// </summary>
        /// <param name="Name">变量名称</param>
        /// <returns>变量值</returns>
        public string Get(string Name)
        {
            object v = myValues.GetValue(Name);
            if (v == null || DBNull.Value.Equals( v ))
                return null;
            else
                return Convert.ToString(v);
        }

        /// <summary>
        /// 获得所有变量的名称
        /// </summary>
        public string[] AllNames
        {
            get
            {
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                foreach (XParameter p in myValues )
                {
                    list.Add(p.Name);
                }
                return (string[])list.ToArray(typeof(string));
            }
        }
    }//public class DataSourceParameterVariableProvider : IVariableProvider
}
