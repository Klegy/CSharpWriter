﻿/*****************************
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

namespace DCSoft.CSharpWriter.Dom.Undo
{
    /// <summary>
    /// 设置元素样式的重复/撤销操作对象
    /// </summary>
    public class XTextUndoSetStyle : XTextUndoBase 
    {
        private DomElement _Element = null;

        public DomElement Element
        {
            get { return _Element; }
            set { _Element = value; }
        }



        private DocumentContentStyle _NewStyle = null;

        public DocumentContentStyle NewStyle
        {
            get { return _NewStyle; }
            set { _NewStyle = value; }
        }

    }
}
