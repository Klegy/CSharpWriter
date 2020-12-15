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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 扩展属性对象
    /// </summary>
    [Serializable]
    public class DomAttribute : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomAttribute()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public DomAttribute(string name, string Value)
        {
            _Name = name;
            _Value = Value;
        }
        private string _Name = null;
        /// <summary>
        /// 属性名
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
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

        private string _Value = null;
        /// <summary>
        /// 属性值
        /// </summary>
        [DefaultValue( null)]
        public string Value
        {
            get
            {
                return _Value; 
            }
            set
            {
                _Value = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DomAttribute Clone()
        {
            return (DomAttribute)((ICloneable)this).Clone();
        }
    }

    /// <summary>
    /// 属性列表
    /// </summary>
    [Serializable ]
    public class XAttributeList : List<DomAttribute> , ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XAttributeList()
        {
        }

        /// <summary>
        /// 获得指定名称的属性对象
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public DomAttribute this[string name]
        {
            get
            {
                foreach (DomAttribute item in this)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 判断是否存在指定名称的属性
        /// </summary>
        /// <param name="name">指定的属性名</param>
        /// <returns>是否存在指定的属性</returns>
        public bool Contains(string name)
        {
            return this[name] != null;
        }

        /// <summary>
        /// 删除指定名称的属性
        /// </summary>
        /// <param name="name">指定的属性名</param>
        public void Remove(string name)
        {
            DomAttribute item = this[name];
            if (item != null)
            {
                this.Remove(item);
            }
        }
        /// <summary>
        /// 获得指定名称的属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public string GetValue(string name)
        {
            DomAttribute item = this[name];
            if (item == null)
            {
                return null;
            }
            else
            {
                return item.Value;
            }
        }

        /// <summary>
        /// 设置指定名称的属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="Value">属性值</param>
        public void SetValue(string name, string Value)
        {
            DomAttribute item = this[name];
            if (item == null)
            {
                item = new DomAttribute(name, Value);
                this.Add(item);
            }
            else
            {
                item.Value = Value;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            XAttributeList list = new XAttributeList();
            foreach (DomAttribute item in this)
            {
                list.Add(item.Clone());
            }
            return list;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public XAttributeList Clone()
        {
            return (XAttributeList)((ICloneable)this).Clone();
        }


        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (DomAttribute item in this)
            {
                str.Append(item.Name + "=" + item.Value);
                if (str.Length > 250)
                {
                    break;
                }
            }
            str.Insert(0, this.Count + " Items:");
            return str.ToString();
        }
    }
}
