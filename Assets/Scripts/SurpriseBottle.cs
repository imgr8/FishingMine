using UnityEngine;
using System;
using System.Collections;

public class SurpriseBottle : MonoBehaviour, ICatchable, ISaveFromEditor {
	public int price = 0;
	public float weight = 1.0f;
	public float speed = 1.0f;
	public string name_ = "SurpriseBottle";

	public string Name {
		get {
			return this.name_;		
		}
	}

	public int Price {
		get {
			return this.price;
		}
	}

	public float Weight {
		get {
			return this.weight;
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

        int randomizer = UnityEngine.Random.Range(0, 4);
        ICatchable bonus;
        switch(randomizer)
        {
            case 0: bonus = new SlowSpeedBonus(sea, 0.5f); break;
            case 1: bonus = new PowerBonus(); break;
            case 2: bonus = new FortuneBonus(sea,1.5f); break;
            case 3: bonus = new DelayBonus(25); break;
            default: bonus = null; break;
        }

        bonus.Use(fisher);
		

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

	public string defaultAction = "";

	public string DefaultAction {
		get {
			return this.defaultAction;
		}
	}

	public void SetAction(string actionName, object data = null) {
		this.behaviour = BehaviourCreator.CreateBehaviour (actionName, this, data);
		if (this.behaviour != null) {
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

	public void ChangeSpeed (float ratio = 1.0f) {
		if (ratio < 0) {
			ratio = 1.0f;
		}

		this.speed *= ratio;
	}

	Action fishBehaviour;

	void EmptyBehaviour () {
		// Пустое поведение
	}

	void Start() {
		this.SetAction (this.DefaultAction);
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

	public void Load(ISea sea, string param = "") {
		this.Sea = sea;
		//this.SetAction(this.DefaultAction);	// Поскольку море не знает об объекте, устанавливаем поведение по-умолчанию сами, в последствии море уже будет само контролировать поведение
	}

	public string Save() {
		return "";
	}
}
