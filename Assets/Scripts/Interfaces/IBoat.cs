using UnityEngine;
using System.Collections;
using System;

public interface IBoat {
	float Capacity {
		get;
	}

	void PutStaff(ICatchable staff);

	ISea Sea {
		get;
		set;
	}

	event Action OnOverload;
}
