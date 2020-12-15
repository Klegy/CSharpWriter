using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DCSoft.CSharpWriter.Dom;
using System.Data;
using System.Collections;

namespace DCSoft.CSharpWriter.Data
{
    /// <summary>
    /// 列表数据源信息
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [Serializable]
    [TypeConverter(typeof(CommonTypeConverter))]
    [System.ComponentModel.Editor(
        typeof( ListSourceInfoEditor ) ,
        typeof( System.Drawing.Design.UITypeEditor ))]
    public sealed class ListSourceInfo : ICloneable
    {
        #region 静态成员 ****************************************

        private static string[] _SupportSourceNames = null;
        /// <summary>
        /// 设计器支持的数据源名称数组
        /// </summary>
        public static string[] SupportSourceNames
        {
            get
            {
                return _SupportSourceNames; 
            }
            set
            {
                _SupportSourceNames = value; 
            }
        }

        public static ListItemCollection GetRuntimeListItems(
            WriterAppHost host ,
            ListSourceInfo info,
            IListSourceProvider provider)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (info.BufferItems && info.RuntimeItems != null)
            {
                return info.RuntimeItems;
            }
            ListItemCollection result = null;
            if (provider == null)
            {
                result = info.Items;
            }
            else if (info.Items != null && info.Items.Count > 0)
            {
                result = info.Items;
            }
            else
            {
                result = new ListItemCollection();
                IEnumerable enums = null;
                object ins = provider.GetListSource(host,info);
                if (ins is IEnumerable)
                {
                    enums = (IEnumerable)ins;
                }
                else if (ins is IListSource)
                {
                    enums = ((IListSource)ins).GetList();
                }
                else if (ins is System.Xml.XmlNode)
                {
                    enums = ((System.Xml.XmlNode)ins).ChildNodes;
                }
                if (enums != null)
                {
                    foreach (object obj in enums)
                    {
                        if (obj is ListItem)
                        {
                            result.Add((ListItem)obj);
                        }
                        else
                        {
                            ListItem newItem = new ListItem();
                            newItem.Text = provider.GetDisplayText(host,obj, info);
                            newItem.Value = provider.GetValue(host,obj, info);
                            newItem.Tag = provider.GetTag(host,obj, info);
                            result.Add(newItem);
                        }
                    }
                }
            }
            if (info.BufferItems)
            {
                info.RuntimeItems = result;
            }
            if (host.Debuger != null)
            {
                host.Debuger.WriteLine(string.Format(
                    WriterStrings.LoadListItems_ProviderType_Name_Num,
                    provider.GetType().Name,
                    info.SourceName ,
                    result == null ? "NULL" : result.Count.ToString()));
            }
            return result;
        }

        public static string StdGetDisplayText(
            object Value,
            ListSourceInfo info ,
            XDataBindingProvider bindingProvider )
        {
            return StdGetFieldValue(Value, info, 0, bindingProvider);
        }

        public static string StdGetValue(
            object Value,
            ListSourceInfo info ,
            XDataBindingProvider bindingProvider )
        {
            return StdGetFieldValue(Value, info, 1, bindingProvider);
        }

        public static string StdGetTag(
            object Value,
            ListSourceInfo info,
            XDataBindingProvider bindingProvider)
        {
            if (Value is ListItem)
            {
                ListItem item = (ListItem)Value;
                return item.Tag;
            }
            return null;
        }

        private static string StdGetFieldValue(
            object Value,
            ListSourceInfo info ,
            int type ,
            XDataBindingProvider bindingProvider)
        {
            if (Value == null || DBNull.Value.Equals(Value))
            {
                return null;
            }
            string strPath = null;
            if (type == 0)
            {
                strPath = info.DisplayPath;
            }
            else if (type == 1)
            {
                strPath = info.ValuePath;
            }
            if (Value is ListItem)
            {
                ListItem item = (ListItem)Value;
                if (type == 0)
                {
                    if (string.IsNullOrEmpty(info.DisplayPath))
                    {
                        return item.Text;
                    }
                }
                else if (type == 1)
                {
                    if (string.IsNullOrEmpty(info.ValuePath))
                    {
                        return item.Value;
                    }
                }
            }
            if (string.IsNullOrEmpty( strPath ))
            {
                object v = null;
                if (Value is IList)
                {
                    IList il = (IList)Value;
                    if (il.Count > type )
                    {
                        v = il[type];
                    }
                    else
                    {
                        v = il[0];
                    }
                    return Convert.ToString(v);
                }
                else if (Value is IDataRecord)
                {
                    IDataRecord record = (IDataRecord)Value;
                    if (record.FieldCount > type)
                    {
                        v = record.GetValue(type);
                    }
                    else
                    {
                        v = record.GetValue(0);
                    }
                }
                else if (Value is DataRow)
                {
                    DataRow row = (DataRow)Value;
                    if (row.Table.Columns.Count > type)
                    {
                        v = row[type];
                    }
                    else
                    {
                        v = row[0];
                    }
                }
                else if (Value is DataRowView)
                {
                    DataRowView row = (DataRowView)Value;
                    if (row.Row.Table.Columns.Count > type)
                    {
                        v = row[type];
                    }
                    else
                    {
                        v = row[0];
                    }
                }
                else
                {
                    v = Value;
                }
                if (v == null || DBNull.Value.Equals(v))
                {
                    return null;
                }
                else
                {
                    return Convert.ToString(v);
                }
            }

            XDataBindingPath path = XDataBindingPath.GetInstance(
                Value.GetType(),
                type == 0 ? info.DisplayPath : info.ValuePath,
                false);
            if (path == null)
            {
                return null;
            }
            else
            {
                object v = Value;
                v = XDataBindingProvider.StdReadValue(path, Value, null, false, bindingProvider );
                if (v == null || DBNull.Value.Equals(v))
                {
                    return null;
                }
                else
                {
                    return Convert.ToString(v);
                }
            }
        }

        #endregion

        /// <summary>
        /// 初始化对象
        /// </summary>
        public ListSourceInfo()
        {
        }
    
        /// <summary>
        /// 对象内容是否为空
        /// </summary>
        [Browsable( false )]
        public bool IsEmpty
        {
            get
            {
                if( this.Items != null && this.Items.Count > 0 )
                {
                    return false ;
                }
                return string.IsNullOrEmpty(this.Name)
                    && string.IsNullOrEmpty(this.SourceName);
                   
            }
        }

        private string _Name = null;
        /// <summary>
        /// 列表数据源名称
        /// </summary>
        [DefaultValue( null)]
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

        private string _SourceName = null;
        /// <summary>
        /// 来源名称
        /// </summary>
        [DefaultValue( null )]
        public string SourceName
        {
            get
            {
                return _SourceName; 
            }
            set
            {
                _SourceName = value; 
            }
        }

        private string _FormatString = null;
        /// <summary>
        /// 格式化字符串
        /// </summary>
        [DefaultValue( null )]
        public string FormatString
        {
            get
            {
                return _FormatString; 
            }
            set
            {
                _FormatString = value;
                if (string.IsNullOrEmpty(_FormatString))
                {
                    _FormatString = null;
                }

            }
        }

        private string _DisplayPath = null;
        /// <summary>
        /// 显示的文本数据路径
        /// </summary>
        [DefaultValue( null )]
        public string DisplayPath
        {
            get
            {
                return _DisplayPath; 
            }
            set
            {
                _DisplayPath = value; 
            }
        }

        private string _ValuePath = null;
        /// <summary>
        /// 后台数据的路径
        /// </summary>
        [DefaultValue( null )]
        public string ValuePath
        {
            get
            {
                return _ValuePath; 
            }
            set
            {
                _ValuePath = value; 
            }
        }

        private ListItemCollection _Items = null;
        /// <summary>
        /// 内置的列表项目
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("Item" , typeof( ListItem ))]
        public ListItemCollection Items
        {
            get
            {
                return _Items; 
            }
            set
            {
                _Items = value; 
            }
        }

        private bool _BufferItems = true;
        /// <summary>
        /// 是否缓存列表项目
        /// </summary>
        [DefaultValue( true )]
        public bool BufferItems
        {
            get
            {
                return _BufferItems; 
            }
            set
            {
                _BufferItems = value; 
            }
        }

        [NonSerialized]
        private ListItemCollection _RuntimeItems = null;
        /// <summary>
        /// 实际运行中的列表项目集合,只有当BufferItems=true时该属性才可能有内容。
        /// </summary>
        [Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public ListItemCollection RuntimeItems
        {
            get
            {
                return _RuntimeItems; 
            }
            set
            {
                _RuntimeItems = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public ListSourceInfo Clone()
        {
            return (ListSourceInfo)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            ListSourceInfo info = ( ListSourceInfo ) this.MemberwiseClone();
            if (this._Items != null)
            {
                info._Items = new ListItemCollection();
                foreach (ListItem item in this._Items)
                {
                    info._Items.Add(item.Clone());
                }
            }
            return info;
        }

        public override string ToString()
        {
            if (this.Items != null && this.Items.Count > 0)
            {
                return this.Items.Count + " items";
            }
            else
            {
                return this.SourceName;
            }
        }
    }

    public class ListSourceInfoEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (dlgListSourceInfo dlg = new dlgListSourceInfo())
            {
                ListSourceInfo list = value as ListSourceInfo;
                if (list == null)
                {
                    list = new ListSourceInfo();
                }
                else
                {
                    list = list.Clone();
                }
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    value = list;
                }
            }
            return value;
        }
    }
}