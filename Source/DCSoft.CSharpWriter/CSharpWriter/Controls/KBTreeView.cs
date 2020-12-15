using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using DCSoft.CSharpWriter.Data ;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 知识库树状列表
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    [System.ComponentModel.ToolboxItem( true )]
    [System.Runtime.InteropServices.ComVisible(true )]
    public class KBTreeView : TreeView
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public KBTreeView()
        {
        }

        private bool _AllowDragContent = true;
        /// <summary>
        /// 允许拖拽知识节点内容
        /// </summary>
        [DefaultValue( true )]
        public bool AllowDragContent
        {
            get 
            {
                return _AllowDragContent; 
            }
            set
            {
                _AllowDragContent = value; 
            }
        }

        private KBLibrary _KBLibrary = null;
        /// <summary>
        /// 使用的知识库对象
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
        public KBLibrary KBLibrary
        {
            get
            {
                return _KBLibrary; 
            }
            set
            {
                _KBLibrary = value; 
            }
        }

        /// <summary>
        /// 加载知识库
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool LoadKBLibrary(string url)
        {
            KBLibrary lib = new KBLibrary();
            UrlStream stream = UrlStream.Open(url);
            if (stream != null)
            {
                using (stream)
                {
                    lib.Load(stream);
                    lib.BaseURL = WriterUtils.GetBaseURL(url);
                    this.KBLibrary = lib;
                    RefreshView();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <returns></returns>
        public bool RefreshView()
        {
            if (this.ImageList == null)
            {
                this.ImageList = new ImageList();
                this.ImageList.Images.Add(WriterResources.KBListEntry);
                this.ImageList.Images.Add(WriterResources.KBTemplate);
                this.ImageList.Images.Add(WriterResources.KBBlankEntry);
            }
            if (this.KBLibrary != null)
            {
                this.BeginUpdate();
                if (this.KBLibrary.KBEntries != null)
                {
                    FillNodes(this.KBLibrary.KBEntries, this.Nodes);
                }
                this.EndUpdate();
            }
            return true ;
        }

        private void FillNodes(KBEntryList entries, TreeNodeCollection nodes)
        {
            foreach (KBEntry entry in entries)
            {
                TreeNode node = new TreeNode();
                node.Name = entry.ID;
                node.Text = entry.Text;
                node.Tag = entry;
                if (entry.Style == KBItemStyle.Template)
                {
                    node.ImageIndex = 1;
                }
                else if (entry.SubEntries != null && entry.SubEntries.Count > 0)
                {
                    node.ImageIndex = 0;
                    FillNodes(entry.SubEntries, node.Nodes);
                }
                else
                {
                    node.ImageIndex = 2;
                }
                node.SelectedImageIndex = node.ImageIndex;
                nodes.Add(node);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.AllowDragContent)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    TreeViewHitTestInfo info = this.HitTest(e.X, e.Y);

                    if ((info.Location == TreeViewHitTestLocations.Image
                            || info.Location == TreeViewHitTestLocations.Label))
                    {
                        this.SelectedNode = info.Node;
                        KBEntry entry = info.Node.Tag as KBEntry;
                        if (entry != null)
                        {
                            if (entry.Style == KBItemStyle.List
                                && entry.SubEntries != null
                                && entry.SubEntries.Count > 0)
                            {
                                return;
                            }
                            if (DCSoft.WinForms.Native.MouseCapturer.DragDetect(this.Handle))
                            {
                                // 开始执行OLE拖拽
                                this.DoDragDrop(info.Node.Tag, DragDropEffects.Copy);
                            }
                        }
                    }
                }
            }
        }
    }
}
