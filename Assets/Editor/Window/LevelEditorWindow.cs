using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

public class LevelEditorWindow : EditorWindow
{
	string fileSaveName = "levelGen_1";
	string levelLogicName = "SimpleLevel";
	string folderToSave = "Assets/Resources/Levels";	// Обязательно папка Assets/Resources!
	string levelDifficult = "none";
	bool groupEnabled;
	bool myBool = true;
	float myFloat = 1.23f;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/Level Editor")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LevelEditorWindow));
	}

	void OnGUI()
	{
		GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
		this.fileSaveName = EditorGUILayout.TextField ("Level name", this.fileSaveName);
		this.levelLogicName = EditorGUILayout.TextField ("Level Logic", this.levelLogicName);
		this.levelDifficult = EditorGUILayout.TextField ("Level difficulty", this.levelDifficult);

		if (GUILayout.Button ("Save", GUIStyle.none)) {
			UnityEngine.Object[] objects = UnityEngine.Object.FindObjectsOfType(typeof(UnityEngine.Object));	//Resources.FindObjectsOfTypeAll (typeof(MonoScript));

			string directory = this.levelDifficult.ToString();

			if (!System.IO.Directory.Exists (this.folderToSave + "/" + directory)) {
				System.IO.Directory.CreateDirectory (this.folderToSave + "/" + directory);
			}

			string level = "";

			level += this.levelLogicName + "\n";

			for (int i = 0; i < objects.Length; i++) {
				ISaveFromEditor saveObject = objects [i] as ISaveFromEditor;
				if (saveObject != null) {
					level += saveObject.Path + "|" + 
						saveObject.GameObject.transform.position.x.ToString() + ":" + 
						saveObject.GameObject.transform.position.y.ToString() + ":" +
						saveObject.GameObject.transform.position.z.ToString() + "|" + 

						saveObject.GameObject.transform.rotation.eulerAngles.x.ToString() + ":" + 
						saveObject.GameObject.transform.rotation.eulerAngles.y.ToString() + ":" +
						saveObject.GameObject.transform.rotation.eulerAngles.z.ToString() + "|" + 

						saveObject.Save() + "\n";
				}
			}

			this.SaveLevelToFile (this.folderToSave + "/" + directory + "/" + this.fileSaveName + ".txt", level);

			Debug.Log ("Level have saved");
		}

		if (GUILayout.Button ("Load", GUIStyle.none)) {
			this.Clear ();

			string [] levelLines = System.IO.File.ReadAllLines (this.folderToSave + "/" + this.levelDifficult.ToString() + "/" + this.fileSaveName + ".txt");

			for (int i = 1; i < levelLines.Length; i++) {
				string line = levelLines [i].Trim ();

				if (line != "") {
					string [] splitLine = line.Split (new char []{'|'});
					GameObject newGameObject = GameObject.Instantiate (Resources.Load(splitLine[0]) as GameObject);

					string [] coordinates = splitLine [1].Split (new char []{':'});

					Vector3 newPosition = new Vector3 (float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));

					newGameObject.transform.position = newPosition;

					string [] euler = splitLine [2].Split (new char []{':'});

					Vector3 newEulerAngle = new Vector3 (float.Parse(euler[0]), float.Parse(euler[1]), float.Parse(euler[2]));

					newGameObject.transform.Rotate(newEulerAngle);

					newGameObject.GetComponent<ISaveFromEditor> ().Load (null, splitLine [3]);
				}
			}

			Debug.Log ("Level have loaded");
		}

		if (GUILayout.Button ("Clear", GUIStyle.none)) {
			this.Clear ();
		}

	}

	void Clear() {
		UnityEngine.Object[] objects = UnityEngine.Object.FindObjectsOfType(typeof(UnityEngine.Object));	

		for (int i = 0; i < objects.Length; i++) {
			ISaveFromEditor saveObject = objects [i] as ISaveFromEditor;

			if (saveObject != null) {
				GameObject.DestroyImmediate (saveObject.GameObject);
			}
		}

		Debug.Log ("Level have cleared");
	}

	void SaveLevelToFile(string path, string level) {
		//System.IO.File.Create (path);
		System.IO.File.WriteAllText (path, level);
	}
}
