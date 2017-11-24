using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladoConcepto
{

	private CTrasladoConceptoContenido contenido;

	public CTrasladoConceptoContenido Contenido
	{
		get { return contenido; }
		set { contenido = value; }
	}

	public CTrasladoConcepto()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		return Error;
	}

}