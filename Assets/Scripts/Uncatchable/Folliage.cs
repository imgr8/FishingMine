using UnityEngine;
using System.Collections;

public class Folliage : MonoBehaviour, IUncatchable, ISaveFromEditor {

	ISea sea;

	public ISea Sea {
		set {
			this.sea = value;
		} 

		get {
			return this.sea;
		}
	}

	public GameObject GameObject {
		get {
			return this.gameObject;
		}
	}

	public void Destroy() {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}

	public string Save() {
		return "";
	}

	public void Load(ISea sea, string param = "") {
		this.Sea = sea;
	}
}
