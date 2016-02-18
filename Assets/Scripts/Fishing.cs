using UnityEngine;
using System.Collections;

public class Fishing : MonoBehaviour {
	public GameObject fisherGameObject;

	IFisher fisher;

	// Use this for initialization
	void Start () {
		this.fisher = this.fisherGameObject.GetComponent<IFisher> ();

		if (this.fisher == null) {
			throw new UnityException ("Fisher are not initialized");
		}

		this.fisher.StartCatchFish ();
	}

}
