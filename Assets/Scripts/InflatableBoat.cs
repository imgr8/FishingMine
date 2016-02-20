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
		
	ISea sea;

	public ISea Sea {
		get {
			return this.sea;
		}

		set {
			this.sea = value;
			this.horizontalWaveStrength = this.sea.FlowHorizontal;
			this.verticalWaveStrength = this.sea.FlowVertical;
		}
	}

	Vector3 boatCenter;

	float horizontalWaveStrength = 0.0f;
	float verticalWaveStrength = 0.0f;

	// Use this for initialization
	void Start () {
		this.boatCenter = this.transform.position;
	}

	// Update is called once per frame
	void Update () {

		this.transform.position = new Vector3 
			(
				this.boatCenter.x + Mathf.Cos(Time.time) * this.horizontalWaveStrength, 
				this.boatCenter.y + Mathf.Sin(Time.time) * this.verticalWaveStrength, 
				this.boatCenter.z
			);
	}
}
