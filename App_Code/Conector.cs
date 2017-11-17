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

    //Funcion Conector
    public static string Connect(string encodeXML)
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
        string Uric = "https://serviciosdemo.diverza.com/api/v1/documents/9196d619-8d8a-48b6-a160-9bc1ce414d7b/cancel";

        string contents = "";
        contents = DownloadPageAsync(Uric,jsoncPost);
        
        return contents;
    }
    public static string DownloadPageAsync(string page, string post)
    {
        var data = Encoding.ASCII.GetBytes(post); 

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(page);
        httpWebRequest.ContentType = "application/json; charset=UTF-8";
        httpWebRequest.Method = "PUT";//"POST";
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