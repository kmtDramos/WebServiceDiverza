using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.IO.Compression;

public partial class Facturacion : System.Web.UI.Page
{

	[WebMethod]
	public static string TimbrarFactura(string Id, string Token, Dictionary<string, object> Comprobante, string RFC, string RefID, string NoCertificado, string Formato, string Correos )
	{
		JObject Respuesta = new JObject();
		string Xml = "";
		CComprobante comprobante = new CComprobante(); ;

		try { comprobante = GenerarComprobante(Comprobante); }
		catch (Exception ex)
		{  }

		Xml = FacturacionXML.XML(comprobante);
      
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
		Document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
		Document.Add("content", encode);

		Request.Add("credentials", Credenciales);
		Request.Add("issuer", Issuer);
		Request.Add("receiver", Conector.ObtenerDestinatarios(Correos.Split(',').ToList()));
		Request.Add("document", Document);
        //return Request.ToString();
        string response = Conector.Emitir(Request);

		Dictionary<string, object> Response = (Dictionary<string, object>)JSON.Parse(response);

		string uuid = "";
		string ref_id = "";
		string content = "";
		string message = "Error en el timbrado";
		string pdf = "";
		string xml = "";
        int Error = 0;

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
                Error = 1;
			}
		}
		else
		{
			message = (string)Response["message"];
            Error = 1;
		}

        Respuesta.Add("Error",Error);
		Respuesta.Add("message", message);
		Respuesta.Add("uuid", uuid);
		Respuesta.Add("ref_id", ref_id);
		Respuesta.Add("content", content);
		Respuesta.Add("request", Request);
		Respuesta.Add("response", response);
		Respuesta.Add("certificado", NoCertificado);
        Respuesta.Add("rfc", RFC);

		return Respuesta.ToString();
    }

    private static CComprobante GenerarComprobante(Dictionary<string, object> Comprobante)
    {

        CComprobante comprobante = new CComprobante();

        comprobante.Serie = Convert.ToString(Comprobante["Serie"]);
        comprobante.Folio = Convert.ToString(Comprobante["Folio"]);
        comprobante.Fecha = Comprobante["Fecha"].ToString();
        comprobante.FormaPago = Comprobante["FormaPago"].ToString();
        comprobante.CondicionDePago = Convert.ToString(Comprobante["CondicionDePago"]);
        comprobante.NoCertificado = Convert.ToString(Comprobante["NoCertificado"]);
        comprobante.Certificado = Convert.ToString(Comprobante["Certificado"]);
        comprobante.Subtotal = Convert.ToDecimal(Comprobante["SubTotal"]);
        comprobante.TipoCambio = Convert.ToDecimal(Comprobante["TipoCambio"]);
        comprobante.Moneda = Convert.ToString(Comprobante["Moneda"]);
        comprobante.Total = Convert.ToDecimal(Comprobante["Total"]);
        comprobante.TipoDeComprobante = Convert.ToString(Comprobante["TipoDeComprobante"]);
        comprobante.MetodoPago = Convert.ToString(Comprobante["MetodoPago"]);
        comprobante.LugarExpedicion = Convert.ToString(Comprobante["LugarExpedicion"]);
        comprobante.Sello = Convert.ToString(Comprobante["Sello"]);

        Dictionary<string, object> Emisor = (Dictionary<string, object>)Comprobante["Emisor"];
        Dictionary<string, object> Receptor = (Dictionary<string, object>)Comprobante["Receptor"];

        comprobante.Emisor.Nombre = Convert.ToString(Emisor["Nombre"]);
        comprobante.Emisor.RFC = Convert.ToString(Emisor["RFC"]);
        comprobante.Emisor.RegimenFiscal = Convert.ToString(Emisor["RegimenFiscal"]);

        comprobante.Receptor.Nombre = Convert.ToString(Receptor["Nombre"]);
        comprobante.Receptor.RFC = Convert.ToString(Receptor["RFC"]);
        comprobante.Receptor.UsoCFDI = Convert.ToString(Receptor["UsoCFDI"]);

        Object[] Conceptos = (Object[])Comprobante["Conceptos"];

        if (Conceptos.Length > 0)
        {
            int i = 0;
            CConcepto[] conceptos = new CConcepto[Conceptos.Length];

            foreach (Dictionary<string, object> Concepto in Conceptos)
            {
                Dictionary<string, object> Impuestos = (Dictionary<string, object>)Concepto["Impuestos"];
                Object[] Traslados = (Object[])Impuestos["Traslados"];

                CImpuestoConcepto impuestos = new CImpuestoConcepto();
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

                conceptos[i] = new CConcepto
                {
                    Importe = Convert.ToDecimal(Concepto["Importe"]),
                    ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]),
                    Descripcion = Convert.ToString(Concepto["Descripcion"]),
                    Cantidad = Convert.ToDecimal(Concepto["Cantidad"]),
                    ClaveUnidad = Convert.ToString(Concepto["ClaveUnidad"]),
                    ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]),
                    Impuestos = impuestos
                };

                conceptos[i].Importe = Convert.ToDecimal(Concepto["Importe"]);
                conceptos[i].ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]);
                conceptos[i].Descripcion = Convert.ToString(Concepto["Descripcion"]);
                conceptos[i].Cantidad = Convert.ToDecimal(Concepto["Cantidad"]);
                conceptos[i].ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]);

                i++;
            }
            comprobante.Conceptos = conceptos;
        }
        else
        {

        }

        Dictionary<string, object> ImpuestosGlobales = (Dictionary<string, object>)Comprobante["Impuestos"];
        Object[] TrasladosGlobales = (Object[])ImpuestosGlobales["Traslados"];

        comprobante.Impuestos.TotalImpuestosTraslados = Convert.ToDecimal(ImpuestosGlobales["TotalImpuestosTrasladados"]);

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

        comprobante.Impuestos.Traslados = trasladosglobales;

        return comprobante;
    }

    [WebMethod]
	public static string CancelarFactura(string Id, string Token, string UUID, string RefID, string RFC, string NoCertificado, string MotivoCancelacion)
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
        //return Request.ToString();
        string response = Conector.Cancelar(Request, UUID);

        Dictionary<string, object> Response = (Dictionary<string, object>)JSON.Parse(response);

        string uuid = "";
        string content = "";
        string date = "";
        string message = "Error en el timbrado";
        int Error = 0;
        if (!Response.ContainsKey("message") || !Response.ContainsKey("error_details"))
        {

            try
            {
                message = "";

                uuid = (Response.ContainsKey("uuid")) ? (string)Response["uuid"] : "";
                date = (Response.ContainsKey("date")) ? (string)Response["date"] : "";
                content = (Response.ContainsKey("acknowledgement")) ? (string)Response["acknowledgement"] : "";

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
            message = (string)Response["error_details"];
            Error = 1;
        }

        Respuesta.Add("Error", Error);
        Respuesta.Add("message", message);
        Respuesta.Add("uuid", uuid);
        Respuesta.Add("date", date);
        Respuesta.Add("ref_id", RefID);
        Respuesta.Add("content", content);
        Respuesta.Add("request", Request);
        Respuesta.Add("response", response);
        Respuesta.Add("certificado", NoCertificado);
        Respuesta.Add("rfc", RFC);
        Respuesta.Add("motivoCancelacion", MotivoCancelacion);

        return Respuesta.ToString();
    }
	
}