using System;
using System.Collections;
using System.Collections.Generic;

namespace DCSoft.CSharpWriter.Dom.Undo
{
	/// <summary>
	/// 撤销表格单元格合并拆分操作
	/// </summary>
	public class XTextUndoCellSpan : XTextUndoBase
	{
        public XTextUndoCellSpan(DomTableCellElement cell, int newRowSpan, int newColSpan)
        {
            myCellElement = cell;
            DomTableElement table = cell.OwnerTable;
            intOldRowSpan = cell.RowSpan;
            intOldColSpan = cell.ColSpan;
            intNewRowSpan = newRowSpan;
            intNewColSpan = newColSpan;

            DomElementList oldOverrideCells = table.GetRange(
                cell.RowIndex,
                cell.ColIndex,
                Math.Max( intOldRowSpan , intNewRowSpan ),
                Math.Max( intOldColSpan , intNewColSpan ), 
                true );
            foreach (DomTableCellElement cell2 in oldOverrideCells)
            {
                _oldCellsContents[cell2] = cell2.Elements.Clone();
            }
        }

        /// <summary>
        /// 记录合并单元格操作后的各个单元格的内容
        /// </summary>
        internal void LogNewCellsContent()
        {
            DomElementList newOverrideCells =  myCellElement.OwnerTable.GetRange(
                myCellElement.RowIndex ,
                myCellElement.ColIndex,
                Math.Max(intOldRowSpan, intNewRowSpan),
                Math.Max(intOldColSpan, intNewColSpan), 
                true );
            foreach (DomTableCellElement cell2 in newOverrideCells)
            {
                _newCellsContents[cell2] = cell2.Elements.Clone();
            }
        }

        private DomTableCellElement myCellElement = null;
		/// <summary>
		/// 单元格对象
		/// </summary>
		public DomTableCellElement CellElement
		{
			get{ return myCellElement ;}
			set{ myCellElement = value;}
		}
		private int intOldRowSpan = 1 ;
		/// <summary>
		/// 单元格旧的跨行数
		/// </summary>
		public int OldRowSpan
		{
			get{ return intOldRowSpan ;}
			set{ intOldRowSpan = value;}
		}
		private int intOldColSpan = 1 ;
		/// <summary>
		/// 单元格旧的跨列数
		/// </summary>
		public int OldColSpan
		{
			get{ return intOldColSpan ;}
			set{ intOldColSpan = value;}
		}
		private int intNewRowSpan = 1 ;
		/// <summary>
		/// 单元格新的跨行数
		/// </summary>
		public int NewRowSpan
		{
			get{ return intNewRowSpan ;}
			set{ intNewRowSpan = value;}
		}
		private int intNewColSpan = 1 ;
		/// <summary>
		/// 单元格新的跨列数
		/// </summary>
		public int NewColSpan
		{
			get{ return intNewColSpan ;}
			set{ intNewColSpan = value;}
		}
        /// <summary>
        /// 未设置单元格合并信息前的单元格内容列表
        /// </summary>
        private Dictionary<DomTableCellElement, DomElementList> _oldCellsContents
            = new Dictionary<DomTableCellElement, DomElementList>();
        /// <summary>
        /// 设置单元格合并信息后的单元格内容列表
        /// </summary>
        private Dictionary<DomTableCellElement, DomElementList> _newCellsContents
            = new Dictionary<DomTableCellElement, DomElementList>();

        private void SetCellContent(Dictionary<DomTableCellElement, DomElementList> list)
        {
            
        }

		/// <summary>
		/// 执行撤销操作
		/// </summary>
        public override void Undo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			Execute( true );
		}
		/// <summary>
		/// 执行重复操作
		/// </summary>
        public override void Redo(DCSoft.CSharpWriter.Undo.XUndoEventArgs args)
		{
			Execute( false );
		}

		/// <summary>
		/// 执行操作
		/// </summary>
		/// <param name="undo">是否执行撤销操作</param>
		private void Execute( bool undo )
		{
			if( myCellElement != null )
			{
				if( intOldRowSpan != intNewRowSpan || intOldColSpan != intNewColSpan )
				{
					if( undo )
					{
						if( intOldRowSpan >=1 && intOldColSpan >= 1 )
						{
                            myCellElement.EditorSetCellSpan(intOldRowSpan, intOldColSpan, false , _oldCellsContents );

                            //myCellElement.OwnerTable.InvalidateView();
                            //myCellElement.RowSpan = intOldRowSpan ;
                            //myCellElement.ColSpan = intOldColSpan ;
                            //myCellElement.OwnerTable.ExecuteLayout();
							//myCellElement.OwnerDocument.SetCurrentElement( myCellElement );
							
                            //myCellElement.OwnerDocument.Modified = true;
						}
					}
					else
					{
						if( intNewRowSpan >= 1 && intNewColSpan >= 1 )
						{
                            myCellElement.EditorSetCellSpan(intNewRowSpan, intNewColSpan, false , _newCellsContents );

                            //myCellElement.OwnerTable.InvalidateView();
                            //myCellElement.RowSpan = intNewRowSpan ;
                            //myCellElement.ColSpan = intNewColSpan ;
                            //myCellElement.OwnerTable.ExecuteLayout();
							//myCellElement.OwnerDocument.SetCurrentElement( myCellElement );

							//myCellElement.OwnerDocument.Modified = true;
						}
					}
				}
			}
		}
	}
}