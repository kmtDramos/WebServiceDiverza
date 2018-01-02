using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CComplemento
/// </summary>
public class CComplemento
{
    private string fechaPago = "";
    private string formaDePagoP = "";
    private string monedaP = "";
    private decimal monto = 0;
    private string tipoCambioP = "";
    private string rfcEmisorCtaOrd = "";
    private string nomBancoOrdExt = "";
    private string ctaOrdenante = "";
    private string rfcEmisorCtaBen = "";
    private string ctaBeneficiario = "";
    private string tipoCadPago = "";
    private string certPago = "";
    private string cadPago = "";
    private string selloPago = "";
    private CDoctoRelacionado doctoRelacionados = new CDoctoRelacionado();
    
    public string FechaPago
    {
        get { return fechaPago; }
        set { fechaPago = value; }
    }

    public string FormaDePagoP
    {
        get { return formaDePagoP; }
        set { formaDePagoP = value; }
    }

    public string MonedaP
    {
        get { return monedaP; }
        set { monedaP = value; }
    }

    public decimal Monto
    {
        get { return monto; }
        set { monto = value; }
    }
    public string TipoCambioP
    {
        get { return tipoCambioP; }
        set { tipoCambioP = value; }
    }

    public string RfcEmisorCtaOrd
    {
        get { return rfcEmisorCtaOrd; }
        set { rfcEmisorCtaOrd = value; }
    }

    public string NomBancoOrdExt
    {
        get { return nomBancoOrdExt; }
        set { nomBancoOrdExt = value; }
    }

    public string CtaOrdenante
    {
        get { return ctaOrdenante; }
        set { ctaOrdenante = value; }
    }

    public string RfcEmisorCtaBen
    {
        get { return rfcEmisorCtaBen; }
        set { rfcEmisorCtaBen = value; }
    }
    public string CtaBeneficiario
    {
        get { return ctaBeneficiario; }
        set { ctaBeneficiario = value; }
    }

    public string TipoCadPago
    {
        get { return tipoCadPago; }
        set { tipoCadPago = value; }
    }

    public string CertPago
    {
        get { return certPago; }
        set { certPago = value; }
    }

    public string CadPago
    {
        get { return cadPago; }
        set { cadPago = value; }
    }

    public string SelloPago
    {
        get { return selloPago; }
        set { selloPago = value; }
    }

    public CDoctoRelacionado DoctoRelacionados
    {
        get { return doctoRelacionados; }
        set { doctoRelacionados = value; }
    }

    public JObject Validar()
    {
        JObject Error = new JObject();
        return Error;
    }
    
}