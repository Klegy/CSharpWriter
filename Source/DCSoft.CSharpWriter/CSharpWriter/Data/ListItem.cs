using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 列表项目
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class ListItem : ICloneable
    {
        private string _Text = null;
        /// <summary>
        /// 列表文本
        /// </summary>
        [DefaultValue(null)]
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private string _Value = null;
        /// <summary>
        /// 列表项目值
        /// </summary>
        [DefaultValue(null)]
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

        private string _Tag = null;
        /// <summary>
        /// 附加数据
        /// </summary>
        [DefaultValue(null)]
        public string Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }
        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return "Text:" + this.Text + " Value:" + this.Value;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ListItem Clone()
        {
            return (ListItem)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 列表项目列表
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class ListItemCollection : List<ListItem> , ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ListItemCollection()
        {
        }

        /// <summary>
        /// 数值转换为文本
        /// </summary>
        /// <param name="Value">数值</param>
        /// <returns>文本</returns>
        public string ValueToText(string Value)
        {
            foreach ( ListItem item in this)
            {
                if (item.Value == Value)
                {
                    return item.Text;
                }
            }
            return null;
        }
        /// <summary>
        /// 文本转换为数值
        /// </summary>
        /// <param name="Text">文本</param>
        /// <returns>数值</returns>
        public string TextToValue(string Text)
        {
            foreach ( ListItem item in this)
            {
                if (item.Text == Text)
                {
                    return item.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ListItemCollection Clone()
        {
            return ( ListItemCollection ) ( ( ICloneable ) this ).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            ListItemCollection list = new ListItemCollection();
            foreach (ListItem item in this)
            {
                list.Add(item.Clone());
            }
            return list;
        }
    }
}
