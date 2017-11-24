using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladosConcepto
{

	private CTrasladoConcepto traslado;

	public CTrasladoConcepto Traslado
	{
		get { return traslado; }
		set { traslado = value; }
	}

	public CTrasladosConcepto()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		Error.Add("Traslado",traslado.Validar());
		return Error;
	}

}