using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fihirana_database.UserControl
{
    [ToolboxItem(true)]
    public partial class RixCustomGrid : GridControl
    {
        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridView1");
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridView1InfoRegistrator());
        }
    }

    public class CustomGridView1InfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName => "CustomGridView1";

        public override BaseView CreateView(GridControl grid)
        {
            return new CustomGridView1(grid);
        }

        public override BaseViewHandler CreateHandler(BaseView view)
        {
            return new CustomGridView1Handler(view as CustomGridView1);
        }
    }

    public class CustomGridView1 : DevExpress.XtraGrid.Views.Grid.GridView
    {
        protected bool isAdvanced = false;

        public CustomGridView1()
        {
        }

        public CustomGridView1(GridControl grid) : base(grid)
        {
        }

        [Description("Advanced Search"), Category("Data")]
        public bool AdvancedSearch
        {
            get { return isAdvanced; }
            set { isAdvanced = value; }
        }

        protected override string ViewName => "CustomGridView1";

        protected override List<IDataColumnInfo> GetFindToColumnsCollection()
        {
            List<IDataColumnInfo> res = base.GetFindToColumnsCollection();

            if (isAdvanced)
            {
                foreach (GridColumn column in Columns)
                {
                    if (!column.Visible && column.GroupIndex == -1)
                    {
                        res.Add(CreateIDataColumnInfoForFilter(column));
                    }
                }
                return res;
            }
            else return res;
        }
    }
    public class CustomGridView1Handler : DevExpress.XtraGrid.Views.Grid.Handler.GridHandler
    {
        public CustomGridView1Handler(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
        {
        }
    }
}
