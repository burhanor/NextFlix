using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace NextFlix.API.Transformers
{
	public class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
	{
		public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
		{
			var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
			if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer" || authScheme.Name == "Cookie"))
			{
				var requirements = new Dictionary<string, OpenApiSecurityScheme>
				{
					["Bearer"] = new OpenApiSecurityScheme
					{
						Type = SecuritySchemeType.Http,
						Scheme = "bearer",
						In = ParameterLocation.Header,
						BearerFormat = "Json Web Token"
					},
					["Cookie"] = new OpenApiSecurityScheme
					{
						Type = SecuritySchemeType.Http,
						Scheme = "cookie",
						In = ParameterLocation.Cookie
					}
				};
				document.Components ??= new OpenApiComponents();
				document.Components.SecuritySchemes = requirements;
			}
			document.Info = new()
			{
				Title = "My API Bearer and Cookie scheme",
				Version = "v1",
				Description = "API for Damien"
			};
		}
	}
}
