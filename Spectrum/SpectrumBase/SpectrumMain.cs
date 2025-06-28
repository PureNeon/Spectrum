using Spectrum.SpectrumBase.Enumerators;
using Spectrum.SpectrumBase.Helpers;
using System.Collections;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using static Spectrum.SpectrumBase.Helpers.SpectrumHelper;

namespace Spectrum.SpectrumBase
{
	public static class SpectrumMain
	{
		public static async Task MainWork(HttpContext context)
		{
			string path = context.Request.Path;

			path = path.Replace('/', '\\');
			path = path.Remove(0, 1);

			RequestMethod method = ArgumentHelper.ConvertRequestMethod(context.Request.Method);

			Dictionary<string, string>? queryString = await ArgumentHelper.ConvertQueryString(context.Request.QueryString);

			string? body = await ArgumentHelper.ConvertBody(context.Request.Body);

			Console.WriteLine($"Spectrum log:\nPath is {path}\nMethod is {method}\nQuery string is {queryString}\nBody is {body}");

			Run(context, method, path, queryString, body);
		}
	}
}
