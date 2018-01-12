using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CCfdiRelacionado
/// </summary>
public class CCfdiRelacionado
{
    private string tipoRelacion = "";
    private string uuid = "";

    // Propiedades utilitarias
    private string error = "";
    private bool valido = false;

    #region Descripcion
    // Getters y Setter

    public string TipoRelacion
    {
        get { return tipoRelacion; }
        set { tipoRelacion = value; }
    }
    public string UUID
    {
        get { return uuid; }
        set { uuid = value; }
    }

    // Getters y Setters utilitarios
    public string Error
    {
        get { return error; }
    }

    public bool Valido
    {
        get { return valido; }
    }

    #endregion Descripcion

    // Funciones
    public void Validar()
    {
        JObject validacion = new JObject();
        error = validacion.ToString();
    }
}