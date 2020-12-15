using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.CSharpWriter.Data;
using DCSoft.CSharpWriter.Dom;
using DCSoft.WinForms;
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 向编辑器插入知识点的操作对象 
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    internal class KBInserter
    {
        public KBInserter(CSWriterControl ctl)
        {
            _WriterControl = ctl;
        }
        private CSWriterControl _WriterControl = null;
        /// <summary>
        /// 编辑器控件对象
        /// </summary>
        public CSWriterControl WriterControl
        {
            get { return _WriterControl; }
            //set { _WriterControl = value; }
        }

        /// <summary>
        /// 显示插入知识库的弹出式列表
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool BeginInsertKB()
        {
            IKBProvider kp = (IKBProvider)this.WriterControl.AppHost.Services.GetService(typeof(IKBProvider));
            if (kp != null)
            {
                List<KBEntry> root = kp.GetSubEntries(this.WriterControl.AppHost , null);
                if (root != null && root.Count > 0)
                {
                    using (XTreeListBoxEditControl ctl = new XTreeListBoxEditControl())
                    {
                        ctl.EditorHost = this.WriterControl.EditorHost;
                        ctl.EditorHost.ElementInstance = this.WriterControl.CurrentElement;
                        foreach (KBEntry item in root)
                        {
                            ctl.ListBox.Items.Add(CreateTreeListBoxItem(item , false ));
                        }//foreach
                        ctl.ListBox.ShowExpendHandleRect = true;
                        ctl.ListBox.ShowEditItemButton = false;
                        ctl.ListBox.MultiSelect = false;
                        ctl.ListBox.MouseMoveChangeSelectedIndex = false;
                        ctl.ListBox.LoadChildItems += 
                            new XTreeListBoxLoadChildItemsEventHandler(ListBox_LoadChildItems);
                        ctl.ListBox.SpellTextChanged += 
                            new CancelEventHandler(ListBox_SpellTextChanged);
                        ctl.Modified = false;
                        if (ctl.ShowDropDown())
                        {
                            if (ctl.ListBox.SelectedItem != null)
                            {
                                KBEntry selItem = (KBEntry)ctl.ListBox.SelectedItem.Tag;
                                DomElementList list = kp.CreateElements(
                                    this.WriterControl.AppHost ,
                                    this.WriterControl.Document,
                                    selItem);
                                if (list != null && list.Count > 0)
                                {
                                    this.WriterControl.DocumentControler.InsertElements(list);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ListBox_SpellTextChanged(object sender, CancelEventArgs e)
        {
            XTreeListBox list = (XTreeListBox)sender;
            string text = list.SpellText;
            KBLibrary lib = (KBLibrary)this.WriterControl.AppHost.Services.GetService(typeof(KBLibrary));
            if (lib == null)
            {
                e.Cancel = false;
            }
            else
            {
                KBEntryList entries = lib.SearchKBEntries(list.SpellText);
                if (entries != null && entries.Count > 0)
                {
                    list.BeginUpdate();
                    list.Items.Clear();
                    foreach (KBEntry item in entries)
                    {
                        XTreeListBoxItem xitem = CreateTreeListBoxItem(item , true );
                        list.Items.Add(xitem);
                    }
                    list.ShowExpendHandleRect = string.IsNullOrEmpty(list.SpellText);
                    list.EndUpdate();
                    TextWindowsFormsEditorHost whost = this.WriterControl.EditorHost;
                    whost.UpdateDropDownControlSize();
                    list.SelectedIndex = -1;
                    list.SelectedIndex = 0;
                    //list.SelectionModified = false;
                    if (list.Parent is XTreeListBoxEditControl)
                    {
                        XTreeListBoxEditControl ctl2 = (XTreeListBoxEditControl)list.Parent;
                        //ctl2.Modified = false;
                    }
                    e.Cancel = true;
                }
            }
        }

        private void ListBox_LoadChildItems(object sender, XTreeListBoxLoadChildItemsEventArgs args)
        {
            IKBProvider kp = (IKBProvider)this.WriterControl.AppHost.Services.GetService(typeof(IKBProvider));
            if (kp != null)
            {
                List<KBEntry> items = kp.GetSubEntries(
                    this.WriterControl.AppHost ,
                    (KBEntry)args.ParentItem.Tag);
                if (items != null && items.Count > 0)
                {
                    foreach (KBEntry item in items)
                    {
                        args.ChildItems.Add(CreateTreeListBoxItem(item , false ));
                    }
                }
            }
        }

        private XTreeListBoxItem CreateTreeListBoxItem(KBEntry entry , bool addParentText )
        {
            XTreeListBoxItem item = new XTreeListBoxItem();
            if (addParentText)
            {
                StringBuilder str = new StringBuilder();
                KBEntry kb = entry.Parent;
                while (kb != null)
                {
                    if (str.Length > 0)
                    {
                        str.Insert( 0 , "\\");
                    }
                    str.Insert( 0 , kb.Text);
                    kb = kb.Parent;
                }
                if (str.Length > 0)
                {
                    item.Text = entry.Text + " - " + str.ToString()  ;
                }
                else
                {
                    item.Text = entry.Text;
                }
            }
            else
            {
                item.Text = entry.Text;
            }
            item.Value = entry.Value;
            item.Tag = entry;
            if (entry == KBEntry.NullKBEntry)
            {
                item.Style = XTreeListBoxItemStyle.Spliter;
                return item;
            }
            item.HasItems = entry.SubEntries != null && entry.SubEntries.Count > 0;
            if (entry.Style == KBItemStyle.Template)
            {
                item.Icon = WriterResources.KBTemplate;
                item.HasItems = false;
            }
            else
            {
                if (item.HasItems == false)
                {
                    item.Icon = WriterResources.KBBlankEntry;
                }
                else
                {
                    item.Icon = WriterResources.KBListEntry;
                }
            }
            return item;
        }
    }
}
