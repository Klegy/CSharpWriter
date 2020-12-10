/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Windows.Forms;

namespace DCSoft.CSharpWriter.Controls
{
    [Serializable()]
    public class KeyState : ICloneable
    {
        public static KeyState GetCurrentState()
        {
            KeyState result = new KeyState();
            System.Windows.Forms.Keys key = System.Windows.Forms.Control.ModifierKeys;
            result._Alt = ((key & Keys.Alt) == Keys.Alt);
            result._Control = ((key & Keys.Control) == Keys.Control);
            result._Shift = ((key & Keys.Shift) == Keys.Shift);
            return result;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        public KeyState()
        {
        }

        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="args">事件参数</param>
        public KeyState(System.Windows.Forms.KeyEventArgs args)
        {
            _Control = args.Control;
            _Alt = args.Alt;
            _Shift = args.Shift;
            _Key = args.KeyCode;
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="key">按键值</param>
        public KeyState(System.Windows.Forms.Keys key)
        {
            _Control = ((key & Keys.Control) == Keys.Control);
            _Alt = ((key & Keys.Alt) == Keys.Alt);
            _Shift = ((key & Keys.Shift) == Keys.Shift);
            _Key = ( key & Keys.KeyCode );
        }

        private bool _Control = false;
        /// <summary>
        /// Control键是否按下
        /// </summary>
        [System.ComponentModel.DefaultValue( false )]
        public bool Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        private bool _Alt = false;
        /// <summary>
        /// 是否按下Alt键
        /// </summary>
        [DefaultValue( false )]
        public bool Alt
        {
            get { return _Alt; }
            set { _Alt = value; }
        }

        private bool _Shift = false;
        /// <summary>
        /// 是否按下Shift键
        /// </summary>
        [DefaultValue( false )]
        public bool Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
        }

        private System.Windows.Forms.Keys _Key = System.Windows.Forms.Keys.None;
        /// <summary>
        /// 按键值
        /// </summary>
        [DefaultValue( System.Windows.Forms.Keys.None )]
        public System.Windows.Forms.Keys Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (_Control)
            {
                str.Append("Control ");
            }
            if (_Shift)
            {
                str.Append("Shift ");
            }
            if (_Alt)
            {
                str.Append("Alt ");
            }
            str.Append(_Key.ToString());
            return str.ToString();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            KeyState result = new KeyState();
            result._Alt = this._Alt;
            result._Control = this._Control;
            result._Key = this._Key;
            result._Shift = this._Shift;
            return result;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public KeyState Clone()
        {
            return (KeyState)((ICloneable)this).Clone();
        }
    }
}
