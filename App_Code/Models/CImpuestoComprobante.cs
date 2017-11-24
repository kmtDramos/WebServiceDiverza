using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CImpuestoComprobante
{

	private decimal totalimpuestostrasladados = 0;
	private CTrasladosComprobante[] traslados = { };

	public decimal TotalImpuestosTraslados
	{
		get { return totalimpuestostrasladados; }
		set { totalimpuestostrasladados = value; }
	}

	public CTrasladosComprobante[] Traslados
	{
		get { return traslados; }
		set { traslados = value; }
	}

	public CImpuestoComprobante()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		if (totalimpuestostrasladados == 0) Error.Add("TotalImpuestosTraslados", "El total de impuestos trasladados no esta definido.");
		int i = 0;
		foreach (CTrasladosComprobante traslado in traslados)
		{
			Error.Add("Traslado[" + i + "]", traslado.Validar());
		}
		return Error;
	}

}