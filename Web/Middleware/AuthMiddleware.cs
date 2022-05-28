using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api_ja_cheguei_mae.Atributttes;
using api_ja_cheguei_mae.Services.JWTService;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

//THIS CLASS IS BASED ON https://mithunvp.com/write-custom-asp-net-core-middleware-web-api/
//THANKS FOR THIS AWESOME ARTICLE :)
// 21/10/2021
namespace api_ja_cheguei_mae.Middlewares
{

	public class AuthMiddleware
	{
		private readonly RequestDelegate _request;
		private readonly IJWTService _jwtService;

        public AuthMiddleware(RequestDelegate request, IJWTService jwtService)
        {
            _request = request;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context)
		{
			context.Response.ContentType = "application/json";
			var ContextAttribute = context.GetEndpoint()?.Metadata?.OfType<RequireAuth>().FirstOrDefault();
			if(ContextAttribute != null)
            {
				bool hasKey = context.Request.Headers.Keys.Contains("Authorization");
				if (!hasKey)
				{
					await context.Response.WriteAsync(JsonConvert.SerializeObject(new
					{
						error = "BAD REQUEST. INSIRA UM TOKEN."
					}));
				}
				else
				{
					string token = context.Request.Headers["Authorization"].ToString().Split(' ')[1];
					 _jwtService.ValidarJWT(token, context);

					await _request.Invoke(context);
				}
			}
            else
            {
				await _request.Invoke(context);
			}
		}

	}
}
