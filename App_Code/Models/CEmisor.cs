using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CEmisor
{

	private string nombre = "";
	private string rfc = "";
	private string regimenfiscal = "";

	public string Nombre
	{
		get { return nombre; }
		set { nombre = value; }
	}

	public string RFC
	{
		get { return rfc; }
		set { rfc = value; }
	}

	public string RegimenFiscal
	{
		get { return regimenfiscal; }
		set { regimenfiscal = value; }
	}

	public CEmisor()
	{

	}

}