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

		SlowSpeedBonus slowSpeedBonus = new SlowSpeedBonus (this.sea, 0.5f);
		slowSpeedBonus.Use (fisher);

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
		//this.StopAction (); // Остановим действие, которое выполнял объект
		return this;
	}

	public void Destroy() {
		if (this.sea != null) {
			this.sea.DestroyObject (this);
		} else {
			GameObject.Destroy (this.GameObject);
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

	void Start() {
		
	}

	void Update() {

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
