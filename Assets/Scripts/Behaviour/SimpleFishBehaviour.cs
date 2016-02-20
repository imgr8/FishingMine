using UnityEngine;
using System.Collections;

// Простое поведение рыбы
public class SimpleFishBehaviour : IBehaviour {
	ICatchable catchable;
	ISea sea;
	Transform obj;
	float speed;

	enum MoveDirection {Right, Left};
	MoveDirection moveDirection = MoveDirection.Right;

	public SimpleFishBehaviour(ICatchable obj) {
		this.catchable = obj;
		this.obj = obj.GameObject.transform;
		this.sea = obj.Sea;
		this.speed = obj.GameObject.GetComponent<Fish>().speed;

		this.moveDirection = (UnityEngine.Random.Range (0, 2) == 1 ? MoveDirection.Right : MoveDirection.Left);

		if (this.moveDirection == MoveDirection.Left) {
			this.obj.transform.Rotate (new Vector3 (0, 180, 0));
		}
	}

	public void Action() {
		if (this.moveDirection == MoveDirection.Left && this.obj.position.x > -this.sea.Width / 2) {
			obj.position += Vector3.left * Time.deltaTime * this.speed;
		} else if (this.moveDirection == MoveDirection.Left && this.obj.position.x <= -this.sea.Width / 2) {
			this.moveDirection = MoveDirection.Right;
			this.obj.transform.Rotate (new Vector3 (0, 180, 0));
		} else if (this.moveDirection == MoveDirection.Right && this.obj.position.x < this.sea.Width / 2) {
			obj.position += Vector3.right * Time.deltaTime * this.speed;
		} else if (this.moveDirection == MoveDirection.Right && this.obj.position.x >= this.sea.Width / 2) {
			this.moveDirection = MoveDirection.Left;
			this.obj.transform.Rotate (new Vector3 (0, 180, 0));
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

// Простое поведение рыбы
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
		} else if (this.moveDirection == MoveDirection.Down && this.obj.position.y > this.sea.Center.y - this.sea.Depth / 2) {
			obj.position += Vector3.down * Time.deltaTime * this.speed;
		} else if (this.moveDirection == MoveDirection.Down && this.obj.position.y <= this.sea.Center.y - this.sea.Depth / 2) {
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