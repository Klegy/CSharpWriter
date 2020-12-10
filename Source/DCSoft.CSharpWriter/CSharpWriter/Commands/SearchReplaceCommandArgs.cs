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
using System.ComponentModel;

namespace DCSoft.CSharpWriter.Commands
{
    /// <summary>
    /// 查找和替换命令参数
    /// </summary>
    [Serializable]
    public class SearchReplaceCommandArgs
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public SearchReplaceCommandArgs()
        {
        }

        private string _SearchString = null;
        /// <summary>
        /// 要查找的字符串
        /// </summary>
        [DefaultValue( null)]
        public string SearchString
        {
            get
            {
                return _SearchString; 
            }
            set
            {
                _SearchString = value; 
            }
        }

        private bool _EnableReplaceString = false;
        /// <summary>
        /// 是否启用替换模式
        /// </summary>
        [DefaultValue(false)]
        public bool EnableReplaceString
        {
            get
            {
                return _EnableReplaceString; 
            }
            set
            {
                _EnableReplaceString = value; 
            }
        }

        private string _ReplaceString = null;
        /// <summary>
        /// 要替换的字符串
        /// </summary>
        [DefaultValue(null)]
        public string ReplaceString
        {
            get
            {
                return _ReplaceString;
            }
            set
            {
                _ReplaceString = value; 
            }
        }

        private bool _Backward = true;
        /// <summary>
        /// True:向后查找；False:向前查找。
        /// </summary>
        [DefaultValue( false )]
        public bool Backward
        {
            get
            {
                return _Backward; 
            }
            set 
            {
                _Backward = value; 
            }
        }
         
        private bool _IgnoreCase = false;
        /// <summary>
        /// 不区分大小写
        /// </summary>
        public bool IgnoreCase
        {
            get 
            {
                return _IgnoreCase; 
            }
            set
            {
                _IgnoreCase = value; 
            }
        }

        private int _Result = 0;
        /// <summary>
        /// 替换的次数
        /// </summary>
        [DefaultValue( 0 )]
        public int Result
        {
            get
            {
                return _Result; 
            }
            set
            {
                _Result = value; 
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public SearchReplaceCommandArgs Clone()
        {
            return (SearchReplaceCommandArgs)this.MemberwiseClone();
        }
    }
}
