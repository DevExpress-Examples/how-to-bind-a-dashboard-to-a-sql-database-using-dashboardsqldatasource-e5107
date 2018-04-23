using DevExpress.XtraBars.Ribbon;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;

namespace Dashboard_SqlDataProvider {
    public partial class Form1 : RibbonForm {
        public Form1() {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();

            #region #SQLDataSource
            Access97ConnectionParameters nwParameters = 
                new Access97ConnectionParameters(@"..\..\Data\nwind.mdb", "Admin", "");
            DataConnection nwConnection = new DataConnection("nwindConnection", nwParameters);
            SqlDataProvider sqlProvider = new SqlDataProvider(nwConnection, "select * from SalesPerson");
            DataSource sqlDataSource = new DataSource("SQL Data Source", sqlProvider);

            dashboardDesigner1.Dashboard.DataSources.Add(sqlDataSource);
            #endregion #SQLDataSource

            InitializeDashboardItems();
        }

        void InitializeDashboardItems() {
            DataSource sqlDataSource = dashboardDesigner1.Dashboard.DataSources[0];

            ChartDashboardItem chart = new ChartDashboardItem(); chart.DataSource = sqlDataSource;
            chart.Arguments.Add(new Dimension("OrderDate", DateTimeGroupInterval.MonthYear));
            chart.Panes.Add(new ChartPane());
            SimpleSeries salesAmountSeries = new SimpleSeries(SimpleSeriesType.SplineArea);
            salesAmountSeries.Value = new Measure("Extended Price");
            chart.Panes[0].Series.Add(salesAmountSeries);

            GridDashboardItem grid = new GridDashboardItem(); grid.DataSource = sqlDataSource;
            grid.Columns.Add(new GridDimensionColumn(new Dimension("Sales Person")));
            grid.Columns.Add(new GridMeasureColumn(new Measure("Extended Price")));

            dashboardDesigner1.Dashboard.Items.AddRange(chart, grid);
        }
    }
}
