using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 数据源绑定功能提供者
    /// </summary>
    ///<remarks>编写 袁永福</remarks>
    public class XDataBindingProvider
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XDataBindingProvider()
        {
        }

        //private XTextDocument _Document = null;
        ///// <summary>
        ///// 文档对象
        ///// </summary>
        //public XTextDocument Document
        //{
        //    get { return _Document; }
        //    set { _Document = value; }
        //}

        public virtual object DomReadValue(
            WriterAppHost host ,
            DomDocument document ,
            DomElement element,
            XDataBinding binding ,
            bool throwException )
        {
            object Value = document.GetParameterValue(binding.DataSource);
            if (Value != null)
            {
                object result = this.ReadValue( binding , Value, null, throwException);
                return result ;
            }
            return null;
        }

        public virtual bool DomWriteValue(
            WriterAppHost host ,
            DomDocument document ,
            DomElement element,
            XDataBinding binding, 
            object newValue ,
            bool throwException)
        {
            bool result = false;
            if (binding.Readonly == false)
            {
                if (string.IsNullOrEmpty(binding.BindingPath))
                {
                    DocumentParameter p = document.Parameters[binding.DataSource];
                    if (p != null)
                    {
                        p.Value = newValue;
                        result =  true;
                    }
                }
                else
                {
                    object Value = document.GetParameterValue(binding.DataSource);
                    if (Value != null)
                    {
                        result = WriteValue(binding, Value, newValue, throwException);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 根据一个路径项目读取数据
        /// </summary>
        /// <param name="item">路径项目对象</param>
        /// <param name="instance">对象实例</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns>读取的数据</returns>
        public virtual object ReadItemValue(
            XDataBindingPathItem item,
            object instance,
            object defaultValue,
            bool throwException)
        {
            return StdReadItemValue(item, instance, defaultValue, throwException);
        }

        /// <summary>
        /// 根据一个路径项目读取数据
        /// </summary>
        /// <param name="item">路径项目对象</param>
        /// <param name="instance">对象实例</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="throwException">是否抛出异常</param>
        /// <returns>读取的数据</returns>
        public static object StdReadItemValue(
            XDataBindingPathItem item,
            object instance,
            object defaultValue,
            bool throwException)
        {
            if (item == null)
            {
                // 路径信息对象为空
                if (throwException)
                {
                    throw new ArgumentNullException("item");
                }
                else
                {
                    return defaultValue;
                }
            }
            if (instance == null)
            {
                // 数据源对象为空
                if (throwException)
                {
                    throw new ArgumentNullException("instance");
                }
                else
                {
                    return defaultValue;
                }
            }
            if (item.InstanceType.IsInstanceOfType(instance) == false)
            {
                //  数据源对象类型不匹配
                if (throwException)
                {
                    throw new InvalidCastException(
                        instance.GetType().FullName + "->" + item.InstanceType.FullName);
                }
                else
                {
                    return defaultValue;
                }
            }
            if (string.IsNullOrEmpty(item.Name))
            {
                // 若没有指定名称则返回对象本身
                return instance;
            }
            if (instance is IDictionary)
            {
                // 处理字典
                IDictionary dic = (IDictionary)instance;
                if (dic.Contains(item.Name))
                {
                    return dic[item.Name];
                }
                else
                {
                    return defaultValue;
                }
            }
            else if (instance is XmlNode)
            {
                // 处理XML节点
                XmlNode node = (XmlNode)instance;
                XmlNode node2 = node.SelectSingleNode(item.Name);
                if (node2 == null)
                {
                    return defaultValue;
                }
                else
                {
                    if (node2 is XmlElement)
                    {
                        return node2.InnerText;
                    }
                    else
                    {
                        return node2.Value;
                    }
                }
            }
            
            else if (instance is DataRow)
            {
                // 处理数据行
                DataRow row = (DataRow)instance;
                if (row.Table.Columns.Contains(item.Name) == false)
                {
                    if (throwException)
                    {
                        throw new IndexOutOfRangeException(item.Name);
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
                if (throwException)
                {
                    return row[item.Name];
                }
                else
                {
                    try
                    {
                        return row[item.Name];
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }
            else if (instance is DataRowView)
            {
                // 处理数据视图行
                DataRowView row = (DataRowView)instance;
                if (throwException)
                {
                    return row[item.Name];
                }
                else
                {
                    try
                    {
                        return row[item.Name];
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }
            else if (instance is IDataRecord)
            {
                // 数据记录对象
                IDataRecord record = (IDataRecord)instance;
                int index = record.GetOrdinal(item.Name);
                if (index < 0)
                {
                    if (throwException)
                    {
                        throw new IndexOutOfRangeException(item.Name);
                    }
                    else
                    {
                        return defaultValue;
                    }
                }
                else
                {
                    if (throwException)
                    {
                        return record.GetValue(index);
                    }
                    else
                    {
                        try
                        {
                            return record.GetValue(index);
                        }
                        catch
                        {
                            return defaultValue;
                        }
                    }
                }
            }
            else
            {
                // 读取对象属性数据
                if (throwException)
                {
                    return item.Property.GetValue(instance);
                }
                else
                {
                    try
                    {
                        return item.Property.GetValue(instance);
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }
        }

        /// <summary>
        /// 写入项目数据
        /// </summary>
        /// <param name="item">项目</param>
        /// <param name="instance">对象实例</param>
        /// <param name="newValue">新数据</param>
        /// <param name="throwException">若发生错误是否抛出异常</param>
        /// <returns>操作是否成功</returns>
        public virtual bool WriteItemValue(
            XDataBindingPathItem item,
            object instance,
            object newValue,
            bool throwException)
        {
            if (item == null)
            {
                // 路径信息对象为空
                if (throwException)
                {
                    throw new ArgumentNullException("item");
                }
                else
                {
                    return false;
                }
            }
            if (instance == null)
            {
                // 数据源对象为空
                if (throwException)
                {
                    throw new ArgumentNullException("instance");
                }
                else
                {
                    return false;
                }
            }
            if (item.InstanceType.IsInstanceOfType(instance) == false)
            {
                //  数据源对象类型不匹配
                if (throwException)
                {
                    throw new InvalidCastException(
                        instance.GetType().FullName + ">" + item.InstanceType.FullName);
                }
                else
                {
                    return false;
                }
            }
            if (string.IsNullOrEmpty(item.Name))
            {
                // 若没有指定名称
                return false;
            }
            if (instance is IDictionary)
            {
                // 处理字典
                IDictionary dic = (IDictionary)instance;
                dic[item.Name] = newValue;
                return true;
            }
            else if (instance is XmlNode)
            {
                // 处理XML节点
                XmlNode node = (XmlNode)instance;
                string name = item.Name;
                XmlNode resultNode = null ;
                if (name == null)
                {
                    resultNode = node ;
                }
                else
                {
                    name = name.Trim();
                    if (name.Length == 0)
                    {
                        resultNode = node;
                    }
                    else
                    {
                        resultNode = node.SelectSingleNode(name);
                        if (resultNode == null)
                        {
                            if (System.Xml.XmlReader.IsName(name))
                            {
                                resultNode = node.OwnerDocument.CreateElement(name);
                                node.AppendChild(resultNode);
                            }
                            else if (name.StartsWith("@") && node is XmlElement )
                            {
                                name = name.Substring(1);
                                if (System.Xml.XmlReader.IsName(name))
                                {
                                    resultNode = node.OwnerDocument.CreateAttribute(name);
                                    node.Attributes.Append(( XmlAttribute ) resultNode);
                                }
                            }
                        }
                    }
                }
                if (resultNode == null)
                {
                    return false;
                }
                else if (resultNode is XmlElement)
                {
                    resultNode.InnerText = Convert.ToString(newValue);
                }
                else
                {
                    resultNode.Value = Convert.ToString(newValue);
                }
                return true;
            }
             
            else if (instance is DataRow)
            {
                // 处理数据行
                DataRow row = (DataRow)instance;
                if (row.Table.Columns.Contains(item.Name) == false)
                {
                    if (throwException)
                    {
                        throw new IndexOutOfRangeException(item.Name);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (throwException)
                {
                    row[item.Name] = newValue;
                    return true;
                }
                else
                {
                    try
                    {
                        row[item.Name] = newValue;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else if (instance is DataRowView)
            {
                // 处理数据视图行
                DataRowView row = (DataRowView)instance;
                if (throwException)
                {
                    row[item.Name] = newValue;
                    return true;
                }
                else
                {
                    try
                    {
                        row[item.Name] = newValue;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                // 读取对象属性数据
                if (throwException)
                {
                    item.Property.SetValue(instance, newValue);
                    return true;
                }
                else
                {
                    try
                    {
                        item.Property.SetValue(instance, newValue);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="binding">数据源绑定信息对象</param>
        /// <param name="dataSourceInstance">数据源对象</param>
        /// <param name="newValue">新数据值</param>
        /// <param name="throwException">若发生错误是否抛出异常</param>
        /// <returns>操作是否成功</returns>
        public virtual bool WriteValue(
            XDataBinding binding,
            object dataSourceInstance,
            object newValue,
            bool throwException)
        {
            if (dataSourceInstance == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("dataSourceInstance");
                }
                else
                {
                    return false;
                }
            }

            XDataBindingPath path = XDataBindingPath.GetInstance(
                dataSourceInstance.GetType(),
                binding.BindingPath,
                throwException);
            if (path != null)
            {
                if (path.Readonly)
                {
                    if (throwException)
                    {
                        throw new NotSupportedException("Write " + path.RootType.FullName + "." + path.Path);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    object currentInstance = dataSourceInstance;
                    for (int iCount = 0; iCount < path.Items.Count - 1; iCount++)
                    {
                        object obj = ReadItemValue(
                            path.Items[iCount],
                            currentInstance,
                            null, 
                            throwException);
                        if (obj == null)
                        {

                        }
                        currentInstance = obj;
                    }
                    if (currentInstance == null)
                    {
                        return false;
                    }
                    else
                    {
                        XDataBindingPathItem lastItem = path.Items[path.Items.Count - 1];
                        if (lastItem.Property != null && lastItem.Property.PropertyType != null)
                        {
                            newValue = ConvertType(
                                binding,
                                newValue,
                                lastItem.Property.PropertyType);
                        }
                        return WriteItemValue(
                            lastItem,
                            currentInstance,
                            newValue,
                            throwException);
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="binding">数据源绑定信息对象</param>
        /// <param name="oldValue">旧数据</param>
        /// <param name="descType">要转换的类型</param>
        /// <returns>转换后的数据</returns>
        protected virtual object ConvertType(XDataBinding binding, object oldValue, Type descType)
        {
            if (string.IsNullOrEmpty(binding.FormatString) == false
                && descType.Equals(typeof(DateTime)))
            {
                DateTime dtm = DateTime.Now;
                DateTime.TryParseExact(
                    Convert.ToString(oldValue),
                    binding.FormatString,
                    null,
                    System.Globalization.DateTimeStyles.AllowWhiteSpaces,
                    out dtm);
                return dtm;
            }
            else
            {
                TypeConverter tc = TypeDescriptor.GetConverter(descType);
                if (tc != null)
                {
                    return tc.ConvertFrom(oldValue);
                }
            }
            return oldValue;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="binding">数据源绑定信息对象</param>
        /// <param name="dataSourceInstance">数据来源对象</param>
        /// <param name="throwException">发生错误时是否抛出异常</param>
        /// <returns>读取的数据</returns>
        public virtual object ReadValue(
            XDataBinding binding,
            object dataSourceInstance,
            object defaultValue,
            bool throwException)
        {
            return StdReadValue(binding, dataSourceInstance, defaultValue, throwException , this );
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="binding">数据源绑定信息对象</param>
        /// <param name="dataSourceInstance">数据来源对象</param>
        /// <param name="throwException">发生错误时是否抛出异常</param>
        /// <returns>读取的数据</returns>
        public static object StdReadValue(
            XDataBinding binding,
            object dataSourceInstance,
            object defaultValue,
            bool throwException,
            XDataBindingProvider provider )
        {
            if (dataSourceInstance == null)
            {
                if (throwException)
                {
                    throw new ArgumentNullException("dataSourceInstance");
                }
                else
                {
                    return false;
                }
            }

            XDataBindingPath path = XDataBindingPath.GetInstance(
                dataSourceInstance.GetType(),
                binding.BindingPath,
                throwException);
            if (path != null)
            {
                object currentInstance = dataSourceInstance;
                for (int iCount = 0; iCount < path.Items.Count; iCount++)
                {
                    object obj = null;
                    if (provider == null)
                    {
                        obj = StdReadItemValue(path.Items[iCount], currentInstance, null, throwException);
                    }
                    else
                    {
                        obj = provider.ReadItemValue(path.Items[iCount], currentInstance, null, throwException);
                    }
                    if (obj == null)
                    {

                    }
                    currentInstance = obj;
                }

                if (string.IsNullOrEmpty(binding.FormatString) == false)
                {
                    // 进行格式化输出
                    if (currentInstance is IFormattable)
                    {
                        currentInstance = ((IFormattable)currentInstance).ToString(binding.FormatString, null);
                    }
                }
                return currentInstance;
            }
            else
            {
                return defaultValue;
            }
        }

        public static object StdReadValue(XDataBindingPath path, object instance, object defaultValue, bool throwException , XDataBindingProvider provider )
        {
            object currentInstance = instance;
            for (int iCount = 0; iCount < path.Items.Count; iCount++)
            {
                object obj = null;
                if (provider == null)
                {
                    obj = StdReadItemValue(
                        path.Items[iCount],
                        currentInstance,
                        null,
                        throwException);
                }
                else
                {
                    obj = provider.ReadItemValue(
                         path.Items[iCount],
                         currentInstance,
                         null,
                         throwException);
                }
                if (obj == null)
                {
                    return defaultValue;
                }
                currentInstance = obj;
            }
            return currentInstance;
        }
    }
}
