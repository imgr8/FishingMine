using UnityEngine;
using System.Collections;

public interface IFisher {
	void StartCatchFish ();

	float Power {
		get;
		set;
	}
}
