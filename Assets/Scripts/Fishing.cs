using UnityEngine;
using System.Collections;

public class Fishing : MonoBehaviour {
	public GameObject fisherGameObject;
	IFisher fisher;

	public GameObject seaGameObject;
	ISea sea;

	// Use this for initialization
	void Start () {
		this.sea = this.seaGameObject.GetComponent<ISea> ();

		if (this.sea == null) {
			throw new UnityException ("Sea are not initialized");
		}

		this.sea.MakeLive ();

		this.fisher = this.fisherGameObject.GetComponent<IFisher> ();

		if (this.fisher == null) {
			throw new UnityException ("Fisher are not initialized");
		}

		this.fisher.Boat.Sea = this.sea;

		this.fisher.StartCatchFish ();
	}

}
