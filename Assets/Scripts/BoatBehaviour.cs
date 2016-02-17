using UnityEngine;
using System.Collections;

public class BoatBehaviour : MonoBehaviour {
	public bool waves = false;
	public float verticalWaveStrength = 0.5f;
	public float horizontalWaveStrength = 0.5f;
	Vector3 boatCenter;

	// Use this for initialization
	void Start () {
		this.boatCenter = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.waves) {
			return;
		}

		this.transform.position = new Vector3 
		(
			this.boatCenter.x + Mathf.Cos(Time.time * this.horizontalWaveStrength), 
			this.boatCenter.y + Mathf.Sin(Time.time * this.verticalWaveStrength), 
			this.boatCenter.z
		);
	}
}
