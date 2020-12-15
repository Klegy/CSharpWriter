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
using DCSoft.WinForms;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 下拉列表方式的数据编辑器
    /// </summary>
    [System.ComponentModel.ToolboxItem( false )]
    public class XTreeListBoxEditControl : Panel , IMessageFilter
    {
        public XTreeListBoxEditControl()
        {
            _ListBox = new XTreeListBox();
            _ListBox.Dock = DockStyle.Fill;
            this.Controls.Add(_ListBox);
            _ListBox.SelectedIndexChanged += new EventHandler(_ListBox_SelectedIndexChanged);
            _ListBox.UserAcceptSelection += new EventHandler(_ListBox_UserAcceptSelection);
            _ListBox.UserCancel += new EventHandler(_ListBox_UserCancel);
            _ListBox.ListContentChanged += new EventHandler(_ListBox_ListContentChanged);
        }

        private TextWindowsFormsEditorHost _EditorHost = null;
        /// <summary>
        /// 编辑器宿主
        /// </summary>
        public TextWindowsFormsEditorHost EditorHost
        {
            get
            {
                return _EditorHost; 
            }
            set
            {
                _EditorHost = value; 
            }
        }

        public bool ShowDropDown()
        {
            //IWindowsFormsEditorService svc = ( IWindowsFormsEditorService ) _EditorHost;
            this._ListBox.CalculateViewSize();
            this._ListBox.RefreshChineseSpell(true);
            this._ListBox.BeginUpdate();
            this._ListBox.EndUpdate();
            if (this._ListBox.SelectedItem != null)
            {
                this._ListBox.ScrollToView(this._ListBox.SelectedItem);
            }
            this.EditorHost.DropDownControl(this);
            return this.Modified;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size size = this._ListBox.GetPreferredSize(proposedSize);
            return size;
        }

        void _ListBox_ListContentChanged(object sender, EventArgs e)
        {
            System.Drawing.Size size = _ListBox.GetPreferredSize(
                new System.Drawing.Size(500, 250));
            size.Width = size.Width + this.Width - this.ClientSize.Width;
            size.Height = size.Height + this.Height - this.ClientSize.Height;
            if (_EditorHost != null)
            {
                _EditorHost.UpdateComposition(size);
            }
            else
            {
                this.Size = size;
            }
        }

        void _ListBox_UserCancel(object sender, EventArgs e)
        {
            this.Modified = false;
            _UserProcessState = UserProcessState.Cancel;
            if (_EditorHost != null)
            {
                _EditorHost.CloseDropDown();
            }
        }

        void _ListBox_UserAcceptSelection(object sender, EventArgs e)
        {
            this.Modified = true;
            _UserProcessState = UserProcessState.Accept;
            if (_EditorHost != null)
            {
                _EditorHost.CloseDropDown();
            }
        }

        void _ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Modified = true;
            //throw new NotImplementedException();
        }

        private XTreeListBox _ListBox = new XTreeListBox();

        public XTreeListBox ListBox
        {
            get
            {
                return _ListBox; 
            }
            set
            {
                _ListBox = value; 
            }
        }

        private bool _Modified = false;
        /// <summary>
        /// 数值发生改变标志
        /// </summary>
        public bool Modified
        {
            get
            {
                return _Modified; 
            }
            set
            {
                _Modified = value;
                //if (value)
                //{
                //    System.Console.Write("");
                //}
            }
        }

        public ImageList ImageList
        {
            get
            {
                return _ListBox.ImageList;
            }
            set
            {
                _ListBox.ImageList = value;
            }
        }

        private UserProcessState _UserProcessState = UserProcessState.Processing;
        //public override UserProcessState GetUserProcessSate()
        //{
        //    return _UserProcessState;
        //}

        //public override UserProcessState GetDefaultUserProcessState()
        //{
        //    if (this._ListBox.SelectionModified == true )
        //        return UserProcessState.Accept;
        //    else
        //        return UserProcessState.Cancel;
        //}

        #region IMessageFilter 成员

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            if (this._ListBox == null || this._ListBox.IsDisposed)
            {
                return false;
            }
            else
            {
                return this._ListBox.PreFilterMessage(ref m);
            }
        }

        #endregion
    }

}
