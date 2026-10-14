using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using ServiceStack.Common.Extensions;

/// <summary>
/// Summary description for repDj
/// </summary>
public class repRendimientoVolumetricoGualvertoV : DevExpress.XtraReports.UI.XtraReport
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
    private XRTable xrTable24;
    private XRTableRow xrTableRow59;
    private XRTableCell xrTableCell340;
    private XRTable xrTable17;
    private XRTableRow xrTableRow41;
    private XRTableCell xrTableCell220;
    public XRTableCell xrTVacio1Operacion;
    public XRTableCell xrTVacio1CargaM3;
    public XRTableCell xrTVacio1CapOperac;
    private XRTableCell xrTableCell224;
    private XRTable xrTable18;
    private XRTableRow xrTableRow42;
    private XRTableCell xrTableCell225;
    private XRTableCell xrTableCell226;
    private XRTableCell xrTableCell227;
    private XRTableCell xrTableCell228;
    private XRTableRow xrTableRow43;
    public XRTableCell xrTVacio1CABZ;
    public XRTableCell xrTVacio1LAT;
    public XRTableCell xrTVacio1FONDO;
    private XRTableCell xrTableCell232;
    public XRTableCell xrTVacio1CombLiq;
    public XRTableCell xrTVacio1CombGas;
    public XRTableCell xrTVacio1VaporTn;
    public XRTableCell xrTVacio1AguaNfr;
    public XRTableCell xrTVacio1AireCom;
    public XRTableCell xrTVacio1CompradakWh;
    public XRTableCell xrTVacio1PropiakWh;
    private XRTableRow xrTableRow44;
    private XRTableCell xrTableCell240;
    public XRTableCell xrTVacio2Operacion;
    public XRTableCell xrTVacio2CargaM3;
    public XRTableCell xrTVacio2CapOperac;
    private XRTableCell xrTableCell244;
    private XRTable xrTable19;
    private XRTableRow xrTableRow45;
    private XRTableCell xrTableCell245;
    private XRTableCell xrTableCell246;
    private XRTableCell xrTableCell247;
    private XRTableCell xrTableCell248;
    private XRTableRow xrTableRow46;
    public XRTableCell xrTVacio2S10DTL;
    public XRTableCell xrTVacio2S20DTL;
    public XRTableCell xrTVacio2S30DTL;
    private XRTableCell xrTableCell252;
    public XRTableCell xrTVacio2CombLiq;
    public XRTableCell xrTVacio2CombGas;
    public XRTableCell xrTVacio2VaporTn;
    public XRTableCell xrTVacio2AguaNfr;
    public XRTableCell xrTVacio2AireCom;
    public XRTableCell xrTVacio2CompradakWh;
    public XRTableCell xrTVacio2PropiakWh;
    private XRTableRow xrTableRow47;
    private XRTableCell xrTableCell260;
    public XRTableCell xrTDesasfaltizacionOperacion;
    public XRTableCell xrTDesasfaltizacionCargaM3;
    public XRTableCell xrTDesasfaltizacionCapOperac;
    private XRTableCell xrTableCell264;
    private XRTable xrTable20;
    private XRTableRow xrTableRow48;
    private XRTableCell xrTableCell265;
    private XRTableCell xrTableCell266;
    private XRTableCell xrTableCell267;
    private XRTableCell xrTableCell268;
    private XRTableRow xrTableRow49;
    public XRTableCell xrTDesasfaltizacionSTOCK;
    public XRTableCell xrTDesasfaltizacionASFALTO;
    public XRTableCell xrTableCell271;
    private XRTableCell xrTableCell272;
    public XRTableCell xrTDesasfaltizacionCombLiq;
    public XRTableCell xrTDesasfaltizacionCombGas;
    public XRTableCell xrTDesasfaltizacionVaporTn;
    public XRTableCell xrTDesasfaltizacionAguaNfr;
    public XRTableCell xrTDesasfaltizacionAireCom;
    public XRTableCell xrTDesasfaltizacionCompradakWh;
    public XRTableCell xrTDesasfaltizacionPropiakWh;
    private XRTableRow xrTableRow50;
    private XRTableCell xrTableCell280;
    public XRTableCell xrTRefFurfuralOperacion;
    public XRTableCell xrTRefFurfuralCargaM3;
    public XRTableCell xrTRefFurfuralCapOperac;
    private XRTableCell xrTableCell284;
    private XRTable xrTable21;
    private XRTableRow xrTableRow51;
    private XRTableCell xrTableCell285;
    private XRTableCell xrTableCell286;
    private XRTableCell xrTableCell287;
    private XRTableCell xrTableCell288;
    private XRTableRow xrTableRow52;
    public XRTableCell xrTRefFurfuS10REF;
    public XRTableCell xrTRefFurfuS20REF;
    public XRTableCell xrTRefFurfuS30REF;
    public XRTableCell xrTRefFurfuBSREF;
    public XRTableCell xrTRefFurfuralCombLiq;
    public XRTableCell xrTRefFurfuralCombGas;
    public XRTableCell xrTRefFurfuralVaporTn;
    public XRTableCell xrTRefFurfuralAguaNfr;
    public XRTableCell xrTRefFurfuralAireCom;
    public XRTableCell xrTRefFurfuralCompradakWh;
    public XRTableCell xrTRefFurfuralPropiakWh;
    private XRTableRow xrTableRow53;
    private XRTableCell xrTableCell300;
    public XRTableCell xrTDesparafinacionOperacion;
    public XRTableCell xrTDesparafinacionCargaM3;
    public XRTableCell xrTDesparafinacionCapOperac;
    private XRTableCell xrTableCell304;
    private XRTable xrTable22;
    private XRTableRow xrTableRow54;
    private XRTableCell xrTableCell305;
    private XRTableCell xrTableCell306;
    private XRTableCell xrTableCell307;
    private XRTableCell xrTableCell308;
    private XRTableRow xrTableRow55;
    public XRTableCell xrTDesparaS10DES;
    public XRTableCell xrTDesparaS20DES;
    public XRTableCell xrTDesparaS30DES;
    public XRTableCell xrTDesparaBSDES;
    public XRTableCell xrTDesparafinacionCombLiq;
    public XRTableCell xrTDesparafinacionCombGas;
    public XRTableCell xrTDesparafinacionVaporTn;
    public XRTableCell xrTDesparafinacionAguaNfr;
    public XRTableCell xrTDesparafinacionAireCom;
    public XRTableCell xrTDesparafinacionCompradakWh;
    public XRTableCell xrTDesparafinacionPropiakWh;
    private XRTableRow xrTableRow56;
    private XRTableCell xrTableCell320;
    public XRTableCell xrTDesaseitadoOperacion;
    public XRTableCell xrTDesaseitadoCargaM3;
    public XRTableCell xrTDesaseitadoCapOperac;
    private XRTableCell xrTableCell324;
    private XRTable xrTable23;
    private XRTableRow xrTableRow57;
    private XRTableCell xrTableCell325;
    private XRTableCell xrTableCell326;
    private XRTableCell xrTableCell327;
    private XRTableCell xrTableCell328;
    private XRTableRow xrTableRow58;
    public XRTableCell xrTDestadPaPARAFINA;
    public XRTableCell xrTDestadPaACEITE;
    private XRTableCell xrTableCell331;
    private XRTableCell xrTableCell332;
    public XRTableCell xrTDesaseitadoCombLiq;
    public XRTableCell xrTDesaseitadoCombGas;
    public XRTableCell xrTDesaseitadoVaporTn;
    public XRTableCell xrTDesaseitadoAguaNfr;
    public XRTableCell xrTDesaseitadoAireCom;
    public XRTableCell xrTDesaseitadoCompradakWh;
    public XRTableCell xrTDesaseitadoPropiakWh;
    private XRTableRow xrTableRow60;
    private XRTableCell xrTableCell341;
    public XRTableCell xrTHidroterminadoOperacion;
    public XRTableCell xrTHidroterminadoCargaM3;
    public XRTableCell xrTHidroterminadoCapOperac;
    private XRTableCell xrTableCell345;
    private XRTable xrTable25;
    private XRTableRow xrTableRow61;
    private XRTableCell xrTableCell353;
    private XRTableCell xrTableCell354;
    private XRTableCell xrTableCell355;
    private XRTableCell xrTableCell356;
    private XRTableRow xrTableRow62;
    public XRTableCell xrTHidroTerminS10BA;
    public XRTableCell xrTHidroTerminS20BA;
    public XRTableCell xrTHidroTerminS30BA;
    public XRTableCell xrTHidroTerminBSBA;
    public XRTableCell xrTHidroterminadoCombLiq;
    public XRTableCell xrTHidroterminadoCombGas;
    public XRTableCell xrTHidroterminadoVaporTn;
    public XRTableCell xrTHidroterminadoAguaNfr;
    public XRTableCell xrTHidroterminadoAireCom;
    public XRTableCell xrTHidroterminadoCompradakWh;
    public XRTableCell xrTHidroterminadoPropiakWh;
    private XRTable xrTable11;
    private XRTableRow xrTableRow36;
    private XRTableCell xrTableCell176;
    public XRTableCell xrTRecuperacnOperacion;
    public XRTableCell xrTRecuperacnCargaM3;
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
    public XRTableCell xrTRecupGasc4;
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
    public XRTableCell xrTRefCataliticaCargaM3;
    public XRTableCell xrTRefCataliticaCapOperac;
    private XRTableCell xrTableCell72;
    private XRTable xrTable10;
    private XRTableRow xrTableRow28;
    private XRTableCell xrTableCell160;
    private XRTableCell xrTableCell161;
    private XRTableCell xrTableCell162;
    private XRTableCell xrTableCell163;
    private XRTableRow xrTableRow29;
    public XRTableCell xrTReforCatGASREF;
    public XRTableCell xrTReforCatC4;
    public XRTableCell xrTReforCatPLAT;
    private XRTableCell xrTableCell167;
    public XRTableCell xrTRefCataliticaCombLiq;
    public XRTableCell xrTRefCataliticaCombGas;
    public XRTableCell xrTRefCataliticaVaporTn;
    public XRTableCell xrTRefCataliticaAguaNfr;
    public XRTableCell xrTRefCataliticaAireCom;
    public XRTableCell xrTRefCataliticaCompradakWh;
    public XRTableCell xrTRefCataliticaPropiakWh;
    private XRTableRow xrTableRow24;
    private XRTableCell xrTableCell112;
    public XRTableCell xrTNurcRefCataliticaOperacion;
    public XRTableCell xrTNurcRefCataliticaCargaM3;
    public XRTableCell xrTNurcRefCataliticaCapOperac;
    private XRTableCell xrTableCell116;
    private XRTable xrTable12;
    private XRTableRow xrTableRow30;
    private XRTableCell xrTableCell168;
    private XRTableCell xrTableCell169;
    private XRTableCell xrTableCell170;
    private XRTableCell xrTableCell171;
    public XRTableRow xrTableRow31;
    public XRTableCell xrTNurcGASREF;
    public XRTableCell xrTNurcGLP;
    public XRTableCell xrTNurcPLAT;
    private XRTableCell xrTableCell175;
    public XRTableCell xrTNurcRefCataliticaCombLiq;
    public XRTableCell xrTNurcRefCataliticaCombGas;
    public XRTableCell xrTNurcRefCataliticaVaporTn;
    public XRTableCell xrTNurcRefCataliticaAguaNfr;
    public XRTableCell xrTNurcRefCataliticaAireCom;
    public XRTableCell xrTNurcRefCataliticaCompradakWh;
    public XRTableCell xrTNurcRefCataliticaPropiakWh;
    private XRTableRow xrTableRow26;
    private XRTableCell xrTableCell136;
    public XRTableCell xrTDestiladoOperacion;
    public XRTableCell xrTDestiladoCargaM3;
    public XRTableCell xrTDestiladoCapOperac;
    private XRTableCell xrTableCell140;
    private XRTable xrTable13;
    private XRTableRow xrTableRow32;
    private XRTableCell xrTableCell196;
    private XRTableCell xrTableCell197;
    private XRTableCell xrTableCell198;
    private XRTableCell xrTableCell199;
    private XRTableRow xrTableRow33;
    public XRTableCell xrTRedistFDOCBZA;
    public XRTableCell xrTRedistREDEST;
    private XRTableCell xrTableCell202;
    private XRTableCell xrTableCell203;
    public XRTableCell xrTDestiladoCombLiq;
    public XRTableCell xrTDestiladoCombGas;
    public XRTableCell xrTDestiladoVaporTn;
    public XRTableCell xrTDestiladoAguaNfr;
    public XRTableCell xrTDestiladoAireCom;
    public XRTableCell xrTDestiladoCompradakWh;
    public XRTableCell xrTDestiladoPropiakWh;
    private XRTableRow xrTableRow25;
    private XRTableCell xrTableCell124;
    public XRTableCell xrTAlquilatosOperacion;
    public XRTableCell xrTAlquilatosCargaM3;
    public XRTableCell xrTAlquilatosCapOperac;
    private XRTableCell xrTableCell128;
    private XRTable xrTable15;
    private XRTableRow xrTableRow34;
    private XRTableCell xrTableCell204;
    private XRTableCell xrTableCell205;
    private XRTableCell xrTableCell206;
    private XRTableCell xrTableCell207;
    private XRTableRow xrTableRow35;
    public XRTableCell xrTAlquilatosc5;
    public XRTableCell xrTAlquilatosc6;
    private XRTableCell xrTableCell210;
    private XRTableCell xrTableCell211;
    public XRTableCell xrTAlquilatosCombLiq;
    public XRTableCell xrTAlquilatosCombGas;
    public XRTableCell xrTAlquilatosVaporTn;
    public XRTableCell xrTAlquilatosAguaNfr;
    public XRTableCell xrTAlquilatosAireCom;
    public XRTableCell xrTAlquilatosCompradakWh;
    public XRTableCell xrTAlquilatosPropiakWh;
    private XRTableRow xrTableRow27;
    private XRTableCell xrTableCell148;
    public XRTableCell xrTDesopentanizaOperacion;
    public XRTableCell xrTDesopentanizaCargaM3;
    public XRTableCell xrTDesopentanizaCapOperac;
    private XRTableCell xrTableCell152;
    private XRTable xrTable16;
    private XRTableRow xrTableRow39;
    private XRTableCell xrTableCell212;
    private XRTableCell xrTableCell213;
    private XRTableCell xrTableCell214;
    private XRTableCell xrTableCell215;
    private XRTableRow xrTableRow40;
    public XRTableCell xrTDeisopentaPESADOS;
    public XRTableCell xrTDeisopentaC4;
    public XRTableCell xrTDeisopentaC5;
    private XRTableCell xrTableCell219;
    public XRTableCell xrTDesopentanizaCombLiq;
    public XRTableCell xrTDesopentanizaCombGas;
    public XRTableCell xrTDesopentanizaVaporTn;
    public XRTableCell xrTDesopentanizaAguaNfr;
    public XRTableCell xrTDesopentanizaAireCom;
    public XRTableCell xrTDesopentanizaCompradakWh;
    public XRTableCell xrTDesopentanizaPropiakWh;
    private XRTable xrTable5;
    private XRTableRow xrTableRow4;
    private XRTableCell xrTU;
    public XRTableCell xrTUdc27Operacion;
    public XRTableCell xrTUdc27CargaM3;
    public XRTableCell xrTUdc27CapOperac;
    private XRTableCell xrTableCell21;
    private XRTable xrTable7;
    private XRTableRow xrTableRow16;
    private XRTableCell xrTableCell80;
    private XRTableCell xrTableCell81;
    private XRTableCell xrTableCell82;
    private XRTableCell xrTableCell83;
    private XRTableRow xrTableRow17;
    public XRTableCell xrTUDC27250c4;
    public XRTableCell xrTUDC27250LSR;
    public XRTableCell xrTUDC27250MSR;
    public XRTableCell xrTUDC27250DL;
    private XRTableRow xrTableRow18;
    private XRTableCell xrTableCell88;
    private XRTableCell xrTableCell89;
    private XRTableCell xrTableCell90;
    private XRTableCell xrTableCell91;
    private XRTableRow xrTableRow19;
    public XRTableCell xrTUDC27250DM;
    public XRTableCell xrTUDC27250DP;
    public XRTableCell xrTUDC27250CR;
    private XRTableCell xrTableCell95;
    public XRTableCell xrTUdc27CombLiq;
    public XRTableCell xrTUdc27CombGas;
    public XRTableCell xrTUdc27VaporTn;
    public XRTableCell xrTUdc27AguaNfr;
    public XRTableCell xrTUdc27AireCom;
    public XRTableCell xrTUdc27CompradakWh;
    public XRTableCell xrTUdc27PropiakWh;
    private XRTableRow xrTableRow14;
    private XRTableCell xrTableCell44;
    public XRTableCell xrTUdc125Operacion;
    public XRTableCell xrTUdc125CargaM3;
    public XRTableCell xrTUdc125CapOperac;
    private XRTableCell xrTableCell60;
    private XRTable xrTable8;
    private XRTableRow xrTableRow20;
    private XRTableCell xrTableCell96;
    private XRTableCell xrTableCell97;
    private XRTableCell xrTableCell98;
    private XRTableCell xrTableCell99;
    private XRTableRow xrTableRow21;
    public XRTableCell xrTUDC1250GLP;
    public XRTableCell xrTUDC1250LSR;
    public XRTableCell xrTUDC1250MSR;
    public XRTableCell xrTUDC1250DO;
    private XRTableRow xrTableRow22;
    private XRTableCell xrTableCell104;
    private XRTableCell xrTableCell105;
    private XRTableCell xrTableCell106;
    private XRTableCell xrTableCell107;
    private XRTableRow xrTableRow23;
    public XRTableCell xrTUDC1250REDSLOP;
    private XRTableCell xrTableCell109;
    private XRTableCell xrTableCell110;
    private XRTableCell xrTableCell111;
    public XRTableCell xrTUdc125CombLiq;
    public XRTableCell xrTUdc125CombGas;
    public XRTableCell xrTUdc125VaporTn;
    public XRTableCell xrTUdc125AguaNfr;
    public XRTableCell xrTUdc125AireCom;
    public XRTableCell xrTUdc125CompradakWh;
    public XRTableCell xrTUdc125PropiakWh;
    private XRTable xrTable6;
    private XRTableRow xrTableRow6;
    private XRTableCell xrTableCell31;
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
    private XRTableCell xrTableCell361;
    public XRTableCell xrTVacio1CrgaPromedio;
    public XRTableCell xrTVacio2CrgaPromedio;
    public XRTableCell xrTDesasfaltizacionCrgaPromedio;
    public XRTableCell xrTRefFurfuralCrgaPromedio;
    public XRTableCell xrTDesparafinacionCrgaPromedio;
    public XRTableCell xrTDesaseitadoCrgaPromedio;
    public XRTableCell xrTHidroterminadoCrgaPromedio;
    public XRTableCell xrTRecuperacnCrgaPromedio;
    public XRTableCell xrTRefCataliticaCrgaPromedio;
    public XRTableCell xrTNurcRefCataliticaCrgaPromedio;
    public XRTableCell xrTDestiladoCrgaPromedio;
    public XRTableCell xrTAlquilatosCrgaPromedio;
    public XRTableCell xrTDesopentanizaCrgaPromedio;
    public XRTableCell xrTUdc27CrgaPromedio;
    public XRTableCell xrTUdc125CrgaPromedio;
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
    private XRTable xrTable26;
    private XRTableRow xrTableRow63;
    private XRTableCell xrTableCell15;
    public XRTableCell xrTQuienImprime;
    private XRTableCell xrTableCell24;
    public XRTableCell xrTFechaHora;
    private XRTable xrTable29;
    private XRTableRow xrTableRow13;
    private XRTableCell xrTableCell23;
    private XRTable xrTable28;
    private XRTableRow xrTableRow12;
    private XRTableCell xrTableCell18;
    private XRTableCell xrTableCell19;
    private XRTableCell xrTableCell20;
    private XRTableCell xrTableCell22;
    private XRPictureBox xrPictureBox2;
    private XRPictureBox xrPictureBox1;
    private XRPictureBox xrPictureBox3;
    private XRPageInfo xrPageInfo1;
    private XRTable xrTable4;
    private XRTableRow xrTableRow10;
    private XRTableCell xrTableCell14;
    private XRTable xrTable27;
    private XRTableRow xrTableRow11;
    private XRTableCell xrTableCell16;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public repRendimientoVolumetricoGualvertoV()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(repRendimientoVolumetricoGualvertoV));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable24 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow59 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell340 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow41 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell220 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell224 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow42 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell225 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell226 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell227 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell228 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow43 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTVacio1CABZ = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1LAT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1FONDO = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell232 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio1PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow44 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell240 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell244 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow45 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell245 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell246 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell247 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell248 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow46 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTVacio2S10DTL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2S20DTL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2S30DTL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell252 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTVacio2PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow47 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell260 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell264 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow48 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell265 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell266 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell267 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell268 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow49 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDesasfaltizacionSTOCK = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionASFALTO = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell271 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell272 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesasfaltizacionPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow50 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell280 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell284 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow51 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell285 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell286 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell287 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell288 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow52 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTRefFurfuS10REF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuS20REF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuS30REF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuBSREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefFurfuralPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow53 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell300 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell304 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow54 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell305 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell306 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell307 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell308 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow55 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDesparaS10DES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparaS20DES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparaS30DES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparaBSDES = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesparafinacionPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow56 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell320 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell324 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow57 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell325 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell326 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell327 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell328 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow58 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDestadPaPARAFINA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestadPaACEITE = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell331 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell332 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesaseitadoPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow60 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell341 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell345 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable25 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow61 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell353 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell354 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell355 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell356 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow62 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTHidroTerminS10BA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroTerminS20BA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroTerminS30BA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroTerminBSBA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTHidroterminadoPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTRecupGasc4 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTReforCatGASREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatC4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTReforCatPLAT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell167 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRefCataliticaPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTNurcGASREF = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcGLP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcPLAT = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell175 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTNurcRefCataliticaPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell196 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell197 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell198 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell199 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTRedistFDOCBZA = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTRedistREDEST = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell202 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell203 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDestiladoPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell204 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell205 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell206 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell207 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTAlquilatosc5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosc6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell210 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell211 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTAlquilatosPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaOperacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell212 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell213 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell214 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell215 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow40 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTDeisopentaPESADOS = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeisopentaC4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDeisopentaC5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell219 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaVaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaAguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaAireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaCompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTDesopentanizaPropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTU = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTUDC27250c4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC27250LSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC27250MSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC27250DL = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow18 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow19 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTUDC27250DM = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC27250DP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC27250CR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc27PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125Operacion = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CargaM3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CrgaPromedio = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CapOperac = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTUDC1250GLP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC1250LSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC1250MSR = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUDC1250DO = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTUDC1250REDSLOP = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CombLiq = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CombGas = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125VaporTn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125AguaNfr = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125AireCom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125CompradakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTUdc125PropiakWh = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrTable26 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow63 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTQuienImprime = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTFechaHora = new DevExpress.XtraReports.UI.XRTableCell();
            this.pageHeaderBand1 = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable29 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable28 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox3 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable27 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
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
            this.xrTableCell361 = new DevExpress.XtraReports.UI.XRTableCell();
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable24,
            this.xrTable17,
            this.xrTable11,
            this.xrTable5});
            this.Detail.HeightF = 530.1453F;
            this.Detail.MultiColumn.ColumnCount = 31;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable24
            // 
            this.xrTable24.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTable24.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable24.LocationFloat = new DevExpress.Utils.PointFloat(1.00004F, 298.6909F);
            this.xrTable24.Name = "xrTable24";
            this.xrTable24.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow59});
            this.xrTable24.SizeF = new System.Drawing.SizeF(1025F, 17.18759F);
            this.xrTable24.StylePriority.UseBorders = false;
            this.xrTable24.StylePriority.UseFont = false;
            this.xrTable24.StylePriority.UseTextAlignment = false;
            this.xrTable24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow59
            // 
            this.xrTableRow59.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow59.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell340});
            this.xrTableRow59.Name = "xrTableRow59";
            this.xrTableRow59.StylePriority.UseBorders = false;
            this.xrTableRow59.Weight = 1D;
            // 
            // xrTableCell340
            // 
            this.xrTableCell340.Name = "xrTableCell340";
            this.xrTableCell340.Text = "LUBRICANTES";
            this.xrTableCell340.Weight = 1D;
            // 
            // xrTable17
            // 
            this.xrTable17.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable17.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(0F, 315.8785F);
            this.xrTable17.Name = "xrTable17";
            this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow41,
            this.xrTableRow44,
            this.xrTableRow47,
            this.xrTableRow50,
            this.xrTableRow53,
            this.xrTableRow56,
            this.xrTableRow60});
            this.xrTable17.SizeF = new System.Drawing.SizeF(1026F, 214.2668F);
            this.xrTable17.StylePriority.UseBorders = false;
            this.xrTable17.StylePriority.UseFont = false;
            this.xrTable17.StylePriority.UseTextAlignment = false;
            this.xrTable17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow41
            // 
            this.xrTableRow41.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell220,
            this.xrTVacio1Operacion,
            this.xrTVacio1CargaM3,
            this.xrTVacio1CrgaPromedio,
            this.xrTVacio1CapOperac,
            this.xrTableCell224,
            this.xrTVacio1CombLiq,
            this.xrTVacio1CombGas,
            this.xrTVacio1VaporTn,
            this.xrTVacio1AguaNfr,
            this.xrTVacio1AireCom,
            this.xrTVacio1CompradakWh,
            this.xrTVacio1PropiakWh});
            this.xrTableRow41.Name = "xrTableRow41";
            this.xrTableRow41.Weight = 1D;
            // 
            // xrTableCell220
            // 
            this.xrTableCell220.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell220.Name = "xrTableCell220";
            this.xrTableCell220.StylePriority.UseBorders = false;
            this.xrTableCell220.Text = "U. VACIO 1";
            this.xrTableCell220.Weight = 0.78366421528433738D;
            // 
            // xrTVacio1Operacion
            // 
            this.xrTVacio1Operacion.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1Operacion.Name = "xrTVacio1Operacion";
            this.xrTVacio1Operacion.StylePriority.UseBorders = false;
            this.xrTVacio1Operacion.Text = "0.00";
            this.xrTVacio1Operacion.Weight = 0.54398581973372906D;
            // 
            // xrTVacio1CargaM3
            // 
            this.xrTVacio1CargaM3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CargaM3.Name = "xrTVacio1CargaM3";
            this.xrTVacio1CargaM3.StylePriority.UseBorders = false;
            this.xrTVacio1CargaM3.Text = "0.00";
            this.xrTVacio1CargaM3.Weight = 0.550702662096649D;
            // 
            // xrTVacio1CrgaPromedio
            // 
            this.xrTVacio1CrgaPromedio.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CrgaPromedio.Name = "xrTVacio1CrgaPromedio";
            this.xrTVacio1CrgaPromedio.StylePriority.UseBorders = false;
            this.xrTVacio1CrgaPromedio.Text = "0.00";
            this.xrTVacio1CrgaPromedio.Weight = 0.57440940612681146D;
            // 
            // xrTVacio1CapOperac
            // 
            this.xrTVacio1CapOperac.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CapOperac.Name = "xrTVacio1CapOperac";
            this.xrTVacio1CapOperac.StylePriority.UseBorders = false;
            this.xrTVacio1CapOperac.Text = "0.00";
            this.xrTVacio1CapOperac.Weight = 0.68886646055665712D;
            // 
            // xrTableCell224
            // 
            this.xrTableCell224.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable18});
            this.xrTableCell224.Name = "xrTableCell224";
            this.xrTableCell224.Text = "xrTableCell72";
            this.xrTableCell224.Weight = 2.7343781008949595D;
            // 
            // xrTable18
            // 
            this.xrTable18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable18.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.2235107F);
            this.xrTable18.Name = "xrTable18";
            this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow42,
            this.xrTableRow43});
            this.xrTable18.SizeF = new System.Drawing.SizeF(269.2706F, 30.38605F);
            this.xrTable18.StylePriority.UseBorders = false;
            this.xrTable18.StylePriority.UseFont = false;
            this.xrTable18.StylePriority.UseTextAlignment = false;
            this.xrTable18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow42
            // 
            this.xrTableRow42.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell225,
            this.xrTableCell226,
            this.xrTableCell227,
            this.xrTableCell228});
            this.xrTableRow42.Name = "xrTableRow42";
            this.xrTableRow42.Weight = 1D;
            // 
            // xrTableCell225
            // 
            this.xrTableCell225.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell225.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell225.Name = "xrTableCell225";
            this.xrTableCell225.StylePriority.UseBackColor = false;
            this.xrTableCell225.StylePriority.UseBorders = false;
            this.xrTableCell225.Text = "CABZ.";
            this.xrTableCell225.Weight = 0.8032400537355473D;
            // 
            // xrTableCell226
            // 
            this.xrTableCell226.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell226.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell226.Name = "xrTableCell226";
            this.xrTableCell226.StylePriority.UseBackColor = false;
            this.xrTableCell226.StylePriority.UseBorders = false;
            this.xrTableCell226.Text = "LAT";
            this.xrTableCell226.Weight = 0.80324115924401418D;
            // 
            // xrTableCell227
            // 
            this.xrTableCell227.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell227.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell227.Name = "xrTableCell227";
            this.xrTableCell227.StylePriority.UseBackColor = false;
            this.xrTableCell227.StylePriority.UseBorders = false;
            this.xrTableCell227.Text = "FONDO";
            this.xrTableCell227.Weight = 0.62962987331026732D;
            // 
            // xrTableCell228
            // 
            this.xrTableCell228.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell228.Name = "xrTableCell228";
            this.xrTableCell228.StylePriority.UseBorders = false;
            this.xrTableCell228.Weight = 0.7638890412620869D;
            // 
            // xrTableRow43
            // 
            this.xrTableRow43.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTVacio1CABZ,
            this.xrTVacio1LAT,
            this.xrTVacio1FONDO,
            this.xrTableCell232});
            this.xrTableRow43.Name = "xrTableRow43";
            this.xrTableRow43.Weight = 1D;
            // 
            // xrTVacio1CABZ
            // 
            this.xrTVacio1CABZ.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTVacio1CABZ.Name = "xrTVacio1CABZ";
            this.xrTVacio1CABZ.StylePriority.UseBorders = false;
            this.xrTVacio1CABZ.Text = "0.00";
            this.xrTVacio1CABZ.Weight = 0.8032400537355473D;
            // 
            // xrTVacio1LAT
            // 
            this.xrTVacio1LAT.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1LAT.Name = "xrTVacio1LAT";
            this.xrTVacio1LAT.StylePriority.UseBorders = false;
            this.xrTVacio1LAT.Text = "0.00";
            this.xrTVacio1LAT.Weight = 0.80324115924401418D;
            // 
            // xrTVacio1FONDO
            // 
            this.xrTVacio1FONDO.Name = "xrTVacio1FONDO";
            this.xrTVacio1FONDO.StylePriority.UseBorders = false;
            this.xrTVacio1FONDO.Text = "0.00";
            this.xrTVacio1FONDO.Weight = 0.62962987331026732D;
            // 
            // xrTableCell232
            // 
            this.xrTableCell232.Name = "xrTableCell232";
            this.xrTableCell232.Weight = 0.7638890412620869D;
            // 
            // xrTVacio1CombLiq
            // 
            this.xrTVacio1CombLiq.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CombLiq.Name = "xrTVacio1CombLiq";
            this.xrTVacio1CombLiq.StylePriority.UseBorders = false;
            this.xrTVacio1CombLiq.Text = "0.00";
            this.xrTVacio1CombLiq.Weight = 0.62624056476309342D;
            // 
            // xrTVacio1CombGas
            // 
            this.xrTVacio1CombGas.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CombGas.Name = "xrTVacio1CombGas";
            this.xrTVacio1CombGas.StylePriority.UseBorders = false;
            this.xrTVacio1CombGas.Text = "0.00";
            this.xrTVacio1CombGas.Weight = 0.66149853943535175D;
            // 
            // xrTVacio1VaporTn
            // 
            this.xrTVacio1VaporTn.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1VaporTn.Name = "xrTVacio1VaporTn";
            this.xrTVacio1VaporTn.StylePriority.UseBorders = false;
            this.xrTVacio1VaporTn.Text = "0.00";
            this.xrTVacio1VaporTn.Weight = 0.60351194169016442D;
            // 
            // xrTVacio1AguaNfr
            // 
            this.xrTVacio1AguaNfr.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1AguaNfr.Name = "xrTVacio1AguaNfr";
            this.xrTVacio1AguaNfr.StylePriority.UseBorders = false;
            this.xrTVacio1AguaNfr.Text = "0.00";
            this.xrTVacio1AguaNfr.Weight = 0.58726167299146048D;
            // 
            // xrTVacio1AireCom
            // 
            this.xrTVacio1AireCom.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1AireCom.Name = "xrTVacio1AireCom";
            this.xrTVacio1AireCom.StylePriority.UseBorders = false;
            this.xrTVacio1AireCom.Text = "0.00";
            this.xrTVacio1AireCom.Weight = 0.73201659312975131D;
            // 
            // xrTVacio1CompradakWh
            // 
            this.xrTVacio1CompradakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1CompradakWh.Name = "xrTVacio1CompradakWh";
            this.xrTVacio1CompradakWh.StylePriority.UseBorders = false;
            this.xrTVacio1CompradakWh.Text = "0.00";
            this.xrTVacio1CompradakWh.Weight = 0.73201781619402639D;
            // 
            // xrTVacio1PropiakWh
            // 
            this.xrTVacio1PropiakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio1PropiakWh.Name = "xrTVacio1PropiakWh";
            this.xrTVacio1PropiakWh.StylePriority.UseBorders = false;
            this.xrTVacio1PropiakWh.Text = "0.00";
            this.xrTVacio1PropiakWh.Weight = 0.600230534289323D;
            // 
            // xrTableRow44
            // 
            this.xrTableRow44.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell240,
            this.xrTVacio2Operacion,
            this.xrTVacio2CargaM3,
            this.xrTVacio2CrgaPromedio,
            this.xrTVacio2CapOperac,
            this.xrTableCell244,
            this.xrTVacio2CombLiq,
            this.xrTVacio2CombGas,
            this.xrTVacio2VaporTn,
            this.xrTVacio2AguaNfr,
            this.xrTVacio2AireCom,
            this.xrTVacio2CompradakWh,
            this.xrTVacio2PropiakWh});
            this.xrTableRow44.Name = "xrTableRow44";
            this.xrTableRow44.Weight = 1D;
            // 
            // xrTableCell240
            // 
            this.xrTableCell240.Name = "xrTableCell240";
            this.xrTableCell240.Text = "U. VACIO 2";
            this.xrTableCell240.Weight = 0.78366437102082764D;
            // 
            // xrTVacio2Operacion
            // 
            this.xrTVacio2Operacion.Name = "xrTVacio2Operacion";
            this.xrTVacio2Operacion.Text = "0.00";
            this.xrTVacio2Operacion.Weight = 0.54398498291123887D;
            // 
            // xrTVacio2CargaM3
            // 
            this.xrTVacio2CargaM3.Name = "xrTVacio2CargaM3";
            this.xrTVacio2CargaM3.Text = "0.00";
            this.xrTVacio2CargaM3.Weight = 0.55070341264927847D;
            // 
            // xrTVacio2CrgaPromedio
            // 
            this.xrTVacio2CrgaPromedio.Name = "xrTVacio2CrgaPromedio";
            this.xrTVacio2CrgaPromedio.Text = "0.00";
            this.xrTVacio2CrgaPromedio.Weight = 0.57441000329248237D;
            // 
            // xrTVacio2CapOperac
            // 
            this.xrTVacio2CapOperac.Name = "xrTVacio2CapOperac";
            this.xrTVacio2CapOperac.Text = "0.00";
            this.xrTVacio2CapOperac.Weight = 0.68886662755064842D;
            // 
            // xrTableCell244
            // 
            this.xrTableCell244.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
            this.xrTableCell244.Name = "xrTableCell244";
            this.xrTableCell244.Text = "xrTableCell72";
            this.xrTableCell244.Weight = 2.7343785684893516D;
            // 
            // xrTable19
            // 
            this.xrTable19.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable19.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable19.Name = "xrTable19";
            this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow45,
            this.xrTableRow46});
            this.xrTable19.SizeF = new System.Drawing.SizeF(269.2704F, 30.6095F);
            this.xrTable19.StylePriority.UseBorders = false;
            this.xrTable19.StylePriority.UseFont = false;
            this.xrTable19.StylePriority.UseTextAlignment = false;
            this.xrTable19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow45
            // 
            this.xrTableRow45.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell245,
            this.xrTableCell246,
            this.xrTableCell247,
            this.xrTableCell248});
            this.xrTableRow45.Name = "xrTableRow45";
            this.xrTableRow45.Weight = 1D;
            // 
            // xrTableCell245
            // 
            this.xrTableCell245.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell245.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell245.Name = "xrTableCell245";
            this.xrTableCell245.StylePriority.UseBackColor = false;
            this.xrTableCell245.StylePriority.UseBorders = false;
            this.xrTableCell245.Text = "S-10-DTL";
            this.xrTableCell245.Weight = 0.8032400537355473D;
            // 
            // xrTableCell246
            // 
            this.xrTableCell246.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell246.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell246.Name = "xrTableCell246";
            this.xrTableCell246.StylePriority.UseBackColor = false;
            this.xrTableCell246.StylePriority.UseBorders = false;
            this.xrTableCell246.Text = "S-20-DTL";
            this.xrTableCell246.Weight = 0.80324115924401418D;
            // 
            // xrTableCell247
            // 
            this.xrTableCell247.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell247.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell247.Name = "xrTableCell247";
            this.xrTableCell247.StylePriority.UseBackColor = false;
            this.xrTableCell247.StylePriority.UseBorders = false;
            this.xrTableCell247.Text = "S-30-DTL";
            this.xrTableCell247.Weight = 0.62962987331026732D;
            // 
            // xrTableCell248
            // 
            this.xrTableCell248.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell248.Name = "xrTableCell248";
            this.xrTableCell248.StylePriority.UseBorders = false;
            this.xrTableCell248.Weight = 0.7638890412620869D;
            // 
            // xrTableRow46
            // 
            this.xrTableRow46.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTVacio2S10DTL,
            this.xrTVacio2S20DTL,
            this.xrTVacio2S30DTL,
            this.xrTableCell252});
            this.xrTableRow46.Name = "xrTableRow46";
            this.xrTableRow46.Weight = 1D;
            // 
            // xrTVacio2S10DTL
            // 
            this.xrTVacio2S10DTL.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTVacio2S10DTL.Name = "xrTVacio2S10DTL";
            this.xrTVacio2S10DTL.StylePriority.UseBorders = false;
            this.xrTVacio2S10DTL.Text = "0.00";
            this.xrTVacio2S10DTL.Weight = 0.8032400537355473D;
            // 
            // xrTVacio2S20DTL
            // 
            this.xrTVacio2S20DTL.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTVacio2S20DTL.Name = "xrTVacio2S20DTL";
            this.xrTVacio2S20DTL.StylePriority.UseBorders = false;
            this.xrTVacio2S20DTL.Text = "0.00";
            this.xrTVacio2S20DTL.Weight = 0.80324115924401418D;
            // 
            // xrTVacio2S30DTL
            // 
            this.xrTVacio2S30DTL.Name = "xrTVacio2S30DTL";
            this.xrTVacio2S30DTL.StylePriority.UseBorders = false;
            this.xrTVacio2S30DTL.Text = "0.00";
            this.xrTVacio2S30DTL.Weight = 0.62962987331026732D;
            // 
            // xrTableCell252
            // 
            this.xrTableCell252.Name = "xrTableCell252";
            this.xrTableCell252.Weight = 0.7638890412620869D;
            // 
            // xrTVacio2CombLiq
            // 
            this.xrTVacio2CombLiq.Name = "xrTVacio2CombLiq";
            this.xrTVacio2CombLiq.Text = "0.00";
            this.xrTVacio2CombLiq.Weight = 0.62623990685738384D;
            // 
            // xrTVacio2CombGas
            // 
            this.xrTVacio2CombGas.Name = "xrTVacio2CombGas";
            this.xrTVacio2CombGas.Text = "0.00";
            this.xrTVacio2CombGas.Weight = 0.66149857883712715D;
            // 
            // xrTVacio2VaporTn
            // 
            this.xrTVacio2VaporTn.Name = "xrTVacio2VaporTn";
            this.xrTVacio2VaporTn.Text = "0.00";
            this.xrTVacio2VaporTn.Weight = 0.60351194229616212D;
            // 
            // xrTVacio2AguaNfr
            // 
            this.xrTVacio2AguaNfr.Name = "xrTVacio2AguaNfr";
            this.xrTVacio2AguaNfr.Text = "0.00";
            this.xrTVacio2AguaNfr.Weight = 0.58726171231876589D;
            // 
            // xrTVacio2AireCom
            // 
            this.xrTVacio2AireCom.Name = "xrTVacio2AireCom";
            this.xrTVacio2AireCom.Text = "0.00";
            this.xrTVacio2AireCom.Weight = 0.73201655512761932D;
            // 
            // xrTVacio2CompradakWh
            // 
            this.xrTVacio2CompradakWh.Name = "xrTVacio2CompradakWh";
            this.xrTVacio2CompradakWh.Text = "0.00";
            this.xrTVacio2CompradakWh.Weight = 0.73201789440391052D;
            // 
            // xrTVacio2PropiakWh
            // 
            this.xrTVacio2PropiakWh.Name = "xrTVacio2PropiakWh";
            this.xrTVacio2PropiakWh.Text = "0.00";
            this.xrTVacio2PropiakWh.Weight = 0.60023045807683917D;
            // 
            // xrTableRow47
            // 
            this.xrTableRow47.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell260,
            this.xrTDesasfaltizacionOperacion,
            this.xrTDesasfaltizacionCargaM3,
            this.xrTDesasfaltizacionCrgaPromedio,
            this.xrTDesasfaltizacionCapOperac,
            this.xrTableCell264,
            this.xrTDesasfaltizacionCombLiq,
            this.xrTDesasfaltizacionCombGas,
            this.xrTDesasfaltizacionVaporTn,
            this.xrTDesasfaltizacionAguaNfr,
            this.xrTDesasfaltizacionAireCom,
            this.xrTDesasfaltizacionCompradakWh,
            this.xrTDesasfaltizacionPropiakWh});
            this.xrTableRow47.Name = "xrTableRow47";
            this.xrTableRow47.Weight = 1D;
            // 
            // xrTableCell260
            // 
            this.xrTableCell260.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell260.Name = "xrTableCell260";
            this.xrTableCell260.StylePriority.UseFont = false;
            this.xrTableCell260.Text = "U. DESASFALTIZACION";
            this.xrTableCell260.Weight = 0.783664446133997D;
            // 
            // xrTDesasfaltizacionOperacion
            // 
            this.xrTDesasfaltizacionOperacion.Name = "xrTDesasfaltizacionOperacion";
            this.xrTDesasfaltizacionOperacion.Text = "0.00";
            this.xrTDesasfaltizacionOperacion.Weight = 0.54398498409199414D;
            // 
            // xrTDesasfaltizacionCargaM3
            // 
            this.xrTDesasfaltizacionCargaM3.Name = "xrTDesasfaltizacionCargaM3";
            this.xrTDesasfaltizacionCargaM3.Text = "0.00";
            this.xrTDesasfaltizacionCargaM3.Weight = 0.55070341264927825D;
            // 
            // xrTDesasfaltizacionCrgaPromedio
            // 
            this.xrTDesasfaltizacionCrgaPromedio.Name = "xrTDesasfaltizacionCrgaPromedio";
            this.xrTDesasfaltizacionCrgaPromedio.Text = "0.00";
            this.xrTDesasfaltizacionCrgaPromedio.Weight = 0.57441015824182307D;
            // 
            // xrTDesasfaltizacionCapOperac
            // 
            this.xrTDesasfaltizacionCapOperac.Name = "xrTDesasfaltizacionCapOperac";
            this.xrTDesasfaltizacionCapOperac.Text = "0.00";
            this.xrTDesasfaltizacionCapOperac.Weight = 0.68886639630738289D;
            // 
            // xrTableCell264
            // 
            this.xrTableCell264.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20});
            this.xrTableCell264.Name = "xrTableCell264";
            this.xrTableCell264.Text = "xrTableCell116";
            this.xrTableCell264.Weight = 2.7343785684893516D;
            // 
            // xrTable20
            // 
            this.xrTable20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable20.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0.2235107F);
            this.xrTable20.Name = "xrTable20";
            this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow48,
            this.xrTableRow49});
            this.xrTable20.SizeF = new System.Drawing.SizeF(269.2705F, 30.38599F);
            this.xrTable20.StylePriority.UseBorders = false;
            this.xrTable20.StylePriority.UseFont = false;
            this.xrTable20.StylePriority.UseTextAlignment = false;
            this.xrTable20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow48
            // 
            this.xrTableRow48.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell265,
            this.xrTableCell266,
            this.xrTableCell267,
            this.xrTableCell268});
            this.xrTableRow48.Name = "xrTableRow48";
            this.xrTableRow48.Weight = 1D;
            // 
            // xrTableCell265
            // 
            this.xrTableCell265.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell265.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell265.Name = "xrTableCell265";
            this.xrTableCell265.StylePriority.UseBackColor = false;
            this.xrTableCell265.StylePriority.UseBorders = false;
            this.xrTableCell265.Text = "B. STOCK";
            this.xrTableCell265.Weight = 0.8032400537355473D;
            // 
            // xrTableCell266
            // 
            this.xrTableCell266.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell266.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell266.Name = "xrTableCell266";
            this.xrTableCell266.StylePriority.UseBackColor = false;
            this.xrTableCell266.StylePriority.UseBorders = false;
            this.xrTableCell266.Text = "ASFALTO";
            this.xrTableCell266.Weight = 0.80324115924401418D;
            // 
            // xrTableCell267
            // 
            this.xrTableCell267.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell267.Name = "xrTableCell267";
            this.xrTableCell267.StylePriority.UseBorders = false;
            this.xrTableCell267.Weight = 0.62962987331026732D;
            // 
            // xrTableCell268
            // 
            this.xrTableCell268.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell268.Name = "xrTableCell268";
            this.xrTableCell268.StylePriority.UseBorders = false;
            this.xrTableCell268.Weight = 0.7638890412620869D;
            // 
            // xrTableRow49
            // 
            this.xrTableRow49.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDesasfaltizacionSTOCK,
            this.xrTDesasfaltizacionASFALTO,
            this.xrTableCell271,
            this.xrTableCell272});
            this.xrTableRow49.Name = "xrTableRow49";
            this.xrTableRow49.Weight = 1D;
            // 
            // xrTDesasfaltizacionSTOCK
            // 
            this.xrTDesasfaltizacionSTOCK.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDesasfaltizacionSTOCK.Name = "xrTDesasfaltizacionSTOCK";
            this.xrTDesasfaltizacionSTOCK.StylePriority.UseBorders = false;
            this.xrTDesasfaltizacionSTOCK.Text = "0.00";
            this.xrTDesasfaltizacionSTOCK.Weight = 0.8032400537355473D;
            // 
            // xrTDesasfaltizacionASFALTO
            // 
            this.xrTDesasfaltizacionASFALTO.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDesasfaltizacionASFALTO.Name = "xrTDesasfaltizacionASFALTO";
            this.xrTDesasfaltizacionASFALTO.StylePriority.UseBorders = false;
            this.xrTDesasfaltizacionASFALTO.Text = "0.00";
            this.xrTDesasfaltizacionASFALTO.Weight = 0.80324115924401418D;
            // 
            // xrTableCell271
            // 
            this.xrTableCell271.Name = "xrTableCell271";
            this.xrTableCell271.StylePriority.UseBorders = false;
            this.xrTableCell271.Weight = 0.62962987331026732D;
            // 
            // xrTableCell272
            // 
            this.xrTableCell272.Name = "xrTableCell272";
            this.xrTableCell272.Weight = 0.7638890412620869D;
            // 
            // xrTDesasfaltizacionCombLiq
            // 
            this.xrTDesasfaltizacionCombLiq.Name = "xrTDesasfaltizacionCombLiq";
            this.xrTDesasfaltizacionCombLiq.Text = "0.00";
            this.xrTDesasfaltizacionCombLiq.Weight = 0.62623990685738384D;
            // 
            // xrTDesasfaltizacionCombGas
            // 
            this.xrTDesasfaltizacionCombGas.Name = "xrTDesasfaltizacionCombGas";
            this.xrTDesasfaltizacionCombGas.Text = "0.00";
            this.xrTDesasfaltizacionCombGas.Weight = 0.66149857883712715D;
            // 
            // xrTDesasfaltizacionVaporTn
            // 
            this.xrTDesasfaltizacionVaporTn.Name = "xrTDesasfaltizacionVaporTn";
            this.xrTDesasfaltizacionVaporTn.Text = "0.00";
            this.xrTDesasfaltizacionVaporTn.Weight = 0.60351194229616212D;
            // 
            // xrTDesasfaltizacionAguaNfr
            // 
            this.xrTDesasfaltizacionAguaNfr.Name = "xrTDesasfaltizacionAguaNfr";
            this.xrTDesasfaltizacionAguaNfr.Text = "0.00";
            this.xrTDesasfaltizacionAguaNfr.Weight = 0.58726171231876589D;
            // 
            // xrTDesasfaltizacionAireCom
            // 
            this.xrTDesasfaltizacionAireCom.Name = "xrTDesasfaltizacionAireCom";
            this.xrTDesasfaltizacionAireCom.Text = "0.00";
            this.xrTDesasfaltizacionAireCom.Weight = 0.73201655512761932D;
            // 
            // xrTDesasfaltizacionCompradakWh
            // 
            this.xrTDesasfaltizacionCompradakWh.Name = "xrTDesasfaltizacionCompradakWh";
            this.xrTDesasfaltizacionCompradakWh.Text = "0.00";
            this.xrTDesasfaltizacionCompradakWh.Weight = 0.73201789440391052D;
            // 
            // xrTDesasfaltizacionPropiakWh
            // 
            this.xrTDesasfaltizacionPropiakWh.Name = "xrTDesasfaltizacionPropiakWh";
            this.xrTDesasfaltizacionPropiakWh.Text = "0.00";
            this.xrTDesasfaltizacionPropiakWh.Weight = 0.60023045807683917D;
            // 
            // xrTableRow50
            // 
            this.xrTableRow50.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell280,
            this.xrTRefFurfuralOperacion,
            this.xrTRefFurfuralCargaM3,
            this.xrTRefFurfuralCrgaPromedio,
            this.xrTRefFurfuralCapOperac,
            this.xrTableCell284,
            this.xrTRefFurfuralCombLiq,
            this.xrTRefFurfuralCombGas,
            this.xrTRefFurfuralVaporTn,
            this.xrTRefFurfuralAguaNfr,
            this.xrTRefFurfuralAireCom,
            this.xrTRefFurfuralCompradakWh,
            this.xrTRefFurfuralPropiakWh});
            this.xrTableRow50.Name = "xrTableRow50";
            this.xrTableRow50.Weight = 1D;
            // 
            // xrTableCell280
            // 
            this.xrTableCell280.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell280.Name = "xrTableCell280";
            this.xrTableCell280.StylePriority.UseFont = false;
            this.xrTableCell280.Text = "U. REFINACION FURFURAL";
            this.xrTableCell280.Weight = 0.78366436914039617D;
            // 
            // xrTRefFurfuralOperacion
            // 
            this.xrTRefFurfuralOperacion.Name = "xrTRefFurfuralOperacion";
            this.xrTRefFurfuralOperacion.Text = "0.00";
            this.xrTRefFurfuralOperacion.Weight = 0.54398574501958608D;
            // 
            // xrTRefFurfuralCargaM3
            // 
            this.xrTRefFurfuralCargaM3.Name = "xrTRefFurfuralCargaM3";
            this.xrTRefFurfuralCargaM3.Text = "0.00";
            this.xrTRefFurfuralCargaM3.Weight = 0.55070281866190329D;
            // 
            // xrTRefFurfuralCrgaPromedio
            // 
            this.xrTRefFurfuralCrgaPromedio.Name = "xrTRefFurfuralCrgaPromedio";
            this.xrTRefFurfuralCrgaPromedio.Text = "0.00";
            this.xrTRefFurfuralCrgaPromedio.Weight = 0.57440925296699952D;
            // 
            // xrTRefFurfuralCapOperac
            // 
            this.xrTRefFurfuralCapOperac.Name = "xrTRefFurfuralCapOperac";
            this.xrTRefFurfuralCapOperac.Text = "0.00";
            this.xrTRefFurfuralCapOperac.Weight = 0.688866453417849D;
            // 
            // xrTableCell284
            // 
            this.xrTableCell284.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21});
            this.xrTableCell284.Name = "xrTableCell284";
            this.xrTableCell284.Text = "xrTableCell140";
            this.xrTableCell284.Weight = 2.7343781012001118D;
            // 
            // xrTable21
            // 
            this.xrTable21.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable21.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 0F);
            this.xrTable21.Name = "xrTable21";
            this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow51,
            this.xrTableRow52});
            this.xrTable21.SizeF = new System.Drawing.SizeF(269.2707F, 30.60956F);
            this.xrTable21.StylePriority.UseBorders = false;
            this.xrTable21.StylePriority.UseFont = false;
            this.xrTable21.StylePriority.UseTextAlignment = false;
            this.xrTable21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow51
            // 
            this.xrTableRow51.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell285,
            this.xrTableCell286,
            this.xrTableCell287,
            this.xrTableCell288});
            this.xrTableRow51.Name = "xrTableRow51";
            this.xrTableRow51.Weight = 1D;
            // 
            // xrTableCell285
            // 
            this.xrTableCell285.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell285.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell285.Name = "xrTableCell285";
            this.xrTableCell285.StylePriority.UseBackColor = false;
            this.xrTableCell285.StylePriority.UseBorders = false;
            this.xrTableCell285.Text = "S-10-REF";
            this.xrTableCell285.Weight = 0.8032400537355473D;
            // 
            // xrTableCell286
            // 
            this.xrTableCell286.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell286.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell286.Name = "xrTableCell286";
            this.xrTableCell286.StylePriority.UseBackColor = false;
            this.xrTableCell286.StylePriority.UseBorders = false;
            this.xrTableCell286.Text = "S-20-REF";
            this.xrTableCell286.Weight = 0.80324115924401418D;
            // 
            // xrTableCell287
            // 
            this.xrTableCell287.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell287.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell287.Name = "xrTableCell287";
            this.xrTableCell287.StylePriority.UseBackColor = false;
            this.xrTableCell287.StylePriority.UseBorders = false;
            this.xrTableCell287.Text = "S-30-REF";
            this.xrTableCell287.Weight = 0.62962987331026732D;
            // 
            // xrTableCell288
            // 
            this.xrTableCell288.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell288.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell288.Name = "xrTableCell288";
            this.xrTableCell288.StylePriority.UseBackColor = false;
            this.xrTableCell288.StylePriority.UseBorders = false;
            this.xrTableCell288.Text = "BS-REF";
            this.xrTableCell288.Weight = 0.7638890412620869D;
            // 
            // xrTableRow52
            // 
            this.xrTableRow52.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTRefFurfuS10REF,
            this.xrTRefFurfuS20REF,
            this.xrTRefFurfuS30REF,
            this.xrTRefFurfuBSREF});
            this.xrTableRow52.Name = "xrTableRow52";
            this.xrTableRow52.Weight = 1D;
            // 
            // xrTRefFurfuS10REF
            // 
            this.xrTRefFurfuS10REF.BackColor = System.Drawing.Color.Transparent;
            this.xrTRefFurfuS10REF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTRefFurfuS10REF.Name = "xrTRefFurfuS10REF";
            this.xrTRefFurfuS10REF.StylePriority.UseBackColor = false;
            this.xrTRefFurfuS10REF.StylePriority.UseBorders = false;
            this.xrTRefFurfuS10REF.Text = "0.00";
            this.xrTRefFurfuS10REF.Weight = 0.8032400537355473D;
            // 
            // xrTRefFurfuS20REF
            // 
            this.xrTRefFurfuS20REF.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRefFurfuS20REF.Name = "xrTRefFurfuS20REF";
            this.xrTRefFurfuS20REF.StylePriority.UseBorders = false;
            this.xrTRefFurfuS20REF.Text = "0.00";
            this.xrTRefFurfuS20REF.Weight = 0.80324115924401418D;
            // 
            // xrTRefFurfuS30REF
            // 
            this.xrTRefFurfuS30REF.Name = "xrTRefFurfuS30REF";
            this.xrTRefFurfuS30REF.StylePriority.UseBorders = false;
            this.xrTRefFurfuS30REF.Text = "0.00";
            this.xrTRefFurfuS30REF.Weight = 0.62962987331026732D;
            // 
            // xrTRefFurfuBSREF
            // 
            this.xrTRefFurfuBSREF.Name = "xrTRefFurfuBSREF";
            this.xrTRefFurfuBSREF.Text = "0.00";
            this.xrTRefFurfuBSREF.Weight = 0.7638890412620869D;
            // 
            // xrTRefFurfuralCombLiq
            // 
            this.xrTRefFurfuralCombLiq.Name = "xrTRefFurfuralCombLiq";
            this.xrTRefFurfuralCombLiq.Text = "0.00";
            this.xrTRefFurfuralCombLiq.Weight = 0.62624056483298118D;
            // 
            // xrTRefFurfuralCombGas
            // 
            this.xrTRefFurfuralCombGas.Name = "xrTRefFurfuralCombGas";
            this.xrTRefFurfuralCombGas.Text = "0.00";
            this.xrTRefFurfuralCombGas.Weight = 0.66149853950917825D;
            // 
            // xrTRefFurfuralVaporTn
            // 
            this.xrTRefFurfuralVaporTn.Name = "xrTRefFurfuralVaporTn";
            this.xrTRefFurfuralVaporTn.Text = "0.00";
            this.xrTRefFurfuralVaporTn.Weight = 0.60351194175749734D;
            // 
            // xrTRefFurfuralAguaNfr
            // 
            this.xrTRefFurfuralAguaNfr.Name = "xrTRefFurfuralAguaNfr";
            this.xrTRefFurfuralAguaNfr.Text = "0.00";
            this.xrTRefFurfuralAguaNfr.Weight = 0.58726167305701271D;
            // 
            // xrTRefFurfuralAireCom
            // 
            this.xrTRefFurfuralAireCom.Name = "xrTRefFurfuralAireCom";
            this.xrTRefFurfuralAireCom.Text = "0.00";
            this.xrTRefFurfuralAireCom.Weight = 0.73201659321144064D;
            // 
            // xrTRefFurfuralCompradakWh
            // 
            this.xrTRefFurfuralCompradakWh.Name = "xrTRefFurfuralCompradakWh";
            this.xrTRefFurfuralCompradakWh.Text = "0.00";
            this.xrTRefFurfuralCompradakWh.Weight = 0.73201781627571683D;
            // 
            // xrTRefFurfuralPropiakWh
            // 
            this.xrTRefFurfuralPropiakWh.Name = "xrTRefFurfuralPropiakWh";
            this.xrTRefFurfuralPropiakWh.Text = "0.00";
            this.xrTRefFurfuralPropiakWh.Weight = 0.600230534429566D;
            // 
            // xrTableRow53
            // 
            this.xrTableRow53.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell300,
            this.xrTDesparafinacionOperacion,
            this.xrTDesparafinacionCargaM3,
            this.xrTDesparafinacionCrgaPromedio,
            this.xrTDesparafinacionCapOperac,
            this.xrTableCell304,
            this.xrTDesparafinacionCombLiq,
            this.xrTDesparafinacionCombGas,
            this.xrTDesparafinacionVaporTn,
            this.xrTDesparafinacionAguaNfr,
            this.xrTDesparafinacionAireCom,
            this.xrTDesparafinacionCompradakWh,
            this.xrTDesparafinacionPropiakWh});
            this.xrTableRow53.Name = "xrTableRow53";
            this.xrTableRow53.Weight = 1D;
            // 
            // xrTableCell300
            // 
            this.xrTableCell300.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell300.Name = "xrTableCell300";
            this.xrTableCell300.StylePriority.UseFont = false;
            this.xrTableCell300.Text = "U. DESPARAFINACION";
            this.xrTableCell300.Weight = 0.78366436905293668D;
            // 
            // xrTDesparafinacionOperacion
            // 
            this.xrTDesparafinacionOperacion.Name = "xrTDesparafinacionOperacion";
            this.xrTDesparafinacionOperacion.Text = "0.00";
            this.xrTDesparafinacionOperacion.Weight = 0.54398388404782538D;
            // 
            // xrTDesparafinacionCargaM3
            // 
            this.xrTDesparafinacionCargaM3.Name = "xrTDesparafinacionCargaM3";
            this.xrTDesparafinacionCargaM3.Text = "0.00";
            this.xrTDesparafinacionCargaM3.Weight = 0.55070444401395346D;
            // 
            // xrTDesparafinacionCrgaPromedio
            // 
            this.xrTDesparafinacionCrgaPromedio.Name = "xrTDesparafinacionCrgaPromedio";
            this.xrTDesparafinacionCrgaPromedio.Text = "0.00";
            this.xrTDesparafinacionCrgaPromedio.Weight = 0.57440940612681146D;
            // 
            // xrTDesparafinacionCapOperac
            // 
            this.xrTDesparafinacionCapOperac.Name = "xrTDesparafinacionCapOperac";
            this.xrTDesparafinacionCapOperac.Text = "0.00";
            this.xrTDesparafinacionCapOperac.Weight = 0.68886646055665712D;
            // 
            // xrTableCell304
            // 
            this.xrTableCell304.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22});
            this.xrTableCell304.Name = "xrTableCell304";
            this.xrTableCell304.Text = "xrTableCell128";
            this.xrTableCell304.Weight = 2.7343781008949595D;
            // 
            // xrTable22
            // 
            this.xrTable22.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable22.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 3.051758E-05F);
            this.xrTable22.Name = "xrTable22";
            this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow54,
            this.xrTableRow55});
            this.xrTable22.SizeF = new System.Drawing.SizeF(269.2707F, 30.60959F);
            this.xrTable22.StylePriority.UseBorders = false;
            this.xrTable22.StylePriority.UseFont = false;
            this.xrTable22.StylePriority.UseTextAlignment = false;
            this.xrTable22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow54
            // 
            this.xrTableRow54.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell305,
            this.xrTableCell306,
            this.xrTableCell307,
            this.xrTableCell308});
            this.xrTableRow54.Name = "xrTableRow54";
            this.xrTableRow54.Weight = 1D;
            // 
            // xrTableCell305
            // 
            this.xrTableCell305.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell305.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell305.Name = "xrTableCell305";
            this.xrTableCell305.StylePriority.UseBackColor = false;
            this.xrTableCell305.StylePriority.UseBorders = false;
            this.xrTableCell305.Text = "S-10-DES";
            this.xrTableCell305.Weight = 0.8032400537355473D;
            // 
            // xrTableCell306
            // 
            this.xrTableCell306.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell306.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell306.Name = "xrTableCell306";
            this.xrTableCell306.StylePriority.UseBackColor = false;
            this.xrTableCell306.StylePriority.UseBorders = false;
            this.xrTableCell306.Text = "S-20-DES";
            this.xrTableCell306.Weight = 0.80324115924401418D;
            // 
            // xrTableCell307
            // 
            this.xrTableCell307.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell307.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell307.Name = "xrTableCell307";
            this.xrTableCell307.StylePriority.UseBackColor = false;
            this.xrTableCell307.StylePriority.UseBorders = false;
            this.xrTableCell307.Text = "S-30-DES";
            this.xrTableCell307.Weight = 0.62962987331026732D;
            // 
            // xrTableCell308
            // 
            this.xrTableCell308.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell308.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell308.Name = "xrTableCell308";
            this.xrTableCell308.StylePriority.UseBackColor = false;
            this.xrTableCell308.StylePriority.UseBorders = false;
            this.xrTableCell308.Text = "BS-DES";
            this.xrTableCell308.Weight = 0.7638890412620869D;
            // 
            // xrTableRow55
            // 
            this.xrTableRow55.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDesparaS10DES,
            this.xrTDesparaS20DES,
            this.xrTDesparaS30DES,
            this.xrTDesparaBSDES});
            this.xrTableRow55.Name = "xrTableRow55";
            this.xrTableRow55.Weight = 1D;
            // 
            // xrTDesparaS10DES
            // 
            this.xrTDesparaS10DES.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDesparaS10DES.Name = "xrTDesparaS10DES";
            this.xrTDesparaS10DES.StylePriority.UseBorders = false;
            this.xrTDesparaS10DES.Text = "0.00";
            this.xrTDesparaS10DES.Weight = 0.8032400537355473D;
            // 
            // xrTDesparaS20DES
            // 
            this.xrTDesparaS20DES.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDesparaS20DES.Name = "xrTDesparaS20DES";
            this.xrTDesparaS20DES.StylePriority.UseBorders = false;
            this.xrTDesparaS20DES.Text = "0.00";
            this.xrTDesparaS20DES.Weight = 0.80324115924401418D;
            // 
            // xrTDesparaS30DES
            // 
            this.xrTDesparaS30DES.Name = "xrTDesparaS30DES";
            this.xrTDesparaS30DES.StylePriority.UseBorders = false;
            this.xrTDesparaS30DES.Text = "0.00";
            this.xrTDesparaS30DES.Weight = 0.62962987331026732D;
            // 
            // xrTDesparaBSDES
            // 
            this.xrTDesparaBSDES.Name = "xrTDesparaBSDES";
            this.xrTDesparaBSDES.Text = "0.00";
            this.xrTDesparaBSDES.Weight = 0.7638890412620869D;
            // 
            // xrTDesparafinacionCombLiq
            // 
            this.xrTDesparafinacionCombLiq.Name = "xrTDesparafinacionCombLiq";
            this.xrTDesparafinacionCombLiq.Text = "0.00";
            this.xrTDesparafinacionCombLiq.Weight = 0.62624056476309342D;
            // 
            // xrTDesparafinacionCombGas
            // 
            this.xrTDesparafinacionCombGas.Name = "xrTDesparafinacionCombGas";
            this.xrTDesparafinacionCombGas.Text = "0.00";
            this.xrTDesparafinacionCombGas.Weight = 0.66149853943535164D;
            // 
            // xrTDesparafinacionVaporTn
            // 
            this.xrTDesparafinacionVaporTn.Name = "xrTDesparafinacionVaporTn";
            this.xrTDesparafinacionVaporTn.Text = "0.00";
            this.xrTDesparafinacionVaporTn.Weight = 0.60351194169016431D;
            // 
            // xrTDesparafinacionAguaNfr
            // 
            this.xrTDesparafinacionAguaNfr.Name = "xrTDesparafinacionAguaNfr";
            this.xrTDesparafinacionAguaNfr.Text = "0.00";
            this.xrTDesparafinacionAguaNfr.Weight = 0.58726167299146048D;
            // 
            // xrTDesparafinacionAireCom
            // 
            this.xrTDesparafinacionAireCom.Name = "xrTDesparafinacionAireCom";
            this.xrTDesparafinacionAireCom.Text = "0.00";
            this.xrTDesparafinacionAireCom.Weight = 0.73201659312975131D;
            // 
            // xrTDesparafinacionCompradakWh
            // 
            this.xrTDesparafinacionCompradakWh.Name = "xrTDesparafinacionCompradakWh";
            this.xrTDesparafinacionCompradakWh.Text = "0.00";
            this.xrTDesparafinacionCompradakWh.Weight = 0.73201781619402639D;
            // 
            // xrTDesparafinacionPropiakWh
            // 
            this.xrTDesparafinacionPropiakWh.Name = "xrTDesparafinacionPropiakWh";
            this.xrTDesparafinacionPropiakWh.Text = "0.00";
            this.xrTDesparafinacionPropiakWh.Weight = 0.600230534289323D;
            // 
            // xrTableRow56
            // 
            this.xrTableRow56.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell320,
            this.xrTDesaseitadoOperacion,
            this.xrTDesaseitadoCargaM3,
            this.xrTDesaseitadoCrgaPromedio,
            this.xrTDesaseitadoCapOperac,
            this.xrTableCell324,
            this.xrTDesaseitadoCombLiq,
            this.xrTDesaseitadoCombGas,
            this.xrTDesaseitadoVaporTn,
            this.xrTDesaseitadoAguaNfr,
            this.xrTDesaseitadoAireCom,
            this.xrTDesaseitadoCompradakWh,
            this.xrTDesaseitadoPropiakWh});
            this.xrTableRow56.Name = "xrTableRow56";
            this.xrTableRow56.Weight = 1D;
            // 
            // xrTableCell320
            // 
            this.xrTableCell320.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell320.Name = "xrTableCell320";
            this.xrTableCell320.StylePriority.UseFont = false;
            this.xrTableCell320.Text = "DESACEITADO PRAFINA";
            this.xrTableCell320.Weight = 0.78366429354614786D;
            // 
            // xrTDesaseitadoOperacion
            // 
            this.xrTDesaseitadoOperacion.Name = "xrTDesaseitadoOperacion";
            this.xrTDesaseitadoOperacion.Text = "0.00";
            this.xrTDesaseitadoOperacion.Weight = 0.54398335594316727D;
            // 
            // xrTDesaseitadoCargaM3
            // 
            this.xrTDesaseitadoCargaM3.Name = "xrTDesaseitadoCargaM3";
            this.xrTDesaseitadoCargaM3.Text = "0.00";
            this.xrTDesaseitadoCargaM3.Weight = 0.55070511709202985D;
            // 
            // xrTDesaseitadoCrgaPromedio
            // 
            this.xrTDesaseitadoCrgaPromedio.Name = "xrTDesaseitadoCrgaPromedio";
            this.xrTDesaseitadoCrgaPromedio.Text = "0.00";
            this.xrTDesaseitadoCrgaPromedio.Weight = 0.57441000329248237D;
            // 
            // xrTDesaseitadoCapOperac
            // 
            this.xrTDesaseitadoCapOperac.Name = "xrTDesaseitadoCapOperac";
            this.xrTDesaseitadoCapOperac.Text = "0.00";
            this.xrTDesaseitadoCapOperac.Weight = 0.68886662755064842D;
            // 
            // xrTableCell324
            // 
            this.xrTableCell324.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23});
            this.xrTableCell324.Name = "xrTableCell324";
            this.xrTableCell324.Text = "xrTableCell152";
            this.xrTableCell324.Weight = 2.7343785684893516D;
            // 
            // xrTable23
            // 
            this.xrTable23.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable23.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0.2235718F);
            this.xrTable23.Name = "xrTable23";
            this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow57,
            this.xrTableRow58});
            this.xrTable23.SizeF = new System.Drawing.SizeF(269.2706F, 30.38602F);
            this.xrTable23.StylePriority.UseBorders = false;
            this.xrTable23.StylePriority.UseFont = false;
            this.xrTable23.StylePriority.UseTextAlignment = false;
            this.xrTable23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow57
            // 
            this.xrTableRow57.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell325,
            this.xrTableCell326,
            this.xrTableCell327,
            this.xrTableCell328});
            this.xrTableRow57.Name = "xrTableRow57";
            this.xrTableRow57.Weight = 1D;
            // 
            // xrTableCell325
            // 
            this.xrTableCell325.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell325.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell325.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell325.Name = "xrTableCell325";
            this.xrTableCell325.StylePriority.UseBackColor = false;
            this.xrTableCell325.StylePriority.UseBorders = false;
            this.xrTableCell325.StylePriority.UseFont = false;
            this.xrTableCell325.Text = "PARAFINA DURA";
            this.xrTableCell325.Weight = 0.8032400537355473D;
            // 
            // xrTableCell326
            // 
            this.xrTableCell326.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell326.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell326.Name = "xrTableCell326";
            this.xrTableCell326.StylePriority.UseBackColor = false;
            this.xrTableCell326.StylePriority.UseBorders = false;
            this.xrTableCell326.Text = "ACEITE";
            this.xrTableCell326.Weight = 0.80324115924401418D;
            // 
            // xrTableCell327
            // 
            this.xrTableCell327.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell327.Name = "xrTableCell327";
            this.xrTableCell327.StylePriority.UseBorders = false;
            this.xrTableCell327.Weight = 0.62962987331026732D;
            // 
            // xrTableCell328
            // 
            this.xrTableCell328.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell328.Name = "xrTableCell328";
            this.xrTableCell328.StylePriority.UseBorders = false;
            this.xrTableCell328.Weight = 0.7638890412620869D;
            // 
            // xrTableRow58
            // 
            this.xrTableRow58.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDestadPaPARAFINA,
            this.xrTDestadPaACEITE,
            this.xrTableCell331,
            this.xrTableCell332});
            this.xrTableRow58.Name = "xrTableRow58";
            this.xrTableRow58.Weight = 1D;
            // 
            // xrTDestadPaPARAFINA
            // 
            this.xrTDestadPaPARAFINA.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDestadPaPARAFINA.Name = "xrTDestadPaPARAFINA";
            this.xrTDestadPaPARAFINA.StylePriority.UseBorders = false;
            this.xrTDestadPaPARAFINA.Text = "0.00";
            this.xrTDestadPaPARAFINA.Weight = 0.8032400537355473D;
            // 
            // xrTDestadPaACEITE
            // 
            this.xrTDestadPaACEITE.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDestadPaACEITE.Name = "xrTDestadPaACEITE";
            this.xrTDestadPaACEITE.StylePriority.UseBorders = false;
            this.xrTDestadPaACEITE.Text = "0.00";
            this.xrTDestadPaACEITE.Weight = 0.80324115924401418D;
            // 
            // xrTableCell331
            // 
            this.xrTableCell331.Name = "xrTableCell331";
            this.xrTableCell331.StylePriority.UseBorders = false;
            this.xrTableCell331.Weight = 0.62962987331026732D;
            // 
            // xrTableCell332
            // 
            this.xrTableCell332.Name = "xrTableCell332";
            this.xrTableCell332.Weight = 0.7638890412620869D;
            // 
            // xrTDesaseitadoCombLiq
            // 
            this.xrTDesaseitadoCombLiq.Name = "xrTDesaseitadoCombLiq";
            this.xrTDesaseitadoCombLiq.Text = "0.00";
            this.xrTDesaseitadoCombLiq.Weight = 0.62623971317070737D;
            // 
            // xrTDesaseitadoCombGas
            // 
            this.xrTDesaseitadoCombGas.Name = "xrTDesaseitadoCombGas";
            this.xrTDesaseitadoCombGas.Text = "0.00";
            this.xrTDesaseitadoCombGas.Weight = 0.66149854009979192D;
            // 
            // xrTDesaseitadoVaporTn
            // 
            this.xrTDesaseitadoVaporTn.Name = "xrTDesaseitadoVaporTn";
            this.xrTDesaseitadoVaporTn.Text = "0.00";
            this.xrTDesaseitadoVaporTn.Weight = 0.60351217472017371D;
            // 
            // xrTDesaseitadoAguaNfr
            // 
            this.xrTDesaseitadoAguaNfr.Name = "xrTDesaseitadoAguaNfr";
            this.xrTDesaseitadoAguaNfr.Text = "0.00";
            this.xrTDesaseitadoAguaNfr.Weight = 0.58726171231876589D;
            // 
            // xrTDesaseitadoAireCom
            // 
            this.xrTDesaseitadoAireCom.Name = "xrTDesaseitadoAireCom";
            this.xrTDesaseitadoAireCom.Text = "0.00";
            this.xrTDesaseitadoAireCom.Weight = 0.73201655512761932D;
            // 
            // xrTDesaseitadoCompradakWh
            // 
            this.xrTDesaseitadoCompradakWh.Name = "xrTDesaseitadoCompradakWh";
            this.xrTDesaseitadoCompradakWh.Text = "0.00";
            this.xrTDesaseitadoCompradakWh.Weight = 0.73201789440391052D;
            // 
            // xrTDesaseitadoPropiakWh
            // 
            this.xrTDesaseitadoPropiakWh.Name = "xrTDesaseitadoPropiakWh";
            this.xrTDesaseitadoPropiakWh.Text = "0.00";
            this.xrTDesaseitadoPropiakWh.Weight = 0.60023045807683917D;
            // 
            // xrTableRow60
            // 
            this.xrTableRow60.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell341,
            this.xrTHidroterminadoOperacion,
            this.xrTHidroterminadoCargaM3,
            this.xrTHidroterminadoCrgaPromedio,
            this.xrTHidroterminadoCapOperac,
            this.xrTableCell345,
            this.xrTHidroterminadoCombLiq,
            this.xrTHidroterminadoCombGas,
            this.xrTHidroterminadoVaporTn,
            this.xrTHidroterminadoAguaNfr,
            this.xrTHidroterminadoAireCom,
            this.xrTHidroterminadoCompradakWh,
            this.xrTHidroterminadoPropiakWh});
            this.xrTableRow60.Name = "xrTableRow60";
            this.xrTableRow60.Weight = 1D;
            // 
            // xrTableCell341
            // 
            this.xrTableCell341.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell341.Name = "xrTableCell341";
            this.xrTableCell341.StylePriority.UseFont = false;
            this.xrTableCell341.Text = "U. HIDROTERMINADO";
            this.xrTableCell341.Weight = 0.78366429236539281D;
            // 
            // xrTHidroterminadoOperacion
            // 
            this.xrTHidroterminadoOperacion.Name = "xrTHidroterminadoOperacion";
            this.xrTHidroterminadoOperacion.Text = "0.00";
            this.xrTHidroterminadoOperacion.Weight = 0.54398335712392243D;
            // 
            // xrTHidroterminadoCargaM3
            // 
            this.xrTHidroterminadoCargaM3.Name = "xrTHidroterminadoCargaM3";
            this.xrTHidroterminadoCargaM3.Text = "0.00";
            this.xrTHidroterminadoCargaM3.Weight = 0.55070511709202985D;
            // 
            // xrTHidroterminadoCrgaPromedio
            // 
            this.xrTHidroterminadoCrgaPromedio.Name = "xrTHidroterminadoCrgaPromedio";
            this.xrTHidroterminadoCrgaPromedio.Text = "0.00";
            this.xrTHidroterminadoCrgaPromedio.Weight = 0.57441000329248237D;
            // 
            // xrTHidroterminadoCapOperac
            // 
            this.xrTHidroterminadoCapOperac.Name = "xrTHidroterminadoCapOperac";
            this.xrTHidroterminadoCapOperac.Text = "0.00";
            this.xrTHidroterminadoCapOperac.Weight = 0.68886662755064842D;
            // 
            // xrTableCell345
            // 
            this.xrTableCell345.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable25});
            this.xrTableCell345.Name = "xrTableCell345";
            this.xrTableCell345.Text = "xrTableCell345";
            this.xrTableCell345.Weight = 2.7343785684893516D;
            // 
            // xrTable25
            // 
            this.xrTable25.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable25.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable25.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable25.Name = "xrTable25";
            this.xrTable25.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow61,
            this.xrTableRow62});
            this.xrTable25.SizeF = new System.Drawing.SizeF(269.2706F, 30.60956F);
            this.xrTable25.StylePriority.UseBorders = false;
            this.xrTable25.StylePriority.UseFont = false;
            this.xrTable25.StylePriority.UseTextAlignment = false;
            this.xrTable25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow61
            // 
            this.xrTableRow61.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell353,
            this.xrTableCell354,
            this.xrTableCell355,
            this.xrTableCell356});
            this.xrTableRow61.Name = "xrTableRow61";
            this.xrTableRow61.Weight = 1D;
            // 
            // xrTableCell353
            // 
            this.xrTableCell353.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell353.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell353.Name = "xrTableCell353";
            this.xrTableCell353.StylePriority.UseBackColor = false;
            this.xrTableCell353.StylePriority.UseBorders = false;
            this.xrTableCell353.Text = "S-10-BA";
            this.xrTableCell353.Weight = 0.8032400537355473D;
            // 
            // xrTableCell354
            // 
            this.xrTableCell354.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell354.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell354.Name = "xrTableCell354";
            this.xrTableCell354.StylePriority.UseBackColor = false;
            this.xrTableCell354.StylePriority.UseBorders = false;
            this.xrTableCell354.Text = "S-20-BA";
            this.xrTableCell354.Weight = 0.80324115924401418D;
            // 
            // xrTableCell355
            // 
            this.xrTableCell355.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell355.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell355.Name = "xrTableCell355";
            this.xrTableCell355.StylePriority.UseBackColor = false;
            this.xrTableCell355.StylePriority.UseBorders = false;
            this.xrTableCell355.Text = "S-30-BA";
            this.xrTableCell355.Weight = 0.62962987331026732D;
            // 
            // xrTableCell356
            // 
            this.xrTableCell356.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell356.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell356.Name = "xrTableCell356";
            this.xrTableCell356.StylePriority.UseBackColor = false;
            this.xrTableCell356.StylePriority.UseBorders = false;
            this.xrTableCell356.Text = "BS-BA";
            this.xrTableCell356.Weight = 0.7638890412620869D;
            // 
            // xrTableRow62
            // 
            this.xrTableRow62.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTHidroTerminS10BA,
            this.xrTHidroTerminS20BA,
            this.xrTHidroTerminS30BA,
            this.xrTHidroTerminBSBA});
            this.xrTableRow62.Name = "xrTableRow62";
            this.xrTableRow62.Weight = 1D;
            // 
            // xrTHidroTerminS10BA
            // 
            this.xrTHidroTerminS10BA.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTHidroTerminS10BA.Name = "xrTHidroTerminS10BA";
            this.xrTHidroTerminS10BA.StylePriority.UseBorders = false;
            this.xrTHidroTerminS10BA.Text = "0.00";
            this.xrTHidroTerminS10BA.Weight = 0.8032400537355473D;
            // 
            // xrTHidroTerminS20BA
            // 
            this.xrTHidroTerminS20BA.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTHidroTerminS20BA.Name = "xrTHidroTerminS20BA";
            this.xrTHidroTerminS20BA.StylePriority.UseBorders = false;
            this.xrTHidroTerminS20BA.Text = "0.00";
            this.xrTHidroTerminS20BA.Weight = 0.80324115924401418D;
            // 
            // xrTHidroTerminS30BA
            // 
            this.xrTHidroTerminS30BA.Name = "xrTHidroTerminS30BA";
            this.xrTHidroTerminS30BA.StylePriority.UseBorders = false;
            this.xrTHidroTerminS30BA.Text = "0.00";
            this.xrTHidroTerminS30BA.Weight = 0.62962987331026732D;
            // 
            // xrTHidroTerminBSBA
            // 
            this.xrTHidroTerminBSBA.Name = "xrTHidroTerminBSBA";
            this.xrTHidroTerminBSBA.Text = "0.00";
            this.xrTHidroTerminBSBA.Weight = 0.7638890412620869D;
            // 
            // xrTHidroterminadoCombLiq
            // 
            this.xrTHidroterminadoCombLiq.Name = "xrTHidroterminadoCombLiq";
            this.xrTHidroterminadoCombLiq.Text = "0.00";
            this.xrTHidroterminadoCombLiq.Weight = 0.62623971317070737D;
            // 
            // xrTHidroterminadoCombGas
            // 
            this.xrTHidroterminadoCombGas.Name = "xrTHidroterminadoCombGas";
            this.xrTHidroterminadoCombGas.Text = "0.00";
            this.xrTHidroterminadoCombGas.Weight = 0.66149854009979181D;
            // 
            // xrTHidroterminadoVaporTn
            // 
            this.xrTHidroterminadoVaporTn.Name = "xrTHidroterminadoVaporTn";
            this.xrTHidroterminadoVaporTn.Text = "0.00";
            this.xrTHidroterminadoVaporTn.Weight = 0.6035121747201736D;
            // 
            // xrTHidroterminadoAguaNfr
            // 
            this.xrTHidroterminadoAguaNfr.Name = "xrTHidroterminadoAguaNfr";
            this.xrTHidroterminadoAguaNfr.Text = "0.00";
            this.xrTHidroterminadoAguaNfr.Weight = 0.58726171231876589D;
            // 
            // xrTHidroterminadoAireCom
            // 
            this.xrTHidroterminadoAireCom.Name = "xrTHidroterminadoAireCom";
            this.xrTHidroterminadoAireCom.Text = "0.00";
            this.xrTHidroterminadoAireCom.Weight = 0.73201655512761932D;
            // 
            // xrTHidroterminadoCompradakWh
            // 
            this.xrTHidroterminadoCompradakWh.Name = "xrTHidroterminadoCompradakWh";
            this.xrTHidroterminadoCompradakWh.Text = "0.00";
            this.xrTHidroterminadoCompradakWh.Weight = 0.73201789440391052D;
            // 
            // xrTHidroterminadoPropiakWh
            // 
            this.xrTHidroterminadoPropiakWh.Name = "xrTHidroterminadoPropiakWh";
            this.xrTHidroterminadoPropiakWh.Text = "0.00";
            this.xrTHidroterminadoPropiakWh.Weight = 0.60023045807683917D;
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
            this.xrTableRow27});
            this.xrTable11.SizeF = new System.Drawing.SizeF(1026F, 184.0149F);
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
            this.xrTableCell176.Text = "U. RECUPERACIÓN GASES";
            this.xrTableCell176.Weight = 0.78196556292781028D;
            // 
            // xrTRecuperacnOperacion
            // 
            this.xrTRecuperacnOperacion.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnOperacion.Name = "xrTRecuperacnOperacion";
            this.xrTRecuperacnOperacion.StylePriority.UseBorders = false;
            this.xrTRecuperacnOperacion.Text = "0.00";
            this.xrTRecuperacnOperacion.Weight = 0.542805320242849D;
            // 
            // xrTRecuperacnCargaM3
            // 
            this.xrTRecuperacnCargaM3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCargaM3.Name = "xrTRecuperacnCargaM3";
            this.xrTRecuperacnCargaM3.StylePriority.UseBorders = false;
            this.xrTRecuperacnCargaM3.Text = "0.00";
            this.xrTRecuperacnCargaM3.Weight = 0.54951044140359118D;
            // 
            // xrTRecuperacnCrgaPromedio
            // 
            this.xrTRecuperacnCrgaPromedio.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCrgaPromedio.Name = "xrTRecuperacnCrgaPromedio";
            this.xrTRecuperacnCrgaPromedio.StylePriority.UseBorders = false;
            this.xrTRecuperacnCrgaPromedio.Text = "0.00";
            this.xrTRecuperacnCrgaPromedio.Weight = 0.5731651435170515D;
            // 
            // xrTRecuperacnCapOperac
            // 
            this.xrTRecuperacnCapOperac.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCapOperac.Name = "xrTRecuperacnCapOperac";
            this.xrTRecuperacnCapOperac.StylePriority.UseBorders = false;
            this.xrTRecuperacnCapOperac.Text = "0.00";
            this.xrTRecuperacnCapOperac.Weight = 0.68737316244263036D;
            // 
            // xrTableCell180
            // 
            this.xrTableCell180.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable14});
            this.xrTableCell180.Name = "xrTableCell180";
            this.xrTableCell180.Text = "xrTableCell72";
            this.xrTableCell180.Weight = 2.7284508786522728D;
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
            this.xrTable14.SizeF = new System.Drawing.SizeF(269.2705F, 30.66913F);
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
            this.xrTRecupGasc4,
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
            // xrTRecupGasc4
            // 
            this.xrTRecupGasc4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecupGasc4.Name = "xrTRecupGasc4";
            this.xrTRecupGasc4.StylePriority.UseBorders = false;
            this.xrTRecupGasc4.Text = "0.00";
            this.xrTRecupGasc4.Weight = 0.80324115924401418D;
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
            this.xrTRecuperacnCombLiq.Weight = 0.62488399742759915D;
            // 
            // xrTRecuperacnCombGas
            // 
            this.xrTRecuperacnCombGas.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCombGas.Name = "xrTRecuperacnCombGas";
            this.xrTRecuperacnCombGas.StylePriority.UseBorders = false;
            this.xrTRecuperacnCombGas.Text = "0.00";
            this.xrTRecuperacnCombGas.Weight = 0.66006411799303577D;
            // 
            // xrTRecuperacnVaporTn
            // 
            this.xrTRecuperacnVaporTn.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnVaporTn.Name = "xrTRecuperacnVaporTn";
            this.xrTRecuperacnVaporTn.StylePriority.UseBorders = false;
            this.xrTRecuperacnVaporTn.Text = "0.00";
            this.xrTRecuperacnVaporTn.Weight = 0.60220466632030212D;
            // 
            // xrTRecuperacnAguaNfr
            // 
            this.xrTRecuperacnAguaNfr.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnAguaNfr.Name = "xrTRecuperacnAguaNfr";
            this.xrTRecuperacnAguaNfr.StylePriority.UseBorders = false;
            this.xrTRecuperacnAguaNfr.Text = "0.00";
            this.xrTRecuperacnAguaNfr.Weight = 0.5859889758536927D;
            // 
            // xrTRecuperacnAireCom
            // 
            this.xrTRecuperacnAireCom.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnAireCom.Name = "xrTRecuperacnAireCom";
            this.xrTRecuperacnAireCom.StylePriority.UseBorders = false;
            this.xrTRecuperacnAireCom.Text = "0.00";
            this.xrTRecuperacnAireCom.Weight = 0.73043050788151642D;
            // 
            // xrTRecuperacnCompradakWh
            // 
            this.xrTRecuperacnCompradakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnCompradakWh.Name = "xrTRecuperacnCompradakWh";
            this.xrTRecuperacnCompradakWh.StylePriority.UseBorders = false;
            this.xrTRecuperacnCompradakWh.Text = "0.00";
            this.xrTRecuperacnCompradakWh.Weight = 0.73043103484929739D;
            // 
            // xrTRecuperacnPropiakWh
            // 
            this.xrTRecuperacnPropiakWh.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRecuperacnPropiakWh.Name = "xrTRecuperacnPropiakWh";
            this.xrTRecuperacnPropiakWh.StylePriority.UseBorders = false;
            this.xrTRecuperacnPropiakWh.Text = "0.00";
            this.xrTRecuperacnPropiakWh.Weight = 0.59892637159618156D;
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
            this.xrTableCell68.Text = "U. REFORMACIÓN CATALITICA";
            this.xrTableCell68.Weight = 0.7819655620653625D;
            // 
            // xrTRefCataliticaOperacion
            // 
            this.xrTRefCataliticaOperacion.Name = "xrTRefCataliticaOperacion";
            this.xrTRefCataliticaOperacion.Text = "0.00";
            this.xrTRefCataliticaOperacion.Weight = 0.54280524776987882D;
            // 
            // xrTRefCataliticaCargaM3
            // 
            this.xrTRefCataliticaCargaM3.Name = "xrTRefCataliticaCargaM3";
            this.xrTRefCataliticaCargaM3.Text = "0.00";
            this.xrTRefCataliticaCargaM3.Weight = 0.549510058763722D;
            // 
            // xrTRefCataliticaCrgaPromedio
            // 
            this.xrTRefCataliticaCrgaPromedio.Name = "xrTRefCataliticaCrgaPromedio";
            this.xrTRefCataliticaCrgaPromedio.Text = "0.00";
            this.xrTRefCataliticaCrgaPromedio.Weight = 0.57316584351871158D;
            // 
            // xrTRefCataliticaCapOperac
            // 
            this.xrTRefCataliticaCapOperac.Name = "xrTRefCataliticaCapOperac";
            this.xrTRefCataliticaCapOperac.Text = "0.00";
            this.xrTRefCataliticaCapOperac.Weight = 0.68737306946104793D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable10});
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.Text = "xrTableCell72";
            this.xrTableCell72.Weight = 2.7284508856746439D;
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
            this.xrTable10.SizeF = new System.Drawing.SizeF(269.2704F, 30.09045F);
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
            this.xrTableCell161.Text = "C4 (-)";
            this.xrTableCell161.Weight = 0.80324115924401418D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell162.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.StylePriority.UseBackColor = false;
            this.xrTableCell162.StylePriority.UseBorders = false;
            this.xrTableCell162.Text = "PLAT";
            this.xrTableCell162.Weight = 0.62962987331026732D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell163.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.StylePriority.UseBackColor = false;
            this.xrTableCell163.StylePriority.UseBorders = false;
            this.xrTableCell163.Text = "100";
            this.xrTableCell163.Weight = 0.7638890412620869D;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTReforCatGASREF,
            this.xrTReforCatC4,
            this.xrTReforCatPLAT,
            this.xrTableCell167});
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 1D;
            // 
            // xrTReforCatGASREF
            // 
            this.xrTReforCatGASREF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTReforCatGASREF.Name = "xrTReforCatGASREF";
            this.xrTReforCatGASREF.StylePriority.UseBorders = false;
            this.xrTReforCatGASREF.Text = "0.00";
            this.xrTReforCatGASREF.Weight = 0.8032400537355473D;
            // 
            // xrTReforCatC4
            // 
            this.xrTReforCatC4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTReforCatC4.Name = "xrTReforCatC4";
            this.xrTReforCatC4.StylePriority.UseBorders = false;
            this.xrTReforCatC4.Text = "0.00";
            this.xrTReforCatC4.Weight = 0.80324115924401418D;
            // 
            // xrTReforCatPLAT
            // 
            this.xrTReforCatPLAT.Name = "xrTReforCatPLAT";
            this.xrTReforCatPLAT.StylePriority.UseBorders = false;
            this.xrTReforCatPLAT.Text = "0.00";
            this.xrTReforCatPLAT.Weight = 0.62962987331026732D;
            // 
            // xrTableCell167
            // 
            this.xrTableCell167.Name = "xrTableCell167";
            this.xrTableCell167.Text = "0.00";
            this.xrTableCell167.Weight = 0.7638890412620869D;
            // 
            // xrTRefCataliticaCombLiq
            // 
            this.xrTRefCataliticaCombLiq.Name = "xrTRefCataliticaCombLiq";
            this.xrTRefCataliticaCombLiq.Text = "0.00";
            this.xrTRefCataliticaCombLiq.Weight = 0.62488399601890943D;
            // 
            // xrTRefCataliticaCombGas
            // 
            this.xrTRefCataliticaCombGas.Name = "xrTRefCataliticaCombGas";
            this.xrTRefCataliticaCombGas.Text = "0.00";
            this.xrTRefCataliticaCombGas.Weight = 0.660064117100682D;
            // 
            // xrTRefCataliticaVaporTn
            // 
            this.xrTRefCataliticaVaporTn.Name = "xrTRefCataliticaVaporTn";
            this.xrTRefCataliticaVaporTn.Text = "0.00";
            this.xrTRefCataliticaVaporTn.Weight = 0.60220466457873267D;
            // 
            // xrTRefCataliticaAguaNfr
            // 
            this.xrTRefCataliticaAguaNfr.Name = "xrTRefCataliticaAguaNfr";
            this.xrTRefCataliticaAguaNfr.Text = "0.00";
            this.xrTRefCataliticaAguaNfr.Weight = 0.58598897387413917D;
            // 
            // xrTRefCataliticaAireCom
            // 
            this.xrTRefCataliticaAireCom.Name = "xrTRefCataliticaAireCom";
            this.xrTRefCataliticaAireCom.Text = "0.00";
            this.xrTRefCataliticaAireCom.Weight = 0.7304305853287093D;
            // 
            // xrTRefCataliticaCompradakWh
            // 
            this.xrTRefCataliticaCompradakWh.Name = "xrTRefCataliticaCompradakWh";
            this.xrTRefCataliticaCompradakWh.Text = "0.00";
            this.xrTRefCataliticaCompradakWh.Weight = 0.73042995269540079D;
            // 
            // xrTRefCataliticaPropiakWh
            // 
            this.xrTRefCataliticaPropiakWh.Name = "xrTRefCataliticaPropiakWh";
            this.xrTRefCataliticaPropiakWh.Text = "0.00";
            this.xrTRefCataliticaPropiakWh.Weight = 0.59892737684573916D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell112,
            this.xrTNurcRefCataliticaOperacion,
            this.xrTNurcRefCataliticaCargaM3,
            this.xrTNurcRefCataliticaCrgaPromedio,
            this.xrTNurcRefCataliticaCapOperac,
            this.xrTableCell116,
            this.xrTNurcRefCataliticaCombLiq,
            this.xrTNurcRefCataliticaCombGas,
            this.xrTNurcRefCataliticaVaporTn,
            this.xrTNurcRefCataliticaAguaNfr,
            this.xrTNurcRefCataliticaAireCom,
            this.xrTNurcRefCataliticaCompradakWh,
            this.xrTNurcRefCataliticaPropiakWh});
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 1D;
            // 
            // xrTableCell112
            // 
            this.xrTableCell112.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell112.Name = "xrTableCell112";
            this.xrTableCell112.StylePriority.UseFont = false;
            this.xrTableCell112.Text = "(NURC) REFORMACION CATALITICA";
            this.xrTableCell112.Weight = 0.78196533055650863D;
            // 
            // xrTNurcRefCataliticaOperacion
            // 
            this.xrTNurcRefCataliticaOperacion.Name = "xrTNurcRefCataliticaOperacion";
            this.xrTNurcRefCataliticaOperacion.Text = "0.00";
            this.xrTNurcRefCataliticaOperacion.Weight = 0.54280581735065769D;
            // 
            // xrTNurcRefCataliticaCargaM3
            // 
            this.xrTNurcRefCataliticaCargaM3.Name = "xrTNurcRefCataliticaCargaM3";
            this.xrTNurcRefCataliticaCargaM3.Text = "0.00";
            this.xrTNurcRefCataliticaCargaM3.Weight = 0.54950969530811455D;
            // 
            // xrTNurcRefCataliticaCrgaPromedio
            // 
            this.xrTNurcRefCataliticaCrgaPromedio.Name = "xrTNurcRefCataliticaCrgaPromedio";
            this.xrTNurcRefCataliticaCrgaPromedio.Text = "0.00";
            this.xrTNurcRefCataliticaCrgaPromedio.Weight = 0.57316505348725644D;
            // 
            // xrTNurcRefCataliticaCapOperac
            // 
            this.xrTNurcRefCataliticaCapOperac.Name = "xrTNurcRefCataliticaCapOperac";
            this.xrTNurcRefCataliticaCapOperac.Text = "0.00";
            this.xrTNurcRefCataliticaCapOperac.Weight = 0.68737358992396025D;
            // 
            // xrTableCell116
            // 
            this.xrTableCell116.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable12});
            this.xrTableCell116.Name = "xrTableCell116";
            this.xrTableCell116.Text = "xrTableCell116";
            this.xrTableCell116.Weight = 2.7284506256649568D;
            // 
            // xrTable12
            // 
            this.xrTable12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable12.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0.2235031F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow30,
            this.xrTableRow31});
            this.xrTable12.SizeF = new System.Drawing.SizeF(269.2704F, 29.86694F);
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
            this.xrTableCell168.Text = "GAS REF";
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
            this.xrTableCell169.Text = "GLP";
            this.xrTableCell169.Weight = 0.80324115924401418D;
            // 
            // xrTableCell170
            // 
            this.xrTableCell170.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell170.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell170.Name = "xrTableCell170";
            this.xrTableCell170.StylePriority.UseBackColor = false;
            this.xrTableCell170.StylePriority.UseBorders = false;
            this.xrTableCell170.Text = "PLAT";
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
            this.xrTNurcGASREF,
            this.xrTNurcGLP,
            this.xrTNurcPLAT,
            this.xrTableCell175});
            this.xrTableRow31.Name = "xrTableRow31";
            this.xrTableRow31.Weight = 1D;
            // 
            // xrTNurcGASREF
            // 
            this.xrTNurcGASREF.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTNurcGASREF.Name = "xrTNurcGASREF";
            this.xrTNurcGASREF.StylePriority.UseBorders = false;
            this.xrTNurcGASREF.Text = "0.00";
            this.xrTNurcGASREF.Weight = 0.8032400537355473D;
            // 
            // xrTNurcGLP
            // 
            this.xrTNurcGLP.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTNurcGLP.Name = "xrTNurcGLP";
            this.xrTNurcGLP.StylePriority.UseBorders = false;
            this.xrTNurcGLP.Text = "0.00";
            this.xrTNurcGLP.Weight = 0.80324115924401418D;
            // 
            // xrTNurcPLAT
            // 
            this.xrTNurcPLAT.Name = "xrTNurcPLAT";
            this.xrTNurcPLAT.StylePriority.UseBorders = false;
            this.xrTNurcPLAT.Text = "0.00";
            this.xrTNurcPLAT.Weight = 0.62962987331026732D;
            // 
            // xrTableCell175
            // 
            this.xrTableCell175.Name = "xrTableCell175";
            this.xrTableCell175.Weight = 0.7638890412620869D;
            // 
            // xrTNurcRefCataliticaCombLiq
            // 
            this.xrTNurcRefCataliticaCombLiq.Name = "xrTNurcRefCataliticaCombLiq";
            this.xrTNurcRefCataliticaCombLiq.Text = "0.00";
            this.xrTNurcRefCataliticaCombLiq.Weight = 0.62488392434693407D;
            // 
            // xrTNurcRefCataliticaCombGas
            // 
            this.xrTNurcRefCataliticaCombGas.Name = "xrTNurcRefCataliticaCombGas";
            this.xrTNurcRefCataliticaCombGas.Text = "0.00";
            this.xrTNurcRefCataliticaCombGas.Weight = 0.66006404336336311D;
            // 
            // xrTNurcRefCataliticaVaporTn
            // 
            this.xrTNurcRefCataliticaVaporTn.Name = "xrTNurcRefCataliticaVaporTn";
            this.xrTNurcRefCataliticaVaporTn.Text = "0.00";
            this.xrTNurcRefCataliticaVaporTn.Weight = 0.60220467154501012D;
            // 
            // xrTNurcRefCataliticaAguaNfr
            // 
            this.xrTNurcRefCataliticaAguaNfr.Name = "xrTNurcRefCataliticaAguaNfr";
            this.xrTNurcRefCataliticaAguaNfr.Text = "0.00";
            this.xrTNurcRefCataliticaAguaNfr.Weight = 0.58598905909908749D;
            // 
            // xrTNurcRefCataliticaAireCom
            // 
            this.xrTNurcRefCataliticaAireCom.Name = "xrTNurcRefCataliticaAireCom";
            this.xrTNurcRefCataliticaAireCom.Text = "0.00";
            this.xrTNurcRefCataliticaAireCom.Weight = 0.73043050746015437D;
            // 
            // xrTNurcRefCataliticaCompradakWh
            // 
            this.xrTNurcRefCataliticaCompradakWh.Name = "xrTNurcRefCataliticaCompradakWh";
            this.xrTNurcRefCataliticaCompradakWh.Text = "0.00";
            this.xrTNurcRefCataliticaCompradakWh.Weight = 0.73043033866735108D;
            // 
            // xrTNurcRefCataliticaPropiakWh
            // 
            this.xrTNurcRefCataliticaPropiakWh.Name = "xrTNurcRefCataliticaPropiakWh";
            this.xrTNurcRefCataliticaPropiakWh.Text = "0.00";
            this.xrTNurcRefCataliticaPropiakWh.Weight = 0.59892706657092853D;
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell136,
            this.xrTDestiladoOperacion,
            this.xrTDestiladoCargaM3,
            this.xrTDestiladoCrgaPromedio,
            this.xrTDestiladoCapOperac,
            this.xrTableCell140,
            this.xrTDestiladoCombLiq,
            this.xrTDestiladoCombGas,
            this.xrTDestiladoVaporTn,
            this.xrTDestiladoAguaNfr,
            this.xrTDestiladoAireCom,
            this.xrTDestiladoCompradakWh,
            this.xrTDestiladoPropiakWh});
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 1D;
            // 
            // xrTableCell136
            // 
            this.xrTableCell136.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell136.Name = "xrTableCell136";
            this.xrTableCell136.StylePriority.UseFont = false;
            this.xrTableCell136.Text = "U. REDESTILADO";
            this.xrTableCell136.Weight = 0.78196548494442919D;
            // 
            // xrTDestiladoOperacion
            // 
            this.xrTDestiladoOperacion.Name = "xrTDestiladoOperacion";
            this.xrTDestiladoOperacion.Text = "0.00";
            this.xrTDestiladoOperacion.Weight = 0.5428058141512685D;
            // 
            // xrTDestiladoCargaM3
            // 
            this.xrTDestiladoCargaM3.Name = "xrTDestiladoCargaM3";
            this.xrTDestiladoCargaM3.Text = "0.00";
            this.xrTDestiladoCargaM3.Weight = 0.5495095348539D;
            // 
            // xrTDestiladoCrgaPromedio
            // 
            this.xrTDestiladoCrgaPromedio.Name = "xrTDestiladoCrgaPromedio";
            this.xrTDestiladoCrgaPromedio.Text = "0.00";
            this.xrTDestiladoCrgaPromedio.Weight = 0.5731648152055151D;
            // 
            // xrTDestiladoCapOperac
            // 
            this.xrTDestiladoCapOperac.Name = "xrTDestiladoCapOperac";
            this.xrTDestiladoCapOperac.Text = "0.00";
            this.xrTDestiladoCapOperac.Weight = 0.68737361090419269D;
            // 
            // xrTableCell140
            // 
            this.xrTableCell140.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
            this.xrTableCell140.Name = "xrTableCell140";
            this.xrTableCell140.Text = "xrTableCell140";
            this.xrTableCell140.Weight = 2.728450615131405D;
            // 
            // xrTable13
            // 
            this.xrTable13.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable13.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(9.155273E-05F, 2.288818E-05F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow32,
            this.xrTableRow33});
            this.xrTable13.SizeF = new System.Drawing.SizeF(269.2705F, 30.09044F);
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
            this.xrTableCell196.Text = "C. FDO+CBZA";
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
            this.xrTableCell197.Text = "REDEST";
            this.xrTableCell197.Weight = 0.80324115924401418D;
            // 
            // xrTableCell198
            // 
            this.xrTableCell198.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell198.Name = "xrTableCell198";
            this.xrTableCell198.StylePriority.UseBorders = false;
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
            this.xrTRedistFDOCBZA,
            this.xrTRedistREDEST,
            this.xrTableCell202,
            this.xrTableCell203});
            this.xrTableRow33.Name = "xrTableRow33";
            this.xrTableRow33.Weight = 1D;
            // 
            // xrTRedistFDOCBZA
            // 
            this.xrTRedistFDOCBZA.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTRedistFDOCBZA.Name = "xrTRedistFDOCBZA";
            this.xrTRedistFDOCBZA.StylePriority.UseBorders = false;
            this.xrTRedistFDOCBZA.Text = "0.00";
            this.xrTRedistFDOCBZA.Weight = 0.8032400537355473D;
            // 
            // xrTRedistREDEST
            // 
            this.xrTRedistREDEST.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTRedistREDEST.Name = "xrTRedistREDEST";
            this.xrTRedistREDEST.StylePriority.UseBorders = false;
            this.xrTRedistREDEST.Text = "0.00";
            this.xrTRedistREDEST.Weight = 0.80324115924401418D;
            // 
            // xrTableCell202
            // 
            this.xrTableCell202.Name = "xrTableCell202";
            this.xrTableCell202.StylePriority.UseBorders = false;
            this.xrTableCell202.Weight = 0.62962987331026732D;
            // 
            // xrTableCell203
            // 
            this.xrTableCell203.Name = "xrTableCell203";
            this.xrTableCell203.Weight = 0.7638890412620869D;
            // 
            // xrTDestiladoCombLiq
            // 
            this.xrTDestiladoCombLiq.Name = "xrTDestiladoCombLiq";
            this.xrTDestiladoCombLiq.Text = "0.00";
            this.xrTDestiladoCombLiq.Weight = 0.62488392645997015D;
            // 
            // xrTDestiladoCombGas
            // 
            this.xrTDestiladoCombGas.Name = "xrTDestiladoCombGas";
            this.xrTDestiladoCombGas.Text = "0.00";
            this.xrTDestiladoCombGas.Weight = 0.6600640447018955D;
            // 
            // xrTDestiladoVaporTn
            // 
            this.xrTDestiladoVaporTn.Name = "xrTDestiladoVaporTn";
            this.xrTDestiladoVaporTn.Text = "0.00";
            this.xrTDestiladoVaporTn.Weight = 0.60220467415736412D;
            // 
            // xrTDestiladoAguaNfr
            // 
            this.xrTDestiladoAguaNfr.Name = "xrTDestiladoAguaNfr";
            this.xrTDestiladoAguaNfr.Text = "0.00";
            this.xrTDestiladoAguaNfr.Weight = 0.585989062068416D;
            // 
            // xrTDestiladoAireCom
            // 
            this.xrTDestiladoAireCom.Name = "xrTDestiladoAireCom";
            this.xrTDestiladoAireCom.Text = "0.00";
            this.xrTDestiladoAireCom.Weight = 0.73043050724947345D;
            // 
            // xrTDestiladoCompradakWh
            // 
            this.xrTDestiladoCompradakWh.Name = "xrTDestiladoCompradakWh";
            this.xrTDestiladoCompradakWh.Text = "0.00";
            this.xrTDestiladoCompradakWh.Weight = 0.73043033845669725D;
            // 
            // xrTDestiladoPropiakWh
            // 
            this.xrTDestiladoPropiakWh.Name = "xrTDestiladoPropiakWh";
            this.xrTDestiladoPropiakWh.Text = "0.00";
            this.xrTDestiladoPropiakWh.Weight = 0.59892706617798264D;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell124,
            this.xrTAlquilatosOperacion,
            this.xrTAlquilatosCargaM3,
            this.xrTAlquilatosCrgaPromedio,
            this.xrTAlquilatosCapOperac,
            this.xrTableCell128,
            this.xrTAlquilatosCombLiq,
            this.xrTAlquilatosCombGas,
            this.xrTAlquilatosVaporTn,
            this.xrTAlquilatosAguaNfr,
            this.xrTAlquilatosAireCom,
            this.xrTAlquilatosCompradakWh,
            this.xrTAlquilatosPropiakWh});
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 1D;
            // 
            // xrTableCell124
            // 
            this.xrTableCell124.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell124.Name = "xrTableCell124";
            this.xrTableCell124.StylePriority.UseFont = false;
            this.xrTableCell124.Text = "U. ALQUILATOS";
            this.xrTableCell124.Weight = 0.78196556191499722D;
            // 
            // xrTAlquilatosOperacion
            // 
            this.xrTAlquilatosOperacion.Name = "xrTAlquilatosOperacion";
            this.xrTAlquilatosOperacion.Text = "0.00";
            this.xrTAlquilatosOperacion.Weight = 0.54280524698736377D;
            // 
            // xrTAlquilatosCargaM3
            // 
            this.xrTAlquilatosCargaM3.Name = "xrTAlquilatosCargaM3";
            this.xrTAlquilatosCargaM3.Text = "0.00";
            this.xrTAlquilatosCargaM3.Weight = 0.54951005486990312D;
            // 
            // xrTAlquilatosCrgaPromedio
            // 
            this.xrTAlquilatosCrgaPromedio.Name = "xrTAlquilatosCrgaPromedio";
            this.xrTAlquilatosCrgaPromedio.Text = "0.00";
            this.xrTAlquilatosCrgaPromedio.Weight = 0.57316553005073967D;
            // 
            // xrTAlquilatosCapOperac
            // 
            this.xrTAlquilatosCapOperac.Name = "xrTAlquilatosCapOperac";
            this.xrTAlquilatosCapOperac.Text = "0.00";
            this.xrTAlquilatosCapOperac.Weight = 0.68737339132440378D;
            // 
            // xrTableCell128
            // 
            this.xrTableCell128.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable15});
            this.xrTableCell128.Name = "xrTableCell128";
            this.xrTableCell128.Text = "xrTableCell128";
            this.xrTableCell128.Weight = 2.7284507240387978D;
            // 
            // xrTable15
            // 
            this.xrTable15.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable15.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable15.Name = "xrTable15";
            this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow34,
            this.xrTableRow35});
            this.xrTable15.SizeF = new System.Drawing.SizeF(269.2706F, 30.09044F);
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
            this.xrTableCell204.Text = "C5+IC5";
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
            this.xrTableCell205.Text = "IC6+IC7";
            this.xrTableCell205.Weight = 0.80324115924401418D;
            // 
            // xrTableCell206
            // 
            this.xrTableCell206.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell206.Name = "xrTableCell206";
            this.xrTableCell206.StylePriority.UseBorders = false;
            this.xrTableCell206.Weight = 0.62962987331026732D;
            // 
            // xrTableCell207
            // 
            this.xrTableCell207.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell207.Name = "xrTableCell207";
            this.xrTableCell207.StylePriority.UseBorders = false;
            this.xrTableCell207.Weight = 0.7638890412620869D;
            // 
            // xrTableRow35
            // 
            this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTAlquilatosc5,
            this.xrTAlquilatosc6,
            this.xrTableCell210,
            this.xrTableCell211});
            this.xrTableRow35.Name = "xrTableRow35";
            this.xrTableRow35.Weight = 1D;
            // 
            // xrTAlquilatosc5
            // 
            this.xrTAlquilatosc5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTAlquilatosc5.Name = "xrTAlquilatosc5";
            this.xrTAlquilatosc5.StylePriority.UseBorders = false;
            this.xrTAlquilatosc5.Text = "0.00";
            this.xrTAlquilatosc5.Weight = 0.8032400537355473D;
            // 
            // xrTAlquilatosc6
            // 
            this.xrTAlquilatosc6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTAlquilatosc6.Name = "xrTAlquilatosc6";
            this.xrTAlquilatosc6.StylePriority.UseBorders = false;
            this.xrTAlquilatosc6.Text = "0.00";
            this.xrTAlquilatosc6.Weight = 0.80324115924401418D;
            // 
            // xrTableCell210
            // 
            this.xrTableCell210.Name = "xrTableCell210";
            this.xrTableCell210.StylePriority.UseBorders = false;
            this.xrTableCell210.Weight = 0.62962987331026732D;
            // 
            // xrTableCell211
            // 
            this.xrTableCell211.Name = "xrTableCell211";
            this.xrTableCell211.Weight = 0.7638890412620869D;
            // 
            // xrTAlquilatosCombLiq
            // 
            this.xrTAlquilatosCombLiq.Name = "xrTAlquilatosCombLiq";
            this.xrTAlquilatosCombLiq.Text = "0.00";
            this.xrTAlquilatosCombLiq.Weight = 0.62488399742759915D;
            // 
            // xrTAlquilatosCombGas
            // 
            this.xrTAlquilatosCombGas.Name = "xrTAlquilatosCombGas";
            this.xrTAlquilatosCombGas.Text = "0.00";
            this.xrTAlquilatosCombGas.Weight = 0.66006411799303577D;
            // 
            // xrTAlquilatosVaporTn
            // 
            this.xrTAlquilatosVaporTn.Name = "xrTAlquilatosVaporTn";
            this.xrTAlquilatosVaporTn.Text = "0.00";
            this.xrTAlquilatosVaporTn.Weight = 0.60220466632030212D;
            // 
            // xrTAlquilatosAguaNfr
            // 
            this.xrTAlquilatosAguaNfr.Name = "xrTAlquilatosAguaNfr";
            this.xrTAlquilatosAguaNfr.Text = "0.00";
            this.xrTAlquilatosAguaNfr.Weight = 0.5859889758536927D;
            // 
            // xrTAlquilatosAireCom
            // 
            this.xrTAlquilatosAireCom.Name = "xrTAlquilatosAireCom";
            this.xrTAlquilatosAireCom.Text = "0.00";
            this.xrTAlquilatosAireCom.Weight = 0.73043050788151642D;
            // 
            // xrTAlquilatosCompradakWh
            // 
            this.xrTAlquilatosCompradakWh.Name = "xrTAlquilatosCompradakWh";
            this.xrTAlquilatosCompradakWh.Text = "0.00";
            this.xrTAlquilatosCompradakWh.Weight = 0.7304304163953963D;
            // 
            // xrTAlquilatosPropiakWh
            // 
            this.xrTAlquilatosPropiakWh.Name = "xrTAlquilatosPropiakWh";
            this.xrTAlquilatosPropiakWh.Text = "0.00";
            this.xrTAlquilatosPropiakWh.Weight = 0.59892699005008265D;
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell148,
            this.xrTDesopentanizaOperacion,
            this.xrTDesopentanizaCargaM3,
            this.xrTDesopentanizaCrgaPromedio,
            this.xrTDesopentanizaCapOperac,
            this.xrTableCell152,
            this.xrTDesopentanizaCombLiq,
            this.xrTDesopentanizaCombGas,
            this.xrTDesopentanizaVaporTn,
            this.xrTDesopentanizaAguaNfr,
            this.xrTDesopentanizaAireCom,
            this.xrTDesopentanizaCompradakWh,
            this.xrTDesopentanizaPropiakWh});
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 1D;
            // 
            // xrTableCell148
            // 
            this.xrTableCell148.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTableCell148.Name = "xrTableCell148";
            this.xrTableCell148.StylePriority.UseFont = false;
            this.xrTableCell148.Text = "U. DEISOPENTANIZADORA";
            this.xrTableCell148.Weight = 0.78196556097736614D;
            // 
            // xrTDesopentanizaOperacion
            // 
            this.xrTDesopentanizaOperacion.Name = "xrTDesopentanizaOperacion";
            this.xrTDesopentanizaOperacion.Text = "0.00";
            this.xrTDesopentanizaOperacion.Weight = 0.54280509479077221D;
            // 
            // xrTDesopentanizaCargaM3
            // 
            this.xrTDesopentanizaCargaM3.Name = "xrTDesopentanizaCargaM3";
            this.xrTDesopentanizaCargaM3.Text = "0.00";
            this.xrTDesopentanizaCargaM3.Weight = 0.5495103660437658D;
            // 
            // xrTDesopentanizaCrgaPromedio
            // 
            this.xrTDesopentanizaCrgaPromedio.Name = "xrTDesopentanizaCrgaPromedio";
            this.xrTDesopentanizaCrgaPromedio.Text = "0.00";
            this.xrTDesopentanizaCrgaPromedio.Weight = 0.57316553217124866D;
            // 
            // xrTDesopentanizaCapOperac
            // 
            this.xrTDesopentanizaCapOperac.Name = "xrTDesopentanizaCapOperac";
            this.xrTDesopentanizaCapOperac.Text = "0.00";
            this.xrTDesopentanizaCapOperac.Weight = 0.6873733066866512D;
            // 
            // xrTableCell152
            // 
            this.xrTableCell152.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16});
            this.xrTableCell152.Name = "xrTableCell152";
            this.xrTableCell152.Text = "xrTableCell152";
            this.xrTableCell152.Weight = 2.7284507275499816D;
            // 
            // xrTable16
            // 
            this.xrTable16.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable16.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0.223526F);
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow39,
            this.xrTableRow40});
            this.xrTable16.SizeF = new System.Drawing.SizeF(269.2705F, 30.44562F);
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
            this.xrTableCell212.Text = "PESADOS";
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
            this.xrTableCell213.Text = "C4";
            this.xrTableCell213.Weight = 0.80324115924401418D;
            // 
            // xrTableCell214
            // 
            this.xrTableCell214.BackColor = System.Drawing.Color.DarkGray;
            this.xrTableCell214.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell214.Name = "xrTableCell214";
            this.xrTableCell214.StylePriority.UseBackColor = false;
            this.xrTableCell214.StylePriority.UseBorders = false;
            this.xrTableCell214.Text = "IC5";
            this.xrTableCell214.Weight = 0.62962987331026732D;
            // 
            // xrTableCell215
            // 
            this.xrTableCell215.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell215.Name = "xrTableCell215";
            this.xrTableCell215.StylePriority.UseBorders = false;
            this.xrTableCell215.Weight = 0.7638890412620869D;
            // 
            // xrTableRow40
            // 
            this.xrTableRow40.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTDeisopentaPESADOS,
            this.xrTDeisopentaC4,
            this.xrTDeisopentaC5,
            this.xrTableCell219});
            this.xrTableRow40.Name = "xrTableRow40";
            this.xrTableRow40.Weight = 1D;
            // 
            // xrTDeisopentaPESADOS
            // 
            this.xrTDeisopentaPESADOS.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTDeisopentaPESADOS.Name = "xrTDeisopentaPESADOS";
            this.xrTDeisopentaPESADOS.StylePriority.UseBorders = false;
            this.xrTDeisopentaPESADOS.Text = "0.00";
            this.xrTDeisopentaPESADOS.Weight = 0.8032400537355473D;
            // 
            // xrTDeisopentaC4
            // 
            this.xrTDeisopentaC4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTDeisopentaC4.Name = "xrTDeisopentaC4";
            this.xrTDeisopentaC4.StylePriority.UseBorders = false;
            this.xrTDeisopentaC4.Text = "0.00";
            this.xrTDeisopentaC4.Weight = 0.80324115924401418D;
            // 
            // xrTDeisopentaC5
            // 
            this.xrTDeisopentaC5.Name = "xrTDeisopentaC5";
            this.xrTDeisopentaC5.StylePriority.UseBorders = false;
            this.xrTDeisopentaC5.Text = "0.00";
            this.xrTDeisopentaC5.Weight = 0.62962987331026732D;
            // 
            // xrTableCell219
            // 
            this.xrTableCell219.Name = "xrTableCell219";
            this.xrTableCell219.Weight = 0.7638890412620869D;
            // 
            // xrTDesopentanizaCombLiq
            // 
            this.xrTDesopentanizaCombLiq.Name = "xrTDesopentanizaCombLiq";
            this.xrTDesopentanizaCombLiq.Text = "0.00";
            this.xrTDesopentanizaCombLiq.Weight = 0.62488399672325434D;
            // 
            // xrTDesopentanizaCombGas
            // 
            this.xrTDesopentanizaCombGas.Name = "xrTDesopentanizaCombGas";
            this.xrTDesopentanizaCombGas.Text = "0.00";
            this.xrTDesopentanizaCombGas.Weight = 0.6600641175468589D;
            // 
            // xrTDesopentanizaVaporTn
            // 
            this.xrTDesopentanizaVaporTn.Name = "xrTDesopentanizaVaporTn";
            this.xrTDesopentanizaVaporTn.Text = "0.00";
            this.xrTDesopentanizaVaporTn.Weight = 0.60220466544951745D;
            // 
            // xrTDesopentanizaAguaNfr
            // 
            this.xrTDesopentanizaAguaNfr.Name = "xrTDesopentanizaAguaNfr";
            this.xrTDesopentanizaAguaNfr.Text = "0.00";
            this.xrTDesopentanizaAguaNfr.Weight = 0.585988974863916D;
            // 
            // xrTDesopentanizaAireCom
            // 
            this.xrTDesopentanizaAireCom.Name = "xrTDesopentanizaAireCom";
            this.xrTDesopentanizaAireCom.Text = "0.00";
            this.xrTDesopentanizaAireCom.Weight = 0.73043058525848159D;
            // 
            // xrTDesopentanizaCompradakWh
            // 
            this.xrTDesopentanizaCompradakWh.Name = "xrTDesopentanizaCompradakWh";
            this.xrTDesopentanizaCompradakWh.Text = "0.00";
            this.xrTDesopentanizaCompradakWh.Weight = 0.73043033915887667D;
            // 
            // xrTDesopentanizaPropiakWh
            // 
            this.xrTDesopentanizaPropiakWh.Name = "xrTDesopentanizaPropiakWh";
            this.xrTDesopentanizaPropiakWh.Text = "0.00";
            this.xrTDesopentanizaPropiakWh.Weight = 0.598926990181064D;
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
            this.xrTU,
            this.xrTUdc27Operacion,
            this.xrTUdc27CargaM3,
            this.xrTUdc27CrgaPromedio,
            this.xrTUdc27CapOperac,
            this.xrTableCell21,
            this.xrTUdc27CombLiq,
            this.xrTUdc27CombGas,
            this.xrTUdc27VaporTn,
            this.xrTUdc27AguaNfr,
            this.xrTUdc27AireCom,
            this.xrTUdc27CompradakWh,
            this.xrTUdc27PropiakWh});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTU
            // 
            this.xrTU.Name = "xrTU";
            this.xrTU.Text = "UDC-27250";
            this.xrTU.Weight = 0.7717207194437179D;
            // 
            // xrTUdc27Operacion
            // 
            this.xrTUdc27Operacion.Name = "xrTUdc27Operacion";
            this.xrTUdc27Operacion.Text = "0.00";
            this.xrTUdc27Operacion.Weight = 0.5356944455121726D;
            // 
            // xrTUdc27CargaM3
            // 
            this.xrTUdc27CargaM3.Name = "xrTUdc27CargaM3";
            this.xrTUdc27CargaM3.Text = "0.00";
            this.xrTUdc27CargaM3.Weight = 0.54231069451440317D;
            // 
            // xrTUdc27CrgaPromedio
            // 
            this.xrTUdc27CrgaPromedio.Name = "xrTUdc27CrgaPromedio";
            this.xrTUdc27CrgaPromedio.Text = "0.00";
            this.xrTUdc27CrgaPromedio.Weight = 0.56565552952560394D;
            // 
            // xrTUdc27CapOperac
            // 
            this.xrTUdc27CapOperac.Name = "xrTUdc27CapOperac";
            this.xrTUdc27CapOperac.Text = "0.00";
            this.xrTUdc27CapOperac.Weight = 0.67836856455512762D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "xrTableCell21";
            this.xrTableCell21.Weight = 2.6927049688977931D;
            // 
            // xrTable7
            // 
            this.xrTable7.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(6.103516E-05F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16,
            this.xrTableRow17,
            this.xrTableRow18,
            this.xrTableRow19});
            this.xrTable7.SizeF = new System.Drawing.SizeF(269.2708F, 57.33802F);
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
            this.xrTableCell80.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseBackColor = false;
            this.xrTableCell80.StylePriority.UseBorders = false;
            this.xrTableCell80.Text = "C4(-)";
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
            this.xrTUDC27250c4,
            this.xrTUDC27250LSR,
            this.xrTUDC27250MSR,
            this.xrTUDC27250DL});
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTUDC27250c4
            // 
            this.xrTUDC27250c4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTUDC27250c4.Name = "xrTUDC27250c4";
            this.xrTUDC27250c4.StylePriority.UseBorders = false;
            this.xrTUDC27250c4.Text = "0.00";
            this.xrTUDC27250c4.Weight = 0.8032400537355473D;
            // 
            // xrTUDC27250LSR
            // 
            this.xrTUDC27250LSR.Name = "xrTUDC27250LSR";
            this.xrTUDC27250LSR.Text = "0.00";
            this.xrTUDC27250LSR.Weight = 0.80324115924401418D;
            // 
            // xrTUDC27250MSR
            // 
            this.xrTUDC27250MSR.Name = "xrTUDC27250MSR";
            this.xrTUDC27250MSR.Text = "0.00";
            this.xrTUDC27250MSR.Weight = 0.62962987331026732D;
            // 
            // xrTUDC27250DL
            // 
            this.xrTUDC27250DL.Name = "xrTUDC27250DL";
            this.xrTUDC27250DL.Text = "0.00";
            this.xrTUDC27250DL.Weight = 0.7638890412620869D;
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
            this.xrTableCell88.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.StylePriority.UseBackColor = false;
            this.xrTableCell88.StylePriority.UseBorders = false;
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
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.Weight = 0.7638890412620869D;
            // 
            // xrTableRow19
            // 
            this.xrTableRow19.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTUDC27250DM,
            this.xrTUDC27250DP,
            this.xrTUDC27250CR,
            this.xrTableCell95});
            this.xrTableRow19.Name = "xrTableRow19";
            this.xrTableRow19.Weight = 1D;
            // 
            // xrTUDC27250DM
            // 
            this.xrTUDC27250DM.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTUDC27250DM.Name = "xrTUDC27250DM";
            this.xrTUDC27250DM.StylePriority.UseBorders = false;
            this.xrTUDC27250DM.Text = "0.00";
            this.xrTUDC27250DM.Weight = 0.8032400537355473D;
            // 
            // xrTUDC27250DP
            // 
            this.xrTUDC27250DP.Name = "xrTUDC27250DP";
            this.xrTUDC27250DP.Text = "0.00";
            this.xrTUDC27250DP.Weight = 0.80324115924401418D;
            // 
            // xrTUDC27250CR
            // 
            this.xrTUDC27250CR.Name = "xrTUDC27250CR";
            this.xrTUDC27250CR.Text = "0.00";
            this.xrTUDC27250CR.Weight = 0.62962987331026732D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.Weight = 0.7638890412620869D;
            // 
            // xrTUdc27CombLiq
            // 
            this.xrTUdc27CombLiq.Name = "xrTUdc27CombLiq";
            this.xrTUdc27CombLiq.Text = "0.00";
            this.xrTUdc27CombLiq.Weight = 0.61669722443502D;
            // 
            // xrTUdc27CombGas
            // 
            this.xrTUdc27CombGas.Name = "xrTUdc27CombGas";
            this.xrTUdc27CombGas.Text = "0.00";
            this.xrTUdc27CombGas.Weight = 0.65141584345302328D;
            // 
            // xrTUdc27VaporTn
            // 
            this.xrTUdc27VaporTn.Name = "xrTUdc27VaporTn";
            this.xrTUdc27VaporTn.Text = "0.00";
            this.xrTUdc27VaporTn.Weight = 0.59431769831909886D;
            // 
            // xrTUdc27AguaNfr
            // 
            this.xrTUdc27AguaNfr.Name = "xrTUdc27AguaNfr";
            this.xrTUdc27AguaNfr.Text = "0.00";
            this.xrTUdc27AguaNfr.Weight = 0.57831039346795632D;
            // 
            // xrTUdc27AireCom
            // 
            this.xrTUdc27AireCom.Name = "xrTUdc27AireCom";
            this.xrTUdc27AireCom.Text = "0.00";
            this.xrTUdc27AireCom.Weight = 0.72086101605735253D;
            // 
            // xrTUdc27CompradakWh
            // 
            this.xrTUdc27CompradakWh.Name = "xrTUdc27CompradakWh";
            this.xrTUdc27CompradakWh.Text = "0.00";
            this.xrTUdc27CompradakWh.Weight = 0.72086223676014827D;
            // 
            // xrTUdc27PropiakWh
            // 
            this.xrTUdc27PropiakWh.Name = "xrTUdc27PropiakWh";
            this.xrTUdc27PropiakWh.Text = "0.00";
            this.xrTUdc27PropiakWh.Weight = 0.5910780314938644D;
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell44,
            this.xrTUdc125Operacion,
            this.xrTUdc125CargaM3,
            this.xrTUdc125CrgaPromedio,
            this.xrTUdc125CapOperac,
            this.xrTableCell60,
            this.xrTUdc125CombLiq,
            this.xrTUdc125CombGas,
            this.xrTUdc125VaporTn,
            this.xrTUdc125AguaNfr,
            this.xrTUdc125AireCom,
            this.xrTUdc125CompradakWh,
            this.xrTUdc125PropiakWh});
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.Text = "UDC-12500";
            this.xrTableCell44.Weight = 0.7717207194437179D;
            // 
            // xrTUdc125Operacion
            // 
            this.xrTUdc125Operacion.Name = "xrTUdc125Operacion";
            this.xrTUdc125Operacion.Text = "0.00";
            this.xrTUdc125Operacion.Weight = 0.5356944468745104D;
            // 
            // xrTUdc125CargaM3
            // 
            this.xrTUdc125CargaM3.Name = "xrTUdc125CargaM3";
            this.xrTUdc125CargaM3.Text = "0.00";
            this.xrTUdc125CargaM3.Weight = 0.54231063126224588D;
            // 
            // xrTUdc125CrgaPromedio
            // 
            this.xrTUdc125CrgaPromedio.Name = "xrTUdc125CrgaPromedio";
            this.xrTUdc125CrgaPromedio.Text = "0.00";
            this.xrTUdc125CrgaPromedio.Weight = 0.56565554325571066D;
            // 
            // xrTUdc125CapOperac
            // 
            this.xrTUdc125CapOperac.Name = "xrTUdc125CapOperac";
            this.xrTUdc125CapOperac.Text = "0.00";
            this.xrTUdc125CapOperac.Weight = 0.678367999414863D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable8});
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Text = "xrTableCell60";
            this.xrTableCell60.Weight = 2.6927058974783789D;
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
            this.xrTable8.SizeF = new System.Drawing.SizeF(269.2706F, 57.33802F);
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
            this.xrTableCell96.Text = "GLP";
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
            this.xrTableCell99.Text = "DO";
            this.xrTableCell99.Weight = 0.7638890412620869D;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTUDC1250GLP,
            this.xrTUDC1250LSR,
            this.xrTUDC1250MSR,
            this.xrTUDC1250DO});
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 1D;
            // 
            // xrTUDC1250GLP
            // 
            this.xrTUDC1250GLP.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTUDC1250GLP.Name = "xrTUDC1250GLP";
            this.xrTUDC1250GLP.StylePriority.UseBorders = false;
            this.xrTUDC1250GLP.Text = "0.00";
            this.xrTUDC1250GLP.Weight = 0.8032400537355473D;
            // 
            // xrTUDC1250LSR
            // 
            this.xrTUDC1250LSR.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTUDC1250LSR.Name = "xrTUDC1250LSR";
            this.xrTUDC1250LSR.StylePriority.UseBorders = false;
            this.xrTUDC1250LSR.Text = "0.00";
            this.xrTUDC1250LSR.Weight = 0.80324115924401418D;
            // 
            // xrTUDC1250MSR
            // 
            this.xrTUDC1250MSR.Name = "xrTUDC1250MSR";
            this.xrTUDC1250MSR.StylePriority.UseBorders = false;
            this.xrTUDC1250MSR.Text = "0.00";
            this.xrTUDC1250MSR.Weight = 0.62962987331026732D;
            // 
            // xrTUDC1250DO
            // 
            this.xrTUDC1250DO.Name = "xrTUDC1250DO";
            this.xrTUDC1250DO.Text = "0.00";
            this.xrTUDC1250DO.Weight = 0.7638890412620869D;
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
            this.xrTableCell104.Text = "C. RED+SLOP";
            this.xrTableCell104.Weight = 0.8032400537355473D;
            // 
            // xrTableCell105
            // 
            this.xrTableCell105.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell105.Name = "xrTableCell105";
            this.xrTableCell105.StylePriority.UseBorders = false;
            this.xrTableCell105.Weight = 0.80324115924401418D;
            // 
            // xrTableCell106
            // 
            this.xrTableCell106.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell106.Name = "xrTableCell106";
            this.xrTableCell106.StylePriority.UseBorders = false;
            this.xrTableCell106.Weight = 0.62962987331026732D;
            // 
            // xrTableCell107
            // 
            this.xrTableCell107.Name = "xrTableCell107";
            this.xrTableCell107.Weight = 0.7638890412620869D;
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTUDC1250REDSLOP,
            this.xrTableCell109,
            this.xrTableCell110,
            this.xrTableCell111});
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 1D;
            // 
            // xrTUDC1250REDSLOP
            // 
            this.xrTUDC1250REDSLOP.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTUDC1250REDSLOP.Name = "xrTUDC1250REDSLOP";
            this.xrTUDC1250REDSLOP.StylePriority.UseBorders = false;
            this.xrTUDC1250REDSLOP.Text = "0.00";
            this.xrTUDC1250REDSLOP.Weight = 0.8032400537355473D;
            // 
            // xrTableCell109
            // 
            this.xrTableCell109.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell109.Name = "xrTableCell109";
            this.xrTableCell109.StylePriority.UseBorders = false;
            this.xrTableCell109.Weight = 0.80324115924401418D;
            // 
            // xrTableCell110
            // 
            this.xrTableCell110.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell110.Name = "xrTableCell110";
            this.xrTableCell110.StylePriority.UseBorders = false;
            this.xrTableCell110.Weight = 0.62962987331026732D;
            // 
            // xrTableCell111
            // 
            this.xrTableCell111.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell111.Name = "xrTableCell111";
            this.xrTableCell111.StylePriority.UseBorders = false;
            this.xrTableCell111.Weight = 0.7638890412620869D;
            // 
            // xrTUdc125CombLiq
            // 
            this.xrTUdc125CombLiq.Name = "xrTUdc125CombLiq";
            this.xrTUdc125CombLiq.Text = "0.00";
            this.xrTUdc125CombLiq.Weight = 0.616697221363672D;
            // 
            // xrTUdc125CombGas
            // 
            this.xrTUdc125CombGas.Name = "xrTUdc125CombGas";
            this.xrTUdc125CombGas.Text = "0.00";
            this.xrTUdc125CombGas.Weight = 0.65141584140534925D;
            // 
            // xrTUdc125VaporTn
            // 
            this.xrTUdc125VaporTn.Name = "xrTUdc125VaporTn";
            this.xrTUdc125VaporTn.Text = "0.00";
            this.xrTUdc125VaporTn.Weight = 0.59431647388501174D;
            // 
            // xrTUdc125AguaNfr
            // 
            this.xrTUdc125AguaNfr.Name = "xrTUdc125AguaNfr";
            this.xrTUdc125AguaNfr.Text = "0.00";
            this.xrTUdc125AguaNfr.Weight = 0.5783103892647683D;
            // 
            // xrTUdc125AireCom
            // 
            this.xrTUdc125AireCom.Name = "xrTUdc125AireCom";
            this.xrTUdc125AireCom.Text = "0.00";
            this.xrTUdc125AireCom.Weight = 0.72086040570592869D;
            // 
            // xrTUdc125CompradakWh
            // 
            this.xrTUdc125CompradakWh.Name = "xrTUdc125CompradakWh";
            this.xrTUdc125CompradakWh.Text = "0.00";
            this.xrTUdc125CompradakWh.Weight = 0.72086284711157655D;
            // 
            // xrTUdc125PropiakWh
            // 
            this.xrTUdc125PropiakWh.Name = "xrTUdc125PropiakWh";
            this.xrTUdc125PropiakWh.Text = "0.00";
            this.xrTUdc125PropiakWh.Weight = 0.59107925249154536D;
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
            this.xrTableCell15,
            this.xrTQuienImprime,
            this.xrTableCell24,
            this.xrTFechaHora});
            this.xrTableRow63.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTableRow63.Name = "xrTableRow63";
            this.xrTableRow63.StylePriority.UseFont = false;
            this.xrTableRow63.Weight = 1D;
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
            this.xrTable29,
            this.xrTable28,
            this.xrPictureBox2,
            this.xrPictureBox1,
            this.xrPictureBox3,
            this.xrPageInfo1,
            this.xrTable4,
            this.xrTable27,
            this.xrTable3,
            this.xrTable1,
            this.xrTable9,
            this.xrTable2,
            this.xrTable6});
            this.pageHeaderBand1.HeightF = 167.5978F;
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
            this.xrTableCell23});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Weight = 1D;
            // 
            // xrTable28
            // 
            this.xrTable28.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTable28.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable28.LocationFloat = new DevExpress.Utils.PointFloat(143.4999F, 49.81412F);
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
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell22});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UsePadding = false;
            this.xrTableCell18.Text = "Código:";
            this.xrTableCell18.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.Text = "Versión:";
            this.xrTableCell19.Weight = 0.82134934269309889D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.Text = "Aprobado:";
            this.xrTableCell20.Weight = 1.4701330216349817D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell22.StylePriority.UsePadding = false;
            this.xrTableCell22.Text = "Páginas:";
            this.xrTableCell22.Weight = 0.70851763567191939D;
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
            this.xrPictureBox3.LocationFloat = new DevExpress.Utils.PointFloat(11.36649F, 4.000011F);
            this.xrPictureBox3.Name = "xrPictureBox3";
            this.xrPictureBox3.SizeF = new System.Drawing.SizeF(119.875F, 55.49995F);
            this.xrPictureBox3.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(801.9915F, 49.81412F);
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
            this.xrTableCell14});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.StylePriority.UseBorders = false;
            this.xrTableRow10.Weight = 1D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Weight = 1D;
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
            this.xrTableCell16});
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 1D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 1D;
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
            this.xrTableCell37.Weight = 0.60738615096511972D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Text = "CARGA TOTAL M3";
            this.xrTableCell39.Weight = 0.61488790950743166D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.Text = "CARGA PROMEDIO BBL/D";
            this.xrTableCell7.Weight = 0.64135701080854979D;
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
            this.xrTable9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable9.Font = new System.Drawing.Font("Times New Roman", 6F);
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(578.6457F, 122.8061F);
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
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(578.6457F, 136.3478F);
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
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell13,
            this.xrTableCell361});
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
            // xrTableCell361
            // 
            this.xrTableCell361.Name = "xrTableCell361";
            this.xrTableCell361.Text = "PROPIA kWh";
            this.xrTableCell361.Weight = 0.81996257599105882D;
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
            // repRendimientoVolumetricoGualvertoV
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
            ((System.ComponentModel.ISupportInitialize)(this.xrTable24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    #region Metodos Propios
    #endregion

}
