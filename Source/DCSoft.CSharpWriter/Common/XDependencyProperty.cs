/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;

namespace DCSoft.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public sealed class XDependencyProperty
    {

        /// <summary>
        /// 表示没有默认值的数值
        /// </summary>
        public readonly static object NullDefaultValue = new object();

        private static Dictionary<Type, Dictionary<string, XDependencyProperty>> _PropertiyTable 
            = new Dictionary<Type, Dictionary<string, XDependencyProperty>>();
        
        /// <summary>
        /// 注册属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="propertyType">属性数据类型</param>
        /// <param name="ownerType">属性所属对象类型</param>
        /// <returns>属性注册对象</returns>
        public static XDependencyProperty Register(
            string name, 
            Type propertyType,
            Type ownerType )
        {
            if (name == null || name.Trim().Length == 0)
            {
                throw new ArgumentNullException("name");
            }
            name = name.Trim();
            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            Dictionary<string, XDependencyProperty> table = null;
            if (_PropertiyTable.ContainsKey(ownerType))
            {
                table = _PropertiyTable[ownerType];
            }
            else
            {
                table = new Dictionary<string, XDependencyProperty>();
                _PropertiyTable[ownerType] = table;
            }
            if (table.ContainsKey(name))
            {
                throw new ArgumentException("Multi " + name);
            }
            XDependencyProperty property = new XDependencyProperty(
                ownerType ,
                name ,
                propertyType );
            property.DefaultValue = ValueTypeHelper.GetDefaultValue(propertyType);
            table[ name ] = property ;
            return property ;
        }

        /// <summary>
        /// 注册属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="propertyType">属性数据类型</param>
        /// <param name="ownerType">属性所属对象类型</param>
        /// <returns>属性注册对象</returns>
        public static XDependencyProperty Register(
            string name,
            Type propertyType,
            Type ownerType,
            object defaultValue)
        {
            if (name == null || name.Trim().Length == 0)
            {
                throw new ArgumentNullException("name");
            }
            name = name.Trim();
            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            if (defaultValue != null)
            {
                if (propertyType.IsInstanceOfType(defaultValue) == false)
                {
                    throw new ArgumentException("bad defaultValue:" + defaultValue);
                }
            }
            Dictionary<string, XDependencyProperty> table = null;
            if (_PropertiyTable.ContainsKey(ownerType))
            {
                table = _PropertiyTable[ownerType];
            }
            else
            {
                table = new Dictionary<string, XDependencyProperty>();
                _PropertiyTable[ownerType] = table;
            }
            if (table.ContainsKey(name))
            {
                throw new ArgumentException("Multi " + name);
            }
            XDependencyProperty property = new XDependencyProperty(
                ownerType,
                name,
                propertyType);
            property.DefaultValue = defaultValue;
            table[name] = property;
            return property;
        }

        public static XDependencyProperty GetProperty(Type ownerType, string name)
        {
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            foreach (Type type in _PropertiyTable.Keys)
            {
                if (type == ownerType || ownerType.IsSubclassOf(type))
                {
                    Dictionary<string, XDependencyProperty> table = _PropertiyTable[type];
                    if (table.ContainsKey(name))
                    {
                        return table[name];
                    }
                }
            }
            return null;
        }

        public static XDependencyProperty[] GetProperties(Type ownerType, bool declearTypeOnly)
        {
            if (ownerType == null)
            {
                throw new ArgumentNullException("ownerType");
            }

            List<XDependencyProperty> result = new List<XDependencyProperty>();
            if (declearTypeOnly)
            {
                if (_PropertiyTable.ContainsKey(ownerType))
                {
                    Dictionary<string, XDependencyProperty> table = _PropertiyTable[ownerType];
                    foreach (XDependencyProperty p in table.Values)
                    {
                        result.Add(p);
                    }
                }
            }
            else
            {
                foreach (Type type in _PropertiyTable.Keys)
                {
                    if (type == ownerType || ownerType.IsSubclassOf(type))
                    {
                        Dictionary<string, XDependencyProperty> table = _PropertiyTable[type];
                        foreach (XDependencyProperty p in table.Values)
                        {
                            result.Add(p);
                        }
                    }
                }
            }
            return result.ToArray();
        }

        //public XDependencyProperty()
        //{
        //}

        private XDependencyProperty(
            Type ownerType,
            string name, 
            Type propertyType)
        {
            _OwnerType = ownerType;
            _Name = name;
            _PropertyType = propertyType;
            _DefaultValue = ValueTypeHelper.GetDefaultValue(_PropertyType);
        }

        private XDependencyProperty(
            Type ownerType, 
            string name,
            Type propertyType ,
            object defaultValue)
        {
            _OwnerType = ownerType;
            _Name = name;
            _PropertyType = propertyType;
            _DefaultValue = defaultValue;
            //_DefaultValue = ValueTypeHelper.GetDefaultValue(_PropertyType);
        }

        internal Type _OwnerType = null;
        /// <summary>
        /// 对象所属的类型
        /// </summary>
        public Type OwnerType
        {
            get
            {
                return _OwnerType; 
            }
            //set
            //{
            //    _OwnerType = value; 
            //}
        }

        private string _Name = null;
        /// <summary>
        /// 对象名称
        /// </summary>
        [DefaultValue(null)]
        public string Name
        {
            get
            {
                return _Name; 
            }
            set
            {
                _Name = value; 
            }
        }

        private Type _PropertyType = typeof(object);
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type PropertyType
        {
            get 
            {
                return _PropertyType; 
            }
            set
            {
                _PropertyType = value; 
            }
        }

        private object _DefaultValue = null;
        /// <summary>
        /// 属性默认值
        /// </summary>
        public object DefaultValue
        {
            get 
            {
                return _DefaultValue; 
            }
            set
            {
                _DefaultValue = value; 
            }
        }

        public bool EqualsDefaultValue(object Value)
        {
            if (_DefaultValue == NullDefaultValue)
            {
                return false;
            }
            if (_DefaultValue is IComparable)
            {
                return ((IComparable)_DefaultValue).CompareTo(Value) == 0;
            }
            return object.Equals(_DefaultValue, Value);
        }

        private System.ComponentModel.TypeConverter _TypeConverter = null;
        /// <summary>
        /// 配套的数据类型转换器
        /// </summary>
        public System.ComponentModel.TypeConverter TypeConverter
        {
            get
            {
                return _TypeConverter; 
            }
            set
            {
                _TypeConverter = value; 
            }
        }

        private System.Drawing.Design.UITypeEditor _Editor = null;
        /// <summary>
        /// 配套的编辑器对象
        /// </summary>
        public System.Drawing.Design.UITypeEditor Editor
        {
            get
            {
                return _Editor; 
            }
            set
            {
                _Editor = value; 
            }
        }

        private bool _IsReadOnly = false;
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set { _IsReadOnly = value; }
        }

        private string _Category = null;
        /// <summary>
        /// 分类
        /// </summary>
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        private string _Description = null;
        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private bool _DesignTimeOnly = false;
        /// <summary>
        /// 是否在设计时才有效
        /// </summary>
        public bool DesignTimeOnly
        {
            get { return _DesignTimeOnly; }
            set { _DesignTimeOnly = value; }
        }

        private string _DisplayName = null;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        private bool _IsBrowsable = true;
        /// <summary>
        /// 是否可浏览
        /// </summary>
        public bool IsBrowsable
        {
            get { return _IsBrowsable; }
            set { _IsBrowsable = value; }
        }

        private bool _IsLocalizable = true;
        /// <summary>
        /// 能否本地化
        /// </summary>
        public bool IsLocalizable
        {
            get { return _IsLocalizable; }
            set { _IsLocalizable = value; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        //private bool _ShouldSerializeValue = false;

        //public bool ShouldSerializeValue
        //{
        //    get { return _ShouldSerializeValue; }
        //    set { _ShouldSerializeValue = value; }
        //}
    }//public class XDependencyProperty


}
