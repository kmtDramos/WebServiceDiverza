﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CConcepto
{

	private string importe = "";
	private decimal valorunitario = 0;
	private string unidad = "unidad";
	private string descripcion = "";
	private decimal cantidad = 0;
	private string claveunidad = "";
	private string claveprodserv = "";
    private decimal descuento = 0;
    private CImpuestoConcepto impuestos = new CImpuestoConcepto();

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

	public string Unidad
	{
		get { return unidad; }
		set { unidad = value; }
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

    public decimal Descuento
    {
        get { return descuento; }
        set { descuento = value; }
    }

    public CImpuestoConcepto Impuestos
	{
		get { return impuestos; }
		set { impuestos = value; }
	}

	public CConcepto()
	{

	}

	public JObject Validar()
	{
		JObject Error = new JObject();
		return Error;
	}

}