using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;

/// <summary>
/// Summary description for Zip
/// </summary>
public class Zip
{
	
	public static string Read (ZipArchiveEntry file)
	{
		string content = "";

		Stream stream = file.Open();
		StreamReader reader = new StreamReader(stream);

		content = reader.ReadToEnd();

		return content;
	}

}