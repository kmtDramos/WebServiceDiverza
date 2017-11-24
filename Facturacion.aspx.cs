using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Facturacion : System.Web.UI.Page
{
	
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	[WebMethod]
	public static string TimbrarFactura(Dictionary<string, object> Comprobante)
	{
		JObject Respuesta = new JObject();

		int Error = 0;
		string DescripcionError = "";

		try
		{
			CComprobante comprobante = new CComprobante();

			comprobante.Serie = Convert.ToString(Comprobante["Serie"]);
			comprobante.Fecha = Comprobante["Fecha"].ToString();
			comprobante.FechaPago = Comprobante["FechaPago"].ToString();
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

			Object[] Conceptos = (Object[]) Comprobante["Conceptos"];

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
						traslados[j] = new CTrasladosConcepto {
							Traslado = traslado
						};
					}

					impuestos.Traslados = traslados;

					conceptos[i] = new CConcepto {
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

			Dictionary<string, object> ImpuestosGlobales = (Dictionary<string, object>) Comprobante["Impuestos"];
			Object[] TrasladosGlobales = (Object[]) ImpuestosGlobales["Traslados"];
			
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

			string xml = FacturacionXML.XML(comprobante);

			Respuesta.Add("XML", xml);

		}
		catch (Exception ex)
		{
			Error = 1;
			DescripcionError = ex.Message +" - "+ ex.StackTrace;
		}

		Respuesta.Add("Error", Error);
		Respuesta.Add("Descripcion", DescripcionError);

		return Respuesta.ToString();
	}
	
}