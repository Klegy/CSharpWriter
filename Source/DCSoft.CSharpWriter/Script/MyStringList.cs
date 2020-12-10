/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;

namespace DCSoft.Script
{
    /// <summary>
    /// string list , would not contains empty , null or same text, ignore case.
    /// </summary>
    [Serializable()]
    public class MyStringList : System.Collections.CollectionBase
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public MyStringList()
        {
        }

        public string this[int index]
        {
            get
            {
                return (string)this.List[index];
            }
        }

        public int Add(string item)
        {
            if (item == null || item.Length == 0)
                return -1;
            if (IndexOf(item) >= 0)
                return -1;
            return this.List.Add(item);
        }

        public bool Contains(string item)
        {
            return this.IndexOf(item) >= 0;
        }

        public MyStringList Clone()
        {
            MyStringList list = new MyStringList();
            foreach (string item in this)
            {
                list.List.Add(item);
            }
            return list;
        }
        public int IndexOf(string item)
        {
            for (int iCount = 0; iCount < this.List.Count; iCount++)
            {
                if (string.Compare((string)(this.List[iCount]), item, true) == 0)
                    return iCount;
            }
            return -1;
        }

    }
}
