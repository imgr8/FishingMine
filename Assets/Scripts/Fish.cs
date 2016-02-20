using UnityEngine;
using System;
using System.Collections;

public class Fish : MonoBehaviour, ICatchable {
	public float weight = 1.0f;
	public float speed = 1.0f;

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

	ISea sea;

	public ISea Sea {
		set {
			this.sea = value;
		} 

		get {
			return this.sea;
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
}
