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
    /// 用户历史信息列表
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    [Serializable]
    public class UserHistoryInfoList : List<UserHistoryInfo>, ICloneable
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public UserHistoryInfoList()
        {
        }

        /// <summary>
        /// 当前编号
        /// </summary>
        public int CurrentIndex
        {
            get
            {
                return this.Count - 1;
            }
        }
        /// <summary>
        /// 当前授权等级
        /// </summary>
        public int CurrentPermissionLevel
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1].PermissionLevel;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserHistoryInfo CurrentInfo
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[this.Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 刷新元素编号
        /// </summary>
        public void RefreshIndex()
        {
            for (int iCount = 0; iCount < this.Count; iCount++)
            {
                this[iCount].Index = iCount;
            }
        }

        /// <summary>
        /// 获得指定编号的用户历史信息对象
        /// </summary>
        /// <param name="index">编号</param>
        /// <returns>用户历史信息对象</returns>
        public UserHistoryInfo GetInfo(int index)
        {
            if (index < 0 || this.Count == 0)
            {
                return null;
            }
            else if (index >= 0 && index < this.Count)
            {
                return this[index];
            }
            else
            {
                return this[0];
            }
        }

        /// <summary>
        /// 获得指定用户编号的授权许可等级
        /// </summary>
        /// <param name="index">用户编号</param>
        /// <returns>许可等级</returns>
        public int GetPermissionLevel(int index)
        {
            UserHistoryInfo info = GetInfo(index);
            if (info == null)
            {
                return -1;
            }
            else
            {
                return info.PermissionLevel;
            }
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        public UserHistoryInfoList Clone()
        {
            return (UserHistoryInfoList)((ICloneable)this).Clone();
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns>复制品</returns>
        object ICloneable.Clone()
        {
            UserHistoryInfoList list = new UserHistoryInfoList();
            foreach (UserHistoryInfo item in this)
            {
                list.Add(item.Clone());
            }
            return list;
        }
    }
}
