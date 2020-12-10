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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 元素事件列表
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class ElementEventList
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementEventList()
        {
        }

        /// <summary>
        /// 文档元素加载事件
        /// </summary>
        public event EventHandler Load = null;
        /// <summary>
        /// 判断是否存在Load事件
        /// </summary>
        public bool HasLoad
        {
            get
            {
                return Load != null;
            }
        }

        /// <summary>
        /// 触发文档元素加载事件
        /// </summary>
        /// <param name="sender">事件参数</param>
        /// <param name="args">事件参数</param>
        public void RaiseLod(object sender, EventArgs args)
        {
            if (Load != null)
            {
                Load(sender, args);
            }
        }

        /// <summary>
        /// 文档内容发生改变后的事件，该事件用于通知情况，不能取消操作
        /// </summary>
        public event ContentChangedEventHandler ContentChanged = null;
        /// <summary>
        /// 触发 ContentChanging事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void RaiseContentChanged(object sender, ContentChangedEventArgs args)
        {
            if (ContentChanged != null)
            {
                ContentChanged(sender, args);
            }
        }

        /// <summary>
        /// 判断是否存在 ContentChanging 事件
        /// </summary>
        public bool HasContentChanged
        {
            get
            {
                return ContentChanged != null;
            }
        }

        /// <summary>
        /// 文档内容准备发生改变事件，可以使用该参数来取消操作
        /// </summary>
        public event ContentChangingEventHandler ContentChanging = null;
        /// <summary>
        /// 触发 ContentChanging事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void RaiseContentChanging(object sender, ContentChangingEventArgs args)
        {
            if (ContentChanging != null)
            {
                ContentChanging(sender, args);
            }
        }
        /// <summary>
        /// 判断是否存在 ContentChanging 事件
        /// </summary>
        public bool HasContentChanging
        {
            get
            {
                return ContentChanging != null;
            }
        }

        /// <summary>
        /// 文本域获得输入焦点事件
        /// </summary>
        public event EventHandler GotFocus = null;
        /// <summary>
        /// 判断是否存在GetFocus事件
        /// </summary>
        public bool HasGotFocus
        {
            get
            {
                return GotFocus != null;
            }
        }

        /// <summary>
        /// 触发获得输入焦点事件
        /// </summary>
        /// <param name="sender">事件发起者</param>
        /// <param name="args">事件参数</param>
        public void RaiseGotFocus(object sender, EventArgs args)
        {
            if (GotFocus != null)
            {
                GotFocus(sender, args);
            }
        }

        /// <summary>
        /// 文本域失去输入焦点事件
        /// </summary>
        public event EventHandler LostFocus = null;
        /// <summary>
        /// 判断是否存在失去输入焦点事件
        /// </summary>
        public bool HasLostFocus
        {
            get
            {
                return LostFocus != null;
            }
        }

        /// <summary>
        /// 触发失去输入焦点事件
        /// </summary>
        /// <param name="sender">事件发起者</param>
        /// <param name="args">事件参数</param>
        public void RaiseLostFocus(object sender, EventArgs args)
        {
            if (LostFocus != null)
            {
                LostFocus(sender, args);
            }
        }

        /// <summary>
        /// 数据正在验证事件,在该事件处理中可撤销相关操作
        /// </summary>
        public event System.ComponentModel.CancelEventHandler Validating = null;
        /// <summary>
        /// 是否存在Validating事件
        /// </summary>
        public bool HasValidating
        {
            get
            {
                return Validating != null;
            }
        }

        /// <summary>
        /// 触发数据正在验证事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void RaiseValidating(object sender, CancelEventArgs args)
        {
            if (Validating != null)
            {
                Validating(sender, args);
            }
        }

        /// <summary>
        /// 数据验证结束的事件
        /// </summary>
        public event EventHandler Validated = null;
        /// <summary>
        /// 是否存在数据验证结束事件
        /// </summary>
        public bool HasValidated
        {
            get
            {
                return Validated != null;
            }
        }

        /// <summary>
        /// 触发数据验证结束事件
        /// </summary>
        /// <param name="sender">事件发起者</param>
        /// <param name="args">参数</param>
        public void RaiseValidated(object sender, EventArgs args)
        {
            if (Validated != null)
            {
                Validated(sender, args);
            }
        }

        /// <summary>
        /// 鼠标进入事件
        /// </summary>
        public event EventHandler MouseEnter = null;
        /// <summary>
        /// 判断是否存在鼠标进入事件
        /// </summary>
        public bool HasMouseEnter
        {
            get
            {
                return MouseEnter != null;
            }
        }
        /// <summary>
        /// 触发鼠标进入事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public void RaiseMouseEnter(object sender, EventArgs args)
        {
            if (MouseEnter != null)
            {
                MouseEnter(sender, args);
            }
        }

        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        public event EventHandler MouseLeave = null;
        /// <summary>
        /// 判断是否存在鼠标离开事件
        /// </summary>
        public bool HasMouseLeave
        {
            get
            {
                return MouseLeave != null;
            }
        }
        /// <summary>
        /// 触发鼠标离开事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public void RaiseMouseLeave(object sender, EventArgs args)
        {
            if (MouseLeave != null)
            {
                MouseLeave(sender, args);
            }
        }

        ///// <summary>
        ///// 绘制文档内容事件
        ///// </summary>
        //public event DocumentPaintEventHandler Paint = null;
        ///// <summary>
        ///// 鼠标按键松开事件
        ///// </summary>
        //public event DocumentEventHandelr MouseUp = null;
        ///// <summary>
        ///// 鼠标按键按下事件
        ///// </summary>
        //public event DocumentEventHandelr MouseDown = null;
        ///// <summary>
        ///// 鼠标移动事件
        ///// </summary>
        //public event DocumentEventHandelr MouseMove = null;
        ///// <summary>
        ///// 鼠标光标进入事件
        ///// </summary>
        //public event DocumentEventHandelr MouseEnter = null;
        ///// <summary>
        ///// 鼠标光标离开事件
        ///// </summary>
        //public event DocumentEventHandelr MouseLeave = null;
    }
}
