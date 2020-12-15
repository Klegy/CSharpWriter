using System;
using System.Collections.Generic;
using System.Text;
using DCSoft.Drawing;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using DCSoft.WinForms;
using DCSoft.WinForms.Native;
using DCSoft.CSharpWriter.Dom.Undo;

namespace DCSoft.CSharpWriter.Dom
{
    /// <summary>
    /// 具有自定义绘制用户界面的输入域对象
    /// </summary>
    /// <remarks>
    /// 派生的类型需要重载RefreshShapeState,DrawContent方法
    /// 编制 袁永福
    /// </remarks>
    public class DomShapeInputFieldElement : DomInputFieldElementBase
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public DomShapeInputFieldElement()
        {
        }

        [NonSerialized]
        private bool _EditMode = false;
        /// <summary>
        /// 编辑模式
        /// </summary>
        [DefaultValue(false)]
        public bool EditMode
        {
            get
            {
                return _EditMode;
            }
            set
            {
                _EditMode = value;
            }
        }


        public bool EditorSetEditMode(bool mode)
        {
            if (this.EditMode != mode)
            {
                DomDocumentContentElement dce = this.DocumentContentElement;
                bool isFocused = false;
                if (this.EditMode)
                {
                    isFocused = this.Focused;
                }
                else
                {
                    if (dce.IsSelected(this)
                        || dce.CurrentElement == this)
                    {
                        isFocused = true;
                    }
                }
                int oldSelectionIndex = dce.Selection.StartIndex;
                DomElement currentElement = dce.CurrentElement;
                this.OwnerDocument.HighlightManager.InvalidateHighlightInfo(this);
                this.EditMode = mode;
                DomContentElement ce = this.ContentElement;
                int startIndex = 0;
                int endIndex = 0;
                if (mode)
                {
                    startIndex = ce.PrivateContent.IndexOf(this);
                    endIndex = startIndex + 1;
                }
                else
                {
                    startIndex = ce.PrivateContent.IndexOf(this.StartElement);
                    endIndex = ce.PrivateContent.IndexOf(this.EndElement);
                }
                if (this.EditMode == false)
                {
                    this.SizeInvalid = true;
                    CheckShapeState(true);
                }
                ce.UpdateContentElements(true);
                ce.RefreshPrivateContent(startIndex - 1, endIndex + 1, false);
                if (isFocused)
                {
                    if (this.EditMode)
                    {
                        this.Focus();
                    }
                    else
                    {
                        dce.SetSelection(dce.Content.IndexOf(this), 0);
                    }
                }
                else
                {
                    // 设置新的插入点位置
                    int index = dce.Content.IndexOf(currentElement);
                    if (index >= 0)
                    {
                        dce.SetSelection(index, 0);
                    }
                    else
                    {
                        oldSelectionIndex = dce.Content.FixElementIndex(oldSelectionIndex);
                        dce.SetSelection(oldSelectionIndex, 0);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 对象中第一个文档元素对象
        /// </summary>
        [Browsable(false)]
        public override DomElement FirstContentElement
        {
            get
            {
                if (this.EditMode)
                {
                    return base.FirstContentElement;
                }
                else
                {
                    return this;
                }
            }
        }

        /// <summary>
        /// 对象中最后一个文档元素对象
        /// </summary>
        [Browsable(false)]
        public override DomElement LastContentElement
        {
            get
            {
                if (this.EditMode)
                {
                    return base.LastContentElement;
                }
                else
                {
                    return this;
                }
            }
        }

        /// <summary>
        /// 添加内容到文档内容列表中
        /// </summary>
        /// <param name="content">文档内容列表</param>
        /// <param name="privateMode">是否是私有模式</param>
        /// <returns>添加的元素个数</returns>
        public override int AppendContent(DomElementList content, bool privateMode)
        {
            if (this.EditMode)
            {
                return base.AppendContent(content, privateMode);
            }
            else
            {
                CheckShapeState(true);
                content.Add(this);
                return 1;
            }
        }


        private bool bolCanResize = true;
        /// <summary>
        /// 用户是否可以改变对象大小
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        [System.Xml.Serialization.XmlIgnore()]
        public virtual bool CanResize
        {
            get
            {
                return bolCanResize;
            }
            set
            {
                bolCanResize = value;
            }
        }


        private bool _Enabled = true;
        /// <summary>
        /// 对象是否可用,可以接受鼠标键盘事件
        /// </summary>
        [System.ComponentModel.DefaultValue(true)]
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


        /// <summary>
        /// 创建一个拖拽矩形对象
        /// </summary>
        /// <returns>新的拖拽矩形对象</returns>
        public virtual DragRectangle CreateDragRectangle()
        {
            DragRectangle.DragRectSize = DomObjectElement.DragBoxSize;
            DragRectangle rect = new DragRectangle(
                new System.Drawing.Rectangle(
                (int)0,
                (int)0,
                (int)this.Width,
                (int)this.Height),
                true);
            rect.CanMove = true;
            rect.LineDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            rect.CanResize = this.CanResize && this.OwnerDocument.DocumentControler.CanModify( this , DomAccessFlags.CheckUserEditable );
            rect.Focus = true;// myOwnerDocument.Content.CurrentElement == this && myOwnerDocument.Content.SelectionLength == 1 ;
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
            return rect;
        }

        /// <summary>
        /// 绘制对象
        /// </summary>
        /// <param name="args"></param>
        public override void Draw(DocumentPaintEventArgs args)
        {
            if (this.EditMode)
            {
                base.Draw(args);
            }
            else
            {
                args.Render.DrawBackground(this, args);
                this.DrawContent(args);
                if (args.RenderStyle == DocumentRenderStyle.Paint && args.ActiveMode)
                {
                    if (this.ShowDragRect && this.Enabled)
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
                System.Drawing.RectangleF bounds = this.AbsBounds;
                bounds.Width = bounds.Width - 1;
                bounds.Height = bounds.Height - 1;
                args.Render.RenderBorder(this, args, bounds);
            }
        }

        /// <summary>
        /// 进行控制点测试
        /// </summary>
        /// <param name="x">测试点X坐标</param>
        /// <param name="y">测试点Y坐标</param>
        /// <returns>测试点所在控制点编号</returns>
        protected virtual DragPointStyle GetDragHit(int x, int y)
        {
            DragRectangle dr = this.CreateDragRectangle();
            if (dr == null)
                return DragPointStyle.None;
            else
                return dr.DragHit(x, y);
        }

        /// <summary>
        /// 处理文档用户界面事件
        /// </summary>
        /// <param name="args">事件参数</param>
        public override void HandleDocumentEvent(DocumentEventArgs args)
        {
            if (args.Style == DocumentEventStyles.KeyDown)
            {
                if (args.KeyCode == System.Windows.Forms.Keys.F2)
                {
                    this.EditorSetEditMode(!this.EditMode);
                    args.CancelBubble = true;
                }
            }
            if (args.Style == DocumentEventStyles.MouseDown)
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
                    if (this.EditMode == false)
                    {
                        if (args.MouseClicks == 2)
                        {
                            EditorSetEditMode(true);
                            args.CancelBubble = true;
                        }
                        else if (this.ShowDragRect)
                        {
                            DragPointStyle hit = this.GetDragHit(args.X, args.Y);
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
                        else
                        {
                            this.OwnerDocument.CurrentContentElement.SetSelection(this.ViewIndex, 1);
                            args.CancelBubble = true;
                        }
                    }
                    else
                    {
                        //this.OwnerDocument.CurrentContentElement.SetSelection(this.ViewIndex, 1);
                        //args.CancelBubble = true;
                        //args.Cursor = System.Windows.Forms.Cursors.Arrow;
                    }
                     
                }
            }
            else if (args.Style == DocumentEventStyles.MouseMove)
            {
                if (this.Enabled)
                {
                    if (this.EditMode == false)
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
            }
            else
            {
                base.HandleDocumentEvent(args);
            }
        }

        /// <summary>
        /// 判断能否使用鼠标拖拽该对象
        /// </summary>
        [System.ComponentModel.Browsable(false)]
        public bool ShowDragRect
        {
            get
            {
                if (this.CanResize == false)
                {
                    return false;
                }
                DomDocumentContentElement dce = this.DocumentContentElement;
                return dce.IsSelected(this) && dce.Selection.AbsLength == 1;
            }
        }

        private bool DragBounds(DragPointStyle hit)
        {
            MouseCapturer cap = new MouseCapturer(this.OwnerDocument.EditorControl);
            cap.Tag = hit;
            cap.ReversibleShape = ReversibleShapeStyle.Custom;
            cap.Draw += new CaptureMouseMoveEventHandler(cap_Draw);
            DomDocumentContentElement ce = this.DocumentContentElement;
            if (cap.CaptureMouseMove())
            {
                if (LastDragBounds.Width > 0 && LastDragBounds.Height > 0)
                {
                    if (LastDragBounds.Width != this.Width
                        || LastDragBounds.Height != this.Height)
                    {
                        System.Drawing.SizeF OldSize = new System.Drawing.SizeF(
                            this.Width,
                            this.Height);
                        this.InvalidateView();
                        this.EditorSize = new SizeF(LastDragBounds.Width, LastDragBounds.Height);
                        this.SizeInvalid = true;
                        CheckShapeState(true);
                        this.InvalidateView();
                        ce.SetSelection(this.ViewIndex, 1);
                        if (this.OwnerDocument.BeginLogUndo())
                        {
                            this.OwnerDocument.UndoList.AddProperty(
                                XTextUndoStyles.EditorSize,
                                OldSize,
                                new System.Drawing.SizeF(this.Width, this.Height),
                                this);
                            this.OwnerDocument.EndLogUndo();
                        }
                        this.ContentElement.RefreshPrivateContent(
                            this.ContentElement.PrivateContent.IndexOf(this));
                        //ce.RefreshPrivateContent(ce.Content.IndexOf(this));
                        this.OwnerDocument.Modified = true;
                        return true;
                    }
                }
            }
            return false;
        }

        private System.Drawing.Rectangle LastDragBounds = System.Drawing.Rectangle.Empty;

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

        ///// <summary>
        ///// 处理文档事件
        ///// </summary>
        ///// <param name="args"></param>
        //public override void HandleDocumentEvent(DocumentEventArgs args)
        //{
        //    if (args.Style == DocumentEventStyles.KeyDown)
        //    {
        //        if (args.KeyCode == System.Windows.Forms.Keys.F2)
        //        {
        //            this.EditorSetEditMode(!this.EditMode);
        //            args.CancelBubble = true;
        //        }
        //    }
        //    else if (args.Style == DocumentEventStyles.MouseDown)
        //    {
        //        if (this.EditMode == false)
        //        {
        //            if (this.OwnerDocument.EditorControl != null)
        //            {
        //                if (this.OwnerDocument.EditorControl.MouseDragScroll)
        //                {
        //                    return;
        //                }
        //            }
        //            if (args.MouseClicks == 2 && this.EditMode == false)
        //            {
        //                EditorSetEditMode(true);
        //                args.CancelBubble = true;
        //            }
        //            else
        //            {
        //                this.OwnerDocument.CurrentContentElement.SetSelection(this.ViewIndex, 1);
        //                args.CancelBubble = true;
        //                args.Cursor = System.Windows.Forms.Cursors.Arrow;
        //            }
        //        }
        //        return;
        //    }
        //    else if (args.Style == DocumentEventStyles.MouseMove)
        //    {
        //        if (this.EditMode == false)
        //        {
        //            args.Cursor = System.Windows.Forms.Cursors.Arrow;
        //        }
        //    }
        //    else
        //    {
        //        base.HandleDocumentEvent(args);
        //    }
        //}

        //public override void Draw(DocumentPaintEventArgs args)
        //{
        //    if (this.EditMode)
        //    {
        //        base.Draw(args);
        //    }
        //    else
        //    {
        //        args.Render.DrawBackground(this, args);
        //        this.DrawContent(args);
        //        args.Render.RenderBorder(this, args, this.AbsBounds);

        //        //base.Draw(args);
        //    }
        //}

        /// <summary>
        /// 检查状态
        /// </summary>
        /// <param name="updateSize"></param>
        public virtual void CheckShapeState( bool updateSize )
        {
        }


        /// <summary>
        /// 计算对象大小
        /// </summary>
        /// <param name="args">参数</param>
        public override void RefreshSize(DocumentPaintEventArgs args)
        {
            this.SizeInvalid = true ;
            CheckShapeState(true);
            base.RefreshSize(args);
        }

         

        /// <summary>
        /// 输出对象到HTML文档
        /// </summary>
        /// <param name="writer">HTML文档书写器</param>
        public override void WriteHTML(Html.WriterHtmlDocumentWriter writer)
        {
            if (this.EditMode)
            {
                base.WriteHTML(writer);
            }
            else
            {
                writer.WriteImageElement(this);
            }
        }

        /// <summary>
        /// 输出对象到RTF文档中
        /// </summary>
        /// <param name="writer">RTF文档书写器</param>
        public override void WriteRTF(RTF.RTFContentWriter writer)
        {
            if (this.EditMode)
            {
                // 若处于编辑状态则正常输出内容
                base.WriteRTF(writer);
            }
            else
            {
                // 否则输出表达式图片对象
                System.Drawing.SizeF size = new System.Drawing.SizeF(this.Width, this.Height);
                size = GraphicsUnitConvert.Convert(
                    size,
                    this.OwnerDocument.DocumentGraphicsUnit,
                    GraphicsUnit.Pixel);
                using (System.Drawing.Image img = base.CreateContentImage())
                {
                    writer.WriteImage(
                        img,
                        (int)size.Width,
                        (int)size.Height,
                        null,
                        this.RuntimeStyle);

                }
            }
        }
    }
}
