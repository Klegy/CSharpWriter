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

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgSpecifyPaste : Form
    {
        public dlgSpecifyPaste()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private System.Windows.Forms.IDataObject _DataObject = null;

        public System.Windows.Forms.IDataObject DataObject
        {
            get
            {
                return _DataObject; 
            }
            set
            {
                _DataObject = value; 
            }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 当前处理的文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private string _ResultFormat = null;
        /// <summary>
        /// 用户指定的格式
        /// </summary>
        public string ResultFormat
        {
            get { return _ResultFormat; }
            set { _ResultFormat = value; }
        }
         
        private void dlgSpecifyPaste_Load(object sender, EventArgs e)
        {
            IntPtr hwnd = DCSoft.WinForms.Native.User32.GetClipboardOwner();
            if (hwnd != IntPtr.Zero)
            {
                DCSoft.WinForms.Native.WindowInformation info = new WinForms.Native.WindowInformation(hwnd);
                if (info.Handle != IntPtr.Zero)
                {
                    lblSource.Text = lblSource.Text + info.Text;
                }
            }
            if (_DataObject == null)
            {
                _DataObject = System.Windows.Forms.Clipboard.GetDataObject();
            }

            foreach (string name in DocumentControler.SupportDataFormats)
            {
                if (_DataObject.GetDataPresent(name))
                {
                    if (this.Document != null)
                    {
                        if (this.Document.DocumentControler.CanInsertObject(
                            -1,
                            _DataObject, 
                            name , 
                            DomAccessFlags.Normal ) == false)
                        {
                            continue;
                        }
                    }
                    lstFormat.Items.Add(name);
                }
            }
            lstFormat.Text = this.ResultFormat;
            if (lstFormat.SelectedIndex < 0 && lstFormat.Items.Count > 0 )
            {
                lstFormat.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _ResultFormat = lstFormat.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstFormat_DoubleClick(object sender, EventArgs e)
        {
            if (lstFormat.SelectedIndex >= 0)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
