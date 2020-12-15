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
using DCSoft.Common;
using System.Xml.Serialization;
using System.ComponentModel;

namespace DCSoft.Drawing
{

    [Serializable()]
    [System.Diagnostics.DebuggerDisplay("Count={ Count }")]
    [System.Diagnostics.DebuggerTypeProxy(typeof(ListDebugView))]
    public class ContentStyleList : System.Collections.CollectionBase
    {
        public ContentStyle this[int index]
        {
            get
            {
                return (ContentStyle)this.List[index];
            }
            set
            {
                this.List[index] = value;
            }
        }

        //public ContentStyle this[string name]
        //{
        //    get
        //    {
        //        if (name == null || name.Trim().Length == 0)
        //        {
        //            throw new ArgumentNullException("name");
        //        }
        //        foreach (ContentStyle item in this)
        //        {
        //            if (item.Name == name)
        //            {
        //                return item;
        //            }
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new ArgumentNullException("value");
        //        }
        //        if (name == null || name.Trim().Length == 0)
        //        {
        //            throw new ArgumentNullException("name");
        //        }
        //        int index = IndexOf(name);
        //        if (index >= 0)
        //        {
        //            this.List[index] = value;
        //        }
        //        else
        //        {
        //            this.List.Add(value);
        //        }
        //    }
        //}

        public int Add(ContentStyle item)
        {
            return this.List.Add(item);
        }

        public void Remove(ContentStyle item)
        {
            this.List.Remove(item);
        }

        public int IndexOf(ContentStyle item)
        {
            return this.List.IndexOf(item);
        }

        //public int IndexOf(string name)
        //{
        //    for (int iCount = 0; iCount < this.Count; iCount++)
        //    {
        //        if (((ContentStyle)this.List[iCount]).Name == name)
        //        {
        //            return iCount;
        //        }
        //    }
        //    return -1;
        //}

        public int IndexOfExt(ContentStyle item)
        {
            int index = this.List.IndexOf(item);
            if (index >= 0)
            {
                return index;
            }
            for (int iCount = 0; iCount < this.Count; iCount++)
            {
                if (((ContentStyle)this.List[iCount]).EqualsStyleValue(item))
                {
                    return iCount;
                }
            }
            return -1;
        }

        public void UpdateStyleIndex()
        {
            for (int iCount = 0; iCount < this.Count; iCount++)
            {
                this[iCount].Index = iCount;
            }
        }

        /// <summary>
        /// 修复字体名称
        /// </summary>
        public void FixFontName()
        {
            foreach ( ContentStyle style in this )
            {
                XFontValue f = style.Font;
                if (f.FixFontName())
                {
                    // 修复错误的字体名称
                    style.FontName = f.Name;
                }
            }//foreach
        }
    }
}
