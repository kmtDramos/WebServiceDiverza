using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CImpuestoConcepto
{

	private CTrasladosConcepto[] traslados;

	public CTrasladosConcepto[] Traslados
	{
		get { return traslados; }
		set { traslados = value; }
	}

	public JObject Validar()
	{
		JObject Error = new JObject();
		int i = 0;
		foreach (CTrasladosConcepto traslado in traslados)
		{
			Error.Add("Concepto["+i+"]",traslado.Validar());
			i++;
		}
		return Error;
	}

}