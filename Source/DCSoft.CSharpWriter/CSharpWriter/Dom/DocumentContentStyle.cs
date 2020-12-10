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
using DCSoft.Common;
using DCSoft.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档样式信息对象
    /// </summary>
    [Serializable()]
    public class DocumentContentStyle : ContentStyle 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentContentStyle()
        {
            //this.FontSize = 10.5f;
        }

        /// <summary>
        /// 获得采用默认值的属性名称列表
        /// </summary>
        /// <returns>属性名称列表</returns>
        public string GetDefaultValuePropertyNames( )
        {
            List<string> result = null;
            foreach (XDependencyProperty p in this.InnerValues.Keys)
            {
                if (p != _DeleterIndex && p != _CreatorIndex)
                {
                    if (p.EqualsDefaultValue(this.InnerValues[p]))
                    {
                        if (result == null)
                        {
                            result = new List<string>();
                        }
                        result.Add(p.Name);
                    }
                }
            }
            if (result != null)
            {
                result.Sort();
                StringBuilder str = new StringBuilder();
                foreach (string name in result)
                {
                    if (str.Length > 0)
                    {
                        str.Append(",");
                    }
                    str.Append(name);
                }
                return str.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 宽度值
        /// </summary>
        [NonSerialized]
        internal float WidthValue = -1;
        /// <summary>
        /// 高度值
        /// </summary>
        [NonSerialized]
        internal float HeightValue = -1;
 
        private static XDependencyProperty _CreatorIndex = XDependencyProperty.Register(
            "CreatorIndex",
            typeof(int),
            typeof(DocumentContentStyle),
            -1);
        /// <summary>
        /// 创建元素的用户信息编号
        /// </summary>
        [System.ComponentModel.DefaultValue(-1)]
        public int CreatorIndex
        {
            get
            {
                return (int)GetValue(_CreatorIndex);
            }
            set
            {
                SetValue(_CreatorIndex, value);
            }
        }

        private static XDependencyProperty _DeleterIndex = XDependencyProperty.Register(
            "DeleterIndex",
            typeof(int),
            typeof(DocumentContentStyle),
            -1);
        /// <summary>
        /// 删除元素的用户信息编号
        /// </summary>
        [System.ComponentModel.DefaultValue(-1)]
        public int DeleterIndex
        {
            get
            {
                return (int)GetValue(_DeleterIndex);
            }
            set
            {
                SetValue(_DeleterIndex, value);
            }
        }

        private static XDependencyProperty _Name = XDependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(DocumentContentStyle),
            null);
        /// <summary>
        /// 对象名称
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Name
        {
            get
            {
                return (string)GetValue(_Name);
            }
            set
            {
                SetValue(_Name, value);
            }
        }

        private static XDependencyProperty _Link = XDependencyProperty.Register(
            "Link",
            typeof(string),
            typeof(DocumentContentStyle),
            null);
        /// <summary>
        /// 超链接地址
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        public string Link
        {
            get
            {
                return (string)GetValue(_Link);
            }
            set
            {
                SetValue(_Link, value);
            }
        }

        [NonSerialized()]
        private int _ReferenceCount = 0;
        /// <summary>
        /// 被引用的次数
        /// </summary>
        [Browsable( false )]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public int ReferenceCount
        {
            get
            {
                return _ReferenceCount; 
            }
            set
            {
                _ReferenceCount = value; 
            }
        }

        [NonSerialized()]
        private float _TabWidth = 100f;
        /// <summary>
        /// 制表宽度
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public float TabWidth
        {
            get 
            {
                return _TabWidth; 
            }
        }

        [NonSerialized()]
        private float _DefaultLineHeight = 0f;
        /// <summary>
        /// 默认行高
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        public float DefaultLineHeight
        {
            get
            {
                return _DefaultLineHeight;
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        public void UpdateState(System.Drawing.Graphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            System.Drawing.SizeF size = g.MeasureString(
                "____",
                this.Font.Value,
                10000,
                System.Drawing.StringFormat.GenericTypographic);
            // 计算默认制表宽度
            _TabWidth = (int)Math.Ceiling(size.Width);
            // 计算默认行高
            _DefaultLineHeight =  
                this.Font.Value.GetHeight(g);
        }


        /// <summary>
        /// 删除与默认样式相同的项目，只保留不同的项目
        /// </summary>
        /// <param name="defaultStyle">默认样式对象</param>
        /// <returns>经过操作后本对象剩余的项目</returns>
        public int RemoveSameStyle(DocumentContentStyle defaultStyle)
        {
            if (defaultStyle == null)
            {
                throw new ArgumentNullException("defaultStyle");
            }

            foreach (XDependencyProperty p in defaultStyle.InnerValues.Keys )
            {
                if (this.InnerValues.ContainsKey(p))
                {
                    object v1 = defaultStyle.InnerValues[p];
                    object v2 = this.InnerValues[p];
                    if (v1 == v2)
                    {
                        this.InnerValues.Remove(p);
                    }
                }
            }
            return this.InnerValues.Count; 
        }

    }


}
