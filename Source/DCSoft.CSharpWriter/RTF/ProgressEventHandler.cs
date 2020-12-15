/*****************************
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
    /// progress event handler type
    /// </summary>
    /// <param name="sender">sender</param>
    /// <param name="args">event arguments</param>
    public delegate void ProgressEventHandler( object sender , ProgressEventArgs args );

    /// <summary>
    /// porgress event arguments
    /// </summary>
    public class ProgressEventArgs : EventArgs 
    {
        public ProgressEventArgs(int max, int Value, string message)
        {
            intMaxValue = max;
            intValue = Value;
            strMessage = message;
        }

        private int intMaxValue = 0;
        /// <summary>
        /// progress max value
        /// </summary>
        public int MaxValue
        {
            get 
            {
                return intMaxValue; 
            }
        }

        private int intValue = 0;
        /// <summary>
        /// current value
        /// </summary>
        public int Value
        {
            get
            {
                return intValue; 
            }
        }

        private string strMessage = null;
        /// <summary>
        /// progress message
        /// </summary>
        public string Message
        {
            get
            {
                return strMessage; 
            }
        }

        private bool bolCancel = false;
        /// <summary>
        /// cancel operation
        /// </summary>
        public bool Cancel
        {
            get
            {
                return bolCancel; 
            }
            set
            {
                bolCancel = value; 
            }
        }
    }
}
