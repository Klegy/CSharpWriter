using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 知识库条目
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class KBEntry
    {
        /// <summary>
        /// 表示空节点的知识点对象
        /// </summary>
        public static readonly KBEntry NullKBEntry = new KBEntry();

        /// <summary>
        /// 初始化对象
        /// </summary>
        public KBEntry()
        {
        }

        private string _ID = null;
        /// <summary>
        /// 编号
        /// </summary>
        [DefaultValue(null)]
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private KBEntry _Parent = null;
        /// <summary>
        /// 父节点
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public KBEntry Parent
        {
            get
            {
                return _Parent; 
            }
            set
            {
                _Parent = value; 
            }
        }

        private string _ParentID = null;
        /// <summary>
        /// 父节点编号,DCWriter并不使用本属性，主要供应用程序组织成树状结构时临时使用,本属性值不保存到文件中。
        /// </summary>
        [Browsable(false)]
        [System.Xml.Serialization.XmlIgnore]
        public string ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                _ParentID = value;
            }
        }

        [NonSerialized]
        private string _SpellCode = null;
        /// <summary>
        /// 自动生成的拼音码
        /// </summary>
        internal string SpellCode
        {
            get
            {
                if (_SpellCode == null)
                {
                    _SpellCode = DCSoft.Common.StringConvertHelper.ToChineseSpell(this.Text);
                }
                return _SpellCode;
            }
        }

        private string _Text = null;
        /// <summary>
        /// 文本值
        /// </summary>
        public string Text
        {
            get
            {
                return _Text; 
            }
            set
            {
                _Text = value;
                _SpellCode = null;
            }
        }

        private string _Value = null;
        /// <summary>
        /// 数值
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private KBEntryList _SubEntries = null;
        /// <summary>
        /// 子节点
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlArrayItem("Entry", typeof(KBEntry))]
        public KBEntryList SubEntries
        {
            get
            {
                return _SubEntries;
            }
            set
            {
                _SubEntries = value;
            }
        }

        private KBItemStyle _Style = KBItemStyle.List;
        /// <summary>
        /// 知识点样式
        /// </summary>
        [DefaultValue( KBItemStyle.List )]
        public KBItemStyle Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        private string _ListItemsSource = null;
        /// <summary>
        /// 列表项目XML定义文件来源
        /// </summary>
        [DefaultValue( null )]
        public string ListItemsSource
        {
            get
            {
                return _ListItemsSource; 
            }
            set
            {
                _ListItemsSource = value; 
            }
        }

        private ListItemCollection _ListItems = null;
        /// <summary>
        /// 列表项目
        /// </summary>
        [System.ComponentModel.DefaultValue(null)]
        [System.Xml.Serialization.XmlArrayItem("Item", typeof(ListItem))]
        public ListItemCollection ListItems
        {
            get
            {
                return _ListItems;
            }
            set
            {
                _ListItems = value;
            }
        }
    }

    /// <summary>
    /// 知识节点列表
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    public class KBEntryList : List<KBEntry>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public KBEntryList()
        {
        }

        /// <summary>
        /// 列表中最后一个元素
        /// </summary>
        public KBEntry Last
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }
    }

    /// <summary>
    /// 知识点类型
    /// </summary>
    public enum KBItemStyle
    {
        /// <summary>
        /// 参数
        /// </summary>
        Parameter,
        /// <summary>
        /// 下拉列表
        /// </summary>
        List,
        /// <summary>
        /// 模板
        /// </summary>
        Template
    }
}
