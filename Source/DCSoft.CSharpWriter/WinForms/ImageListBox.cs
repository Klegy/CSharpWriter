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
using System.Windows.Forms;
using System.Drawing ;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace DCSoft.WinForms
{
    /// <summary>
    /// 带图片的列表框控件
    /// </summary>
    [System.ComponentModel.ToolboxItem( true )]
    [DefaultProperty("Items")]
    [DefaultEvent("SelectedIndexChagned")]
    public class ImageListBox : UserControl
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ImageListBox()
        {
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private int _ItemHeight = 0;
        /// <summary>
        /// 列表项目的高度，若为0则自动计算
        /// </summary>
        [System.ComponentModel.DefaultValue( 0 )]
        public int ItemHeight
        {
            get
            {
                return _ItemHeight; 
            }
            set
            {
                _ItemHeight = value;
                _RuntimeItemHeight = 0;
                _InvalidateStateFlag = true;
            }
        }

        private bool _InvalidateStateFlag = true ;

        public void CheckInvalidateState()
        {
            if (_InvalidateStateFlag)
            {
                RefreshState();
            }
        }

        public void RefreshState()
        {
            _InvalidateStateFlag = false;
            this.AutoScrollMinSize = new Size(1, this.RuntimeItemHeight * this.Items.Count);
        }

        private int _RuntimeItemHeight = 0;
        /// <summary>
        /// 实际的项目高度
        /// </summary>
        [Browsable( false )]
        public int RuntimeItemHeight
        {
            get
            {
                if (_ItemHeight > 0)
                {
                    return _ItemHeight;
                }
                if (_RuntimeItemHeight <= 0)
                {
                    _RuntimeItemHeight = (int)this.Font.GetHeight();
                    foreach (ImageListBoxItem item in this.Items)
                    {
                        if( item.Image != null )
                        {
                            _RuntimeItemHeight = Math.Max(
                                _RuntimeItemHeight ,
                                item.Image.Height + 3);
                        }
                        else if( this.ImageList != null )
                        {
                            _RuntimeItemHeight = Math.Max(
                                _RuntimeItemHeight ,
                                this.ImageList.ImageSize.Height + 3 );
                        }
                    }//foreach
                }
                return _RuntimeItemHeight; 
            }
        }

        private ImageListBoxItemCollection _Items = new ImageListBoxItemCollection();
        /// <summary>
        /// 列表项目
        /// </summary>
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
        public ImageListBoxItemCollection Items
        {
            get
            {
                if (_Items == null)
                {
                    _Items = new ImageListBoxItemCollection();
                }
                return _Items; 
            }
            set
            {
                _Items = value;
                _InvalidateStateFlag = true;
            }
        }

        private ImageList _ImageList = null;
        /// <summary>
        /// 图标列表对象
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public ImageList ImageList
        {
            get
            {
                return _ImageList; 
            }
            set
            {
                _ImageList = value; 
            }
        }

        private bool _MultiSelect = false;
        /// <summary>
        /// 能否多选
        /// </summary>
        [DefaultValue( false )]
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect; 
            }
            set
            {
                _MultiSelect = value; 
            }
        }

        /// <summary>
        /// 当前列表项目发生改变事件
        /// </summary>
        public event EventHandler SelectedIndexChagned = null;

        public virtual void OnSelectedIndexChanged( EventArgs args )
        {
            if (SelectedIndexChagned != null)
            {
                SelectedIndexChagned(this, args );
            }
        }

        /// <summary>
        /// 当前选中的项目的从0开始的序号,若没有选中任何列表项目则返回-1。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                for(  int iCount = 0 ; iCount < this.Items.Count ; iCount ++ )
                {
                    if (this.Items[iCount].Selected)
                    {
                        return iCount;
                    }
                }
                return -1;
            }
            set
            {
                bool changed = false;
                for (int iCount = 0; iCount < this.Items.Count; iCount++)
                {
                    if (this.Items[iCount].Selected != (iCount == value))
                    {
                        changed = true;
                        this.Items[iCount].Selected = (iCount == value);
                        this.InvalidateItem(this.Items[iCount]);
                    }
                }
                if (changed)
                {
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 当前选中的项目
        /// </summary>
        [Browsable( false )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public ImageListBoxItem SelectedItem
        {
            get
            {
                foreach (ImageListBoxItem item in this.Items)
                {
                    if (item.Selected)
                    {
                        return item;
                    }
                }
                return null;
            }
            set
            {
                bool changed = false;
                foreach (ImageListBoxItem item in this.Items)
                {
                    if (item.Selected != (item == value))
                    {
                        changed = true;
                        item.Selected = (item == value);
                        this.InvalidateItem(item);
                    }
                }
                if (changed)
                {
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 处理鼠标按键按下事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            CheckInvalidateState();
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool changed = false;
                ImageListBoxItem currentItem = GetItem(e.X , e.Y );
                if (this.MultiSelect)
                {
                    if (currentItem != null)
                    {
                        currentItem.Selected = !currentItem.Selected;
                        this.InvalidateItem(currentItem);
                        changed = true;
                    }
                }
                else
                {
                    foreach (ImageListBoxItem item in this.Items)
                    {
                        if (item.Selected != (item == currentItem))
                        {
                            item.Selected = (item == currentItem);
                            this.InvalidateItem(item);
                            changed = true;
                        }
                    }
                }
                if (changed)
                {
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
            base.OnMouseClick(e);
        }

        private void InvalidateItem(ImageListBoxItem item)
        {
            CheckInvalidateState();
            if (item != null)
            {
                this.Invalidate(GetItemBounds(item));
            }
        }

        public Rectangle GetItemBounds(ImageListBoxItem item)
        {
            CheckInvalidateState();
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = this.Items.IndexOf(item);
            if (index >= 0)
            {
                return new Rectangle(
                    1,
                    this.AutoScrollPosition.Y + this.RuntimeItemHeight * index,
                    this.ClientSize.Width-2,
                    this.RuntimeItemHeight);
            }
            else
            {
                throw new ArgumentOutOfRangeException("item not in list");
            }
        }

        /// <summary>
        /// 获得指定位置下的列表项目
        /// </summary>
        /// <param name="x">指定点的X坐标</param>
        /// <param name="y">指定点的Y坐标</param>
        /// <returns>获得的列表项目</returns>
        public ImageListBoxItem GetItem(int x, int y)
        {
            CheckInvalidateState();
            int rh = this.RuntimeItemHeight;
            int index = ( int ) Math.Floor(y * 1.0 / rh);
            if (index >= 0 && index < this.Items.Count)
            {
                return this.Items[index];
            }
            else
            {
                return null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            CheckInvalidateState();
            int rh = this.RuntimeItemHeight;
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                using (SolidBrush brush = new SolidBrush(this.ForeColor))
                {
                    foreach (ImageListBoxItem item in this.Items) 
                    {
                        Rectangle itemBounds = GetItemBounds(item);
                        if (itemBounds.IntersectsWith(e.ClipRectangle) == false)
                        {
                            // 不在剪切矩形中，不绘制该项目
                            continue;
                        }
                        if (item.Selected)
                        {
                            // 若项目被选中，则绘制高亮度背景色
                            e.Graphics.FillRectangle(SystemBrushes.Highlight, itemBounds);
                        }
                        Size imgSize = Size.Empty;
                        if (item.Image != null)
                        {
                            // 绘制自带图标
                            imgSize = item.Image.Size;
                            e.Graphics.DrawImage(
                                item.Image,
                                itemBounds.Left,
                                itemBounds.Top + (itemBounds.Height - imgSize.Height) / 2);
                        }
                        else if (this.ImageList != null)
                        {
                            // 绘制图标列表中的图标
                            imgSize = this.ImageList.ImageSize;
                            if (item.ImageIndex >= 0 && item.ImageIndex < this.ImageList.Images.Count)
                            {
                                this.ImageList.Draw(
                                    e.Graphics,
                                    itemBounds.Left,
                                    itemBounds.Top + (itemBounds.Height - imgSize.Height) / 2,
                                    item.ImageIndex);
                            }
                        }
                        if (item.Text != null && item.Text.Length > 0)
                        {
                            // 绘制文本
                            RectangleF textBounds = new RectangleF(
                                itemBounds.Left + imgSize.Width + 2,
                                itemBounds.Top,
                                itemBounds.Width - imgSize.Width - 2,
                                itemBounds.Height);
                            if (item.Selected)
                            {
                                e.Graphics.DrawString(
                                    item.Text, 
                                    this.Font, 
                                    SystemBrushes.HighlightText,
                                    textBounds,
                                    format);
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    item.Text,
                                    this.Font,
                                    brush,
                                    textBounds,
                                    format);
                            }
                        }
                    }//foreach
                }//using
            }//using
            base.OnPaint(e);
        }
    }//public class ImageListBox : UserControl

    [Serializable]
    [TypeConverter( typeof( ImageListBoxItemCollectionTypeConverter ))]
    public class ImageListBoxItemCollection : List<ImageListBoxItem>
    {
        public override string ToString()
        {
            return this.Count + " items";
        }
    }

    public class ImageListBoxItemCollectionTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals( typeof( InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value)
        {
            if (Value is InstanceDescriptor)
            {
                ImageListBoxItemCollection items = new ImageListBoxItemCollection();
                InstanceDescriptor descriptor = (InstanceDescriptor)Value;
                foreach (object obj in descriptor.Arguments)
                {
                    if (obj is ImageListBoxItem)
                    {
                        items.Add((ImageListBoxItem)obj);
                    }
                }
                return items;
            }
            return base.ConvertFrom(context, culture, Value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if( destinationType.Equals( typeof( InstanceDescriptor )))
            {
                return true ;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value, Type destinationType)
        {
            if( destinationType.Equals( typeof( InstanceDescriptor )))
            {
                ImageListBoxItemCollection items = new ImageListBoxItemCollection();
                InstanceDescriptor descriptor = new InstanceDescriptor( typeof( ImageListBoxItemCollection ) .GetConstructor( new Type[]{ }) , null , false );
                return descriptor ;
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }
    }

    [Serializable()]
    [TypeConverter( typeof( ImageListBoxItemTypeConverter))]
    public class ImageListBoxItem
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public ImageListBoxItem()
        {
        }

        private Image _Image = null;
        /// <summary>
        /// 图片
        /// </summary>
        public Image Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        private int _ImageIndex = 0;
        /// <summary>
        /// 所使用的图标编号
        /// </summary>
        public int ImageIndex
        {
            get { return _ImageIndex; }
            set { _ImageIndex = value; }
        }

        private string _Text = null;
        /// <summary>
        /// 项目文本
        /// </summary>
        [System.ComponentModel.Localizable(true )]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private bool _Selected = false;
        [Browsable( false )]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        [System.Xml.Serialization.XmlIgnore()]
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; }
        }

        private object _Tag = null;
        /// <summary>
        /// 对象附加数据
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.Xml.Serialization.XmlIgnore()]
        public object Tag
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
        /// <summary>
        /// 返回表示对象数据的字符串
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return this.Text;
        }
    }

    /// <summary>
    /// 类型转换器
    /// </summary>
    public class ImageListBoxItemTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(value, attributes);
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals(typeof(InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value)
        {
            if (Value is InstanceDescriptor)
            {
                ImageListBoxItem item = new ImageListBoxItem();
                InstanceDescriptor descriptor = (InstanceDescriptor)Value;
                return item;
            }
            return base.ConvertFrom(context, culture, Value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object Value, Type destinationType)
        {
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                //MessageBox.Show("aa");
                ImageListBoxItem item = (ImageListBoxItem)Value;
                InstanceDescriptor descriptor = new InstanceDescriptor(typeof(ImageListBoxItem).GetConstructor(new Type[] { }), null, false);
                return descriptor;
            }
            return base.ConvertTo(context, culture, Value, destinationType);
        }

    }
}
