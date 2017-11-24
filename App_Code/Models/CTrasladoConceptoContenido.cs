using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CTrasladoConceptoContenido
{

	private decimal _base = 0;
	private string tipofactor = "";
	private decimal tasaocuota = 0;
	private string impuesto = "";
	private decimal importe = 0;

	public decimal Base
	{
		get { return _base; }
		set { _base = value; }
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

	public string Impuesto
	{
		get { return impuesto; }
		set { impuesto = value; }
	}

	public decimal Importe
	{
		get { return importe; }
		set { importe = value; }
	}

	public CTrasladoConceptoContenido()
	{

	}

}