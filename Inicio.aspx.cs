using System;
using System.Web.Services;

public partial class Inicio : System.Web.UI.Page
{
    //PageLoad aspx
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime FechaActual = DateTime.Now;
        FechaActual = FechaActual.ToUniversalTime();
        caja.InnerHtml = "Fecha Actual | " + Convert.ToString(FechaActual.ToShortDateString());
    }

    //Manda llamar al Conector del WebService Diverza
    [WebMethod]
    public static string LoadWeb(string pUsuario, string pContrasena)
    {
        string result = Conector.Run();
        //caja.InnerHtml = result;
        return result;
       
    }
}