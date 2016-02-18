using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour, ICatchable {
	public float weight = 1.0f;

	public float Weight {
		get {
			return this.weight;
		}
	}

	public float Price {
		get {
			return 100;
		}
	}

	public GameObject GameObject {
		get {
			return this.gameObject;
		}
	}
}
