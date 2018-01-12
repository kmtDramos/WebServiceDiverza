using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FacturacionXML
{

	public static string XML (CComprobante Comprobante)
	{
        string xml =
        "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + System.Environment.NewLine +
        "<cfdi:Comprobante" + System.Environment.NewLine +
        "    Certificado=\"\"" + System.Environment.NewLine +
        "    NoCertificado=\"" + Comprobante.NoCertificado + "\"" + System.Environment.NewLine +
        "    FormaPago=\"" + Comprobante.FormaPago + "\"" + System.Environment.NewLine +
        "    Sello=\"" + Comprobante.Sello + "\"" + System.Environment.NewLine +
        "    Fecha=\"" + Comprobante.Fecha + "\"" + System.Environment.NewLine +
        "    Folio=\"" + Comprobante.Folio + "\"" + System.Environment.NewLine +
        "    Serie=\"" + Comprobante.Serie + "\"" + System.Environment.NewLine +
        "    Version=\"3.3\"" + System.Environment.NewLine +
        "    LugarExpedicion=\"" + Comprobante.LugarExpedicion + "\"" + System.Environment.NewLine +
        "    CondicionesDePago=\"" + Comprobante.CondicionDePago + "\"" + System.Environment.NewLine +
        "    MetodoPago=\"" + Comprobante.MetodoPago + "\"" + System.Environment.NewLine +
        "    TipoDeComprobante=\"" + Comprobante.TipoDeComprobante + "\"" + System.Environment.NewLine +
        "    Total=\"" + Comprobante.Total.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    SubTotal=\"" + Comprobante.Subtotal.ToString("0.##") + "\"" + System.Environment.NewLine;
        if (Comprobante.Descuento != 0)
        {
            xml +=
            "    Descuento=\"" + Comprobante.Descuento + "\"" + System.Environment.NewLine;
        }
        xml +=
        "    Moneda=\"" + Comprobante.Moneda + "\"" + System.Environment.NewLine +
        "    TipoCambio=\"" + Comprobante.TipoCambio.ToString("0.##") + "\"" + System.Environment.NewLine +
        "    xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\"" + System.Environment.NewLine +
        "    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"" + System.Environment.NewLine +
        "    xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\">" + System.Environment.NewLine;

        if(Comprobante.CFDIRelacionado.TipoRelacion != "")
        {
            xml +=
            "   <cfdi:CfdiRelacionados " + System.Environment.NewLine +
            "       TipoRelacion = \"" + Comprobante.CFDIRelacionado.TipoRelacion + "\" >" + System.Environment.NewLine +
            "       <cfdi:CfdiRelacionado UUID = \"" + Comprobante.CFDIRelacionado.UUID + "\" /> " + System.Environment.NewLine +
            "   </cfdi:CfdiRelacionados> " + System.Environment.NewLine;
        }

        xml +=
		"    <cfdi:Emisor" + System.Environment.NewLine +
		"        Nombre=\"" + Comprobante.Emisor.Nombre + "\"" + System.Environment.NewLine +
		"        Rfc=\"" + Comprobante.Emisor.RFC + "\"" + System.Environment.NewLine +
		"        RegimenFiscal=\"" + Comprobante.Emisor.RegimenFiscal + "\"/>" + System.Environment.NewLine +
		"    <cfdi:Receptor" + System.Environment.NewLine +
		"        Nombre=\"" + Comprobante.Receptor.Nombre + "\"" + System.Environment.NewLine +
		"        Rfc=\"" + Comprobante.Receptor.RFC + "\"" + System.Environment.NewLine +
		"        UsoCFDI=\"" + Comprobante.Receptor.UsoCFDI + "\"/>" + System.Environment.NewLine +
		"    <cfdi:Conceptos>" + System.Environment.NewLine;

        foreach (CConcepto Concepto in Comprobante.Conceptos)
        {
            xml +=
            "        <cfdi:Concepto" + System.Environment.NewLine +
            "            Importe=\"" + Concepto.Importe + "\"" + System.Environment.NewLine +
            "            ValorUnitario=\"" + Concepto.ValorUnitario.ToString("0.##") + "\"" + System.Environment.NewLine +
            "            Descripcion=\"" + Concepto.Descripcion + "\"" + System.Environment.NewLine +
            "            Unidad=\"" + Concepto.Unidad + "\"" + System.Environment.NewLine +
            "            ClaveUnidad=\"" + Concepto.ClaveUnidad + "\"" + System.Environment.NewLine +
            "            Cantidad=\"" + Concepto.Cantidad.ToString("0.######") + "\"" + System.Environment.NewLine;

            if (Concepto.Descuento != 0)
            {
                xml +=
                "            Descuento=\"" + Concepto.Descuento + "\"" + System.Environment.NewLine;
            }

            xml +=
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
        if (Comprobante.Impuestos.TotalImpuestosTraslados != 0)
        {
            xml +=
            "    <cfdi:Impuestos TotalImpuestosTrasladados=\"" + Comprobante.Impuestos.TotalImpuestosTraslados + "\">" + System.Environment.NewLine +
            "        <cfdi:Traslados>" + System.Environment.NewLine +
            "            <cfdi:Traslado" + System.Environment.NewLine +
            "                Importe=\"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Importe.ToString("0.##") + "\"" + System.Environment.NewLine +
            "                TipoFactor=\"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\"" + System.Environment.NewLine +
            "                TasaOCuota=\"" + "0.160000" + "\"" + System.Environment.NewLine +
            "                Impuesto=\"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\"/>" + System.Environment.NewLine +
            "        </cfdi:Traslados>" + System.Environment.NewLine +
            "    </cfdi:Impuestos>" + System.Environment.NewLine;
        }
        xml +=
        "</cfdi:Comprobante>";

        return xml;
	}

}