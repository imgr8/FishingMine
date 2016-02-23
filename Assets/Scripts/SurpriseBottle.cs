﻿using UnityEngine;
using System;
using System.Collections;

public class SurpriseBottle : MonoBehaviour, ICatchable, ISaveFromEditor {
	public int price = 0;
	public float weight = 1.0f;
	public float speed = 1.0f;

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
		//fisher.Boat.PutStaff (this);
		PowerBonus powerBonus = new PowerBonus();
		powerBonus.Use (fisher);

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

	public void SetAction(string actionName, object data = null) {
		this.behaviour = BehaviourCreator.CreateBehaviour (actionName, this, data);
		this.fishBehaviour = this.behaviour.Action;
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

	void Awake() {
		this.fishBehaviour = this.EmptyBehaviour;
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

	public void Load(ISea sea) {
		this.Sea = sea;
		this.SetAction("SimpleSurpriseBottleBehaviour");	// Поскольку море не знает об объекте, устанавливаем поведение по-умолчанию сами, в последствии море уже будет само контролировать поведение
	}

}
