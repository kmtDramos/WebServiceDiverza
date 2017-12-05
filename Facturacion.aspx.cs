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
		JObject Datos = Conector.Emitir(Id, Token, RFC, RefID, NoCertificado, Formato, Correos.Split(',').ToList(), encode);

		JObject Response = new JObject().FromObject(Datos.Get("Response"));

		string uuid = "";
		string ref_id = "";
		string content = "";
		string message = "";
		string pdf = "";
		string xml = "";

		if (!Response.Exist("message"))
		{

			try
			{
				uuid = (Response.Exist("uuid")) ? (string)Response.Get("uuid") : "";
				ref_id = (Response.Exist("uuid")) ? (string)Response.Get("ref_id") : "";
				uuid = (Response.Exist("uuid")) ? (string)Response.Get("uuid") : "";
				content = (Response.Exist("content")) ? ((string)Response.Get("content")) : "";

				ZipArchive zip = new ZipArchive(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)), ZipArchiveMode.Read);

				ZipArchiveEntry file_pdf = zip.GetEntry("invoice.pdf");
				ZipArchiveEntry file_xml = zip.GetEntry("invoice.xml");

				pdf = Base64.Encode(Zip.Read(file_pdf));
				xml = Base64.Encode(Zip.Read(file_xml));
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}
		}
		else
		{
			message = content;// (string)Response.Get("message");
		}

		
		Respuesta.Add("message", message);
		Respuesta.Add("uuid", uuid);
		Respuesta.Add("ref_id", ref_id);
		Respuesta.Add("content", content);
		Respuesta.Add("pdf", pdf);
		Respuesta.Add("xml", xml);

		return Respuesta.ToString();
	}

	[WebMethod]
	public static string CancelarFactura(string Id, string Token, string UUID, string RFC, string NoCertificado)
	{
		return Conector.Cancelar(Id, Token, UUID, RFC, NoCertificado);
	}

	private static CComprobante GenerarComprobante (Dictionary<string, object> Comprobante)
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
	
}