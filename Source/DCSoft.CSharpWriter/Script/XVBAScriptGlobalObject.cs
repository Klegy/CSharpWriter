/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.Collections;

namespace DCSoft.Script
{
    /// <summary>
    /// script global object information
    /// </summary>
    /// <remarks>
    /// global object just like document,window,event in javascript.
    /// </remarks>
    [System.Xml.Serialization.XmlType()]
    public class XVBAScriptGlobalObject
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public XVBAScriptGlobalObject()
        {
        }

        private string strName = null;
        /// <summary>
        /// name
        /// </summary>
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

        private object objValue = null;
        /// <summary>
        /// value
        /// </summary>
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

        private Type objValueType = typeof(object);
        /// <summary>
        /// value type
        /// </summary>
        public Type ValueType
        {
            get
            {
                return objValueType;
            }
            set
            {
                objValueType = value;
            }
        }
    }

    /// <summary>
    /// global object instance list
    /// </summary>
    [System.Xml.Serialization.XmlType()]
    public class XVBAScriptGlobalObjectList : System.Collections.IEnumerable , ICloneable
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public XVBAScriptGlobalObjectList()
        {
        }

        /// <summary>
        /// get instance specify  name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>instance</returns>
        public object this[string name]
        {
            get
            {
                foreach (XVBAScriptGlobalObject item in myItems)
                {
                    if (string.Compare(item.Name, name, true) == 0)
                    {
                        return item.Value;
                    }
                }
                return null;
            }
            set
            {
                foreach (XVBAScriptGlobalObject item in myItems)
                {
                    if (string.Compare(item.Name, name, true) == 0)
                    {
                        item.Value = value;
                        if (value != null)
                        {
                            item.ValueType = value.GetType();
                        }
                        return;
                    }
                }
                XVBAScriptGlobalObject newItem = new XVBAScriptGlobalObject();
                newItem.Name = name;
                newItem.Value = value;
                if (value != null)
                {
                    newItem.ValueType = value.GetType();
                }
                myItems.Add(newItem);
            }
        }

        public void SetValue(string name, object Value, Type ValueType)
        {
            if (System.Xml.XmlReader.IsName(name) == false)
            {
                throw new ArgumentException("name");
            }

            if (ValueType == null)
            {
                throw new ArgumentNullException("ValueType");
            }
            foreach (XVBAScriptGlobalObject item in myItems)
            {
                if (string.Compare(item.Name, name, true) == 0)
                {
                    item.Value = Value;
                    item.ValueType = ValueType;
                    return;
                }
            }
            XVBAScriptGlobalObject newItem = new XVBAScriptGlobalObject();
            newItem.Name = name;
            newItem.Value = Value;
            newItem.ValueType = ValueType;
            myItems.Add(newItem);
        }

        private XVBAScriptGlobalObject GetItem(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            foreach (XVBAScriptGlobalObject item in this.myItems )
            {
                if (string.Compare(item.Name, name, true) == 0)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 删除指定名称的全局对象
        /// </summary>
        /// <param name="name">指定的名称</param>
        public void Remove(string name)
        {
            XVBAScriptGlobalObject item = GetItem(name);
            if (item != null)
            {
                myItems.Remove(item);
            }
        }

        public void Clear()
        {
            myItems.Clear();
        }
        public int Count
        {
            get
            {
                return myItems.Count;
            }
        }

        private ArrayList myItems = new ArrayList();

        public IEnumerator GetEnumerator()
        {
            return myItems.GetEnumerator();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XVBAScriptGlobalObjectList list = ( XVBAScriptGlobalObjectList) System.Activator.CreateInstance(this.GetType());
            list.myItems.Clear();
            foreach (XVBAScriptGlobalObject item in this.myItems)
            {
                XVBAScriptGlobalObject newItem = new XVBAScriptGlobalObject();
                newItem.Name = item.Name;
                newItem.Value = item.Value;
                newItem.ValueType = item.ValueType;
                list.myItems.Add(newItem);
            }
            return list;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XVBAScriptGlobalObjectList Clone()
        {
            return (XVBAScriptGlobalObjectList)((ICloneable)this).Clone();
        }

        public void CopyTo(XVBAScriptGlobalObjectList list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (myItems == null)
            {
                list.myItems = new ArrayList();
            }
            else
            {
                list.myItems = (ArrayList)this.myItems.Clone();
            }
        }
    }
}
