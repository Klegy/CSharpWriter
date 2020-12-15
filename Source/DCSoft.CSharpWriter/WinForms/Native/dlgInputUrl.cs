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

namespace DCSoft.WinForms.Native
{
    /// <summary>
    /// URL地址输入对话框
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public partial class dlgInputUrl : Form
    {
        public dlgInputUrl()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private string _InputURL = null;
        /// <summary>
        /// 输入的URL地址
        /// </summary>
        public string InputURL
        {
            get { return _InputURL; }
            set { _InputURL = value; }
        }

        private void dlgInputUrl_Load(object sender, EventArgs e)
        {
            string[] urls = IEHelper.TypedURLs;
            if (urls != null)
            {
                cboURL.Items.AddRange(urls);
            }
            cboURL.Text = _InputURL;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string txt = cboURL.Text;
            if (txt.Trim().Length == 0)
            {
                return;
            }
            _InputURL = txt ;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
