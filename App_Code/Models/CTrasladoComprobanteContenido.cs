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

	public string Impuresto
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

}