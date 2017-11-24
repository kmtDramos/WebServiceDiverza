using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comprobante
/// </summary>
public class CComprobante
{

	// Propiedades default
	private string serie = "";
	private DateTime fecha = default(DateTime);
	private DateTime fechapago = default(DateTime);
	private string condiciondepago = "";
	private string nocertificado = "";
	private string certificado = "";
	private decimal subtotal = 0;
	private decimal tipocambio = 0;
	private string moneda = "";
	private decimal total = 0;
	private string tipodecomprobante = "";
	private string lugarexpedicion = "";
	private string sello = "";
	private CEmisor emisor = new CEmisor();
	private CReceptor receptor = new CReceptor();
	private CConcepto[] conceptos = { };
	private CImpuestoComprobante impuesto = new CImpuestoComprobante();

	// Propiedades utilitarias
	private string error = "";
	private bool valido = false;

	#region Descripcion
	// Getters y Setter
	public string Serie
	{
		get { return serie; }
		set { serie = value; }
	}

	public DateTime Fecha
	{
		get { return fecha; }
		set { fecha = value; }
	}

	public DateTime FechaPago
	{
		get { return fechapago; }
		set { fechapago = value; }
	}

	public string CondicionDePago
	{
		get { return condiciondepago; }
		set { condiciondepago = value; }
	}

	public string NoCertificado
	{
		get { return nocertificado; }
		set { nocertificado = value; }
	}

	public decimal Subtotal
	{
		get { return subtotal; }
		set { subtotal = value; }
	}

	public decimal TipoCambio
	{
		get { return tipocambio; }
		set { tipocambio = value; }
	}
	
	public string Moneda
	{
		get { return moneda; }
		set { moneda = value; }
	}

	public decimal Total
	{
		get { return total; }
		set { total = value; }
	}

	public string TipoDeComprobante
	{
		get { return tipodecomprobante; }
		set { tipodecomprobante = value; }
	}

	public string LugarExpedicion
	{
		get { return lugarexpedicion; }
		set { lugarexpedicion = value; }
	}

	public string Sello
	{
		get { return sello; }
		set { sello = value; }
	}

	public CEmisor Emisor
	{
		get { return emisor; }
		set { emisor = value; }
	}

	public CReceptor Receptor
	{
		get { return receptor; }
		set { receptor = value; }
	}

	public CConcepto[] Conceptos
	{
		get { return conceptos; }
		set { conceptos = value; }
	}

	public CImpuestoComprobante Impuesto
	{
		get { return impuesto; }
		set { impuesto = value; }
	}

	// Getters y Setters utilitarios
	public string Error
	{
		get { return error; }
	}

	public bool Valido
	{
		get { return valido; }
	}

	// Constructor
	public CComprobante()
	{
		JObject Error = new JObject();
		Error.Add("Descripcion", "El comprobante no ha sido validado");
		error = Error.ToString();
	}

	#endregion Descripcion

	// Funciones
	public void Validar()
	{
		JObject Error = new JObject();

		JObject validacion = new JObject();

		if (serie == "") validacion.Add("Serie", "");
		if (fecha == default(DateTime)) validacion.Add("Fecha", "");
		if (fechapago == default(DateTime)) validacion.Add("FechaPago", "");
		if (condiciondepago == "") validacion.Add("CondicionDePago", "");
		if (subtotal == 0) validacion.Add("Subtotal", "");
		if (tipocambio == 0) validacion.Add("TipoCambio", "");
		if (moneda == "") validacion.Add("Mondea", "");
		if (total == 0) validacion.Add("Total", "");
		if (tipodecomprobante == "") validacion.Add("TipoDeComprobante", "");
		if (lugarexpedicion == "") validacion.Add("LugarExpedicion", "");
		if (emisor.Nombre == "") validacion.Add("EmisorNombre", "");
		if (emisor.RFC == "") validacion.Add("EmisorRFC", "");
		if (emisor.RegimenFiscal == "") validacion.Add("EmisorRegimenFiscal", "");
		if (receptor.Nombre == "") validacion.Add("ReceptroNombre", "");
		if (receptor.RFC == "") validacion.Add("ReceptorRFC", "");
		if (receptor.UsoCFDI == "") validacion.Add("ReceptorUsoCFDI", "");
		int i = 0;
		foreach (CConcepto concepto in conceptos)
		{
			validacion.Add("Concepto["+ i +"]", concepto.Validar());
			i++;
		}
		validacion.Add("Impuesto", impuesto.Validar());

		error = validacion.ToString();
	}

}