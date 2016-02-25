using UnityEngine;
using System.Collections;

public static class LevelLogicLoader {

	public static ILevel Load(string name) {
		name = name.Trim ();
		switch (name) {
		case "MyLevel_Bonus_1":
			return new MyLevel_Bonus_1 ();
		case "MyLevel_OnMoney_1":
			return new MyLevel_OnMoney_1 ();
		case "MyLevel_OnDestroy_1":
			return new MyLevel_OnDestroy_1 ();
		case "MyLevel_OnCertainCatch_1":
			return new MyLevel_OnCertainCatch_1 ();
		default:
			return null;
		}
	}

}
