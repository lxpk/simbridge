using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WebSession {
	public enum AuthenticationMode {
		None = 0,
		WebAPI = 1
	}
	
	private IWebAuthenticator authenticator;
	private string userName, password, baseUrl;
	
	public WebSession(string baseUrl) : this(baseUrl, AuthenticationMode.None, null, null) { }
	
	public WebSession(string baseUrl, WebSession.AuthenticationMode securityMode, string userName, string password) {
		this.baseUrl = baseUrl;
		this.userName = userName;
		this.password = password;
		authenticator = WebAuthenticatorFactory.Create(securityMode, baseUrl);
	}
	
	public bool CanAuthenticate() {
		if(authenticator != null) {
			return authenticator.Authenticate(userName, password);
		}
		
		return false;
	}
	
	public WWW MakeRequest(string path, WWWForm formData, Hashtable headers) {
		if(authenticator != null) {
			if(!authenticator.IsAuthenticated() && authenticator.Authenticate(userName, password)) {
				throw new UnityException("Unable to authenticate user");
			}
			
			if(authenticator.IsAuthenticated()) {
				if(headers == null)
					headers = new Hashtable();
				
				foreach(DictionaryEntry authHeader in authenticator.GetAuthHeaders()) {
					headers.Add(authHeader.Key, authHeader.Value);
				}
			}
		}
		
		if(formData == null) {
			//HACK:Use with dictionary instead?//return new WWW(baseUrl + path, null, headers);
		} else {
			//return new WWW(baseUrl + path, formData.data, headers);
		}
		return new WWW(baseUrl+path);
	}
}
