using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PagosXML
{
    public static string XML(CPago Comprobante)
    {
        string xml =
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + System.Environment.NewLine +
        "<cfdi:Comprobante" + System.Environment.NewLine +
        "    Certificado=\"\"" + System.Environment.NewLine +
        "    NoCertificado=\"" + Comprobante.NoCertificado + "\"" + System.Environment.NewLine +
        "    Sello=\"" + Comprobante.Sello + "\"" + System.Environment.NewLine +
        "    Fecha=\"" + Comprobante.Fecha + "\"" + System.Environment.NewLine +
        "    Folio=\"" + Comprobante.Folio + "\"" + System.Environment.NewLine +
        "    Serie=\"" + Comprobante.Serie + "\"" + System.Environment.NewLine +
        "    Version=\"3.3\"" + System.Environment.NewLine +
        "    LugarExpedicion=\"" + Comprobante.LugarExpedicion + "\"" + System.Environment.NewLine +
        "    TipoDeComprobante=\"" + Comprobante.TipoDeComprobante + "\"" + System.Environment.NewLine +
        "    Total=\"" + Comprobante.Total.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    SubTotal=\"" + Comprobante.Subtotal.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    Moneda=\"" + Comprobante.Moneda + "\"" + System.Environment.NewLine +
        "    xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\"" + System.Environment.NewLine +
        "    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" + System.Environment.NewLine +
        "    xmlns:pago10 = \"http://www.sat.gob.mx/Pagos\"" + System.Environment.NewLine +
        "    xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd\" > " + System.Environment.NewLine +
        "    <cfdi:Emisor" + System.Environment.NewLine +
        "        Nombre=\"" + Comprobante.Emisor.Nombre + "\"" + System.Environment.NewLine +
        "        Rfc=\"" + Comprobante.Emisor.RFC + "\"" + System.Environment.NewLine +
        "        RegimenFiscal=\"" + Comprobante.Emisor.RegimenFiscal + "\"/>" + System.Environment.NewLine +
        "    <cfdi:Receptor" + System.Environment.NewLine +
        "        Nombre=\"" + Comprobante.Receptor.Nombre + "\"" + System.Environment.NewLine +
        "        Rfc=\"" + Comprobante.Receptor.RFC + "\"" + System.Environment.NewLine +
        "        UsoCFDI=\"" + Comprobante.Receptor.UsoCFDI + "\"/>" + System.Environment.NewLine +

        "    <cfdi:Conceptos>" + System.Environment.NewLine +
        "        <cfdi:Concepto" + System.Environment.NewLine +
        "            Importe=\"" + Comprobante.Concepto.Importe.ToString("0.##") + "\"" + System.Environment.NewLine +
        "            ValorUnitario=\"" + Comprobante.Concepto.ValorUnitario.ToString("0.##") + "\"" + System.Environment.NewLine +
        "            Descripcion=\"" + Comprobante.Concepto.Descripcion + "\"" + System.Environment.NewLine +
        "            ClaveUnidad=\"" + Comprobante.Concepto.ClaveUnidad + "\"" + System.Environment.NewLine +
        "            Cantidad=\"" + Comprobante.Concepto.Cantidad.ToString("0.######") + "\"" + System.Environment.NewLine +
        "            ClaveProdServ=\"" + Comprobante.Concepto.ClaveProdServ + "\" />" + System.Environment.NewLine +
        "    </cfdi:Conceptos>" + System.Environment.NewLine +

        "    <cfdi:Complemento > " + System.Environment.NewLine +
        "        <pago10:Pagos Version = \"1.0\" > " + System.Environment.NewLine +
        "            <pago10:Pago " + System.Environment.NewLine +
        "                FechaPago = \"" + Comprobante.Complementos.FechaPago + "\" " + System.Environment.NewLine +
        "                FormaDePagoP = \"" + Comprobante.Complementos.FormaDePagoP + "\" " + System.Environment.NewLine +
        "                MonedaP = \"" + Comprobante.Complementos.MonedaP + "\" " + System.Environment.NewLine +
        "                Monto = \"" + Comprobante.Complementos.Monto + "\" " + System.Environment.NewLine;
        if (Comprobante.Complementos.NumOperacion != "")
        {
            xml +=
            "            NumOperacion = \"" + Comprobante.Complementos.NumOperacion + "\" " + System.Environment.NewLine;
        }
        if (Comprobante.Complementos.TipoCambioP != "")
        {
            xml +=
            "           TipoCambioP = \"" + Comprobante.Complementos.TipoCambioP + "\" " + System.Environment.NewLine;
        }
        if (Comprobante.Complementos.RfcEmisorCtaOrd != "") {
            xml +=
            "           RfcEmisorCtaOrd = \"" + Comprobante.Complementos.RfcEmisorCtaOrd + "\" " + System.Environment.NewLine +
            "           CtaOrdenante = \"" + Comprobante.Complementos.CtaOrdenante + "\" " + System.Environment.NewLine;
        }
        if (Comprobante.Complementos.NomBancoOrdExt != "") {
            xml +=
            "           NomBancoOrdExt = \"" + Comprobante.Complementos.NomBancoOrdExt + "\" " + System.Environment.NewLine;
        }
        if (Comprobante.Complementos.RfcEmisorCtaBen != "") {
            xml +=
            "           RfcEmisorCtaBen = \"" + Comprobante.Complementos.RfcEmisorCtaBen + "\" " + System.Environment.NewLine +
            "           CtaBeneficiario = \"" + Comprobante.Complementos.CtaBeneficiario + "\" ";
        }
        //"                NomBancoOrdExt = \"" + Comprobante.Complementos.NomBancoOrdExt + "\" " + System.Environment.NewLine +
        //"                TipoCadPago = \"" + Comprobante.Complementos.TipoCadPago + "\" " + System.Environment.NewLine +
        //"                CertPago = \"" + Comprobante.Complementos.CertPago + "\" " + System.Environment.NewLine +
        //"                CadPago = \"" + Comprobante.Complementos.CadPago + "\" " + System.Environment.NewLine +
        //"                SelloPago = \"" + Comprobante.Complementos.SelloPago + "\" " + System.Environment.NewLine +
        xml +=
        "> " + System.Environment.NewLine;
        foreach (CDoctoRelacionado Documento in Comprobante.Complementos.DoctoRelacionados)
        {
            xml +=
            "               <pago10:DoctoRelacionado " + System.Environment.NewLine +
            "                   IdDocumento = \"" + Documento.IdDocumento + "\" " + System.Environment.NewLine +
            "                   Serie = \"" + Documento.Serie + "\" " + System.Environment.NewLine +
            "                   Folio = \"" + Documento.Folio + "\" " + System.Environment.NewLine +
            "                   MonedaDR = \"" + Documento.MonedaDR + "\" " + System.Environment.NewLine;
            if (Documento.TipoCambioDR != "")
            {
                xml +=
                "               TipoCambioDR = \"" + Documento.TipoCambioDR + "\" " + System.Environment.NewLine;
            }
            xml +=
            "                   MetodoDePagoDR = \"" + Documento.MetodoDePagoDR + "\" " + System.Environment.NewLine +
            "                   NumParcialidad = \"" + Documento.NumParcialidad + "\" " + System.Environment.NewLine;
            if (Documento.MetodoDePagoDR != "PUE")
            {
                xml +=
                "                   ImpSaldoAnt = \"" + Documento.ImpSaldoAnt + "\" " + System.Environment.NewLine;
            }
            xml += 
            "                   ImpPagado = \"" + Documento.ImpPagado + "\" " + System.Environment.NewLine;
            if (Documento.MetodoDePagoDR != "PUE")
            {
                xml +=
                "               ImpSaldoInsoluto = \"" + Documento.ImpSaldoInsoluto + "\" ";
            }
            xml +=
            " /> " + System.Environment.NewLine;
        }
        xml +=
        "           </pago10:Pago > " + System.Environment.NewLine +
        "        </pago10:Pagos > " + System.Environment.NewLine +
        "    </cfdi:Complemento > " + System.Environment.NewLine +
        "</cfdi:Comprobante>";

        return xml;
    }
}