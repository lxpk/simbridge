using System;

namespace AssemblyCSharp
{
	public static class WebAuthenticatorFactory
	{
		public static IWebAuthenticator Create(WebSession.AuthenticationMode mode, string baseUrl)
		{
			if(mode == WebSession.AuthenticationMode.WebAPI) {
				return new WebApiAuthenticator(baseUrl);
			}
			return null;
		}
	}
}

