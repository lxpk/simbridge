#if false // This script is temporarily disabled, enable in project window
using UnityEngine;
using System.Collections;

//Written by Matt Gonzalez
//with help from Nicholas H.Tollervey's ConsoleBot2.5
//and AIML base files from the A.L.I.C.E Bot project

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMLbot;

public class Alice : MonoBehaviour {
	
	private Chatbot.alice aliceBot;
	public String question = "Hello world";
	public String answer = "";
	
	public String textInput;
	public String chatLog;
	public String context;
	
	public String yourName = "YOU";
	public String botName = "ADA";
	
	// Use this for initialization
	void Start () {
		aliceBot = new Chatbot.alice();
		aliceBot.Initialize();
		//answer = aliceBot.getOutput( question );
	}
	
	// Update is called once per frame
	void Update () {
		Listen ();
	}
	
	void Speak () 
	{
		answer = aliceBot.getOutput( PlayerPrefs.GetString( "messageSent") );
		PlayerPrefs.SetString( "messageReceived", answer );
	}
	
	void Listen () 
	{
		if ( PlayerPrefs.GetString( "messageSent" ) != null && PlayerPrefs.GetString ( "messageSent" ) != "" )
		{
			Speak ( );
		}
		PlayerPrefs.DeleteKey( "messageSent" );
	} 
		
	void OnGUI ()
	{
//		GUILayout.BeginArea( new Rect( 0,Screen.height/12,Screen.width, Screen.height/10 ) );
//		{
//			GUILayout.BeginVertical();
//			{
//				GUILayout.BeginScrollView( new Vector2( 0, 999999999f ) );
//				{
//					GUILayout.TextArea( chatLog );
//				}
//				GUILayout.EndScrollView();
//				GUILayout.BeginHorizontal();
//				{
//					textInput = GUILayout.TextField( textInput );
//					if ( GUILayout.Button("SAY") )
//					{
//						answer = aliceBot.getOutput( textInput );
//						chatLog += System.Environment.NewLine + yourName + ": " + textInput;
//						chatLog += System.Environment.NewLine + botName + ": " + answer;
//						PlayerPrefs.SetString("UnitySpeak", answer );
//						textInput = "";
//					}
//				}
//				GUILayout.EndHorizontal();
//			}
//			GUILayout.EndVertical();
//		}
//		GUILayout.EndArea();
	}
}



namespace Chatbot
{
	class alice
	{
		private Bot myBot;
		private User myUser;
			
		/// <summary>
		/// Create a new instance of the ALICE object
		/// </summary>
		public alice()
		{ 
			myBot = new Bot();
			myUser = new User("consoleUser", myBot);
		}
			
		/// <summary>
		/// This initialization can be put in the alice() method
		/// but I kept it seperate due to the nature of my program.
		/// This method loads all the AIML files located in the \AIML folder
		/// </summary>
		public void Initialize()
		{
			Debug.Log ("AIML at "+myBot.PathToAIML);
			Debug.Log ("config at "+myBot.PathToConfigFiles);
			myBot.loadSettings();
			myBot.isAcceptingUserInput = false;
			myBot.loadAIMLFromFiles();
			myBot.isAcceptingUserInput = true;
		}
			
		/// <summary>
		/// This method takes an input string, then finds a response using the the AIMLbot library and returns it
		/// </summary>
		/// <param name="input">Input Text</param>
		/// <returns>Response</returns>
		public String getOutput(String input)
		{
			Request r = new Request(input, myUser, myBot);
			Result res = myBot.Chat(r);
			return(res.Output);
		}
 	}
}
#endif // Currently disabled script
