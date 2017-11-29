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
        contents = DownloadPageAsync(Uric, jsoncPost, "PUT");

        return contents;
    }

    public static string NotaCredito(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> correos, string encodeXML)
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
                                "\"receiver\": {" + Receivers(correos) + "}," +
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

    public static string Pago(string Id, string Token, string RFC, string RefId, string Certificado, string Formato, List<string> correos, string encodeXML)
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
                                "\"receiver\": {" + Receivers(correos) + "}," +
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
    
    public static string Emitir(string Id, string Token, string rfc, string RefId, string Certificado, string Formato, List<string> correos, string encodeXML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

        string jsonPost = "{" +
                                "\"credentials\":  {" +
                                    "\"id\": \"" + Id + "\"," +
                                     "\"token\": \"" + Token + "\"" +
                                "}," +
                                "\"issuer\": {" +
                                    "\"rfc\": \""+ rfc +"\"" +

                                "}," +
                                "\"receiver\": {" + Receivers(correos) + "}," +
                                "\"document\": {" +
                                    "\"ref-id\": \"" + RefId + "\"," +
									"\"certificate-number\":\"" + Certificado + "\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \""+ Formato + "\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";
		
        string contents = "";
        contents = DownloadPageAsync(Uri, jsonPost, "POST");
        return contents;
    }

	public static string Receivers (List<string> correos)
	{
		string receivers = "";

		foreach(string correo in correos)
		{
			receivers = "{\"email\":\"" + correo + "\", \"format\":\"xml+pdf\", \"template\":\"letter\"}";
		}

		receivers = "\"emails\":[" + receivers + "]";

		return receivers;
	}

    public static string DownloadPageAsync(string page, string post, string method)
    {
		string r = "";
        var data = Encoding.ASCII.GetBytes(post); 

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(page);
        httpWebRequest.ContentType = "application/json; charset=UTF-8";
        httpWebRequest.Method = method;
        httpWebRequest.ContentLength = data.Length;

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(post);
            streamWriter.Close();
        }
        try
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                r = result; 
            }
        }
        catch(WebException wex)
        {
            var pageContent = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
            r = pageContent;
        }
        return r;
        
    }

}