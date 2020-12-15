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
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Collections;
using DCSoft.CSharpWriter.Commands.Design;
using DCSoft.CSharpWriter.Controls;
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 命令控制对象
    /// </summary>
    /// <remark>本控件用于将用户界面控件的事件转换为对系统命令的调用，并提供设计时的支持。
    /// 本对象支持的用户界面控件有Button、TextBox、ComboBox、Menu、ToolStripItem、ToolStripButton、ToolStripTextBox、ToolStripComboBox、ToolSTripMenuItem等等
    /// 编写 袁永福</remarks>
    [ToolboxBitmap(typeof(CSWriterCommandControler))]
    [ToolboxItem(true)]
    //[ToolboxItemFilter("System.Windows.Forms")]
    [ProvideProperty("CommandName", typeof(System.ComponentModel.Component))]
    public class CSWriterCommandControler : Component, IExtenderProvider, System.ComponentModel.ISupportInitialize
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public CSWriterCommandControler()
        {
            
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="c">组件容器对象</param>
        public CSWriterCommandControler(IContainer c)
            : this()
        {
            if (c != null)
            {
                c.Add(this);
            }
            //_ControlEventHandler = new EventHandler(this.HandleControlExecuteActionEvent);
            //_ControlKeyDownEventHandler = new KeyEventHandler(this.HandleKeyDownEvent);
            //_ControlValueChangedHandler = new EventHandler(this.HandleControlValueChangedEvent);
            //_ControlValidateHandler = new EventHandler(this.HandleValidateEvent);
        }

        private EventHandler _ControlEventHandler = null;
        private EventHandler _ControlValueChangedHandler = null;
        private KeyEventHandler _ControlKeyDownEventHandler = null;
        private EventHandler _ControlValidateHandler = null;
        private EventHandler _ControlDropDownOpeningHandler = null;

        private void CheckEventHandler()
        {
            if (_ControlEventHandler == null)
            {
                _ControlEventHandler = new EventHandler(this.HandleControlExecuteCommandEvent);
                _ControlKeyDownEventHandler = new KeyEventHandler(this.HandleKeyDownEvent);
                _ControlValueChangedHandler = new EventHandler(this.HandleControlValueChangedEvent);
                _ControlValidateHandler = new EventHandler(this.HandleValidateEvent);
                _ControlDropDownOpeningHandler = new EventHandler(this.HandleControlDropDownOpeningEvent);
            }
        }

        [NonSerialized]
        private CSWriterControl _EditControl = null;
        /// <summary>
        /// 文本编辑器控件
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public CSWriterControl EditControl
        {
            get
            {
                return _EditControl; 
            }
            set
            {
                _EditControl = value; 
            }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 正在处理的文档对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DomDocument Document
        {
            get
            {
                return _Document; 
            }
            set
            {
                _Document = value; 
            }
        }


        private CSWriterCommandContainer _CommandContainer = null;
        /// <summary>
        /// 命令容器对象
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CSWriterCommandContainer CommandContainer
        {
            get
            {
                if (_CommandContainer == null)
                {
                    _CommandContainer = new CSWriterCommandContainer();
                }
                return _CommandContainer; 
            }
            set
            {
                _CommandContainer = value; 
            }
        }

        //private WriterCommandCenter _CommandCenter = null;
        ///// <summary>
        ///// 命令中心
        ///// </summary>
        //[System.ComponentModel.Browsable( false )]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        //public WriterCommandCenter CommandCenter
        //{
        //    get
        //    {
        //        return _CommandCenter; 
        //    }
        //    set
        //    {
        //        _CommandCenter = value;
        //    }
        //}

        //private StyleDesignerHost _DesignerHost = null;
        ///// <summary>
        ///// 对象所属的设计器宿主对象
        ///// </summary>
        //public StyleDesignerHost DesignerHost
        //{
        //    get { return _DesignerHost; }
        //    set { _DesignerHost = value; }
        //}

        ///// <summary>
        ///// 获得设计器宿主对象
        ///// </summary>
        ///// <param name="ctl"></param>
        ///// <returns></returns>
        //private StyleDesignerHost GetDesignerHost(object ctl)
        //{
        //    if (_DesignerHost == null)
        //    {
        //        return StyleDesignerHost.GetDesignerHost(ctl);
        //    }
        //    else
        //    {
        //        return _DesignerHost;
        //    }
        //}

        #region IExtenderProvider 成员

        /// <summary>
        /// 判断对象能否被添加扩展属性
        /// </summary>
        /// <param name="extendee">要处理的对象</param>
        /// <returns>能否添加扩展属性</returns>
        public bool CanExtend(object extendee)
        {
            if (extendee is CSWriterCommandControler
                || extendee is CSWriterControl )
                return false;

            //if (extendee is DesignerActionToolStripButton
            //    || extendee is DesignerActionToolStripComboBox
            //    || extendee is DesignerActionToolStripDropDownButton
            //    || extendee is DesignerActionToolStripMenuItem
            //    || extendee is DesignerActionToolStripTextBox
            //    || extendee is DesignerActionContextMenuStrip)
            //    return false;

            return extendee is System.Windows.Forms.Control
                || extendee is System.Windows.Forms.ToolStripItem
                || extendee is System.Windows.Forms.MenuItem;
        }

        #endregion

        /// <summary>
        /// 动作绑定映射表
        /// </summary>
        private Dictionary<object, string> _CommandTable = new Dictionary<object, string>();
        /// <summary>
        /// 动作绑定映射表
        /// </summary>
        internal Dictionary<object, string> CommandTable
        {
            get
            {
                if (_CommandTable == null)
                {
                    _CommandTable = new Dictionary<object, string>();
                }
                return _CommandTable; 
            }
        }

        /// <summary>
        /// 获得绑定的动作名称
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <returns>获得的动作名称</returns>
        [Editor(typeof(WriterCommandNameDlgEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(null)]
        public string GetCommandName(Component ctl)
        {
            if (this.CommandTable.ContainsKey(ctl))
            {
                return this.CommandTable[ctl];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置绑定的动作名称
        /// </summary>
        /// <param name="ctl">控件对象</param>
        /// <param name="actionName">动作名称</param>
        [Editor(typeof(WriterCommandNameDlgEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
        [DefaultValue(null)]
        public void SetCommandName(Component ctl, string actionName)
        {
            if (ctl == null)
            {
                throw new ArgumentNullException("ctl");
            }
            if (actionName != null && actionName.Trim().Length > 0)
            {
                this.CommandTable[ctl] = actionName.Trim();
            }
            else
            {
                if (this.CommandTable.ContainsKey(ctl))
                {
                    this.CommandTable.Remove(ctl);
                }
            }
        }

        private bool bolIsDisposed = false;

        /// <summary>
        /// 对象已经被销毁掉了
        /// </summary>
        [Browsable(false)]
        public bool IsDisposed
        {
            get
            {
                return bolIsDisposed;
            }
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            bolIsDisposed = true;
            foreach (object ctl in _CommandTable.Keys)
            {
                UnBindingEvent(ctl);
            }
            _CommandTable.Clear();
            _CommandContainer = null;
            _HandlingEventControls.Clear();
            _ValueModifiedControls.Clear();
            _ControlEventHandler = null;
            _ControlKeyDownEventHandler = null;
            _ControlValidateHandler = null;
            _ControlValueChangedHandler = null;
            base.Dispose(disposing);
        }

        /// <summary>
        /// 已经绑定了事件的控件对象列表
        /// </summary>
        private ArrayList _BindingEventControls = new ArrayList();

        /// <summary>
        /// 绑定用户界面控件事件
        /// </summary>
        /// <param name="ctl">控件对象</param>
        private void BindingEvent(object ctl)
        {
            CheckEventHandler();
            if (ctl == null)
            {
                throw new ArgumentNullException("ctl");
            }
            if (_BindingEventControls.Contains(ctl))
            {
                return;
            }
            _BindingEventControls.Add(ctl);
            if (ctl is ComboBox)
            {
                // 下拉列表控件
                ComboBox cbo = (ComboBox)ctl;
                cbo.SelectedIndexChanged += _ControlEventHandler;
                cbo.KeyDown += _ControlKeyDownEventHandler;
                cbo.Validated += this._ControlValidateHandler;
                cbo.SelectedValueChanged += this._ControlValueChangedHandler;
                cbo.TextChanged += this._ControlValueChangedHandler;
            }
            else if (ctl is TextBox)
            {
                // 文本框控件
                TextBox txt = (TextBox)ctl;
                if (txt.Multiline == false)
                {
                    txt.KeyDown += this._ControlKeyDownEventHandler;
                }
                txt.Validated += this._ControlValidateHandler;
                txt.TextChanged += this._ControlValueChangedHandler;
            }
            else if (ctl is Button)
            {
                // 按钮控件
                ((Button)ctl).Click += _ControlEventHandler;
            }
            else if (ctl is Control)
            {
                // 其他WinForm控件
                ((Control)ctl).Click += _ControlEventHandler;
            }
            else if (ctl is ToolStripComboBox)
            {
                // 工具条中的下拉列表控件
                ToolStripComboBox cbo = (ToolStripComboBox)ctl;
                cbo.SelectedIndexChanged += _ControlEventHandler;
                cbo.KeyDown += _ControlKeyDownEventHandler;
                cbo.Validated += this._ControlValidateHandler;
                cbo.ComboBox.SelectedValueChanged += this._ControlValueChangedHandler;
                cbo.TextChanged += this._ControlValueChangedHandler;
            }
            else if (ctl is ToolStripTextBox)
            {
                // 工具条中的文本框控件
                ToolStripTextBox txt = (ToolStripTextBox)ctl;
                txt.KeyDown += this._ControlKeyDownEventHandler;
                txt.Validated += this._ControlValidateHandler;
                txt.TextChanged += this._ControlValueChangedHandler;
            }
            else if (ctl is ToolStripItem)
            {
                // 工具条中的项目
                ((ToolStripItem)ctl).Click += _ControlEventHandler;
            }
            else if (ctl is MenuItem)
            {
                // 菜单项目
                ((MenuItem)ctl).Click += _ControlEventHandler;
            }
            
        }

        /// <summary>
        /// 取消绑定用户界面控件事件
        /// </summary>
        /// <param name="ctl">控件对象</param>
        private void UnBindingEvent(object ctl)
        {
            if (ctl == null)
            {
                return;
            }
            try
            {
                CheckEventHandler();
                if (_BindingEventControls.Contains(ctl))
                {
                    _BindingEventControls.Remove(ctl);
                }
                if (ctl is ComboBox)
                {
                    ComboBox cbo = (ComboBox)ctl;
                    cbo.SelectedIndexChanged -= _ControlEventHandler;
                    cbo.KeyDown -= _ControlKeyDownEventHandler;
                    cbo.Validated -= this._ControlValidateHandler;
                    cbo.SelectedValueChanged -= this._ControlValueChangedHandler;
                    cbo.TextChanged -= this._ControlValueChangedHandler;
                }
                else if (ctl is TextBox)
                {
                    TextBox txt = (TextBox)ctl;
                    if (txt.Multiline == false)
                    {
                        txt.KeyDown -= this._ControlKeyDownEventHandler;
                    }
                    txt.Validated -= this._ControlValidateHandler;
                    txt.TextChanged -= this._ControlValueChangedHandler;
                }
                else if (ctl is Control)
                {
                    ((Control)ctl).Click -= _ControlEventHandler;
                }
                else if (ctl is ToolStripTextBox)
                {
                    ToolStripTextBox txt = (ToolStripTextBox)ctl;
                    txt.KeyDown -= this._ControlKeyDownEventHandler;
                    txt.Validated -= this._ControlValidateHandler;
                    txt.TextChanged -= this._ControlValueChangedHandler;
                }
                else if (ctl is ToolStripComboBox)
                {
                    ToolStripComboBox cbo = (ToolStripComboBox)ctl;
                    cbo.SelectedIndexChanged -= _ControlEventHandler;
                    cbo.KeyDown -= _ControlKeyDownEventHandler;
                    cbo.Validated -= this._ControlValidateHandler;
                    if (cbo.ComboBox != null)
                    {
                        cbo.ComboBox.SelectedValueChanged -= this._ControlValueChangedHandler;
                    }
                    cbo.TextChanged -= this._ControlValueChangedHandler;
                }
                else if (ctl is ToolStripItem)
                {
                    ((ToolStripItem)ctl).Click -= _ControlEventHandler;
                }
                else if (ctl is MenuItem)
                {
                    ((MenuItem)ctl).Click -= _ControlEventHandler;
                }
            }
            catch( Exception ext )
            {
                MessageBox.Show(ext.ToString());
                //System.Console.WriteLine(ext.ToString());
            }
        }

        /// <summary>
        /// 数值发生改变的控件列表
        /// </summary>
        private ArrayList _ValueModifiedControls = new ArrayList();

        /// <summary>
        /// 处理控件内容发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleControlValueChangedEvent(object sender, EventArgs args)
        {
            if (sender != null && _ValueModifiedControls.Contains(sender) == false)
            {
                _ValueModifiedControls.Add(sender);
            }
        }

        /// <summary>
        /// 处理显示子菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleControlDropDownOpeningEvent(object sender, EventArgs args)
        {

        }

        /// <summary>
        /// 正在处理用户界面事件的控件列表，使用本列表是为了防止递归调用
        /// </summary>
        private ArrayList _HandlingEventControls = new ArrayList();

        /// <summary>
        /// 处理验证控件数据内容事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleValidateEvent(object sender, EventArgs args)
        {
            if (sender != null)
            {
                if (_ValueModifiedControls.Contains(sender))
                {
                    _ValueModifiedControls.Remove(sender);
                    HandleControlExecuteCommandEvent(sender, args);
                }
            }
        }

        /// <summary>
        /// 处理控件按键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleKeyDownEvent(object sender, KeyEventArgs args)
        {
            if (sender == null || args == null)
            {
                return;
            }

            if (args.KeyCode == Keys.Enter)
            {
                if (_HandlingEventControls.Contains(sender))
                {
                    return;
                }
                _HandlingEventControls.Add(sender);
                try
                {
                    //StyleDesignerHost host = GetDesignerHost(sender);
                    //if (host != null && host.EditControl != null)
                    //{
                    //    host.EditControl.Focus();
                    //}
                    InnerExecuteControlCommand(sender, args);
                }
                finally
                {
                    _HandlingEventControls.Remove(sender);
                }
            }
        }

        /// <summary>
        /// 判断控件是否已经被销毁了
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <returns>是否已经销毁了</returns>
        internal static bool IsControlDisposed(object control)
        {
            if (control is System.Windows.Forms.Control)
            {
                return ((System.Windows.Forms.Control)control).IsDisposed;
            }
            if (control is System.Windows.Forms.ToolStripItem)
            {
                return ((System.Windows.Forms.ToolStripItem)control).IsDisposed;
            }
            return false;
        }

        /// <summary>
        /// 处理控件执行动作事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HandleControlExecuteCommandEvent(object sender, EventArgs args)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (sender == null)
            {
                return;
            }

            if (_CommandTable.ContainsKey(sender) == false)
            {
                return;
            }

            if (IsControlDisposed(sender))
            {
                UnBindingEvent(sender);
                _CommandTable.Remove(sender);
                return;
            }

            if (_HandlingEventControls.Contains(sender))
            {
                return;
            }
            _HandlingEventControls.Add(sender);

            try
            {
                InnerExecuteControlCommand(sender, args);
            }
            finally
            {
                _HandlingEventControls.Remove(sender);
            }
        }

        /// <summary>
        /// 执行指定的控件绑定的动作
        /// </summary>
        /// <param name="control">控件对象</param>
        public void ExecuteControlCommand(object control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (_CommandTable.ContainsKey(control))
            {
                InnerExecuteControlCommand(control, null);
            }
        }

        /// <summary>
        /// 内部的执行控件绑定的动作对象
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="args">用户界面事件参数</param>
        private void InnerExecuteControlCommand(object sender, EventArgs args)
        {
            if (sender == null)
            {
                return;
            }
            if (_ValueModifiedControls.Contains(sender))
            {
                _ValueModifiedControls.Remove(sender);
            }
            string name = _CommandTable[sender];
            if (name == null)
            {
                return;
            }
            name = name.Trim();
            if (name.Length == 0)
            {
                return;
            }
            if (this.CommandContainer == null)
            {
                return;
            }

            if (this.IsUpdatingBindControlStatus)
            {
                return;
            }
             
            WriterCommand cmd = this.CommandContainer.GetCommand(name);
            if (cmd == null)
            {
                return;
            }
            
            InnerExecuteCommand(cmd, sender, args , true , null );
        }


        private bool _IsExecutingCommand = false;
        /// <summary>
        /// 正在执行命令功能
        /// </summary>
        [Browsable(false)]
        public bool IsExecutingCommand
        {
            get
            {
                return _IsExecutingCommand;
            }
        }

        private object InnerExecuteCommand(
            WriterCommand cmd,
            object uiControl,
            EventArgs eventArgs,
            bool showUI,
            object parameter)
        {
            WriterCommandEventArgs cmdArgs = new WriterCommandEventArgs();
            cmdArgs.EditorControl = this.EditControl;
            if (this.EditControl != null)
            {
                cmdArgs.Host = this.EditControl.AppHost;
            }
            cmdArgs.Name = cmd.Name;
            cmdArgs.Document = this.Document;
            cmdArgs.UIElement = uiControl;
            cmdArgs.UIEventArgs = eventArgs ;
            cmdArgs.ShowUI = showUI;
            cmdArgs.Parameter = parameter;
            
            cmdArgs.Mode = WriterCommandEventMode.QueryState ;

            cmd.Invoke(cmdArgs);
            this.ReadControlState(cmd.Name, cmdArgs);
            if (cmdArgs.Enabled)
            {
                _IsExecutingCommand = true;
                cmdArgs.Mode = WriterCommandEventMode.Invoke;
                cmdArgs.Cancel = false;
                if (this.EditControl == null)
                {
                    try
                    {
                        cmd.Invoke(cmdArgs);
                    }
                    finally
                    {
                        _IsExecutingCommand = false;
                    }
                }
                else
                {
                    try
                    {
                        cmd.Invoke(cmdArgs);
                        this.EditControl.OnAfterExecuteCommand(cmdArgs);
                    }
                    catch (Exception ext)
                    {
                        this.EditControl.OnCommandError(cmd, cmdArgs, ext);
                    }
                    finally
                    {
                        _IsExecutingCommand = false;
                    }
                }
                if (cmdArgs.RefreshLevel == UIStateRefreshLevel.Current)
                {
                    // 刷新当前命令绑定的用户界面元素状态
                    this.UpdateBindingControlStatus(cmd.Name);
                }
                else if (cmdArgs.RefreshLevel == UIStateRefreshLevel.All)
                {
                    // 刷新所有的被绑定命令的用户界面元素状态
                    this.UpdateBindingControlStatus();
                }
                return cmdArgs.Result;
            }//if
            return null;
        }


        /// <summary>
        /// 判断指定名称的命令是否可用
        /// </summary>
        /// <param name="commandName">命令名称</param>
        /// <returns>该命令是否可用</returns>
        public bool IsCommandEnabled(string commandName)
        {
            WriterCommand cmd = this.CommandContainer.GetCommand(commandName);
            if (cmd != null)
            {
                WriterCommandEventArgs args = new WriterCommandEventArgs(
                    this.EditControl,
                    this.Document,
                    WriterCommandEventMode.QueryState);
                args.ShowUI = true;
                cmd.Invoke(args);
                return args.Enabled;
            }
            return false;
        }

        /// <summary>
        /// 判断指定名称的命令的状态是否处于选中状态
        /// </summary>
        /// <param name="commandName">命令名称</param>
        /// <returns>该命令是否处于选中状态</returns>
        public bool IsCommandChecked(string commandName)
        {
            WriterCommand cmd = this.CommandContainer.GetCommand(commandName);
            if (cmd != null)
            {
                WriterCommandEventArgs args = new WriterCommandEventArgs(
                    this.EditControl,
                    this.Document,
                    WriterCommandEventMode.QueryState);
                args.ShowUI = true;
                cmd.Invoke(args);
                return args.Checked;
            }
            return false;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandName">命令文本</param>
        /// <param name="showUI">是否允许显示用户界面</param>
        /// <param name="parameter">用户参数</param>
        /// <returns>执行操作后的返回值</returns>
        public object ExecuteCommand(string commandName, bool showUI, object parameter)
        {
            WriterCommand cmd = this.CommandContainer.GetCommand(commandName);
            if (cmd != null)
            {
                return InnerExecuteCommand(cmd, null, null, showUI, parameter);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 启动对象
        /// </summary>
        public void Start()
        {
            if (this.DesignMode == true)
            {
                return;
            }
            WriterCommandEventArgs args = new WriterCommandEventArgs();
            args.EditorControl = this.EditControl;
            args.Document = this.Document;
            this.Document.FixDomState();
            foreach (object control in this._CommandTable.Keys)
            {
                string actionName = _CommandTable[control];
                if (actionName == null || actionName.Trim().Length == 0)
                {
                    continue;
                }
                actionName = actionName.Trim();
                if (_BindingEventControls.Contains(control) == false)
                {
                    BindingEvent(control);
                }
                WriterCommand cmd = this.CommandContainer.GetCommand(actionName);
                if (cmd != null)
                {
                    // 初始化命令绑定的用户界面控件
                    args.UIElement = control;
                    args.Mode = WriterCommandEventMode.InitalizeUIElement;
                    cmd.Invoke(args);
                }
            }//foreach
            this.UpdateBindingControlStatus();
        }


        private bool bolIsUpdatingBindControlStatus = false;
        /// <summary>
        /// 正在更新用户界面控件状态
        /// </summary>
        [Browsable(false)]
        public bool IsUpdatingBindControlStatus
        {
            get
            {
                return bolIsUpdatingBindControlStatus;
            }
        }

        public void UpdateBindingControlStatus()
        {
            UpdateBindingControlStatus(null);
        }

        /// <summary>
        /// 读取控件状态
        /// </summary>
        /// <param name="commandName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool ReadControlState(string commandName, WriterCommandEventArgs args)
        {
            if (this.EditControl == null || this.EditControl.InDesignMode )
            {
                // 编辑器控件不存在获得处于设计模式则不执行后续代码
                return false;
            }

            WriterCommand cmd = this.CommandContainer.GetCommand(commandName);
            if (cmd == null)
            {
                return false;
            }
            bool result = false;
            foreach (object control in this._CommandTable.Keys )
            {
                if (string.Compare(_CommandTable[control], commandName, true) == 0)
                {
                    if (control is System.Windows.Forms.CheckBox)
                    {
                        args.Checked = ((System.Windows.Forms.CheckBox)control).Checked;
                        result = true;
                    }
                    else if (control is RadioButton)
                    {
                        args.Checked = ((RadioButton)control).Checked;
                    }
                    else if (control is System.Windows.Forms.ToolStripButton)
                    {
                        args.Checked = ((System.Windows.Forms.ToolStripButton)control).Checked;
                        result = true;
                    }
                    else if (control is System.Windows.Forms.ToolStripMenuItem)
                    {
                        args.Checked = ((System.Windows.Forms.ToolStripMenuItem)control).Checked;
                        result = true;
                    }
                    else if (control is System.Windows.Forms.MenuItem)
                    {
                        args.Checked = ((System.Windows.Forms.MenuItem)control).Checked;
                        result = true;
                    }
                    else if (control is TextBoxBase)
                    {
                        args.Parameter = ((TextBox)control).Text;
                        result = true;
                    }
                    else if (control is ToolStripTextBox)
                    {
                        args.Parameter = ((ToolStripTextBox)control).Text;
                        result = true;
                    }
                    else if (control is ToolStripComboBox)
                    {
                        args.Parameter = ((ToolStripComboBox)control).Text;
                        result = true;
                    }
                }
            }//foreach
            return result;
        }

        public void UpdateBindingControlStatus(string specifyCommandName)
        {
            try
            {
                bolIsUpdatingBindControlStatus = true;
                if (specifyCommandName != null)
                {
                    specifyCommandName = specifyCommandName.Trim();
                    if (specifyCommandName.Length == 0)
                    {
                        specifyCommandName = null;
                    }
                }

                ArrayList controls = new ArrayList(_CommandTable.Keys );
                foreach (object control in controls)
                {
                    if (IsControlDisposed(control))
                    {
                        _CommandTable.Remove(control);
                        continue;
                    }
                    string name = _CommandTable[control];

                    if (specifyCommandName != null
                        && string.Compare(specifyCommandName, name, true) != 0)
                    {
                        continue;
                    }
                    WriterCommand cmd = this.CommandContainer.GetCommand(name);
                    if (cmd != null)
                    {
                        UpdateControlStates(control, cmd);
                    }
                }//foreach
            }
            finally
            {
                bolIsUpdatingBindControlStatus = false;
            }
        }


        private void UpdateControlStates(object control, WriterCommand command)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            WriterCommandEventArgs args = new WriterCommandEventArgs(
                this.EditControl,
                this.Document,
                WriterCommandEventMode.QueryState);
            args.UIElement = control;
            command.Invoke(args);
            if (control is System.Windows.Forms.Control)
            {
                System.Windows.Forms.Control ctl = (System.Windows.Forms.Control)control;
                if (command == null)
                {
                    ctl.Enabled = false;
                    ctl.Visible = false;
                }
                else
                {
                    ctl.Enabled = args.Enabled;
                    ctl.Visible = args.Visible;
                    if (ctl is CheckBox)
                    {
                        ((CheckBox)ctl).Checked = args.Checked;
                    }
                    else if (ctl is RadioButton)
                    {
                        ((RadioButton)ctl).Checked = args.Checked;
                    }
                }
            }
            else if (control is System.Windows.Forms.ToolStripItem)
            {
                System.Windows.Forms.ToolStripItem item = 
                    (System.Windows.Forms.ToolStripItem)control;
                if (command == null)
                {
                    item.Enabled = false;
                    item.Visible = false;
                }
                else
                {
                    item.Enabled = args.Enabled;
                    item.Visible = args.Visible;
                    if (item is System.Windows.Forms.ToolStripButton)
                    {
                        ((ToolStripButton)item).Checked = args.Checked;
                    }
                    else if (item is ToolStripMenuItem)
                    {
                        ((ToolStripMenuItem)item).Checked = args.Checked;
                    }
                    else if (item is ToolStripComboBox)
                    {
                        ToolStripComboBox cbo = (ToolStripComboBox)item;
                        cbo.Text = Convert.ToString( args.Parameter);
                    }
                    else if (item is ToolStripTextBox)
                    {
                        ToolStripTextBox txt = (ToolStripTextBox)item;
                        txt.Text = Convert.ToString(args.Parameter);
                    }
                }
            }
            else if (control is MenuItem)
            {
                MenuItem item = (MenuItem)control;
                if (command == null)
                {
                    item.Enabled = false;
                    item.Visible = false;
                }
                else
                {
                    item.Enabled = args.Enabled;
                    item.Visible = args.Visible;
                    item.Checked = args.Checked;
                }
            }
            args.Mode = WriterCommandEventMode.UpdateUIElement;
            args.UIElement = control;
            command.Invoke(args);
        }

        #region ISupportInitialize 成员

        /// <summary>
        /// 开始初始化对象
        /// </summary>
        public void BeginInit()
        {
            //不做任何处理
        }

        /// <summary>
        /// 结束初始化对象
        /// </summary>
        public void EndInit()
        {
            //Start();
        }

        #endregion
    }
}