using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CReceptor
{

	private string nombre = "";
	private string rfc = "";
	private string usocfdi = "";

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
	
	public string UsoCFDI
	{
		get { return usocfdi; }
		set { usocfdi = value; }
	}

	public CReceptor()
	{

	}

}