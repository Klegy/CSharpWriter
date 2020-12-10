/*****************************
CSharpWriter is a RTF style Text writer control written by C#2.0,Currently,
it use <LGPL> license(maybe change later).More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can send to 28348092@qq.com(or yyf9989@hotmail.com).
*****************************///@DCHC@
using System;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Reflection;
using System.Collections;
using System.Collections.Generic ;
using System.Drawing;
using System.Drawing.Design;

namespace DCSoft.CSharpWriter.Commands.Design
{
    public class WriterCommandNameDlgEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (dlgCommandNameEditor dlg = new dlgCommandNameEditor())
            {
                dlg.InputCommandName = Convert.ToString(value);
                dlg.CommandDescriptors = GetCommandDescriptors(context);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OnValueChanged(context, provider, dlg.InputCommandName);
                    return dlg.InputCommandName;
                }
            }
            return value;
        }


        private void OnValueChanged(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object NewValue)
        {
            string name = Convert.ToString(NewValue);
            WriterCommandDescriptor cmd = GetCommandDescriptor( context , name );

            if (cmd != null)
            {
                if (context.Instance is ToolStripItem
                    || context.Instance is Button
                    || context.Instance is MenuItem)
                {
                    IComponentChangeService svc = (IComponentChangeService)context.GetService(
                        typeof(IComponentChangeService));
                    if (cmd.Image != null)
                    {
                        PropertyDescriptor prop = TypeDescriptor.GetProperties(context.Instance)["Image"];
                        svc.OnComponentChanging(
                            context.Instance,
                            prop);
                        object oldValue = prop.GetValue(context.Instance);
                        prop.SetValue(context.Instance, cmd.Image);
                        svc.OnComponentChanged(
                            context.Instance,
                            prop,
                            oldValue,
                            cmd.Image);
                    }
                    if (context.Instance.GetType().GetProperty("Text") != null)
                    {
                        PropertyDescriptor prop = TypeDescriptor.GetProperties(context.Instance)["Text"];

                        string desc = cmd.Description;
                        if (desc != null && desc.Trim().Length > 0)
                        {
                            svc.OnComponentChanging(
                                context.Instance,
                                prop);
                            object oldValue = prop.GetValue(context.Instance);
                            prop.SetValue(context.Instance, desc);
                            svc.OnComponentChanged(
                                context.Instance,
                                prop,
                                oldValue,
                                desc);
                        }
                    }
                }
            }
        }

        private WriterCommandDescriptor GetCommandDescriptor(ITypeDescriptorContext context, string name)
        {
            WriterCommandDescriptor result = null;
            foreach (object item in GetCommandDescriptors(context))
            {
                if (item is WriterCommandDescriptor)
                {
                    if (string.Compare(((WriterCommandDescriptor)item).CommandName, name, true) == 0)
                    {
                        result = (WriterCommandDescriptor)item;
                        break;
                    }
                }
                else if (item is WriterCommandModuleDescriptor)
                {
                    WriterCommandModuleDescriptor module = (WriterCommandModuleDescriptor)item;
                    foreach (WriterCommandDescriptor d in module.Commands)
                    {
                        if (string.Compare(d.CommandName, name, true) == 0)
                        {
                            result = d;
                            break;
                        }
                    }
                    if (result != null)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        private static ArrayList _buffer = null;

        public static ArrayList GetCommandDescriptors(ITypeDescriptorContext context)
        {
            if (_buffer != null)
            {
                return _buffer;
            }
           
            ArrayList types = new ArrayList();
            try
            {
                types.Add(typeof(DCSoft.CSharpWriter.Commands.WriterCommandModuleBrowse));
                types.Add(typeof(DCSoft.CSharpWriter.Commands.WriterCommandModuleEdit));
                types.Add(typeof(DCSoft.CSharpWriter.Commands.WriterCommandModuleFile));
               

                //foreach (Type t in typeof(WriterCommandModule).Assembly.GetTypes())
                //{
                //    if (t.IsSubclassOf(typeof(WriterCommandModule)))
                //    {
                //        types.Add(t);
                //    }
                //    else if (t.IsSubclassOf(typeof(WriterCommand)))
                //    {
                //        types.Add(t);
                //    }
                //}
            }
            catch (Exception ext)
            {
                MessageBox.Show(ext.ToString());
            }
            //MessageBox.Show(td == null ? "null" : td.GetType().FullName);
            ITypeDiscoveryService td = (ITypeDiscoveryService)context.GetService(typeof(ITypeDiscoveryService));
            if (td != null)
            {
                //MessageBox.Show(td.GetType().FullName + " " + td.GetType().Assembly.Location );
                ICollection acts = td.GetTypes(typeof(WriterCommand), false);
                //MessageBox.Show(acts == null ? "-1" : acts.Count.ToString());
                if (acts != null)
                {
                    foreach (Type t in acts)
                    {
                        if (types.Contains(t) == false)
                        {
                            types.Add(t);
                        }
                    }

                    //types.AddRange(acts);
                }//if

                acts = td.GetTypes(typeof(WriterCommandModule), false);
                //MessageBox.Show(acts == null ? "no module" : acts.Count.ToString());
                if (acts != null)
                {
                    foreach (Type t in acts)
                    {
                        if (types.Contains(t) == false)
                        {
                            types.Add(t);
                        }
                    }
                    //types.AddRange(acts);
                }
            }//if
            _buffer = GetCommandDescriptors((Type[])types.ToArray(typeof(Type)));
            return _buffer;
        }

        public static ArrayList GetCommandDescriptors(Type[] types )
        {
            ArrayList cmds = new ArrayList();
            ArrayList mdls = new ArrayList();

            //StringBuilder str = new StringBuilder();
            //str.AppendLine("-------------");
            //foreach (Type t in types)
            //{
            //    str.AppendLine("#" + t.FullName);
            //}
            //str.AppendLine("-------------");
            //MessageBox.Show(str.ToString());

            foreach (Type type in types)
            {
                if( type.IsSubclassOf( typeof( WriterCommand ))
                    && type.Equals( typeof( WriterCommandDelegate )) == false )
                {
                    WriterCommandDescriptor cmd = WriterCommandDescriptor.Create(type, false);
                    if (cmd != null)
                    {
                        cmds.Add(cmd);
                    }
                }
                else if (type.IsSubclassOf(typeof(WriterCommandModule)))
                {
                    WriterCommandModuleDescriptor mdl = WriterCommandModuleDescriptor.Create(type, false);
                    if (mdl != null)
                    {
                        mdls.Add(mdl);
                    }
                }
            }
            cmds.Sort(new CommandDescriptorNameComparer());
            mdls.Sort(new CommandDescriptorNameComparer());
            ArrayList result = new ArrayList();

            result.AddRange(cmds);
            result.AddRange(mdls);

            return result;
        }

        private class CommandDescriptorNameComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                if (x is WriterCommandDescriptor)
                {
                    return string.Compare(
                        ((WriterCommandDescriptor)x).CommandName,
                        ((WriterCommandDescriptor)y).CommandName,
                        true);
                }
                else if (x is WriterCommandModuleDescriptor)
                {
                    WriterCommandModuleDescriptor d1 = (WriterCommandModuleDescriptor)x;
                    WriterCommandModuleDescriptor d2 = (WriterCommandModuleDescriptor)y;
                    return string.Compare(d1.Name, d2.Name, true);
                }
                return 0;
            }
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            WriterCommandDescriptor cmd = GetCommandDescriptor(e.Context, Convert.ToString(e.Value));
            if (cmd != null && cmd.Image != null)
            {
                e.Graphics.DrawImage(cmd.Image, e.Bounds.Left, e.Bounds.Top);
            }
            base.PaintValue(e);
        }
    }

    ///// <summary>
    ///// 动作名编辑器
    ///// </summary>
    //public class WriterCommandNameEditor : DCSoft.WinForms.Design.CustomDrawValueListBoxEditor
    //{
    //    public override System.Drawing.Size BoxSize
    //    {
    //        get
    //        {
    //            return new System.Drawing.Size(16, 16);
    //        }
    //    }

    //    //private static System.Collections.Hashtable myTypeActions = new System.Collections.Hashtable();

    //    //private class MyCommandDescriptor : System.Collections.Generic.IComparer<MyCommandDescriptor >
    //    //{
    //    //    public Type CommandType = null;
    //    //    public string ModuleName = null;
    //    //    public string Name = null;
    //    //    public string Text = null;
    //    //    public System.Drawing.Image Image = null;

    //    //    #region IComparer<MyCommandDescriptor> 成员

    //    //    public int Compare(MyCommandDescriptor x, MyCommandDescriptor y)
    //    //    {
    //    //        int result = string.Compare(x.ModuleName, y.ModuleName);
    //    //        if (result == 0)
    //    //        {
    //    //            result = string.Compare(x.Name, y.Name);
    //    //        }
    //    //        return result;
    //    //    }

    //    //    #endregion
    //    //}

    //    private static List<WriterCommandDescriptor> _Commands = null;

    //    //private static bool bolInit = false;
    //    private void CheckCommands(ITypeDescriptorContext context)
    //    {
    //    }

    //    internal static WriterCommandCenter GetDesignCommandCenter(ITypeDescriptorContext context)
    //    {

    //        ArrayList result = new ArrayList();

    //        ITypeDiscoveryService td = (ITypeDiscoveryService)context.GetService(typeof(ITypeDiscoveryService));
    //        //MessageBox.Show(td == null ? "null" : td.GetType().FullName);
    //        if (td != null)
    //        {
    //            ICollection acts = td.GetTypes(typeof(WriterCommandBase), false);
    //            //MessageBox.Show(acts == null ? "-1" : acts.Count.ToString());
    //            if (acts != null)
    //            {
    //                foreach (Type act in acts)
    //                {
    //                    WriterCommandDescriptor wcd = WriterCommandDescriptionAttribute.CreateDescriptor(act);
    //                    result.Add(wcd);
    //                }//foreach
    //            }//if
    //            acts = td.GetTypes(typeof(WriterCommandModule), false);
    //            if (acts != null)
    //            {
    //                foreach (Type grp in acts)
    //                {
    //                    List<WriterCommandDescriptor> list = WriterCommandDescriptionAttribute.CreateDescriptors(grp);
    //                    result.AddRange(list);
    //                }
    //            }
    //        }//if
    //        result.Sort(new NameComparer());
    //        return result;
    //    }

    //    private class NameComparer : System.Collections.Generic.IComparer<WriterCommandDescriptor>
    //    {
    //        #region IComparer<WriterCommandDescriptor> 成员

    //        public int Compare(WriterCommandDescriptor x, WriterCommandDescriptor y)
    //        {
    //            int result = string.Compare(x.ModuleName, y.ModuleName);
    //            if (result == 0)
    //            {
    //                result = string.Compare(x.CommandName, y.CommandName);
    //            }
    //            return result;
    //            throw new NotImplementedException();
    //        }

    //        #endregion
    //    }

    //    //private DesignerActionList myActions = null;

    //    protected override void FillListBox(ListBox ctl, ITypeDescriptorContext context)
    //    {
    //        CheckCommands(context);
    //        ctl.DisplayMember = "Name";
    //        System.Collections.ArrayList list = new System.Collections.ArrayList();
    //        list.AddRange(_Commands);
    //        foreach (WriterCommandDescriptor action in list)
    //        {
    //            ctl.Items.Add(action.CommandName);
    //        }
    //    }

    //    protected override void DrawValue(
    //        System.Drawing.Graphics g,
    //        System.Drawing.Rectangle Bounds,
    //        object Value,
    //        ITypeDescriptorContext context)
    //    {
    //        base.Context = context;
    //        CheckCommands(context);

    //        string name = Convert.ToString(Value);
    //        if (name != null)
    //        {
    //            foreach (WriterCommandDescriptor cmd in _Commands)
    //            {
    //                if (string.Compare(cmd.CommandName, name, true) == 0)
    //                {
    //                    if (cmd.Image != null)
    //                    {
    //                        g.DrawImage(cmd.Image, Bounds.Left, Bounds.Top);
    //                    }
    //                    break;
    //                }
    //            }
    //        }
    //    }

    //    protected override void OnValueChanged(
    //        ITypeDescriptorContext context,
    //        IServiceProvider provider,
    //        object NewValue)
    //    {
    //        CheckCommands(context);
    //        string name = Convert.ToString(NewValue);
    //        WriterCommandDescriptor cmd = null;
    //        foreach (WriterCommandDescriptor item in _Commands)
    //        {
    //            if (string.Compare(item.CommandName, name, true) == 0)
    //            {
    //                cmd = item;
    //                break;
    //            }
    //        }
    //        if (cmd != null)
    //        {
    //            if (context.Instance is ToolStripItem
    //                || context.Instance is Button
    //                || context.Instance is MenuItem)
    //            {
    //                IComponentChangeService svc = (IComponentChangeService)base.Context.GetService(
    //                    typeof(IComponentChangeService));
    //                if (cmd.Image != null)
    //                {
    //                    PropertyDescriptor prop = TypeDescriptor.GetProperties(context.Instance)["Image"];
    //                    svc.OnComponentChanging(
    //                        context.Instance,
    //                        prop);
    //                    object oldValue = prop.GetValue(context.Instance);
    //                    prop.SetValue(context.Instance, cmd.Image);
    //                    svc.OnComponentChanged(
    //                        context.Instance,
    //                        prop,
    //                        oldValue,
    //                        cmd.Image);
    //                }
    //                if (context.Instance.GetType().GetProperty("Text") != null)
    //                {
    //                    PropertyDescriptor prop = TypeDescriptor.GetProperties(context.Instance)["Text"];

    //                    string desc = cmd.Description;
    //                    if (desc != null && desc.Trim().Length > 0)
    //                    {
    //                        svc.OnComponentChanging(
    //                            context.Instance,
    //                            prop);
    //                        object oldValue = prop.GetValue(context.Instance);
    //                        prop.SetValue(context.Instance, desc);
    //                        svc.OnComponentChanged(
    //                            context.Instance,
    //                            prop,
    //                            oldValue,
    //                            desc);
    //                    }
    //                }
    //            }
    //        }
    //        base.OnValueChanged(context, provider, NewValue);
    //    }


    //}

}
