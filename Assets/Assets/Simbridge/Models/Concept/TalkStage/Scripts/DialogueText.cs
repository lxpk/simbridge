using UnityEngine; 
using System.Collections;

public class DialogueText : MonoBehaviour {
	private float startTime;
	private float restSeconds;
	private int roundedRestSeconds;
	private float displaySeconds;
	private float displayMinutes;
	public int CountDownSeconds=120;
	private float Timeleft;
	string timetext;
	
	public TextMesh textMesh;
	
	// Use this for initialization
	
	void Start () 
	{
	    startTime=Time.deltaTime;
	
	}
	
	void OnGUI()
	{
		
	    Timeleft= Time.time-startTime;
	
	    restSeconds = CountDownSeconds-(Timeleft);
	
		roundedRestSeconds=Mathf.CeilToInt(restSeconds);
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = (roundedRestSeconds / 60)%60;
		
		timetext = (displayMinutes.ToString()+":");
		if (displaySeconds > 9)
		{
		    timetext = timetext + displaySeconds.ToString();
		}
		else 
		{
		    timetext = timetext + "0" + displaySeconds.ToString();
		}
		if ( textMesh != null )
		{
			textMesh.text = timetext;
		}
		else
		{
			//GUI.Label(new Rect(650.0f, 0.0f, 100.0f, 75.0f), PlayerPrefs.GetString("currentText");
		}
    }
}