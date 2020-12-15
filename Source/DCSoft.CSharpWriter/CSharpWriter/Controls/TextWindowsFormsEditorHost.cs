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
using System.Windows.Forms;
using DCSoft.WinForms ;
using System.Drawing;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing.Design;
using DCSoft.CSharpWriter.Dom;
using DCSoft.WinForms.Native;

namespace DCSoft.CSharpWriter.Controls
{
    public class TextWindowsFormsEditorHost : 
        IWindowsFormsEditorService ,
        IDisposable,
        ITypeDescriptorContext 
    {

        private CSWriterControl _EditControl = null;
        /// <summary>
        /// 编辑器对象
        /// </summary>
        public CSWriterControl EditControl
        {
            get
            {
                return _EditControl; 
            }
            set
            {
                _EditControl = value;
                if (_EditControl != null)
                {
                    ApplicationStyle style = DCSoft.WinForms.Utils.GetApplicationStyle(_EditControl);

                    ForceFousePopupControl = ( style == ApplicationStyle.WinForm);
                }
            }
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
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

        //private Dictionary<Type, ElementValueEditor> _ElementValueEditors = new Dictionary<Type, ElementValueEditor>();
        ///// <summary>
        ///// 文档元素编辑器列表
        ///// </summary>
        //public Dictionary<Type, ElementValueEditor> ElementValueEditors
        //{
        //    get
        //    {
        //        return _ElementValueEditors; 
        //    }
        //}

        private XPopupForm _PopupForm = null;

        private static Dictionary<Type, ElementValueEditor> _Editors
            = new Dictionary<Type, ElementValueEditor>();

        public virtual ElementValueEditor GetEditor(Type elementType)
        {
            if (_Editors.ContainsKey(elementType))
            {
                return _Editors[elementType];
            }
            else
            {
                return null;
            }
        }

        public virtual void SetEditor(Type elementType, ElementValueEditor editor)
        {
            _Editors[elementType] = editor;
        }

        private ElementValueEditContext _CurrentEditContext = null;
        /// <summary>
        /// 当前正在执行的文档元素值编辑操作上下文对象
        /// </summary>
        public ElementValueEditContext CurrentEditContext
        {
            get
            {
                return _CurrentEditContext; 
            }
        }

        /// <summary>
        /// 编辑文档元素数值
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <param name="editor">编辑器对象</param>
        /// <returns>操作是否成功</returns>
        public ElementValueEditResult EditValue(DomElement element, ElementValueEditor editor)
        {
            if (editor == null)
            {
                throw new ArgumentNullException("editor");
            }
            this._ElementInstance = element;
            ElementValueEditContext context = new ElementValueEditContext();
            context.Document = this.Document;
            context.Element = element ;
            context.PropertyName = null;
            context.Editor = editor;
            try
            {
                _CurrentEditContext = context;
                _CurrentEditContext.EditStyle = editor.GetEditStyle(this, context);
                ElementValueEditResult result = editor.EditValue(this, context);
                return result;
            }
            finally
            {
                _CurrentEditContext = null;
            }
        }

        /// <summary>
        /// 取消当前编辑操作
        /// </summary>
        public void CancelEditValue()
        {
            if (this.CurrentEditContext != null)
            {
                if (_PopupForm != null)
                {
                    _PopupForm.CloseList();
                }
            }
        }

        #region IWindowsFormsEditorService 成员

        public void CloseDropDown()
        {
            if (_PopupForm != null)
            {
                _PopupForm.CloseList();
            }
        }

        private static bool _ForceFousePopupControl = false;
        /// <summary>
        /// 判断应用程序是不是WPF应用程序
        /// </summary>
        public static bool ForceFousePopupControl
        {
            get
            {
                return _ForceFousePopupControl; 
            }
            set
            {
                _ForceFousePopupControl = value; 
            }
        }

        private bool _UserAccept = false;
        /// <summary>
        /// 用户是否确认操作
        /// </summary>
        public bool UserAccept
        {
            get 
            {
                return _UserAccept; 
            }
            set
            {
                _UserAccept = value; 
            }
        }

        /// <summary>
        /// 更新弹出式下拉列表用户界面的大小
        /// </summary>
        public void UpdateDropDownControlSize()
        {
            if (_PopupForm != null && _PopupForm.Controls.Count > 0)
            {
                Control control = _PopupForm.Controls[0];
                Size size = control.GetPreferredSize(new Size(0, 300));// .Size;
                _PopupForm.ClientSize = new Size(
                    size.Width + 5 + PopupFormSizeFix.Width ,
                    size.Height + 5 + PopupFormSizeFix.Height );

                if (this._ElementInstance is DomElement)
                {
                    DomElement element = (DomElement)this._ElementInstance;
                    if (!(element is DomParagraphFlagElement))
                    {
                        element = element.FirstContentElement;
                    }
                    if (element == null)
                    {
                        return;
                    }
                    Rectangle rect = this._EditControl.GetCompositionRect(
                        element.AbsLeft,
                        element.AbsTop,
                        element.Height);
                    _PopupForm.CompositionRect = rect;
                    _PopupForm.UpdateComposition();
                    //_PopupForm.Invalidate();
                    control.Dock = DockStyle.Fill;
                    control.Refresh();
                    //control.Invalidate();
                }
            }
        }

        private static Size _PopupFormSizeFix = new Size(0, 0);
        /// <summary>
        /// 弹出式的窗体大小修正量
        /// </summary>
        /// <remarks>
        /// 一些应用程序使用了皮肤的功能，此时弹出式的窗体可能强加上皮肤的功能，导致客户区大小设置不正确。
        /// 此时可以使用该属性来对冲掉皮肤对客户区大小的影响。
        /// </remarks>
        public static Size PopupFormSizeFix
        {
            get
            {
                return _PopupFormSizeFix; 
            }
            set
            {
                _PopupFormSizeFix = value; 
            }
        }

        /// <summary>
        /// 弹出下拉列表
        /// </summary>
        /// <param name="control">要显示的数据内容控件</param>
        /// <returns>用户是否确认数据编辑操作</returns>
        public void DropDownControl(Control control)
        {
            _UserAccept = false;
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (_PopupForm == null)
            {
                _PopupForm = new XPopupForm();
                _PopupForm.OwnerControl = this._EditControl;
                //_PopupForm.ControlBox = true;
                
            }
            Size size = control.GetPreferredSize( new Size( 0 , 300 ));// .Size;
            _PopupForm.ClientSize =new Size(
                size.Width + 5 + PopupFormSizeFix.Width ,
                size.Height +5 + PopupFormSizeFix.Height ) ;
            
            WindowInformation info = new WindowInformation(_PopupForm.Handle);
            Rectangle rect2 = info.Bounds;
            Rectangle crect2 = info.ClientBounds;
            _PopupForm.Controls.Clear();
            _PopupForm.Controls.Add(control);
            control.Dock = DockStyle.Fill;
             

            if (_PopupForm.ClientSize.Width < control.Width)
            {

            }
            _PopupForm.AutoClose = true;
            //_PopupForm.CanGetFocus = true;
            //_PopupForm.Visible = true;
            //_PopupForm.Show();
            ImeMode back = ImeMode.NoControl;
            if (this._EditControl != null
                && _EditControl.IsHandleCreated
                && _EditControl.IsDisposed == false 
                && _ElementInstance != null )
            {
                _PopupForm.Owner = this._EditControl.FindForm();
                _PopupForm.TopMost = (_PopupForm.Owner != null);

                if (this._ElementInstance is DomElement)
                {
                    DomElement element = (DomElement)this._ElementInstance;
                    if ( ! (element is DomParagraphFlagElement))
                    {
                        element = element.FirstContentElement;
                    }
                    if (element == null)
                    {
                        goto EndFunction;
                    }
                    Rectangle rect = this._EditControl.GetCompositionRect(
                        element.AbsLeft,
                        element.AbsTop,
                        element.Height);
                    _PopupForm.CompositionRect = rect;
                    _PopupForm.UpdateComposition();
                }
                _PopupForm.Show();
                if (control is MonthCalendar)
                {
                    MonthCalendar mc = (MonthCalendar)control;
                    size = mc.Size;
                    _PopupForm.ClientSize = new Size(
                        size.Width + PopupFormSizeFix.Width , 
                        size.Height + PopupFormSizeFix.Height );
                }
                else if (control is DateTimeSelectControl)
                {
                    DateTimeSelectControl dtc = (DateTimeSelectControl)control;
                    size = dtc.GetPreferredSize(Size.Empty);
                    _PopupForm.ClientSize = new Size(
                        size.Width + PopupFormSizeFix.Width,
                        size.Height + PopupFormSizeFix.Height);
                }
                if (_PopupForm.CanGetFocus == false)
                {
                    if (ForceFousePopupControl )
                    {
                        this._EditControl.Focus();
                    }
                }
                back = _EditControl.ImeMode;
                _EditControl.ImeMode = ImeMode.Disable;
            }//if
            _PopupForm.WaitUserSelected();
EndFunction :
            _PopupForm.CloseList();
            if (_PopupForm != null)
            {
                _PopupForm.Controls.Clear();
            }
            if (control != null)
            {
                control.Dispose();
            }
            if (_EditControl != null)
            {
                _EditControl.ImeMode = back;
                _EditControl.Focus();
            }
            
            //_UserAccept = ( _PopupForm.UserProcessState == UserProcessState.Accept );
        }

        public void UpdateComposition(Size clientSize)
        {
            if (_PopupForm != null )
            {
                _PopupForm.ClientSize = new Size(
                    clientSize.Width + PopupFormSizeFix.Width , 
                    clientSize.Height + PopupFormSizeFix.Height );
                _PopupForm.UpdateComposition();
            }
        }

        public DialogResult ShowDialog(Form dialog)
        {
            return dialog.ShowDialog( this.EditControl );
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_PopupForm != null)
            {
                _PopupForm.Dispose();
                _PopupForm = null;
            }
        }

        #endregion

        #region ITypeDescriptorContext 成员

        IContainer ITypeDescriptorContext.Container
        {
            get { return null; }
        }

        private object _ElementInstance = null;

        public object ElementInstance
        {
            get { return _ElementInstance; }
            set { _ElementInstance = value; }
        }

        object ITypeDescriptorContext.Instance
        {
            get { return _ElementInstance; }
        }

        void ITypeDescriptorContext.OnComponentChanged()
        {
            //throw new NotImplementedException();
        }

        bool ITypeDescriptorContext.OnComponentChanging()
        {
            return true;
            //throw new NotImplementedException();
        }

        PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
        {
            get { return null; }
        }

        #endregion

        #region IServiceProvider 成员

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInstanceOfType(this))
            {
                return this;
            }
            Type tt = this.GetType();
            foreach (Type it in tt.GetInterfaces())
            {
                if (it.Equals(serviceType))
                {
                    return this;
                }
            }
            if (this.EditControl != null)
            {
                return this.EditControl.AppHost.Services.GetService(serviceType);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
