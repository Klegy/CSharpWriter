using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom ;
using DCSoft.CSharpWriter.Data;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgXDataBinding : Form
    {
        public dlgXDataBinding()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private DomDocument _Document = null;

        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private XDataBinding _XDataBinding = null;
        /// <summary>
        /// 数据源绑定信息对象
        /// </summary>
        public XDataBinding XDataBinding
        {
            get
            {
                return _XDataBinding; 
            }
            set
            {
                _XDataBinding = value; 
            }
        }

        private void dlgXDataBinding_Load(object sender, EventArgs e)
        {
            if (_XDataBinding == null)
            {
                _XDataBinding = new XDataBinding();
            }
            if (_Document != null)
            {
                cboDataSource.Items.AddRange(_Document.InnerParameters.Names);
                if (_Document.Parameters != null)
                {
                    cboDataSource.Items.AddRange(_Document.Parameters.Names);
                }
            }
            cboDataSource.Text = _XDataBinding.DataSource;
            cboFormat.Text = _XDataBinding.FormatString;
            txtPath.Text = _XDataBinding.BindingPath;
            chkReadonly.Checked = _XDataBinding.Readonly;
            chkAutoUpdate.Checked = _XDataBinding.AutoUpdate;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _XDataBinding.DataSource = GetMemberStringValue(cboDataSource.Text);
            _XDataBinding.FormatString = GetMemberStringValue(cboFormat.Text);
            _XDataBinding.Readonly = chkReadonly.Checked;
            _XDataBinding.AutoUpdate = chkAutoUpdate.Checked;
            _XDataBinding.BindingPath = GetMemberStringValue(txtPath.Text.Trim());
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private string GetMemberStringValue(string txt)
        {
            txt = txt.Trim();
            if (txt.Length == 0)
            {
                return null;
            }
            else
            {
                return txt;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
