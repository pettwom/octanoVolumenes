using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using ServiceStack.Common.Extensions;

/// <summary>
/// Summary description for repDj
/// </summary>
public class repRendimientoVolumetricoGEldeBell : DevExpress.XtraReports.UI.XtraReport
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
    private ReportFooterBand ReportFooter;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
    private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
    private BackgroundWorker backgroundWorker1;
    private XRTable xrTable11;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell176;
    public XRTableCell xrTRecuperacnOperacion;
    public XRTableCell xrTRecuperacnCrgaPromedio;
    public XRTableCell xrTRecuperacnCapOperac;
    private XRTableCell xrTableCell180;
    private XRTable xrTable14;
    private XRTableRow xrTableRow37;
    private XRTableCell xrTableCell181;
    private XRTableCell xrTableCell182;
    private XRTableCell xrTableCell183;
    private XRTableCell xrTableCell184;
    private XRTableRow xrTableRow38;
    public XRTableCell xrTRecupGasGASREF;
    public XRTableCell xrTRecupGasC4;
    public XRTableCell xrTRecupGasGLP;
    public XRTableCell xrTRecupGasC3;
    public XRTableCell xrTRecuperacnCombLiq;
    public XRTableCell xrTRecuperacnCombGas;
    public XRTableCell xrTRecuperacnVaporTn;
    public XRTableCell xrTRecuperacnAguaNfr;
    public XRTableCell xrTRecuperacnAireCom;
    public XRTableCell xrTRecuperacnCompradakWh;
    public XRTableCell xrTRecuperacnPropiakWh;
    private XRTableRow xrTableRow15;
    private XRTableCell xrTableCell68;
    public XRTableCell xrTRefCataliticaOperacion;
    public XRTableCell xrTRefCataliticaCrgaPromedio;
    public XRTableCell xrTRefCataliticaCapOperac;

    private XRTableCell xrTableCell72;
    private XRTable xrTable10;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell160;
    private XRTableCell xrTableCell161;
    private XRTableCell xrTableCell162;
    private XRTableCell xrTableCell163;
    private XRTableRow xrTableRow29;
    public XRTableCell xrTCatalitica1GASREF;
    public XRTableCell xrTCatalitica1C4C3;
    public XRTableCell xrTCatalitica1PLATF;
    public XRTableCell xrTCatalitica1RECGASES;
    public XRTableCell xrTCatalitica2GASREF;
    public XRTableCell xrTCatalitica2C4C3;
    public XRTableCell xrTCatalitica2PLATF;
    public XRTableCell xrTCatalitica2RECGASES;

    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell136;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell124;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell112;
    private XRTableCell xrTableCell116;
    private XRTableCell xrTableCell140;
    public XRTableCell xrTRefCataliticaCombLiq;
    public XRTableCell xrTRefCataliticaCombGas;
    public XRTableCell xrTRefCataliticaVaporTn;
    public XRTableCell xrTRefCataliticaAguaNfr;
    public XRTableCell xrTRefCataliticaAireCom;
    public XRTableCell xrTRefCataliticaCompradakWh;
    public XRTableCell xrTRefCataliticaPropiakWh;
    public XRTableCell xrTRefCatalitica2Operacion;
    public XRTableCell xrTRefCatalitica2CrgaPromedio;
    public XRTableCell xrTRefCatalitica2CapOperac;
    public XRTableCell xrTRefCatalitica2CombLiq;
    public XRTableCell xrTRefCatalitica2CombGas;
    public XRTableCell xrTRefCatalitica2VaporTn;
    public XRTableCell xrTRefCatalitica2AguaNfr;
    public XRTableCell xrTRefCatalitica2AireCom;
    public XRTableCell xrTRefCatalitica2CompradakWh;
    public XRTableCell xrTRefCatalitica2PropiakWh;
    public XRTableCell xrTReDestiladoOperacion;
    public XRTableCell xrTReDestiladoCrgaPromedio;
    public XRTableCell xrTReDestiladoCapOperac;
    public XRTableCell xrTReDestiladoCombLiq;
    public XRTableCell xrTReDestiladoCombGas;
    public XRTableCell xrTReDestiladoVaporTn;
    public XRTableCell xrTReDestiladoAguaNfr;
    public XRTableCell xrTReDestiladoAireCom;
    public XRTableCell xrTReDestiladoCompradakWh;
    public XRTableCell xrTReDestiladoPropiakWh;
    public XRTableCell xrTDeganOperacion;
    public XRTableCell xrTDeganCrgaPromedio;
    public XRTableCell xrTDeganCapOperac;
    public XRTableCell xrTableCell128;
    public XRTableCell xrTDeganCombLiq;
    public XRTableCell xrTDeganCombGas;
    public XRTableCell xrTDeganVaporTn;
    public XRTableCell xrTDeganAguaNfr;
    public XRTableCell xrTDeganAireCom;
    public XRTableCell xrTDeganCompradakWh;
    public XRTableCell xrTDeganPropiakWh;
    public XRTableCell xrTNuisOperacion;
    public XRTableCell xrTNuisCrgaPromedio;
    public XRTableCell xrTNuisCapOperac;
    public XRTableCell xrTableCell152;
    public XRTableCell xrTNuisCombLiq;
    public XRTableCell xrTNuisCombGas;
    public XRTableCell xrTNuisVaporTn;
    public XRTableCell xrTNuisAguaNfr;
    public XRTableCell xrTNuisAireCom;
    public XRTableCell xrTNuisCompradakWh;
    public XRTableCell xrTNuisPropiakWh;


    private XRTable xrTable5; private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell148;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTableCell14;
    public XRTableCell xrTDestAdmosfericaOperacion;
    public XRTableCell xrTDestAdmosfericaCargaM3;
    public XRTableCell xrTDestAdmosfericaCapOperac;
    private XRTableCell xrTableCell21;
    private XRTable xrTable7;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRTableRow xrTableRow17;
    public XRTableCell xrTDetil1C4;
    public XRTableCell xrTDetil1LSR;
    public XRTableCell xrTDetil1MSR;
    public XRTableCell xrTDetil1DL;
    public XRTableCell xrTDetil1DM;
    public XRTableCell xrTDetil1DP;
    public XRTableCell xrTDetil1CR;
    public XRTableCell xrTDetil1GM;

    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableRow xrTableRow19;

    public XRTableCell xrTDestAdmosfericaCombLiq;
    public XRTableCell xrTDestAdmosfericaCombGas;
    public XRTableCell xrTDestAdmosfericaVaporTn;
    public XRTableCell xrTDestAdmosfericaAguaNfr;
    public XRTableCell xrTDestAdmosfericaAireCom;
    public XRTableCell xrTDestAdmosfericaCompradakWh;
    public XRTableCell xrTDestAdmosfericaPropiakWh;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell44;
    public XRTableCell xrTDestAdmosferica2Operacion;
    public XRTableCell xrTDestAdmosferica2CargaM3;
    public XRTableCell xrTDestAdmosferica2CapOperac;
    private XRTableCell xrTableCell60;
    private XRTable xrTable8;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableRow xrTableRow21;
    public XRTableCell xrTDetil2C4;
    public XRTableCell xrTDetil2LSR;
    public XRTableCell xrTDetil2MSR;
    public XRTableCell xrTDetil2DL;
    public XRTableCell xrTDetil2DM;
    public XRTableCell xrTDetil2DP;
    public XRTableCell xrTDetil2CR;
    public XRTableCell xrTDetil2RECIRC;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell107;
    private XRTableRow xrTableRow23;

    public XRTableCell xrTDestAdmosferica2CombLiq;
    public XRTableCell xrTDestAdmosferica2CombGas;
    public XRTableCell xrTDestAdmosferica2VaporTn;
    public XRTableCell xrTDestAdmosferica2AguaNfr;
    public XRTableCell xrTDestAdmosferica2AireCom;
    public XRTableCell xrTDestAdmosferica2CompradakWh;
    public XRTableCell xrTDestAdmosferica2PropiakWh;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell31;
    private XRTable xrTable16;
    private XRTableRow xrTableRow39;
    private XRTableCell xrTableCell212;
    private XRTableCell xrTableCell213;
    private XRTableCell xrTableCell214;
    private XRTableCell xrTableCell215;
    private XRTableRow xrTableRow40;

    private XRTable xrTable12;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell168;
    private XRTableCell xrTableCell169;
    private XRTableCell xrTableCell170;
    private XRTableCell xrTableCell171;
    private XRTableRow xrTableRow31;
    public XRTableCell xrRedestiREDEST;
    public XRTableCell xrRedestiCORTE;

    public XRTableCell xrTNuisISOMERADO;
    public XRTableCell xrTNuisREFINERIA;
    public XRTableCell xrTNuisLIVIANA;
    public XRTableCell xrTNuisCRUDO;
    public XRTableCell xrTJetFuiDL;
    public XRTableCell xrTJetFuiDM;

    private XRTableCell xrTableCell174;
    private XRTableCell xrTableCell175;
    private XRTable xrTable13;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTableCell196;
    private XRTableCell xrTableCell197;
    private XRTableCell xrTableCell198;
    private XRTableCell xrTableCell199;
    private XRTableRow xrTableRow33;
    public XRTableCell xrTDeganLIVIANA;
    public XRTableCell xrTDeganMEDIA;
    public XRTableCell xrTDeganNAFTA;
    private XRTableCell xrTableCell203;

    private XRTable xrTable15;
    private XRTableRow xrTableRow34;
    private XRTableCell xrTableCell204;
    private XRTableCell xrTableCell205;
    private XRTableCell xrTableCell206;
    private XRTableCell xrTableCell207;
    private XRTableRow xrTableRow35;

    private XRTableRow xrTableRow41;
    private XRTableCell xrTableCell220;
    public XRTableCell xrTJetFuilOperacion;
    public XRTableCell xrTJetFuilCrgaPromedio;
    public XRTableCell xrTJetFuilCapOperac;
    private XRTableCell xrTableCell224;
    private XRTable xrTable17;
    private XRTableRow xrTableRow42;
    private XRTableCell xrTableCell232;
    private XRTableCell xrTableCell233;
    private XRTableCell xrTableCell234;
    private XRTableCell xrTableCell235;
    private XRTableRow xrTableRow43;

    private XRTableCell xrTableCell238;
    private XRTableCell xrTableCell239;
    public XRTableCell xrTJetFuilCombLiq;
    public XRTableCell xrTJetFuilCombGas;
    public XRTableCell xrTJetFuilVaporTn;
    public XRTableCell xrTJetFuilAguaNfr;
    public XRTableCell xrTJetFuilAireCom;
    public XRTableCell xrTJetFuilCompradakWh;
    public XRTableCell xrTJetFuilPropiakWh;
    private XRTable xrTable3;
    private XRTableRow xrTableRow5;
    private XRTableCell xrTableCell32;
    public XRTableCell xtcRazonSocial;
    private XRTableCell xrTableCell17;
    public XRTableCell xtcActividad;
    private XRTableRow xrTableRow7;
    private XRTableCell xrTableCell35;
    public XRTableCell xtcGestion;
    private XRTableCell xrTableCell48;
    public XRTableCell xtcPeriodo;
    private XRTable xrTable1;
    private XRTableRow xrTableRow8;
    private XRTableCell xrTableCell36;
    private XRTableCell xrTableCell37;
    private XRTableCell xrTableCell39;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell41;
    private XRTableCell xrTableCell42;
    private XRTable xrTable9;
    private XRTableRow xrTableRow9;
    private XRTableCell xrTableCell46;
    private XRTableCell xrTableCell50;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell8;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell10;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell15;
    private XRTable xrTable29;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell18;
    private XRTable xrTable28;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell26;
    private XRTableCell xrTableCell28;
    private XRTableCell xrTableCell30;
    private XRTableCell xrTableCell33;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox3;
    private XRPageInfo xrPageInfo1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell34;
    private XRTable xrTable27;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell38;
    public XRTableCell xrTRecuperacnCargaM3;
    public XRTableCell xrTRefCataliticaCargaM3;
    public XRTableCell xrTRefCatalitica2CargaM3;
    public XRTableCell xrTReDestiladoCargaM3;
    public XRTableCell xrTDeganCargaM3;
    public XRTableCell xrTNuisCargaM3;
    public XRTableCell xrTJetFuilCargaM3;
    public XRTableCell xrTDestAdmosfericaCrgaPromedio;
    public XRTableCell xrTDestAdmosferica2CrgaPromedio;
    private XRTable xrTable26;
    private XRTableRow xrTableRow63;
    private XRTableCell xrTableCell16;
    public XRTableCell xrTQuienImprime;
    private XRTableCell xrTableCell24;
    public XRTableCell xrTFechaHora;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public repRendimientoVolumetricoGEldeBell()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(repRendimientoVolumetricoGEldeBell));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell176 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell180 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell181 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell182 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell183 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell184 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTRecupGasGASREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecupGasC4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecupGasGLP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecupGasC3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRecuperacnPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTCatalitica1GASREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica1C4C3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica1PLATF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica1RECGASES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell212 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell213 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell214 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell215 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTCatalitica2GASREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica2C4C3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica2PLATF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCatalitica2RECGASES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCatalitica2PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrRedestiREDEST = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrRedestiCORTE = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell174 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell175 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReDestiladoPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell196 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell197 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell198 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell199 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDeganLIVIANA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganMEDIA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganNAFTA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell203 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeganPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell204 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell205 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell206 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell207 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTNuisISOMERADO = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisREFINERIA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisLIVIANA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCRUDO = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNuisPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow41 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell220 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell224 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell232 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell233 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell234 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell235 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow43 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTJetFuiDL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuiDM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell238 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell239 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTJetFuilPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDetil1C4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1LSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1MSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1DL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDetil1DM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1DP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1CR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil1GM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosfericaPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDetil2C4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2LSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2MSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2DL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDetil2DM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2DP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2CR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDetil2RECIRC = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestAdmosferica2PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable26 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow63 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTQuienImprime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTFechaHora = new DevExpress.XtraReports.UI.XRTableCell();
            this.pageHeaderBand1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable29 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable28 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable27 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable11,
            this.xrTable5});
            this.Detail.HeightF = 329.7441F;
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
            this.xrTable11.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(0F, 114.676F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow36,
            this.xrTableRow15,
            this.xrTableRow24,
            this.xrTableRow26,
            this.xrTableRow25,
            this.xrTableRow27,
            this.xrTableRow41});
            this.xrTable11.SizeF = new System.Drawing.SizeF(1026F, 215.0681F);
            this.xrTable11.StylePriority.UseBorders = false;
            this.xrTable11.StylePriority.UseFont = false;
            this.xrTable11.StylePriority.UseTextAlignment = false;
            this.xrTable11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow36
            // 
            this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell176,
            this.xrTRecuperacnOperacion,
            this.xrTRecuperacnCargaM3,
            this.xrTRecuperacnCrgaPromedio,
            this.xrTRecuperacnCapOperac,
            this.xrTableCell180,
            this.xrTRecuperacnCombLiq,
            this.xrTRecuperacnCombGas,
            this.xrTRecuperacnVaporTn,
            this.xrTRecuperacnAguaNfr,
            this.xrTRecuperacnAireCom,
            this.xrTRecuperacnCompradakWh,
            this.xrTRecuperacnPropiakWh});
            this.xrTableRow36.Name = "xrTableRow36";
            this.xrTableRow36.Weight = 1D;
            // 
            // xrTableCell176
            // 
            this.xrTableCell176.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell176.Name = "xrTableCell176";
            this.xrTableCell176.StylePriority.UseBorders = false;
            this.xrTableCell176.Text = "U. RECUPERACION GASES";
            this.xrTableCell176.Weight = 0.77743449568368339D;
            // 
            // xrTRecuperacnOperacion
            // 
            this.xrTRecuperacnOperacion.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnOperacion.Name = "xrTRecuperacnOperacion";
            this.xrTRecuperacnOperacion.StylePriority.UseBorders = false;
            this.xrTRecuperacnOperacion.Text = "0.00";
            this.xrTRecuperacnOperacion.Weight = 0.514657551156342D;
            // 
            // xrTRecuperacnCargaM3
            // 
            this.xrTRecuperacnCargaM3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCargaM3.Name = "xrTRecuperacnCargaM3";
            this.xrTRecuperacnCargaM3.StylePriority.UseBorders = false;
            this.xrTRecuperacnCargaM3.Text = "0.00";
            this.xrTRecuperacnCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTRecuperacnCrgaPromedio
            // 
            this.xrTRecuperacnCrgaPromedio.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCrgaPromedio.Name = "xrTRecuperacnCrgaPromedio";
            this.xrTRecuperacnCrgaPromedio.StylePriority.UseBorders = false;
            this.xrTRecuperacnCrgaPromedio.Text = "0.00";
            this.xrTRecuperacnCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTRecuperacnCapOperac
            // 
            this.xrTRecuperacnCapOperac.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCapOperac.Name = "xrTRecuperacnCapOperac";
            this.xrTRecuperacnCapOperac.StylePriority.UseBorders = false;
            this.xrTRecuperacnCapOperac.Text = "0.00";
            this.xrTRecuperacnCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell180
            // 
            this.xrTableCell180.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
            this.xrTableCell180.Name = "xrTableCell180";
            this.xrTableCell180.Text = "xrTableCell72";
            this.xrTableCell180.Weight = 2.7638611833152913D;
            // 
            // xrTable14
            // 
            this.xrTable14.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable14.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable14.Name = "xrTable14";
            this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow37,
            this.xrTableRow38});
            this.xrTable14.SizeF = new System.Drawing.SizeF(269.7807F, 30.724F);
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
            this.xrTableCell181.Text = "GAS REF";
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
            this.xrTableCell182.Text = "C4";
            this.xrTableCell182.Weight = 0.80324115924401418D;
            // 
            // xrTableCell183
            // 
            this.xrTableCell183.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell183.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell183.Name = "xrTableCell183";
            this.xrTableCell183.StylePriority.UseBackColor = false;
            this.xrTableCell183.StylePriority.UseBorders = false;
            this.xrTableCell183.Text = "GLP";
            this.xrTableCell183.Weight = 0.62962987331026732D;
            // 
            // xrTableCell184
            // 
            this.xrTableCell184.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell184.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell184.Name = "xrTableCell184";
            this.xrTableCell184.StylePriority.UseBackColor = false;
            this.xrTableCell184.StylePriority.UseBorders = false;
            this.xrTableCell184.Text = "C3";
            this.xrTableCell184.Weight = 0.7638890412620869D;
            // 
            // xrTableRow38
            // 
            this.xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRecupGasGASREF,
            this.xrTRecupGasC4,
            this.xrTRecupGasGLP,
            this.xrTRecupGasC3});
            this.xrTableRow38.Name = "xrTableRow38";
            this.xrTableRow38.Weight = 1D;
            // 
            // xrTRecupGasGASREF
            // 
            this.xrTRecupGasGASREF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTRecupGasGASREF.Name = "xrTRecupGasGASREF";
            this.xrTRecupGasGASREF.StylePriority.UseBorders = false;
            this.xrTRecupGasGASREF.Text = "0.00";
            this.xrTRecupGasGASREF.Weight = 0.8032400537355473D;
            // 
            // xrTRecupGasC4
            // 
            this.xrTRecupGasC4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecupGasC4.Name = "xrTRecupGasC4";
            this.xrTRecupGasC4.StylePriority.UseBorders = false;
            this.xrTRecupGasC4.Text = "0.00";
            this.xrTRecupGasC4.Weight = 0.80324115924401418D;
            // 
            // xrTRecupGasGLP
            // 
            this.xrTRecupGasGLP.Name = "xrTRecupGasGLP";
            this.xrTRecupGasGLP.StylePriority.UseBorders = false;
            this.xrTRecupGasGLP.Text = "0.00";
            this.xrTRecupGasGLP.Weight = 0.62962987331026732D;
            // 
            // xrTRecupGasC3
            // 
            this.xrTRecupGasC3.Name = "xrTRecupGasC3";
            this.xrTRecupGasC3.Text = "0.00";
            this.xrTRecupGasC3.Weight = 0.7638890412620869D;
            // 
            // xrTRecuperacnCombLiq
            // 
            this.xrTRecuperacnCombLiq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCombLiq.Name = "xrTRecuperacnCombLiq";
            this.xrTRecuperacnCombLiq.StylePriority.UseBorders = false;
            this.xrTRecuperacnCombLiq.Text = "0.00";
            this.xrTRecuperacnCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTRecuperacnCombGas
            // 
            this.xrTRecuperacnCombGas.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCombGas.Name = "xrTRecuperacnCombGas";
            this.xrTRecuperacnCombGas.StylePriority.UseBorders = false;
            this.xrTRecuperacnCombGas.Text = "0.00";
            this.xrTRecuperacnCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTRecuperacnVaporTn
            // 
            this.xrTRecuperacnVaporTn.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnVaporTn.Name = "xrTRecuperacnVaporTn";
            this.xrTRecuperacnVaporTn.StylePriority.UseBorders = false;
            this.xrTRecuperacnVaporTn.Text = "0.00";
            this.xrTRecuperacnVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTRecuperacnAguaNfr
            // 
            this.xrTRecuperacnAguaNfr.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnAguaNfr.Name = "xrTRecuperacnAguaNfr";
            this.xrTRecuperacnAguaNfr.StylePriority.UseBorders = false;
            this.xrTRecuperacnAguaNfr.Text = "0.00";
            this.xrTRecuperacnAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTRecuperacnAireCom
            // 
            this.xrTRecuperacnAireCom.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnAireCom.Name = "xrTRecuperacnAireCom";
            this.xrTRecuperacnAireCom.StylePriority.UseBorders = false;
            this.xrTRecuperacnAireCom.Text = "0.00";
            this.xrTRecuperacnAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTRecuperacnCompradakWh
            // 
            this.xrTRecuperacnCompradakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCompradakWh.Name = "xrTRecuperacnCompradakWh";
            this.xrTRecuperacnCompradakWh.StylePriority.UseBorders = false;
            this.xrTRecuperacnCompradakWh.Text = "0.00";
            this.xrTRecuperacnCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTRecuperacnPropiakWh
            // 
            this.xrTRecuperacnPropiakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnPropiakWh.Name = "xrTRecuperacnPropiakWh";
            this.xrTRecuperacnPropiakWh.StylePriority.UseBorders = false;
            this.xrTRecuperacnPropiakWh.Text = "0.00";
            this.xrTRecuperacnPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTRefCataliticaOperacion,
            this.xrTRefCataliticaCargaM3,
            this.xrTRefCataliticaCrgaPromedio,
            this.xrTRefCataliticaCapOperac,
            this.xrTableCell72,
            this.xrTRefCataliticaCombLiq,
            this.xrTRefCataliticaCombGas,
            this.xrTRefCataliticaVaporTn,
            this.xrTRefCataliticaAguaNfr,
            this.xrTRefCataliticaAireCom,
            this.xrTRefCataliticaCompradakWh,
            this.xrTRefCataliticaPropiakWh});
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.Text = "U. REFORMACION CATALITICA I";
            this.xrTableCell68.Weight = 0.77743449568368339D;
            // 
            // xrTRefCataliticaOperacion
            // 
            this.xrTRefCataliticaOperacion.Name = "xrTRefCataliticaOperacion";
            this.xrTRefCataliticaOperacion.Text = "0.00";
            this.xrTRefCataliticaOperacion.Weight = 0.514657551156342D;
            // 
            // xrTRefCataliticaCargaM3
            // 
            this.xrTRefCataliticaCargaM3.Name = "xrTRefCataliticaCargaM3";
            this.xrTRefCataliticaCargaM3.Text = "0.00";
            this.xrTRefCataliticaCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTRefCataliticaCrgaPromedio
            // 
            this.xrTRefCataliticaCrgaPromedio.Name = "xrTRefCataliticaCrgaPromedio";
            this.xrTRefCataliticaCrgaPromedio.Text = "0.00";
            this.xrTRefCataliticaCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTRefCataliticaCapOperac
            // 
            this.xrTRefCataliticaCapOperac.Name = "xrTRefCataliticaCapOperac";
            this.xrTRefCataliticaCapOperac.Text = "0.00";
            this.xrTRefCataliticaCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Text = "xrTableCell72";
            this.xrTableCell72.Weight = 2.7638611833152913D;
            // 
            // xrTable10
            // 
            this.xrTable10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable10.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 1.525879E-05F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow28,
            this.xrTableRow29});
            this.xrTable10.SizeF = new System.Drawing.SizeF(269.7805F, 30.72401F);
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
            this.xrTableCell163});
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
            this.xrTableCell160.Text = "GAS REF";
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
            this.xrTableCell161.Text = "C4 + C3";
            this.xrTableCell161.Weight = 0.80324115924401418D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell162.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.StylePriority.UseBackColor = false;
            this.xrTableCell162.StylePriority.UseBorders = false;
            this.xrTableCell162.Text = "PLATF";
            this.xrTableCell162.Weight = 0.62962987331026732D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell163.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.StylePriority.UseBackColor = false;
            this.xrTableCell163.StylePriority.UseBorders = false;
            this.xrTableCell163.Text = "REC. GASES";
            this.xrTableCell163.Weight = 0.7638890412620869D;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCatalitica1GASREF,
            this.xrTCatalitica1C4C3,
            this.xrTCatalitica1PLATF,
            this.xrTCatalitica1RECGASES});
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTCatalitica1GASREF
            // 
            this.xrTCatalitica1GASREF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTCatalitica1GASREF.Name = "xrTCatalitica1GASREF";
            this.xrTCatalitica1GASREF.StylePriority.UseBorders = false;
            this.xrTCatalitica1GASREF.Text = "0.00";
            this.xrTCatalitica1GASREF.Weight = 0.8032400537355473D;
            // 
            // xrTCatalitica1C4C3
            // 
            this.xrTCatalitica1C4C3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTCatalitica1C4C3.Name = "xrTCatalitica1C4C3";
            this.xrTCatalitica1C4C3.StylePriority.UseBorders = false;
            this.xrTCatalitica1C4C3.Text = "0.00";
            this.xrTCatalitica1C4C3.Weight = 0.80324115924401418D;
            // 
            // xrTCatalitica1PLATF
            // 
            this.xrTCatalitica1PLATF.Name = "xrTCatalitica1PLATF";
            this.xrTCatalitica1PLATF.StylePriority.UseBorders = false;
            this.xrTCatalitica1PLATF.Text = "0.00";
            this.xrTCatalitica1PLATF.Weight = 0.62962987331026732D;
            // 
            // xrTCatalitica1RECGASES
            // 
            this.xrTCatalitica1RECGASES.Name = "xrTCatalitica1RECGASES";
            this.xrTCatalitica1RECGASES.Text = "0.00";
            this.xrTCatalitica1RECGASES.Weight = 0.7638890412620869D;
            // 
            // xrTRefCataliticaCombLiq
            // 
            this.xrTRefCataliticaCombLiq.Name = "xrTRefCataliticaCombLiq";
            this.xrTRefCataliticaCombLiq.Text = "0.00";
            this.xrTRefCataliticaCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTRefCataliticaCombGas
            // 
            this.xrTRefCataliticaCombGas.Name = "xrTRefCataliticaCombGas";
            this.xrTRefCataliticaCombGas.Text = "0.00";
            this.xrTRefCataliticaCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTRefCataliticaVaporTn
            // 
            this.xrTRefCataliticaVaporTn.Name = "xrTRefCataliticaVaporTn";
            this.xrTRefCataliticaVaporTn.Text = "0.00";
            this.xrTRefCataliticaVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTRefCataliticaAguaNfr
            // 
            this.xrTRefCataliticaAguaNfr.Name = "xrTRefCataliticaAguaNfr";
            this.xrTRefCataliticaAguaNfr.Text = "0.00";
            this.xrTRefCataliticaAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTRefCataliticaAireCom
            // 
            this.xrTRefCataliticaAireCom.Name = "xrTRefCataliticaAireCom";
            this.xrTRefCataliticaAireCom.Text = "0.00";
            this.xrTRefCataliticaAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTRefCataliticaCompradakWh
            // 
            this.xrTRefCataliticaCompradakWh.Name = "xrTRefCataliticaCompradakWh";
            this.xrTRefCataliticaCompradakWh.Text = "0.00";
            this.xrTRefCataliticaCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTRefCataliticaPropiakWh
            // 
            this.xrTRefCataliticaPropiakWh.Name = "xrTRefCataliticaPropiakWh";
            this.xrTRefCataliticaPropiakWh.Text = "0.00";
            this.xrTRefCataliticaPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell112,
            this.xrTRefCatalitica2Operacion,
            this.xrTRefCatalitica2CargaM3,
            this.xrTRefCatalitica2CrgaPromedio,
            this.xrTRefCatalitica2CapOperac,
            this.xrTableCell116,
            this.xrTRefCatalitica2CombLiq,
            this.xrTRefCatalitica2CombGas,
            this.xrTRefCatalitica2VaporTn,
            this.xrTRefCatalitica2AguaNfr,
            this.xrTRefCatalitica2AireCom,
            this.xrTRefCatalitica2CompradakWh,
            this.xrTRefCatalitica2PropiakWh});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell112
            // 
            this.xrTableCell112.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell112.Name = "xrTableCell112";
            this.xrTableCell112.StylePriority.UseFont = false;
            this.xrTableCell112.Text = "U. REFORMACION CATALITICA II";
            this.xrTableCell112.Weight = 0.77743449568368339D;
            // 
            // xrTRefCatalitica2Operacion
            // 
            this.xrTRefCatalitica2Operacion.Name = "xrTRefCatalitica2Operacion";
            this.xrTRefCatalitica2Operacion.Text = "0.00";
            this.xrTRefCatalitica2Operacion.Weight = 0.514657551156342D;
            // 
            // xrTRefCatalitica2CargaM3
            // 
            this.xrTRefCatalitica2CargaM3.Name = "xrTRefCatalitica2CargaM3";
            this.xrTRefCatalitica2CargaM3.Text = "0.00";
            this.xrTRefCatalitica2CargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTRefCatalitica2CrgaPromedio
            // 
            this.xrTRefCatalitica2CrgaPromedio.Name = "xrTRefCatalitica2CrgaPromedio";
            this.xrTRefCatalitica2CrgaPromedio.Text = "0.00";
            this.xrTRefCatalitica2CrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTRefCatalitica2CapOperac
            // 
            this.xrTRefCatalitica2CapOperac.Name = "xrTRefCatalitica2CapOperac";
            this.xrTRefCatalitica2CapOperac.Text = "0.00";
            this.xrTRefCatalitica2CapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell116
            // 
            this.xrTableCell116.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
            this.xrTableCell116.Name = "xrTableCell116";
            this.xrTableCell116.Text = "xrTableCell116";
            this.xrTableCell116.Weight = 2.7638611833152913D;
            // 
            // xrTable16
            // 
            this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable16.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0.2234879F);
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow39,
            this.xrTableRow40});
            this.xrTable16.SizeF = new System.Drawing.SizeF(269.7806F, 30.50053F);
            this.xrTable16.StylePriority.UseBorders = false;
            this.xrTable16.StylePriority.UseFont = false;
            this.xrTable16.StylePriority.UseTextAlignment = false;
            this.xrTable16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow39
            // 
            this.xrTableRow39.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell212,
            this.xrTableCell213,
            this.xrTableCell214,
            this.xrTableCell215});
            this.xrTableRow39.Name = "xrTableRow39";
            this.xrTableRow39.Weight = 1D;
            // 
            // xrTableCell212
            // 
            this.xrTableCell212.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell212.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell212.Name = "xrTableCell212";
            this.xrTableCell212.StylePriority.UseBackColor = false;
            this.xrTableCell212.StylePriority.UseBorders = false;
            this.xrTableCell212.Text = "GAS REF";
            this.xrTableCell212.Weight = 0.8032400537355473D;
            // 
            // xrTableCell213
            // 
            this.xrTableCell213.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell213.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell213.Name = "xrTableCell213";
            this.xrTableCell213.StylePriority.UseBackColor = false;
            this.xrTableCell213.StylePriority.UseBorders = false;
            this.xrTableCell213.Text = "C4 + C3";
            this.xrTableCell213.Weight = 0.80324115924401418D;
            // 
            // xrTableCell214
            // 
            this.xrTableCell214.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell214.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell214.Name = "xrTableCell214";
            this.xrTableCell214.StylePriority.UseBackColor = false;
            this.xrTableCell214.StylePriority.UseBorders = false;
            this.xrTableCell214.Text = "PLATF";
            this.xrTableCell214.Weight = 0.62962987331026732D;
            // 
            // xrTableCell215
            // 
            this.xrTableCell215.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell215.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell215.Name = "xrTableCell215";
            this.xrTableCell215.StylePriority.UseBackColor = false;
            this.xrTableCell215.StylePriority.UseBorders = false;
            this.xrTableCell215.Text = "REC. GASES";
            this.xrTableCell215.Weight = 0.7638890412620869D;
            // 
            // xrTableRow40
            // 
            this.xrTableRow40.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCatalitica2GASREF,
            this.xrTCatalitica2C4C3,
            this.xrTCatalitica2PLATF,
            this.xrTCatalitica2RECGASES});
            this.xrTableRow40.Name = "xrTableRow40";
            this.xrTableRow40.Weight = 1D;
            // 
            // xrTCatalitica2GASREF
            // 
            this.xrTCatalitica2GASREF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTCatalitica2GASREF.Name = "xrTCatalitica2GASREF";
            this.xrTCatalitica2GASREF.StylePriority.UseBorders = false;
            this.xrTCatalitica2GASREF.Text = "0.00";
            this.xrTCatalitica2GASREF.Weight = 0.8032400537355473D;
            // 
            // xrTCatalitica2C4C3
            // 
            this.xrTCatalitica2C4C3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTCatalitica2C4C3.Name = "xrTCatalitica2C4C3";
            this.xrTCatalitica2C4C3.StylePriority.UseBorders = false;
            this.xrTCatalitica2C4C3.Text = "0.00";
            this.xrTCatalitica2C4C3.Weight = 0.80324115924401418D;
            // 
            // xrTCatalitica2PLATF
            // 
            this.xrTCatalitica2PLATF.Name = "xrTCatalitica2PLATF";
            this.xrTCatalitica2PLATF.StylePriority.UseBorders = false;
            this.xrTCatalitica2PLATF.Text = "0.00";
            this.xrTCatalitica2PLATF.Weight = 0.62962987331026732D;
            // 
            // xrTCatalitica2RECGASES
            // 
            this.xrTCatalitica2RECGASES.Name = "xrTCatalitica2RECGASES";
            this.xrTCatalitica2RECGASES.Text = "0.00";
            this.xrTCatalitica2RECGASES.Weight = 0.7638890412620869D;
            // 
            // xrTRefCatalitica2CombLiq
            // 
            this.xrTRefCatalitica2CombLiq.Name = "xrTRefCatalitica2CombLiq";
            this.xrTRefCatalitica2CombLiq.Text = "0.00";
            this.xrTRefCatalitica2CombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTRefCatalitica2CombGas
            // 
            this.xrTRefCatalitica2CombGas.Name = "xrTRefCatalitica2CombGas";
            this.xrTRefCatalitica2CombGas.Text = "0.00";
            this.xrTRefCatalitica2CombGas.Weight = 0.66736686935431844D;
            // 
            // xrTRefCatalitica2VaporTn
            // 
            this.xrTRefCatalitica2VaporTn.Name = "xrTRefCatalitica2VaporTn";
            this.xrTRefCatalitica2VaporTn.Text = "0.00";
            this.xrTRefCatalitica2VaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTRefCatalitica2AguaNfr
            // 
            this.xrTRefCatalitica2AguaNfr.Name = "xrTRefCatalitica2AguaNfr";
            this.xrTRefCatalitica2AguaNfr.Text = "0.00";
            this.xrTRefCatalitica2AguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTRefCatalitica2AireCom
            // 
            this.xrTRefCatalitica2AireCom.Name = "xrTRefCatalitica2AireCom";
            this.xrTRefCatalitica2AireCom.Text = "0.00";
            this.xrTRefCatalitica2AireCom.Weight = 0.7385109204857675D;
            // 
            // xrTRefCatalitica2CompradakWh
            // 
            this.xrTRefCatalitica2CompradakWh.Name = "xrTRefCatalitica2CompradakWh";
            this.xrTRefCatalitica2CompradakWh.Text = "0.00";
            this.xrTRefCatalitica2CompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTRefCatalitica2PropiakWh
            // 
            this.xrTRefCatalitica2PropiakWh.Name = "xrTRefCatalitica2PropiakWh";
            this.xrTRefCatalitica2PropiakWh.Text = "0.00";
            this.xrTRefCatalitica2PropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell136,
            this.xrTReDestiladoOperacion,
            this.xrTReDestiladoCargaM3,
            this.xrTReDestiladoCrgaPromedio,
            this.xrTReDestiladoCapOperac,
            this.xrTableCell140,
            this.xrTReDestiladoCombLiq,
            this.xrTReDestiladoCombGas,
            this.xrTReDestiladoVaporTn,
            this.xrTReDestiladoAguaNfr,
            this.xrTReDestiladoAireCom,
            this.xrTReDestiladoCompradakWh,
            this.xrTReDestiladoPropiakWh});
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 1D;
            // 
            // xrTableCell136
            // 
            this.xrTableCell136.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell136.Name = "xrTableCell136";
            this.xrTableCell136.StylePriority.UseFont = false;
            this.xrTableCell136.Text = "U. REDESTILADO";
            this.xrTableCell136.Weight = 0.77743449568368339D;
            // 
            // xrTReDestiladoOperacion
            // 
            this.xrTReDestiladoOperacion.Name = "xrTReDestiladoOperacion";
            this.xrTReDestiladoOperacion.Text = "0.00";
            this.xrTReDestiladoOperacion.Weight = 0.514657551156342D;
            // 
            // xrTReDestiladoCargaM3
            // 
            this.xrTReDestiladoCargaM3.Name = "xrTReDestiladoCargaM3";
            this.xrTReDestiladoCargaM3.Text = "0.00";
            this.xrTReDestiladoCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTReDestiladoCrgaPromedio
            // 
            this.xrTReDestiladoCrgaPromedio.Name = "xrTReDestiladoCrgaPromedio";
            this.xrTReDestiladoCrgaPromedio.Text = "0.00";
            this.xrTReDestiladoCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTReDestiladoCapOperac
            // 
            this.xrTReDestiladoCapOperac.Name = "xrTReDestiladoCapOperac";
            this.xrTReDestiladoCapOperac.Text = "0.00";
            this.xrTReDestiladoCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell140
            // 
            this.xrTableCell140.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
            this.xrTableCell140.Name = "xrTableCell140";
            this.xrTableCell140.Text = "xrTableCell140";
            this.xrTableCell140.Weight = 2.7638611833152913D;
            // 
            // xrTable12
            // 
            this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable12.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0.2235107F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow30,
            this.xrTableRow31});
            this.xrTable12.SizeF = new System.Drawing.SizeF(269.7805F, 30.5005F);
            this.xrTable12.StylePriority.UseBorders = false;
            this.xrTable12.StylePriority.UseFont = false;
            this.xrTable12.StylePriority.UseTextAlignment = false;
            this.xrTable12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow30
            // 
            this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell168,
            this.xrTableCell169,
            this.xrTableCell170,
            this.xrTableCell171});
            this.xrTableRow30.Name = "xrTableRow30";
            this.xrTableRow30.Weight = 1D;
            // 
            // xrTableCell168
            // 
            this.xrTableCell168.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell168.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell168.Name = "xrTableCell168";
            this.xrTableCell168.StylePriority.UseBackColor = false;
            this.xrTableCell168.StylePriority.UseBorders = false;
            this.xrTableCell168.Text = "REDEST";
            this.xrTableCell168.Weight = 0.8032400537355473D;
            // 
            // xrTableCell169
            // 
            this.xrTableCell169.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell169.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell169.Name = "xrTableCell169";
            this.xrTableCell169.StylePriority.UseBackColor = false;
            this.xrTableCell169.StylePriority.UseBorders = false;
            this.xrTableCell169.Text = "CORTE FONDO";
            this.xrTableCell169.Weight = 0.80324115924401418D;
            // 
            // xrTableCell170
            // 
            this.xrTableCell170.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell170.Name = "xrTableCell170";
            this.xrTableCell170.StylePriority.UseBorders = false;
            this.xrTableCell170.Weight = 0.62962987331026732D;
            // 
            // xrTableCell171
            // 
            this.xrTableCell171.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell171.Name = "xrTableCell171";
            this.xrTableCell171.StylePriority.UseBorders = false;
            this.xrTableCell171.Weight = 0.7638890412620869D;
            // 
            // xrTableRow31
            // 
            this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrRedestiREDEST,
            this.xrRedestiCORTE,
            this.xrTableCell174,
            this.xrTableCell175});
            this.xrTableRow31.Name = "xrTableRow31";
            this.xrTableRow31.Weight = 1D;
            // 
            // xrRedestiREDEST
            // 
            this.xrRedestiREDEST.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrRedestiREDEST.Name = "xrRedestiREDEST";
            this.xrRedestiREDEST.StylePriority.UseBorders = false;
            this.xrRedestiREDEST.Text = "0.00";
            this.xrRedestiREDEST.Weight = 0.8032400537355473D;
            // 
            // xrRedestiCORTE
            // 
            this.xrRedestiCORTE.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrRedestiCORTE.Name = "xrRedestiCORTE";
            this.xrRedestiCORTE.StylePriority.UseBorders = false;
            this.xrRedestiCORTE.Text = "0.00";
            this.xrRedestiCORTE.Weight = 0.80324115924401418D;
            // 
            // xrTableCell174
            // 
            this.xrTableCell174.Name = "xrTableCell174";
            this.xrTableCell174.StylePriority.UseBorders = false;
            this.xrTableCell174.Weight = 0.62962987331026732D;
            // 
            // xrTableCell175
            // 
            this.xrTableCell175.Name = "xrTableCell175";
            this.xrTableCell175.Weight = 0.7638890412620869D;
            // 
            // xrTReDestiladoCombLiq
            // 
            this.xrTReDestiladoCombLiq.Name = "xrTReDestiladoCombLiq";
            this.xrTReDestiladoCombLiq.Text = "0.00";
            this.xrTReDestiladoCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTReDestiladoCombGas
            // 
            this.xrTReDestiladoCombGas.Name = "xrTReDestiladoCombGas";
            this.xrTReDestiladoCombGas.Text = "0.00";
            this.xrTReDestiladoCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTReDestiladoVaporTn
            // 
            this.xrTReDestiladoVaporTn.Name = "xrTReDestiladoVaporTn";
            this.xrTReDestiladoVaporTn.Text = "0.00";
            this.xrTReDestiladoVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTReDestiladoAguaNfr
            // 
            this.xrTReDestiladoAguaNfr.Name = "xrTReDestiladoAguaNfr";
            this.xrTReDestiladoAguaNfr.Text = "0.00";
            this.xrTReDestiladoAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTReDestiladoAireCom
            // 
            this.xrTReDestiladoAireCom.Name = "xrTReDestiladoAireCom";
            this.xrTReDestiladoAireCom.Text = "0.00";
            this.xrTReDestiladoAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTReDestiladoCompradakWh
            // 
            this.xrTReDestiladoCompradakWh.Name = "xrTReDestiladoCompradakWh";
            this.xrTReDestiladoCompradakWh.Text = "0.00";
            this.xrTReDestiladoCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTReDestiladoPropiakWh
            // 
            this.xrTReDestiladoPropiakWh.Name = "xrTReDestiladoPropiakWh";
            this.xrTReDestiladoPropiakWh.Text = "0.00";
            this.xrTReDestiladoPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell124,
            this.xrTDeganOperacion,
            this.xrTDeganCargaM3,
            this.xrTDeganCrgaPromedio,
            this.xrTDeganCapOperac,
            this.xrTableCell128,
            this.xrTDeganCombLiq,
            this.xrTDeganCombGas,
            this.xrTDeganVaporTn,
            this.xrTDeganAguaNfr,
            this.xrTDeganAireCom,
            this.xrTDeganCompradakWh,
            this.xrTDeganPropiakWh});
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell124
            // 
            this.xrTableCell124.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell124.Name = "xrTableCell124";
            this.xrTableCell124.StylePriority.UseFont = false;
            this.xrTableCell124.Text = "U. DEGAN";
            this.xrTableCell124.Weight = 0.77743449568368339D;
            // 
            // xrTDeganOperacion
            // 
            this.xrTDeganOperacion.Name = "xrTDeganOperacion";
            this.xrTDeganOperacion.Text = "0.00";
            this.xrTDeganOperacion.Weight = 0.514657551156342D;
            // 
            // xrTDeganCargaM3
            // 
            this.xrTDeganCargaM3.Name = "xrTDeganCargaM3";
            this.xrTDeganCargaM3.Text = "0.00";
            this.xrTDeganCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTDeganCrgaPromedio
            // 
            this.xrTDeganCrgaPromedio.Name = "xrTDeganCrgaPromedio";
            this.xrTDeganCrgaPromedio.Text = "0.00";
            this.xrTDeganCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTDeganCapOperac
            // 
            this.xrTDeganCapOperac.Name = "xrTDeganCapOperac";
            this.xrTDeganCapOperac.Text = "0.00";
            this.xrTDeganCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell128
            // 
            this.xrTableCell128.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
            this.xrTableCell128.Name = "xrTableCell128";
            this.xrTableCell128.Text = "xrTableCell128";
            this.xrTableCell128.Weight = 2.7638611833152913D;
            // 
            // xrTable13
            // 
            this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable13.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow32,
            this.xrTableRow33});
            this.xrTable13.SizeF = new System.Drawing.SizeF(269.7805F, 30.724F);
            this.xrTable13.StylePriority.UseBorders = false;
            this.xrTable13.StylePriority.UseFont = false;
            this.xrTable13.StylePriority.UseTextAlignment = false;
            this.xrTable13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow32
            // 
            this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell196,
            this.xrTableCell197,
            this.xrTableCell198,
            this.xrTableCell199});
            this.xrTableRow32.Name = "xrTableRow32";
            this.xrTableRow32.Weight = 1D;
            // 
            // xrTableCell196
            // 
            this.xrTableCell196.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell196.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell196.Name = "xrTableCell196";
            this.xrTableCell196.StylePriority.UseBackColor = false;
            this.xrTableCell196.StylePriority.UseBorders = false;
            this.xrTableCell196.Text = "GAS LIVIANA";
            this.xrTableCell196.Weight = 0.8032400537355473D;
            // 
            // xrTableCell197
            // 
            this.xrTableCell197.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell197.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell197.Name = "xrTableCell197";
            this.xrTableCell197.StylePriority.UseBackColor = false;
            this.xrTableCell197.StylePriority.UseBorders = false;
            this.xrTableCell197.Text = "GAS MEDIA";
            this.xrTableCell197.Weight = 0.80324115924401418D;
            // 
            // xrTableCell198
            // 
            this.xrTableCell198.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell198.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell198.Name = "xrTableCell198";
            this.xrTableCell198.StylePriority.UseBackColor = false;
            this.xrTableCell198.StylePriority.UseBorders = false;
            this.xrTableCell198.Text = "NAFTA";
            this.xrTableCell198.Weight = 0.62962987331026732D;
            // 
            // xrTableCell199
            // 
            this.xrTableCell199.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell199.Name = "xrTableCell199";
            this.xrTableCell199.StylePriority.UseBorders = false;
            this.xrTableCell199.Weight = 0.7638890412620869D;
            // 
            // xrTableRow33
            // 
            this.xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDeganLIVIANA,
            this.xrTDeganMEDIA,
            this.xrTDeganNAFTA,
            this.xrTableCell203});
            this.xrTableRow33.Name = "xrTableRow33";
            this.xrTableRow33.Weight = 1D;
            // 
            // xrTDeganLIVIANA
            // 
            this.xrTDeganLIVIANA.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDeganLIVIANA.Name = "xrTDeganLIVIANA";
            this.xrTDeganLIVIANA.StylePriority.UseBorders = false;
            this.xrTDeganLIVIANA.Text = "0.00";
            this.xrTDeganLIVIANA.Weight = 0.8032400537355473D;
            // 
            // xrTDeganMEDIA
            // 
            this.xrTDeganMEDIA.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDeganMEDIA.Name = "xrTDeganMEDIA";
            this.xrTDeganMEDIA.StylePriority.UseBorders = false;
            this.xrTDeganMEDIA.Text = "0.00";
            this.xrTDeganMEDIA.Weight = 0.80324115924401418D;
            // 
            // xrTDeganNAFTA
            // 
            this.xrTDeganNAFTA.Name = "xrTDeganNAFTA";
            this.xrTDeganNAFTA.StylePriority.UseBorders = false;
            this.xrTDeganNAFTA.Text = "0.00";
            this.xrTDeganNAFTA.Weight = 0.62962987331026732D;
            // 
            // xrTableCell203
            // 
            this.xrTableCell203.Name = "xrTableCell203";
            this.xrTableCell203.Weight = 0.7638890412620869D;
            // 
            // xrTDeganCombLiq
            // 
            this.xrTDeganCombLiq.Name = "xrTDeganCombLiq";
            this.xrTDeganCombLiq.Text = "0.00";
            this.xrTDeganCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTDeganCombGas
            // 
            this.xrTDeganCombGas.Name = "xrTDeganCombGas";
            this.xrTDeganCombGas.Text = "0.00";
            this.xrTDeganCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTDeganVaporTn
            // 
            this.xrTDeganVaporTn.Name = "xrTDeganVaporTn";
            this.xrTDeganVaporTn.Text = "0.00";
            this.xrTDeganVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTDeganAguaNfr
            // 
            this.xrTDeganAguaNfr.Name = "xrTDeganAguaNfr";
            this.xrTDeganAguaNfr.Text = "0.00";
            this.xrTDeganAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTDeganAireCom
            // 
            this.xrTDeganAireCom.Name = "xrTDeganAireCom";
            this.xrTDeganAireCom.Text = "0.00";
            this.xrTDeganAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTDeganCompradakWh
            // 
            this.xrTDeganCompradakWh.Name = "xrTDeganCompradakWh";
            this.xrTDeganCompradakWh.Text = "0.00";
            this.xrTDeganCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTDeganPropiakWh
            // 
            this.xrTDeganPropiakWh.Name = "xrTDeganPropiakWh";
            this.xrTDeganPropiakWh.Text = "0.00";
            this.xrTDeganPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell148,
            this.xrTNuisOperacion,
            this.xrTNuisCargaM3,
            this.xrTNuisCrgaPromedio,
            this.xrTNuisCapOperac,
            this.xrTableCell152,
            this.xrTNuisCombLiq,
            this.xrTNuisCombGas,
            this.xrTNuisVaporTn,
            this.xrTNuisAguaNfr,
            this.xrTNuisAireCom,
            this.xrTNuisCompradakWh,
            this.xrTNuisPropiakWh});
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell148
            // 
            this.xrTableCell148.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell148.Name = "xrTableCell148";
            this.xrTableCell148.StylePriority.UseFont = false;
            this.xrTableCell148.Text = "U. NUIS";
            this.xrTableCell148.Weight = 0.77743449568368339D;
            // 
            // xrTNuisOperacion
            // 
            this.xrTNuisOperacion.Name = "xrTNuisOperacion";
            this.xrTNuisOperacion.Text = "0.00";
            this.xrTNuisOperacion.Weight = 0.514657551156342D;
            // 
            // xrTNuisCargaM3
            // 
            this.xrTNuisCargaM3.Name = "xrTNuisCargaM3";
            this.xrTNuisCargaM3.Text = "0.00";
            this.xrTNuisCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTNuisCrgaPromedio
            // 
            this.xrTNuisCrgaPromedio.Name = "xrTNuisCrgaPromedio";
            this.xrTNuisCrgaPromedio.Text = "0.00";
            this.xrTNuisCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTNuisCapOperac
            // 
            this.xrTNuisCapOperac.Name = "xrTNuisCapOperac";
            this.xrTNuisCapOperac.Text = "0.00";
            this.xrTNuisCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell152
            // 
            this.xrTableCell152.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
            this.xrTableCell152.Name = "xrTableCell152";
            this.xrTableCell152.Text = "xrTableCell152";
            this.xrTableCell152.Weight = 2.7638611833152913D;
            // 
            // xrTable15
            // 
            this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable15.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0.2234955F);
            this.xrTable15.Name = "xrTable15";
            this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow34,
            this.xrTableRow35});
            this.xrTable15.SizeF = new System.Drawing.SizeF(269.7806F, 30.50049F);
            this.xrTable15.StylePriority.UseBorders = false;
            this.xrTable15.StylePriority.UseFont = false;
            this.xrTable15.StylePriority.UseTextAlignment = false;
            this.xrTable15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow34
            // 
            this.xrTableRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell204,
            this.xrTableCell205,
            this.xrTableCell206,
            this.xrTableCell207});
            this.xrTableRow34.Name = "xrTableRow34";
            this.xrTableRow34.Weight = 1D;
            // 
            // xrTableCell204
            // 
            this.xrTableCell204.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell204.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell204.Name = "xrTableCell204";
            this.xrTableCell204.StylePriority.UseBackColor = false;
            this.xrTableCell204.StylePriority.UseBorders = false;
            this.xrTableCell204.Text = "ISOMERADO";
            this.xrTableCell204.Weight = 0.8032400537355473D;
            // 
            // xrTableCell205
            // 
            this.xrTableCell205.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell205.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell205.Name = "xrTableCell205";
            this.xrTableCell205.StylePriority.UseBackColor = false;
            this.xrTableCell205.StylePriority.UseBorders = false;
            this.xrTableCell205.Text = "GAS REFINERIA";
            this.xrTableCell205.Weight = 0.80324115924401418D;
            // 
            // xrTableCell206
            // 
            this.xrTableCell206.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell206.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell206.Name = "xrTableCell206";
            this.xrTableCell206.StylePriority.UseBackColor = false;
            this.xrTableCell206.StylePriority.UseBorders = false;
            this.xrTableCell206.Text = "GAS LIVIANA";
            this.xrTableCell206.Weight = 0.62962987331026732D;
            // 
            // xrTableCell207
            // 
            this.xrTableCell207.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell207.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell207.Name = "xrTableCell207";
            this.xrTableCell207.StylePriority.UseBackColor = false;
            this.xrTableCell207.StylePriority.UseBorders = false;
            this.xrTableCell207.Text = "P. CRUDO";
            this.xrTableCell207.Weight = 0.7638890412620869D;
            // 
            // xrTableRow35
            // 
            this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTNuisISOMERADO,
            this.xrTNuisREFINERIA,
            this.xrTNuisLIVIANA,
            this.xrTNuisCRUDO});
            this.xrTableRow35.Name = "xrTableRow35";
            this.xrTableRow35.Weight = 1D;
            // 
            // xrTNuisISOMERADO
            // 
            this.xrTNuisISOMERADO.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTNuisISOMERADO.Name = "xrTNuisISOMERADO";
            this.xrTNuisISOMERADO.StylePriority.UseBorders = false;
            this.xrTNuisISOMERADO.Text = "0.00";
            this.xrTNuisISOMERADO.Weight = 0.8032400537355473D;
            // 
            // xrTNuisREFINERIA
            // 
            this.xrTNuisREFINERIA.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTNuisREFINERIA.Name = "xrTNuisREFINERIA";
            this.xrTNuisREFINERIA.StylePriority.UseBorders = false;
            this.xrTNuisREFINERIA.Text = "0.00";
            this.xrTNuisREFINERIA.Weight = 0.80324115924401418D;
            // 
            // xrTNuisLIVIANA
            // 
            this.xrTNuisLIVIANA.Name = "xrTNuisLIVIANA";
            this.xrTNuisLIVIANA.StylePriority.UseBorders = false;
            this.xrTNuisLIVIANA.Text = "0.00";
            this.xrTNuisLIVIANA.Weight = 0.62962987331026732D;
            // 
            // xrTNuisCRUDO
            // 
            this.xrTNuisCRUDO.Name = "xrTNuisCRUDO";
            this.xrTNuisCRUDO.Text = "0.00";
            this.xrTNuisCRUDO.Weight = 0.7638890412620869D;
            // 
            // xrTNuisCombLiq
            // 
            this.xrTNuisCombLiq.Name = "xrTNuisCombLiq";
            this.xrTNuisCombLiq.Text = "0.00";
            this.xrTNuisCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTNuisCombGas
            // 
            this.xrTNuisCombGas.Name = "xrTNuisCombGas";
            this.xrTNuisCombGas.Text = "0.00";
            this.xrTNuisCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTNuisVaporTn
            // 
            this.xrTNuisVaporTn.Name = "xrTNuisVaporTn";
            this.xrTNuisVaporTn.Text = "0.00";
            this.xrTNuisVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTNuisAguaNfr
            // 
            this.xrTNuisAguaNfr.Name = "xrTNuisAguaNfr";
            this.xrTNuisAguaNfr.Text = "0.00";
            this.xrTNuisAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTNuisAireCom
            // 
            this.xrTNuisAireCom.Name = "xrTNuisAireCom";
            this.xrTNuisAireCom.Text = "0.00";
            this.xrTNuisAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTNuisCompradakWh
            // 
            this.xrTNuisCompradakWh.Name = "xrTNuisCompradakWh";
            this.xrTNuisCompradakWh.Text = "0.00";
            this.xrTNuisCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTNuisPropiakWh
            // 
            this.xrTNuisPropiakWh.Name = "xrTNuisPropiakWh";
            this.xrTNuisPropiakWh.Text = "0.00";
            this.xrTNuisPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTableRow41
            // 
            this.xrTableRow41.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell220,
            this.xrTJetFuilOperacion,
            this.xrTJetFuilCargaM3,
            this.xrTJetFuilCrgaPromedio,
            this.xrTJetFuilCapOperac,
            this.xrTableCell224,
            this.xrTJetFuilCombLiq,
            this.xrTJetFuilCombGas,
            this.xrTJetFuilVaporTn,
            this.xrTJetFuilAguaNfr,
            this.xrTJetFuilAireCom,
            this.xrTJetFuilCompradakWh,
            this.xrTJetFuilPropiakWh});
            this.xrTableRow41.Name = "xrTableRow41";
            this.xrTableRow41.Weight = 1D;
            // 
            // xrTableCell220
            // 
            this.xrTableCell220.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell220.Name = "xrTableCell220";
            this.xrTableCell220.StylePriority.UseFont = false;
            this.xrTableCell220.Text = "U. TRATAMIENTO JET FUIL";
            this.xrTableCell220.Weight = 0.77743449568368339D;
            // 
            // xrTJetFuilOperacion
            // 
            this.xrTJetFuilOperacion.Name = "xrTJetFuilOperacion";
            this.xrTJetFuilOperacion.Text = "0.00";
            this.xrTJetFuilOperacion.Weight = 0.514657551156342D;
            // 
            // xrTJetFuilCargaM3
            // 
            this.xrTJetFuilCargaM3.Name = "xrTJetFuilCargaM3";
            this.xrTJetFuilCargaM3.Text = "0.00";
            this.xrTJetFuilCargaM3.Weight = 0.49620601785161478D;
            // 
            // xrTJetFuilCrgaPromedio
            // 
            this.xrTJetFuilCrgaPromedio.Name = "xrTJetFuilCrgaPromedio";
            this.xrTJetFuilCrgaPromedio.Text = "0.00";
            this.xrTJetFuilCrgaPromedio.Weight = 0.68622242901004316D;
            // 
            // xrTJetFuilCapOperac
            // 
            this.xrTJetFuilCapOperac.Name = "xrTJetFuilCapOperac";
            this.xrTJetFuilCapOperac.Text = "0.00";
            this.xrTJetFuilCapOperac.Weight = 0.67950680126464857D;
            // 
            // xrTableCell224
            // 
            this.xrTableCell224.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
            this.xrTableCell224.Name = "xrTableCell224";
            this.xrTableCell224.Text = "xrTableCell224";
            this.xrTableCell224.Weight = 2.7638611833152913D;
            // 
            // xrTable17
            // 
            this.xrTable17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable17.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0F);
            this.xrTable17.Name = "xrTable17";
            this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42,
            this.xrTableRow43});
            this.xrTable17.SizeF = new System.Drawing.SizeF(269.7806F, 30.72403F);
            this.xrTable17.StylePriority.UseBorders = false;
            this.xrTable17.StylePriority.UseFont = false;
            this.xrTable17.StylePriority.UseTextAlignment = false;
            this.xrTable17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell232,
            this.xrTableCell233,
            this.xrTableCell234,
            this.xrTableCell235});
            this.xrTableRow42.Name = "xrTableRow42";
            this.xrTableRow42.Weight = 1D;
            // 
            // xrTableCell232
            // 
            this.xrTableCell232.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell232.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell232.Name = "xrTableCell232";
            this.xrTableCell232.StylePriority.UseBackColor = false;
            this.xrTableCell232.StylePriority.UseBorders = false;
            this.xrTableCell232.Text = "DL";
            this.xrTableCell232.Weight = 0.8032400537355473D;
            // 
            // xrTableCell233
            // 
            this.xrTableCell233.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell233.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell233.Name = "xrTableCell233";
            this.xrTableCell233.StylePriority.UseBackColor = false;
            this.xrTableCell233.StylePriority.UseBorders = false;
            this.xrTableCell233.Text = "DM";
            this.xrTableCell233.Weight = 0.80324115924401418D;
            // 
            // xrTableCell234
            // 
            this.xrTableCell234.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell234.Name = "xrTableCell234";
            this.xrTableCell234.StylePriority.UseBorders = false;
            this.xrTableCell234.Weight = 0.62962987331026732D;
            // 
            // xrTableCell235
            // 
            this.xrTableCell235.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell235.Name = "xrTableCell235";
            this.xrTableCell235.StylePriority.UseBorders = false;
            this.xrTableCell235.Weight = 0.7638890412620869D;
            // 
            // xrTableRow43
            // 
            this.xrTableRow43.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTJetFuiDL,
            this.xrTJetFuiDM,
            this.xrTableCell238,
            this.xrTableCell239});
            this.xrTableRow43.Name = "xrTableRow43";
            this.xrTableRow43.Weight = 1D;
            // 
            // xrTJetFuiDL
            // 
            this.xrTJetFuiDL.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTJetFuiDL.Name = "xrTJetFuiDL";
            this.xrTJetFuiDL.StylePriority.UseBorders = false;
            this.xrTJetFuiDL.Text = "0.00";
            this.xrTJetFuiDL.Weight = 0.8032400537355473D;
            // 
            // xrTJetFuiDM
            // 
            this.xrTJetFuiDM.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTJetFuiDM.Name = "xrTJetFuiDM";
            this.xrTJetFuiDM.StylePriority.UseBorders = false;
            this.xrTJetFuiDM.Text = "0.00";
            this.xrTJetFuiDM.Weight = 0.80324115924401418D;
            // 
            // xrTableCell238
            // 
            this.xrTableCell238.Name = "xrTableCell238";
            this.xrTableCell238.StylePriority.UseBorders = false;
            this.xrTableCell238.Weight = 0.62962987331026732D;
            // 
            // xrTableCell239
            // 
            this.xrTableCell239.Name = "xrTableCell239";
            this.xrTableCell239.Weight = 0.7638890412620869D;
            // 
            // xrTJetFuilCombLiq
            // 
            this.xrTJetFuilCombLiq.Name = "xrTJetFuilCombLiq";
            this.xrTJetFuilCombLiq.Text = "0.00";
            this.xrTJetFuilCombLiq.Weight = 0.63179609359304867D;
            // 
            // xrTJetFuilCombGas
            // 
            this.xrTJetFuilCombGas.Name = "xrTJetFuilCombGas";
            this.xrTJetFuilCombGas.Text = "0.00";
            this.xrTJetFuilCombGas.Weight = 0.66736686935431844D;
            // 
            // xrTJetFuilVaporTn
            // 
            this.xrTJetFuilVaporTn.Name = "xrTJetFuilVaporTn";
            this.xrTJetFuilVaporTn.Text = "0.00";
            this.xrTJetFuilVaporTn.Weight = 0.60886836725441629D;
            // 
            // xrTJetFuilAguaNfr
            // 
            this.xrTJetFuilAguaNfr.Name = "xrTJetFuilAguaNfr";
            this.xrTJetFuilAguaNfr.Text = "0.00";
            this.xrTJetFuilAguaNfr.Weight = 0.59246877113720142D;
            // 
            // xrTJetFuilAireCom
            // 
            this.xrTJetFuilAireCom.Name = "xrTJetFuilAireCom";
            this.xrTJetFuilAireCom.Text = "0.00";
            this.xrTJetFuilAireCom.Weight = 0.7385109204857675D;
            // 
            // xrTJetFuilCompradakWh
            // 
            this.xrTJetFuilCompradakWh.Name = "xrTJetFuilCompradakWh";
            this.xrTJetFuilCompradakWh.Text = "0.00";
            this.xrTJetFuilCompradakWh.Weight = 0.73851339551566619D;
            // 
            // xrTJetFuilPropiakWh
            // 
            this.xrTJetFuilPropiakWh.Name = "xrTJetFuilPropiakWh";
            this.xrTJetFuilPropiakWh.Text = "0.00";
            this.xrTJetFuilPropiakWh.Weight = 0.61579629938948877D;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow14});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1026F, 114.676F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.xrTDestAdmosfericaOperacion,
            this.xrTDestAdmosfericaCargaM3,
            this.xrTDestAdmosfericaCrgaPromedio,
            this.xrTDestAdmosfericaCapOperac,
            this.xrTableCell21,
            this.xrTDestAdmosfericaCombLiq,
            this.xrTDestAdmosfericaCombGas,
            this.xrTDestAdmosfericaVaporTn,
            this.xrTDestAdmosfericaAguaNfr,
            this.xrTDestAdmosfericaAireCom,
            this.xrTDestAdmosfericaCompradakWh,
            this.xrTDestAdmosfericaPropiakWh});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Text = "U. DESTILACION ADMOSFERICA I";
            this.xrTableCell14.Weight = 0.7717207194437179D;
            // 
            // xrTDestAdmosfericaOperacion
            // 
            this.xrTDestAdmosfericaOperacion.Name = "xrTDestAdmosfericaOperacion";
            this.xrTDestAdmosfericaOperacion.Text = "0.00";
            this.xrTDestAdmosfericaOperacion.Weight = 0.51087495339428091D;
            // 
            // xrTDestAdmosfericaCargaM3
            // 
            this.xrTDestAdmosfericaCargaM3.Name = "xrTDestAdmosfericaCargaM3";
            this.xrTDestAdmosfericaCargaM3.Text = "0.00";
            this.xrTDestAdmosfericaCargaM3.Weight = 0.49255905683276896D;
            // 
            // xrTDestAdmosfericaCrgaPromedio
            // 
            this.xrTDestAdmosfericaCrgaPromedio.Name = "xrTDestAdmosfericaCrgaPromedio";
            this.xrTDestAdmosfericaCrgaPromedio.Text = "0.00";
            this.xrTDestAdmosfericaCrgaPromedio.Weight = 0.68117900010608312D;
            // 
            // xrTDestAdmosfericaCapOperac
            // 
            this.xrTDestAdmosfericaCapOperac.Name = "xrTDestAdmosfericaCapOperac";
            this.xrTDestAdmosfericaCapOperac.Text = "0.00";
            this.xrTDestAdmosfericaCapOperac.Weight = 0.67451267173024254D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.Weight = 2.743547428332116D;
            // 
            // xrTable7
            // 
            this.xrTable7.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16,
            this.xrTableRow17,
            this.xrTableRow18,
            this.xrTableRow19});
            this.xrTable7.SizeF = new System.Drawing.SizeF(269.7808F, 57.33802F);
            this.xrTable7.StylePriority.UseFont = false;
            this.xrTable7.StylePriority.UseTextAlignment = false;
            this.xrTable7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell80,
            this.xrTableCell81,
            this.xrTableCell82,
            this.xrTableCell83});
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseBackColor = false;
            this.xrTableCell80.Text = "C4 +";
            this.xrTableCell80.Weight = 0.8032400537355473D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseBackColor = false;
            this.xrTableCell81.Text = "LSR";
            this.xrTableCell81.Weight = 0.80324115924401418D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseBackColor = false;
            this.xrTableCell82.Text = "MSR";
            this.xrTableCell82.Weight = 0.62962987331026732D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.StylePriority.UseBackColor = false;
            this.xrTableCell83.Text = "DL";
            this.xrTableCell83.Weight = 0.7638890412620869D;
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDetil1C4,
            this.xrTDetil1LSR,
            this.xrTDetil1MSR,
            this.xrTDetil1DL});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTDetil1C4
            // 
            this.xrTDetil1C4.Name = "xrTDetil1C4";
            this.xrTDetil1C4.Text = "0.00";
            this.xrTDetil1C4.Weight = 0.8032400537355473D;
            // 
            // xrTDetil1LSR
            // 
            this.xrTDetil1LSR.Name = "xrTDetil1LSR";
            this.xrTDetil1LSR.Text = "0.00";
            this.xrTDetil1LSR.Weight = 0.80324115924401418D;
            // 
            // xrTDetil1MSR
            // 
            this.xrTDetil1MSR.Name = "xrTDetil1MSR";
            this.xrTDetil1MSR.Text = "0.00";
            this.xrTDetil1MSR.Weight = 0.62962987331026732D;
            // 
            // xrTDetil1DL
            // 
            this.xrTDetil1DL.Name = "xrTDetil1DL";
            this.xrTDetil1DL.Text = "0.00";
            this.xrTDetil1DL.Weight = 0.7638890412620869D;
            // 
            // xrTableRow18
            // 
            this.xrTableRow18.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell88,
            this.xrTableCell89,
            this.xrTableCell90,
            this.xrTableCell91});
            this.xrTableRow18.Name = "xrTableRow18";
            this.xrTableRow18.Weight = 1D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.StylePriority.UseBackColor = false;
            this.xrTableCell88.Text = "DM";
            this.xrTableCell88.Weight = 0.8032400537355473D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.StylePriority.UseBackColor = false;
            this.xrTableCell89.Text = "DP";
            this.xrTableCell89.Weight = 0.80324115924401418D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.StylePriority.UseBackColor = false;
            this.xrTableCell90.Text = "CR";
            this.xrTableCell90.Weight = 0.62962987331026732D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.StylePriority.UseBackColor = false;
            this.xrTableCell91.Text = "GM";
            this.xrTableCell91.Weight = 0.7638890412620869D;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDetil1DM,
            this.xrTDetil1DP,
            this.xrTDetil1CR,
            this.xrTDetil1GM});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTDetil1DM
            // 
            this.xrTDetil1DM.Name = "xrTDetil1DM";
            this.xrTDetil1DM.Text = "0.00";
            this.xrTDetil1DM.Weight = 0.8032400537355473D;
            // 
            // xrTDetil1DP
            // 
            this.xrTDetil1DP.Name = "xrTDetil1DP";
            this.xrTDetil1DP.Text = "0.00";
            this.xrTDetil1DP.Weight = 0.80324115924401418D;
            // 
            // xrTDetil1CR
            // 
            this.xrTDetil1CR.Name = "xrTDetil1CR";
            this.xrTDetil1CR.Text = "0.00";
            this.xrTDetil1CR.Weight = 0.62962987331026732D;
            // 
            // xrTDetil1GM
            // 
            this.xrTDetil1GM.Name = "xrTDetil1GM";
            this.xrTDetil1GM.Text = "0.00";
            this.xrTDetil1GM.Weight = 0.7638890412620869D;
            // 
            // xrTDestAdmosfericaCombLiq
            // 
            this.xrTDestAdmosfericaCombLiq.Name = "xrTDestAdmosfericaCombLiq";
            this.xrTDestAdmosfericaCombLiq.Text = "0.00";
            this.xrTDestAdmosfericaCombLiq.Weight = 0.62715324871990208D;
            // 
            // xrTDestAdmosfericaCombGas
            // 
            this.xrTDestAdmosfericaCombGas.Name = "xrTDestAdmosfericaCombGas";
            this.xrTDestAdmosfericaCombGas.Text = "0.00";
            this.xrTDestAdmosfericaCombGas.Weight = 0.662461203230017D;
            // 
            // xrTDestAdmosfericaVaporTn
            // 
            this.xrTDestAdmosfericaVaporTn.Name = "xrTDestAdmosfericaVaporTn";
            this.xrTDestAdmosfericaVaporTn.Text = "0.00";
            this.xrTDestAdmosfericaVaporTn.Weight = 0.60439434675135417D;
            // 
            // xrTDestAdmosfericaAguaNfr
            // 
            this.xrTDestAdmosfericaAguaNfr.Name = "xrTDestAdmosfericaAguaNfr";
            this.xrTDestAdmosfericaAguaNfr.Text = "0.00";
            this.xrTDestAdmosfericaAguaNfr.Weight = 0.588114961724658D;
            // 
            // xrTDestAdmosfericaAireCom
            // 
            this.xrTDestAdmosfericaAireCom.Name = "xrTDestAdmosfericaAireCom";
            this.xrTDestAdmosfericaAireCom.Text = "0.00";
            this.xrTDestAdmosfericaAireCom.Weight = 0.73308197571010769D;
            // 
            // xrTDestAdmosfericaCompradakWh
            // 
            this.xrTDestAdmosfericaCompradakWh.Name = "xrTDestAdmosfericaCompradakWh";
            this.xrTDestAdmosfericaCompradakWh.Text = "0.00";
            this.xrTDestAdmosfericaCompradakWh.Weight = 0.73308513868709813D;
            // 
            // xrTDestAdmosfericaPropiakWh
            // 
            this.xrTDestAdmosfericaPropiakWh.Name = "xrTDestAdmosfericaPropiakWh";
            this.xrTDestAdmosfericaPropiakWh.Text = "0.00";
            this.xrTDestAdmosfericaPropiakWh.Weight = 0.60109931598820132D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell44,
            this.xrTDestAdmosferica2Operacion,
            this.xrTDestAdmosferica2CargaM3,
            this.xrTDestAdmosferica2CrgaPromedio,
            this.xrTDestAdmosferica2CapOperac,
            this.xrTableCell60,
            this.xrTDestAdmosferica2CombLiq,
            this.xrTDestAdmosferica2CombGas,
            this.xrTDestAdmosferica2VaporTn,
            this.xrTDestAdmosferica2AguaNfr,
            this.xrTDestAdmosferica2AireCom,
            this.xrTDestAdmosferica2CompradakWh,
            this.xrTDestAdmosferica2PropiakWh});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Text = "U. DESTILACION ADMOSFERICA II";
            this.xrTableCell44.Weight = 0.7717207194437179D;
            // 
            // xrTDestAdmosferica2Operacion
            // 
            this.xrTDestAdmosferica2Operacion.Name = "xrTDestAdmosferica2Operacion";
            this.xrTDestAdmosferica2Operacion.Text = "0.00";
            this.xrTDestAdmosferica2Operacion.Weight = 0.51087495339428091D;
            // 
            // xrTDestAdmosferica2CargaM3
            // 
            this.xrTDestAdmosferica2CargaM3.Name = "xrTDestAdmosferica2CargaM3";
            this.xrTDestAdmosferica2CargaM3.Text = "0.00";
            this.xrTDestAdmosferica2CargaM3.Weight = 0.49255905683276896D;
            // 
            // xrTDestAdmosferica2CrgaPromedio
            // 
            this.xrTDestAdmosferica2CrgaPromedio.Name = "xrTDestAdmosferica2CrgaPromedio";
            this.xrTDestAdmosferica2CrgaPromedio.Text = "0.00";
            this.xrTDestAdmosferica2CrgaPromedio.Weight = 0.68117900010608312D;
            // 
            // xrTDestAdmosferica2CapOperac
            // 
            this.xrTDestAdmosferica2CapOperac.Name = "xrTDestAdmosferica2CapOperac";
            this.xrTDestAdmosferica2CapOperac.Text = "0.00";
            this.xrTDestAdmosferica2CapOperac.Weight = 0.67451267173024254D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Text = "xrTableCell60";
            this.xrTableCell60.Weight = 2.743547428332116D;
            // 
            // xrTable8
            // 
            this.xrTable8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable8.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20,
            this.xrTableRow21,
            this.xrTableRow22,
            this.xrTableRow23});
            this.xrTable8.SizeF = new System.Drawing.SizeF(269.7807F, 57.33802F);
            this.xrTable8.StylePriority.UseBorders = false;
            this.xrTable8.StylePriority.UseFont = false;
            this.xrTable8.StylePriority.UseTextAlignment = false;
            this.xrTable8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99});
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 1D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell96.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.StylePriority.UseBackColor = false;
            this.xrTableCell96.StylePriority.UseBorders = false;
            this.xrTableCell96.Text = "C4 +";
            this.xrTableCell96.Weight = 0.8032400537355473D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell97.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.StylePriority.UseBackColor = false;
            this.xrTableCell97.StylePriority.UseBorders = false;
            this.xrTableCell97.Text = "LSR";
            this.xrTableCell97.Weight = 0.80324115924401418D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell98.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.StylePriority.UseBackColor = false;
            this.xrTableCell98.StylePriority.UseBorders = false;
            this.xrTableCell98.Text = "MSR";
            this.xrTableCell98.Weight = 0.62962987331026732D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell99.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.StylePriority.UseBackColor = false;
            this.xrTableCell99.StylePriority.UseBorders = false;
            this.xrTableCell99.Text = "DL";
            this.xrTableCell99.Weight = 0.7638890412620869D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDetil2C4,
            this.xrTDetil2LSR,
            this.xrTDetil2MSR,
            this.xrTDetil2DL});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTDetil2C4
            // 
            this.xrTDetil2C4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDetil2C4.Name = "xrTDetil2C4";
            this.xrTDetil2C4.StylePriority.UseBorders = false;
            this.xrTDetil2C4.Text = "0.00";
            this.xrTDetil2C4.Weight = 0.8032400537355473D;
            // 
            // xrTDetil2LSR
            // 
            this.xrTDetil2LSR.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDetil2LSR.Name = "xrTDetil2LSR";
            this.xrTDetil2LSR.StylePriority.UseBorders = false;
            this.xrTDetil2LSR.Text = "0.00";
            this.xrTDetil2LSR.Weight = 0.80324115924401418D;
            // 
            // xrTDetil2MSR
            // 
            this.xrTDetil2MSR.Name = "xrTDetil2MSR";
            this.xrTDetil2MSR.StylePriority.UseBorders = false;
            this.xrTDetil2MSR.Text = "0.00";
            this.xrTDetil2MSR.Weight = 0.62962987331026732D;
            // 
            // xrTDetil2DL
            // 
            this.xrTDetil2DL.Name = "xrTDetil2DL";
            this.xrTDetil2DL.Text = "0.00";
            this.xrTDetil2DL.Weight = 0.7638890412620869D;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell104,
            this.xrTableCell105,
            this.xrTableCell106,
            this.xrTableCell107});
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 1D;
            // 
            // xrTableCell104
            // 
            this.xrTableCell104.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell104.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell104.Name = "xrTableCell104";
            this.xrTableCell104.StylePriority.UseBackColor = false;
            this.xrTableCell104.StylePriority.UseBorders = false;
            this.xrTableCell104.Text = "DM";
            this.xrTableCell104.Weight = 0.8032400537355473D;
            // 
            // xrTableCell105
            // 
            this.xrTableCell105.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell105.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell105.Name = "xrTableCell105";
            this.xrTableCell105.StylePriority.UseBackColor = false;
            this.xrTableCell105.StylePriority.UseBorders = false;
            this.xrTableCell105.Text = "DP";
            this.xrTableCell105.Weight = 0.80324115924401418D;
            // 
            // xrTableCell106
            // 
            this.xrTableCell106.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell106.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell106.Name = "xrTableCell106";
            this.xrTableCell106.StylePriority.UseBackColor = false;
            this.xrTableCell106.StylePriority.UseBorders = false;
            this.xrTableCell106.Text = "CR";
            this.xrTableCell106.Weight = 0.62962987331026732D;
            // 
            // xrTableCell107
            // 
            this.xrTableCell107.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell107.Name = "xrTableCell107";
            this.xrTableCell107.StylePriority.UseBackColor = false;
            this.xrTableCell107.Text = "RECIRC";
            this.xrTableCell107.Weight = 0.7638890412620869D;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDetil2DM,
            this.xrTDetil2DP,
            this.xrTDetil2CR,
            this.xrTDetil2RECIRC});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTDetil2DM
            // 
            this.xrTDetil2DM.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTDetil2DM.Name = "xrTDetil2DM";
            this.xrTDetil2DM.StylePriority.UseBorders = false;
            this.xrTDetil2DM.Text = "0.00";
            this.xrTDetil2DM.Weight = 0.8032400537355473D;
            // 
            // xrTDetil2DP
            // 
            this.xrTDetil2DP.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTDetil2DP.Name = "xrTDetil2DP";
            this.xrTDetil2DP.StylePriority.UseBorders = false;
            this.xrTDetil2DP.Text = "0.00";
            this.xrTDetil2DP.Weight = 0.80324115924401418D;
            // 
            // xrTDetil2CR
            // 
            this.xrTDetil2CR.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTDetil2CR.Name = "xrTDetil2CR";
            this.xrTDetil2CR.StylePriority.UseBorders = false;
            this.xrTDetil2CR.Text = "0.00";
            this.xrTDetil2CR.Weight = 0.62962987331026732D;
            // 
            // xrTDetil2RECIRC
            // 
            this.xrTDetil2RECIRC.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTDetil2RECIRC.Name = "xrTDetil2RECIRC";
            this.xrTDetil2RECIRC.StylePriority.UseBorders = false;
            this.xrTDetil2RECIRC.Text = "0.00";
            this.xrTDetil2RECIRC.Weight = 0.7638890412620869D;
            // 
            // xrTDestAdmosferica2CombLiq
            // 
            this.xrTDestAdmosferica2CombLiq.Name = "xrTDestAdmosferica2CombLiq";
            this.xrTDestAdmosferica2CombLiq.Text = "0.00";
            this.xrTDestAdmosferica2CombLiq.Weight = 0.62715324871990208D;
            // 
            // xrTDestAdmosferica2CombGas
            // 
            this.xrTDestAdmosferica2CombGas.Name = "xrTDestAdmosferica2CombGas";
            this.xrTDestAdmosferica2CombGas.Text = "0.00";
            this.xrTDestAdmosferica2CombGas.Weight = 0.662461203230017D;
            // 
            // xrTDestAdmosferica2VaporTn
            // 
            this.xrTDestAdmosferica2VaporTn.Name = "xrTDestAdmosferica2VaporTn";
            this.xrTDestAdmosferica2VaporTn.Text = "0.00";
            this.xrTDestAdmosferica2VaporTn.Weight = 0.60439434675135417D;
            // 
            // xrTDestAdmosferica2AguaNfr
            // 
            this.xrTDestAdmosferica2AguaNfr.Name = "xrTDestAdmosferica2AguaNfr";
            this.xrTDestAdmosferica2AguaNfr.Text = "0.00";
            this.xrTDestAdmosferica2AguaNfr.Weight = 0.588114961724658D;
            // 
            // xrTDestAdmosferica2AireCom
            // 
            this.xrTDestAdmosferica2AireCom.Name = "xrTDestAdmosferica2AireCom";
            this.xrTDestAdmosferica2AireCom.Text = "0.00";
            this.xrTDestAdmosferica2AireCom.Weight = 0.73308197571010769D;
            // 
            // xrTDestAdmosferica2CompradakWh
            // 
            this.xrTDestAdmosferica2CompradakWh.Name = "xrTDestAdmosferica2CompradakWh";
            this.xrTDestAdmosferica2CompradakWh.Text = "0.00";
            this.xrTDestAdmosferica2CompradakWh.Weight = 0.73308513868709813D;
            // 
            // xrTDestAdmosferica2PropiakWh
            // 
            this.xrTDestAdmosferica2PropiakWh.Name = "xrTDestAdmosferica2PropiakWh";
            this.xrTDestAdmosferica2PropiakWh.Text = "0.00";
            this.xrTDestAdmosferica2PropiakWh.Weight = 0.60109931598820132D;
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
            this.xrTable26});
            this.BottomMargin.HeightF = 25F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable26
            // 
            this.xrTable26.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable26.Name = "xrTable26";
            this.xrTable26.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow63});
            this.xrTable26.SizeF = new System.Drawing.SizeF(1025F, 25F);
            this.xrTable26.StylePriority.UseTextAlignment = false;
            this.xrTable26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow63
            // 
            this.xrTableRow63.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16,
            this.xrTQuienImprime,
            this.xrTableCell24,
            this.xrTFechaHora});
            this.xrTableRow63.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTableRow63.Name = "xrTableRow63";
            this.xrTableRow63.StylePriority.UseFont = false;
            this.xrTableRow63.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "Impreso por: ";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell16.Weight = 0.30115943587550448D;
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
            this.xrTable29,
            this.xrTable28,
            this.xrPictureBox2,
            this.xrPictureBox1,
            this.xrPictureBox3,
            this.xrPageInfo1,
            this.xrTable4,
            this.xrTable27,
            this.xrTable1,
            this.xrTable9,
            this.xrTable2,
            this.xrTable3,
            this.xrTable6});
            this.pageHeaderBand1.HeightF = 170.6956F;
            this.pageHeaderBand1.Name = "pageHeaderBand1";
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
            this.xrTableCell18});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Weight = 1D;
            // 
            // xrTable28
            // 
            this.xrTable28.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable28.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable28.LocationFloat = new DevExpress.Utils.PointFloat(143.4999F, 49.81413F);
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
            this.xrTableCell26,
            this.xrTableCell28,
            this.xrTableCell30,
            this.xrTableCell33});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.Text = "Código:";
            this.xrTableCell26.Weight = 1D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Text = "Versión:";
            this.xrTableCell28.Weight = 0.82134934269309889D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseBorders = false;
            this.xrTableCell30.Text = "Aprobado:";
            this.xrTableCell30.Weight = 1.4701330216349817D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Text = "Páginas:";
            this.xrTableCell33.Weight = 0.70851763567191939D;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(882.0611F, 4.000015F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(104.6874F, 62F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(987.2499F, 5.499962F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(35F, 54F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPictureBox3
            // 
            this.xrPictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox3.Image")));
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(11.36649F, 4.000008F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(119.875F, 55.49995F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(801.9915F, 49.81413F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(70.06964F, 13.54167F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrTable4
            // 
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0.499939F, 0F);
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
            this.xrTableCell34});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.StylePriority.UseBorders = false;
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.Weight = 1D;
            // 
            // xrTable27
            // 
            this.xrTable27.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable27.LocationFloat = new DevExpress.Utils.PointFloat(143.4999F, 0F);
            this.xrTable27.Name = "xrTable27";
            this.xrTable27.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable27.SizeF = new System.Drawing.SizeF(738.5612F, 48.95826F);
            this.xrTable27.StylePriority.UseBorders = false;
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell38});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            this.xrTableCell38.Text = "RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS";
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell38.Weight = 1D;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.Transparent;
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 125.9039F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable1.SizeF = new System.Drawing.SizeF(578.6458F, 44.79167F);
            this.xrTable1.StylePriority.UseBackColor = false;
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
            this.xrTableCell7,
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
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "CARGA PROMEDIO BBL/D";
            this.xrTableCell7.Weight = 0.75946434866426749D;
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
            // xrTable9
            // 
            this.xrTable9.BackColor = System.Drawing.Color.Transparent;
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(577.6457F, 125.9039F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable9.SizeF = new System.Drawing.SizeF(448.3543F, 13.54167F);
            this.xrTable9.StylePriority.UseBackColor = false;
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
            this.xrTableCell50.Weight = 1.00762322762337D;
            // 
            // xrTable2
            // 
            this.xrTable2.BackColor = System.Drawing.Color.Transparent;
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(577.6457F, 139.4456F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(448.3542F, 31.24997F);
            this.xrTable2.StylePriority.UseBackColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell15});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "COMB LIQUIDO M3";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 0.85549854741164288D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Text = "COMB GAS MMSCF";
            this.xrTableCell9.Weight = 0.9036646046240242D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "VAPOR TN";
            this.xrTableCell10.Weight = 0.8244535520040952D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "AGUA NFR M3";
            this.xrTableCell11.Weight = 0.80224722749218369D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Text = "AIRE COMP NM3";
            this.xrTableCell12.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "COMPRADA kWh";
            this.xrTableCell13.Weight = 1.0000008466970349D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Text = "PROPIA hWh";
            this.xrTableCell15.Weight = 0.833832320034013D;
            // 
            // xrTable3
            // 
            this.xrTable3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.59777F);
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
            // xrTable6
            // 
            this.xrTable6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 108.1956F);
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
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
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
            // repRendimientoVolumetricoGEldeBell
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
            this.Margins = new System.Drawing.Printing.Margins(35, 39, 20, 25);
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Metodos Propios

    #endregion

}
