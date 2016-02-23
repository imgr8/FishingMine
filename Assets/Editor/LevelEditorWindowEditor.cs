using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelEditorWindowEditor : Editor {
	[MenuItem ("Window/LevelEditor")]

	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(LevelEditorWindow));
	}

	void OnGUI () {
		// The actual window code goes here
	}

}
