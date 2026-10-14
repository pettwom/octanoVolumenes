<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrFecheroMes.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrFecheroMes" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<script type="text/javascript">
    var defaultMonth = "Jan";
    var currentMonthElement = null;
    function OnClick(s, e) {
        //s.GetMainElement().style.backgroundColor = "orange";
        currentMonthElement = s;
        var strMes = s.GetText().split('-');
        var strGest = lblYear.GetText();
        var fini = new Date(strGest, strMes[0] - 1, 1);
        detFechaIni.SetDate(fini);
        var ffin = new Date(strGest, strMes[0], 0);
        detFechaFin.SetDate(ffin);
        dde.SetText(lblYear.GetText() + "-" + strMes[1]);
        dde.HideDropDown();
    }
    function OnPrevClick(s, e) {
        lblYear.SetText(parseInt(lblYear.GetText()) - 1);
        currentMonthElement = null;
    }
    function OnNextClick(s, e) {
        lblYear.SetText(parseInt(lblYear.GetText()) + 1);
        currentMonthElement = null;
    }
    //function OnOkClick(s, e) {
    ////Cuando se active boton OK
    //    var month = !!currentMonthElement ? currentMonthElement.GetText() : defaultMonth;
    //    dde.SetText(lblYear.GetText() + "-" + month);
    //    dde.HideDropDown();
    //}
    </script>

<style type="text/css">
    .auto-style10 {
        height: 26px;
    }
    .auto-style11 {
        width: 168px;
        height: 26px;
    }
    .auto-style12 {
        width: 138px;
        height: 26px;
    }
    .panelPersona
    {
        position: fixed;
        top: 410px;
        width: 500px;
        background: white;
    }
    .hydro_smart_container
    {
        margin: 0;
        padding: 0;
        Height: 690px;
    }
    .hydro_smart_header
    {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }
    .hydro_smart_table
    {
        font-size: smaller;
        width: 95%;
        margin: auto;
    }
    .hydro_smart_info {
        /*border: #FFCE4F solid thin;*/
        border: #1d9915 solid thin;
        margin: 5px;
        /*background: #FFF8F0;*/
        background: #f0fff8;
        -ms-border-radius: 3px;
        border-radius: 3px;
    }
    .alert-box {
    color:#555;
    border-radius:10px;
    font-family:Tahoma,Geneva,Arial,sans-serif;font-size:11px;
    padding:10px 10px 10px 36px;
    margin:10px;
}
    
    .cell .buttonMonth
        {
            border-width: 1px;
            width: 50px;
            text-align: center;
            text-decoration:none;
            color:inherit;
        }
        .tab
        {
            width: 100%;
            border-color: #600;
            border-width: 0 0 1px 1px;
            border-style: solid;
        }
        .cell
        {
            border-color: #600;
            border-width: 1px 1px 0 0;
            border-style: solid;
            margin: 0;
            padding: 4px;
            background-color: #eee6a3;
        }
    
    </style>

<div>
    
    <table >
        <tr>
            <td>
                <dx:ASPxDropDownEdit runat="server" Width="140px" ClientInstanceName="dde" Theme="Metropolis" EnableTheming="True" ID="dde" OnInit="dde_Init">
                    <ClearButton Visibility="Auto"></ClearButton>
                </dx:ASPxDropDownEdit>
            </td>
        </tr>
    </table>

</div>





    
