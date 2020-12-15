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
    public partial class dlgPageInfoType : Form
    {
        public dlgPageInfoType()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private PageInfoContentType _ContentType = PageInfoContentType.PageIndex;

        public PageInfoContentType ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        private void dlgPageInfoType_Load(object sender, EventArgs e)
        {
            if (_ContentType == PageInfoContentType.PageIndex)
            {
                lstPageInfoType.SelectedIndex = 0;
            }
            else
            {
                lstPageInfoType.SelectedIndex = 1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstPageInfoType.SelectedIndex == 0)
            {
                _ContentType = PageInfoContentType.PageIndex;
            }
            else
            {
                _ContentType = PageInfoContentType.NumOfPages;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
