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

namespace DCSoft.Script
{
    public partial class dlgBrowseGACAssembly : Form
    {
        public dlgBrowseGACAssembly()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private DotNetAssemblyInfo mySelectedAssembly = null;

        public DotNetAssemblyInfo SelectedAssembly
        {
            get
            {
                return mySelectedAssembly; 
            }
            set
            {
                mySelectedAssembly = value; 
            }
        }
        
        private void dlgBrowseGACAssembly_Load(object sender, EventArgs e)
        {
            foreach ( DotNetAssemblyInfo info in DotNetAssemblyInfoList.GlobalList )
            {
                ListViewItem item = new ListViewItem(info.Name);
                item.SubItems.Add(info.VersionString);
                item.SubItems.Add(info.FileName );
                item.Tag = info ;
                lvwAssembly.Items.Add(item);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (lvwAssembly.SelectedItems.Count > 0)
            {
                ListViewItem item = lvwAssembly.SelectedItems[0];
                mySelectedAssembly = (DotNetAssemblyInfo)item.Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}