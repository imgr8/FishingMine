using UnityEngine;
using System.Collections;

public interface ILevelLoader {

	ILevel LoadLevel (string path, int? levelDifficulty = null);

}
