namespace MakeSoft.Tools.Web
{
    using MakeSoft.Tools;
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GridViewExtended
    {
        private GridView gridview;
        private bool isfooterPrepared = false;
        private bool isgroupPrepared = false;
        private List<GridViewColumnCalc> listacolumncalc;
        private List<GridViewGroup> listagroup;
        private int visiblecolumnscount = -1;

        public GridViewExtended(GridView gridview)
        {
            this.gridview = gridview;
            this.gridview.RowDataBound += new GridViewRowEventHandler(this.RowDataBound);
            this.listagroup = new List<GridViewGroup>();
            this.listacolumncalc = new List<GridViewColumnCalc>();
        }

        protected GridViewRow AddGriwViewRow(GridViewRow currentrow)
        {
            Table table = (Table) this.gridview.Controls[0];
            int rowIndex = table.Rows.GetRowIndex(currentrow);
            GridViewRow child = new GridViewRow(rowIndex, rowIndex, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell cell = new TableCell {
                ColumnSpan = this.visiblecolumnscount,
                Text = "&nbsp;"
            };
            child.Cells.Add(cell);
            table.Controls.AddAt(rowIndex, child);
            return child;
        }

        protected GridViewRow AddGriwViewRow(GridViewRow currentrow, GridViewGroup group)
        {
            Table table = (Table) this.gridview.Controls[0];
            int rowIndex = table.Rows.GetRowIndex(currentrow);
            GridViewRow child = new GridViewRow(rowIndex, rowIndex, DataControlRowType.DataRow, DataControlRowState.Normal);
            TableCell[] cells = new TableCell[group.ListColumnsCalc.Count + 1];
            int num2 = this.visiblecolumnscount - group.ListColumnsCalc.Count;
            int num3 = 0;
            if (num2 > 0)
            {
                TableCell cell = new TableCell {
                    ColumnSpan = num2,
                    Text = "&nbsp;"
                };
                cells[0] = cell;
                num3++;
            }
            for (int i = num3; i <= group.ListColumnsCalc.Count; i++)
            {
                cells[i] = new TableCell { ColumnSpan = 0, Text = "&nbsp;" };
            }
            child.Cells.AddRange(cells);
            table.Controls.AddAt(rowIndex, child);
            return child;
        }

        public void ExecuteSortGroup()
        {
            this.gridview.Sort(this.GetGroupSort(), SortDirection.Ascending);
        }

        protected void GenerateSummaryGroup(GridViewGroup group, GridViewRow currentrow)
        {
            for (int i = this.listagroup.Count - 1; i >= this.GetIndexGroup(group.FieldName); i--)
            {
                if (this.listagroup[i].IsStarted)
                {
                    this.ProcessSummaryGroup(this.listagroup[i], currentrow);
                    this.listagroup[i].Reset();
                }
            }
        }

        protected int GetAbsoluteColumnIndex(string columname)
        {
            int num = 0;
            foreach (DataControlField field in this.gridview.Columns)
            {
                if ((field is BoundField) && ((field as BoundField).DataField.ToUpper() == columname.ToUpper()))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        protected int GetCellIndex(int colindex, GridViewRow row)
        {
            int num = this.visiblecolumnscount - row.Cells.Count;
            return (colindex - num);
        }

        protected int GetColumnsVisibleCount()
        {
            int num = 0;
            for (int i = 0; i < this.gridview.Columns.Count; i++)
            {
                if (this.gridview.Columns[i].Visible)
                {
                    num++;
                }
            }
            return num;
        }

        protected GridViewGroup GetGridViewGroup(string groupname)
        {
            foreach (GridViewGroup group in this.listagroup)
            {
                if (group.FieldName == groupname)
                {
                    return group;
                }
            }
            return null;
        }

        protected string GetGroupSort()
        {
            string str = string.Empty;
            foreach (GridViewGroup group in this.listagroup)
            {
                str = str + group.FieldName + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

        protected int GetIndexGroup(string groupname)
        {
            int num = 0;
            foreach (GridViewGroup group in this.listagroup)
            {
                if (group.FieldName == groupname)
                {
                    return num;
                }
                num++;
            }
            return -1;
        }

        protected int GetVisibleColumnIndex(string columname)
        {
            int num = 0;
            foreach (DataControlField field in this.gridview.Columns)
            {
                if (field.Visible)
                {
                    if ((field is BoundField) && ((field as BoundField).DataField.ToUpper() == columname.ToUpper()))
                    {
                        return num;
                    }
                    num++;
                }
            }
            return -1;
        }

        protected void PrepareColumnsFooter()
        {
            this.PrepareIndexColumn();
            this.gridview.ShowFooter = this.listacolumncalc.Count != 0;
            this.isfooterPrepared = true;
        }

        protected void PrepareGroups()
        {
            foreach (GridViewGroup group in this.listagroup)
            {
                group.Index = this.GetVisibleColumnIndex(group.FieldName);
                if (group.Index == -1)
                {
                    throw new Exception("campo " + group.FieldName + " n\x00e3o localizando");
                }
                this.gridview.Columns[this.GetAbsoluteColumnIndex(group.FieldName)].Visible = false;
            }
            foreach (GridViewGroup group in this.listagroup)
            {
                this.PrepareIndexColumn(group);
            }
            this.isgroupPrepared = true;
        }

        protected void PrepareIndexColumn()
        {
            foreach (GridViewColumnCalc calc in this.listacolumncalc)
            {
                calc.Index = this.GetVisibleColumnIndex(calc.FieldName);
                if (calc.Index == -1)
                {
                    throw new Exception("campo " + calc.FieldName + " n\x00e3o localizando");
                }
            }
        }

        protected void PrepareIndexColumn(GridViewGroup group)
        {
            foreach (GridViewColumnCalc calc in group.ListColumnsCalc)
            {
                calc.Index = this.GetVisibleColumnIndex(calc.FieldName);
                if (calc.Index == -1)
                {
                    throw new Exception("campo " + calc.FieldName + " n\x00e3o localizando");
                }
            }
        }

        protected void ProcessGroup(GridViewGroup group, GridViewRow currentrow)
        {
            if (group.CurrentValue == DataBinder.Eval(currentrow.DataItem, group.FieldName).ToString())
            {
                group.RowDataBound(currentrow);
            }
            else
            {
                if (group.IsStarted)
                {
                    this.GenerateSummaryGroup(group, currentrow);
                }
                group.Start();
                group.CurrentValue = DataBinder.Eval(currentrow.DataItem, group.FieldName).ToString();
                this.AddGriwViewRow(currentrow).Cells[0].Text = group.DisplayName + " " + group.CurrentValue;
                group.RowDataBound(currentrow);
            }
        }

        protected void ProcessSummaryFooter(GridViewRow row)
        {
            foreach (GridViewColumnCalc calc in this.listacolumncalc)
            {
                row.Cells[0].Text = "Total";
                row.Cells[calc.Index].Text = Functions.FormatNumber(calc.Value, 2);
                row.Cells[calc.Index].HorizontalAlign = HorizontalAlign.Right;
                calc.Init();
            }
        }

        protected void ProcessSummaryGroup(GridViewGroup group, GridViewRow currentrow)
        {
            GridViewRow row = this.AddGriwViewRow(currentrow, group);
            row.Cells[0].Text = group.DisplayName + " " + group.CurrentValue;
            foreach (GridViewColumnCalc calc in group.ListColumnsCalc)
            {
                int cellIndex = this.GetCellIndex(calc.Index, row);
                row.Cells[cellIndex].Text = Functions.FormatNumber(calc.Value, 2);
                row.Cells[cellIndex].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        public void RegisterColumnCalc(string fieldname, GridViewColumnCalcOperation operacao, string displayname)
        {
            this.listacolumncalc.Add(new GridViewColumnCalc(fieldname, operacao, displayname));
        }

        public void RegisterColumnCalc(string groupname, string fieldname, GridViewColumnCalcOperation operacao, string displayname)
        {
            GridViewGroup gridViewGroup = this.GetGridViewGroup(groupname);
            if (gridViewGroup == null)
            {
                new Exception("O grupo" + groupname + " n\x00e3o foi registrado");
            }
            gridViewGroup.ListColumnsCalc.Add(new GridViewColumnCalc(fieldname, operacao, displayname));
        }

        public void RegisterGroup(string fieldname, string displayname)
        {
            this.listagroup.Add(new GridViewGroup(fieldname, displayname));
        }

        protected void RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!this.isfooterPrepared)
            {
                this.PrepareColumnsFooter();
            }
            if (!this.isgroupPrepared)
            {
                this.PrepareGroups();
            }
            if (this.visiblecolumnscount == -1)
            {
                this.visiblecolumnscount = this.GetColumnsVisibleCount();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    foreach (GridViewGroup group in this.listagroup)
                    {
                        this.ProcessGroup(group, e.Row);
                    }
                }
                foreach (GridViewColumnCalc calc in this.listacolumncalc)
                {
                    calc.RowDataBound(e.Row);
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    foreach (GridViewGroup group in this.listagroup)
                    {
                        this.GenerateSummaryGroup(group, e.Row);
                    }
                    this.ProcessSummaryFooter(e.Row);
                }
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                }
            }
        }
    }
}

