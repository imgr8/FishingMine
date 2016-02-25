using UnityEngine;
using System.Collections;

// Простое поведение рыбы
public class SimpleFishBehaviour : IBehaviour {
	ICatchable catchable;
	ISea sea;
	Transform obj;
	float speed;
    float deviation;
    float startHorizontalPosition;

	enum MoveDirection {Right, Left};
	MoveDirection moveDirection = MoveDirection.Right;

	public SimpleFishBehaviour(ICatchable obj) {
		this.catchable = obj;
		this.obj = obj.GameObject.transform;
		this.sea = obj.Sea;
		//this.speed = obj.GameObject.GetComponent<Fish>().speed;

	
	}

	public void Action() {

	}

	float savedSpeed;

	public void Stop() {
		this.savedSpeed = this.speed;
		this.speed = 0;
	}

	public void Resume() {
		this.speed = this.savedSpeed;
	}

	public void Change(object data = null) {
		//this.speed = speed;
	}
}

