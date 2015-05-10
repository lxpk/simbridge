// ----- DisableScript.cs ----- 
// Adds context menu "Compile Script Settings" in order to
// disable (or enable) all three kinds of Unity scripts from
// inside the project view
// ----------------------------
// V 1.1 (c) 2013, 2014 Standardverlag, written by G. Mattner
// ----------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class Disable : Editor
{
		private static string first;
		private static string last;
		private static string file;
		private static string type;
		private static Object obj;
		private static List<string> lines ;
	
		[MenuItem("Assets/Compile Script Settings/Enable Script")]
		public static void EnableScript ()
		{
			foreach(Object o in Selection.objects)
			{
				obj = o;

				if (!ReadIn ())
						continue;
		
				if (lines.IndexOf (first) == 0)
						lines.RemoveAt (0);
				int ll = lines.IndexOf (last);
				if (ll > 0)
						lines.RemoveAt (ll);
		
				WriteOut ();
			}

		}
	
		[MenuItem("Assets/Compile Script Settings/Disable Script")]
		public static void DisableScript ()
		{
			foreach(Object o in Selection.objects)
			{
				obj = o;

				if (!ReadIn ())
						continue;
				
				if (lines.IndexOf (first) == 0)
						continue;		
			
				lines.Insert (0, first);
				lines.Add (last);
		
				WriteOut ();
			}
			
		//Undo.UndoRedoCallback += Refr;

		}
	
		private static void WriteOut ()
		{
				try {
						FileUtil.DeleteFileOrDirectory (file);
						
						System.IO.StreamWriter fw = new System.IO.StreamWriter (file);
						foreach (string l in lines) {
								fw.WriteLine (l);	
						}
						fw.Flush ();
						fw.Close ();

							
				//AssetDatabase.Refresh ();	
				//EditorUtility.SetDirty(obj);		
				AssetDatabase.ImportAsset (file, ImportAssetOptions.ForceUpdate);

				} catch (IOException e) {
						Debug.LogError ("An IO Exception Occurred: " + e);
				}
			
		}
	
		private static bool ReadIn ()
		{
			try {
				file = AssetDatabase.GetAssetPath (obj); //Selection.activeObject);
								type = System.IO.Path.GetExtension (file);
		
								if (System.IO.Path.GetFileNameWithoutExtension (file) == "DisableScript") 
								{
									return false; 
								}
		
			//AssetDatabase.ImportAsset(file, ImportAssetOptions.ForceUpdate);

								first = string.Empty;
								last = string.Empty;
		
								switch (type) {
			
								case ".cs":
								case ".js":
										first = "#if false // This script is temporarily disabled, enable in project window";
										last = "#endif // Currently disabled script";
										break;
								case ".boo":
										first = "/* This script is temporarily disabled, enable in project window";
										last = "Currently disabled script */";
										break;
								default:
										Debug.Log ("No script! Please select a CS, JS, or BOO file to enable/disable.");
										break;
								}
		
								if (first.Length == 0) {
										return false;
								}
		
								lines = new List<string> ();

								string line;
		
								System.IO.StreamReader fs = new System.IO.StreamReader (file);
								while ((line = fs.ReadLine()) != null) {
										lines.Add (line);		
								}	
								fs.Close ();
		
						} catch (IOException e) {
								Debug.LogError ("An IO Exception Occurred: " + e);
								return false;
						}
				

				return true;		
		}
	
		[MenuItem("Assets/Compile Script Settings/Enable Script", true)]
		[MenuItem("Assets/Compile Script Settings/Disable Script", true)]
		static bool ValidateLogSelectedScript ()
		{
				return (Selection.activeObject.GetType () == typeof(UnityEditor.MonoScript)); //MonoScript );
		}

		public static void Refr()
	{
		AssetDatabase.Refresh();

	}

}
