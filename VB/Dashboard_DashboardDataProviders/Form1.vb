Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess
Imports DevExpress.DataAccess.ConnectionParameters

Namespace Dashboard_SqlDataProvider
    Partial Public Class Form1
        Inherits RibbonForm

        Public Sub New()
            InitializeComponent()
            dashboardDesigner1.CreateRibbon()

'            #Region "#SQLDataSource"
            Dim nwParameters As New Access97ConnectionParameters("..\..\Data\nwind.mdb", "Admin", "")
            Dim nwConnection As New DataConnection("nwindConnection", nwParameters)
            Dim sqlProvider As New SqlDataProvider(nwConnection, "select * from SalesPerson")
            Dim sqlDataSource As New DataSource("SQL Data Source", sqlProvider)

            dashboardDesigner1.Dashboard.DataSources.Add(sqlDataSource)
'            #End Region ' #SQLDataSource

            InitializeDashboardItems()
        End Sub

        Private Sub InitializeDashboardItems()
            Dim sqlDataSource As DataSource = dashboardDesigner1.Dashboard.DataSources(0)

            Dim chart As New ChartDashboardItem()
            chart.DataSource = sqlDataSource
            chart.Arguments.Add(New Dimension("OrderDate", DateTimeGroupInterval.MonthYear))
            chart.Panes.Add(New ChartPane())
            Dim salesAmountSeries As New SimpleSeries(SimpleSeriesType.SplineArea)
            salesAmountSeries.Value = New Measure("Extended Price")
            chart.Panes(0).Series.Add(salesAmountSeries)

            Dim grid As New GridDashboardItem()
            grid.DataSource = sqlDataSource
            grid.Columns.Add(New GridDimensionColumn(New Dimension("Sales Person")))
            grid.Columns.Add(New GridMeasureColumn(New Measure("Extended Price")))

            dashboardDesigner1.Dashboard.Items.AddRange(chart, grid)
        End Sub
    End Class
End Namespace
