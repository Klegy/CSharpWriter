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
using DCSoft.CSharpWriter.Dom ;

namespace DCSoft.CSharpWriter.Commands
{
    public abstract class XTextElementProperties
    {
        private DomDocument _Document = null;
        /// <summary>
        /// 文档对象
        /// </summary>
        public DomDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }
        /// <summary>
        /// 根据内容设置创建元素
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <returns>创建的元素</returns>
        public abstract DomElement CreateElement(DomDocument document);
        /// <summary>
        /// 为新增元素而显示用户界面来编辑对象数据
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>用户操作是否成功</returns>
        public abstract bool PromptNewElement(WriterCommandEventArgs args);
        /// <summary>
        /// 为修改已有的元素的属性而显示用户界面
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>用户操作是否成功</returns>
        public abstract bool PromptEditProperties(WriterCommandEventArgs args);
        /// <summary>
        /// 读取元素的属性值到本对象中
        /// </summary>
        /// <param name="element">文档元素对象</param>
        /// <returns>操作是否成功</returns>
        public abstract bool ReadProperties(DomElement element);
        /// <summary>
        /// 根据对象数据设置文档元素
        /// </summary>
        /// <param name="document">文档对象</param>
        /// <param name="element">要处理的文档元素对象</param>
        /// <param name="logUndo">是否记录撤销操作信息</param>
        /// <returns>操作是否成功</returns>
        public abstract bool ApplyToElement(DomDocument document, DomElement element , bool logUndo );
    }
}
