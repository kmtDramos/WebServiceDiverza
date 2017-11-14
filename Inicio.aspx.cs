using System;
using System.Web.Services;

public partial class Inicio : System.Web.UI.Page
{

    //Manda llamar al Conector del WebService Diverza
    [WebMethod]
    public static string LoadWeb(string data)
    {
        return Conector.Connect(data); 
    }
}