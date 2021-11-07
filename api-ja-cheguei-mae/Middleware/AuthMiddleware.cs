using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api_ja_cheguei_mae.Atributttes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//THIS CLASS IS BASED ON https://mithunvp.com/write-custom-asp-net-core-middleware-web-api/
//THANKS FOR THIS AWESOME ARTICLE :)
// 21/10/2021
namespace api_ja_cheguei_mae.Middlewares
{

	public class AuthMiddleware
	{
		private readonly RequestDelegate _request;

		public AuthMiddleware(RequestDelegate request)
		{
			_request = request;
		}
		public async Task Invoke(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			var ContextAttribute = context.GetEndpoint()?.Metadata?.OfType<RequireAuth>().FirstOrDefault();
			if(ContextAttribute != null)
            {

				context.Request.Headers.ToList().ForEach((v)=> Debug.WriteLine($"{v.Key}-{v.Value}"));
				bool hasKey = context.Request.Headers.Keys.Contains("Authorization");
                if (hasKey)
				{
					string token = context.Request.Headers["Authorization"].ToString().Split(' ')[1];
					Debug.WriteLine(token.Split(' '));
				}
                               //prática ruim aqui --> testes precisam ser mais objetivos.
				await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = "BAD REQUEST. INSIRA UM TOKEN."
                }));
            }
            else
            {
				await _request.Invoke(context);
			}
		}

	}
}
