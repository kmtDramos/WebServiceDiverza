using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Facturacion : System.Web.UI.Page
{
	
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	[WebMethod]
	public static string TimbrarFactura(Dictionary<string, object> Comprobante)
	{
		JObject Respuesta = new JObject();

		int Error = 0;
		string DescripcionError = "";

		try
		{
			CComprobante comprobante = new CComprobante();
			
			Respuesta.Add("Comprobante", comprobante);

		}
		catch (Exception ex)
		{
			Error = 1;
			DescripcionError = ex.Message;
		}

		Respuesta.Add("Error", Error);
		Respuesta.Add("Descripcion", DescripcionError);

		return Respuesta.ToString();
	}
	
}