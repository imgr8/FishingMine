using UnityEngine;
using System.Collections;

public static class LevelLogicLoader {

	public static ILevel Load(string name) {
		switch (name) {
		case "SimpleLevel":
			return new SimpleLevel ();
		default:
			return null;
		}
	}

}
