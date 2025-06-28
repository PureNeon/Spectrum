using Microsoft.AspNetCore.Http;
using Spectrum.SpectrumBase.Enumerators;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Spectrum.SpectrumBase.Helpers
{
	public static class SpectrumHelper
	{
		public static async void Run(HttpContext context, RequestMethod method, string path, Dictionary<string, string>? queryString, string? body)
		{
			string fullPath = AppContext.BaseDirectory + path + "\\Index.dll";

			Console.WriteLine(fullPath);

			Assembly pluginAssembly;

			if (File.Exists(fullPath))
			{
				pluginAssembly = Assembly.LoadFrom(fullPath);

				// Получение типа класса
				Type pluginType = pluginAssembly.GetType("Index.Index");

				// Создание экземпляра класса
				object pluginInstance = Activator.CreateInstance(pluginType);

				// Получение метода

				MethodInfo pluginMethod = pluginType.GetMethod(method.ToString());

				object result = null;

				if (pluginMethod != null)
					// Вызов метода
					if (method == RequestMethod.GET)
						result = pluginMethod.Invoke(pluginInstance, new object[] { queryString });
					else
						result = pluginMethod.Invoke(pluginInstance, new object[] { queryString, body });

				if (result != null)
				{
					Dictionary<string, object> re = (Dictionary<string, object>)result;

					if (re.ContainsKey("Code"))
					{
						context.Response.StatusCode = (int)re["Code"];
					}

					if (re.ContainsKey("Body"))
					{
						context.Response.WriteAsync(re["Body"].ToString());
					}
				}
				else
				{
					MethodNotImplemented(context, method.ToString(), path);
				}
			}
			else
			{
				MethodNotImplemented(context, method.ToString(), path);
			}
		}

		public static async void MethodNotImplemented(HttpContext context, string method, string path)
		{
			context.Response.StatusCode = 404;
			context.Response.WriteAsync($"The method {method} is not implemented by the \"{path}\" path");
		}

		public static bool IsValidJson(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
				return false;

			try
			{
				using (JsonDocument doc = JsonDocument.Parse(json))
					return true;
			}
			catch (JsonException)
			{
				return false;
			}
		}
	}
}
