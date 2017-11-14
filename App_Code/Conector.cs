using System;

public class Conector
{

    //Funcion Conector
    public static string Connect(string encodeXML)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
        
        string post = "{\"credentials\": {\"id\": \"3935\",\"token\": \"ABCD1234\"  }, " +
                      "\"issuer\": {    \"rfc\": \"AAA010101AAA\"  }," +
                      "\"receiver\": {    \"emails\": [      {        \"email\": \"mferna.92@gmail.com\",        \"format\": \"xml+pdf\",        \"template\": \"letter\"      }    ]  }," +
                      "\"document\": {    \"ref-id\": \"1234568\",    \"certificate-number\": \"\",    \"section\": \"all\",    \"format\": \"pdf\",    \"template\": \"letter\",    \"type\": \"application/vnd.diverza.cfdi_3.3+xml\",    \"content\": \"" + encodeXML+ "\"  }}";

        
        
        JObject json = new JObject();

        // credentials
        JObject credentials = new JObject();
        credentials.Add("id", "3935");
        credentials.Add("token", "ABCD1234");
        json.Add("Credentials", credentials);

        // issuer
        JObject issuer = new JObject();
        issuer.Add("rfc", "AAA010101AAA");
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
        document.Add("ref-id", "EDV2017040300001");
        document.Add("certificate-number", "20001000000200001428");
        document.Add("section", "all");
        document.Add("format", "xml");
        document.Add("template", "letter");
        document.Add("type", "application/vnd.diverza.cfdi_3.3+xml");
        document.Add("content", encodeXML);
        json.Add("docuemnt", document);
        
        System.Net.WebClient wc = new System.Net.WebClient();
        wc.Headers[System.Net.HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
        string contents = wc.UploadString(Uri, post);

        return post;
    }
    
}