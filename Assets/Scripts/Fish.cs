using UnityEngine;
using System;
using System.Collections;

public class Fish : MonoBehaviour, ICatchable, IMovable, ISaveFromEditor {
	public float weight = 1.0f;
	public int price = 100;

	public string objectName = "Fish";

	public string Name {
		get {
			return this.objectName;
		}
	}

	public float Weight {
		get {
			return this.weight;
		}
	}

	public int Price {
		get {
			return this.price;
		}
	}

	public GameObject GameObject {
		get {
			return this.gameObject;
		}
	}

	public void ChangeWeight (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		this.weight *= ratio;	
	}

	public void ChangePrice (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		int tmp = (int)(this.price * ratio);

		this.price = tmp;	
	}

	ISea sea;

	public ISea Sea {
		set {
			this.sea = value;
		} 

		get {
			return this.sea;
		}
	}

	public void Use(IFisher fisher) {
		fisher.Boat.PutStaff (this);

		if (this.onUsed != null) {
			this.onUsed.Invoke (this);
		}
	}

	event Action<ICatchable> onUsed;

	public event Action<ICatchable> OnUsed {
		add {
			this.onUsed += value;
		}

		remove {
			this.onUsed -= value;
		}
	}

	public ICatchable WhenCatched(IHook hook) {
		this.MoveBehaviour.StopMove();
		return this;
	}

	public void Destroy() {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
		}
	}

	IMove moveBehaviour;

	public IMove MoveBehaviour {
		get {
			return this.moveBehaviour;
		}

		set {
			this.moveBehaviour = value;
		}
	}

	void Start() {
		this.moveBehaviour = this.GetComponent<IMove> ();
		this.moveBehaviour.Init (this.sea);
	}

	void Update() {
		this.moveBehaviour.Move ();
	}

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}
		
	public string Save() {
		
		string parameters = "";
		/*
		short initParam = 0;	// None by default

		if (this.initialLook == InitialLook.Left) {
			initParam = 1;
		} else {
			initParam = 2;
		}

		parameters = initParam.ToString () + "@" +
		this.Weight.ToString () + "@" +
		this.Price.ToString () + "@" +
		this.speed.ToString ();
*/
		return parameters;
	}

	public void Load(ISea sea, string param) {
		/*
		this.Sea = sea;

		string [] parameters = param.Split (new char [] { '@' });

		int initLook = int.Parse (parameters [0]);

		if (initLook == 0) {
			this.initialLook = InitialLook.None;
		} else if (initLook == 1) {
			this.initialLook = InitialLook.Left;
		} else {
			this.initialLook = InitialLook.Right;
		}

		this.weight = float.Parse (parameters [1], System.Globalization.NumberStyles.Any);
		this.price = int.Parse (parameters [2]);
		this.speed = float.Parse (parameters[3], System.Globalization.NumberStyles.Any);
		*/
	}

}
