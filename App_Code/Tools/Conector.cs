using System;
using System.Collections.Specialized;
using System.Net;

using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Conector
{

    public static JObject Cancelar(string Id, string Token, string RFC, string Certificado, string UUID)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/docuemnts/" + UUID + "/cancel";

        JObject Request = new JObject();
        JObject Credenciales = new JObject();
        JObject Issuer = new JObject();
        JObject Document = new JObject();

        Credenciales.Add("id", Id);
        Credenciales.Add("token", Token);

        Issuer.Add("rfc", RFC);

        Document.Add("certificate-number", Certificado);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("document", Document);

        JObject Datos = new JObject();

        Datos.Add("Request", Request);
        Datos.Add("Response", new JObject().FromString(Peticion(Uri, Request.ToString(), "PUT")));

        return Datos;
    }

    public static JObject NotaCredito(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> Correos, string XML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

        JObject Request = new JObject();
        JObject Credenciales = new JObject();
        JObject Issuer = new JObject();
        JObject Receivers = new JObject();
        JObject Document = new JObject();

        Credenciales.Add("id", Id);
        Credenciales.Add("token", Token);

        Issuer.Add("rfc", RFC);

        Document.Add("ref-id", RefId);
        Document.Add("certificate-number", Certificado);
        Document.Add("section", "all");
        Document.Add("format", Formato);
        Document.Add("template", "letter");
        Document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
        Document.Add("content", XML);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("receiver", ObtenerDestinatarios(Correos));
        Request.Add("document", Document);

        JObject Datos = new JObject();

        Datos.Add("Request", Request);
        Datos.Add("Response", new JObject().FromString(Peticion(Uri, Request.ToString(), "POST")));

        return Datos;

    }

    public static JObject Pago(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> Correos, string XML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

        JObject Request = new JObject();
        JObject Credenciales = new JObject();
        JObject Issuer = new JObject();
        JObject Receivers = new JObject();
        JObject Document = new JObject();

        Credenciales.Add("id", Id);
        Credenciales.Add("token", Token);

        Issuer.Add("rfc", RFC);

        Document.Add("ref-id", RefId);
        Document.Add("certificate-number", Certificado);
        Document.Add("section", "all");
        Document.Add("format", Formato);
        Document.Add("template", "letter");
        Document.Add("type", "application/vnd.diverza.cfdi_3.3_complemento+xml");
        Document.Add("content", XML);

        Request.Add("credentials", Credenciales);
        Request.Add("issuer", Issuer);
        Request.Add("receiver", ObtenerDestinatarios(Correos));
        Request.Add("document", Document);

        JObject Datos = new JObject();

        Datos.Add("Request", Request);
        Datos.Add("Response", new JObject().FromString(Peticion(Uri, Request.ToString(), "POST")));

        return Datos;
      
    }
    
    public static JObject Emitir(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> Correos, string XML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

		JObject Request = new JObject();
		JObject Credenciales = new JObject();
		JObject Issuer = new JObject();
		JObject Receivers = new JObject();
		JObject Document = new JObject();

		Credenciales.Add("id", Id);
		Credenciales.Add("token", Token);

		Issuer.Add("rfc", RFC);

		Document.Add("ref-id", RefId);
		Document.Add("certificate-number", Certificado);
		Document.Add("section", "all");
		Document.Add("format", Formato);
		Document.Add("template", "letter");
		Document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
		Document.Add("content", XML);

		Request.Add("credentials", Credenciales);
		Request.Add("issuer", Issuer);
		Request.Add("receiver", ObtenerDestinatarios(Correos));
		Request.Add("document", Document);

		JObject Datos = new JObject();

		Datos.Add("Request", Request);
		Datos.Add("Response", new JObject().FromString(Peticion(Uri, Request.ToString(), "POST")));

		return Datos;
	}

	public static JObject ObtenerDestinatarios (List<string> correos)
	{
		JObject receivers = new JObject();

		JArray emails = new JArray();

		foreach(string correo in correos)
		{
			JObject email = new JObject();
			email.Add("email", correo);
			email.Add("format", "xml+pdf");
			email.Add("template", "letter");
			emails.Add(email);
		}

		receivers.Add("emails", emails);
		
		return receivers;
	}

    public static string Peticion(string URI, string Request, string Metodo)
    {
		string Resultado = "";

        var Data = Encoding.ASCII.GetBytes(Request); 

        var Consulta = (HttpWebRequest)WebRequest.Create(URI);
		Consulta.ContentType = "application/json; charset=UTF-8";
		Consulta.Method = Metodo;

		StreamWriter Envio = new StreamWriter(Consulta.GetRequestStream());
		Envio.Write(Request);
		Envio.Close();

        try
        {
			HttpWebResponse Respuesta = (HttpWebResponse)Consulta.GetResponse();
			StreamReader Lector = new StreamReader(Respuesta.GetResponseStream());
			Resultado = Lector.ReadToEnd();
        }
        catch(WebException ex)
        {
            Resultado = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
        }

        return Resultado;
        
    }

}