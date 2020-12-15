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
using System.Collections;

namespace DCSoft.Common
{
    /// <summary>
    /// IDE调试模式下查看列表类型数据的视图对象
    /// </summary>
    /// <remarks>编制 袁永福</remarks>
    public class ListDebugView
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        /// <param name="instance"></param>
        public ListDebugView(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            myInstance = instance;
        }

        private object myInstance = null;

        /// <summary>
        /// output debug item at design time
        /// </summary>
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.RootHidden)]
        public object Items
        {
            get
            {
                if (myInstance is System.Collections.IEnumerable)
                {
                    ArrayList list = new ArrayList();
                    foreach (object obj in ((IEnumerable)myInstance))
                    {
                        list.Add(obj);
                    }
                    return list.ToArray();
                }
                else
                {
                    return myInstance;
                }
            }
        }
    }
}
