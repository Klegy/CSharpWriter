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
//using DCSoft.Common;

namespace DCSoft.Script
{
    public partial class dlgScriptOptions : Form
    {
        public dlgScriptOptions()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private XVBAOptions myOptionsInstance = null;
        /// <summary>
        /// script options
        /// </summary>
        public XVBAOptions OptionsInstance
        {
            get
            {
                return myOptionsInstance; 
            }
            set
            {
                myOptionsInstance = value; 
            }
        }

        private void dlgScriptOptions_Load(object sender, EventArgs e)
        {
            if (myOptionsInstance != null)
            {
                foreach (DotNetAssemblyInfo info in myOptionsInstance.ReferenceAssemblies)
                {
                    DotNetAssemblyInfo info2 = DotNetAssemblyInfoList.GlobalList[info.Name];
                    if (info2 != null && info2.SourceStyle == info.SourceStyle)
                    {
                        info.FileName = DotNetAssemblyInfo.GetFileNameByCodeBase(info2.CodeBase);
                        info.Version = info2.Version;
                        info.RuntimeVersion = info2.RuntimeVersion;
                        info.Flags = info2.Flags;
                        info.CodeBase = info2.CodeBase;
                    }
                    ListViewItem item = new ListViewItem(info.Name);
                    item.SubItems.Add(info.VersionString);
                    item.SubItems.Add(info.FileName);
                    item.Tag = info;
                    lvwAssembly.Items.Add(item);
                }
                System.Text.StringBuilder str = new StringBuilder();
                foreach (string item in myOptionsInstance.ImportNamespaces)
                {
                    if (str.Length > 0)
                        str.Append(Environment.NewLine);
                    str.Append(item);
                }
                this.txtImports.Text = str.ToString();
            }
        }

        private void cmdAddGACAssembly_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            using (dlgBrowseGACAssembly dlg = new dlgBrowseGACAssembly())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;

                    DotNetAssemblyInfo info = dlg.SelectedAssembly;
                    ListViewItem item = new ListViewItem(info.Name);
                    item.SubItems.Add(info.VersionString);
                    item.SubItems.Add(info.FileName);
                    item.Tag = info;
                    lvwAssembly.Items.Add(item);
                }
            }
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void cmdBrowseAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.Filter = ScriptStrings.AssemblyFileFilter;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    DotNetAssemblyInfo info = DotNetAssemblyInfo.CreateByFileName(dlg.FileName);
                    ListViewItem item = new ListViewItem(info.Name);
                    item.SubItems.Add(info.VersionString);
                    item.SubItems.Add(info.FileName);
                    item.Tag = info;
                    lvwAssembly.Items.Add(item);
                }
            }
        }

        private void cmdDeleteAssembly_Click(object sender, EventArgs e)
        {
            if (lvwAssembly.SelectedItems.Count > 0)
            {
                lvwAssembly.Items.Remove(lvwAssembly.SelectedItems[0]);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (myOptionsInstance != null)
            {

                myOptionsInstance.ImportNamespaces = new MyStringList();
                System.IO.StringReader reader = new System.IO.StringReader(txtImports.Text);
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = line.Trim();

                    if (line.Length > 0 && myOptionsInstance.ImportNamespaces.Contains(line) == false)
                    {
                        myOptionsInstance.ImportNamespaces.Add(line);
                    }
                 
                    line = reader.ReadLine();
                }
                myOptionsInstance.ReferenceAssemblies.Clear();
                foreach (ListViewItem item in lvwAssembly.Items)
                {
                    myOptionsInstance.ReferenceAssemblies.Add( ( DotNetAssemblyInfo ) item.Tag);
                }
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