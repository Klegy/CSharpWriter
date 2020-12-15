using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Controls
{
    public partial class dlgInsertParameter : Form
    {
        public dlgInsertParameter()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private string _InputName = null;

        public string InputName
        {
            get { return _InputName; }
            set { _InputName = value; }
        }

        private string _InputParameterName = null;

        public string InputParameterName
        {
            get { return _InputParameterName; }
            set { _InputParameterName = value; }
        }


        private DomDocument _Document = null;

        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        private void dlgInsertParameter_Load(object sender, EventArgs e)
        {
            if (this.Document != null)
            {
                lstName.Items.AddRange(this.Document.InnerParameters.Names);
                if (this.Document.Parameters != null)
                {
                    lstName.Items.AddRange(this.Document.Parameters.Names);
                }
            }
            txtName.Text = this.InputName;
            lstName.Text = this.InputParameterName;
        }

        private void lstName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Document != null)
            {
                lblPreview.Text = Convert.ToString(this.Document.GetParameterValue(lstName.Text));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.InputName = txtName.Text;
            this.InputParameterName = lstName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
