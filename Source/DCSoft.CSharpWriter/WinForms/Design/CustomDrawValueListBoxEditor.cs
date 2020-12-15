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
using System.Drawing.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace DCSoft.WinForms.Design
{
    
    /// <summary>
    /// 能自定义绘制项目的下拉列表样式的属性值编辑器
    /// </summary>
    /// <remarks>编写 袁永福</remarks>
    public class CustomDrawValueListBoxEditor : UITypeEditor
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public CustomDrawValueListBoxEditor()
        {
        }

        private ListControlEditProvider myProvider = null;

        public ListControlEditProvider Provider
        {
            get { return myProvider; }
            set { myProvider = value; }
        }

        /// <summary>
        /// 绘制图区域的大小
        /// </summary>
        public virtual Size BoxSize
        {
            get
            {
                if (myProvider == null)
                    return Size.Empty;
                else
                    return myProvider.BoxSize;
            }
        }

        /// <summary>
        /// 绘制图形的接口
        /// </summary>
        /// <param name="g">图形绘制对象</param>
        /// <param name="Bounds">绘制区域的边界</param>
        /// <param name="Value">要绘制的数据</param>
        /// <param name="context">上下文信息</param>
        protected virtual void DrawValue(
            Graphics g,
            System.Drawing.Rectangle Bounds,
            object Value,
            ITypeDescriptorContext context)
        {
            if (myProvider != null)
            {
                myProvider.Context = context;
                myProvider.DrawValue(g, Bounds, Value);
            }
        }

        /// <summary>
        /// 填充列表框控件的项目列表
        /// </summary>
        /// <param name="ctl">列表框控件</param>
        protected virtual void FillListBox(ListBox ctl, ITypeDescriptorContext context)
        {
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return this.BoxSize.IsEmpty == false;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            DrawValue(e.Graphics, e.Bounds, e.Value, e.Context);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        private ITypeDescriptorContext myContext = null;
        protected ITypeDescriptorContext Context
        {
            get
            {
                return myContext;
            }
            set
            {
                myContext = value;
            }
        }

        private IWindowsFormsEditorService myService = null;
        protected IWindowsFormsEditorService Service
        {
            get
            {
                return myService;
            }
        }

        public override object EditValue(
            ITypeDescriptorContext context,
            IServiceProvider provider,
            object Value)
        {
            myContext = context;
           
            myService = (IWindowsFormsEditorService)provider.GetService(
                typeof(IWindowsFormsEditorService));
            
            //MessageBox.Show(myService.GetType().FullName);

            using (ListBox lst = new ListBox())
            {
                lst.BorderStyle = BorderStyle.None;
                if (myProvider == null)
                {
                    if (this.BoxSize.Height > 0)
                    {
                        lst.DrawMode = DrawMode.OwnerDrawFixed;
                        lst.DrawItem += new DrawItemEventHandler(lst_DrawItem);

                        lst.ItemHeight = this.BoxSize.Height + 5;
                        if (lst.ItemHeight < 20)
                            lst.ItemHeight = 20;
                    }
                    FillListBox(lst, context);
                }
                else
                {
                    myProvider.Context = context;
                    myProvider.Value = Value;
                    myProvider.BindControl(lst);
                }
                //int itemHeight = lst.ItemHeight;
                if (lst.Items.Count == 0)
                {
                    return Value;
                }
                // 计算控件大小
                int MaxWidth = 20;
                using (Graphics g = lst.CreateGraphics())
                {
                    foreach (object obj in lst.Items)
                    {
                        string txt = lst.GetItemText(obj);
                        if (txt != null)
                        {
                            SizeF size = g.MeasureString(
                                txt,
                                lst.Font,
                                10000,
                                StringFormat.GenericDefault);
                            if (MaxWidth < size.Width)
                            {
                                MaxWidth = (int)size.Width;
                            }
                        }
                    }
                }
                //lst.ItemHeight = itemHeight;
                MaxWidth = MaxWidth + this.BoxSize.Width + 30;
                if (MaxWidth < 150)
                    MaxWidth = 150;
                int height = lst.Items.Count * lst.ItemHeight + 5;
                double maxHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height * 0.4;
                if (maxHeight > 400)
                    maxHeight = 400;
                if (height > maxHeight)
                    height = (int)Math.Ceiling(maxHeight / lst.ItemHeight) * lst.ItemHeight + 5;
                lst.Size = new Size(MaxWidth, height);
                if (myProvider != null)
                {
                    foreach (object item in lst.Items)
                    {
                        object v = myProvider.GetItemValue(item);
                        if ( v == Value)
                        {
                            lst.SelectedItem = item;
                            break;
                        }
                        if (v != null && v.Equals(Value))
                        {
                            lst.SelectedItem = item;
                            break;
                        }
                    }
                }
                else
                {
                    lst.SelectedItem = Value;
                }
                lst.SelectedIndexChanged += new EventHandler(lst_SelectedIndexChanged);
                int index = lst.SelectedIndex;
                myService.DropDownControl(lst);
                if (index == lst.SelectedIndex)
                {
                    return Value;
                }
                else
                {
                    object NewValue = null;
                    if (this.Provider == null)
                        NewValue = lst.SelectedItem;
                    else
                        NewValue = this.Provider.GetItemValue(lst.SelectedItem);
                    OnValueChanged( context , provider , NewValue);
                    return NewValue;
                }
            }//using
        }

        protected virtual void OnValueChanged(
            ITypeDescriptorContext context,
            IServiceProvider provider, 
            object NewValue)
        {
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            myService.CloseDropDown();
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lst = (ListBox)sender;
            // 计算图标大小
            System.Drawing.Rectangle rect = new Rectangle(
                e.Bounds.Left + 2,
                e.Bounds.Top + (e.Bounds.Height - this.BoxSize.Height) / 2,
                this.BoxSize.Width,
                this.BoxSize.Height);
            // 绘制项目背景
            e.DrawBackground();
            // 绘制图标
            this.DrawValue(e.Graphics, rect, lst.Items[e.Index], myContext);
            e.Graphics.DrawRectangle(Pens.Black, rect);
            // 绘制项目文本
            string txt = lst.GetItemText(lst.Items[e.Index]);
            if (txt != null)
            {
                using (SolidBrush b = new SolidBrush(e.ForeColor))
                {
                    using (StringFormat f = new StringFormat())
                    {
                        f.Alignment = StringAlignment.Near;
                        f.LineAlignment = StringAlignment.Center;
                        f.FormatFlags = StringFormatFlags.NoWrap;
                        e.Graphics.DrawString(
                            txt,
                            e.Font,
                            b,
                            new RectangleF(
                            rect.Right + 3,
                            e.Bounds.Top,
                            e.Bounds.Width - rect.Right - 3,
                            e.Bounds.Height),
                            f);
                    }//using
                }//using
            }//if
        }


        public void CheckExecuteDesignTimeEntryPoint()
        {
            CheckExecuteDesignTimeEntryPoint(this.Context);
        }

        private static bool bolCheckExecuteDesignTimeEntryPoint = false;

        public static void CheckExecuteDesignTimeEntryPoint(ITypeDescriptorContext context)
        {
            if (bolCheckExecuteDesignTimeEntryPoint == false && context != null )
            {
                bolCheckExecuteDesignTimeEntryPoint = true;
                IDesignerHost host = (IDesignerHost)context.GetService(typeof(IDesignerHost));
                if (host != null)
                {
                    ITypeResolutionService typer = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
                    if (typer != null)
                    {
                        Type t = typer.GetType(host.RootComponentClassName, false);
                        if (t != null)
                        {
                            ExecuteDesignTimeEntryPoint(t.Assembly);
                        }
                    }
                }
            }
        }

        public static bool ExecuteDesignTimeEntryPoint(System.Reflection.Assembly asm)
        {
            MethodInfo method = asm.EntryPoint;
            //System.Windows.Forms.MessageBox.Show(method == null ? "NULL" : method.Name);
            if (method != null)
            {
                MethodInfo designMain = method.DeclaringType.GetMethod("DesignTimeMain",
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.DeclaredOnly);
                //System.Windows.Forms.MessageBox.Show( designMain == null ? "NULL" : designMain.Name );
                if (designMain != null)
                {
                    designMain.Invoke(null, null);
                    return true;
                }

            }
            else
            {
                Type t = asm.GetType("DesignTimeMainContainer", false, true);
                if (t != null)
                {
                    MethodInfo designMain = t.GetMethod("DesignTimeMain",
                         BindingFlags.Public |
                         BindingFlags.Static |
                         BindingFlags.DeclaredOnly);
                    if (designMain != null)
                    {
                        designMain.Invoke(null, null);
                        return true;
                    }
                }
            }
            return false;
        }

    }//public class CustomListBoxEditor : UITypeEditor

    public abstract class ListControlEditProvider
    {
        private System.Windows.Forms.ListControl myControl = null;

        public System.Windows.Forms.ListControl Control
        {
            get
            {
                return myControl;
            }
        }

        public void BindControl(System.Windows.Forms.ListControl ctl)
        {
            myControl = ctl;
            if (ctl is ComboBox)
            {
                ComboBox cbo = (ComboBox)ctl;
                if (this.BoxSize.Height > 0)
                {
                    cbo.DrawMode = DrawMode.OwnerDrawVariable;
                    cbo.MeasureItem += new MeasureItemEventHandler(HandleMeasureItem);
                    cbo.DrawItem += new DrawItemEventHandler(HandleDrawItem);
                    cbo.ItemHeight = this.BoxSize.Height + 5;
                }
                else
                {
                    cbo.DrawMode = DrawMode.Normal;
                }
                System.Collections.IEnumerable items = this.GetItems();
                if (items != null)
                {
                    foreach (object obj in items)
                    {
                        cbo.Items.Add(obj);
                    }
                }
            }
            else if (ctl is ListBox)
            {
                ListBox lst = (ListBox)ctl;
                if (this.BoxSize.Height > 0)
                {
                    lst.DrawMode = DrawMode.OwnerDrawVariable;
                    lst.ItemHeight = this.BoxSize.Height + 5;
                    lst.MeasureItem += new MeasureItemEventHandler(HandleMeasureItem);
                    lst.DrawItem += new DrawItemEventHandler(HandleDrawItem);
                }
                else
                {
                    lst.DrawMode = DrawMode.Normal;
                }
                System.Collections.IEnumerable items = this.GetItems();
                if (items != null)
                {
                    foreach (object obj in items)
                    {
                        lst.Items.Add(obj);
                    }
                }
            }
        }

        public void RefreshItems()
        {
            if (myControl is ComboBox)
            {
                ComboBox cbo = (ComboBox)myControl;
                cbo.Items.Clear();
                System.Collections.IEnumerable items = this.GetItems();
                if (items != null)
                {
                    foreach (object obj in items)
                    {
                        cbo.Items.Add(obj);
                    }
                }
            }
            else if (myControl is ListBox)
            {
                ListBox lst = (ListBox)myControl;
                lst.Items.Clear();
                System.Collections.IEnumerable items = this.GetItems();
                if (items != null)
                {
                    foreach (object obj in items)
                    {
                        lst.Items.Add(obj);
                    }
                }
            }
        }

        private object objValue = null;
        /// <summary>
        /// 当前数值
        /// </summary>
        public object Value
        {
            get { return objValue; }
            set { objValue = value; }
        }

        private ITypeDescriptorContext myContext = null;
        /// <summary>
        /// 当前属性编辑器使用的上下文对象
        /// </summary>
        public ITypeDescriptorContext Context
        {
            get
            {
                return myContext;
            }
            set
            {
                myContext = value;
            }
        }

        protected virtual void HandleDrawItem(object sender, DrawItemEventArgs e)
        {
            object v = null;
            if (myControl is ListBox)
                v = ((ListBox)myControl).Items[e.Index];
            else if (myControl is ComboBox)
                v = ((ComboBox)myControl).Items[e.Index];

            // 绘制项目背景
            e.DrawBackground();

            // 计算图标大小
            System.Drawing.Size boxSize = this.BoxSize;
            System.Drawing.Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, 0, e.Bounds.Height);
            if (boxSize.Width > 0 && boxSize.Height > 0)
            {
                rect = new Rectangle(
                    e.Bounds.Left + 2,
                    e.Bounds.Top + (e.Bounds.Height - this.BoxSize.Height) / 2,
                    boxSize.Width,
                    boxSize.Height);
                // 绘制图标
                this.DrawValue(e.Graphics, rect, v);
                e.Graphics.DrawRectangle(Pens.Black, rect);
            }
            // 绘制项目文本
            string txt = GetItemText(v);//myControl.GetItemText(v);
            if (txt != null)
            {
                using (SolidBrush b = new SolidBrush(e.ForeColor))
                {
                    using (StringFormat f = new StringFormat())
                    {
                        f.Alignment = StringAlignment.Near;
                        f.LineAlignment = StringAlignment.Center;
                        f.FormatFlags = StringFormatFlags.NoWrap;
                        e.Graphics.DrawString(
                            txt,
                            e.Font,
                            b,
                            new RectangleF(
                            rect.Right + 3,
                            e.Bounds.Top,
                            e.Bounds.Width - rect.Right - 3,
                            e.Bounds.Height),
                            f);
                    }//using
                }//using
            }//if
        }

        protected virtual void HandleMeasureItem(object sender, MeasureItemEventArgs e)
        {
            System.Drawing.Size size = this.BoxSize;
            if (size.Height > 0)
            {
                e.ItemHeight = size.Height + 5;
            }
            else
            {
                e.ItemHeight = 1+( int ) Math.Ceiling(myControl.Font.GetHeight(e.Graphics));
            }
            //else
            //    e.ItemHeight = 20;
            //e.ItemWidth = this.BoxSize.Width;
            //e.ItemHeight = this.BoxSize.Height;
        }

        public virtual System.Drawing.Size BoxSize
        {
            get
            {
                return System.Drawing.Size.Empty;
            }
        }

        public virtual System.Collections.IEnumerable GetItems()
        {
            return null;
        }

        public virtual void DrawValue(
            System.Drawing.Graphics graphics,
            System.Drawing.Rectangle bounds,
            object Value)
        {
        }

        public virtual object GetItemValue(object item)
        {
            return item;
        }
        public virtual string GetItemText(object item)
        {
            if (item == null)
                return "";
            return Convert.ToString(item);
        }
    }


}
