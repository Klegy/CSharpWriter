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
using System.ComponentModel;
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.Security
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class UserLoginInfo
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public UserLoginInfo()
        {
        }

        private string _ID = null;
        /// <summary>
        /// 用户编号
        /// </summary>
        [DefaultValue(null)]
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        private string _Name = null;
        /// <summary>
        /// 用户名
        /// </summary>
        [DefaultValue(null)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private int _PermissionLevel = 0;
        /// <summary>
        /// 用户权限许可等级,数值越高，等级就越高，高等级能压制低等级，低等级无法修改高等级。
        /// </summary>
        [DefaultValue(0)]
        public int PermissionLevel
        {
            get
            {
                return _PermissionLevel;
            }
            set
            {
                _PermissionLevel = value;
            }
        }

        private string _Description = null;
        /// <summary>
        /// 登录时附加的说明文字
        /// </summary>
        [DefaultValue(null)]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        private string _Tag = null;
        /// <summary>
        /// 扩展数据
        /// </summary>
        [DefaultValue(null)]
        public string Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }
    }
}
