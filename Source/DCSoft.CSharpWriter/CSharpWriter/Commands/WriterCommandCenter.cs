/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
//using System;
//using System.Collections.Generic;
//using System.Text;
//using DCSoft.CSharpWriter.Controls;
//using DCSoft.CSharpWriter.Dom;
//using System.Windows.Forms;

//namespace DCSoft.CSharpWriter.Commands
//{
//    /// <summary>
//    /// 功能动作列表
//    /// </summary>
//    public class WriterCommandCenter
//    {
//        /// <summary>
//        /// 初始化对象
//        /// </summary>
//        public WriterCommandCenter()
//        {
//            this._ControlStateManager.OwnerCommandCenter = this;
//        }


//        private TextDocumentEditControl _EditControl = null;
//        /// <summary>
//        /// 编辑器控件
//        /// </summary>
//        public TextDocumentEditControl EditControl
//        {
//            get { return _EditControl; }
//            set { _EditControl = value; }
//        }

//        private XTextDocument _Document = null;
//        /// <summary>
//        /// 文档对象
//        /// </summary>
//        public XTextDocument Document
//        {
//            get { return _Document; }
//            set { _Document = value; }
//        }

//        private WriterCommandContainer _CommandContainer = new WriterCommandContainer();
//        /// <summary>
//        /// 命令容器对象
//        /// </summary>
//        public WriterCommandContainer CommandContainer
//        {
//            get
//            {
//                return _CommandContainer; 
//            }
//        }

//        private ControlStateManager _ControlStateManager = new ControlStateManager();
//        /// <summary>
//        /// 控件状态控制器
//        /// </summary>
//        public ControlStateManager ControlStateManager
//        {
//            get
//            {
//                return _ControlStateManager; 
//            }
//            //set { _ControlStateManager = value; }
//        }

//        public WriterCommandBase Active(TextDocumentEditControl ctl, XTextDocument document, KeyEventArgs e)
//        {
//            return this.CommandContainer.Active(ctl, document, e);
//        }

//        public WriterCommandBase GetCommand(string name)
//        {
//            return this.CommandContainer.GetCommand(name);
//        }

//        /// <summary>
//        /// 判断指定名称的命令是否可用
//        /// </summary>
//        /// <param name="commandName">命令名称</param>
//        /// <returns>该命令是否可用</returns>
//        public bool IsCommandEnabled(string commandName)
//        {
//            WriterCommandBase cmd = this.CommandContainer.GetCommand(commandName);
//            if (cmd != null)
//            {
//                WriterCommandEventArgs args = new WriterCommandEventArgs(
//                    this.EditControl,
//                    this.Document,
//                    WriterCommandEventMode.QueryState);
//                args.EnableGUI = true ;
//                cmd.Invoke(args);
//                return args.Enabled;
//            }
//            return false;
//        }

//        /// <summary>
//        /// 判断指定名称的命令的状态是否处于选中状态
//        /// </summary>
//        /// <param name="commandName">命令名称</param>
//        /// <returns>该命令是否处于选中状态</returns>
//        public bool IsCommandChecked(string commandName)
//        {
//            WriterCommandBase cmd = this.CommandContainer.GetCommand(commandName);
//            if (cmd != null)
//            {
//                WriterCommandEventArgs args = new WriterCommandEventArgs(
//                    this.EditControl,
//                    this.Document,
//                    WriterCommandEventMode.QueryState);
//                args.EnableGUI = true;
//                cmd.Invoke(args);
//                return args.Checked;
//            }
//            return false;
//        }

//        /// <summary>
//        /// 执行命令
//        /// </summary>
//        /// <param name="commandName">命令文本</param>
//        /// <param name="enableUI">是否允许显示用户界面</param>
//        /// <param name="parameter">用户参数</param>
//        public void ExecuteCommand(string commandName, bool enableUI, object parameter)
//        {
//            WriterCommandBase cmd = this.CommandContainer.GetCommand(commandName);
//            if (cmd != null)
//            {
//                WriterCommandEventArgs args = new WriterCommandEventArgs(
//                    this.EditControl,
//                    this.Document,
//                    WriterCommandEventMode.QueryState);
//                args.EnableGUI = enableUI;
//                cmd.Invoke(args);
//                if (args.Enabled)
//                {
//                    args.Mode = WriterCommandEventMode.Invoke;
//                    cmd.Invoke(args);
//                    if (args.InvalidateUIState)
//                    {
//                        this.ControlStateManager.UpdateBindingControlStatus();
//                    }
//                }
//            }
//        }
//    }
//}
