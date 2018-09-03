using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.IO.Compression;

public partial class Pagos : System.Web.UI.Page
{

    [WebMethod]
    public static string TimbrarPago(string Id, string Token, Dictionary<string, object> Comprobante, string RFC, string RefID, string NoCertificado, string Formato, string Correos, string RutaCFDI)
    {
        JObject Respuesta = new JObject();
        string Xml = "";
        CPago comprobante = new CPago(); ;
        
        try { comprobante = GenerarComprobante(Comprobante); }
        catch (Exception ex)
        { }

        Xml = PagosXML.XML(comprobante);

        // Save file XML in
        System.IO.Directory.CreateDirectory(@"" + RutaCFDI + @"\Pagos\in\" + RFC);
        System.IO.File.WriteAllText(@"" + RutaCFDI + @"\Pagos\in\" + RFC + @"\" + RefID + ".xml", Xml);

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
        Document.Add("type", "application/vnd.diverza.cfdi_3.3_complemento+xml");
        Document.Add("content", encode);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("receiver", Conector.ObtenerDestinatarios(Correos.Split(',').ToList()));
        Request.Add("document", Document);
        string response = Conector.Pago(Request);
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
                GuardarContenido(RutaCFDI, content, comprobante.Receptor.RFC, comprobante.Serie, comprobante.Folio);

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
        Respuesta.Add("content", content);
        Respuesta.Add("request", Request);
        Respuesta.Add("response", response);
        Respuesta.Add("certificado", NoCertificado);
        Respuesta.Add("serie", Convert.ToString(Comprobante["Serie"]));
        Respuesta.Add("folio", Convert.ToString(Comprobante["Folio"]));
        Respuesta.Add("rfc", RFC);

        return Respuesta.ToString();
    }

    private static CPago GenerarComprobante(Dictionary<string, object> Comprobante)
    {

        CPago comprobante = new CPago();

        comprobante.Serie = Convert.ToString(Comprobante["Serie"]);
        comprobante.Folio = Convert.ToString(Comprobante["Folio"]);
        comprobante.Fecha = Comprobante["Fecha"].ToString();
        comprobante.LugarExpedicion = Convert.ToString(Comprobante["LugarExpedicion"]);
        comprobante.Moneda = Convert.ToString(Comprobante["Moneda"]);
        comprobante.TipoDeComprobante = Convert.ToString(Comprobante["TipoDeComprobante"]);
        comprobante.Subtotal = Convert.ToDecimal(Comprobante["SubTotal"]);
        comprobante.Total = Convert.ToDecimal(Comprobante["Total"]);
        comprobante.NoCertificado = Convert.ToString(Comprobante["NoCertificado"]);
        comprobante.Certificado = Convert.ToString(Comprobante["Certificado"]);
        comprobante.Sello = Convert.ToString(Comprobante["Sello"]);

        Dictionary<string, object> Emisor = (Dictionary<string, object>)Comprobante["Emisor"];
        Dictionary<string, object> Receptor = (Dictionary<string, object>)Comprobante["Receptor"];

        comprobante.Emisor.RFC = Convert.ToString(Emisor["RFC"]);
        comprobante.Emisor.Nombre = Convert.ToString(Emisor["Nombre"]);
        comprobante.Emisor.RegimenFiscal = Convert.ToString(Emisor["RegimenFiscal"]);

        comprobante.Receptor.RFC = Convert.ToString(Receptor["RFC"]);
        comprobante.Receptor.Nombre = Convert.ToString(Receptor["Nombre"]);
        comprobante.Receptor.UsoCFDI = Convert.ToString(Receptor["UsoCFDI"]);

        Dictionary<string, object> Concepto = (Dictionary<string, object>)Comprobante["Concepto"];

        comprobante.Concepto.ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]);
        comprobante.Concepto.Cantidad = Convert.ToDecimal(Concepto["Cantidad"]);
        comprobante.Concepto.ClaveUnidad = Convert.ToString(Concepto["ClaveUnidad"]);
        comprobante.Concepto.Descripcion = Convert.ToString(Concepto["Descripcion"]);
        comprobante.Concepto.ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]);
        comprobante.Concepto.Importe = Convert.ToDecimal(Concepto["Importe"]);

        Dictionary<string, object> Complemento = (Dictionary<string, object>)Comprobante["Complemento"];
        
        comprobante.Complementos.FechaPago = Complemento["FechaPago"].ToString();
        comprobante.Complementos.FormaDePagoP = Convert.ToString(Complemento["FormaDePagoP"]);
        comprobante.Complementos.MonedaP = Convert.ToString(Complemento["MonedaP"]);
        comprobante.Complementos.TipoCambioP = Convert.ToString(Complemento["TipoCambioP"]);
        comprobante.Complementos.Monto = Convert.ToDecimal(Complemento["Monto"]);
        comprobante.Complementos.NumOperacion = Convert.ToString(Complemento["NumOperacion"]);
        comprobante.Complementos.RfcEmisorCtaOrd = Convert.ToString(Complemento["RfcEmisorCtaOrd"]);
        comprobante.Complementos.CtaOrdenante = Convert.ToString(Complemento["CtaOrdenante"]);
        comprobante.Complementos.RfcEmisorCtaBen = Convert.ToString(Complemento["RfcEmisorCtaBen"]);
        comprobante.Complementos.CtaBeneficiario = Convert.ToString(Complemento["CtaBeneficiario"]);
        //comprobante.Complementos.TipoCadPago = Convert.ToString(Complemento["TipoCadPago"]);
        //comprobante.Complementos.CertPago = Convert.ToString(Complemento["CertPago"]);
        //comprobante.Complementos.CadPago = Convert.ToString(Complemento["CadPago"]);
        //comprobante.Complementos.SelloPago = Convert.ToString(Complemento["SelloPago"]);

        Object[] Documentos = (Object[])Complemento["DoctosRelacionados"];

        if (Documentos.Length > 0)
        {
            int i = 0;
            CDoctoRelacionado[] docto = new CDoctoRelacionado[Documentos.Length];

            foreach (Dictionary<string, object> documento in Documentos)
            {
                docto[i] = new CDoctoRelacionado
                {
                    IdDocumento = Convert.ToString(documento["IdDocumento"]),
                    Serie = Convert.ToString(documento["Serie"]),
                    Folio = Convert.ToString(documento["Folio"]),
                    MonedaDR = Convert.ToString(documento["MonedaDR"]),
                    TipoCambioDR = Convert.ToString(documento["TipoCambioDR"]),
                    MetodoDePagoDR = Convert.ToString(documento["MetodoDePagoDR"]),
                    NumParcialidad = Convert.ToString(documento["NumParcialidad"]),
                    ImpSaldoAnt = Convert.ToDecimal(documento["ImpSaldoAnt"]),
                    ImpPagado = Convert.ToDecimal(documento["ImpPagado"]),
                    ImpSaldoInsoluto = Convert.ToDecimal(documento["ImpSaldoInsoluto"])
                };

                docto[i].IdDocumento = Convert.ToString(documento["IdDocumento"]);
                docto[i].Serie = Convert.ToString(documento["Serie"]);
                docto[i].Folio = Convert.ToString(documento["Folio"]);
                docto[i].MonedaDR = Convert.ToString(documento["MonedaDR"]);
                docto[i].TipoCambioDR = Convert.ToString(documento["TipoCambioDR"]);
                docto[i].MetodoDePagoDR = Convert.ToString(documento["MetodoDePagoDR"]);
                docto[i].NumParcialidad = Convert.ToString(documento["NumParcialidad"]);
                docto[i].ImpSaldoAnt = Convert.ToDecimal(documento["ImpSaldoAnt"]);
                docto[i].ImpPagado = Convert.ToDecimal(documento["ImpPagado"]);
                docto[i].ImpSaldoInsoluto = Convert.ToDecimal(documento["ImpSaldoInsoluto"]);

                i++;
            }
            comprobante.Complementos.DoctoRelacionados = docto;
        }
        else
        {

        }

        return comprobante;
    }

    private static void GuardarContenido(string RutaCFDI, string Contenido, string RFC, string Serie, string Folio)
    {
        string RFCCliente = RFC.Replace("&amp;", "&");
        string nameFile = Serie + Folio;
        System.IO.Directory.CreateDirectory(@"" + RutaCFDI + @"\Pagos\out\" + RFCCliente);
        System.IO.File.WriteAllBytes(@"" + RutaCFDI + @"\Pagos\out\" + RFCCliente + @"\" + nameFile + ".zip", Decode(Contenido));

        //Descomprimir zip
        string zipPath = @"" + RutaCFDI + @"\Pagos\out\" + RFCCliente + @"\" + nameFile + ".zip";
        string extractPath = @"" + RutaCFDI + @"\Pagos\out\" + RFCCliente;

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

}