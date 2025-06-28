using Spectrum.MiddleWare;

namespace Spectrum
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var app = builder.Build();
			app.UseMiddleware<RequestGetter>();
			app.Run();
		}
	}
}
