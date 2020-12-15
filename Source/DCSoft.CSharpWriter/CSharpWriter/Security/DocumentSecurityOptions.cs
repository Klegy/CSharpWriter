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
    /// 授权相关的选项
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable()]
    [TypeConverter(typeof(CommonTypeConverter))]
    public class DocumentSecurityOptions : ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DocumentSecurityOptions()
        {
        }

        private bool _EnablePermission = false;
        /// <summary>
        /// 启用文档内容安全和权限控制。若为false则不启用，文档内容可任意编辑。默认为false。
        /// </summary>
        [DefaultValue(false)]
        public bool EnablePermission
        {
            get
            {
                return _EnablePermission;
            }
            set
            {
                _EnablePermission = value;
            }
        }

        private bool _RealDeleteOwnerContent = true;
        /// <summary>
        /// 用户是物理删除自己曾经输入的内容。默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool RealDeleteOwnerContent
        {
            get
            {
                return _RealDeleteOwnerContent; 
            }
            set
            {
                _RealDeleteOwnerContent = value; 
            }
        }

        private bool _ShowPermissionTip = true;
        /// <summary>
        /// 是否显示授权相关的提示信息，若为true，则在编辑器中当鼠标移动到
        /// 文档内容时，会以提示文本的方式显示文档内容权限和痕迹信息。
        /// 默认为true。
        /// </summary>
        [DefaultValue(true)]
        public bool ShowPermissionTip
        {
            get
            {
                return _ShowPermissionTip;
            }
            set
            {
                _ShowPermissionTip = value;
            }
        }

        private bool _PowerfulSignDocument = true;
        /// <summary>
        /// 使用强权文档签名。若设置为false，则高权限的用户仍然可以修改低权限签名锁定的内容。
        /// 默认为true。
        /// </summary>
        [DefaultValue( true )]
        public bool PowerfulSignDocument
        {
            get
            {
                return _PowerfulSignDocument; 
            }
            set
            {
                _PowerfulSignDocument = value; 
            }
        }
         

        private bool _EnableLogicDelete = false;
        /// <summary>
        /// 执行逻辑删除。默认为false。
        /// </summary>
        [DefaultValue(false)]
        public bool EnableLogicDelete
        {
            get
            {
                return _EnableLogicDelete;
            }
            set
            {
                _EnableLogicDelete = value;
            }
        }

        private bool _ShowLogicDeletedContent = false;
        /// <summary>
        /// 显示被逻辑删除的元素。默认为false。
        /// </summary>
        [DefaultValue(false)]
        public bool ShowLogicDeletedContent
        {
            get
            {
                return _ShowLogicDeletedContent;
            }
            set
            {
                _ShowLogicDeletedContent = value;
            }
        }

        private bool _ShowPermissionMark = false;
        /// <summary>
        /// 是否显示授权标记。若为true，则用蓝色下划线标记出高权限用户输入
        /// 的内容，使用删除线标记出被逻辑删除的内容。
        /// 默认为false。
        /// </summary>
        [DefaultValue(false)]
        public bool ShowPermissionMark
        {
            get
            {
                return _ShowPermissionMark;
            }
            set
            {
                _ShowPermissionMark = value;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public DocumentSecurityOptions Clone()
        {
            return (DocumentSecurityOptions)((ICloneable)this).Clone();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 返回表示对象的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (this.EnablePermission)
            {
                str.Append("Enable ");
            }
            if (this.EnableLogicDelete)
            {
                str.Append("LogicDelete ");
            }
            if (this.ShowLogicDeletedContent)
            {
                str.Append("ShowLogicDeleted ");
            }
            if (this.ShowPermissionMark)
            {
                str.Append("ShowMark ");
            }
            if (this.ShowPermissionTip)
            {
                str.Append("ShowTip ");
            }
            return str.ToString();
        }
    }
}
