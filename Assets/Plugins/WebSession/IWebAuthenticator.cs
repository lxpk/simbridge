using System;
using System.Collections;

namespace AssemblyCSharp
{
	public interface IWebAuthenticator
	{
		bool IsAuthenticated();
		bool Authenticate(string userName, string password);
		Hashtable GetAuthHeaders();
	}
}

