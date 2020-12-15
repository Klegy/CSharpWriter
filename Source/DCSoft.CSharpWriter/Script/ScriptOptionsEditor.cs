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
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace DCSoft.Script
{
    public class ScriptOptionsEditor : UITypeEditor
    {
        public ScriptOptionsEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider, 
            object Value)
        {
            XVBAOptions opt = Value as XVBAOptions;
            if (opt == null)
            {
                opt = new XVBAOptions();
            }
            else
            {
                opt = opt.Clone();
            }
            IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(
                typeof(IWindowsFormsEditorService));
            using (dlgScriptOptions dlg = new dlgScriptOptions())
            {
                dlg.OptionsInstance = opt;
                if (svc.ShowDialog(dlg) == System.Windows.Forms.DialogResult.OK)
                {
                    return opt;
                }
            }
            return Value;
        }
    }
}
