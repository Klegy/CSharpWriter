using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgInsertTable : Form
    {
        public dlgInsertTable()
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

        private XTextTableElementProperties _TableCreationInfo = new XTextTableElementProperties();
        /// <summary>
        /// 用户使用的创建表格信息对象
        /// </summary>
        public XTextTableElementProperties TableCreationInfo
        {
            get { return _TableCreationInfo; }
            set { _TableCreationInfo = value; }
        }

        private void dlgInsertTable_Load(object sender, EventArgs e)
        {
            if (_TableCreationInfo != null)
            {
                txtRows.Value = _TableCreationInfo.RowsCount;
                txtColumns.Value = _TableCreationInfo.ColumnsCount;
            }
        }

        private void ReadSettings(XTextTableElementProperties info)
        {
            info.RowsCount =( int ) txtRows.Value;
            info.ColumnsCount = (int)txtColumns.Value;
        }

        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            XTextTableElementProperties info = new XTextTableElementProperties();
            ReadSettings(info);
            info.DrawPreview(e.Graphics, new Rectangle(10, 10, pnlPreview.ClientSize.Width - 20, pnlPreview.ClientSize.Height - 20));
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (_TableCreationInfo == null)
            {
                _TableCreationInfo = new XTextTableElementProperties();
            }
            ReadSettings(_TableCreationInfo);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRows_ValueChanged(object sender, EventArgs e)
        {
            pnlPreview.Invalidate();
        }

        private void txtColumns_ValueChanged(object sender, EventArgs e)
        {
            pnlPreview.Invalidate();
        }
    }
}
