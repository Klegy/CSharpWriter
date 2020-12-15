using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.Collections;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 对象属性值访问路径
    /// </summary>
    public class XDataBindingPath
    {
        /// <summary>
        /// 缓存的路径信息对象
        /// </summary>
        private static List<XDataBindingPath> _buffer = new List<XDataBindingPath>();
        /// <summary>
        /// 错误的路径信息对象
        /// </summary>
        private static List<XDataBindingPath> _badPath = new List<XDataBindingPath>();

        /// <summary>
        /// 清空缓存的数据
        /// </summary>
        public static void ClearBuffer()
        {
            _buffer.Clear();
            _badPath.Clear();
        }

        /// <summary>
        /// 获得指定的对象属性值访问路径
        /// </summary>
        /// <param name="rootType">根对象</param>
        /// <param name="path">访问路径</param>
        /// <param name="throwException">若遇到错误是否抛出异常</param>
        /// <returns>获得的访问信息对象</returns>
        public static XDataBindingPath GetInstance(Type rootType, string path, bool throwException)
        {
            if (rootType == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("rootType");
                }
                else
                {
                    return null;
                }
            }
            foreach (XDataBindingPath item in _buffer)
            {
                if (item.RootType.Equals(rootType)
                    && string.Compare(item.Path, path, false) == 0)
                {
                    return item;
                }
            }

            foreach (XDataBindingPath item in _badPath)
            {
                if (item.RootType == rootType
                    && item.Path == path)
                {
                    if (throwException)
                    {
                        throw new ArgumentOutOfRangeException("Type=" + rootType.FullName + " Path=" + path);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            XDataBindingPath newItem = Create(rootType, path, throwException);
            if (newItem != null)
            {
                _buffer.Add(newItem);
            }
            else
            {
                XDataBindingPath badItem = new XDataBindingPath();
                badItem._RootType = rootType;
                badItem._Path = path;
                _badPath.Add(badItem);
            }
            return newItem;
        }
         

        private static XDataBindingPath Create(Type rootType, string path, bool throwException)
        {
            XDataBindingPath result = new XDataBindingPath();
            result._RootType = rootType;
            result._Path = path;
            if (string.IsNullOrEmpty(path))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = null;
                newItem.InstanceType = rootType;
                result._Items.Add(newItem);
            }
            else if ( typeof( XmlNode ).IsAssignableFrom( rootType ))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = path ;
                newItem.InstanceType = rootType;
                newItem.Style = XBindingPathItemStyle.XPath;
                result._Items.Add(newItem);
            }
            else if( typeof( DataRow ) .IsAssignableFrom( rootType ))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = path;
                newItem.InstanceType = rootType;
                newItem.Style = XBindingPathItemStyle.DataRow;
                result._Items.Add(newItem);
            }
            else if (typeof(DataRowView).IsAssignableFrom(rootType))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = path;
                newItem.InstanceType = rootType;
                newItem.Style = XBindingPathItemStyle.DataRow;
                result._Items.Add(newItem);
            }
            else if (typeof(IDataRecord).IsAssignableFrom(rootType))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = path;
                newItem.InstanceType = rootType;
                newItem.Style = XBindingPathItemStyle.Record ;
                result._Items.Add(newItem);
            }
            else if (typeof(IDictionary).IsAssignableFrom(rootType))
            {
                XDataBindingPathItem newItem = new XDataBindingPathItem();
                newItem.Name = path;
                newItem.InstanceType = rootType;
                newItem.Style = XBindingPathItemStyle.Dictionary;
                result._Items.Add(newItem);
            }
            else
            {
                string[] paths = path.Split('.');
                Type currentType = result._RootType;
                for (int iCount = 0; iCount < paths.Length; iCount++)
                {
                    string item = paths[iCount].Trim();
                    if (string.IsNullOrEmpty(item))
                    {
                        // 将调用对象的ToString方法来获得数据
                        XDataBindingPathItem newItem = new XDataBindingPathItem();
                        newItem.InstanceType = currentType;
                        newItem.Name = null;
                        currentType = typeof(string);
                        result._Items.Add(newItem);
                    }
                    else
                    {
                        PropertyDescriptorCollection ps = TypeDescriptor.GetProperties(currentType);
                        bool find = false;

                        foreach (PropertyDescriptor p in ps)
                        {
                            if (string.Compare(p.Name, item, true) == 0)
                            {
                                XDataBindingPathItem newItem = new XDataBindingPathItem();

                                newItem.InstanceType = currentType;
                                newItem.Property = p;
                                newItem.Name = p.Name;
                                newItem.Style = XBindingPathItemStyle.Property;
                                result._Items.Add(newItem);
                                currentType = p.PropertyType;
                                find = true;
                                break;
                            }
                        }

                        if (find == false)
                        {
                            if (throwException)
                            {
                                throw new NotSupportedException(currentType.FullName + "." + item);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }//for
            }
            // 判断这条路径是否是只读的
            result._Readonly = false;
            XDataBindingPathItem lastItem = result._Items[result._Items.Count - 1];
            if (lastItem.Style == XBindingPathItemStyle.Property)
            {
                if (lastItem.Property == null)
                {
                    result._Readonly = true;
                }
                else
                {
                    result._Readonly = lastItem.Property.IsReadOnly;
                }
            }
            else
            {
                if (lastItem.Style == XBindingPathItemStyle.DataRow
                    || lastItem.Style == XBindingPathItemStyle.Dictionary
                    || lastItem.Style == XBindingPathItemStyle.XPath)
                {
                    result._Readonly = false;
                }
                else
                {
                    result._Readonly = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        private XDataBindingPath()
        {
        }

        private Type _RootType = null;

        public Type RootType
        {
            get { return _RootType; }
        }

        private string _Path = null;

        public string Path
        {
            get { return _Path; }
        }

        private bool _Readonly = false;
        /// <summary>
        /// 路径是只读的
        /// </summary>
        public bool Readonly
        {
            get { return _Readonly; }
        }
         
        private List<XDataBindingPathItem> _Items = new List<XDataBindingPathItem>();

        public List<XDataBindingPathItem> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
            }
        }


    }

    /// <summary>
    /// 绑定路径项目对象
    /// </summary>
    public class XDataBindingPathItem
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDataBindingPathItem()
        {
        }

        private Type _InstanceType = null;
        /// <summary>
        /// 实例对象类型
        /// </summary>
        public Type InstanceType
        {
            get { return _InstanceType; }
            set { _InstanceType = value; }
        }
        private PropertyDescriptor _Property = null;
        /// <summary>
        /// 绑定的属性对象
        /// </summary>
        public PropertyDescriptor Property
        {
            get { return _Property; }
            set { _Property = value; }
        }
        private string _Name = null;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private XBindingPathItemStyle _Style = XBindingPathItemStyle.Property;

        public XBindingPathItemStyle Style
        {
            get { return _Style; }
            set { _Style = value; }
        }
    }

    /// <summary>
    /// 数据源绑定路径项目类型
    /// </summary>
    public enum XBindingPathItemStyle
    {
        /// <summary>
        /// 对象自己
        /// </summary>
        Self,
        /// <summary>
        /// XPath
        /// </summary>
        XPath,
        /// <summary>
        /// 字典
        /// </summary>
        Dictionary,
        /// <summary>
        /// 数据行
        /// </summary>
        DataRow,
        /// <summary>
        /// 对象属性
        /// </summary>
        Property,
        /// <summary>
        /// 记录
        /// </summary>
        Record ,
        /// <summary>
        /// 自动判断
        /// </summary>
        Auto
    }
}
