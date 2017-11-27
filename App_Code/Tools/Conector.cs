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

    public static string Cancelar(string uuid)
    {
        string jsoncPost = "{" +
                               "\"credentials\": {" +
                                       "\"id\": \"94327\"," +
                                       "\"token\": \"$2b$12$pj0NTsT/brybD2cJrNa8iuRRE5KoxeEFHcm/yJooiSbiAdbiTGzIq\"" +
                               "}," +
                               "\"issuer\": {" +
                                       "\"rfc\": \"MAG041126GT8\"" +
                               "}," +
                               "\"document\": {" +
                                       "\"certificate-number\": \"20001000000300022755\"" +
                               "}" +
                           "}";

        string Uric = "https://serviciosdemo.diverza.com/api/v1/docuemnts/"+uuid+"/cancel";

        string contents = "";
        contents = DownloadPageAsync(Uric, jsoncPost, "PUT");

        return contents;
    }

    public static string NotaCredito(string encodeXML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";

        string jsonPost = "{" +
                                "\"credentials\":  {" +
                                    "\"id\": \"94327\"," +
                                     "\"token\": \"$2b$12$pj0NTsT/brybD2cJrNa8iuRRE5KoxeEFHcm/yJooiSbiAdbiTGzIq\"" +

                                "}," +
                                "\"issuer\": {" +
                                    "\"rfc\": \"MAG041126GT8\"" +

                                "}," +
                                "\"receiver\": {" +
                                    "\"emails\":" +
                                        "[" +
                                            "{" +
                                                "\"email\": \"mferna.92@gmail.com\"," +
                                                 "\"format\": \"xml+pdf\"," +
                                                 "\"template\": \"letter\"" +
                                            "}" +
                                        "]" +
                                "}," +
                                "\"document\": {" +
                                    "\"ref-id\": \"" + DateTime.Now.Ticks.ToString() + "\"," +
                                    "\"certificate-number\":\"20001000000300022755\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"pdf\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        string contents = "";
        contents = DownloadPageAsync(Uri, jsonPost, "POST");

        return contents;
    }

    public static string Pago(string encodeXML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
        
        string jsonPost = "{" +
                                "\"credentials\":  {" +
                                    "\"id\": \"94327\"," +
                                     "\"token\": \"$2b$12$pj0NTsT/brybD2cJrNa8iuRRE5KoxeEFHcm/yJooiSbiAdbiTGzIq\"" +

                                "}," +
                                "\"issuer\": {" +
                                    "\"rfc\": \"MAG041126GT8\"" +

                                "}," +
                                "\"receiver\": {" +
                                    "\"emails\":" +
                                        "[" +
                                            "{" +
                                                "\"email\": \"mferna.92@gmail.com\"," +
                                                 "\"format\": \"xml+pdf\"," +
                                                 "\"template\": \"letter\"" +
                                            "}" +
                                        "]" +
                                "}," +
                                "\"document\": {" +
                                    "\"ref-id\": \"" + DateTime.Now.Ticks.ToString() + "\"," +
                                    "\"certificate-number\":\"20001000000300022755\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"pdf\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3_complemento+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        string contents = "";
        contents = DownloadPageAsync(Uri, jsonPost, "POST");

        return contents;
    }
    
    public static string Emitir(int Id, string Token, string rfc, string RefId,List<string> correos, string encodeXML)
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
									"\"certificate-number\":\"" + RefId + "\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"pdf\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";
		
        string contents = "";
        contents = DownloadPageAsync(Uri, jsonPost, "POST");
		contents = (contents == "") ? jsonPost : contents;
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