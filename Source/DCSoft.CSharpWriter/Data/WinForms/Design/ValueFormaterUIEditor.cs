/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace DCSoft.Data.WinForms.Design
{
    /// <summary>
    /// 数据源格式编辑器
    /// </summary>
    public class ValueFormaterUIEditor : System.Drawing.Design.UITypeEditor
    {
        /// <summary>
        /// 返回编辑器类型
        /// </summary>
        /// <param name="context">参数</param>
        /// <returns>类型为下拉列表类型</returns>
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="context">参数</param>
        /// <param name="provider">参数</param>
        /// <param name="Value">旧数值</param>
        /// <returns>新数值</returns>
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object Value)
        {
            if (provider == null)
                return Value;
            System.Windows.Forms.Design.IWindowsFormsEditorService svc = 
                (System.Windows.Forms.Design.IWindowsFormsEditorService)
                provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
            if (svc == null)
                return Value;
            using (dlgFormatDesigner dlg = new dlgFormatDesigner())
            {
                ValueFormater format = Value as ValueFormater;
                if (format == null)
                    format = new ValueFormater();
                else
                    format = format.Clone();
                dlg.InputFormater = format;
                if( svc.ShowDialog( dlg ) == DialogResult.OK )
                {
                    return dlg.InputFormater;
                }
            }
            return Value;
        }
         
    }//public class FormatUIEditor : System.Drawing.Design.UITypeEditor
}