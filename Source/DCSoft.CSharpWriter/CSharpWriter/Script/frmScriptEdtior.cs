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
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Script
{
    public partial class frmScriptEdtior : Form
    {
        public frmScriptEdtior()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private bool _Readonly = false;
        /// <summary>
        /// 内容只读
        /// </summary>
        public bool Readonly
        {
            get { return _Readonly; }
            set { _Readonly = value; }
        }

        private string _ScriptText = null;

        /// <summary>
        /// 编辑的脚本代码文本
        /// </summary>
        public string ScriptText
        {
            get
            {
                return _ScriptText;
            }
            set
            {
                _ScriptText = value;
            }
        }

        private void frmScriptEdtior_Load(object sender, EventArgs e)
        {
            txtScript.Text = _ScriptText;
            txtScript.Modified = false;
            this.btnSave.Enabled =  this.Readonly == false ;
            txtScript.ReadOnly = this.Readonly;
            if (this.Readonly)
            {
                this.Text = this.Text + "[" + WriterStrings.Readonly + "]";
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _ScriptText = txtScript.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            DocumentScriptEngine engine = new DocumentScriptEngine();
            engine.Document = this.Document;
            engine.ScriptText = txtScript.Text;
            if (engine.DebugCompile())
            {
                txtCompileResult.Text = "";
                MessageBox.Show(
                    this, 
                    WriterStrings.ScriptCompileOK,
                    WriterStrings.SystemAlert,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                txtCompileResult.Text = engine.RuntimeScriptTextWithCompilerErrorMessage;
                myTabControl.SelectedIndex = 1;
                MessageBox.Show(
                    this,
                    WriterStrings.ScriptCompileFail,
                    WriterStrings.SystemAlert,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void frmScriptEdtior_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtScript.Modified
                && this.Readonly == false
                && this.DialogResult == System.Windows.Forms.DialogResult.Cancel )
            {
                DialogResult resut = MessageBox.Show(
                    this,
                    WriterStrings.QuerySave,
                    WriterStrings.SystemAlert,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                switch (resut)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        _ScriptText = txtScript.Text;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
