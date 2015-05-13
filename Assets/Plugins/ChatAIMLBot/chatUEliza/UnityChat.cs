using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMLbot;

public class UnityChat : MonoBehaviour {
	
	public String messageSent = "";
	public String messageReceived = "";
	
	public String question = "Hello world";
	public String answer = "";
	
	public String textInput;
	public String chatLog;
	public String context;
	
	public String yourName = "YOU";
	public String botName = "ADA";
	
	public bool showChat = false;
	
	// Use this for initialization
	void Start () {
	
	}
		
	void OnGUI ()
	{
		if ( showChat )
		{
			GUILayout.BeginArea( new Rect( 0, Screen.height/12*10, Screen.width/3, Screen.height/12*2 ) );
			{
				GUILayout.BeginVertical();
				{
					GUILayout.BeginScrollView( new Vector2( 0, 999999999f ) );
					{
						GUILayout.TextArea( chatLog );
					}
					GUILayout.EndScrollView();
					GUILayout.BeginHorizontal();
					{
						textInput = GUILayout.TextField( textInput );
						if ( GUILayout.Button("SAY", GUILayout.Width ( 100f ) ) )
						{
							Say ( );
							textInput = "";
						}
					}
					GUILayout.EndHorizontal();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Listen ();
	}
	
	void Say ()
	{
		PlayerPrefs.SetString ( "messageSent", textInput );
		chatLog += System.Environment.NewLine + yourName + ": " + textInput;
	}
	
	void Hear () 
	{
		messageReceived = PlayerPrefs.GetString ( "messageReceived" );
		chatLog += System.Environment.NewLine + botName + ": " + PlayerPrefs.GetString ( "messageReceived" );
		PlayerPrefs.SetString("UnitySpeak", messageReceived );
		PlayerPrefs.DeleteKey( "messageReceived" );
	}
	
	void Listen () 
	{
		if ( PlayerPrefs.GetString ( "messageReceived" ) != null && PlayerPrefs.GetString ( "messageReceived" ) != "" )
		{
			Hear ();
		}
		PlayerPrefs.DeleteKey ( "messageReceived" );
	}
}
