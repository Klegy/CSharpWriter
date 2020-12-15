using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCSoft.CSharpWriter.Data
{
    public partial class dlgListSourceInfo : Form
    {
        public dlgListSourceInfo()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ListSourceInfo _ListSource = null;

        public ListSourceInfo ListSource
        {
            get { return _ListSource; }
            set { _ListSource = value; }
        }

        private void dlgListSourceInfo_Load(object sender, EventArgs e)
        {
            if (ListSourceInfo.SupportSourceNames != null)
            {
                cboSource.Items.AddRange(ListSourceInfo.SupportSourceNames);
            }
            if (_ListSource != null)
            {
                txtName.Text = _ListSource.Name;
                cboSource.Text = _ListSource.SourceName;
                cboDisplayPath.Text = _ListSource.DisplayPath;
                txtFormatString.Text = _ListSource.FormatString;
                cboValuePath.Text = _ListSource.ValuePath;
                if (_ListSource.Items != null)
                {
                    foreach (ListItem item in _ListSource.Items)
                    {
                        dgvItems.Rows.Add(item.Text, item.Value);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_ListSource == null)
            {
                _ListSource = new ListSourceInfo();
            }
            _ListSource.Name = txtName.Text.Trim();
            _ListSource.SourceName = cboSource.Text.Trim();
            _ListSource.DisplayPath = cboDisplayPath.Text.Trim();
            _ListSource.FormatString = txtFormatString.Text.Trim();
            _ListSource.ValuePath = cboValuePath.Text.Trim();
            _ListSource.Items = new ListItemCollection();
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.Index != dgvItems.NewRowIndex)
                {
                    ListItem item = new ListItem();
                    item.Text = Convert.ToString(row.Cells[0].Value);
                    item.Value = Convert.ToString(row.Cells[1].Value);
                    _ListSource.Items.Add(item);
                }
            }
            if (_ListSource.Items.Count == 0)
            {
                _ListSource.Items = null;
            }
            _ListSource.RuntimeItems = null;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ( dgvItems.CurrentRow != null 
                && dgvItems.CurrentRow.Index != dgvItems.NewRowIndex)
            {
                dgvItems.Rows.RemoveAt(dgvItems.CurrentRow.Index);
            }
        }
    }
}
