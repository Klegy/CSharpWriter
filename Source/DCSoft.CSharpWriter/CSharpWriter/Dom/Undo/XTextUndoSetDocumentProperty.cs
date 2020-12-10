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
using DCSoft.CSharpWriter.Dom;
using DCSoft.CSharpWriter.Dom.Undo;
using DCSoft.CSharpWriter.Undo;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 设置文档属性
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    internal class XTextUndoSetDocumentProperty : XTextUndoBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="oldValue">旧属性值</param>
        /// <param name="newValue">新属性值</param>
        public XTextUndoSetDocumentProperty(DomDocument document , string propertyName, object oldValue , object newValue)
        {
            _Document = document;
            _PropertyName = propertyName;
            _OldValue = oldValue;
            _NewValue = newValue;
        }
        private DomDocument _Document = null;
        private string _PropertyName = null;
        private object _NewValue = null;
        private object _OldValue = null;

        public override void Undo( XUndoEventArgs args)
        {
            switch (_PropertyName)
            {
                case "DefaultStyle":
                    _Document.EditorSetDefaultStyle((DocumentContentStyle)_OldValue, false);
                    break;
            }
        }

        public override void Redo(XUndoEventArgs args)
        {
            switch (_PropertyName)
            {
                case "DefaultStyle":
                    _Document.EditorSetDefaultStyle((DocumentContentStyle)_NewValue, false);
                    break;
            }
        }

    }
}
