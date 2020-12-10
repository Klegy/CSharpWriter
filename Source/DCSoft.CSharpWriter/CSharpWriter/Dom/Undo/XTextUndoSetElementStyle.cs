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

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 撤销、重复设置段落样式的对象
    /// </summary>
    public class XTextUndoSetElementStyle : XTextUndoBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XTextUndoSetElementStyle()
        {
        }

        private bool _ParagraphStyle = false;
        /// <summary>
        /// 设置段落样式
        /// </summary>
        public bool ParagraphStyle
        {
            get { return _ParagraphStyle; }
            set { _ParagraphStyle = value; }
        }

        private Dictionary<DomElement, int> _OldStyleIndex = new Dictionary<DomElement, int>();
        private Dictionary<DomElement, int> _NewStyleIndex = new Dictionary<DomElement, int>();

        public void AddInfo(DomElement element, int oldStyleIndex, int newStyleIndex)
        {
            _OldStyleIndex[element] = oldStyleIndex;
            _NewStyleIndex[element] = newStyleIndex;
        }

        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            if (_NewStyleIndex.Count > 0)
            {
                if (this.ParagraphStyle)
                {
                    this.Document.EditorSetParagraphStyle(_NewStyleIndex, false);
                }
                else
                {
                    this.Document.EditorSetElementStyle(_NewStyleIndex, false);
                }
            }
        }

        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            if (_OldStyleIndex.Count > 0)
            {
                if (this.ParagraphStyle)
                {
                    this.Document.EditorSetParagraphStyle(_OldStyleIndex, false);
                }
                else
                {
                    this.Document.EditorSetElementStyle(_OldStyleIndex, false);
                }
            }
        }
    }
}
