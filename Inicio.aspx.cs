using System;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class Inicio : System.Web.UI.Page
{
    //Manda llamar al Conector del WebService Diverza
    [WebMethod]
    public static string Emitir(string data)
    {
        string xml = XML.XMLEmitir(data);
        return Conector.Emitir(xml); 
    }

    [WebMethod]
    public static string Cancelar(string uuid)
    {
        return Conector.Cancelar(uuid);

    }

    [WebMethod]
    public static string NotaCredito(string data)
    {
        string xml = XML.XMLNotasCredito(data);
        return Conector.NotaCredito(xml);
    }

    [WebMethod]
    public static string Pago(string data)
    {
        string xml = XML.XMLPagos(data);
        return Conector.Pago(xml);
    }
}