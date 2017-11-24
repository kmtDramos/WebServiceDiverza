using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladosConcepto
{

	private CTrasladoConcepto[] traslados;

	public CTrasladoConcepto[] Traslados
	{
		get { return traslados; }
		set { traslados = value; }
	}

	public CTrasladosConcepto()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		int i = 0;
		foreach (CTrasladoConcepto traslado in traslados)
		{
			Error.Add("Traslado["+i+"]",traslado.Validar());
			i++;
		}
		return Error;
	}

}