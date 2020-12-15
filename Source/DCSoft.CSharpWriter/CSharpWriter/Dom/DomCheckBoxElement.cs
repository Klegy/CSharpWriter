using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Xml.Serialization ;
using System.Drawing ;
using DCSoft.Drawing;
using DCSoft.RTF;
using DCSoft.CSharpWriter.RTF;
using DCSoft.Common;
using DCSoft.CSharpWriter.Data;


namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 复选框控件
    /// </summary>
    [XmlType("XTextCheckBox")]
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("CheckBox:Group={GroupName} , Checked={Checked}")]
    [System.Drawing.ToolboxBitmap(typeof(DomCheckBoxElement))]
    //[XTextElementDescriptor(
    //    PropertiesType = typeof(DCSoft.CSharpWriter.Commands.XTextCheckBoxElementProperties))]
    [Editor( 
        typeof( DCSoft.CSharpWriter.Commands.XTextCheckBoxElementEditor ) , 
        typeof( ElementEditor ))]
    public class DomCheckBoxElement : DomCheckBoxElementBase 
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomCheckBoxElement()
        {
        }
                
        //private EventExpressionInfoList _EventExpressions = null;
        ///// <summary>
        ///// 事件表达式列表
        ///// </summary>
        //[DefaultValue(null)]
        //[System.Xml.Serialization.XmlArrayItem("Expression", typeof(EventExpressionInfo))]
        //public EventExpressionInfoList EventExpressions
        //{
        //    get
        //    {
        //        return _EventExpressions;
        //    }
        //    set
        //    {
        //        _EventExpressions = value;
        //    }
        //}
         
        /// <summary>
        /// 触发内容已经改变事件
        /// </summary>
        /// <param name="sender">参数</param>
        /// <param name="args">参数</param>
        public override void OnContentChanged(object sender, ContentChangedEventArgs args)
        {
            base.OnContentChanged(sender, args);

        }
    }
}
