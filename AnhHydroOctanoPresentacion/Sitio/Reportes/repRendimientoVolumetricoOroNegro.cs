using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using ServiceStack.Common.Extensions;

/// <summary>
/// Summary description for repDj
/// </summary>
public class repRendimientoVolumetricoOroNegro : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private PageHeaderBand pageHeaderBand1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private ReportHeaderBand reportHeaderBand1;
    private XRControlStyle Title;
    private XRControlStyle FieldCaption;
    private XRControlStyle PageInfo;
    private XRControlStyle DataField;
    private XRTable xrTable3;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell35;
    public XRTableCell xtcGestion;
    public XRTableCell xtcPeriodo;
    private ReportFooterBand ReportFooter;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
    private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
    private BackgroundWorker backgroundWorker1;
    private XRTable xrTable11;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell176;
    public XRTableCell xrTDestAdmosOprDia;
    public XRTableCell xrTDestAdmosCargTotal;
    public XRTableCell xrTDestAdmosCapOperaci;
    private XRTableCell xrTableCell180;
    private XRTable xrTable14;
    private XRTableRow xrTableRow37;
    private XRTableCell xrTableCell181;
    private XRTableCell xrTableCell182;
    private XRTableCell xrTableCell183;
    private XRTableCell xrTableCell184;
    private XRTableRow xrTableRow38;
    public XRTableCell xrTDestRendDiesl;
    public XRTableCell xrTDestRendgasb;
    public XRTableCell xrTDestRendCrudRes;
    public XRTableCell xrTDestRendPerdid;
    public XRTableCell xrTDestAdmosCombLiqu;
    public XRTableCell xrTDestAdmosCombGas;
    public XRTableCell xrTDestAdmosVaporTn;
    public XRTableCell xrTDestAdmosAguaNfr;
    public XRTableCell xrTDestAdmosAireComp;
    public XRTableCell xrTDestAdmosCompkWh;
    public XRTableCell xrTDestAdmosPropkWh;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell68;
    public XRTableCell xrTReforCatOprDia;
    public XRTableCell xrTReformCatsCargTotal0;
    public XRTableCell xrTReformCatOperaci;
    private XRTableCell xrTableCell72;
    private XRTable xrTable10;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell160;
    private XRTableCell xrTableCell161;
    private XRTableCell xrTableCell162;
    private XRTableCell xrTableCell163;
    private XRTableRow xrTableRow29;
    private XRTableCell xrTReforCatGasEsp;
    private XRTableCell xrTReforCatGasRef;
    private XRTableCell xrTReforCatGlp;
    private XRTableCell xrTReforCatNaftLiv;
    public XRTableCell xrTReformCatbLiqu;
    public XRTableCell xrTReformCatCombGas;
    public XRTableCell xrTReformCatVaporTn;
    public XRTableCell xrTReformCatAguaNfr;
    public XRTableCell xrTReformCatAireComp;
    public XRTableCell xrTReformCatCompkWh;
    public XRTableCell xrTReformCatPropkWh;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTable xrTable9;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell50;
    private XRTable xrTable1;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell31;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell32;
    public XRTableCell xtcRazonSocial;
    private XRTableCell xrTableCell17;
    public XRTableCell xtcActividad;
    private XRTableCell xrTableCell48;
    private XRTableCell xrTableCell16;
    private XRTableCell xrTReforCatPerdida;
    public XRTableCell xrTDestAdmosCargPromBBL;
    public XRTableCell xrTReformCatsCargPromBbl;
    private XRTableCell xrTableCell14;
    private XRTable xrTable5;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell15;
    public XRTableCell xrTQuienImprime;
    private XRTableCell xrTableCell24;
    public XRTableCell xrTFechaHora;
    private XRTable xrTable7;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell21;
    private XRTableCell xrTableCell22;
    private XRTableCell xrTableCell23;
    private XRTable xrTable27;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell18;
    private XRTable xrTable4;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell19;
    private XRPageInfo xrPageInfo1;
    private XRPictureBox xrPictureBox3;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox2;
    private XRTable xrTable28;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell25;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell27;
    private XRTable xrTable29;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell28;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public repRendimientoVolumetricoOroNegro()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(repRendimientoVolumetricoOroNegro));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell176 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosOprDia = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCargTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCargPromBBL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCapOperaci = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell180 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell181 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell182 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell183 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell184 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDestRendDiesl = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestRendgasb = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestRendCrudRes = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestRendPerdid = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCombLiqu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosAireComp = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosCompkWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosPropkWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatOprDia = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatsCargTotal0 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatsCargPromBbl = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatOperaci = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTReforCatGasEsp = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatGasRef = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatGlp = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatNaftLiv = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatPerdida = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatbLiqu = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatAireComp = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatCompkWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReformCatPropkWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTQuienImprime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTFechaHora = new DevExpress.XtraReports.UI.XRTableCell();
            this.pageHeaderBand1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtcRazonSocial = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtcActividad = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtcGestion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtcPeriodo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FieldCaption = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
            this.DataField = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.xrTable27 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable28 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable29 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11});
            this.Detail.HeightF = 95.13623F;
            this.Detail.MultiColumn.ColumnCount = 31;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable11
            // 
            this.xrTable11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable11.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow36,
            this.xrTableRow15});
            this.xrTable11.SizeF = new System.Drawing.SizeF(1026F, 95.13623F);
            this.xrTable11.StylePriority.UseBorders = false;
            this.xrTable11.StylePriority.UseFont = false;
            this.xrTable11.StylePriority.UseTextAlignment = false;
            this.xrTable11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow36
            // 
            this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell176,
            this.xrTDestAdmosOprDia,
            this.xrTDestAdmosCargTotal,
            this.xrTDestAdmosCargPromBBL,
            this.xrTDestAdmosCapOperaci,
            this.xrTableCell180,
            this.xrTDestAdmosCombLiqu,
            this.xrTDestAdmosCombGas,
            this.xrTDestAdmosVaporTn,
            this.xrTDestAdmosAguaNfr,
            this.xrTDestAdmosAireComp,
            this.xrTDestAdmosCompkWh,
            this.xrTDestAdmosPropkWh});
            this.xrTableRow36.Name = "xrTableRow36";
            this.xrTableRow36.Weight = 1D;
            // 
            // xrTableCell176
            // 
            this.xrTableCell176.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell176.Name = "xrTableCell176";
            this.xrTableCell176.StylePriority.UseBorders = false;
            this.xrTableCell176.Text = "U. DESTILACION ADMOSFERICA";
            this.xrTableCell176.Weight = 0.779883018063799D;
            // 
            // xrTDestAdmosOprDia
            // 
            this.xrTDestAdmosOprDia.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosOprDia.Name = "xrTDestAdmosOprDia";
            this.xrTDestAdmosOprDia.StylePriority.UseBorders = false;
            this.xrTDestAdmosOprDia.Weight = 0.49466859208465874D;
            // 
            // xrTDestAdmosCargTotal
            // 
            this.xrTDestAdmosCargTotal.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCargTotal.Name = "xrTDestAdmosCargTotal";
            this.xrTDestAdmosCargTotal.StylePriority.UseBorders = false;
            this.xrTDestAdmosCargTotal.Weight = 0.48946963361739948D;
            // 
            // xrTDestAdmosCargPromBBL
            // 
            this.xrTDestAdmosCargPromBBL.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCargPromBBL.Name = "xrTDestAdmosCargPromBBL";
            this.xrTDestAdmosCargPromBBL.StylePriority.UseBorders = false;
            this.xrTDestAdmosCargPromBBL.Weight = 0.67690652822303865D;
            // 
            // xrTDestAdmosCapOperaci
            // 
            this.xrTDestAdmosCapOperaci.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCapOperaci.Name = "xrTDestAdmosCapOperaci";
            this.xrTDestAdmosCapOperaci.StylePriority.UseBorders = false;
            this.xrTDestAdmosCapOperaci.Weight = 0.67028239495063158D;
            // 
            // xrTableCell180
            // 
            this.xrTableCell180.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
            this.xrTableCell180.Name = "xrTableCell180";
            this.xrTableCell180.Text = "xrTableCell72";
            this.xrTableCell180.Weight = 2.7364472677409046D;
            // 
            // xrTable14
            // 
            this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable14.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable14.Name = "xrTable14";
            this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow37,
            this.xrTableRow38});
            this.xrTable14.SizeF = new System.Drawing.SizeF(269.2708F, 47.56811F);
            this.xrTable14.StylePriority.UseBorders = false;
            this.xrTable14.StylePriority.UseFont = false;
            this.xrTable14.StylePriority.UseTextAlignment = false;
            this.xrTable14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow37
            // 
            this.xrTableRow37.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell181,
            this.xrTableCell182,
            this.xrTableCell183,
            this.xrTableCell184});
            this.xrTableRow37.Name = "xrTableRow37";
            this.xrTableRow37.Weight = 1D;
            // 
            // xrTableCell181
            // 
            this.xrTableCell181.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell181.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell181.Name = "xrTableCell181";
            this.xrTableCell181.StylePriority.UseBackColor = false;
            this.xrTableCell181.StylePriority.UseBorders = false;
            this.xrTableCell181.Text = "DIESEL OIL";
            this.xrTableCell181.Weight = 0.8032400537355473D;
            // 
            // xrTableCell182
            // 
            this.xrTableCell182.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell182.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell182.Name = "xrTableCell182";
            this.xrTableCell182.StylePriority.UseBackColor = false;
            this.xrTableCell182.StylePriority.UseBorders = false;
            this.xrTableCell182.Text = "GASOLINA BLANCA";
            this.xrTableCell182.Weight = 0.80324115924401418D;
            // 
            // xrTableCell183
            // 
            this.xrTableCell183.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell183.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell183.Name = "xrTableCell183";
            this.xrTableCell183.StylePriority.UseBackColor = false;
            this.xrTableCell183.StylePriority.UseBorders = false;
            this.xrTableCell183.Text = "CRUDO RESIDUAL";
            this.xrTableCell183.Weight = 0.7685187835819447D;
            // 
            // xrTableCell184
            // 
            this.xrTableCell184.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell184.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell184.Name = "xrTableCell184";
            this.xrTableCell184.StylePriority.UseBackColor = false;
            this.xrTableCell184.StylePriority.UseBorders = false;
            this.xrTableCell184.Text = "PERDIDAS";
            this.xrTableCell184.Weight = 1.2152789392268935D;
            // 
            // xrTableRow38
            // 
            this.xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDestRendDiesl,
            this.xrTDestRendgasb,
            this.xrTDestRendCrudRes,
            this.xrTDestRendPerdid});
            this.xrTableRow38.Name = "xrTableRow38";
            this.xrTableRow38.Weight = 1D;
            // 
            // xrTDestRendDiesl
            // 
            this.xrTDestRendDiesl.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDestRendDiesl.Name = "xrTDestRendDiesl";
            this.xrTDestRendDiesl.StylePriority.UseBorders = false;
            this.xrTDestRendDiesl.Text = "0,00";
            this.xrTDestRendDiesl.Weight = 0.8032400537355473D;
            // 
            // xrTDestRendgasb
            // 
            this.xrTDestRendgasb.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestRendgasb.Name = "xrTDestRendgasb";
            this.xrTDestRendgasb.StylePriority.UseBorders = false;
            this.xrTDestRendgasb.Text = "0,00";
            this.xrTDestRendgasb.Weight = 0.80324115924401418D;
            // 
            // xrTDestRendCrudRes
            // 
            this.xrTDestRendCrudRes.Name = "xrTDestRendCrudRes";
            this.xrTDestRendCrudRes.StylePriority.UseBorders = false;
            this.xrTDestRendCrudRes.Text = "0,00";
            this.xrTDestRendCrudRes.Weight = 0.768518318753855D;
            // 
            // xrTDestRendPerdid
            // 
            this.xrTDestRendPerdid.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTDestRendPerdid.Name = "xrTDestRendPerdid";
            this.xrTDestRendPerdid.StylePriority.UseBorders = false;
            this.xrTDestRendPerdid.Text = "0,00";
            this.xrTDestRendPerdid.Weight = 1.2152792214438615D;
            // 
            // xrTDestAdmosCombLiqu
            // 
            this.xrTDestAdmosCombLiqu.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCombLiqu.Name = "xrTDestAdmosCombLiqu";
            this.xrTDestAdmosCombLiqu.StylePriority.UseBorders = false;
            this.xrTDestAdmosCombLiqu.Weight = 0.623220502968652D;
            // 
            // xrTDestAdmosCombGas
            // 
            this.xrTDestAdmosCombGas.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCombGas.Name = "xrTDestAdmosCombGas";
            this.xrTDestAdmosCombGas.StylePriority.UseBorders = false;
            this.xrTDestAdmosCombGas.Weight = 0.65830433691480872D;
            // 
            // xrTDestAdmosVaporTn
            // 
            this.xrTDestAdmosVaporTn.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosVaporTn.Name = "xrTDestAdmosVaporTn";
            this.xrTDestAdmosVaporTn.StylePriority.UseBorders = false;
            this.xrTDestAdmosVaporTn.Weight = 0.60060328612815628D;
            // 
            // xrTDestAdmosAguaNfr
            // 
            this.xrTDestAdmosAguaNfr.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosAguaNfr.Name = "xrTDestAdmosAguaNfr";
            this.xrTDestAdmosAguaNfr.StylePriority.UseBorders = false;
            this.xrTDestAdmosAguaNfr.Weight = 0.58442638930382D;
            // 
            // xrTDestAdmosAireComp
            // 
            this.xrTDestAdmosAireComp.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosAireComp.Name = "xrTDestAdmosAireComp";
            this.xrTDestAdmosAireComp.StylePriority.UseBorders = false;
            this.xrTDestAdmosAireComp.Weight = 0.72848617549233008D;
            // 
            // xrTDestAdmosCompkWh
            // 
            this.xrTDestAdmosCompkWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosCompkWh.Name = "xrTDestAdmosCompkWh";
            this.xrTDestAdmosCompkWh.StylePriority.UseBorders = false;
            this.xrTDestAdmosCompkWh.Weight = 0.72848728215770442D;
            // 
            // xrTDestAdmosPropkWh
            // 
            this.xrTDestAdmosPropkWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestAdmosPropkWh.Name = "xrTDestAdmosPropkWh";
            this.xrTDestAdmosPropkWh.StylePriority.UseBorders = false;
            this.xrTDestAdmosPropkWh.Weight = 0.59732965373435787D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTReforCatOprDia,
            this.xrTReformCatsCargTotal0,
            this.xrTReformCatsCargPromBbl,
            this.xrTReformCatOperaci,
            this.xrTableCell72,
            this.xrTReformCatbLiqu,
            this.xrTReformCatCombGas,
            this.xrTReformCatVaporTn,
            this.xrTReformCatAguaNfr,
            this.xrTReformCatAireComp,
            this.xrTReformCatCompkWh,
            this.xrTReformCatPropkWh});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.Text = "UNIDAD DE REFORMACION CATALITICA";
            this.xrTableCell68.Weight = 0.77988294061299279D;
            // 
            // xrTReforCatOprDia
            // 
            this.xrTReforCatOprDia.Name = "xrTReforCatOprDia";
            this.xrTReforCatOprDia.Weight = 0.49466862920271992D;
            // 
            // xrTReformCatsCargTotal0
            // 
            this.xrTReformCatsCargTotal0.Name = "xrTReformCatsCargTotal0";
            this.xrTReformCatsCargTotal0.Weight = 0.48946978878042691D;
            // 
            // xrTReformCatsCargPromBbl
            // 
            this.xrTReformCatsCargPromBbl.Name = "xrTReformCatsCargPromBbl";
            this.xrTReformCatsCargPromBbl.Weight = 0.67690652987394673D;
            // 
            // xrTReformCatOperaci
            // 
            this.xrTReformCatOperaci.Name = "xrTReformCatOperaci";
            this.xrTReformCatOperaci.Weight = 0.6702818536394396D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Text = "xrTableCell72";
            this.xrTableCell72.Weight = 2.736447036599249D;
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28,
            this.xrTableRow29});
            this.xrTable10.SizeF = new System.Drawing.SizeF(269.2707F, 47.56811F);
            this.xrTable10.StylePriority.UseBorders = false;
            this.xrTable10.StylePriority.UseFont = false;
            this.xrTable10.StylePriority.UseTextAlignment = false;
            this.xrTable10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell160,
            this.xrTableCell161,
            this.xrTableCell162,
            this.xrTableCell163,
            this.xrTableCell16});
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.Weight = 1D;
            // 
            // xrTableCell160
            // 
            this.xrTableCell160.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell160.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell160.Name = "xrTableCell160";
            this.xrTableCell160.StylePriority.UseBackColor = false;
            this.xrTableCell160.StylePriority.UseBorders = false;
            this.xrTableCell160.Text = "GASOLINA ESPECIAL";
            this.xrTableCell160.Weight = 0.8032400537355473D;
            // 
            // xrTableCell161
            // 
            this.xrTableCell161.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell161.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell161.Name = "xrTableCell161";
            this.xrTableCell161.StylePriority.UseBackColor = false;
            this.xrTableCell161.StylePriority.UseBorders = false;
            this.xrTableCell161.Text = "GAS DE REFINERIA";
            this.xrTableCell161.Weight = 0.80324115924401418D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell162.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.StylePriority.UseBackColor = false;
            this.xrTableCell162.StylePriority.UseBorders = false;
            this.xrTableCell162.Text = "GLP";
            this.xrTableCell162.Weight = 0.76851850565263558D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell163.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.StylePriority.UseBackColor = false;
            this.xrTableCell163.StylePriority.UseBorders = false;
            this.xrTableCell163.Text = "NAFTA LIVIANA";
            this.xrTableCell163.Weight = 0.6087973454983675D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell16.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBackColor = false;
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.Text = "PERDIDA";
            this.xrTableCell16.Weight = 0.60648094837881394D;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTReforCatGasEsp,
            this.xrTReforCatGasRef,
            this.xrTReforCatGlp,
            this.xrTReforCatNaftLiv,
            this.xrTReforCatPerdida});
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTReforCatGasEsp
            // 
            this.xrTReforCatGasEsp.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTReforCatGasEsp.Name = "xrTReforCatGasEsp";
            this.xrTReforCatGasEsp.StylePriority.UseBorders = false;
            this.xrTReforCatGasEsp.Text = "0,00";
            this.xrTReforCatGasEsp.Weight = 0.8032400537355473D;
            // 
            // xrTReforCatGasRef
            // 
            this.xrTReforCatGasRef.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTReforCatGasRef.Name = "xrTReforCatGasRef";
            this.xrTReforCatGasRef.StylePriority.UseBorders = false;
            this.xrTReforCatGasRef.Text = "0,00";
            this.xrTReforCatGasRef.Weight = 0.80324115924401418D;
            // 
            // xrTReforCatGlp
            // 
            this.xrTReforCatGlp.Name = "xrTReforCatGlp";
            this.xrTReforCatGlp.StylePriority.UseBorders = false;
            this.xrTReforCatGlp.Text = "0,00";
            this.xrTReforCatGlp.Weight = 0.76851809875135213D;
            // 
            // xrTReforCatNaftLiv
            // 
            this.xrTReforCatNaftLiv.Name = "xrTReforCatNaftLiv";
            this.xrTReforCatNaftLiv.Text = "0,00";
            this.xrTReforCatNaftLiv.Weight = 0.608798566202218D;
            // 
            // xrTReforCatPerdida
            // 
            this.xrTReforCatPerdida.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTReforCatPerdida.Name = "xrTReforCatPerdida";
            this.xrTReforCatPerdida.StylePriority.UseBorders = false;
            this.xrTReforCatPerdida.Text = "0,00";
            this.xrTReforCatPerdida.Weight = 0.60648013457624694D;
            // 
            // xrTReformCatbLiqu
            // 
            this.xrTReformCatbLiqu.Name = "xrTReformCatbLiqu";
            this.xrTReformCatbLiqu.Weight = 0.62322111979953121D;
            // 
            // xrTReformCatCombGas
            // 
            this.xrTReformCatCombGas.Name = "xrTReformCatCombGas";
            this.xrTReformCatCombGas.Weight = 0.65830564765478727D;
            // 
            // xrTReformCatVaporTn
            // 
            this.xrTReformCatVaporTn.Name = "xrTReformCatVaporTn";
            this.xrTReformCatVaporTn.Weight = 0.60060327324031015D;
            // 
            // xrTReformCatAguaNfr
            // 
            this.xrTReformCatAguaNfr.Name = "xrTReformCatAguaNfr";
            this.xrTReformCatAguaNfr.Weight = 0.58442574687998528D;
            // 
            // xrTReformCatAireComp
            // 
            this.xrTReformCatAireComp.Name = "xrTReformCatAireComp";
            this.xrTReformCatAireComp.Weight = 0.72848559726392925D;
            // 
            // xrTReformCatCompkWh
            // 
            this.xrTReformCatCompkWh.Name = "xrTReformCatCompkWh";
            this.xrTReformCatCompkWh.Weight = 0.72848789899264332D;
            // 
            // xrTReformCatPropkWh
            // 
            this.xrTReformCatPropkWh.Name = "xrTReformCatPropkWh";
            this.xrTReformCatPropkWh.Weight = 0.59732903698726181D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 20.29166F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.BottomMargin.HeightF = 41.60118F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable5
            // 
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1025F, 25F);
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell15,
            this.xrTQuienImprime,
            this.xrTableCell24,
            this.xrTFechaHora});
            this.xrTableRow4.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.StylePriority.UseFont = false;
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "Impreso por: ";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell15.Weight = 0.30115943587550448D;
            // 
            // xrTQuienImprime
            // 
            this.xrTQuienImprime.Name = "xrTQuienImprime";
            this.xrTQuienImprime.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTQuienImprime.StylePriority.UsePadding = false;
            this.xrTQuienImprime.StylePriority.UseTextAlignment = false;
            this.xrTQuienImprime.Text = "xrTQuienImprime";
            this.xrTQuienImprime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTQuienImprime.Weight = 3.0371769944713454D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "Fecha y Hora:";
            this.xrTableCell24.Weight = 0.24327722981102667D;
            // 
            // xrTFechaHora
            // 
            this.xrTFechaHora.Name = "xrTFechaHora";
            this.xrTFechaHora.StylePriority.UseTextAlignment = false;
            this.xrTFechaHora.Text = "xrTFechaHora";
            this.xrTFechaHora.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTFechaHora.Weight = 0.4183863398421232D;
            // 
            // pageHeaderBand1
            // 
            this.pageHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable27,
            this.xrTable4,
            this.xrPageInfo1,
            this.xrPictureBox3,
            this.xrPictureBox1,
            this.xrPictureBox2,
            this.xrTable28,
            this.xrTable29,
            this.xrTable2,
            this.xrTable9,
            this.xrTable1,
            this.xrTable6,
            this.xrTable3});
            this.pageHeaderBand1.HeightF = 167.5978F;
            this.pageHeaderBand1.Name = "pageHeaderBand1";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(578.6458F, 136.3478F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(447.3544F, 31.24997F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.Text = "COMB LIQUIDO M3";
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.85549854741164288D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.Text = "COMB GAS MMSCF";
            this.xrTableCell8.Weight = 0.9036646046240242D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "VAPOR TN";
            this.xrTableCell9.Weight = 0.8244535520040952D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "AGUA NFR M3";
            this.xrTableCell10.Weight = 0.80224722749218369D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "AIRE COMP NM3";
            this.xrTableCell11.Weight = 1D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "COMPRADA kWh";
            this.xrTableCell12.Weight = 1.0000008466970349D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "PROPIA hWh";
            this.xrTableCell13.Weight = 0.81996257599105882D;
            // 
            // xrTable9
            // 
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(578.6458F, 122.8061F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable9.SizeF = new System.Drawing.SizeF(447.3542F, 13.54167F);
            this.xrTable9.StylePriority.UseBorders = false;
            this.xrTable9.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell50});
            this.xrTableRow9.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.StylePriority.UseFont = false;
            this.xrTableRow9.StylePriority.UseTextAlignment = false;
            this.xrTableRow9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableRow9.Weight = 1D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell46.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.Text = "CONSUMOS";
            this.xrTableCell46.Weight = 2.4098665523823475D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell50.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.Text = "E. ELECTRICA";
            this.xrTableCell50.Weight = 1D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 122.8061F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable1.SizeF = new System.Drawing.SizeF(578.6458F, 44.79167F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell39,
            this.xrTableCell14,
            this.xrTableCell41,
            this.xrTableCell42});
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Text = "CARBURANTES";
            this.xrTableCell36.Weight = 0.87500000290716384D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Text = "OPERACION DIAS";
            this.xrTableCell37.Weight = 0.55499969467347354D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "CARGA TOTAL M3";
            this.xrTableCell39.Weight = 0.54916702794336014D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "CARGA PROMEDIO BBL/D";
            this.xrTableCell14.Weight = 0.75946434866426749D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Text = "% CAPACIDAD OPERACIÓN";
            this.xrTableCell41.Weight = 0.75203175054387694D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Text = "RENDIMIENTOS % VOLUMETRICOS";
            this.xrTableCell42.Weight = 3.0701939562737177D;
            // 
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 105.0978F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable6.SizeF = new System.Drawing.SizeF(1026F, 17.70833F);
            this.xrTable6.StylePriority.UseBorders = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell31});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell31.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseBackColor = false;
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            this.xrTableCell31.Text = "RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS ";
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell31.Weight = 1D;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.59776F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 2, 0, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5,
            this.xrTableRow7});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1026F, 37.50002F);
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell32,
            this.xtcRazonSocial,
            this.xrTableCell17,
            this.xtcActividad});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell32.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseBorders = false;
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.Text = "RAZON SOCIAL :";
            this.xrTableCell32.Weight = 1.939683196375082D;
            // 
            // xtcRazonSocial
            // 
            this.xtcRazonSocial.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xtcRazonSocial.Name = "xtcRazonSocial";
            this.xtcRazonSocial.StylePriority.UseBorders = false;
            this.xtcRazonSocial.Text = "xtcRazonSocial";
            this.xtcRazonSocial.Weight = 5.0026339255706773D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.Weight = 1.1506223880657389D;
            // 
            // xtcActividad
            // 
            this.xtcActividad.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xtcActividad.Name = "xtcActividad";
            this.xtcActividad.StylePriority.UseBorders = false;
            this.xtcActividad.Weight = 5.82395181159624D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell35,
            this.xtcGestion,
            this.xrTableCell48,
            this.xtcPeriodo});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell35.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.Text = "GESTION :";
            this.xrTableCell35.Weight = 0.82834115670911712D;
            // 
            // xtcGestion
            // 
            this.xtcGestion.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xtcGestion.Name = "xtcGestion";
            this.xtcGestion.StylePriority.UseBorders = false;
            this.xtcGestion.Text = "xtcGestion";
            this.xtcGestion.Weight = 2.183947853284891D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell48.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.Text = "PERIODO :";
            this.xrTableCell48.Weight = 0.49925772299414467D;
            // 
            // xtcPeriodo
            // 
            this.xtcPeriodo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xtcPeriodo.Name = "xtcPeriodo";
            this.xtcPeriodo.StylePriority.UseBorders = false;
            this.xtcPeriodo.Text = "xtcPeriodo";
            this.xtcPeriodo.Weight = 2.52702773483155D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 1D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 1D;
            // 
            // reportHeaderBand1
            // 
            this.reportHeaderBand1.HeightF = 0F;
            this.reportHeaderBand1.Name = "reportHeaderBand1";
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.BorderColor = System.Drawing.Color.Black;
            this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Title.BorderWidth = 1F;
            this.Title.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.Title.ForeColor = System.Drawing.Color.Maroon;
            this.Title.Name = "Title";
            // 
            // FieldCaption
            // 
            this.FieldCaption.BackColor = System.Drawing.Color.Transparent;
            this.FieldCaption.BorderColor = System.Drawing.Color.Black;
            this.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.FieldCaption.BorderWidth = 1F;
            this.FieldCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.FieldCaption.ForeColor = System.Drawing.Color.Maroon;
            this.FieldCaption.Name = "FieldCaption";
            // 
            // PageInfo
            // 
            this.PageInfo.BackColor = System.Drawing.Color.Transparent;
            this.PageInfo.BorderColor = System.Drawing.Color.Black;
            this.PageInfo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.PageInfo.BorderWidth = 1F;
            this.PageInfo.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.PageInfo.ForeColor = System.Drawing.Color.Black;
            this.PageInfo.Name = "PageInfo";
            // 
            // DataField
            // 
            this.DataField.BackColor = System.Drawing.Color.Transparent;
            this.DataField.BorderColor = System.Drawing.Color.Black;
            this.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DataField.BorderWidth = 1F;
            this.DataField.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.DataField.ForeColor = System.Drawing.Color.Black;
            this.DataField.Name = "DataField";
            this.DataField.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.ReportFooter.HeightF = 2F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable7
            // 
            this.xrTable7.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(140.7415F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
            this.xrTable7.SizeF = new System.Drawing.SizeF(230.7692F, 2F);
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.CanGrow = false;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.Weight = 1D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.CanGrow = false;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "xrTableCell22";
            this.xrTableCell22.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.CanGrow = false;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "xrTableCell23";
            this.xrTableCell23.Weight = 1D;
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = null;
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(AnhHydroOctanoPresentacion.Clases.Controles.CMovAcumuladoRep);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // xrTable27
            // 
            this.xrTable27.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable27.LocationFloat = new DevExpress.Utils.PointFloat(143.5F, 0F);
            this.xrTable27.Name = "xrTable27";
            this.xrTable27.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable27.SizeF = new System.Drawing.SizeF(738.5612F, 48.95826F);
            this.xrTable27.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell18});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell18.Weight = 1D;
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.5F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable4.SizeF = new System.Drawing.SizeF(143F, 67.59776F);
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.StylePriority.UseBorders = false;
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Weight = 1D;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(801.9916F, 49.81412F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(70.06964F, 13.54167F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(11.36655F, 4.000011F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(119.875F, 55.49995F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(987.2499F, 5.499962F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(35F, 54F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(882.0612F, 4.000015F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(104.6874F, 62F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrTable28
            // 
            this.xrTable28.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable28.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable28.LocationFloat = new DevExpress.Utils.PointFloat(143.5F, 49.81412F);
            this.xrTable28.Name = "xrTable28";
            this.xrTable28.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable28.SizeF = new System.Drawing.SizeF(738.5613F, 16.1859F);
            this.xrTable28.StylePriority.UseBorders = false;
            this.xrTable28.StylePriority.UseFont = false;
            this.xrTable28.StylePriority.UseTextAlignment = false;
            this.xrTable28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell20,
            this.xrTableCell25,
            this.xrTableCell26,
            this.xrTableCell27});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.Text = "Código:";
            this.xrTableCell20.Weight = 1D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "Versión:";
            this.xrTableCell25.Weight = 0.82134934269309889D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.Text = "Aprobado:";
            this.xrTableCell26.Weight = 1.4701330216349817D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Text = "Páginas:";
            this.xrTableCell27.Weight = 0.70851763567191939D;
            // 
            // xrTable29
            // 
            this.xrTable29.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable29.LocationFloat = new DevExpress.Utils.PointFloat(882.061F, 0F);
            this.xrTable29.Name = "xrTable29";
            this.xrTable29.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable29.SizeF = new System.Drawing.SizeF(143.439F, 66.00002F);
            this.xrTable29.StylePriority.UseBorders = false;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Weight = 1D;
            // 
            // repRendimientoVolumetricoOroNegro
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.pageHeaderBand1,
            this.reportHeaderBand1,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource2});
            this.DataSource = this.objectDataSource2;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(35, 39, 20, 42);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.FieldCaption,
            this.PageInfo,
            this.DataField});
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion


}
