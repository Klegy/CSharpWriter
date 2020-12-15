/*****************************
CSharpWriter is a RTF style Text writer control written by C#,Currently,
it use <LGPL> license.More than RichTextBox, 
It is provide a DOM to access every thing in document and save in XML format.
It can use in WinForm.NET ,WPF,Console application.Any idea about CSharpWriter 
can write to 28348092@qq.com(or yyf9989@hotmail.com). 
Project web site is [https://github.com/dcsoft-yyf/CSharpWriter].
*****************************///@DCHC@
using System;
using DCSoft.WinForms;
using DCSoft.Drawing;
using DCSoft.CSharpWriter.Dom.Undo ;
using DCSoft.WinForms.Native;
using System.Drawing;

namespace DCSoft.CSharpWriter.Dom
{
	/// <summary>
	/// 嵌入在文档中的对象基础类型
	/// </summary>
    [Serializable]
	public class DomObjectElement : DomElement
	{
        public static int DragBoxSize = 20 ;

		/// <summary>
		/// 初始化对象
		/// </summary>
		public DomObjectElement()
		{
		}

        /// <summary>
        /// 预览内容用的图片，用于快速在用户界面上绘制文档内容，打印的时候则尽量不用
        /// </summary>
        public virtual System.Drawing.Image PreviewImage
        {
            get
            {
                return null;
            }
        }

		/// <summary>
		/// 对象宽度和高度的比例,若大于等于0.1则该设置有效，否则无效
		/// </summary>
		private double dblWidthHeightRate = 0 ;

		/// <summary>
		/// 对象宽度高度比,若大于等于0.1则该设置有效，否则无效
		/// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
		public virtual double WidthHeightRate
		{
			get
            {
                return dblWidthHeightRate ;
            }
			set
            {
                dblWidthHeightRate = value ;
            }
		}

		private bool bolCanResize = true ;
		/// <summary>
		/// 用户是否可以改变对象大小
		/// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public virtual bool CanResize
		{
			get
            {
                return bolCanResize ;
            }
			set
            {
                bolCanResize = value;
            }
		}

		/// <summary>
		/// 对象名称
		/// </summary>
		private string _Name = null;
		/// <summary>
		/// 对象名称
		/// </summary>
		public string Name
		{
			get
            {
                return _Name ;
            }
			set
            {
                _Name = value;
            }
		}

        private bool _Enabled = true;
        /// <summary>
        /// 对象是否可用,可以接受鼠标键盘事件
        /// </summary>
        [System.ComponentModel.DefaultValue( true )]
        public bool Enabled
        {
            get
            {
                return _Enabled; 
            }
            set
            {
                _Enabled = value; 
            }
        }

        private string _Tag = null;
        /// <summary>
        /// 附加数据
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Tag
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
		/// 创建一个拖拽矩形对象
		/// </summary>
		/// <returns>新的拖拽矩形对象</returns>
		public virtual DragRectangle CreateDragRectangle()
		{
			DragRectangle.DragRectSize = DragBoxSize ;
			DragRectangle rect = new DragRectangle(
				new System.Drawing.Rectangle( 
				(int)0 ,
                (int)0,
                (int)this.Width,
                (int)this.Height),
				true );
			rect.CanMove = true ;
            rect.LineDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			rect.CanResize = this.CanResize && this.OwnerDocument.DocumentControler.CanModify( 
                this ,
                DomAccessFlags.Normal ) ;
			rect.Focus = true ;// myOwnerDocument.Content.CurrentElement == this && myOwnerDocument.Content.SelectionLength == 1 ;
            if (rect.CanResize)
            {
                if (this.OwnerDocument.Options.SecurityOptions.EnablePermission)
                {
                    // 进行权限判断用户能否改变元素大小
                    if (this.Style.DeleterIndex >= 0)
                    {
                        rect.CanResize = false;
                    }
                    else if (this.CreatorPermessionLevel > this.OwnerDocument.UserHistories.CurrentPermissionLevel)
                    {
                        rect.CanResize = false;
                    }
                }
            }
			return rect ;
		}

        //public override void RefreshView(DocumentPaintEventArgs  args)
        //{
        //    base.DrawBackground( args);
        //    this.DrawContent( args  );
        //    if( ShowDragRect )
        //    {
        //        DragRectangle dr = this.CreateDragRectangle(); 
        //        dr.RefreshView( args.Graphics );
        //    }
        //}

        /// <summary>
        /// 绘制对象
        /// </summary>
        /// <param name="args"></param>
        public override void Draw(DocumentPaintEventArgs args)
        {
            args.Render.DrawBackground(this, args);
            this.DrawContent(args);
            if (args.RenderStyle == DocumentRenderStyle.Paint && args.ActiveMode)
            {
                if (this.ShowDragRect && this.Enabled )
                {
                    DragRectangle dr = this.CreateDragRectangle();
                    dr.Bounds = new Rectangle(
                        (int)this.AbsLeft,
                        (int)this.AbsTop,
                        dr.Bounds.Width,
                        dr.Bounds.Height);
                    dr.RefreshView(args.Graphics);
                }
            }
            System.Drawing.RectangleF bounds = this.AbsBounds ;
            bounds.Width = bounds.Width - 1;
            bounds.Height = bounds.Height - 1;
            args.Render.RenderBorder(this, args, bounds);
        }

		/// <summary>
		/// 进行控制点测试
		/// </summary>
		/// <param name="x">测试点X坐标</param>
		/// <param name="y">测试点Y坐标</param>
		/// <returns>测试点所在控制点编号</returns>
		protected virtual DragPointStyle GetDragHit( int x , int y )
		{
			DragRectangle dr = this.CreateDragRectangle(); 
			if( dr == null )
				return DragPointStyle.None ;
			else
				return dr.DragHit( x , y );
		}

        /// <summary>
        /// 处理文档用户界面事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.MouseDown )
            {
                if (this.Enabled)
                {
                    if (this.OwnerDocument.EditorControl != null)
                    {
                        if (this.OwnerDocument.EditorControl.MouseDragScroll)
                        {
                            return;
                        }
                    }
                    DragPointStyle hit = this.GetDragHit(args.X, args.Y);
                    if (this.ShowDragRect)
                    {
                        if (hit >= 0)
                        {
                            DragBounds(hit);
                            args.CancelBubble = true;
                            if (this.OwnerDocument.EditorControl != null)
                            {
                                this.OwnerDocument.EditorControl.UpdateTextCaret();
                            }
                            return;
                        }
                    }
                    //if (this.AbsBounds.Contains(args.X, args.Y))
                    {
                        this.OwnerDocument.CurrentContentElement.SetSelection(this.ViewIndex, 1);
                        args.CancelBubble = true;
                    }
                }
            }
            else if (args.Style == DocumentEventStyles.MouseMove)
            {
                if (this.Enabled)
                {
                    if (this.ShowDragRect)
                    {
                        DragRectangle dr = this.CreateDragRectangle();
                        DragPointStyle hit = dr.DragHit(args.X, args.Y);
                        if (hit >= 0)
                        {
                            args.Cursor = DragRectangle.GetMouseCursor(hit);
                            base.HandleDocumentEvent(args);
                            return;
                        }
                    }
                    args.Cursor = System.Windows.Forms.Cursors.Arrow;
                }
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }
         
		/// <summary>
		/// 判断能否使用鼠标拖拽该对象
		/// </summary>
        [System.ComponentModel.Browsable( false )]
		public bool ShowDragRect
		{
			get
			{
                if (this.CanResize == false)
                {
                    return false;
                }
                DomDocumentContentElement dce = this.DocumentContentElement;
                return dce.IsSelected( this ) && dce.Selection.AbsLength == 1 ;
			}
		}

		private bool DragBounds( DragPointStyle hit )
		{
			MouseCapturer cap = new MouseCapturer( this.OwnerDocument.EditorControl );
			cap.Tag = hit ;
			cap.ReversibleShape = ReversibleShapeStyle.Custom ;
			cap.Draw +=new CaptureMouseMoveEventHandler(cap_Draw);
            DomDocumentContentElement ce = this.DocumentContentElement;
			if( cap.CaptureMouseMove())
			{
				if( LastDragBounds.Width > 0 && LastDragBounds.Height > 0 )
				{
					if( LastDragBounds.Width != this.Width 
                        || LastDragBounds.Height != this.Height )
					{
						System.Drawing.SizeF OldSize = new System.Drawing.SizeF( 
                            this.Width , 
                            this.Height );
						this.InvalidateView();
						this.EditorSize = new SizeF(LastDragBounds.Width , LastDragBounds.Height );
						this.InvalidateView();
                        ce.SetSelection( this.ViewIndex , 1 );
						if( this.OwnerDocument.BeginLogUndo() )
						{
							this.OwnerDocument.UndoList.AddProperty( 
								XTextUndoStyles.EditorSize , 
								OldSize , 
								new System.Drawing.SizeF( this.Width , this.Height ),
								this );
							this.OwnerDocument.EndLogUndo();
						}
                        this.ContentElement.RefreshPrivateContent(
                            this.ContentElement.PrivateContent.IndexOf(this));
                        //ce.RefreshPrivateContent(ce.Content.IndexOf(this));
						this.OwnerDocument.Modified = true ;
						return true ;
					}
				}
			}
			return false;
		}

		private System.Drawing.Rectangle LastDragBounds = System.Drawing.Rectangle.Empty ;

        private void cap_Draw(object sender, CaptureMouseMoveEventArgs e)
        {
            DragPointStyle hit = (DragPointStyle)e.Sender.Tag;
            System.Drawing.Rectangle rect = Rectangle.Ceiling(this.AbsBounds);
            System.Drawing.Point p1 = e.StartPosition;
            System.Drawing.Point p2 = e.CurrentPosition;
            SimpleRectangleTransform trans = this.OwnerDocument.EditorControl.GetTransformItemByDescPoint(rect.Left, rect.Top);
            if (trans != null)
            {
                p1 = trans.TransformPoint(p1);
                p2 = trans.TransformPoint(p2);
                rect = DragRectangle.CalcuteDragRectangle(
                    (int)(p2.X - p1.X),
                    (int)(p2.Y - p1.Y),
                    hit,
                    Rectangle.Ceiling(this.AbsBounds));
                if (rect.Width > (int)this.OwnerDocument.Width)
                {
                    rect.Width = (int)this.OwnerDocument.Width;
                }
                if (this.WidthHeightRate > 0.1)
                {
                    rect.Height = (int)(rect.Width / this.WidthHeightRate);
                }
                LastDragBounds = rect;
                rect = trans.UnTransformRectangle(rect);
                using (ReversibleDrawer drawer =
                           ReversibleDrawer.FromHwnd(this.OwnerDocument.EditorControl.Handle))
                {
                    drawer.PenStyle = PenStyle.PS_DOT;
                    drawer.PenColor = System.Drawing.Color.Red;
                    drawer.DrawRectangle(rect);
                }
            }
        }

        /// <summary>
        /// 专为编辑器提供的对象大小属性
        /// </summary>
        [System.ComponentModel.Browsable( false )]
        [System.Xml.Serialization.XmlIgnore()]
        public override SizeF EditorSize
        {
            get
            {
                return new SizeF(this.Width, this.Height);
            }
            set
            {
                float width = value.Width;
                float height = value.Height;
                if (width > this.OwnerDocument.Width)
                {
                    width = this.OwnerDocument.Width;
                }
                double rate = this.WidthHeightRate;
                if (rate > 0.1)
                {
                    height = (int)(width / rate);
                }
                if (height > this.OwnerDocument.PageSettings.ViewPaperHeight)
                {
                    height = this.OwnerDocument.PageSettings.ViewPaperHeight;
                    if (rate > 0.1)
                    {
                        width = (int)(height * rate);
                    }
                }
                this.Width = width;
                this.Height = height;
            }
        }
         
	}
}