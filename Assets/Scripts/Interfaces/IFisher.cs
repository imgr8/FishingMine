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
		
}
