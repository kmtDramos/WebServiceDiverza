using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CDoctoRelacionado
/// </summary>
public class CDoctoRelacionado
{
    private string idDocumento = "";
    private string serie = "";
    private string folio = "";
    private string monedaDR = "";
    private string tipoCambioDR = "";
    private string metodoDePagoDR = "";
    private string numParcialidad = "";
    private decimal impSaldoAnt = 0;
    private decimal impPagado = 0;
    private decimal impSaldoInsoluto = 0;

    public string IdDocumento
    {
        get { return idDocumento; }
        set { idDocumento = value; }
    }
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

    public string MonedaDR
    {
        get { return monedaDR; }
        set { monedaDR = value; }
    }

    public string MetodoDePagoDR
    {
        get { return metodoDePagoDR; }
        set { metodoDePagoDR = value; }
    }
    public string TipoCambioDR
    {
        get { return tipoCambioDR; }
        set { tipoCambioDR = value; }
    }

    public string NumParcialidad
    {
        get { return numParcialidad; }
        set { numParcialidad = value; }
    }

    public decimal ImpSaldoAnt
    {
        get { return impSaldoAnt; }
        set { impSaldoAnt = value; }
    }

    public decimal ImpPagado
    {
        get { return impPagado; }
        set { impPagado = value; }
    }

    public decimal ImpSaldoInsoluto
    {
        get { return impSaldoInsoluto; }
        set { impSaldoInsoluto = value; }
    }
}