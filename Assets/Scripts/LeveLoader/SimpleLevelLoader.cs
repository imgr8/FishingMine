using UnityEngine;
using System.Collections;

public class SimpleLevelLoader : ILevelLoader {
	ISea sea;
	string folderPath = "Levels";

	public SimpleLevelLoader(ISea sea) {
		this.sea = sea;
	}

	public ILevel LoadLevel (string levelName, int? levelDifficulty = null) {
		string difDir = (levelDifficulty == null) ? "none" : levelDifficulty.ToString();

		TextAsset level = Resources.Load (this.folderPath + "/" + difDir + "/" + levelName) as TextAsset;
		string[] levelLines = level.text.Split (new char [] { '\n' });

		if (levelLines.Length < 2) {
			return null;
		}

		ILevel levelLogic = LevelLogicLoader.Load (levelLines [0]);

		for (int i = 1; i < levelLines.Length; i++) {
			string line = levelLines [i].Trim ();

			if (line != "") {
				string[] splitLine = line.Split (new char []{ '|' });
				GameObject newGameObject = GameObject.Instantiate (Resources.Load (splitLine [0]) as GameObject);

				string[] coordinates = splitLine [1].Split (new char []{ ':' });

				Vector3 newPosition = new Vector3 (float.Parse (coordinates [0]), float.Parse (coordinates [1]), float.Parse (coordinates [2]));

				newGameObject.transform.position = newPosition;

				string [] euler = splitLine [2].Split (new char []{':'});

				Vector3 newEulerAngle = new Vector3 (float.Parse(euler[0]), float.Parse(euler[1]), float.Parse(euler[2]));

				newGameObject.transform.Rotate(newEulerAngle);

				newGameObject.GetComponent<ISaveFromEditor> ().Load (this.sea, splitLine [3]);

				this.sea.AddObject (newGameObject);
			}
		}

		return levelLogic;
	}
}
