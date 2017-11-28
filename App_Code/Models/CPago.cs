using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comprobante
/// </summary>
public class CPago
{

    // Propiedades default
    private string serie = "";
    private DateTime fecha = default(DateTime);
    private string nocertificado = "";
    private string certificado = "";
    private decimal subtotal = 0;
    private decimal total = 0;
    private string tipodecomprobante = "";
    private string lugarexpedicion = "";
    private string sello = "";
    private CEmisor emisor = new CEmisor();
    private CReceptor receptor = new CReceptor();
    private CConceptoPago conceptopago = new CConceptoPago();
    private CComplemento complementos = new CComplemento();

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

    public string Fecha
    {
        get { return fecha.ToString("o").Substring(0, 19); }
        set { fecha = DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind); }
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

    public CConceptoPago Concepto
    {
        get { return conceptopago; }
        set { conceptopago = value; }
    }

    public CComplemento Complementos
    {
        get { return complementos; }
        set { complementos = value; }
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

    #endregion Descripcion

    // Funciones
    public void Validar()
    {
        JObject validacion = new JObject();
        error = validacion.ToString();
    }

}