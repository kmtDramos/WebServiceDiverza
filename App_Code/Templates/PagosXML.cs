using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PagosXML
{
    public static string XML(CPago Comprobante)
    {
        string xml =
            "<? xml version = \"1.0\" encoding = \"utf-8\" ?> " +
                "<cfdi:Comprobante " +
                    "xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " +
                    "xmlns:cfdi = \"http://www.sat.gob.mx/cfd/3\" " +
                    "xmlns:pago10 = \"http://www.sat.gob.mx/Pagos\" " +
                    "xsi:schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " +
                    "Version = \"3.3\" " +
                    "Serie = \"" + Comprobante.Serie + "\" " +
                    "Folio = \"\" " +
                    "Fecha = \"" + Comprobante.Fecha + "\" " +
                    "LugarExpedicion = \"" + Comprobante.LugarExpedicion + "\" " +
                    "Moneda = \"XXX\" " +
                    "TipoDeComprobante = \"" + Comprobante.TipoDeComprobante + "\" " +
                    "SubTotal = \"" + Comprobante.Subtotal + "\" " +
                    "Total = \"" + Comprobante.Total + "\" " +
                    "NoCertificado = \"" + Comprobante.NoCertificado + "\" " + // \"20001000000300022755\" " +
                    "Certificado = \"" + Comprobante.Certificado + "\" " +
                    "Sello = \"" + Comprobante.Sello + "\" > " +

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
                    "</cfdi:Conceptos > " +
                    
                    "<cfdi:Complemento > " +
                        "<pago10:Pagos Version = \"1.0\" > " +
                            "<pago10:Pago " +
                                "FechaPago = \"" + Comprobante.Complementos.FechaPago + "\" " +
                                "FormaDePagoP = \"" + Comprobante.Complementos.FormaDePagoP + "\" " +
                                "MonedaP = \"" + Comprobante.Complementos.MonedaP + "\" " +
                                "Monto = \"" + Comprobante.Complementos.Monto + "\" > " +

                                "RfcEmisorCtaOrd = \"" + Comprobante.Complementos.RfcEmisorCtaOrd + "\" > " +
                                "NomBancoOrdExt = \"" + Comprobante.Complementos.NomBancoOrdExt + "\" > " +
                                "CtaOrdenante = \"" + Comprobante.Complementos.CtaOrdenante + "\" > " +
                                "RfcEmisorCtaBen = \"" + Comprobante.Complementos.RfcEmisorCtaBen + "\" > " +
                                "CtaBeneficiario = \"" + Comprobante.Complementos.CtaBeneficiario + "\" > " +
                                "TipoCadPago = \"" + Comprobante.Complementos.TipoCadPago + "\" > " +
                                "CertPago = \"" + Comprobante.Complementos.CertPago + "\" > " +
                                "CadPago = \"" + Comprobante.Complementos.CadPago + "\" > " +
                                "SelloPago = \"" + Comprobante.Complementos.SelloPago + "\" > " +

                                "<pago10:DoctoRelacionado " +
                                    "IdDocumento = \"" + Comprobante.Complementos.DoctoRelacionados.IdDocumento + "\" " +
                                    "MonedaDR = \"" + Comprobante.Complementos.DoctoRelacionados.MonedaDR + "\" " +
                                    "MetodoDePagoDR = \"" + Comprobante.Complementos.DoctoRelacionados.MetodoDePagoDR + "\" " +
                                    "NumParcialidad = \"" + Comprobante.Complementos.DoctoRelacionados.NumParcialidad + "\" " +
                                    "ImpSaldoAnt = \"" + Comprobante.Complementos.DoctoRelacionados.ImpSaldoAnt + "\" " +
                                    "ImpPagado = \"" + Comprobante.Complementos.DoctoRelacionados.ImpPagado + "\" " +
                                    "ImpSaldoInsoluto = \"" + Comprobante.Complementos.DoctoRelacionados.ImpSaldoInsoluto + "\" /> " +
                            "</pago10:Pago > " +
                        "</pago10:Pagos > " +
                    "</cfdi:Complemento > " +
                "</cfdi:Comprobante > ";

        return xml;
    }
}