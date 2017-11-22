using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class XML
{
    public static string XMLNotasCredito(string data)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<cfdi:Comprobante " +
                            "xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " +
                            "xmlns:cfdi = \"http://www.sat.gob.mx/cfd/3\" " +
                            "xsi:schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " +

                            "Version = \"3.3\" " +
                            "Serie = \"\" " +
                            "Folio = \"090146344\" " +
                            "Fecha = \"2017-05-11T12:56:11\" " +
                            "LugarExpedicion = \"64102\" " +
                            "Moneda = \"MXN\" " +
                            "TipoCambio = \"1\" " +
                            "CondicionesDePago=\"Para la fecha de 18/07/2017\" " +

                            "FormaPago=\"01\" " +
                            "MetodoPago = \"PUE\" " +
                            "TipoDeComprobante = \"E\" " +

                            "SubTotal = \"25.00\" " +
                            "Total = \"29.00\" " +

                            "NoCertificado = \"20001000000300022755\" " +
                            "Certificado = \"\" " +
                            "Sello = \"\" >" +

                            "<cfdi:CfdiRelacionados " +
                                "TipoRelacion = \"01\" >" +
                                
                                "<cfdi:CfdiRelacionado " + 
                                    "UUID = \"00000000-0000-0000-0000-000000000000\" />" +

                            "</cfdi:CfdiRelacionados > " +

                            "<cfdi:Emisor " +
                                "Rfc = \"MAG041126GT8\" " +
                                "Nombre = \"GAsercom DEMO NOTA CREDITO\" " +
                                "RegimenFiscal = \"601\" />" +

                            "<cfdi:Receptor " +
                                "Rfc = \"PUUJ841226AF5\" " +
                                "Nombre = \"FERNANDO ESPINO\" " +
                                "UsoCFDI = \"G03\" />" +

                            "<cfdi:Conceptos >" +
                                "<cfdi:Concepto " +
                                    "ClaveProdServ = \"84111506\" " +
                                    "Cantidad = \"1\" " +
                                    "Unidad = \"ACT\" " +
                                    "ClaveUnidad = \"ACT\" " +
                                    "Descripcion = \"Servicios de Facturación\" " +
                                    "ValorUnitario = \"25.00\" " +
                                    "Importe = \"25.00\" >" +

                                    "<cfdi:Impuestos >" +
                                        "<cfdi:Traslados >" +
                                            "<cfdi:Traslado " +
                                                "Base = \"25.00\" " +
                                                "Impuesto = \"002\" " +
                                                "TipoFactor = \"Tasa\" " +
                                                "TasaOCuota = \"0.160000\" " +
                                                "Importe = \"4.00\" />" +
                                        "</cfdi:Traslados >" +
                                    "</cfdi:Impuestos >" +

                                "</cfdi:Concepto >" +
                            "</cfdi:Conceptos >" +

                            "<cfdi:Impuestos TotalImpuestosTrasladados = \"4.00\" >" +
                                "<cfdi:Traslados >" +
                                    "<cfdi:Traslado " +
                                        "Impuesto = \"002\" " +
                                        "TipoFactor = \"Tasa\" " +
                                        "TasaOCuota = \"0.160000\" " +
                                        "Importe = \"4.00\" />" +
                                "</cfdi:Traslados >" +
                            "</cfdi:Impuestos >" +
                        "</cfdi:Comprobante >";

        string encode = Base64Encode(xml);

        return encode;
    }


    public static string XMLPagos(string data)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<cfdi:Comprobante " +
                            "xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " +
                            "xmlns:cfdi = \"http://www.sat.gob.mx/cfd/3\" " +
                            "xmlns:pago10 = \"http://www.sat.gob.mx/Pagos\" " +
                            "xsi:schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " +

                            "Version = \"3.3\" " +
                            "Serie = \"\" " +
                            "Folio = \"090146344\" " +
                            "Fecha = \"2017-05-11T12:56:11\" " +
                            "LugarExpedicion = \"64102\" " +
                            
                            "TipoDeComprobante = \"P\" " +

                            "SubTotal = \"0\" " +
                            "Total = \"0\" " +

                            "NoCertificado = \"20001000000300022755\" " +
                            "Certificado = \"\" " +
                            "Sello = \"\" >" +

                            "<cfdi:Emisor " +
                                "Rfc = \"MAG041126GT8\" " +
                                "Nombre = \"GAsercom DEMO PAGOS\" " +
                                "RegimenFiscal = \"601\" />" +

                            "<cfdi:Receptor " +
                                "Rfc = \"PUUJ841226AF5\" " +
                                "Nombre = \"FERNANDO ESPINO\" " +
                                "UsoCFDI = \"G03\" />" +

                            "<cfdi:Conceptos >" +
                                "<cfdi:Concepto " +
                                    "ClaveProdServ = \"84111506\" " +
                                    "Cantidad = \"1\" " +
                                    "ClaveUnidad = \"ACT\" " +
                                    "Descripcion = \"Pago\" " +
                                    "ValorUnitario = \"0\" " +
                                    "Importe = \"0\" " +
                                "</cfdi:Concepto >" +
                            "</cfdi:Conceptos >" +

                            "<cfdi:Complemento >" +
                                "<pago10:Pagos Version = \"1.0\" >" +
                                    "<pago10:Pago " +
                                        "FechaPago = \"2017-03-22T09:00:00\" " +
                                        "FormaDePagoP = \"06\" " +
                                        "MonedaP = \"MXN\" " +
                                        "Monto = \"50.00\" " +
                                        "RfcEmisorCtaOrd = \"\" " +
                                        "NomBancoOrdExt = \"\" " +
                                        "CtaOrdenante = \"\" " +
                                        "RfcEmisorCtaBen = \"\" " + 
                                        "CtaBeneficiario = \"\" " +
                                        "TipoCadPago = \"\" " +
                                        "CertPago = \"\" " +
                                        "CadPago = \"\" " +
                                        "SelloPago = \"\" >" +

                                        "<pago10:DoctoRelacionado " +
                                            "IdDocumento = \"00000000-0000-0000-0000-000000000000\" " +
                                            "Serie = \"\" " +
                                            "Folio = \"\" " +
                                            "Moneda = \"MXN\" " +
                                            "MetodoDePagoDR = \"PPD\" " +
                                            "NumParcialidad = \"1\" " +
                                            "ImpSaldoAnt = \"0.0\" " +
                                            "ImpPagado = \"0.0\" " +
                                            "ImpSaldoInsoluto = \"0.0\" />" +

                                    "</pago10:Pago> " +
                                "</pago10:Pagos >" +
                            "</cfdi:Complemento >" +
                        "</cfdi:Comprobante >";


        string encode = Base64Encode(xml);

        return encode;
    }

    public static string XMLEmitir(string data)
    {
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<cfdi:Comprobante " +
                            "xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " +
                            "xmlns:cfdi = \"http://www.sat.gob.mx/cfd/3\" " +
                            "xsi:schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " +
                            
                            "Version = \"3.3\" " +
                            "Serie = \"\" "+
                            "Folio = \"090146344\" " +
                            "Fecha = \"2017-05-11T12:56:11\" " +
                            "LugarExpedicion = \"64102\" " +
                            "Moneda = \"MXN\" " +
                            "TipoCambio = \"1\" " +
                            "CondicionesDePago=\"Para la fecha de 18/07/2017\" " +

                            "FormaPago=\"03\" " +
                            "MetodoPago = \"PUE\" " +
                            "TipoDeComprobante = \"I\" " +

                            "SubTotal = \"2207.88\" " +
                            "Total = \"2561.14\" " +
                            "Descuento = \"0.0\" " +
                            
                            "NoCertificado = \"20001000000300022755\" " +
                            "Certificado = \"\" " +
                            "Sello = \"\" >" +

                            "<cfdi:Emisor " +
                                "Rfc = \"MAG041126GT8\" " +
                                "Nombre = \"GAsercom DEMO FACTURA\" " +
                                "RegimenFiscal = \"601\" />" +

                            "<cfdi:Receptor " +
                                "Rfc = \"PUUJ841226AF5\" " +
                                "Nombre = \"FERNANDO ESPINO\" " +
                                "UsoCFDI = \"G03\" />" +

                            "<cfdi:Conceptos >" +
                                "<cfdi:Concepto " +
                                    "ClaveProdServ = \"51102200\" " +
                                    "Cantidad = \"4\" " +
                                    "Unidad = \"Pieza\" " +
                                    "ClaveUnidad = \"3I\" " +
                                    "Descripcion = \"Apple Macbook Pro AG SCD DRS 9X10CM (1X10PK) US\" " +
                                    "ValorUnitario = \"551.97\" " +
                                    "Importe = \"2207.88\" " +
                                    "Descuento = \"0.0\" >" +

                                    "<cfdi:Impuestos >" +
                                        "<cfdi:Traslados >" +
                                            "<cfdi:Traslado " +
                                                "Base = \"2207.88\" " +
                                                "Impuesto = \"002\" " +
                                                "TipoFactor = \"Tasa\" " +
                                                "TasaOCuota = \"0.160000\" " +
                                                "Importe = \"353.26\" />" +
                                        "</cfdi:Traslados >" +
                                    "</cfdi:Impuestos >" +

                                "</cfdi:Concepto >" +
                            "</cfdi:Conceptos >" +

                            "<cfdi:Impuestos TotalImpuestosTrasladados = \"353.26\" >" +
                                "<cfdi:Traslados >" +
                                    "<cfdi:Traslado " +
                                        "Impuesto = \"002\" " +
                                        "TipoFactor = \"Tasa\" " +
                                        "TasaOCuota = \"0.160000\" " +
                                        "Importe = \"353.26\" />" +
                                "</cfdi:Traslados >" +
                            "</cfdi:Impuestos >" +
                        "</cfdi:Comprobante >";

        string encode = Base64Encode(xml);

        return encode;
    }

    //Encode Base64
    private static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    //Decode Base64
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}