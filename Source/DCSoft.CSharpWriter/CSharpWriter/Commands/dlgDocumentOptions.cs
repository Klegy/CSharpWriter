/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter ;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgDocumentOptions : Form
    {
        public dlgDocumentOptions()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private DocumentOptions _DocumentOptions = null;

        public DocumentOptions DocumentOptions
        {
            get
            {
                return _DocumentOptions; 
            }
            set
            {
                _DocumentOptions = value; 
            }
        }

        private void dlgDocumentOptions_Load(object sender, EventArgs e)
        {
            if (_DocumentOptions == null)
            {
                _DocumentOptions = new DocumentOptions();
            }
            pgOptions.SelectedObject = _DocumentOptions;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
