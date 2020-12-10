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

namespace DCSoft.CSharpWriter.Commands
{
    public partial class frmViewXML : Form
    {
        public frmViewXML()
        {
            InitializeComponent();
        }

        public string XMLSource
        {
            get
            {
                return txtXML.Text;
            }
            set
            {
                txtXML.Text = value;
            }
        }

        private void frmViewXML_Load(object sender, EventArgs e)
        {

        }
    }
}
