﻿using System;
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
    public static string NotaCredito(JObject Request)
    { 
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
        return Peticion(Uri, Request.ToString(), "POST");
        
    }

    public static string Pago(JObject Request)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/issue";
        return Peticion(Uri, Request.ToString(), "POST");
    }

    public static string Cancelar(JObject Request, string UUID)
    {
        string Uri = "https://serviciosdemo.diverza.com/api/v1/documents/" + UUID + "/cancel";
        return Peticion(Uri, Request.ToString(), "PUT");
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