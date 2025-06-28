using Spectrum.SpectrumBase.Enumerators;
using Spectrum.SpectrumBase.Helpers;

using static Spectrum.SpectrumBase.SpectrumMain;

namespace Spectrum.MiddleWare
{
	public class RequestGetter
	{
		private readonly RequestDelegate _next;

		public RequestGetter(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			await MainWork(context);
		}
	}
}