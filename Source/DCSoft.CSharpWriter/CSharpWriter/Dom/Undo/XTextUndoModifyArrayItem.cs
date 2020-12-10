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
using System.Collections;

namespace DCSoft.CSharpWriter.Dom.Undo
{
    public class XTextUndoModifyArrayItem : XTextUndoBase
    {

        public XTextUndoModifyArrayItem()
        {
        }

        public XTextUndoModifyArrayItem(
            XTextUndoModifyListAction action,
            IList listInstance,
            int index,
            object oldValue,
            object newValue)
        {
            _Action = action;
            _ListInstance = listInstance;
            _Index = index;
            _OldValue = oldValue;
            _NewValue = newValue;
        }

        private XTextUndoModifyListAction _Action = XTextUndoModifyListAction.Change;

        public XTextUndoModifyListAction Action
        {
            get
            {
                return _Action; 
            }
            set
            {
                _Action = value; 
            }
        }

        private System.Collections.IList _ListInstance = null;

        public System.Collections.IList ListInstance
        {
            get 
            {
                return _ListInstance;
            }
            set
            {
                _ListInstance = value;
            }
        }

        private object _OldValue = null;

        public object OldValue
        {
            get
            { 
                return _OldValue; 
            }
            set
            {
                _OldValue = value; 
            }
        }

        private object _NewValue = null;

        public object NewValue
        {
            get
            { 
                return _NewValue;
            }
            set
            {
                _NewValue = value;
            }
        }

        private int _Index = -1;

        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public override void Undo( DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            if (_ListInstance == null)
            {
                throw new ArgumentNullException("ListInstance");
            }
            switch (this.Action)
            {
                case XTextUndoModifyListAction.Insert:
                    {
                        if (this._Index >= 0 && this._Index < _ListInstance.Count)
                        {
                            _ListInstance.RemoveAt(this._Index);
                        }
                        else
                        {
                            _ListInstance.Remove(this._NewValue);
                        }
                    }
                    break;
                case XTextUndoModifyListAction.Remove:
                    _ListInstance.Insert(_Index, _OldValue);
                    break;
                case XTextUndoModifyListAction.Change:
                    if (_Index >= 0 && _Index < _ListInstance.Count)
                    {
                        _ListInstance[_Index] = _OldValue;
                    }
                    break;
                case XTextUndoModifyListAction.InsertRange:
                    {
                        if (_NewValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_NewValue)
                            {
                                _ListInstance.RemoveAt(index);
                            }
                        }
                    }
                    break;
                case XTextUndoModifyListAction.RemoveRange:
                    {
                        if (_OldValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_OldValue)
                            {
                                _ListInstance.Insert(index, obj);
                                index++;
                            }
                        }
                    }
                    break;
                case XTextUndoModifyListAction.ChangeRange:
                    {
                        if (_OldValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_OldValue)
                            {
                                _ListInstance[index] = obj;
                                index++;
                            }
                        }
                    }
                    break;
            }//switch
            if (AfterUndo != null)
            {
                AfterUndo(this, EventArgs.Empty);
            }
        }//Undo

        /// <summary>
        /// 撤销完成后事件
        /// </summary>
        public event EventHandler AfterUndo = null;

        public override void Redo( DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
        {
            if (_ListInstance == null)
            {
                throw new ArgumentNullException("ListInstance");
            }
            switch (this.Action)
            {
                case XTextUndoModifyListAction.Insert:
                    {
                        _ListInstance.Insert(this._Index, this._NewValue);
                    }
                    break;
                case XTextUndoModifyListAction.Remove:
                    if (_OldValue != null)
                    {
                        if (_Index >= 0 && _Index < _ListInstance.Count)
                        {
                            _ListInstance.RemoveAt(_Index);
                        }
                        else
                        {
                            _ListInstance.Remove(_OldValue);
                        }
                    }
                    break;
                case XTextUndoModifyListAction.Change:
                    if (_Index >= 0 && _Index < _ListInstance.Count)
                    {
                        _ListInstance[_Index] = _NewValue;
                    }
                    break;
                case XTextUndoModifyListAction.InsertRange:
                    {
                        if ( _OldValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_OldValue)
                            {
                                _ListInstance.Insert(index, obj);
                                index++;
                            }
                        }
                    }
                    break;
                case XTextUndoModifyListAction.RemoveRange:
                    {
                        if ( _NewValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_NewValue)
                            {
                                _ListInstance.RemoveAt(index);
                            }
                        }
                    }
                    break;
                case XTextUndoModifyListAction.ChangeRange:
                    {
                        if (_NewValue is IEnumerable)
                        {
                            int index = this._Index;
                            foreach (object obj in (IEnumerable)_NewValue)
                            {
                                _ListInstance[index] = obj;
                                index++;
                            }
                        }
                    }
                    break;
            }//switch
            if (AfterRedo != null)
            {
                AfterRedo(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 重复操作完成后事件
        /// </summary>
        public event EventHandler AfterRedo = null;
    }

    public enum XTextUndoModifyListAction
    {
        /// <summary>
        /// 插入新元素
        /// </summary>
        Insert ,
        /// <summary>
        /// 删除元素
        /// </summary>
        Remove ,
        /// <summary>
        /// 修改元素值
        /// </summary>
        Change ,
        /// <summary>
        /// 插入多个元素
        /// </summary>
        InsertRange ,
        /// <summary>
        /// 删除多个元素
        /// </summary>
        RemoveRange ,
        /// <summary>
        /// 修改多个元素值
        /// </summary>
        ChangeRange
    }
}
