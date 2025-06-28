using Spectrum.SpectrumBase.Enumerators;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Spectrum.SpectrumBase.Helpers
{
	public static class ArgumentHelper
	{
		public static RequestMethod ConvertRequestMethod(string method)
		{
			switch (method)
			{
				case "GET":
					return RequestMethod.GET;
				case "POST":
					return RequestMethod.POST;
				case "PUT":
					return RequestMethod.PUT;
				case "PATCH":
					return RequestMethod.PATCH;
				case "DELETE":
					return RequestMethod.DELETE;
				default:
					throw new ArgumentException($"Method {method} is not supported");
			}
		}

		public static async Task<Dictionary<string, string>> ConvertQueryString(QueryString? queryString)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			await Task.Run(() =>
			{
				string? temp = queryString.ToString();

				if (temp == string.Empty || temp == null)
				{
					return;
				}

				temp = queryString.ToString();

				if (temp == null)
				{
					return;
				}

				temp = temp.Replace("?", "");

				string[] temp2 = temp.Split('&');

				foreach (string s in temp2)
				{
					string[] temp3 = s.Split('=');
					if (temp3.Length == 2)
						result.Add(temp3[0], temp3[1]);
					if (temp3.Length == 1)
						result.Add(temp3[0], "");
				}
			});

			return result;
		}


		public static async Task<string?> ConvertBody(Stream body)
		{
			string? result;

			using var reader = new StreamReader(
			body,
			encoding: Encoding.UTF8,
			detectEncodingFromByteOrderMarks: false,
			bufferSize: 1024,
			leaveOpen: true);

			result = await reader.ReadToEndAsync();

			return result;
		}
	}
}
