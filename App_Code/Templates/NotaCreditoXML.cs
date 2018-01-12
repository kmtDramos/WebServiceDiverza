using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class NotaCreditoXML
{
    public static string XML(CNotaCredito Comprobante)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<cfdi:Comprobante " +
                            "xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " +
                            "xmlns:cfdi = \"http://www.sat.gob.mx/cfd/3\" " +
                            "xsi:schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " +

                            "Version = \"3.3\" " +
                            "Serie = \"" + Comprobante.Serie + "\" " +
                            "Folio = \"\" " +
                            "Fecha = \"" + Comprobante.Fecha + "\" " +
                            "LugarExpedicion = \"" + Comprobante.LugarExpedicion + "\" " +
                            "Moneda = \"" + Comprobante.Moneda + "\" " +
                            "TipoCambio = \"" + Comprobante.TipoCambio + "\" " +
                            "CondicionesDePago=\"" + Comprobante.CondicionDePago + "\" " +

                            "FormaPago=\"" +  Comprobante.FormaPago + "\" " +
                            "MetodoPago = \"" + Comprobante.MetodoPago + "\" " +
                            "TipoDeComprobante = \"" + Comprobante.TipoDeComprobante + "\" " +

                            "SubTotal = \"" + Comprobante.Subtotal + "\" " +
                            "Total = \"" + Comprobante.Total + "\" " +

                            "NoCertificado = \"" + Comprobante.NoCertificado + "\" " +
                            "Certificado = \"\" " +
                            "Sello = \"\" >" +

                            "<cfdi:CfdiRelacionados " +
                                "TipoRelacion = \"" + Comprobante.CfdiRelacionado.TipoRelacion + "\" >" +

                                "<cfdi:CfdiRelacionado " +
                                    "UUID = \"" + Comprobante.CfdiRelacionado.UUID + "\" />" +

                            "</cfdi:CfdiRelacionados > " +

                            "<cfdi:Emisor " +
                                "Rfc = \"" + Comprobante.Emisor.RFC + "\" " +
                                "Nombre = \"" + Comprobante.Emisor.Nombre + "\" " +
                                "RegimenFiscal = \"" + Comprobante.Emisor.RegimenFiscal + "\" /> " +
                            "<cfdi:Receptor " +
                                "Rfc = \"" + Comprobante.Receptor.RFC + "\" " +
                                "Nombre = \"" + Comprobante.Receptor.Nombre + "\" " +
                                "UsoCFDI = \"" + Comprobante.Receptor.UsoCFDI + "\" /> " +

                            "<cfdi:Conceptos > " +
                                "<cfdi:Concepto " +
                                    "ClaveProdServ = \"" + Comprobante.Concepto.ClaveProdServ + "\" " +
                                    "Cantidad = \"" + Comprobante.Concepto.Cantidad + "\" " +
                                    "ClaveUnidad = \"" + Comprobante.Concepto.ClaveUnidad + "\" " +
                                    "Descripcion = \"" + Comprobante.Concepto.Descripcion + "\" " +
                                    "ValorUnitario = \"" + Comprobante.Concepto.ValorUnitario + "\" " +
                                    "Importe = \"" + Comprobante.Concepto.Importe + "\" /> " +
                                    
                                    "<cfdi:Impuestos > " + 
                                        "<cfdi:Traslados>" + 
                                            "<cfdi:Traslado " + 
                                                "Base=\"" + Comprobante.Concepto.Impuestos.Traslados[0].Traslado.Contenido.Base.ToString("0.##") + "\"" + 
                                                "TipoFactor=\"" + Comprobante.Concepto.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\"" + 
                                                "TasaOCuota=\"" + "0.160000" + "\"" + 
                                                "Impuesto=\"" + Comprobante.Concepto.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\"" + 
                                                "Importe=\"" + Comprobante.Concepto.Impuestos.Traslados[0].Traslado.Contenido.Importe.ToString("0.##") + "\"/>" + 
                                        "</cfdi:Traslados>" + 
                                    "</cfdi:Impuestos>" +
                                "</cfdi:Concepto >" +
                            "</cfdi:Conceptos >" +

                            "<cfdi:Impuestos TotalImpuestosTrasladados = \"" + Comprobante.Impuestos.TotalImpuestosTraslados + "\" >" +
                                "<cfdi:Traslados >" +
                                    "<cfdi:Traslado " +
                                        "Impuesto = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\" " +
                                        "TipoFactor = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\" " +
                                        "TasaOCuota = \"0.160000\" " +
                                        "Importe = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Importe + "\" />" +
                                "</cfdi:Traslados >" +
                            "</cfdi:Impuestos >" +
                        "</cfdi:Comprobante >";

        return xml;
    }
}