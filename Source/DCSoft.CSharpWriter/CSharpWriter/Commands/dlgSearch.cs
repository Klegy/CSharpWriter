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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Controls ;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgSearch : Form
    {
        //private Dictionary<object, dlgSearch> _Instances = new Dictionary<object, dlgSearch>();
        ///// <summary>
        ///// 获得对话框对象实例
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <returns>获得的对象实例</returns>
        //public dlgSearch GetInstance(object key)
        //{
        //    if (_Instances.ContainsKey(key))
        //    {
        //        dlgSearch dlg = _Instances[key];
        //        if (dlg.IsDisposed)
        //        {
        //            dlg = new dlgSearch();
        //            _Instances[key] = dlg;
        //        }
        //        return dlg;
        //    }
        //    else
        //    {
        //        dlgSearch dlg = new dlgSearch();
        //        _Instances[key] = dlg;
        //        return dlg;
        //    }
        //}

        public dlgSearch()
        {
            InitializeComponent();
        }

        private SearchReplaceCommandArgs _CommandArgs = null;
        /// <summary>
        /// 命令参数对象
        /// </summary>
        public SearchReplaceCommandArgs CommandArgs
        {
            get { return _CommandArgs; }
            set { _CommandArgs = value; }
        }

        private CSWriterControl _WriterControl = null;

        public CSWriterControl WriterControl
        {
            get { return _WriterControl; }
            set { _WriterControl = value; }
        }

        private DomDocument _TextDocument = null;

        public DomDocument TextDocument
        {
            get { return _TextDocument; }
            set { _TextDocument = value; }
        }

        private void dlgSearch_Load(object sender, EventArgs e)
        {
            //UpdateUIState();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (this.WriterControl != null)
            {
                this.WriterControl.ForceShowCaret = true;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.WriterControl != null)
            {
                this.WriterControl.ForceShowCaret = false ;
            }
        }

        /// <summary>
        /// 根据命令参数对象来更新用户界面
        /// </summary>
        public void UpdateUIState()
        {
            if (_CommandArgs == null)
            {
                _CommandArgs = new SearchReplaceCommandArgs();
            }
            txtSearchString.Text = _CommandArgs.SearchString;
            chkReplace.Checked = _CommandArgs.EnableReplaceString;
            if (this.WriterControl != null)
            {
                chkReplace.Enabled = this.WriterControl.Readonly == false ;
            }
            txtReplaceString.Text = _CommandArgs.ReplaceString;
            txtReplaceString.Enabled = chkReplace.Enabled && chkReplace.Checked;
            rdoUP.Checked = _CommandArgs.Backward == false;
            rdoDown.Checked = _CommandArgs.Backward;
            chkIgnoreCase.Checked = _CommandArgs.IgnoreCase;
            UpdateButtonState();
        }

        private void chkReplace_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        /// <summary>
        /// 根据用户界面的状态来更新命令参数对象
        /// </summary>
        private void UpdateCommandArgs()
        {
            _CommandArgs.SearchString = txtSearchString.Text;
            _CommandArgs.EnableReplaceString = chkReplace.Checked;
            _CommandArgs.ReplaceString = txtReplaceString.Text;
            _CommandArgs.IgnoreCase = chkIgnoreCase.Checked;
            if (rdoDown.Checked)
            {
                _CommandArgs.Backward = true;
            }
            else if (rdoUP.Checked)
            {
                _CommandArgs.Backward = false;
            }
        }

        /// <summary>
        /// 创建查找替换文本内容的操作对象
        /// </summary>
        /// <returns>创建的操作对象</returns>
        private ContentSearchReplacer CreateSearchReplacer()
        {
            UpdateCommandArgs();
            DomDocument document = this.TextDocument;
            if (document == null && this.WriterControl != null)
            {
                document = this.WriterControl.Document;
            }
            if (document == null)
            {
                return null ;
            }
            ContentSearchReplacer sr = new ContentSearchReplacer();
            sr.Document = document;
            return sr;
        }

        /// <summary>
        /// 查找按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ContentSearchReplacer sr = CreateSearchReplacer();
            if (sr != null)
            {
                int index = sr.Search(this._CommandArgs, true , -1 );
                if (index < 0)
                {
                    MessageBox.Show(
                        this,
                        WriterStrings.CannotSearchSpecifyContent,
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (this.WriterControl != null)
                {
                    //this.WriterControl.Focus();
                }
            }
        }

        /// <summary>
        /// 替换按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReplace_Click(object sender, EventArgs e)
        {
            ContentSearchReplacer sr = CreateSearchReplacer();
            if (sr != null)
            {
                int index = sr.Replace(this._CommandArgs  );
                if (index < 0)
                {
                    MessageBox.Show(
                        this,
                        WriterStrings.CannotSearchSpecifyContent,
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (this.WriterControl != null)
                {
                    //this.WriterControl.Focus();
                }
            }
        }

        private void txtSearchString_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            bool hasContent = string.IsNullOrEmpty(txtSearchString.Text) == false;
            btnSearch.Enabled = hasContent;
            txtReplaceString.Enabled = chkReplace.Checked && chkReplace.Enabled;
            btnReplace.Enabled = hasContent && chkReplace.Checked && chkReplace.Enabled;
            btnReplaceAll.Enabled = hasContent && chkReplace.Checked && chkReplace.Enabled;
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            ContentSearchReplacer sr = CreateSearchReplacer();
            if (sr != null)
            {
                int index = sr.ReplaceAll(this._CommandArgs);
                if (index < 0)
                {
                    MessageBox.Show(
                        this,
                        WriterStrings.CannotSearchSpecifyContent,
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        this , 
                        string.Format( WriterStrings.PromptReplaceAllResult_Times , index ) ,
                        this.Text , 
                        MessageBoxButtons.OK  , 
                        MessageBoxIcon.Information );
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
