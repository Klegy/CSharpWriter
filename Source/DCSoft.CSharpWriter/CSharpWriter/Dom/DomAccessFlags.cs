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

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 访问文档对象模型时的标记
    /// </summary>
    [Flags]
    public enum DomAccessFlags
    {
        /// <summary>
        /// 没有任何标记
        /// </summary>
        None = 0 ,
        /// <summary>
        /// 检查控件是否只读
        /// </summary>
        CheckControlReadonly= 1 ,
        /// <summary>
        /// 是否检查用户可直接编辑设置
        /// </summary>
        CheckUserEditable = 2 ,
        /// <summary>
        /// 检查输入域是否只读
        /// </summary>
        CheckReadonly = 4 ,
        /// <summary>
        /// 检查用户权限限制
        /// </summary>
        CheckPermission= 8 ,
        /// <summary>
        /// 检查表单视图模式
        /// </summary>
        CheckFormView = 16 ,
        /// <summary>
        /// 检查文档锁定状态
        /// </summary>
        CheckLock = 32,
        ///// <summary>
        ///// 临时性的禁止授权控制
        ///// </summary>
        //ForbitPermission= 64,
        /// <summary>
        /// 所有的标记
        /// </summary>
        Normal = CheckControlReadonly 
            | CheckUserEditable
            | CheckReadonly 
            | CheckPermission 
            | CheckFormView 
            | CheckLock 
    }
}
