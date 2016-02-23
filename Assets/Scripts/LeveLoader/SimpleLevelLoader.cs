using UnityEngine;
using System.Collections;

public class SimpleLevelLoader : ILevelLoader {
	ISea sea;
	string folderPath = "Levels";

	public SimpleLevelLoader(ISea sea) {
		this.sea = sea;
	}

	public void LoadLevel (string levelName) {
		Object obj = Resources.Load (this.folderPath + "/" + levelName);
		Debug.Log (this.folderPath + "/" + levelName);
		TextAsset level = Resources.Load (this.folderPath + "/" + levelName) as TextAsset;
		string[] levelLines = level.text.Split (new char [] { '\n' });

		for (int i = 0; i < levelLines.Length; i++) {
			string line = levelLines [i].Trim ();

			if (line != "") {
				string[] splitLine = line.Split (new char []{ '|' });
				GameObject newGameObject = GameObject.Instantiate (Resources.Load (splitLine [0]) as GameObject);

				string[] coordinates = splitLine [1].Split (new char []{ ':' });

				Vector3 newPosition = new Vector3 (float.Parse (coordinates [0]), float.Parse (coordinates [1]), float.Parse (coordinates [2]));

				newGameObject.transform.position = newPosition;

				newGameObject.GetComponent<ISaveFromEditor> ().Load (sea);

				this.sea.AddObject (newGameObject);
			}
		}
	}
}
