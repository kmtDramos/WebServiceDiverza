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
	private string folio = "";
	private DateTime fecha = default(DateTime);
	private string formapago = "";
	private string condiciondepago = "";
	private string nocertificado = "";
	private string certificado = "";
	private decimal subtotal = 0;
	private decimal tipocambio = 0;
	private string moneda = "";
	private decimal total = 0;
    private decimal descuento = 0;
	private string tipodecomprobante = "";
	private string metodopago = "";
	private string lugarexpedicion = "";
	private string sello = "";
	private CEmisor emisor = new CEmisor();
	private CReceptor receptor = new CReceptor();
	private CConcepto[] conceptos;
	private CImpuestoComprobante impuestos = new CImpuestoComprobante();
    private CCfdiRelacionado cfdiRelacionado = new CCfdiRelacionado();

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

	public string Folio
	{
		get { return folio; }
		set { folio = value; }
	}

	public string Fecha
	{
		get { return fecha.ToString("o").Substring(0, 19); }
		set { fecha = DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind); }
	}

	public string FormaPago
	{
		get { return formapago; }
		set { formapago = value; }
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

	public string Certificado
	{
		get { return certificado; }
		set { certificado = value; }
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

    public decimal Descuento
    {
        get { return descuento; }
        set { descuento = value; }
    }

    public string TipoDeComprobante
	{
		get { return tipodecomprobante; }
		set { tipodecomprobante = value; }
	}

	public string MetodoPago
	{
		get { return metodopago; }
		set { metodopago = value; }
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

	public CImpuestoComprobante Impuestos
	{
		get { return impuestos; }
		set { impuestos = value; }
	}

    public CCfdiRelacionado CFDIRelacionado
    {
        get { return cfdiRelacionado; }
        set { cfdiRelacionado = value; }
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

	}

	#endregion Descripcion

	// Funciones
	public void Validar()
	{
		JObject validacion = new JObject();
		error = validacion.ToString();
	}

}