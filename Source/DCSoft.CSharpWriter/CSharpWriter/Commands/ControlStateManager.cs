/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Windows.Forms;
//using System.ComponentModel;
//using System.Collections;
//using DCSoft.CSharpWriter.Commands ;

//namespace DCSoft.CSharpWriter.Commands
//{
//    /// <summary>
//    /// 控件状态控制器
//    /// </summary>
//    public class ControlStateManager
//    {
//        private static ControlStateManager _Instance = new ControlStateManager();

//        /// <summary>
//        /// 对象唯一静态实例
//        /// </summary>
//        public static ControlStateManager Instance
//        {
//            get 
//            {
//                return _Instance; 
//            }
//        }

//        /// <summary>
//        /// 初始化对象
//        /// </summary>
//        public ControlStateManager()
//        {
//        }

//        private WriterCommandCenter _OwnerCommandCenter = null;
//        /// <summary>
//        /// 拥有该控件状态控制器的命令中心对象
//        /// </summary>
//        public WriterCommandCenter OwnerCommandCenter
//        {
//            get { return _OwnerCommandCenter; }
//            set { _OwnerCommandCenter = value; }
//        }

//        public void Clear()
//        {
//            _BindingControls.Clear();
//        }

//        private Dictionary<object, string> _BindingControls = new Dictionary<object, string>();

//        public void AddBindingControl(object control, string commandName)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }
//            if (commandName == null || commandName.Trim().Length == 0)
//            {
//                throw new ArgumentNullException("commandName");
//            }
//            _BindingControls[control] = commandName.Trim();
//        }

//        public object GetBindingControl(string commandName)
//        {
//            return GetBindingControl(commandName, typeof(object));
//        }

//        public object GetBindingControl(string commandName, Type controlType)
//        {
//            foreach (object ctl in _BindingControls.Keys)
//            {
//                if (string.Compare(commandName, _BindingControls[ctl], true) == 0)
//                {
//                    if (controlType.IsInstanceOfType(ctl))
//                    {
//                        return ctl;
//                    }
//                }
//            }
//            return null;
//        }

//        private bool bolIsUpdatingBindControlStatus = false;
//        /// <summary>
//        /// 正在更新用户界面控件状态
//        /// </summary>
//        [Browsable(false)]
//        public bool IsUpdatingBindControlStatus
//        {
//            get
//            {
//                return bolIsUpdatingBindControlStatus;
//            }
//        }

//        public void UpdateBindingControlStatus( )
//        {
//            UpdateBindingControlStatus(null);
//        }

//        /// <summary>
//        /// 读取控件状态
//        /// </summary>
//        /// <param name="commandName"></param>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public bool ReadControlState(string commandName , WriterCommandEventArgs args)
//        {
//            WriterCommandBase cmd = this.OwnerCommandCenter.GetCommand(commandName);
//            if (cmd == null)
//            {
//                return false;
//            }
//            bool result = false;
//            foreach (object control in _BindingControls.Keys)
//            {
//                if (string.Compare(_BindingControls[control], commandName, true) == 0)
//                {
//                    if (control is System.Windows.Forms.CheckBox)
//                    {
//                        args.Checked = ((System.Windows.Forms.CheckBox)control).Checked;
//                        result = true;
//                    }
//                    else if (control is System.Windows.Forms.ToolStripButton)
//                    {
//                        args.Checked = ((System.Windows.Forms.ToolStripButton)control).Checked;
//                        result = true;
//                    }
//                    else if (control is System.Windows.Forms.ToolStripMenuItem)
//                    {
//                        args.Checked = ((System.Windows.Forms.ToolStripMenuItem)control).Checked;
//                        result = true;
//                    }
//                    else if (control is System.Windows.Forms.MenuItem)
//                    {
//                        args.Checked = ((System.Windows.Forms.MenuItem)control).Checked;
//                        result = true;
//                    }
//                    else if (control is TextBoxBase)
//                    {
//                        args.Parameter = ((TextBox)control).Text;
//                        result = true;
//                    }
//                    else if (control is ToolStripTextBox)
//                    {
//                        args.Parameter = ((ToolStripTextBox)control).Text;
//                        result = true;
//                    }
//                    else if (control is ToolStripComboBox)
//                    {
//                        args.Parameter = ((ToolStripComboBox)control).Text;
//                        result = true;
//                    }
//                }
//            }//foreach
//            return result;
//        }

//        public void UpdateBindingControlStatus( string specifyCommandName )
//        {
//            try
//            {
//                bolIsUpdatingBindControlStatus = true;
//                if( specifyCommandName != null )
//                {
//                    specifyCommandName = specifyCommandName.Trim();
//                    if( specifyCommandName.Length == 0 )
//                    {
//                        specifyCommandName = null ;
//                    }
//                }
                
//                ArrayList controls = new ArrayList(_BindingControls.Keys);
//                foreach (object control in controls)
//                {
//                    if (IsControlDisposed(control))
//                    {
//                        _BindingControls.Remove(control);
//                        continue;
//                    }
//                    string name = _BindingControls[control];
                    
//                    if (specifyCommandName != null 
//                        && string.Compare(specifyCommandName, name, true) != 0)
//                    {
//                        continue;
//                    }
//                    WriterCommandBase cmd = this.OwnerCommandCenter.GetCommand(name);
//                    if (cmd != null)
//                    {
//                        UpdateControlStates(control, cmd);
//                    }
//                }//foreach
//            }
//            finally
//            {
//                bolIsUpdatingBindControlStatus = false;
//            }
//        }

//        public static bool IsControlDisposed(object control)
//        {
//            if (control is System.Windows.Forms.Control)
//            {
//                return ((System.Windows.Forms.Control)control).IsDisposed;
//            }
//            if (control is System.Windows.Forms.ToolStripItem)
//            {
//                return ((System.Windows.Forms.ToolStripItem)control).IsDisposed;
//            }
//            return false;
//        }

//        private void UpdateControlStates(object control, WriterCommandBase command)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }
//            WriterCommandEventArgs args = new WriterCommandEventArgs(
//                this.OwnerCommandCenter.EditControl, 
//                this.OwnerCommandCenter.Document , 
//                WriterCommandEventMode.QueryState );
//            args.UIElement = control;
//            command.Invoke(args);
//            if (control is System.Windows.Forms.Control)
//            {
//                System.Windows.Forms.Control ctl = (System.Windows.Forms.Control)control;
//                if (command == null)
//                {
//                    ctl.Enabled = false;
//                    ctl.Visible = false;
//                }
//                else
//                {
//                    ctl.Enabled = args.Enabled;
//                    ctl.Visible = args.Visible;
//                    if (ctl is CheckBox)
//                    {
//                        ((CheckBox)ctl).Checked = args.Checked;
//                    }
//                }
//            }
//            else if (control is System.Windows.Forms.ToolStripItem)
//            {
//                System.Windows.Forms.ToolStripItem item = (System.Windows.Forms.ToolStripItem)control;
//                if (command == null)
//                {
//                    item.Enabled = false;
//                    item.Visible = false;
//                }
//                else
//                {
//                    item.Enabled = args.Enabled;
//                    item.Visible = args.Visible;
//                    if (item is System.Windows.Forms.ToolStripButton)
//                    {
//                        ((ToolStripButton)item).Checked = args.Checked;
//                    }
//                    else if (item is ToolStripMenuItem)
//                    {
//                        ((ToolStripMenuItem)item).Checked = args.Checked;
//                    }
//                }
//            }
//            else if (control is MenuItem)
//            {
//                MenuItem item = (MenuItem)control;
//                if (command == null)
//                {
//                    item.Enabled = false;
//                    item.Visible = false;
//                }
//                else
//                {
//                    item.Enabled = args.Enabled;
//                    item.Visible = args.Visible;
//                    item.Checked = args.Checked;
//                }
//            }
//            args.Mode = WriterCommandEventMode.UpdateUIElement;
//            args.UIElement = control;
//            command.Invoke(args);
//        }


//    }
//}
