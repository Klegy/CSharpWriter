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
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public partial class dlgAttributes : Form
    {
        public dlgAttributes()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private XAttributeList _InputAttributes = null;
        /// <summary>
        /// 属性列表
        /// </summary>
        public XAttributeList InputAttributes
        {
            get
            {
                return _InputAttributes; 
            }
            set
            {
                _InputAttributes = value; 
            }
        }

        private void dlgAttributes_Load(object sender, EventArgs e)
        {
            if (_InputAttributes == null)
            {
                _InputAttributes = new XAttributeList();
            }
            dgvAttributes.Rows.Clear();
            foreach (DomAttribute item in _InputAttributes)
            {
                int index = dgvAttributes.Rows.Add(item.Name, item.Value, "...");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _InputAttributes.Clear();
            foreach (DataGridViewRow row in dgvAttributes.Rows)
            {
                if (row.Index != dgvAttributes.NewRowIndex)
                {
                    DomAttribute item = new DomAttribute();
                    item.Name = Convert.ToString(row.Cells[0].Value);
                    item.Value = Convert.ToString(row.Cells[1].Value);
                    _InputAttributes.Add(item);
                }
            }//foreach
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
