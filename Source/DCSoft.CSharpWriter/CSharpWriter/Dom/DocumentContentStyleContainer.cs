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
using DCSoft.Common;
using DCSoft.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;
using DCSoft.CSharpWriter.Dom.Undo;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 文档样式容器
    /// </summary>
    [Serializable()]
    public class DocumentContentStyleContainer : ContentStyleContainer
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentContentStyleContainer()
        {
        }

        private DomDocument _Document = null;
        /// <summary>
        /// 对象所示文档对象
        /// </summary>
        [Browsable(false)]
        [XmlIgnore()]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal DomDocument Document
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

        //private DocumentContentStyle _Default = new DocumentContentStyle();
        ///// <summary>
        ///// 默认样式
        ///// </summary>
        //public DocumentContentStyle Default
        //{
        //    get
        //    {
        //        return _Default;
        //    }
        //    set
        //    {
        //        _Default = value;
        //        this.RefreshRuntimeStyleList();
        //    }
        //}

        /// <summary>
        /// 默认样式
        /// </summary>
        [System.Xml.Serialization.XmlElement("Default",typeof( DocumentContentStyle ))]
        public override ContentStyle Default
        {
            get
            {
                return base.Default;
            }
            set
            {
                if (value != null && value.GetType().Equals(typeof(ContentStyle)))
                {
                    DocumentContentStyle style = new DocumentContentStyle();
                    XDependencyObject.CopyValueFast(value, style);
                    base.Default = style;
                }
                else
                {
                    base.Default = value;
                }
            }
        }

        /// <summary>
        /// 样式列表
        /// </summary>
        [System.Xml.Serialization.XmlArrayItem("Style", typeof(DocumentContentStyle))]
        public override ContentStyleList Styles
        {
            get
            {
                return base.Styles;
            }
            set
            {
                base.Styles = value;
            }
        }
         
        public override ContentStyle CreateStyleInstance()
        {
            return new DocumentContentStyle();
        }


        /// <summary>
        /// 更新所有的样式对象的内部状态
        /// </summary>
        /// <param name="g"></param>
        public void UpdateState(System.Drawing.Graphics g)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (this.Default != null)
            {
                ((DocumentContentStyle ) this.Default).UpdateState(g);
            }
            //if (this. != null)
            //{
            //    this.Current.UpdateState(g);
            //}
            foreach (DocumentContentStyle style in this.Styles)
            {
                style.UpdateState(g);
            }
            this.ClearRuntimeStyleList();
        }

    }
}
