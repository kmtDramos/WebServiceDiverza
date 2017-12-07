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

    public static string Cancelar(string Id, string Token, string UUID, string RFC, string NoCertificado)
    {
        string jsoncPost = "{" +
                               "\"credentials\": {" +
									   "\"id\": \"" + Id +"\"," +
									   "\"token\": \"" + Token +"\"" +
                               "}," +
                               "\"issuer\": {" +
									   "\"rfc\": \"" + RFC +"\"" +
                               "}," +
                               "\"document\": {" +
									   "\"certificate-number\": \"" + NoCertificado +"\"" +
                               "}" +
                           "}";

        string Uric = "https://serviciosdemo.diverza.com/api/v1/docuemnts/"+ UUID +"/cancel";

        string contents = "";
        contents = Peticion(Uric, jsoncPost, "PUT");

        return contents;
    }

    public static string NotaCredito(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> Correos, string encodeXML)
    { 
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

        string jsonPost = "{" +
                                "\"credentials\":  {" +
                                    "\"id\": \"" + Id + "\"," +
                                     "\"token\": \"" + Token + "\"" +

                                "}," +
                                "\"issuer\": {" +
                                    "\"rfc\": \"" + RFC + "\"" +

                                "}," +
                                "\"receiver\": {" + ObtenerDestinatarios(Correos) + "}," +
                                "\"document\": {" +
                                    "\"ref-id\": \"" + RefId + "\"," +
                                    "\"certificate-number\":\"" + Certificado + "\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"" + Formato + "\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        string contents = "";
        contents = Peticion(Uri, jsonPost, "POST");

        return contents;
    }

    public static string Pago(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> Correos, string encodeXML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
        
        string jsonPost = "{" +
                                "\"credentials\":  {" +
                                    "\"id\": \"" + Id + "\"," +
                                     "\"token\": \"" + Token + "\"" +

                                "}," +
                                "\"issuer\": {" +
                                    "\"rfc\": \"" + RFC + "\"" +

                                "}," +
                                "\"receiver\": {" + ObtenerDestinatarios(Correos) + "}," +
                                "\"document\": {" +
                                    "\"ref-id\": \"" + RefId + "\"," +
                                    "\"certificate-number\":\"" + Certificado + "\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"" + Formato + "\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        string contents = "";
        contents = Peticion(Uri, jsonPost, "POST");

        return contents;
    }
    
    public static string Emitir(JObject Request)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
		return Peticion(Uri, Request.ToString(), "POST");
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
		Consulta.ContentType = "application/json";
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