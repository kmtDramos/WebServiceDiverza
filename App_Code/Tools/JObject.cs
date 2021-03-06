﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class JObject
{ 
	private Dictionary<string, object> obj;

	public JObject()
	{
		obj = new Dictionary<string, object>();
	}

	public void Add(string Property, JObject Value)
	{
		obj.Add(Property, Value.ToDictionary());
	}

	public void Add(string Property, JArray Value)
	{
		obj.Add(Property, Value.ToList());
	}

	public void Add(string Property, object Value)
	{
		obj.Add(Property, Value);
	}

	public void Remove(string Property)
	{
		obj.Remove(Property);
	}
	
	public object Get(string Property)
	{
		return obj[Property];
	}

	public bool Exist(string Property)
	{
		return obj.ContainsKey(Property);
	}
	
	public JObject FromObject (object obj)
	{
		this.obj = (Dictionary<string, object>)obj;
		return this;
	}

	public JObject FromDictionary(Dictionary<string, object> dic)
	{
		this.obj = dic;
		return this;
	}

	public JObject FromString(string json)
	{
		return this.FromObject(JSON.Parse(json));
	}

	public Dictionary<string, object> ToDictionary()
	{
		return obj;
	}

	override public string ToString()
	{
		string json = JSON.Stringify(obj);
		json = json.Replace(@"u003c", "<");
		json = json.Replace(@"u003e", ">");
		json = json.Replace(@"\r\n", System.Environment.NewLine);
		return json;
	}

}