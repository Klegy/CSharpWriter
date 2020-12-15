using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgContentLinkEditor : Form
    {
        public dlgContentLinkEditor()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ElementEditEventArgs _SourceEventArgs = null;

        public ElementEditEventArgs SourceEventArgs
        {
            get { return _SourceEventArgs; }
            set { _SourceEventArgs = value; }
        }

        private void dlgContentLink_Load(object sender, EventArgs e)
        {
            if (_SourceEventArgs != null && _SourceEventArgs.Element is DomContentLinkFieldElement)
            {
                DomContentLinkFieldElement element = (DomContentLinkFieldElement)_SourceEventArgs.Element;
                txtFileName.Text = element.FileName;
                txtTarget.Text = element.TargetRange;
                chkAutoUpdate.Checked = element.AutoUpdate;
                chkReadonly.Checked = element.Readonly;
                chkSaveContentToFile.Checked = element.SaveContentToFile;
            }
        }

        private bool _Modified = false;

        public bool Modified
        {
            get { return _Modified; }
            set { _Modified = value; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if( _SourceEventArgs != null 
                && _SourceEventArgs.Element is DomContentLinkFieldElement )
            {
                DomDocument document = _SourceEventArgs.Document;
                DomContentLinkFieldElement element = (DomContentLinkFieldElement)_SourceEventArgs.Element;
                bool logUndo = this.SourceEventArgs.LogUndo && document.CanLogUndo;
                string txt = txtFileName.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != element.FileName)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "FileName",
                            element.FileName,
                            txt,
                            element);
                    }
                    element.FileName = txt;
                    this.Modified = true;
                }
                txt = txtTarget.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != element.TargetRange)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "TargetRange",
                            element.TargetRange,
                            txt,
                            element);
                    }
                    element.TargetRange = txt;
                    this.Modified = true;
                }
                if (chkAutoUpdate.Checked != element.AutoUpdate)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "AutoUpdate",
                            element.AutoUpdate,
                            chkAutoUpdate.Checked,
                            element);
                    }
                    element.AutoUpdate = chkAutoUpdate.Checked;
                    this.Modified = true;
                }
                if (chkReadonly.Checked != element.Readonly)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "Readonly",
                            element.Readonly, 
                            chkReadonly.Checked,
                            element);
                    }
                    element.Readonly = chkReadonly.Checked;
                    this.Modified = true;
                }
                if ( chkSaveContentToFile.Checked != element.SaveContentToFile )
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "SaveContentToFile",
                            element.SaveContentToFile,
                            chkSaveContentToFile.Checked,
                            element);
                    }
                    element.SaveContentToFile = chkSaveContentToFile.Checked;
                    this.Modified = true;
                }
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                    if (this.Modified)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null)
            {
                IFileSystem fs = this.SourceEventArgs.Host.FileSystems.Docuemnt ;
                if (fs != null)
                {
                    string fileName = fs.BrowseOpen(this.SourceEventArgs.Host.Services , txtFileName.Text);
                    if (string.IsNullOrEmpty(fileName) == false)
                    {
                        txtFileName.Text = fileName;
                    }
                }
            }
        }
    }
}
