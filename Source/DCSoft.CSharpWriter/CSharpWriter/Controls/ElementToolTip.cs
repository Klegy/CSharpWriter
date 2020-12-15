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
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 元素提示文本信息对象
    /// </summary>
    public class ElementToolTip
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementToolTip()
        {
        }

        private DomElementList _Elements = new DomElementList();
        /// <summary>
        /// 元素
        /// </summary>
        public DomElementList Elements
        {
            get { return _Elements; }
            set { _Elements = value; }
        }

        private string _Title = null;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private string _Text = null;
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private ToolTipStyle _Style = ToolTipStyle.ToolTip;
        /// <summary>
        /// 样式
        /// </summary>
        public ToolTipStyle Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        private ToolTipLevel _Level = ToolTipLevel.Normal;
        /// <summary>
        /// 等级
        /// </summary>
        public ToolTipLevel Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        private bool _Disposable = false ;
        /// <summary>
        /// 一次性的提示文本
        /// </summary>
        public bool Disposable
        {
            get { return _Disposable; }
            set { _Disposable = value; }
        }
    }

    /// <summary>
    /// 提示文本信息列表
    /// </summary>
    public class ElementToolTipList : List<ElementToolTip>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementToolTipList()
        {
        }

        /// <summary>
        /// 获得指定元素的项目
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>获得的项目</returns>
        public ElementToolTip this[DomElement element]
        {
            get
            {
                if (element == null)
                {
                    return null;
                }
                foreach (ElementToolTip item in this)
                {
                    if (item.Elements.Contains(element))
                    {
                        return item;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 判断是否已经存在指定文档元素的提示信息
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>是否存在提示信息</returns>
        public bool Contains(DomElement element)
        {
            return this[element] != null;
        }

        public void Remove(DomElement element)
        {
            ElementToolTip item = this[element];
            if (item != null)
            {
                if (item.Elements.Count > 1)
                {
                    item.Elements.Remove(element);
                }
                else
                {
                    this.Remove(item);
                }
                _Version++;
            }
        }

        private int _Version = 0;
        /// <summary>
        /// 列表的内容版本号，对列表内容的所有的修改都增加该版本号
        /// </summary>
        public int Version
        {
            get { return _Version; }
        }

        public void IncreateVersion()
        {
            _Version++;
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="element">文档元素</param>
        /// <param name="text">提示文本内容</param>
        public ElementToolTip Add(DomElement element, string text)
        {
            return Add(element, text, ToolTipStyle.ToolTip, ToolTipLevel.Normal);
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="element">文档元素</param>
        /// <param name="text">提示文本内容</param>
        /// <param name="style">样式</param>
        /// <param name="level">等级</param>
        public ElementToolTip Add(DomElement element, string text, ToolTipStyle style, ToolTipLevel level)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            // 删除旧信息
            foreach (ElementToolTip item in this)
            {
                if (item.Elements.Contains(element))
                {
                    if (item.Text == text
                        && item.Style == style
                        && item.Level == level)
                    {
                        // 内容完全一样，则退出处理
                        return item ;
                    }
                    item.Elements.Remove(element);
                    if (item.Elements.Count == 0)
                    {
                        this.Remove(item);
                    }
                    _Version++;
                    break;
                }
            }

            if (text != null && text.Length > 0)
            {
                // 添加新信息
                ElementToolTip newItem = new ElementToolTip();
                newItem.Elements.Add(element);
                newItem.Text = text;
                newItem.Style = style;
                newItem.Level = level;
                this.Add(newItem);
                _Version++;
                return newItem;
            }
            return null;
        }
    }

    public enum ToolTipStyle
    {
        /// <summary>
        /// 普通提示文本，当鼠标移动到元素上就显示提示
        /// </summary>
        ToolTip  ,
        /// <summary>
        /// 静态提示文本，一直显示在用户界面上
        /// </summary>
        Static ,
        /// <summary>
        /// 右边提示文本，一直显示在用户界面的右侧
        /// </summary>
        RightSide
    }

    /// <summary>
    /// 提示文本等级
    /// </summary>
    public enum ToolTipLevel
    {
        /// <summary>
        /// 普通提示文本
        /// </summary>
        Normal ,
        /// <summary>
        /// 警告提示文本
        /// </summary>
        Warring ,
        /// <summary>
        /// 错误提示文本
        /// </summary>
        Error 
         
    }
}
     
