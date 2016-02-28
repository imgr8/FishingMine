using UnityEngine;
using System.Collections;

public class SimpleVerticalMove : MonoBehaviour, IMove {

	public MoveDirection moveDirection = MoveDirection.Up;
	public InitialLook initLook = InitialLook.Right;

	ISea sea;

	public void Init (ISea sea) {
		this.sea = sea;

		if (this.moveDirection == MoveDirection.Random) {
			this.moveDirection = (Random.Range (0, 2) == 0) ? MoveDirection.Up : MoveDirection.Down;
		}

		if (this.initLook == InitialLook.Up && this.moveDirection == MoveDirection.Down ||
			this.initLook == InitialLook.Down && this.moveDirection == MoveDirection.Up) {
			this.transform.Rotate(new Vector3(0, 0, 180));	
		}

	}

	public float Speed {
		set {
			this.verticalSpeed = value;
		}
	}

	float horizontalSpeed = 0.0f;

	public float HorizontalSpeed {
		get {
			return this.horizontalSpeed;
		}

		set {
			
		}
	}

	public float verticalSpeed = 0.0f;

	public float VerticalSpeed {
		get {
			return this.verticalSpeed;	
		}

		set {
			this.verticalSpeed = value;
		}
	}

	public void Move() {
		if (this.moveDirection == MoveDirection.Up && this.transform.position.y < this.sea.Center.y) {
			this.transform.position += Vector3.up * Time.deltaTime * this.verticalSpeed;
		} else if (this.moveDirection == MoveDirection.Up && this.transform.position.y >= this.sea.Center.y) {
			this.moveDirection = MoveDirection.Down;
		} else if (this.moveDirection == MoveDirection.Down && this.transform.position.y > this.sea.Center.y - this.sea.Depth / 2) {
			this.transform.position += Vector3.down * Time.deltaTime * this.verticalSpeed;
		} else if (this.moveDirection == MoveDirection.Down && this.transform.position.y <= this.sea.Center.y - this.sea.Depth / 2) {
			this.moveDirection = MoveDirection.Up;
		}
	}

	float savedHorizontalSpeed = 0.0f;

	public void StopMove() {
		this.savedHorizontalSpeed = this.horizontalSpeed;
		this.Speed = 0;
	}

	public void ResumeMove() {
		this.horizontalSpeed = this.savedHorizontalSpeed;
	}
}
