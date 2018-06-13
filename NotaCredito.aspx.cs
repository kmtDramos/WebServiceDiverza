using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.IO.Compression;

public partial class NotaCredito : System.Web.UI.Page
{

    [WebMethod]
    public static string TimbrarNotaCredito(string Id, string Token, Dictionary<string, object> NotaCredito, string RFC, string RefID, string NoCertificado, string Formato, string Correos, string RutaCFDI)
    {
        JObject Respuesta = new JObject();
        string Xml = "";
        CNotaCredito notaCredito = new CNotaCredito(); ;

        try { notaCredito = GenerarNotaCredito(NotaCredito); }
        catch (Exception ex)
        { }

        Xml = NotaCreditoXML.XML(notaCredito);
        
        // Save file XML in
        System.IO.Directory.CreateDirectory(@"" + RutaCFDI + @"\NotaCredito\in\" + RFC);
        System.IO.File.WriteAllText(@"" + RutaCFDI + @"\NotaCredito\in\" + RFC + @"\" + RefID + ".xml", Xml);
        //return Xml;
        string encode = Base64.Encode(Xml);

        JObject Request = new JObject();
        JObject Credenciales = new JObject();
        JObject Issuer = new JObject();
        JObject Receivers = new JObject();
        JObject Document = new JObject();

        Credenciales.Add("id", Id);
        Credenciales.Add("token", Token);

        Issuer.Add("rfc", RFC);

        Document.Add("ref-id", RefID);
        Document.Add("certificate-number", NoCertificado);
        Document.Add("section", "all");
        Document.Add("format", Formato);
        Document.Add("template", "letter");
        Document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
        Document.Add("content", encode);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("receiver", Conector.ObtenerDestinatarios(Correos.Split(',').ToList()));
        Request.Add("document", Document);
        string response = Conector.NotaCredito(Request);
        //return response.ToString();
        Dictionary<string, object> Response = (Dictionary<string, object>)JSON.Parse(response);
        //return Response.ToString();
        string uuid = "";
        string ref_id = "";
        string content = "";
        string message = "Error en el timbrado";
        int Error = 0;

        if (!Response.ContainsKey("message"))
        {

            try
            {
                message = "";

                uuid = (Response.ContainsKey("uuid")) ? (string)Response["uuid"] : "";
                ref_id = (Response.ContainsKey("uuid")) ? (string)Response["ref_id"] : "";
                content = (Response.ContainsKey("content")) ? (string)Response["content"] : "";

                // Save file ZIP out
                GuardarContenido(RutaCFDI, content, notaCredito.Receptor.RFC, notaCredito.Serie, notaCredito.Folio);

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Error = 1;
            }
        }
        else
        {
            message = (string)Response["message"];
            Error = 1;
        }

        Respuesta.Add("Error", Error);
        Respuesta.Add("message", message);
        Respuesta.Add("uuid", uuid);
        Respuesta.Add("ref_id", ref_id);
        Respuesta.Add("certificado", NoCertificado);
        Respuesta.Add("content", content);
        Respuesta.Add("request", Request);
        Respuesta.Add("response", response);

        return Respuesta.ToString();
    }

    private static CNotaCredito GenerarNotaCredito(Dictionary<string, object> NotaCredito)
    {

        CNotaCredito notaCredito = new CNotaCredito();

        notaCredito.Serie = Convert.ToString(NotaCredito["Serie"]);
        notaCredito.Folio = Convert.ToString(NotaCredito["Folio"]);
        notaCredito.Fecha = NotaCredito["Fecha"].ToString();
        notaCredito.FormaPago = NotaCredito["FormaPago"].ToString();
        notaCredito.CondicionDePago = Convert.ToString(NotaCredito["CondicionDePago"]);
        notaCredito.NoCertificado = Convert.ToString(NotaCredito["NoCertificado"]);
        notaCredito.Certificado = Convert.ToString(NotaCredito["Certificado"]);
        notaCredito.Subtotal = Convert.ToDecimal(NotaCredito["SubTotal"]);
        notaCredito.TipoCambio = Convert.ToDecimal(NotaCredito["TipoCambio"]);
        notaCredito.Moneda = Convert.ToString(NotaCredito["Moneda"]);
        notaCredito.Total = Convert.ToDecimal(NotaCredito["Total"]);
        notaCredito.TipoDeComprobante = Convert.ToString(NotaCredito["TipoDeComprobante"]);
        notaCredito.MetodoPago = Convert.ToString(NotaCredito["MetodoPago"]);
        notaCredito.LugarExpedicion = Convert.ToString(NotaCredito["LugarExpedicion"]);
        notaCredito.Sello = Convert.ToString(NotaCredito["Sello"]);

        //Dictionary<string, object> CFDIRelacionado = (Dictionary<string, object>)NotaCredito["CFDIRelacionado"];
        //notaCredito.CfdiRelacionado.TipoRelacion = Convert.ToString(CFDIRelacionado["TipoRelacion"]);
        //notaCredito.CfdiRelacionado.UUID = Convert.ToString(CFDIRelacionado["UUID"]);

        Object[] CfdisRelacionados = (Object[])NotaCredito["CFDISRelacionados"];
        if (CfdisRelacionados.Length > 0)
        {
            int i = 0;
            CCfdiRelacionado[] cfdisRelacionados = new CCfdiRelacionado[CfdisRelacionados.Length];

            foreach(Dictionary<string, object> cfdiRelacionado in CfdisRelacionados)
            {
                cfdisRelacionados[i] = new CCfdiRelacionado
                {
                    TipoRelacion = Convert.ToString(cfdiRelacionado["TipoRelacion"]),
                    UUID = Convert.ToString(cfdiRelacionado["UUID"])
                };

                cfdisRelacionados[i].TipoRelacion = Convert.ToString(cfdiRelacionado["TipoRelacion"]);
                cfdisRelacionados[i].UUID = Convert.ToString(cfdiRelacionado["UUID"]);

                notaCredito.TipoRelacion = Convert.ToString(cfdiRelacionado["TipoRelacion"]);

                i++;
            }
            notaCredito.CfdisRelacionados = cfdisRelacionados;
        }
        else
        {

        }

        Dictionary<string, object> Emisor = (Dictionary<string, object>)NotaCredito["Emisor"];
        Dictionary<string, object> Receptor = (Dictionary<string, object>)NotaCredito["Receptor"];

        notaCredito.Emisor.Nombre = Convert.ToString(Emisor["Nombre"]);
        notaCredito.Emisor.RFC = Convert.ToString(Emisor["RFC"]);
        notaCredito.Emisor.RegimenFiscal = Convert.ToString(Emisor["RegimenFiscal"]);

        notaCredito.Receptor.Nombre = Convert.ToString(Receptor["Nombre"]);
        notaCredito.Receptor.RFC = Convert.ToString(Receptor["RFC"]);
        notaCredito.Receptor.UsoCFDI = Convert.ToString(Receptor["UsoCFDI"]);

        Object[] Conceptos = (Object[])NotaCredito["Conceptos"];

        if (Conceptos.Length > 0)
        {
            int i = 0;
            CConceptoNotaCredito[] conceptos = new CConceptoNotaCredito[Conceptos.Length];

            foreach (Dictionary<string, object> Concepto in Conceptos)
            {
                Dictionary<string, object> Impuestos = (Dictionary<string, object>)Concepto["Impuestos"];
                Object[] Traslados = (Object[])Impuestos["Traslados"];

                CImpuestoConceptoNotaCredito impuestos = new CImpuestoConceptoNotaCredito();
                CTrasladosConcepto[] traslados = new CTrasladosConcepto[Traslados.Length];

                int j = 0;
                foreach (Dictionary<string, object> Traslado in Traslados)
                {
                    Dictionary<string, object> Contenido = (Dictionary<string, object>)Traslado["Traslado"];
                    CTrasladoConcepto traslado = new CTrasladoConcepto();
                    CTrasladoConceptoContenido contenido = new CTrasladoConceptoContenido();
                    contenido.Base = Convert.ToDecimal(Contenido["Base"]);
                    contenido.TipoFactor = Convert.ToString(Contenido["TipoFactor"]);
                    contenido.TasaOCuota = Convert.ToDecimal(Contenido["TasaOCuota"]);
                    contenido.Impuesto = Convert.ToString(Contenido["Impuesto"]);
                    contenido.Importe = Convert.ToDecimal(Contenido["Importe"]);
                    traslado.Contenido = contenido;
                    traslados[j] = new CTrasladosConcepto
                    {
                        Traslado = traslado
                    };
                }

                impuestos.Traslados = traslados;

                conceptos[i] = new CConceptoNotaCredito
                {
                    Importe = Convert.ToString(Concepto["Importe"]),
                    ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]),
                    Descripcion = Convert.ToString(Concepto["Descripcion"]),
                    Cantidad = Convert.ToDecimal(Concepto["Cantidad"]),
                    ClaveUnidad = Convert.ToString(Concepto["ClaveUnidad"]),
                    ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]),
                    Impuestos = impuestos
                };

                conceptos[i].Importe = Convert.ToString(Concepto["Importe"]);
                conceptos[i].ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]);
                conceptos[i].Descripcion = Convert.ToString(Concepto["Descripcion"]);
                conceptos[i].Cantidad = Convert.ToDecimal(Concepto["Cantidad"]);
                conceptos[i].ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]);

                i++;
            }
            notaCredito.Conceptos = conceptos;
        }
        else
        {

        }

        Dictionary<string, object> ImpuestosGlobales = (Dictionary<string, object>)NotaCredito["Impuestos"];
        Object[] TrasladosGlobales = (Object[])ImpuestosGlobales["Traslados"];

        notaCredito.Impuestos.TotalImpuestosTraslados = Convert.ToDecimal(ImpuestosGlobales["TotalImpuestosTrasladados"]);

        CTrasladosComprobante[] trasladosglobales = new CTrasladosComprobante[TrasladosGlobales.Length];

        int h = 0;
        foreach (Dictionary<string, object> TrasladoGlobal in TrasladosGlobales)
        {
            Dictionary<string, object> ContenidoTrasladoGlobal = (Dictionary<string, object>)TrasladoGlobal["Traslado"];
            CTrasladoComprobante traslado = new CTrasladoComprobante();
            CTrasladoComprobanteContenido contenido = new CTrasladoComprobanteContenido();
            contenido.Importe = Convert.ToDecimal(ContenidoTrasladoGlobal["Importe"]);
            contenido.Impuesto = Convert.ToString(ContenidoTrasladoGlobal["Impuesto"]);
            contenido.TasaOCuota = Convert.ToDecimal(ContenidoTrasladoGlobal["TasaOCuota"]);
            contenido.TipoFactor = Convert.ToString(ContenidoTrasladoGlobal["TipoFactor"]);
            traslado.Contenido = contenido;
            trasladosglobales[h] = new CTrasladosComprobante { Traslado = traslado };
        }

        notaCredito.Impuestos.Traslados = trasladosglobales;

        return notaCredito;
    }

    private static void GuardarContenido(string RutaCFDI, string Contenido, string RFC, string Serie, string Folio)
    {
        string RFCCliente = RFC.Replace("&amp;", "&");
        string nameFile = Serie + Folio;
        System.IO.Directory.CreateDirectory(@"" + RutaCFDI + @"\NotaCredito\out\" + RFCCliente);
        System.IO.File.WriteAllBytes(@"" + RutaCFDI + @"\NotaCredito\out\" + RFCCliente + @"\" + nameFile + ".zip", Decode(Contenido));

        //Descomprimir zip
        string zipPath = @"" + RutaCFDI + @"\NotaCredito\out\" + RFCCliente + @"\" + nameFile + ".zip";
        string extractPath = @"" + RutaCFDI + @"\NotaCredito\out\" + RFCCliente;

        ZipArchive archive = ZipFile.OpenRead(zipPath);
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    entry.ExtractToFile(Path.Combine(extractPath, nameFile + ".xml"));
                }

                if (entry.FullName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    entry.ExtractToFile(Path.Combine(extractPath, nameFile + ".pdf"));
                }
            }
        }
    }

    private static byte[] Decode(string Hash)
    {
        byte[] bytes = System.Convert.FromBase64String(Hash);
        return bytes; //System.Text.Encoding.UTF8.GetString(bytes);
    }

    [WebMethod]
    public static string CancelarNotaCredito(string Id, string Token, string UUID, string RefID, string RFC, string NoCertificado, string MotivoCancelacion)
    {
        JObject Respuesta = new JObject();
        JObject Request = new JObject();
        JObject Credenciales = new JObject();
        JObject Issuer = new JObject();
        JObject Document = new JObject();

        Credenciales.Add("id", Id);
        Credenciales.Add("token", Token);

        Issuer.Add("rfc", RFC);

        Document.Add("certificate-number", NoCertificado);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("document", Document);
        string response = Conector.Cancelar(Request, UUID);

        Dictionary<string, object> Response = (Dictionary<string, object>)JSON.Parse(response);

        string date = "";
        string message = "Error en el timbrado";
        int Error = 0;

        if (!Response.ContainsKey("message") || !Response.ContainsKey("error_details"))
        {

            try
            {
                message = "";

                date = (Response.ContainsKey("date")) ? (string)Response["date"] : "";

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Error = 1;
            }
        }
        else
        {
            if (Response.ContainsKey("error_details") && (string)Response["error_details"] == "[\"Document already cancelled\"]")
            {
                message = (string)Response["error_details"];
                Error = 0;
            }
            else
            {
                message = (string)Response["message"];
                Error = 1;

            }
        }

        Respuesta.Add("Error", Error);
        Respuesta.Add("message", message);
        Respuesta.Add("date", date);
        Respuesta.Add("ref_id", RefID);
        Respuesta.Add("request", Request);
        Respuesta.Add("response", response);
        Respuesta.Add("certificado", NoCertificado);
        Respuesta.Add("rfc", RFC);
        Respuesta.Add("motivoCancelacion", MotivoCancelacion);

        return Respuesta.ToString();
    }

}