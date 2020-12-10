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

namespace DCSoft.Common
{
    /// <summary>
    /// 基于列表方式的字典类型
    /// </summary>
    /// <remarks>本字典内部采用列表方式来实现，速度慢，但能保持关键字的添加顺序。
    /// 编制 袁永福</remarks>
    [Serializable()]
    public class ListDictionary<TKey , TValue >
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ListDictionary()
        {
        }

        private List<ListItem<TKey, TValue>> _Items = new List<ListItem<TKey, TValue>>();

        private class ListItem<TKey2, TValue2>
        {
            public TKey2 _Key = default( TKey2 );
            public TValue2 _Value = default(TValue2 );
        }
        /// <summary>
        /// 清空字典
        /// </summary>
        public void Clear()
        {
            _Items.Clear();
        }
        /// <summary>
        /// 字典中项目的个数
        /// </summary>
        public int Count
        {
            get
            {
                return _Items.Count;
            }
        }
        /// <summary>
        /// 获得所有的键值
        /// </summary>
        public List<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (ListItem<TKey, TValue> item in _Items)
                {
                    keys.Add(item._Key);
                }
                return keys;
            }
        }
        /// <summary>
        /// 获得所有的数值
        /// </summary>
        public List<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                foreach (ListItem<TKey, TValue> item in _Items)
                {
                    values.Add(item._Value);
                }
                return values;
            }
        }

        private ListItem<TKey, TValue> GetItem(TKey keyValue)
        {
            foreach (ListItem<TKey, TValue> item in _Items)
            {
                if (item._Key.Equals(keyValue))
                {
                    return item ;
                }
            }
            return null;
        }
        /// <summary>
        /// 删除指定的键值
        /// </summary>
        /// <param name="keyValue"></param>
        public void Remove(TKey keyValue)
        {
            ListItem<TKey, TValue> item = GetItem(keyValue);
            if (item != null)
            {
                _Items.Remove(item);
            }
        }
        /// <summary>
        /// 判断是否存在指定的键值
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <returns>是否存在指定的键值</returns>
        public bool ContainsKey(TKey keyValue)
        {
            return GetItem(keyValue) != null;
        }

        /// <summary>
        /// 设置/获得指定的键值对应的数值
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <returns>数值</returns>
        public TValue this[TKey keyValue]
        {
            get
            {
                if (keyValue == null)
                {
                    throw new ArgumentNullException("keyValue");
                }
                ListItem<TKey, TValue> item = GetItem(keyValue);
                if (item == null)
                {
                    return default(TValue);
                }
                else
                {
                    return item._Value;
                }
            }
            set
            {
                ListItem<TKey, TValue> item = GetItem(keyValue);
                if (item == null)
                {
                    item = new ListItem<TKey, TValue>();
                    item._Key = keyValue;
                    item._Value = value;
                    _Items.Add(item);
                }
                else
                {
                    item._Value = value;
                }
            }
        }

    }
}
