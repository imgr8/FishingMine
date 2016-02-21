using UnityEngine;
using System.Collections;

public interface IFisher {

	void StartCatchFish ();
	void StopCatchFish();

	ISpinning Spinning {
		get;
		set;
	}

	IBoat Boat {
		get;
		set;
	}

	float Power {
		get;
		set;
	}

	void ClearState();	// Установить состояние рыбака в начальное, например, чтобы убрать влияние временных бонусов в начале уровня
		
}
