using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladosConcepto
{

	private CTrasladoConcepto traslado = new CTrasladoConcepto();

	public CTrasladoConcepto Traslado
	{
		get { return traslado; }
		set { traslado = value; }
	}


	public JObject Validar ()
	{
		JObject Error = new JObject();
		Error.Add("Traslado",traslado.Validar());
		return Error;
	}

}