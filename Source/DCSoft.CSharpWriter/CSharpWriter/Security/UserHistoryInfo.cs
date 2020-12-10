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
using System.Xml.Serialization;

namespace DCSoft.CSharpWriter.Security
{
    /// <summary>
    /// 用户历史信息
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    public class UserHistoryInfo : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public UserHistoryInfo()
        {
        }

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="info"></param>
        public UserHistoryInfo(UserLoginInfo info)
        {
            if (info != null)
            {
                this.ID = info.ID;
                this.Name = info.Name;
                this.PermissionLevel = info.PermissionLevel;
                this.SavedTime = NullDateTime;
                this.Description = info.Description;
                this.Tag = info.Tag;
            }
        }


        private int _Index = 0;
        /// <summary>
        /// 对象编号
        /// </summary>
        [System.ComponentModel.DefaultValue(0)]
        [System.Xml.Serialization.XmlAttribute()]
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
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

        private DateTime _SavedTime = NullDateTime;
        /// <summary>
        /// 保存文档的时间
        /// </summary>
        public DateTime SavedTime
        {
            get
            {
                return _SavedTime;
            }
            set
            {
                _SavedTime = value;
            }
        }

        /// <summary>
        /// 表示保存时间的字符串
        /// </summary>
        [Browsable(false)]
        public string SaveTimeString
        {
            get
            {
                if (_SavedTime == NullDateTime)
                {
                    return "";
                }
                else
                {
                    return _SavedTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

        private int _PermissionLevel = 0;
        /// <summary>
        /// 用户权限许可等级
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
        /// 附加的说明文字
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

        public static DateTime NullDateTime = new DateTime(1980, 1, 1);

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public UserHistoryInfo Clone()
        {
            return (UserHistoryInfo)((ICloneable)this).Clone();
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
