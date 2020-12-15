using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Dom;
using System.Windows.Forms;
using DCSoft.WinForms;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 文本输入域编辑器对象
    /// </summary>
    public class XTextInputFieldElementValueEditor : ElementValueEditor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextInputFieldElementValueEditor()
        {
        }

        public override ElementValueEditorEditStyle GetEditStyle(
            TextWindowsFormsEditorHost host,
            ElementValueEditContext context)
        {
            DomInputFieldElement field = (DomInputFieldElement)context.Element;
            InputFieldSettings settings = field.FieldSettings;
            if (settings != null)
            {
                if (settings.EditStyle == InputFieldEditStyle.Date)
                {
                    return ElementValueEditorEditStyle.DropDown;

                }
                else if (settings.EditStyle == InputFieldEditStyle.DropdownList)
                {
                    return ElementValueEditorEditStyle.DropDown;
                }
            }
            return ElementValueEditorEditStyle.None;
        }

        /// <summary>
        /// 编辑数值
        /// </summary>
        /// <param name="host">宿主对象</param>
        /// <param name="context">上下文对象</param>
        /// <returns>操作是否成功</returns>
        public override ElementValueEditResult EditValue(
            TextWindowsFormsEditorHost host,
            ElementValueEditContext context)
        {
            DomInputFieldElement field = (DomInputFieldElement)context.Element;
            InputFieldSettings settings = field.FieldSettings;
            if (settings != null)
            {
                if (settings.EditStyle == InputFieldEditStyle.Date 
                    || settings.EditStyle == InputFieldEditStyle.DateTime )
                {
                    // 编辑日期值
                    string strValue = field.InnerValue;
                    if (string.IsNullOrEmpty(strValue))
                    {
                        strValue = field.Text ;
                    }
                    DateTime oldDate = DateTime.Now;
                    DateTime newDate = DateTime.Now;
                    bool modified = false;
                    if (DateTime.TryParse(strValue, out oldDate) == false)
                    {
                        oldDate = DateTime.Now;
                    }
                    if (settings.EditStyle == InputFieldEditStyle.DateTime)
                    {
                        using (DateTimeSelectControl ctl = new DateTimeSelectControl())
                        {
                            ctl.DateTimeValue = oldDate;
                            ctl.EditorService = host;
                            host.DropDownControl(ctl);
                            modified = ctl.Modified;
                            newDate = ctl.DateTimeValue;
                        }
                    }
                    else if (settings.EditStyle == InputFieldEditStyle.Date)
                    {
                        using (MonthCalendar cld = new MonthCalendar())
                        {
                            cld.SetDate(oldDate);
                            cld.DateSelected += delegate(object sender, DateRangeEventArgs args)
                            {
                                newDate = cld.SelectionStart;
                                modified = true;
                                host.CloseDropDown();
                            };
                            cld.KeyDown += delegate(object sender, KeyEventArgs args)
                            {
                                if (args.KeyCode == Keys.Enter)
                                {
                                    // 敲回车键则关闭编辑操作
                                    modified = true;
                                    host.CloseDropDown();
                                }
                                else if (args.KeyCode == Keys.Escape)
                                {
                                    // Esc键取消操作
                                    modified = false;
                                    host.CloseDropDown();
                                }
                            };
                            host.DropDownControl(cld);
                        }//using
                    }
                    if (modified)
                    {
                        string newText = null;
                        if (field.DisplayFormat != null && field.DisplayFormat.IsEmpty == false)
                        {
                            newText = field.DisplayFormat.Execute( newDate.ToString());
                        }
                        else
                        {
                            if (settings.EditStyle == InputFieldEditStyle.Date)
                            {
                                newText = newDate.ToString("yyyy-MM-dd");
                            }
                            else if( settings.EditStyle == InputFieldEditStyle.DateTime )
                            {
                                newText = newDate.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                         
                        if (field.Text != newText)
                        {
                            field.OwnerDocument.BeginLogUndo();
                            if (field.SetEditorTextExt(
                                newText,
                                // 需要进行完整的操作许可判断，除了UserEditable之外。
                                (DomAccessFlags )( DomAccessFlags.Normal - DomAccessFlags.CheckUserEditable ) ,
                                false ) )
                            {
                                // 设置字段的InnerValue属性值
                                if (field.InnerValue != newText)
                                {
                                    if (host.Document.CanLogUndo)
                                    {
                                        host.Document.UndoList.AddProperty(
                                            "InnerValue",
                                            field.InnerValue,
                                            newText,
                                            field);
                                    }
                                    field.InnerValue = newText;
                                }
                                field.OwnerDocument.EndLogUndo();
                                return ElementValueEditResult.UserAccept ;
                            }
                            else
                            {
                                field.OwnerDocument.CancelLogUndo();
                                return ElementValueEditResult.UserCancel ;
                            }
                        }
                        //return true;
                    }
                    return ElementValueEditResult.UserCancel ;
                }
                else if (settings.EditStyle == InputFieldEditStyle.DropdownList)
                {
                    ListSourceInfo lsInfo = field.FieldSettings.ListSource;
                    if (lsInfo == null || lsInfo.IsEmpty )
                    {
                        return ElementValueEditResult.None ;
                    }
                    IListSourceProvider provider = (IListSourceProvider)host.GetService(
                        typeof(IListSourceProvider));
                    if (provider == null)
                    {
                        // 用户没有提供列表项目提供者，使用默认的项目提供者。
                        DefaultListSourceProvider p = new DefaultListSourceProvider();
                        provider = p;
                        // 没有提供列表项目提供者，无法执行操作。
                        //return false;
                    }
                    string strValue = field.InnerValue;
                    if (string.IsNullOrEmpty(strValue))
                    {
                        strValue = field.Text;
                    }
                    using (XTreeListBoxEditControl ctl = new XTreeListBoxEditControl())
                    {
                        // 用下拉列表来编辑值
                        ctl.EditorHost = host;
                        WriterAppHost ah = ( WriterAppHost ) host.GetService( typeof( WriterAppHost ));
                        ListItemCollection list = ListSourceInfo.GetRuntimeListItems(
                            ah,
                            lsInfo, 
                            provider);
                        if (list != null)
                        {
                            foreach (ListItem item in list)
                            {
                                XTreeListBoxItem newItem = new XTreeListBoxItem();
                                newItem.Text = item.Text;
                                newItem.Value = item.Value;
                                newItem.Tag = item.Tag ;
                                newItem.HasItems = false;
                                ctl.ListBox.Items.Add(newItem);
                            }
                        }
                        ctl.ListBox.ShowExpendHandleRect = false;
                        ctl.ListBox.ShowEditItemButton = false;
                        ctl.ListBox.ShowShortCut = false;
                        ctl.ListBox.MultiSelect = settings.MultiSelect;
                        ctl.ListBox.SelectedValue = strValue;
                        ctl.ListBox.SelectionModified = false;
                        ctl.ListBox.MouseMoveChangeSelectedIndex = false;
                        ctl.Modified = false;
                        if (ctl.ShowDropDown())
                        {
                            // 用户选择了操作
                            string newText = ctl.ListBox.SelectedText;
                            string newValue = ctl.ListBox.SelectedValue;
                            if (field.Text != newText)
                            {
                                host.Document.BeginLogUndo();
                                if (field.SetEditorTextExt(
                                    newText,
                                    // 需要进行完整的操作许可判断，除了UserEditable之外。
                                    (DomAccessFlags)(DomAccessFlags.Normal - DomAccessFlags.CheckUserEditable),
                                    false ))
                                {
                                    if (field.InnerValue != newValue)
                                    {
                                        // 设置字段的InnerValue属性值
                                        if (host.Document.CanLogUndo)
                                        {
                                            host.Document.UndoList.AddProperty(
                                                "InnerValue",
                                                field.InnerValue,
                                                newValue,
                                                field);
                                        }
                                        field.InnerValue = newValue;
                                    }
                                    host.Document.EndLogUndo();
                                    return ElementValueEditResult.UserAccept ;
                                }
                                else
                                {
                                    host.Document.CancelLogUndo();
                                    return ElementValueEditResult.UserCancel ;
                                }
                            }
                            return ElementValueEditResult.UserCancel ;
                        }
                    }
                }
            }
            return ElementValueEditResult.None ;
        }
    }
}
