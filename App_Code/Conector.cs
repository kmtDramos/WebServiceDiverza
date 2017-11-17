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
                                    "\"ref-id\": \"FEI2017111500015\"," +
                                     "\"certificate-number\":\"20001000000300022755\"," +
                                    "\"section\": \"all\"," +
                                    "\"format\": \"pdf\"," +
                                    "\"template\": \"letter\"," +
                                    "\"type\": \"application/vnd.diverza.cfdi_3.3+xml\"," +
                                    "\"content\": \"" + encodeXML + "\"" +
                                "}" +
                           "}";

        //return jsonPost;
        //"\"content\": \"" + encodeXML + "\"}" +
        //"\"content\": \"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiID8+DQo8Y2ZkaTpDb21wcm9iYW50ZSANCiAgICB4bWxuczp4c2k9Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvWE1MU2NoZW1hLWluc3RhbmNlIiANCiAgICB4bWxuczpjZmRpPSJodHRwOi8vd3d3LnNhdC5nb2IubXgvY2ZkLzMiIA0KICAgIHhzaTpzY2hlbWFMb2NhdGlvbj0iaHR0cDovL3d3dy5zYXQuZ29iLm14L2NmZC8zIGh0dHA6Ly93d3cuc2F0LmdvYi5teC9zaXRpb19pbnRlcm5ldC9jZmQvMy9jZmR2MzMueHNkIiANCiAgICBWZXJzaW9uPSIzLjMiIA0KICAgIEZvbGlvPSIwOTAxNDYzNDQiIA0KICAgIEZlY2hhPSIyMDE3LTExLTA5VDEzOjE5OjM2IiANCiAgICBGb3JtYVBhZ289IjAzIiANCiAgICBDb25kaWNpb25lc0RlUGFnbz0iMTgvMDcvMjAxNyIgDQogICAgTm9DZXJ0aWZpY2Fkbz0iMjAwMDEwMDAwMDAzMDAwMjI3NTUiIA0KICAgIENlcnRpZmljYWRvPSIiIA0KICAgIFN1YlRvdGFsPSIyMjA3Ljg4IiANCiAgICBUaXBvQ2FtYmlvPSIxIiANCiAgICBNb25lZGE9Ik1YTiIgDQogICAgVG90YWw9IjI1NjEuMTQiIA0KICAgIFRpcG9EZUNvbXByb2JhbnRlPSJJIiANCiAgICBNZXRvZG9QYWdvPSJQVUUiIA0KICAgIEx1Z2FyRXhwZWRpY2lvbj0iNTE5MDYiIA0KICAgIFNlbGxvPSIiPg0KICA8Y2ZkaTpFbWlzb3IgDQogICAgUmZjPSJNQUcwNDExMjZHVDgiIA0KICAgIE5vbWJyZT0iR0FzZXJjb20gREVNTyIgDQogICAgUmVnaW1lbkZpc2NhbD0iNjAxIi8+DQogIDxjZmRpOlJlY2VwdG9yIA0KICAgIFJmYz0iT09URjU3MDYwMjRLNyIgDQogICAgTm9tYnJlPSJGRVJOQU5ETyBFc3Bpbm8gSSIgDQogICAgVXNvQ0ZEST0iRzAzIi8+DQogIDxjZmRpOkNvbmNlcHRvcz4NCiAgICA8Y2ZkaTpDb25jZXB0byANCiAgICAgICAgQ2xhdmVQcm9kU2Vydj0iNTExMDIyMDAiIA0KICAgICAgICBDYW50aWRhZD0iNCIgDQogICAgICAgIENsYXZlVW5pZGFkPSIzSSIgDQogICAgICAgIERlc2NyaXBjaW9uPSJBcHBsZSBNYWNib29rIFBybyBBRyBTQ0QgRFJTIDlYMTBDTSAoMVgxMFBLKSBVUyIgDQogICAgICAgIFZhbG9yVW5pdGFyaW89IjU1MS45NyIgDQogICAgICAgIEltcG9ydGU9IjIyMDcuODgiPg0KICAgICAgPGNmZGk6SW1wdWVzdG9zPg0KICAgICAgICA8Y2ZkaTpUcmFzbGFkb3M+DQogICAgICAgICAgPGNmZGk6VHJhc2xhZG8gDQogICAgICAgICAgICBCYXNlPSIyMjA3Ljg4IiANCiAgICAgICAgICAgIEltcHVlc3RvPSIwMDIiIA0KICAgICAgICAgICAgVGlwb0ZhY3Rvcj0iVGFzYSIgDQogICAgICAgICAgICBUYXNhT0N1b3RhPSIwLjE2MDAwMCIgDQogICAgICAgICAgICBJbXBvcnRlPSIzNTMuMjYiLz4NCiAgICAgICAgICA8L2NmZGk6VHJhc2xhZG9zPg0KICAgICAgICA8L2NmZGk6SW1wdWVzdG9zPg0KICAgICAgPC9jZmRpOkNvbmNlcHRvPg0KICAgIDwvY2ZkaTpDb25jZXB0b3M+DQogIDxjZmRpOkltcHVlc3RvcyANCiAgICBUb3RhbEltcHVlc3Rvc1RyYXNsYWRhZG9zPSIzNTMuMjYiPg0KICAgIDxjZmRpOlRyYXNsYWRvcz4NCiAgICAgIDxjZmRpOlRyYXNsYWRvIA0KICAgICAgICBJbXB1ZXN0bz0iMDAyIiANCiAgICAgICAgVGlwb0ZhY3Rvcj0iVGFzYSIgDQogICAgICAgIFRhc2FPQ3VvdGE9IjAuMTYwMDAwIiANCiAgICAgICAgSW1wb3J0ZT0iMzUzLjI2Ii8+DQogICAgICA8L2NmZGk6VHJhc2xhZG9zPg0KICAgIDwvY2ZkaTpJbXB1ZXN0b3M+DQo8L2NmZGk6Q29tcHJvYmFudGU+\"}" +
        //"}";

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

        string contents = "";

        
        //Cancel
        JObject jsonc = new JObject();
        jsonc.Add("credentials", credentials);
        jsonc.Add("issuer",issuer);
        string Uric = "https://serviciosdemo.diverza.com/api/v1/documents/873045ad-c985-40e2-8f14-732152bef3c1/cancel";
        
       
        WebClient wc = new WebClient();
        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
        wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; rv:5.0) Gecko/20100101 Firefox/5.0";
        contents = wc.UploadString(Uri, "POST", jsonPost);//jsonc.ToString());


        //contents = DownloadPageAsync(Uri,jsonPost);
        return contents;
    }
    public static string DownloadPageAsync(string page, string post)
    {
        var data = Encoding.ASCII.GetBytes(post); 

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(page);
        httpWebRequest.ContentType = "application/json; charset=UTF-8";
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentLength = data.Length;

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            //initiate the request
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var resToWrite = serializer.Deserialize<Dictionary<string, object>>(post);
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