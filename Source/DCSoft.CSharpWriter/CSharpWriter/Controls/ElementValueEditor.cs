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
using DCSoft.CSharpWriter.Dom ;
using System.Windows.Forms;
using DCSoft.WinForms;
using DCSoft.Common;

namespace DCSoft.CSharpWriter.Controls
{
    /// <summary>
    /// 元素属性值编辑相关的上下文
    /// </summary>
    public class ElementValueEditContext
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementValueEditContext()
        {
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

        private DomElement _Element = null;
        /// <summary>
        /// 当前编辑的元素对象
        /// </summary>
        public DomElement Element
        {
            get 
            {
                return _Element; 
            }
            set
            {
                _Element = value; 
            }
        }

        private string _PropertyName = null;
        /// <summary>
        /// 编辑的属性名
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _PropertyName; 
            }
            set
            {
                _PropertyName = value; 
            }
        }

        private ElementValueEditor _Editor = null;
        /// <summary>
        /// 正在运行的文档元素值编辑器
        /// </summary>
        public ElementValueEditor Editor
        {
            get { return _Editor; }
            set { _Editor = value; }
        }

        private ElementValueEditorEditStyle _EditStyle = ElementValueEditorEditStyle.None;
        /// <summary>
        /// 正在使用的编辑器编辑样式
        /// </summary>
        public ElementValueEditorEditStyle EditStyle
        {
            get { return _EditStyle; }
            set { _EditStyle = value; }
        }
    }
    public abstract class ElementValueEditor
    {
        /// <summary>
        /// 编辑元素数值
        /// </summary>
        /// <param name="host">编辑器宿主对象</param>
        /// <param name="context">上下文对象</param>
        /// <param name="Value">要编辑的旧数值</param>
        /// <returns>编辑后的新数值</returns>
        public virtual ElementValueEditResult EditValue(
            TextWindowsFormsEditorHost host,
            ElementValueEditContext context )
        {
            return ElementValueEditResult.None ;
        }

        /// <summary>
        /// 获得编辑元素的方式
        /// </summary>
        /// <param name="host">编辑器宿主</param>
        /// <param name="context">编辑上下文对象</param>
        /// <returns>编辑的方式</returns>
        public virtual ElementValueEditorEditStyle GetEditStyle(
            TextWindowsFormsEditorHost host,
            ElementValueEditContext context)
        {
            return ElementValueEditorEditStyle.None;
        }
    }

    /// <summary>
    /// 数据编辑器对象列表
    /// </summary>
    public class ElementValueEditorContainer : List<ElementValueEditor>
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ElementValueEditorContainer()
        {
        }
        /// <summary>
        /// 获得指定类型的编辑器
        /// </summary>
        /// <param name="editorType">编辑器类型</param>
        /// <returns>获得的编辑器对象实例</returns>
        public ElementValueEditor this[Type editorType]
        {
            get
            {
                if (editorType == null)
                {
                    throw new ArgumentNullException("editorType");
                }
                foreach (ElementValueEditor item in this)
                {
                    if (item.GetType().Equals(editorType))
                    {
                        return item;
                    }
                }
                foreach (ElementValueEditor item in this)
                {
                    if ( item.GetType().IsSubclassOf(editorType))
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }

    

    //public class DateTimeElementValueEditor : ElementValueEditor
    //{
    //    public override bool EditValue(
    //        TextWindowsFormsEditorHost host,
    //        ElementValueEditContext context)
    //    {
    //        using ( MonthCalendar cld = new MonthCalendar())
    //        {
    //            DateTime dtm = DateTime.Now;
    //            if (DateTime.TryParse( context.Element.Text, out dtm) == false)
    //            {
    //                dtm = DateTime.Now;
    //            }
    //            cld.TodayDate = dtm;
    //            bool modified = false ;
    //            cld.DateSelected += delegate( object sender , DateRangeEventArgs args )
    //                {
    //                    dtm = cld.SelectionStart ;
    //                    modified = true;
    //                    host.CloseDropDown();
    //                };
    //            host.DropDownControl(cld);
    //            if (modified)
    //            {
    //                host.Document.BeginLogUndo();
    //                context.Element.Text = dtm.ToString();
    //                host.Document.EndLogUndo();
    //                //host.Document.OnSelectionChanged();
    //                return true;
    //            }
    //            return false ;
    //        }
    //    }

    //    void cld_DateSelected(object sender, DateRangeEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //        //System.Drawing.Design.UITypeEditorEditStyle.
    //    }
    //}

    /// <summary>
    /// 编辑器编辑模式
    /// </summary>
    public enum ElementValueEditorEditStyle
    {
        /// <summary>
        /// 无编辑器
        /// </summary>
        None ,
        /// <summary>
        /// 下拉列表模式
        /// </summary>
        DropDown ,
        /// <summary>
        /// 对话框模式
        /// </summary>
        Modal
    }
}
