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
    public static string TimbrarPagos(string Id, string Token, Dictionary<string, object> Comprobante, string RFC, string RefID, string NoCertificado, string Formato, string Correos)
    {
        JObject Respuesta = new JObject();
        string Xml = "";
        CPago comprobante = new CPago(); ;

        try { comprobante = GenerarComprobante(Comprobante); }
        catch (Exception ex)
        { }

        Xml = PagosXML.XML(comprobante);
        System.IO.Directory.CreateDirectory(@"C:\inetpub\wwwroot\WebServiceDiverza\XML\" + RFC);
        System.IO.File.WriteAllText(@"C:\inetpub\wwwroot\WebServiceDiverza\XML\" + RFC + @"\" + RefID + ".xml", Xml);
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

        Dictionary<string, object> Response = (Dictionary<string, object>)JSON.Parse(response);

        string uuid = "";
        string ref_id = "";
        string content = "";
        string message = "Error en el timbrado";
        string pdf = "";
        string xml = "";

        if (!Response.ContainsKey("message"))
        {

            try
            {
                message = "";

                uuid = (Response.ContainsKey("uuid")) ? (string)Response["uuid"] : "";
                ref_id = (Response.ContainsKey("uuid")) ? (string)Response["ref_id"] : "";
                uuid = (Response.ContainsKey("uuid")) ? (string)Response["uuid"] : "";
                content = (Response.ContainsKey("content")) ? (string)Response["content"] : "";

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }
        else
        {
            message = (string)Response["message"];
        }


        Respuesta.Add("message", message);
        Respuesta.Add("uuid", uuid);
        Respuesta.Add("ref_id", ref_id);
        Respuesta.Add("content", content);
        Respuesta.Add("request", Request);
        Respuesta.Add("response", response);
        Respuesta.Add("certificado", NoCertificado);

        return content;//Xml; //response;
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
        comprobante.Concepto.Cantidad = Convert.ToDecimal(Concepto["ClaveUnidad"]);
        comprobante.Concepto.Descripcion = Convert.ToString(Concepto["Descripcion"]);
        comprobante.Concepto.ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]);
        comprobante.Concepto.Importe = Convert.ToDecimal(Concepto["Importe"]);

        Dictionary<string, object> Complemento = (Dictionary<string, object>)Comprobante["Complemento"];

        comprobante.Complementos.FechaPago = Convert.ToString(Complemento["FechaPago"]);
        comprobante.Complementos.FormaDePagoP = Convert.ToString(Complemento["FormaDePagoP"]);
        comprobante.Complementos.FechaPago = Complemento["FechaPago"].ToString();
        comprobante.Complementos.FormaDePagoP = Convert.ToString(Complemento["FormaDePagoP"]);
        comprobante.Complementos.MonedaP = Convert.ToString(Complemento["MonedaP"]);
        comprobante.Complementos.Monto = Convert.ToDecimal(Complemento["Monto"]);
        comprobante.Complementos.RfcEmisorCtaOrd = Convert.ToString(Complemento["RfcEmisorCtOrd"]);
        comprobante.Complementos.NomBancoOrdExt = Convert.ToString(Complemento["NomBancoOrdExt"]);
        comprobante.Complementos.CtaOrdenante = Convert.ToString(Complemento["CtaOrdenante"]);
        comprobante.Complementos.RfcEmisorCtaBen = Convert.ToString(Complemento["RfcEmisorCtaBen"]);
        comprobante.Complementos.CtaBeneficiario = Convert.ToString(Complemento["CtaBeneficiario"]);
        comprobante.Complementos.TipoCadPago = Convert.ToString(Complemento["TipoCadPago"]);
        comprobante.Complementos.CertPago = Convert.ToString(Complemento["CertPago"]);
        comprobante.Complementos.CadPago = Convert.ToString(Complemento["CadPago"]);
        comprobante.Complementos.SelloPago = Convert.ToString(Complemento["SelloPago"]);

        Dictionary<string, object> DoctoRelacionados = (Dictionary<string, object>)Complemento["DoctoRelacionados"];

        comprobante.Complementos.DoctoRelacionados.IdDocumento = Convert.ToString(DoctoRelacionados["IdDocumento"]);
        comprobante.Complementos.DoctoRelacionados.MonedaDR = Convert.ToString(DoctoRelacionados["MonedaDR"]);
        comprobante.Complementos.DoctoRelacionados.MetodoDePagoDR = Convert.ToString(DoctoRelacionados["MetodoDePagoDR"]);
        comprobante.Complementos.DoctoRelacionados.NumParcialidad = Convert.ToString(DoctoRelacionados["NumParcialidad"]);
        comprobante.Complementos.DoctoRelacionados.ImpSaldoAnt = Convert.ToDecimal(DoctoRelacionados["ImpSaldoAnt"]);
        comprobante.Complementos.DoctoRelacionados.ImpPagado = Convert.ToDecimal(DoctoRelacionados["ImpPagado"]);
        comprobante.Complementos.DoctoRelacionados.ImpSaldoInsoluto = Convert.ToDecimal(DoctoRelacionados["ImpSaldoInsoluto"]);

        return comprobante;
    }
}