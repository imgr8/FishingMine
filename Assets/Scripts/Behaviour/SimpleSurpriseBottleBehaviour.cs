using UnityEngine;
using System.Collections;

// Простое поведение бутылки с сюрпризом
public class SimpleSurpriseBottleBehaviour : IBehaviour {
	ICatchable catchable;
	ISea sea;
	Transform obj;
	float speed;

	enum MoveDirection {Right, Left};
	MoveDirection moveDirection = MoveDirection.Right;

	public SimpleSurpriseBottleBehaviour(ICatchable obj) {
		this.catchable = obj;
		this.obj = obj.GameObject.transform;
		this.sea = obj.Sea;
		this.speed = obj.GameObject.GetComponent<SurpriseBottle>().speed;

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