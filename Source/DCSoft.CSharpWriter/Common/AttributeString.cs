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
    /// 属性字符串列表
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    public class AttributeString : List<AttributeStringItem> , ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public AttributeString()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="listStr">字符串值</param>
        public AttributeString(string listStr)
        {
            this.Parse(listStr);
        }

        public AttributeStringItem this[string name]
        {
            get
            {
                foreach (AttributeStringItem item in this)
                {
                    if (string.Compare(item.Name, name, true) == 0)
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        public string GetValue(string name)
        {

            AttributeStringItem item = this[name];
            if (item == null)
            {
                return null;
            }
            else
            {
                return item.Value;
            }
        }

        public void SetValue(string name, string Value )
        {
            AttributeStringItem item = this[name];
            if (item == null)
            {
                item = new AttributeStringItem();
                item.Name = name;
                item.Value = Value;
                this.Add(item);
            }
            else
            {
                item.Value = Value;
            }
        }

        public bool Contains(string name)
        {
            return this[name] != null;
        }

        public void Remvoe(string name)
        {
            AttributeStringItem item = this[name];
            if (item != null)
            {
                base.Remove(item);
            }
        }

        /// <summary>
        /// 解析字符串，获得其中的数据
        /// </summary>
        /// <param name="text"></param>
        public void Parse(string text)
        {
            this.Clear();
            if (text == null || text.Length == 0)
            {
                return;
            }
            while (text.Length > 0 )
            {
                string newName = null;
                string newValue = null;
                int index = text.IndexOf(":");
                if (index > 0)
                {
                    newName = text.Substring(0, index);
                    text = text.Substring(index + 1);
                    if (text.StartsWith("'"))
                    {
                        int index2 = text.IndexOf("'", 1);
                        if (index2 < 0)
                        {
                            index2 = text.IndexOf(";");
                        }
                        if (index2 >= 0)
                        {
                            newValue = text.Substring(1, index2);
                            text = text.Substring(index2 + 1) ;
                            if (text.StartsWith("'"))
                            {
                                text = text.Substring(1);
                            }
                        }
                        else
                        {
                            newValue = text.Substring(1);
                            text = "";
                        }
                    }//if
                    else
                    {
                        int index3 = text.IndexOf(";");
                        if (index3 >= 0)
                        {
                            newValue = text.Substring(0, index3);
                            text = text.Substring(index3 + 1);
                        }
                        else
                        {
                            newValue = text;
                            text = "";
                        }
                    }
                }
                else
                {
                    newName = text.Trim();
                    text = "";
                }
                AttributeStringItem item = new AttributeStringItem();
                item.Name = newName;
                item.Value = newValue;
                this.Add(item);
            }
        }

        /// <summary>
        /// 返回表示对象内容的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (AttributeStringItem item in this)
            {
                if (str.Length > 0)
                {
                    str.Append(";");
                }
                str.Append(item.Name);
                str.Append(":");
                string txt = item.Value;
                if (txt != null && txt.Length > 0)
                {
                    if (txt.IndexOf(":") >= 0 || txt.IndexOf(";") >= 0)
                    {
                        txt = "'" + txt + "'";
                    }
                    str.Append(txt);
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            AttributeString list = new AttributeString();
            foreach (AttributeStringItem item in this)
            {
                AttributeStringItem item2 = new AttributeStringItem();
                item2.Name = item.Name;
                item2.Value = item.Value;
                list.Add(item2);
            }
            return list;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public AttributeString Clone()
        {
            return (AttributeString)((ICloneable)this).Clone();
        }

    }

    /// <summary>
    /// 属性项目
    /// </summary>
    [Serializable]
    public class AttributeStringItem
    {
        public AttributeStringItem()
        {
        }

        private string _Name = null;
        /// <summary>
        /// 属性名
        /// </summary>
        [DefaultValue(null)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Value = null;
        /// <summary>
        /// 属性值
        /// </summary>
        [DefaultValue(null)]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
