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
        //Cancel
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

    public static string NotaCredito()
    {
        return "";
    }

    public static string Pago(String encodeXML)
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
                                    "\"type\": \"application / vnd.diverza.cfdi_3.3_complemento + xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        string contents = "";
        contents = DownloadPageAsync(Uri, jsonPost, "POST");

        return contents;
    }

    //Funcion Conector
    public static string Emitir(string encodeXML)
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


        /*
        JObject json = new JObject();

        // credentials
        JObject credentials = new JObject();
        credentials.Add("id", "94327");
        credentials.Add("token", "$2b$12$pj0NTsT/brybD2cJrNa8iuRRE5KoxeEFHcm/yJooiSbiAdbiTGzIq");
        json.Add("credentials", credentials);

        // issuer
        JObject issuer = new JObject();
        issuer.Add("rfc", "MAG041126GT8");
        json.Add("issuer", issuer);

        // receiver
        JObject receiver = new JObject();
        JArray emails = new JArray();
        JObject fer = new JObject();
        fer.Add("email", "mferna.92@gmail.com");
        fer.Add("format", "xml+pdf");
        fer.Add("template", "letter");
        emails.Add(fer);
        receiver.Add("emails", emails);
        json.Add("receiver", receiver);

        // document
        JObject document = new JObject();
        document.Add("ref-id", "FEI2017111500003");
        document.Add("certificate-number", "20001000000300022755");
        document.Add("section", "all");
        document.Add("format", "xml");
        document.Add("template", "letter");
        document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
        document.Add("content", encodeXML);
        json.Add("document", document);
        */
       

        string contents = "";
        contents = DownloadPageAsync(Uri,jsonPost, "POST");
        
        return contents;
    }
    public static string DownloadPageAsync(string page, string post, string method)
    {
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
        string r = "NADA";
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