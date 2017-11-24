using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladoComprobante
{

	private CTrasladoComprobanteContenido contendio = new CTrasladoComprobanteContenido();

	public CTrasladoComprobanteContenido Contenido
	{
		get { return contendio; }
		set { contendio = value; }
	}

	public CTrasladoComprobante()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		Error.Add("Contenido", contendio.Validar());
		return Error;
	}

}