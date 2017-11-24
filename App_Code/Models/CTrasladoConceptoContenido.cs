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

	public JObject Validar ()
	{
		JObject Error = new JObject();
		if (_base == 0) Error.Add("Base", "La base del traslado de concepto no esta definida.");
		if (tipofactor == "") Error.Add("TipoFactor", "El tipo de factor del concepto no esta definido.");
		if (tasaocuota == 0) Error.Add("TasaOCuota", "La tasa o cuota del concepto no esta definido.");
		if (impuesto == "") Error.Add("Impuesto", "El impuesto del concepto no esta definido");
		if (importe == 0) Error.Add("Importe", "El importe del concepto no esta definido");
		return Error;
	}

}