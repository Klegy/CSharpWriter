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
using System.Reflection ;
using System.Collections;

namespace DCSoft.CSharpWriter.Commands.Design
{
    public partial class dlgCommandNameEditor : Form
    {
        public static void Test()
        {
            using (dlgCommandNameEditor dlg = new dlgCommandNameEditor())
            {

                dlg._CommandDescriptors = WriterCommandNameDlgEditor.GetCommandDescriptors(dlg.GetType().Assembly.GetTypes());
                dlg.ShowDialog();
            }
 
        }

        public dlgCommandNameEditor()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private ArrayList _CommandDescriptors = null;
        /// <summary>
        /// 功能模块信息列表
        /// </summary>
        public ArrayList CommandDescriptors
        {
            get 
            {
                return _CommandDescriptors; 
            }
            set
            {
                _CommandDescriptors = value; 
            }
        }
         

        private string _InputCommandName = null;

        public string InputCommandName
        {
            get { return _InputCommandName; }
            set { _InputCommandName = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this._CommandDescriptors != null)
            {
                FillTreeView(tvwCommand, this._CommandDescriptors);
                if (this.InputCommandName != null && this.InputCommandName.Length > 0)
                {
                    foreach (TreeNode moduleNode in tvwCommand.Nodes)
                    {
                        if (moduleNode.Tag is WriterCommandDescriptor && moduleNode.Text == this.InputCommandName)
                        {
                            tvwCommand.SelectedNode = moduleNode;
                            moduleNode.EnsureVisible();
                            break;
                        }
                        foreach (TreeNode cmdNode in moduleNode.Nodes)
                        {
                            if (cmdNode.Text == this.InputCommandName)
                            {
                                tvwCommand.SelectedNode = cmdNode;
                                cmdNode.EnsureVisible();
                                return;
                            }
                        }
                    }
                }
                if (tvwCommand.Nodes.Count > 0)
                {
                    tvwCommand.Nodes[0].Expand();
                }
            }
            tvwCommand_AfterSelect(null, null);
        }

        public static void FillTreeView(TreeView tvw, IEnumerable descriptors )
        {
            ImageList images = new ImageList();
            images.Images.Add(CommandUtils.GetResourceImage(
                typeof(dlgCommandNameEditor).Assembly,
                "DCSoft.CSharpWriter.Commands.Images.CommandModule.bmp"));
            images.Images.Add(CommandUtils.GetResourceImage(
                typeof(dlgCommandNameEditor).Assembly,
                "DCSoft.CSharpWriter.Commands.Images.CommandDefault.bmp"));
            Dictionary<object , int > indexs = new Dictionary<object,int>();
            // 初始化图标列表
            foreach (object item in descriptors)
            {
                if (item is WriterCommandDescriptor)
                {
                    WriterCommandDescriptor d = (WriterCommandDescriptor)item;
                    if (d.Image != null)
                    {
                        images.Images.Add(d.Image);
                        indexs[d] = images.Images.Count - 1;
                    }
                    else
                    {
                        indexs[d] = 1;
                    }
                }
                else if (item is WriterCommandModuleDescriptor)
                {
                    WriterCommandModuleDescriptor m = (WriterCommandModuleDescriptor)item;
                    if (m.Image != null)
                    {
                        images.Images.Add(m.Image);
                        indexs[m] = images.Images.Count - 1;
                    }
                    else
                    {
                        indexs[m] = 0;
                    }
                    foreach (WriterCommandDescriptor d in m.Commands)
                    {
                        if (d.Image != null)
                        {
                            images.Images.Add(d.Image);
                            indexs[d] = images.Images.Count - 1;
                        }
                        else
                        {
                            indexs[d] = 1;
                        }
                    }
                }
            }//foreach
            
            tvw.ImageList = images;
            foreach (object item in descriptors)
            {
                if (item is WriterCommandDescriptor)
                {
                    WriterCommandDescriptor d = (WriterCommandDescriptor)item;
                    TreeNode cmdNode = new TreeNode(d.CommandName);
                    cmdNode.Tag = item;
                    if (indexs.ContainsKey(item))
                    {
                        cmdNode.ImageIndex = indexs[item];
                        cmdNode.SelectedImageIndex = cmdNode.ImageIndex;
                    }
                    tvw.Nodes.Add(cmdNode);
                }
                else if (item is WriterCommandModuleDescriptor)
                {
                    WriterCommandModuleDescriptor m = (WriterCommandModuleDescriptor)item;
                    TreeNode moduleNode = new TreeNode(m.Name);
                    moduleNode.Tag = m;
                    if (indexs.ContainsKey(item))
                    {
                        moduleNode.ImageIndex = indexs[item];
                        moduleNode.SelectedImageIndex = moduleNode.ImageIndex;
                    }
                    foreach (WriterCommandDescriptor d in m.Commands)
                    {
                        TreeNode cmdNode = new TreeNode(d.CommandName);
                        cmdNode.Tag = d;
                        if (indexs.ContainsKey(d))
                        {
                            cmdNode.ImageIndex = indexs[d];
                            cmdNode.SelectedImageIndex = cmdNode.ImageIndex;
                        }
                        moduleNode.Nodes.Add(cmdNode);
                    }
                    tvw.Nodes.Add(moduleNode);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.InputCommandName = null;
            if (tvwCommand.SelectedNode != null && tvwCommand.SelectedNode.Level == 1 )
            {
                this.InputCommandName = tvwCommand.SelectedNode.Text ;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tvwCommand_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.btnOK.Enabled = (tvwCommand.SelectedNode != null 
                && tvwCommand.SelectedNode.Tag is WriterCommandDescriptor) ;
        }
    }
}
