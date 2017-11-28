using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CConceptoPago
{

    private decimal importe = 0;
    private decimal valorunitario = 0;
    private string descripcion = "";
    private decimal cantidad = 0;
    private string claveunidad = "";
    private string claveprodserv = "";

    public decimal Importe
    {
        get { return importe; }
        set { importe = value; }
    }

    public decimal ValorUnitario
    {
        get { return valorunitario; }
        set { valorunitario = value; }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }

    public decimal Cantidad
    {
        get { return cantidad; }
        set { cantidad = value; }
    }

    public string ClaveUnidad
    {
        get { return claveunidad; }
        set { claveunidad = value; }
    }

    public string ClaveProdServ
    {
        get { return claveprodserv; }
        set { claveprodserv = value; }
    }


    public JObject Validar()
    {
        JObject Error = new JObject();
        return Error;
    }

}