//http://www.cthreepo.com/games/eliza/
//
//document.open();
var message : String = "";
var yourMessage : String = "";
//public var unitySpeak : UnitySpeak;

//function OnGUI()
//{
//	GUI.Box( new Rect( 0.0f, 0.0f, 1000.0f, 100.0f), message );
//	yourMessage = GUI.TextArea( new Rect( 100.0f, 100.0f, 300.0f, 100.0f), yourMessage);
//	if ( GUI.Button( new Rect( 200.0f, 200.0f, 50.0f, 50.0f ), "Send" ) )
//	{
//		doeliza( yourMessage );
//	}
//}


function Update () {
	Listen ();
}

function Speak () 
{
	//answer = aliceBot.getOutput( messageReceived );
	doeliza( PlayerPrefs.GetString ( "messageSent" ) );
	// Reply() sets messageReceived now
	
	//PlayerPrefs.SetString( "messageReceived", answer );
}

function Listen () 
{
	if ( PlayerPrefs.GetString( "messageSent" ) != null && PlayerPrefs.GetString ( "messageSent" ) != "")
	{
		Speak ( );
	}
	PlayerPrefs.DeleteKey( "messageSent" );
}

//document.write('<form action="#" method="get" name="elizafrm" onsubmit="doeliza(document.elizafrm.you.value);return false;"><table border="1"><tr><td align="right">');
//document.write('You:</td><td><input name="you" type="text" size="64" /></td></tr>');
//document.write('<tr><td align="right" valign="top">Eliza:</td><td>');
//document.write('<div id="elresp" style="overflow:hidden;height:150px;">Hi, I\'m Eliza. What\'s your problem?</div>');
//document.write('</td></tr></table><input style="visibility:hidden;display:none;" name="" type="submit" value="Submit"/></form>');
//
var lastcommand="1234567";
/*
1000 REM *******************************
1010 REM *****PROGRAM DATA FOLLOWS******
1020 REM *******************************
1030 REM *********KEYWORDS**************
1049 REM *******************************
*/

var wordin=new Array(
" ARE "," WERE "," YOU "," YOUR"," I'VE "," I'M "," ME ");
var wordout=new Array(
" AM "," WAS "," I "," MY "," YOU'VE "," YOU'RE "," YOU ");

// Keywords are paired with replies by a reply code list
// This would be much more elegant in a data structure rather than separate arrays
var keywords=new Array("NOKEYFOUND", 
"CAN YOU ","CAN I ","YOU ARE ","YOU'RE ","I DON'T ","I FEEL ","WHY DON'T YOU ", 
"WHY CAN'T I ","ARE YOU ","I CAN'T ","I AM ","I'M ","YOU ","I WANT ","WHAT ","HOW ","WHO ","WHERE ","WHEN ","WHY ",
"NAME ","CAUSE ","SORRY ","DREAM ","HELLO ","HI ","MAYBE ","NO","YOUR ","ALWAYS ","THINK ","ALIKE ","YES ","FRIEND ","COMPUTER", "NOKEYFOUND");

var replies=new Array("",
"DON'T YOU BELIEVE THAT I CAN*",
"PERHAPS YOU WOULD LIKE TO BE LIKE ME*",
"YOU WANT ME TO BE ABLE TO*","PERHAPS YOU DON'T WANT TO*",
"DO YOU WANT TO BE ABLE TO*","WHAT MAKES YOU THINK I AM*",
"DOES IT PLEASE YOU TO BELIEVE I AM*",
"PERHAPS YOU WOULD LIKE TO BE*",
"DO YOU SOMETIMES WISH YOU WERE*",
"DON'T YOU REALLY*",
"WHY DON'T YOU*",
"DO YOU WISH TO BE ABLE TO*",
"DOES THAT TROUBLE YOU*",
"DO YOU OFTEN FEEL*",
"DO YOU OFTEN FEEL*",
"DO YOU ENJOY FEELING*",
"DO YOU REALLY BELIEVE I DON'T*",
"PERHAPS IN GOOD TIME I WILL*",
"DO YOU WANT ME TO*",
"DO YOU THINK YOU SHOULD BE ABLE TO*",
"WHY CAN'T YOU*",
"WHY ARE YOU INTERESTED IN WHETHER OR NOT I AM*",
"WOULD YOU PREFER IF I WERE NOT*",
"PERHAPS IN YOUR FANTASIES I AM*",
"HOW DO YOU KNOW YOU CAN'T*",
"HAVE YOU TRIED?",
//"PERHAPS YOU CAN NOW*","DID YOU COME TO ME BECAUSE YOU ARE*","HOW LONG HAVE YOU BEEN*",
"PERHAPS YOU CAN NOW*",
"DID YOU COME TO ME BECAUSE *",
"HOW LONG HAVE YOU BEEN*",
"DO YOU BELIEVE IT IS NORMAL TO BE*",
"DO YOU ENJOY BEING*",
"WE WERE DISCUSSING YOU, NOT ME.",
"OH, I*","YOU'RE NOT REALLY TALKING ABOUT ME, ARE YOU?",
"WHAT WOULD IT MEAN TO YOU IF YOU GOT*",
"WHY DO YOU WANT*","SUPPOSE YOU SOON GOT*",
"WHAT IF YOU NEVER GOT*","I SOMETIMES ALSO WANT*",
"WHY DO YOU ASK?","DOES THAT QUESTION INTEREST YOU?",
"WHAT ANSWER WOULD PLEASE YOU THE MOST?",
"WHAT DO YOU THINK?",
"ARE SUCH QUESTIONS ON YOUR MIND OFTEN?",
"WHAT IS IT THAT YOU REALLY WANT TO KNOW?",
"HAVE YOU ASKED ANYONE ELSE?",
"HAVE YOU ASKED SUCH QUESTIONS BEFORE?",
"WHAT ELSE COMES TO MIND WHEN YOU ASK THAT?",
"NAMES DON'T INTEREST ME.",
"I DON'T CARE ABOUT NAMES, PLEASE GO ON.",
"IS THAT THE REAL REASON?",
"DON'T ANY OTHER REASONS COME TO MIND?",
"DOES THAT REASON EXPLAIN ANYTHING ELSE?",
"WHAT OTHER REASONS MIGHT THERE BE?",
"PLEASE DON'T APOLOGIZE!",
"APOLOGIES ARE NOT NECESSARY.",
"WHAT FEELINGS DO YOU HAVE WHEN YOU APOLOGIZE?",
"DON'T BE SO DEFENSIVE!",
"WHAT DOES THAT DREAM SUGGEST TO YOU?",
"DO YOU DREAM OFTEN?",
"WHAT PERSONS APPEAR IN YOUR DREAMS?",
"ARE YOU DISTURBED BY YOUR DREAMS?",
"HOW DO YOU DO ...PLEASE STATE YOUR PROBLEM.",
"YOU DON'T SEEM QUITE CERTAIN.",
"WHY THE UNCERTAIN TONE?","CAN'T YOU BE MORE POSITIVE?",
"YOU AREN'T SURE?",
"DON'T YOU KNOW?","ARE YOU SAYING NO JUST TO BE NEGATIVE?",
"YOU ARE BEING A BIT NEGATIVE.",
"WHY NOT?","ARE YOU SURE?",
"WHY NO?",
"WHY ARE YOU CONCERNED ABOUT MY*",
"WHAT ABOUT YOUR OWN*",
"CAN YOU THINK OF A SPECIFIC EXAMPLE?",
"WHEN?",
"WHAT ARE YOU THINKING OF?",
"REALLY, ALWAYS?","DO YOU REALLY THINK SO?",
"BUT YOU ARE NOT SURE YOU*","DO YOU DOUBT YOU*",
"IN WHAT WAY?","WHAT RESEMBLANCE DO YOU SEE?",
"WHAT DOES THE SIMILARITY SUGGEST TO YOU?",
"WHAT OTHER CONNECTIONS DO YOU SEE?",
"COULD THERE REALLY BE SOME CONNECTION?",
"HOW?","YOU SEEM QUITE POSITIVE.",
"ARE YOU SURE?",
"I SEE.",
"I UNDERSTAND.",
"WHY DO YOU BRING UP THE TOPIC OF FRIENDS?",
"DO YOUR FRIENDS WORRY YOU?",
"DO YOUR FRIENDS PICK ON YOU?",
"ARE YOU SURE YOU HAVE ANY FRIENDS?",
"DO YOU IMPOSE ON YOUR FRIENDS?",
"PERHAPS YOUR LOVE FOR FRIENDS WORRIES YOU.",
"DO COMPUTERS WORRY YOU?",
"ARE YOU TALKING ABOUT ME IN PARTICULAR?",
"ARE YOU FRIGHTENED BY MACHINES?",
"WHY DO YOU MENTION COMPUTERS?",
"WHAT DO YOU THINK MACHINES HAVE TO DO WITH YOUR PROBLEM?",
"DON'T YOU THINK COMPUTERS CAN HELP PEOPLE?",
"WHAT IS IT ABOUT MACHINES THAT WORRIES YOU?",
"SAY, DO YOU HAVE ANY PSYCHOLOGICAL PROBLEMS?",
"WHAT DOES THAT SUGGEST TO YOU?",
"I SEE.","I'M NOT SURE I UNDERSTAND YOU FULLY.",
"COME COME ELUCIDATE YOUR THOUGHTS.",
"CAN YOU ELABORATE ON THAT?","THAT IS QUITE INTERESTING.");
var replycode=new Array(
1,3,4,2,6,4,6,4,10,4,14,3,17,3,20,2,22,3,25,3,28,4,28,4,32,3,35,5,40,9,40,9,40,9,
40,9,40,9,40,9,49,2,51,4,55,4,59,4,63,1,63,1,64,5,69,5,74,2,76,4,80,3,83,7,90,3,93,6,99,7,106,6);
//READ replycodeS[X],L:replycodeR[X]=replycodeS[X]:replycodeN[X]=replycodeS[X]+L-1
var replycodeS=new Array();
var replycodeR=new Array();
var replycodeN=new Array();

function Start()
{
	//unitySpeak = GameObject.Find("UnitySpeak").GetComponent("UnitySpeak");
	//unitySpeak.Say("Hi");
	for (j=0;j<replycode.length;j+=2) {
		var s=replycode[j];
		var l=replycode[j+1];
		//var k=Math.floor(j/2);
		var k=Mathf.Floor(j/2);
		replycodeS[k+1]=s;
		replycodeR[k+1]=s;
		replycodeN[k+1]=s+l-1;
	}
}

function doeliza(msg : String) {
	// convert to upper case
	msg=msg.ToUpper();//.toUpperCase();
	// strip punctuation
	msg=str_replace(msg,','," ");
	msg=str_replace(msg,'.'," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	if (msg==lastcommand) {
		reply("PLEASE DON'T REPEAT YOURSELF!");
		return;
	}
	lastcommand=msg;
	msg="  "+msg+"  ";

	// find msg in the keywords array
	keyword = findKeyWord(msg);
	var kw : String = keywords[keyword];
    //msg=msg.substr(msg.indexOf(kw)+kw.length);
    msg.Substring(msg.IndexOf(kw)+kw.length);//=msg.substr(msg.indexOf(kw)+kw.length);
	
	msg=replaceWords(msg);
	var replyMsg : String = replies[ replycodeR [ keyword ] ];
	

	// now scramble the keywords a little
	replycodeR[keyword]=replycodeR[keyword]+1;
	if (replycodeR[keyword]>replycodeN[keyword]) {
		replycodeR[keyword]=replycodeS[keyword];
	}
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	msg=str_replace(msg,'  '," ");
	//msg.Replace('  '," ");
	if (msg==" ") {
		reply("YOU WILL HAVE TO ELABORATE MORE FOR ME TO HELP YOU");
		return;
	}

	if (replyMsg.IndexOf('*')>0) {
		//replyMsg=replyMsg.substr(0,replyMsg.indexOf("*")) +" "+msg;
		replyMsg = replyMsg.Substring(0,replyMsg.IndexOf("*")) +" "+msg;
		reply(replyMsg);
	} else {
		reply(replyMsg);
	}
	
	return false;

}

function str_replace(haystack : String, needle : String, replacement:String) {
    var temp : String = haystack;// = haystack.Split(needle);
    return temp.Replace(needle, replacement);
    //return temp.join(replacement);
}

function reply(s:String) {
//	if ( unitySpeak != null )
//	{
//		//unitySpeak.Speak( s );
//	}
	s=s.ToLower();
	PlayerPrefs.SetString( "messageReceived", s );
//	var id=document.getElementById("elresp");
//	id.innerHTML=s+"<br/>"+id.innerHTML;
	//document.elizafrm.eliza.value=s+"\r\n"+document.elizafrm.eliza.value;
	message = s+"\r\n"+message;
}
function findKeyWord(k:String) {
	k=k+" ";
	for (j=0;j<keywords.length;j++) {
		if (k.IndexOf(keywords[j])>=0) {
			//found string 
			return j;
		}
	}
	return 36;
}
function replaceWords(msg : String) {
	// the input words in have to be replaced in viewpoint
	for (j=0;j<wordin.length;j++) {
		//k=msg.indexOf(wordin[j]);
		k=msg.IndexOf(wordin[j]);
		if (k>=0) {
			//alert(msg+" - "+wordout[j]+","+wordin[j] );
			//msg=msg.substr(0,k)+wordout[j]+msg.substr(k+wordin[j].length);
//			msg=str_replace(msg,wordin[j],wordout[j]);
			msg=str_replace(msg,wordin[j],"");

		}
	}
	return msg;
}

