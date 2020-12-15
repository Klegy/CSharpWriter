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
using DCSoft.CSharpWriter.Dom ;
using DCSoft.Drawing;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgImageElementEditor : Form
    {
        public dlgImageElementEditor()
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


        private void dlgImageElementEditor_Load(object sender, EventArgs e)
        {
            if ( this.SourceEventArgs != null && this.SourceEventArgs.Element is DomImageElement )
            {
                DomImageElement img = (DomImageElement)this.SourceEventArgs.Element;
                txtID.Text = img.ID;
                txtTitle.Text = img.Title;
                txtAlt.Text = img.Alt;
                txtSource.Text = img.Source;
                chkKeepWidthHeightRate.Checked = img.KeepWidthHeightRate;
                chkSaveContentInFile.Checked = img.SaveContentInFile;
                picImage.Image = img.Image.Value;
                picImage.Tag = img.Image;
            }
            UpdateImageButton();
        }

        private void UpdateImageButton()
        {
            btnClear.Enabled = picImage.Image != null;
            btnSave.Enabled = picImage.Image != null;
            btnCopy.Enabled = picImage.Image != null;
            
        }
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = WriterStrings.ImageFileFilter;
                dlg.CheckFileExists = true;
                dlg.ShowReadOnly = false;
                dlg.FileName = txtSource.Text;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    txtSource.Text = dlg.FileName;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            picImage.Image = null;
            picImage.Tag = null;
            UpdateImageButton();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = WriterStrings.ImageFileFilter;
                dlg.CheckFileExists = true;
                dlg.ShowReadOnly = false;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    XImageValue img = new XImageValue(dlg.FileName);
                    picImage.Tag = img;
                    picImage.Image = img.Value;
                    UpdateImageButton();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (picImage.Image != null)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = WriterStrings.ImageFileFilter;
                    dlg.OverwritePrompt = true;
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        XImageValue img = (XImageValue)picImage.Tag;
                        img.Save(dlg.FileName);
                    }
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (picImage.Image != null)
            {
                //DataObject obj = new DataObject();
                //obj.SetData(DataFormats.Bitmap, picImage.Image);
                Clipboard.SetData(DataFormats.Bitmap, picImage.Image);
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                picImage.Image = Clipboard.GetImage();
                picImage.Tag = new XImageValue(picImage.Image);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SourceEventArgs != null && this.SourceEventArgs.Element is DomImageElement)
            {
                DomDocument document = this.SourceEventArgs.Document ;
                DomImageElement element = (DomImageElement)this.SourceEventArgs.Element;
                element.OwnerDocument = document;
                bool logUndo = this.SourceEventArgs.LogUndo && document.CanLogUndo;
                bool modified = false;
                string txt = txtID.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != element.ID)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty("ID", element.ID, txt, element);
                    }
                    element.ID = txt;
                    modified = true;
                }

                txt = txtTitle.Text.Trim();
                if( txt.Length == 0 )
                {
                    txt = null ;
                }
                if( txt != element.Title )
                {
                    if( logUndo )
                    {
                        document.UndoList.AddProperty("Title" , element.Title , txt , element );
                    }
                    element.Title = txt ;
                    modified = true ;
                }

                txt = txtAlt.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != element.Alt)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty("Alt", element.Alt, txt, element);
                    }
                    element.Alt = txt;
                    modified = true;
                }

                txt = txtSource.Text.Trim();
                if (txt.Length == 0)
                {
                    txt = null;
                }
                if (txt != element.Source)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty("Source", element.Source, txt, element);
                    }
                    element.Source = txt;
                    modified = true;
                    element.UpdateImageContent();
                }

                if (chkKeepWidthHeightRate.Checked != element.KeepWidthHeightRate)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "KeepWidthHeightRate", 
                            element.KeepWidthHeightRate, 
                            chkKeepWidthHeightRate.Checked,
                            element);
                    }
                    element.KeepWidthHeightRate = chkKeepWidthHeightRate.Checked;
                    modified = true;
                }

                if (chkSaveContentInFile.Checked != element.SaveContentInFile)
                {
                    if (logUndo)
                    {
                        document.UndoList.AddProperty(
                            "SaveContentInFile",
                            element.SaveContentInFile,
                            chkSaveContentInFile.Checked,
                            element);
                    }
                    element.SaveContentInFile = chkSaveContentInFile.Checked;
                    modified = true;
                }

                if( picImage.Tag != element.Image )
                {
                    XImageValue img = (XImageValue)picImage.Tag;
                    if (img == null)
                    {
                        img = new XImageValue();
                    }
                    if (img.HasContent)
                    {
                        if (logUndo)
                        {
                            document.UndoList.AddProperty(
                                "Image",
                                element.Image,
                                img,
                                element);
                        }
                        element.Image = img;
                        modified = true;
                    }
                }
                if (modified)
                {
                    SizeF oldSize = new SizeF(element.Width, element.Height);
                    element.UpdateSize();
                    SizeF newSize = new SizeF(element.Width, element.Height);
                    if (logUndo)
                    {
                        document.UndoList.AddProperty("Width", oldSize.Width, element.Width, element);
                        document.UndoList.AddProperty("Height", oldSize.Height, element.Height, element);
                    }
                }
                if (this.SourceEventArgs.Method == ElementEditMethod.Edit)
                {
                    if (modified)
                    {
                        element.UpdateContentVersion();
                        DomContentElement ce = element.ContentElement;
                        ce.SetLinesInvalidateState(
                            element.OwnerLine,
                            element.OwnerLine);
                        ce.UpdateContentElements(true);
                        element.SizeInvalid = true;
                        ce.RefreshPrivateContent(element.ViewIndex);
                        ContentChangedEventArgs args = new ContentChangedEventArgs();
                        args.Document = element.OwnerDocument;
                        args.Element = element;
                        args.LoadingDocument = false;
                        element.Parent.RaiseBubbleOnContentChanged(args);
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
    }
}
