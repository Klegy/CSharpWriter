using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 输入设置
    /// </summary>
    [Serializable]
    [TypeConverter( typeof( CommonTypeConverter ))]
    public class InputFieldSettings : ICloneable
    {
        private static string[] _SupportCustomListSourceNames = null;
        /// <summary>
        /// 支持的自定义列表来源名称
        /// </summary>
        public static string[] SupportCustomListSourceNames
        {
            get
            {
                return _SupportCustomListSourceNames; 
            }
            set
            {
                _SupportCustomListSourceNames = value; 
            }
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        public InputFieldSettings()
        {
        }
         
        //private bool _UserEditable = true;
        ///// <summary>
        ///// 用户可以直接修改文本域中的内容
        ///// </summary>
        //[System.ComponentModel.DefaultValue(true)]
        //public bool UserEditable
        //{
        //    get
        //    {
        //        return _UserEditable;
        //    }
        //    set
        //    {
        //        _UserEditable = value;
        //    }
        //}

        private InputFieldEditStyle _EditStyle = InputFieldEditStyle.Text;
        /// <summary>
        /// 输入方式
        /// </summary>
        [DefaultValue( InputFieldEditStyle.Text )]
        public InputFieldEditStyle EditStyle
        {
            get
            {
                return _EditStyle; 
            }
            set
            {
                _EditStyle = value; 
            }
        }
         

        private bool _MultiSelect = false;
        /// <summary>
        /// 允许多选列表项目
        /// </summary>
        [DefaultValue( false )]
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect; 
            }
            set
            { 
                _MultiSelect = value; 
            }
        }

        private ListSourceInfo _ListSource = null;
        /// <summary>
        /// 列表内容来源
        /// </summary>
        [DefaultValue( null )]
        public ListSourceInfo ListSource
        {
            get 
            {
                return _ListSource; 
            }
            set
            {
                _ListSource = value; 
            }
        }

        [NonSerialized]
        private bool _EnableCustomListItems = true;
        /// <summary>
        /// 是否允许使用自定义列表项目,已过时，不再使用。
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
        [System.Obsolete("本属性仅为保留向前兼容性，不再使用！！！")]
        public bool EnableCustomListItems
        {
            get
            {
                return _EnableCustomListItems;
            }
            set
            {
                _EnableCustomListItems = value;
            }
        }
         

        ////private bool _HasReadCustomListitems = false;
        /////// <summary>
        /////// 已经读取了自定义的列表元素
        /////// </summary>
        ////[System.ComponentModel.Browsable(false)]
        ////[System.Xml.Serialization.XmlIgnore]
        ////public bool HasReadCustomListitems
        ////{
        ////    get
        ////    {
        ////        return _HasReadCustomListitems; 
        ////    }
        ////    set
        ////    {
        ////        _HasReadCustomListitems = value; 
        ////    }
        ////}

        [NonSerialized]
        private string _CustomListSource = null;
        /// <summary>
        /// 自定义的下列表数据源的名称,已过时，不再使用。
        /// </summary>
        [DefaultValue(null)]
        [System.Obsolete("本属性仅为保留向前兼容性，不再使用！！！")]
        public string CustomListSource
        {
            get { return _CustomListSource; }
            set { _CustomListSource = value; }
        }

        [NonSerialized]
        private InputFieldListItemList _ListItems = new InputFieldListItemList();
        /// <summary>
        /// 列表项目,已过时，不再使用。
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("Item", typeof(InputFieldListItem))]
        [DefaultValue( null)]
        //[System.Obsolete("本属性仅为保留向前兼容性，不再使用！！！")]
        public InputFieldListItemList ListItems
        {
            get
            {
                if (_ListItems == null)
                {
                    _ListItems = new InputFieldListItemList();
                }
                return _ListItems;
            }
            set
            {
                _ListItems = value;
            }
        }

        /// <summary>
        /// 为了程序兼容性而修正相关的设置
        /// </summary>
        public void FixListSourceSettings()
        {
            if (_ListSource == null)
            {
                _ListSource = new ListSourceInfo();
            }
            if (string.IsNullOrEmpty(this.CustomListSource) == false )
            {
                this.ListSource.SourceName = this.CustomListSource;
            }
            this.CustomListSource = null;
            if (this.ListItems != null && this.ListItems.Count > 0)
            {
                if (this.ListSource.Items == null)
                {
                    this.ListSource.Items = new ListItemCollection();
                }
                foreach (InputFieldListItem item in this.ListItems)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = item.Text;
                    newItem.Value = item.Value;
                    newItem.Tag = item.Tag;
                    this.ListSource.Items.Add(newItem);
                }//foreach
            }
            this.ListItems = null;
            if (this._ListSource.IsEmpty)
            {
                this._ListSource = null;
            }
        }
         
        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public InputFieldSettings Clone()
        {
            return (InputFieldSettings)((ICloneable)this).Clone();
        }
    

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            InputFieldSettings instance = (InputFieldSettings)this.MemberwiseClone();
            if (this._ListSource != null)
            {
                instance._ListSource = this._ListSource.Clone();
            }
            
            return instance;
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            if (this.EditStyle == InputFieldEditStyle.Text)
            {
                return "Text";
            }
            else if (this.EditStyle == InputFieldEditStyle.DropdownList)
            {
                if (this.ListSource == null)
                {
                    return "None list item";
                }
                else
                {
                    return "List:" + this.ListSource.ToString();
                }
            }
            else if (this.EditStyle == InputFieldEditStyle.Date)
            {
                return "DateTime ";
            }
            return "";
        }
    }

    /// <summary>
    /// 输入域设置信息提供者接口
    /// </summary>
    public interface IInputFieldSettingsProvider
    {
        /// <summary>
        /// 获得指定编号的输入域设置信息对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>获得的设置信息对象</returns>
        InputFieldSettings GetSettings(string id);
        /// <summary>
        /// 将指定的对象转换为输入域信息对象
        /// </summary>
        /// <param name="sourceValue">原始对象</param>
        /// <returns>转换后的输入域信息对象</returns>
        InputFieldSettings Convert(object sourceValue);
    }

    /// <summary>
    /// 输入域列表项目
    /// </summary>
    [Serializable]
    [System.Obsolete]
    [System.Xml.Serialization.XmlType]
    public class InputFieldListItem
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public InputFieldListItem()
        {
        }

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

    }


    /// <summary>
    /// 下列列表项目列表
    /// </summary>
    [Serializable]
    [System.Obsolete]
    [System.Xml.Serialization.XmlType]
    public class InputFieldListItemList : List<InputFieldListItem>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public InputFieldListItemList()
        {
        }
         
    }

    /// <summary>
    /// 文本输入域输入方式
    /// </summary>
    public enum InputFieldEditStyle
    {
        /// <summary>
        /// 直接输入纯文本
        /// </summary>
        Text ,
        /// <summary>
        /// 下拉列表方式
        /// </summary>
        DropdownList ,
        /// <summary>
        /// 日期类型
        /// </summary>
        Date,
        /// <summary>
        /// 时间日期类型
        /// </summary>
        DateTime ,
        DateTimeWithoutSecond,
        /// <summary>
        /// 时间类型
        /// </summary>
        Time
        ///// <summary>
        ///// 对话框模式
        ///// </summary>
        //Dialog
    }
}
