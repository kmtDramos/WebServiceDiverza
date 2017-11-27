using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Base64
/// </summary>
public class Base64
{

	public static string Encode (string Content)
	{
		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Content);
		return System.Convert.ToBase64String(bytes);
	}

	public static string Decode (string Hash)
	{
		byte[] bytes = System.Convert.FromBase64String(Hash);
		return System.Text.Encoding.UTF8.GetString(bytes);
	}

}