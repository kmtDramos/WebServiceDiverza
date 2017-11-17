using System;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Inicio : System.Web.UI.Page
{
    //Manda llamar al Conector del WebService Diverza
    [WebMethod]
    public static string LoadWeb(string data)
    {
        return Conector.Connect(data); 
    }
}