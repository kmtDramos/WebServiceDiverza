﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CConceptoNotaCredito
{

    private string importe = "";
    private decimal valorunitario = 0;
    private string descripcion = "";
    private decimal cantidad = 0;
    private string claveunidad = "";
    private string claveprodserv = "";
    private CImpuestoConceptoNotaCredito impuestos = new CImpuestoConceptoNotaCredito();

    public string Importe
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

    public CImpuestoConceptoNotaCredito Impuestos
    {
        get { return impuestos; }
        set { impuestos = value; }
    }

    public JObject Validar()
    {
        JObject Error = new JObject();
        return Error;
    }

}