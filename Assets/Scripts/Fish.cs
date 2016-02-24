using UnityEngine;
using System;
using System.Collections;

public enum InitialLook { Right, Left, None };

public class Fish : MonoBehaviour, ICatchable, ISaveFromEditor {
	public float weight = 1.0f;
	public float speed = 1.0f;
	public int price = 100;
    public float deviation;
    public bool isDeviation;
	public InitialLook initialLook = InitialLook.None;

	public InitialLook InitialLook {
		get {
			return this.initialLook;
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
		this.StopAction (); // Остановим действие, которое выполнял объект
		return this;
	}

	public void Destroy() {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
		}
	}

	IBehaviour behaviour;

	public string defaultAction = "SimpleFishBehaviour";

	public string DefaultAction {
		get {
			return this.defaultAction;
		}
	}

	public void SetAction(string actionName, object data = null) {
		this.behaviour = BehaviourCreator.CreateBehaviour (actionName, this, data);
		if (behaviour != null) {
			this.fishBehaviour = this.behaviour.Action;
		} else {
			this.fishBehaviour = this.EmptyBehaviour;
		}
	}
		
	public void ChangeAction (object data) {
		if (this.behaviour != null) {
			this.behaviour.Change(data);
		}
	}

	public void StopAction () {
		if (this.behaviour != null) {
			this.behaviour.Stop ();
		}
	}

	public void ResumeAction() {
		if (this.behaviour != null) {
			this.behaviour.Resume ();
		}
	}
		
	Action fishBehaviour;

	void EmptyBehaviour () {
		// Пустое поведение
	}

	void Start() {
		//this.fishBehaviour = this.EmptyBehaviour;
		this.SetAction(this.defaultAction);
	}

	void Update() {
		fishBehaviour.Invoke ();	
	}

	public string path;

	public string Path {
		get {
			return this.path;
		}
	}
		
	public string Save() {
		string parameters = "";

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

		return parameters;
	}

	public void Load(ISea sea, string param) {
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
	}

}
