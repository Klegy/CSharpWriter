﻿/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */



using System;
using System.Text;

namespace DCSoft.RTF
{
    /// <summary>
    /// RTF element's list
    /// </summary>
    [Serializable()]
    [System.Diagnostics.DebuggerTypeProxy(typeof(RTFInstanceDebugView))]
    public class RTFDomElementList : System.Collections.CollectionBase
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public RTFDomElementList()
        {
        }

        /// <summary>
        ///  get the element at special index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>element</returns>
        public RTFDomElement this[int index]
        {
            get
            {
                return (RTFDomElement)this.List[index];
            }
        }

        /// <summary>
        /// get the last element in the list
        /// </summary>
        public RTFDomElement LastElement
        {
            get
            {
                if (this.Count > 0)
                    return (RTFDomElement)this.List[this.Count - 1];
                else
                    return null;
            }
        }
        /// <summary>
        /// add element
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>index</returns>
        public int Add(RTFDomElement element )
        {
            return this.List.Add( element );
        }
        /// <summary>
        /// insert element
        /// </summary>
        /// <param name="index">special index</param>
        /// <param name="element">element</param>
        public void Insert(int index, RTFDomElement element)
        {
            this.List.Insert(index, element);
        }
        /// <summary>
        /// Get the index of special element that starts with 0.
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>index , if not find element , then return -1</returns>
        public int IndexOf(RTFDomElement element)
        {
            return this.List.IndexOf(element);
        }
        /// <summary>
        /// delete element
        /// </summary>
        /// <param name="node">element</param>
        public void Remove(RTFDomElement node)
        {
            this.List.Remove(node);
        }
        /// <summary>
        /// return element array
        /// </summary>
        /// <returns>array</returns>
        public RTFDomElement[] ToArray()
        {
            return (RTFDomElement[])this.InnerList.ToArray(typeof(RTFDomElement));
        }
    }
}
