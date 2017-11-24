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
			comprobante.Fecha = DateTime.Parse(Comprobante["Fecha"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
			comprobante.FechaPago = DateTime.Parse(Comprobante["FechaPago"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
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

			Object[] Conceptos = (Object[]) Comprobante["Conceptos"];

			if (Conceptos.Length > 0)
			{
				int i = 0;
				CConcepto[] conceptos = new CConcepto[Conceptos.Length];

				foreach (Dictionary<string, object> Concepto in Conceptos)
				{
					Dictionary<string, object> Impuestos = (Dictionary<string, object>)Concepto["Impuestos"];
					Object[] Traslados = (Object[])Impuestos["Traslados"];
					Dictionary<string, object> Traslado = (Dictionary<string, object>)Traslados[0];
					Dictionary<string, object> Contenido = (Dictionary<string, object>)Traslado["Traslado"];

					CImpuestoConcepto impuestos = new CImpuestoConcepto();
					CTrasladosConcepto[] traslados = new CTrasladosConcepto[Traslados.Length];
					CTrasladoConcepto traslado = new CTrasladoConcepto();
					CTrasladoConceptoContenido contenido = new CTrasladoConceptoContenido();
					traslado.Contenido = contenido;

					conceptos[i] = new CConcepto {
						Importe = Convert.ToDecimal(Concepto["Importe"]),
						ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]),
						Descripcion = Convert.ToString(Concepto["Descripcion"]),
						Cantidad = Convert.ToDecimal(Concepto["Cantidad"]),
						ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]),
						Impuestos = impuestos
					};

					conceptos[i].Importe = Convert.ToDecimal(Concepto["Importe"]);
					conceptos[i].ValorUnitario = Convert.ToDecimal(Concepto["ValorUnitario"]);
					conceptos[i].Descripcion = Convert.ToString(Concepto["Descripcion"]);
					conceptos[i].Cantidad = Convert.ToDecimal(Concepto["Cantidad"]);
					conceptos[i].ClaveProdServ = Convert.ToString(Concepto["ClaveProdServ"]);

					conceptos[i].Impuesto.Traslados[0].Traslado.Contenido.Base = Convert.ToDecimal(Traslado["Base"]);
					conceptos[i].Impuesto.Traslados[0].Traslado.Contenido.TipoFactor = Convert.ToString(Traslado["TipoFactor"]);
					conceptos[i].Impuesto.Traslados[0].Traslado.Contenido.TasaOCuota = Convert.ToDecimal(Traslado["TasaOCuota"]);
					conceptos[i].Impuesto.Traslados[0].Traslado.Contenido.Impuesto = Convert.ToString(Traslado["Impuesto"]);
					conceptos[i].Impuesto.Traslados[0].Traslado.Contenido.Importe = Convert.ToDecimal(Traslado["Importe"]);
					
					i++;
				}
				comprobante.Conceptos = conceptos;
			}
			else
			{

			}

			Dictionary<string, object> ImpuestosGlobales = (Dictionary<string, object>) Comprobante["Impuestos"];
			Object[] TrasladosGlobales = (Object[]) ImpuestosGlobales["Traslados"];
			Dictionary<string, object> TrasladoGlobal = (Dictionary<string, object>) TrasladosGlobales[0];
			Dictionary<string, object> ContenidoTrasladoGlobal = (Dictionary<string, object>) TrasladoGlobal["Traslado"];
			
			comprobante.Impuestos.TotalImpuestosTraslados = Convert.ToDecimal(TrasladoGlobal["TotalImpuestosTraslados"]);
			comprobante.Impuestos.Traslados[0].Traslado.Contenido.Impuresto = Convert.ToString(ContenidoTrasladoGlobal["Impuesto"]);
			comprobante.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor = Convert.ToString(ContenidoTrasladoGlobal["TipoFactor"]);
			comprobante.Impuestos.Traslados[0].Traslado.Contenido.TasaOCuota = Convert.ToDecimal(ContenidoTrasladoGlobal["TasaOCuota"]);
			comprobante.Impuestos.Traslados[0].Traslado.Contenido.Importe = Convert.ToDecimal(ContenidoTrasladoGlobal["Importe"]);

			Respuesta.Add("Comprobante", comprobante);

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