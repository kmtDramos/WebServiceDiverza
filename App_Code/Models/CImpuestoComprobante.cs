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

}