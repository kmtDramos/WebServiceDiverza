using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FacturacionXML
{
	public static string XML(CComprobante Comprobante)
	{
		string xml =
		"<?xml version=\"1.0\" encoding=\"utf - 8\" ?>" + System.Environment.NewLine +
		"< cfdi:Comprobante" + System.Environment.NewLine +
			"xmlns:xsi = \"http://www.w3.org/2001/XMLSchema-instance\" " + System.Environment.NewLine +
			"xmlns: cfdi = \"http://www.sat.gob.mx/cfd/3\" " + System.Environment.NewLine +
			"xsi: schemaLocation = \"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" " + System.Environment.NewLine +
			"Version = \"3.3\" " + System.Environment.NewLine +
			"Folio = \"\" " + System.Environment.NewLine +
			"Fecha = \"" + Comprobante.Fecha + "\" " + System.Environment.NewLine +
			"FormaPago = \"" + Comprobante.FechaPago + "\" " + System.Environment.NewLine +
			"CondicionesDePago = \"" + Comprobante.CondicionDePago + "\" " + System.Environment.NewLine +
			"NoCertificado = \"" + Comprobante.NoCertificado + "\" " + System.Environment.NewLine +
			"Certificado = \"" + Comprobante.Certificado + "\" " + System.Environment.NewLine +
			"SubTotal = \"" + Comprobante.Subtotal + "\" " + System.Environment.NewLine +
			"TipoCambio = \"" + Comprobante.TipoCambio + "\" " + System.Environment.NewLine +
			"Moneda = \"" + Comprobante.Moneda + "\" " + System.Environment.NewLine +
			"Total = \"" + Comprobante.Total + "\" " + System.Environment.NewLine +
			"TipoDeComprobante = \"" + Comprobante.TipoDeComprobante + "\" " + System.Environment.NewLine +
			"MetodoPago = \"" + Comprobante.MetodoPago + "\" " + System.Environment.NewLine +
			"LugarExpedicion = \"" + Comprobante.LugarExpedicion + "\" " + System.Environment.NewLine +
			"Sello = \"" + Comprobante.Sello + "\">" + System.Environment.NewLine +
		"< cfdi:Emisor" + System.Environment.NewLine +
			"Nombre = \"" + Comprobante.Emisor.Nombre + "\" " + System.Environment.NewLine +
			"Rfc = \"" + Comprobante.Emisor.RFC + "\" " + System.Environment.NewLine +
			"RegimenFiscal = \"" + Comprobante.Emisor.RegimenFiscal + "\"/>" + System.Environment.NewLine +
		"< cfdi:Receptor" + System.Environment.NewLine +
			"Rfc = \"" + Comprobante.Receptor.RFC + "\" " + System.Environment.NewLine +
			"Nombre = \"" + Comprobante.Receptor.Nombre + "\" " + System.Environment.NewLine +
			"UsoCFDI = \"" + Comprobante.Receptor.UsoCFDI + "\"/>" + System.Environment.NewLine +
		"< cfdi:Conceptos >" + System.Environment.NewLine;

		foreach (CConcepto Concepto in Comprobante.Conceptos)
		{
			xml +=
				"< cfdi:Concepto" + System.Environment.NewLine +
					"ClaveProdServ = \" " + Concepto.ClaveProdServ + "\" " + System.Environment.NewLine +
					"Cantidad = \"" + Concepto.Cantidad + "\" " + System.Environment.NewLine +
					"ClaveUnidad = \"" + Concepto.ClaveUnidad + "\" " + System.Environment.NewLine +
					"Descripcion = \"" + Concepto.Descripcion + "\" " + System.Environment.NewLine +
					"ValorUnitario = \"" + Concepto.ValorUnitario + "\" " + System.Environment.NewLine +
					"Importe = \"" + Concepto.Importe + "\">" + System.Environment.NewLine +
					"< cfdi:Impuestos >" + System.Environment.NewLine +
						"< cfdi:Traslados >" + System.Environment.NewLine +
							"< cfdi:Traslado" + System.Environment.NewLine +
								"Base = \"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Base + "\" " + System.Environment.NewLine +
								"Impuesto = \"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\" " + System.Environment.NewLine +
								"TipoFactor = \"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\" " + System.Environment.NewLine +
								"TasaOCuota = \"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.TasaOCuota + "\" " + System.Environment.NewLine +
								"Importe = \"" + Concepto.Impuestos.Traslados[0].Traslado.Contenido.Importe + "\"/>" + System.Environment.NewLine +
						"</ cfdi:Traslados >" + System.Environment.NewLine +
					"</ cfdi:Impuestos >" + System.Environment.NewLine +
				"</ cfdi:Concepto >" + System.Environment.NewLine;
		}

		xml +=
		"</ cfdi:Conceptos >" + System.Environment.NewLine;
		
		xml +=
		"< cfdi:Impuestos TotalImpuestosTrasladados = \" " + Comprobante.Impuestos.TotalImpuestosTraslados + "\" " + System.Environment.NewLine +
	 			"< cfdi:Traslados >" + System.Environment.NewLine +
					"< cfdi:Traslado" + System.Environment.NewLine +
						"Impuesto = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Impuesto + "\" " + System.Environment.NewLine +
						"TipoFactor = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.TipoFactor + "\" " + System.Environment.NewLine +
						"TasaOCuota = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.TasaOCuota + "\" " + System.Environment.NewLine +
						"Importe = \"" + Comprobante.Impuestos.Traslados[0].Traslado.Contenido.Importe + "\"/>" + System.Environment.NewLine +
				"</ cfdi:Traslados >" + System.Environment.NewLine +
			"</ cfdi:Impuestos >" + System.Environment.NewLine +
		"</ cfdi:Comprobante >" + System.Environment.NewLine;

		return xml;
	}
}