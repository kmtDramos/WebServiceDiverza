using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CCfdiRelacionado
/// </summary>
public class CCfdiRelacionados
{
    private string tipoRelacion = "";
    private CCfdiRelacionado cfdiRelacionado = new CCfdiRelacionado();

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

    public CCfdiRelacionado cfdiRelacionados
    {
        get { return cfdiRelacionados; }
        set { cfdiRelacionados = value; }
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