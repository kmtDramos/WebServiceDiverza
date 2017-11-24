using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladoComprobanteContenido
{

	private string impuesto = "";
	private string tipofactor = "";
	private decimal tasaocuota = 0;
	private decimal importe = 0;

	public string Impuesto
	{
		get { return impuesto; }
		set { impuesto = value; }
	}

	public string TipoFactor
	{
		get { return tipofactor; }
		set { tipofactor = value; }
	}

	public decimal TasaOCuota
	{
		get { return tasaocuota; }
		set { tasaocuota = value; }
	}

	public decimal Importe
	{
		get { return importe; }
		set { importe = value; }
	}

	public CTrasladoComprobanteContenido()
	{

	}

	public JObject Validar ()
	{
		JObject Error = new JObject();
		if (impuesto == "") Error.Add("Impuresto", "El impuesto no esta definido");
		if (tipofactor == "") Error.Add("TipoFactor", "El tipo de factor no esta definido");
		if (tasaocuota == 0) Error.Add("TasaOCuota", "La tasa o cuota no esta definida");
		if (importe == 0) Error.Add("Importe", "El importe no esta definido");
		return Error;
	}

}