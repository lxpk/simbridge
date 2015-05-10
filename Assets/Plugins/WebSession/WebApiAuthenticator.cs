using UnityEngine;
using System;
using System.Collections;

namespace AssemblyCSharp
{
	public class WebApiAuthenticator : IWebAuthenticator
	{
		private class WebApiToken {
			public string AccessCode { get; set; }
			public DateTime ExpirationTime { get; set; }
		}
		
		private WebApiToken token;
		private string authUrl;
		
		public WebApiAuthenticator (string baseUrl)
		{
			
			authUrl = baseUrl + "/Token";
		}
		
		public bool IsAuthenticated() {
			return token != null && token.ExpirationTime > DateTime.Now;
		}
		
		public bool Authenticate(string userName, string password) {
			WWWForm form = new WWWForm();
			form.AddField ("grant_type", "password");
			form.AddField ("UserName", userName);
			form.AddField ("Password", password);
			
			var www = new WWW(authUrl, form);
			while(!www.isDone) {}
			
			if(www.error == null) {
				token = ParseResponse(www.text);
				return true;
			} else {
				token = null;	
			}
			
			return false;
		}
		
		//Response is a JSON object
		//"access_token":"boQtj0SCGz2GFGz[...]",
	    //"token_type":"bearer",
	    //"expires_in":1209599,
	    //"userName":"Alice",
	    //".issued":"Mon, 14 Oct 2013 06:53:32 GMT",
	    //".expires":"Mon, 28 Oct 2013 06:53:32 GMT"
		private WebApiToken ParseResponse(string text) {
			var result = SimpleJSON.JSON.Parse (text);
			return token = new WebApiToken() {
				AccessCode = result["access_token"].Value,
				ExpirationTime = DateTime.Parse (result[".expires"].Value)
			};
		}
		
		public Hashtable GetAuthHeaders() {
			if(IsAuthenticated()) {
				return new Hashtable() {
					{ "Authorization", String.Format ("Bearer {0}", token.AccessCode) }
				};
			} else {
				return null;
			}
		}
	}
}

