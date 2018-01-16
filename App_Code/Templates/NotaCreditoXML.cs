using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class NotaCreditoXML
{
    public static string XML(CNotaCredito NotaCredito)
    {
        string xml =
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + System.Environment.NewLine +
        "<cfdi:Comprobante" + System.Environment.NewLine +
        "    Certificado=\"\"" + System.Environment.NewLine +
        "    NoCertificado=\"" + NotaCredito.NoCertificado + "\"" + System.Environment.NewLine +
        "    FormaPago=\"" + NotaCredito.FormaPago + "\"" + System.Environment.NewLine +
        "    Sello=\"" + NotaCredito.Sello + "\"" + System.Environment.NewLine +
        "    Fecha=\"" + NotaCredito.Fecha + "\"" + System.Environment.NewLine +
        "    Folio=\"" + NotaCredito.Folio + "\"" + System.Environment.NewLine +
        "    Serie=\"" + NotaCredito.Serie + "\"" + System.Environment.NewLine +
        "    Version=\"3.3\"" + System.Environment.NewLine +
        "    LugarExpedicion=\"" + NotaCredito.LugarExpedicion + "\"" + System.Environment.NewLine +
        "    CondicionesDePago=\"" + NotaCredito.CondicionDePago + "\"" + System.Environment.NewLine +
        "    MetodoPago=\"" + NotaCredito.MetodoPago + "\"" + System.Environment.NewLine +
        "    TipoDeComprobante=\"" + NotaCredito.TipoDeComprobante + "\"" + System.Environment.NewLine +
        "    Total=\"" + NotaCredito.Total.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    SubTotal=\"" + NotaCredito.Subtotal.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    Moneda=\"" + NotaCredito.Moneda + "\"" + System.Environment.NewLine +
        "    TipoCambio=\"" + NotaCredito.TipoCambio.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\"" + System.Environment.NewLine +
        "    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" + System.Environment.NewLine +
        "    xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\">" + System.Environment.NewLine;

        foreach (CCfdiRelacionado cfdiRelacionado in NotaCredito.CfdisRelacionados)
        {
            xml +=
            "   <cfdi:CfdiRelacionados " + System.Environment.NewLine +
            "       TipoRelacion = \"" + cfdiRelacionado.TipoRelacion + "\" >" + System.Environment.NewLine +
            "       <cfdi:CfdiRelacionado UUID = \"" + cfdiRelacionado.UUID + "\" /> " + System.Environment.NewLine +
            "   </cfdi:CfdiRelacionados> " + System.Environment.NewLine;
        }

        xml +=
        "    <cfdi:Emisor" + System.Environment.NewLine +
        "        Nombre=\"" + NotaCredito.Emisor.Nombre + "\"" + System.Environment.NewLine +
        "        Rfc=\"" + NotaCredito.Emisor.RFC + "\"" + System.Environment.NewLine +
        "        RegimenFiscal=\"" + NotaCredito.Emisor.RegimenFiscal + "\"/>" + System.Environment.NewLine +
        "    <cfdi:Receptor" + System.Environment.NewLine +
        "        Nombre=\"" + NotaCredito.Receptor.Nombre + "\"" + System.Environment.NewLine +
        "        Rfc=\"" + NotaCredito.Receptor.RFC + "\"" + System.Environment.NewLine +
        "        UsoCFDI=\"" + NotaCredito.Receptor.UsoCFDI + "\"/>" + System.Environment.NewLine +
        "    <cfdi:Conceptos>" + System.Environment.NewLine;

        foreach (CConceptoNotaCredito Concepto in NotaCredito.Conceptos)
        {
            xml +=
            "        <cfdi:Concepto" + System.Environment.NewLine +
            "            Importe=\"" + Concepto.Importe + "\"" + System.Environment.NewLine +
            "            ValorUnitario=\"" + Concepto.ValorUnitario.ToString("0.##") + "\"" + System.Environment.NewLine +
            "            Descripcion=\"" + Concepto.Descripcion + "\"" + System.Environment.NewLine +
            "            ClaveUnidad=\"" + Concepto.ClaveUnidad + "\"" + System.Environment.NewLine +
            "            Cantidad=\"" + Concepto.Cantidad.ToString("0.######") + "\"" + System.Environment.NewLine +
            "            ClaveProdServ=\"" + Concepto.ClaveProdServ + "\">" + System.Environment.NewLine;
            
            if (Concepto.Impuestos.Traslados[0].Traslado.Contenido.Importe != 0)
            {
                xml +=
                "            <cfdi:Impuestos>" + System.Environment.NewLine +
                "                <cfdi:Traslados>" + System.Environment.NewLine +
                "                    <cfdi:Traslado Base=\"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Base.ToString("0.##") + "\"" + System.Environment.NewLine +
                "                        TipoFactor=\"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\"" + System.Environment.NewLine +
                "                        TasaOCuota=\"" + "0.160000" + "\"" + System.Environment.NewLine +
                "                        Impuesto=\"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\"" + System.Environment.NewLine +
                "                        Importe=\"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Importe.ToString("0.##") + "\"/>" + System.Environment.NewLine +
                "                </cfdi:Traslados>" + System.Environment.NewLine +
                "            </cfdi:Impuestos>" + System.Environment.NewLine;
            }
            xml +=
            "        </cfdi:Concepto>" + System.Environment.NewLine;
        }

        xml +=
        "    </cfdi:Conceptos>" + System.Environment.NewLine;
        if (NotaCredito.Impuestos.TotalImpuestosTraslados != 0)
        {
            xml +=
            "    <cfdi:Impuestos TotalImpuestosTrasladados=\"" + NotaCredito.Impuestos.TotalImpuestosTraslados + "\">" + System.Environment.NewLine +
            "        <cfdi:Traslados>" + System.Environment.NewLine +
            "            <cfdi:Traslado" + System.Environment.NewLine +
            "                Importe=\"" + NotaCredito.Impuestos.Traslados[0].Traslado.Contenido.Importe.ToString("0.##") + "\"" + System.Environment.NewLine +
            "                TipoFactor=\"" + NotaCredito.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\"" + System.Environment.NewLine +
            "                TasaOCuota=\"" + "0.160000" + "\"" + System.Environment.NewLine +
            "                Impuesto=\"" + NotaCredito.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\"/>" + System.Environment.NewLine +
            "        </cfdi:Traslados>" + System.Environment.NewLine +
            "    </cfdi:Impuestos>" + System.Environment.NewLine;
        }
        xml +=
        "</cfdi:Comprobante>";

        return xml;
    }
}