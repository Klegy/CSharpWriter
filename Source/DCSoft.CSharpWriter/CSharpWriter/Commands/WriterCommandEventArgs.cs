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
using DCSoft.CSharpWriter.Controls ;
using System.ComponentModel;
using DCSoft.Common ;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
    public delegate void WriterCommandEventHandler( object sender , WriterCommandEventArgs args );

    public class WriterCommandEventArgs : EventArgs
    {
        public WriterCommandEventArgs()
        {
        }

        public WriterCommandEventArgs(
            CSWriterControl ctl,
            DomDocument document,
            WriterCommandEventMode mode)
        {
            _EditorControl = ctl;
            if (_EditorControl != null)
            {
                _Host = _EditorControl.AppHost;
            }
            _Document = document;
            _Mode = mode;
        }

        private string _Name = null;
        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private WriterAppHost _Host = null;
        /// <summary>
        /// 编辑器宿主对象
        /// </summary>
        public WriterAppHost Host
        {
            get
            {
                return _Host; 
            }
            set
            {
                _Host = value; 
            }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }

        /// <summary>
        /// 文档内容控制器
        /// </summary>
        public DocumentControler DocumentControler
        {
            get
            {
                if (_EditorControl != null)
                {
                    return _EditorControl.DocumentControler;
                }
                if (_Document != null)
                {
                    return _Document.DocumentControler;
                }
                return null;
            }
        }

        [NonSerialized]
        private CSWriterControl _EditorControl = null;
        /// <summary>
        /// 编辑器控件对象
        /// </summary>
        public CSWriterControl EditorControl
        {
            get { return _EditorControl; }
            set { _EditorControl = value; }
        }

        private WriterCommandEventMode _Mode = WriterCommandEventMode.QueryState;
        /// <summary>
        /// 参数模式
        /// </summary>
        public WriterCommandEventMode Mode
        {
            get
            {
                return _Mode; 
            }
            set
            {
                _Mode = value; 
            }
        }


        private bool bolAltKey = false;
        /// <summary>
        /// 用户是否按下了 Alt 键
        /// </summary>
        public bool AltKey
        {
            get
            {
                return bolAltKey;
            }
            set
            {
                bolAltKey = value;
            }
        }

        private bool bolCtlKey = false;
        /// <summary>
        /// 用户是否按下的 Ctl 键
        /// </summary>
        public bool CtlKey
        {
            get
            {
                return bolCtlKey;
            }
            set
            {
                bolCtlKey = value;
            }
        }

        private bool bolShiftKey = false;
        /// <summary>
        /// 用户是否按下了 Shift 键
        /// </summary>
        public bool ShiftKey
        {
            get
            {
                return bolShiftKey;
            }
            set
            {
                bolShiftKey = value;
            }
        }

        private System.Windows.Forms.Keys intKeyCode
            = System.Windows.Forms.Keys.None;
        /// <summary>
        /// 键盘按键值
        /// </summary>
        public System.Windows.Forms.Keys KeyCode
        {
            get
            {
                return intKeyCode;
            }
            set
            {
                intKeyCode = value;
            }
        }

        internal char intKeyChar = char.MinValue;
        /// <summary>
        /// 键盘字符值
        /// </summary>
        public char KeyChar
        {
            get
            {
                return intKeyChar;
            }
        }

        private DomElement _SourceElement = null;
        /// <summary>
        /// 执行动作相关的元素对象
        /// </summary>
        public DomElement SourceElement
        {
            get { return _SourceElement; }
            set { _SourceElement = value; }
        }

        private object _Parameter = null;
        /// <summary>
        /// 相关参数对象
        /// </summary>
        public object Parameter
        {
            get
            {
                return _Parameter; 
            }
            set
            {
                _Parameter = value; 
            }
        }

        private bool _ShowUI = true;
        /// <summary>
        /// 允许显示图形化用户界面
        /// </summary>
        public bool ShowUI
        {
            get 
            {
                return _ShowUI; 
            }
            set
            {
                _ShowUI = value; 
            }
        }


        private object _UIElement = null;
        /// <summary>
        /// 触发动作的用户界面控件对象
        /// </summary>
        public object UIElement
        {
            get { return _UIElement; }
            set { _UIElement = value; }
        }

        private object _UIEventArgs = null;
        /// <summary>
        /// 触发动作时的用户界面事件参数对象
        /// </summary>
        public object UIEventArgs
        {
            get { return _UIEventArgs; }
            set { _UIEventArgs = value; }
        }

        private bool _Enabled = true;
        /// <summary>
        /// 动作是否可用
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        private bool _Visible = true;
        /// <summary>
        /// 对象是否可见
        /// </summary>
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        private bool _Checked = false ;
        /// <summary>
        /// 动作是否处于选择状态
        /// </summary>
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }

        private bool _Actived = false;
        /// <summary>
        /// 动作是否处于激活状态
        /// </summary>
        public bool Actived
        {
            get { return _Actived; }
            set { _Actived = value; }
        }

        private bool _Cancel = false;
        /// <summary>
        /// 取消操作
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _Cancel; 
            }
            set
            {
                _Cancel = value; 
            }
        }

        private object _Result = null;
        /// <summary>
        /// 命令返回值
        /// </summary>
        public object Result
        {
            get
            {
                return _Result; 
            }
            set
            {
                _Result = value; 
            }
        }

        private UIStateRefreshLevel _RefreshLevel = UIStateRefreshLevel.Current ;
        /// <summary>
        /// 用户界面命令控件刷新等级
        /// </summary>
        public UIStateRefreshLevel RefreshLevel
        {
            get
            {
                return _RefreshLevel; 
            }
            set
            {
                _RefreshLevel = value; 
            }
        }

        //private bool _InvalidateUIState = false;
        ///// <summary>
        ///// 让用户界面状态无效
        ///// </summary>
        //public bool InvalidateUIState
        //{
        //    get
        //    {
        //        return _InvalidateUIState; 
        //    }
        //    set
        //    {
        //        _InvalidateUIState = value; 
        //    }
        //}
    }

    /// <summary>
    /// 用户界面命令控件刷新等级
    /// </summary>
    public enum UIStateRefreshLevel
    {
        /// <summary>
        /// 不刷新
        /// </summary>
        None ,
        /// <summary>
        /// 只刷新当前命令绑定的用户界面控件的状态
        /// </summary>
        Current ,
        /// <summary>
        /// 刷新所有的用户界面控件的状态
        /// </summary>
        All 
    }
    /// <summary>
    /// 编辑器动作对象状态
    /// </summary>
    [Serializable()]
    public class WriterCommandState : XDependencyObject
    {
        public WriterCommandState()
        {
        }

        private static XDependencyProperty _Name = XDependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(WriterCommandState),
            null);

        /// <summary>
        /// 动作名称
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Name
        {
            get
            {
                return ( string ) base.GetValue(_Name);
            }
            set
            {
                base.SetValue(_Name, value);
            }
        }

        private static XDependencyProperty _Icon = XDependencyProperty.Register(
            "Icon",
            typeof(XImageValue),
            typeof(WriterCommandState),
            null);

        /// <summary>
        /// 图标
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public DCSoft.Drawing.XImageValue Icon
        {
            get
            {
                return ( XImageValue ) base.GetValue( _Icon ); 
            }
            set
            {
                SetValue(_Icon, value);
            }
        }

        private static XDependencyProperty _Text = XDependencyProperty.Register(
            "Text" ,
            typeof( string ) ,
            typeof( WriterCommandState ),
            null);
        /// <summary>
        /// 动作文本
        /// </summary>
        [DefaultValue(null)]
        public string Text
        {
            get { return ( string )base.GetValue( _Text ); }
            set { base.SetValue(_Text, value); }
        }

        private static XDependencyProperty _Enabled = XDependencyProperty.Register(
            "Enabled",
            typeof(bool),
            typeof(WriterCommandState),
            true);
        /// <summary>
        /// 动作是否可用
        /// </summary>
        [DefaultValue( true )]
        public bool Enabled
        {
            get
            {
                return ( bool ) base.GetValue( _Enabled ); 
            }
            set 
            {
                base.SetValue( _Enabled , value ); 
            }
        }

        private static XDependencyProperty _Visible = XDependencyProperty.Register(
            "Visible",
            typeof(bool),
            typeof(WriterCommandState),
            true);
        /// <summary>
        /// 动作是否可见
        /// </summary>
        [DefaultValue(true)]
        public bool Visible
        {
            get { return ( bool ) base.GetValue( _Visible ); }
            set { base.SetValue( _Visible , value ); }
        }

        private static XDependencyProperty _Checked = XDependencyProperty.Register(
            "Checked",
            typeof(bool),
            typeof(WriterCommandState),
            true);
        /// <summary>
        /// 动作选择状态
        /// </summary>
        [DefaultValue(true)]
        public bool Checked
        {
            get { return (bool)base.GetValue(_Checked); }
            set { base.SetValue(_Checked, value); }
        }


        private static XDependencyProperty _ReferenceCount = XDependencyProperty.Register(
            "ReferenceCount",
            typeof(int),
            typeof(WriterCommandState),
            true);
        /// <summary>
        /// 引用次数
        /// </summary>
        [DefaultValue(0)]
        public int ReferenceCount
        {
            get
            {
                return ( int) GetValue( _ReferenceCount ); 
            }
            set
            {
                base.SetValue( _ReferenceCount , value );  
            }
        }
    }

    /// <summary>
    /// 布尔值
    /// </summary>
    public enum XBooleanValue
    {
        /// <summary>
        /// 真
        /// </summary>
        True ,
        /// <summary>
        /// 假
        /// </summary>
        False ,
        /// <summary>
        /// 默认值
        /// </summary>
        Default 
    }
    

    /// <summary>
    /// 执行动作参数类型
    /// </summary>
    public enum WriterCommandEventMode
    {
        /// <summary>
        /// 初始化用户界面控件
        /// </summary>
        InitalizeUIElement ,
        /// <summary>
        /// 更新用户界面控件
        /// </summary>
        UpdateUIElement ,
        /// <summary>
        /// 查询参数状态
        /// </summary>
        QueryState ,
        /// <summary>
        /// 查询命令激活状态
        /// </summary>
        QueryActive ,
        /// <summary>
        /// 开始执行动作
        /// </summary>
        BeforeExecute ,
        /// <summary>
        /// 执行动作
        /// </summary>
        Invoke ,
        /// <summary>
        /// 结束执行动作
        /// </summary>
        AfterExecute
    }
}
