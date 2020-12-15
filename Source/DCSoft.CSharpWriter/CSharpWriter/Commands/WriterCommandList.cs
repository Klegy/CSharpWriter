/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.CSharpWriter.Controls ;
using DCSoft.CSharpWriter;
using DCSoft.CSharpWriter.Dom;

namespace DCSoft.CSharpWriter.Commands
{
	/// <summary>
	/// 文本编辑器动作列表
	/// </summary>
    [System.Diagnostics.DebuggerTypeProxy(typeof(DCSoft.Common.ListDebugView))]
	public class WriterCommandList : System.Collections.CollectionBase 
	{
        //private XTextDocument myDocument = null;
        ///// <summary>
        ///// 文档对象
        ///// </summary>
        //public XTextDocument Document
        //{
        //    get
        //    {
        //        return myDocument ;
        //    }
        //    set
        //    {
        //        myDocument = value;
        //    }
        //}

        //private TextDocumentEditControl _EditorControl = null;
        ///// <summary>
        ///// 编辑器控件
        ///// </summary>
        //public TextDocumentEditControl EditorControl
        //{
        //    get
        //    {
        //        return _EditorControl; 
        //    }
        //    set 
        //    {
        //        _EditorControl = value; 
        //    }
        //}

		/// <summary>
		/// 获得指定位置的动作对象
		/// </summary>
		public WriterCommand this[ int index ]
		{
			get
            {
                return ( WriterCommand ) this.List[ index ] ;
            }
		}
		/// <summary>
		/// 获得指定名称的动作对象,名称比较不区分大小写
		/// </summary>
		public WriterCommand this[ string name ]
		{
			get
			{
                if (name == null)
                {
                    return null;
                }
                foreach (WriterCommand a in this)
                {
                    if (string.Compare(a.Name, name, true) == 0)
                    {
                        return a;
                    }
                }
				return null;
			}
		}
		/// <summary>
		/// 添加动作,添加前会删除列表中相同名称的动作
		/// </summary>
		/// <param name="a">动作对象</param>
		/// <returns>动作在列表中的需要</returns>
		public int Add( WriterCommand a )
		{
			WriterCommand old = this[ a.Name ] ;
            if (old != null)
            {
                this.List.Remove(old);
            }
            //a.myDocument = this.Document;
         	return this.List.Add( a );
		}
		/// <summary>
		/// 删除动作
		/// </summary>
		/// <param name="a">要删除的动作</param>
		public void Remove( WriterCommand a )
		{
			this.List.Remove( a );
		}
		/// <summary>
		/// 激活动作
		/// </summary>
		/// <param name="args">文档事件参数</param>
		/// <returns>激活的动作对象</returns>
		public WriterCommand Active( DocumentEventArgs args )
		{
            WriterCommandEventArgs args2 = new WriterCommandEventArgs(
                args.Document.EditorControl,
                args.Document,
                WriterCommandEventMode.QueryState);
            args2.CtlKey = args.CtlKey;
            args2.ShiftKey = args.ShiftKey;
            args2.AltKey = args.AltKey;
            args2.KeyCode = args.KeyCode;
			foreach( WriterCommand a in this )
			{
                a.Invoke(args2);
                if (args2.Enabled && args2.Actived )
                {
                    return a;
                }
			}//foreach
			return null;
		}
	}
}