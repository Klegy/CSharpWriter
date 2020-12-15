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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Collections;

namespace DCSoft.Common
{
    /// <summary>
    /// 具有附加属性系统的对象类型
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public abstract class XDependencyObject : ICustomTypeDescriptor
    {

        #region 静态成员 ****************************************************

        

        public static void ApplyDefaultValuePropertyNames( XDependencyObject instance , string names )
        {
            if (string.IsNullOrEmpty(names) == false && instance != null )
            {
                foreach (string name in names.Split(','))
                {
                    bool match = false;
                    foreach (XDependencyProperty p in instance.InnerValues.Keys)
                    {
                        if (p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            match = true;
                            break;
                        }
                    }//foreach
                    if (match == false)
                    {
                        XDependencyProperty p = XDependencyProperty.GetProperty(instance.GetType(), name);
                        if (p != null)
                        {
                            instance.InnerValues[p] = p.DefaultValue;
                        }
                    }
                }//foreach
            }
        }

        ///// <summary>
        ///// 获得采用默认值的属性名称列表
        ///// </summary>
        ///// <returns>属性名称列表</returns>
        //public static string GetDefaultValuePropertyNames(XDependencyObject instance)
        //{
        //    if (instance == null)
        //    {
        //        return null;
        //    }
        //    List<string> result = null;
        //    foreach (XDependencyProperty p in instance.InnerValues.Keys)
        //    {
        //        if (p.EqualsDefaultValue(instance.InnerValues[p]))
        //        {
        //            if (result == null)
        //            {
        //                result = new List<string>();
        //            }
        //            result.Add(p.Name);
        //        }
        //    }
        //    if (result != null)
        //    {
        //        result.Sort();
        //        StringBuilder str = new StringBuilder();
        //        foreach (string name in result)
        //        {
        //            if (str.Length > 0)
        //            {
        //                str.Append(",");
        //            }
        //            str.Append(name);
        //        }
        //        return str.ToString();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public static string GetStyleString(XDependencyObject instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            StringBuilder str = new StringBuilder();
            foreach (XDependencyProperty p in instance._InnerValues.Keys)
            {
                if (str.Length > 0)
                {
                    str.Append(";");
                }
                str.Append(p.Name + ":" + instance._InnerValues[p]);
            }
            return str.ToString();
        }

        /// <summary>
        /// 判断对象是否存在指定名称的属性值，名称不区分大小写
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>是否存在指定名称的属性值</returns>
        public static bool HasPropertyValue(XDependencyObject instance, string propertyName)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (propertyName == null || propertyName.Trim().Length == 0)
            {
                throw new ArgumentNullException("propertyName");
            }
            if (instance._InnerValues != null && instance._InnerValues.Count > 0)
            {
                propertyName = propertyName.Trim();
                foreach (XDependencyProperty p in instance._InnerValues.Keys )
                {
                    if (string.Compare(p.Name, propertyName, true) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获得两个对象中属性值不同的属性
        /// </summary>
        /// <param name="instance1">对象1</param>
        /// <param name="instance2">对象2</param>
        /// <returns>属性值不同的属性对象数组</returns>
        public static XDependencyProperty[] GetDifferenceProperties(
            XDependencyObject instance1,
            XDependencyObject instance2)
        {
            if (instance1 == null)
            {
                throw new ArgumentNullException("instance1");
            }
            if (instance2 == null)
            {
                throw new ArgumentNullException("instance2");
            }
            List<XDependencyProperty> result = new List<XDependencyProperty>();
            foreach (XDependencyProperty p in instance1._InnerValues.Keys)
            {
                if (instance2.InnerValues.ContainsKey(p))
                {
                    object v1 = instance1.InnerValues[p];
                    object v2 = instance2.InnerValues[p];
                    if (v1 != v2)
                    {
                        result.Add(p);
                    }
                }
                else
                {
                    result.Add(p);
                }
            }
            foreach (XDependencyProperty p in instance2.InnerValues.Keys)
            {
                if (result.Contains(p) == false 
                    && instance1.InnerValues.ContainsKey(p) == false)
                {
                    result.Add(p);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 快速复制对象数据，不进行默认值的判断
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <param name="destination">复制目标</param>
        public static void CopyValueFast(XDependencyObject source, XDependencyObject destination)
        {
            if (source == destination)
            {
                return;
            }
            if (source == null || destination == null)
            {
                return;
            }
            destination.InnerValues.Clear();
            foreach (XDependencyProperty p in source.InnerValues.Keys)
            {
                object v = source.InnerValues[p];
                if (v is ICloneable)
                {
                    v = ((ICloneable)v).Clone();
                }
                destination._InnerValues[p] = v;
            }
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="source">数据来源</param>
        /// <param name="destination">复制目标</param>
        public static void CopyValues(
            XDependencyObject source,
            XDependencyObject destination)
        {
            if (source == destination)
            {
                return;
            }
            if (source == null || destination == null)
            {
                return;
            }
            destination.InnerValues.Clear();
            foreach (XDependencyProperty p in source.InnerValues.Keys)
            {
                object v = source.InnerValues[p];
                if (v is ICloneable)
                {
                    v = ((ICloneable)v).Clone();
                }
                destination.SetValue(p, v);
            }
        }

        /// <summary>
        /// 合并数据
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="destination">数据目标对象</param>
        /// <param name="overWrite">源数据是否覆盖目标数据</param>
        /// <returns>修改了目标对象的属性个数</returns>
        public static int MergeValues(
            XDependencyObject source,
            XDependencyObject destination , 
            bool overWrite )
        {
            if (source == destination)
            {
                return 0 ;
            }
            if (source == null || destination == null)
            {
                return 0 ;
            }
            int result = 0;
            foreach (XDependencyProperty p in source.InnerValues.Keys)
            {
                if (destination.InnerValues.ContainsKey(p) == false)
                {
                    object v = source.GetValue( p );
                    if (v is ICloneable)
                    {
                        v = ((ICloneable)v).Clone();
                    }
                    destination.SetValue(p, v);
                    result++;
                }
                else
                {
                    if (overWrite)
                    {
                        bool back = destination._DisableDefaultValue;
                        destination._DisableDefaultValue = source._DisableDefaultValue;
                        destination.SetValue( p , source.GetValue( p ));
                        destination._DisableDefaultValue = back;
                        result++;
                    }
                    //object v = source.GetValue( p );
                    //object v2 = destination.GetValue(p);
                    //if (v != v2)
                    //{
                    //    destination.SetValue(p, v);
                    //    result++;
                    //}
                }
            }
            return result;
        }

        /// <summary>
        /// 删除指定名称的属性，属性名不区分大小写
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>操作是否修改了数据</returns>
        public static bool RemoveProperty(XDependencyObject instance, string propertyName )
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            foreach (XDependencyProperty p in instance._InnerValues.Keys)
            {
                if (string.Compare(p.Name, propertyName, true) == 0)
                {
                    instance._InnerValues.Remove(p);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 比较对象数据是否相同
        /// </summary>
        /// <param name="instance1">对象1</param>
        /// <param name="instance1">对象2</param>
        /// <returns></returns>
        public static bool EqualsValue(
            XDependencyObject instance1,
            XDependencyObject instance2)
        {
            if (instance1 == instance2)
            {
                return true;
            }
            if (instance1 == null || instance2 == null)
            {
                return false;
            }
            if (instance1.InnerValues.Count != instance2.InnerValues.Count)
            {
                return false;
            }
            foreach (XDependencyProperty p in instance1.InnerValues.Keys)
            {
                if (instance2.InnerValues.ContainsKey(p))
                {
                    object v = instance1.InnerValues[p];
                    object v2 = instance2.InnerValues[p];
                    if (object.Equals(v, v2) == false)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获得对象中所有存在的属性的名称
        /// </summary>
        /// <param name="instance">对象</param>
        /// <returns>属性名称数组</returns>
        public static string[] GetExistPropertyNames(XDependencyObject instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (instance._InnerValues != null)
            {
                string[] result = new string[ instance._InnerValues.Count];
                int iCount = 0;
                foreach (XDependencyProperty p in instance._InnerValues.Keys)
                {
                    result[iCount] = p.Name;
                    iCount++;
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 获得所有的属性值组成的字典，字典键值为属性名，字典值为属性值。
        /// </summary>
        /// <param name="instance">对象</param>
        /// <returns>获得的字典</returns>
        public static Hashtable GetPropertyValues(XDependencyObject instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (instance._InnerValues != null)
            {
                Hashtable table = new Hashtable();
                foreach (XDependencyProperty p in instance._InnerValues.Keys)
                {
                    table[p.Name] = instance._InnerValues[p];
                }
                return table;
            }
            else
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDependencyObject()
        {
        }

        protected XDependencyPropertyObjectValues _InnerValues
            = new XDependencyPropertyObjectValues();

        /// <summary>
        /// 内部的数据列表
        /// </summary>
        protected Dictionary<XDependencyProperty, object> InnerValues
        {
            get
            {
                _InnerValues.CheckState(this.GetType());
                return _InnerValues; 
            }
        }
        
 

        private bool _ValueLocked = false;
        /// <summary>
        /// 是否锁定数据
        /// </summary>
        [DefaultValue(false)]
        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool ValueLocked
        {
            get
            {
                return _ValueLocked;
            }
            set
            {
                _ValueLocked = value;
            }
        }

        protected void ClearWithDispose()
        {
            if (this._InnerValues != null)
            {
                foreach (object obj in this._InnerValues.Values)
                {
                    if (obj is IDisposable)
                    {
                        ((IDisposable)obj).Dispose();
                    }
                }
                this._InnerValues.Clear();
            }
        }


        /// <summary>
        /// 获得对象数据
        /// </summary>
        /// <param name="property">属性对象</param>
        /// <returns>获得的数据</returns>
        public virtual object GetValue(XDependencyProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (property.OwnerType.IsInstanceOfType(this) == false)
            {
                throw new ArgumentException("need " + property.OwnerType.FullName);
            }
            _InnerValues.CheckState(this.GetType());
            if (_InnerValues.ContainsKey(property))
            {
                return _InnerValues[property];
            }
            else
            {
                return property.DefaultValue;
            }
        }

        private bool _DisableDefaultValue = false;
        /// <summary>
        /// 禁止默认值规则
        /// </summary>
        [DefaultValue( false )]
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        [EditorBrowsable(  EditorBrowsableState.Advanced )]
        public bool DisableDefaultValue
        {
            get
            {
                return _DisableDefaultValue; 
            }
            set
            {
                _DisableDefaultValue = value; 
            }
        }

        /// <summary>
        /// 设置对象数据
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="Value">属性值</param>
        public virtual void SetValue(XDependencyProperty property, object Value)
        {
            if (_ValueLocked)
            {
                throw new InvalidOperationException("Locked");
            }
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if ( property.OwnerType.IsInstanceOfType( this ) == false )
            {
                throw new ArgumentException("need " + property.OwnerType.FullName);
            }
            _InnerValues.CheckState(this.GetType());
            if (ValueChanging != null || this is IXDependencyPropertyLoggable)
            {
                object oldValue = null;
                if (_InnerValues.ContainsKey(property))
                {
                    oldValue = _InnerValues[property];
                }
                else
                {
                    oldValue = property.DefaultValue;
                }
                if (ValueChanging != null)
                {
                    XDependencyObjectEventArgs args = new XDependencyObjectEventArgs(
                        this,
                        property,
                        oldValue,
                        Value);
                    ValueChanging(this, args);
                    if (args.Cancel)
                    {
                        return;
                    }
                    Value = args.NewValue;
                }
                if (this is IXDependencyPropertyLoggable)
                {
                    IXDependencyPropertyLogger logger = 
                        ((IXDependencyPropertyLoggable)this).PropertyLogger;
                    if (logger != null && logger.Enabled )
                    {
                        logger.Log(this, property, oldValue, Value);
                    }
                }
                if ( this._DisableDefaultValue == false 
                    && property.EqualsDefaultValue(Value))
                {
                    if (_InnerValues.ContainsKey(property))
                    {
                        _InnerValues.Remove(property);
                    }
                }
                else
                {
                    _InnerValues[property] = Value;
                }
                if (ValueChanged != null)
                {
                    XDependencyObjectEventArgs args2 = new XDependencyObjectEventArgs(
                        this,
                        property,
                        Value,
                        Value);
                    ValueChanged(this, args2);
                }
            }
            else
            {
                if (property.EqualsDefaultValue(Value))
                {
                    if (_InnerValues.ContainsKey(property))
                    {
                        _InnerValues.Remove(property);
                    }
                }
                else
                {
                    _InnerValues[property] = Value;
                }
                if (ValueChanged != null)
                {
                    XDependencyObjectEventArgs args2 = new XDependencyObjectEventArgs(
                        this,
                        property,
                        Value,
                        Value);
                    ValueChanged(this, args2);
                }
            }
            this._ValueModified = true;
        }

        [NonSerialized()]
        private bool _ValueModified = false;
        /// <summary>
        /// 对象数据是否改变标记
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        [System.ComponentModel.Browsable( false )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public bool ValueModified
        {
            get
            {
                return _ValueModified; 
            }
            set
            {
                _ValueModified = value; 
            }
        }

        /// <summary>
        /// 对象数据发生改变时的处理，此时可以取消操作或者修改要设置的新的数值
        /// </summary>
        public event XDependencyObjectEventHandler ValueChanging = null;
        
        /// <summary>
        /// 对象数据发生改变后的处理，此时仅仅通知操作，不能取消和修改新数值
        /// </summary>
        public event XDependencyObjectEventHandler ValueChanged = null;

        #region ICustomTypeDescriptor 成员

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true );
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this,true);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
            XDependencyProperty[] ps = XDependencyProperty.GetProperties(this.GetType(), false);
            foreach (XDependencyProperty p in ps)
            {
                properties.Add(new XDependencyPropertyDescriptor(p, attributes)); 
            }
            return properties;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

    /// <summary>
    /// 能被记录属性值改变记录的对象
    /// </summary>
    public interface IXDependencyPropertyLoggable
    {
        /// <summary>
        /// 获得属性值更改记录对象
        /// </summary>
        IXDependencyPropertyLogger PropertyLogger { get; set; }
    }

    /// <summary>
    /// 记录属性值改变过程的日志对象
    /// </summary>
    public interface IXDependencyPropertyLogger
    {
        
        /// <summary>
        /// 对象是否可用
        /// </summary>
        bool Enabled{ get ;}

        /// <summary>
        /// 做出记录
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="property">属性</param>
        /// <param name="oldValue">旧数据</param>
        /// <param name="newValue">新数据</param>
        void Log(
            XDependencyObject instance,
            XDependencyProperty property, 
            object oldValue,
            object newValue);
    }

    public class DefaultXDependencyPropertyLogger : IXDependencyPropertyLogger
    { 
        private bool _Enabled = true;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public int Count
        {
            get
            {
                return _Items.Count;
            }
        }

        public void Log(
            XDependencyObject instance,
            XDependencyProperty property,
            object oldValue,
            object newValue)
        {
            if (this.Enabled == false)
            {
                return;
            }
            foreach (LogItem item in this._Items)
            {
                if (item.Instance == instance && item.Property == property)
                {
                    item.NewValue = newValue;
                    return;
                }
            }
            LogItem newItem = new LogItem();
            newItem.Instance = instance;
            newItem.Property = property;
            newItem.OldValue = oldValue;
            newItem.NewValue = newValue;
            this._Items.Add(newItem);
        }

        public void Undo()
        {
            for (int iCount = this._Items.Count - 1; iCount >= 0; iCount--)
            {
                LogItem item = this._Items[iCount];
                item.Instance.SetValue(item.Property, item.OldValue);
            }
        }

        public void Redo()
        {
            foreach (LogItem item in _Items)
            {
                item.Instance.SetValue(item.Property, item.NewValue);
            }
        }

        private List<LogItem> _Items = new List<LogItem>();

        private class LogItem
        {
            public XDependencyObject Instance = null;
            public XDependencyProperty Property = null;
            public object OldValue = null;
            public object NewValue = null;

        }
    }

    [Serializable]
    public class XDependencyPropertyObjectValues : Dictionary< XDependencyProperty , object > , ISerializable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDependencyPropertyObjectValues()
        {
        }

        /// <summary>
        /// 为反二进制序列化而定义的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>由于XDependencyProperty类型不能进行二进制序列化，因此在此进行自定义二进制序列化</remarks>
        protected XDependencyPropertyObjectValues(SerializationInfo info, StreamingContext context)
        {
            _serializeValues = (object[])info.GetValue("Values", typeof(object[]));
        }

        private object[] _serializeValues = null;

        /// <summary>
        /// 自定义的进行二进制序列化的过程
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            object[] values = new object[this.Count * 2];
            int iCount = 0;
            foreach (XDependencyProperty p in this.Keys)
            {
                values[iCount * 2] = p.Name;
                values[iCount * 2 + 1] = this[p];
                iCount++;
            }
            info.AddValue("Values", values, typeof(object[]));
        }

        /// <summary>
        /// 检查列表状态，将二进制序列化所得的数据转换为列表数据
        /// </summary>
        /// <param name="ownerType">包含该列表的对象类型</param>
        public void CheckState(Type ownerType)
        {
            if (_serializeValues != null)
            {
                for (int iCount = 0; iCount < _serializeValues.Length; iCount += 2)
                {
                    string name = (string)_serializeValues[iCount];
                    object v = _serializeValues[iCount + 1];
                    XDependencyProperty p = XDependencyProperty.GetProperty( ownerType , name);
                    this[p] = v;
                }
                _serializeValues = null;
            }
        }

        
    }

}