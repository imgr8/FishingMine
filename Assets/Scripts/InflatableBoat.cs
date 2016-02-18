using UnityEngine;
using System;
using System.Collections;

public class InflatableBoat : MonoBehaviour, IBoat {
	public float maxCapacity = 100;

	float capacity;

	public float Capacity {
		get {
			return this.capacity;
		}
	}

	public void PutStaff(ICatchable staff) {
		this.capacity += staff.Weight;

		if (this.capacity > this.maxCapacity) {
			if (this.onOverload != null) {
				this.onOverload.Invoke ();
			}
		}
	}

	event Action onOverload;

	public event Action OnOverload {
		add {
			this.onOverload += value;
		}

		remove {
			this.onOverload -= value;
		}
	}
		
}
