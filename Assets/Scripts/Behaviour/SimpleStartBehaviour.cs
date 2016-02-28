using UnityEngine;
using System.Collections;

// Простое поведение звезды
public class SimpleStarBehaviour : IBehaviour {
	ICatchable catchable;
	ISea sea;
	Transform obj;
	float speed;

	enum MoveDirection {Up, Down};
	MoveDirection moveDirection = MoveDirection.Up;

	public SimpleStarBehaviour(ICatchable obj) {
		this.catchable = obj;
		this.obj = obj.GameObject.transform;
		this.sea = obj.Sea;
		this.speed = obj.GameObject.GetComponent<Fish>().speed;

		this.moveDirection = (UnityEngine.Random.Range (0, 2) == 1 ? MoveDirection.Up : MoveDirection.Down);

	}

	public void Action() {
		if (this.moveDirection == MoveDirection.Up && this.obj.position.y < this.sea.Center.y) {
			obj.position += Vector3.up * Time.deltaTime * this.speed;
		} else if (this.moveDirection == MoveDirection.Up && this.obj.position.y >= this.sea.Center.y) {
			this.moveDirection = MoveDirection.Down;
		} else if (this.moveDirection == MoveDirection.Down && this.obj.position.y > this.sea.Center.y - this.sea.Depth / 2 + 0.2f) {
			obj.position += Vector3.down * Time.deltaTime * this.speed;
		} else if (this.moveDirection == MoveDirection.Down && this.obj.position.y <= this.sea.Center.y - this.sea.Depth / 2 + 0.2f) {
			this.moveDirection = MoveDirection.Up;
		}

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