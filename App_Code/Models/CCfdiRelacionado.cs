﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CCfdiRelacionado
/// </summary>
public class CCfdiRelacionado
{
    private string cfdiRelacionado = "";

    // Propiedades utilitarias
    private string error = "";
    private bool valido = false;

    #region Descripcion
    // Getters y Setter

    public string CfdiRelacionado
    {
        get { return cfdiRelacionado; }
        set { cfdiRelacionado = value; }
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