using UnityEngine;
using System.Collections;

public enum MoveDirection {Right, Left, Up, Down, Random};
public enum InitialLook { Right, Left, Up, Down, None }

public class SimpleHorizontalMove : MonoBehaviour, IMove {

	public MoveDirection moveDirection = MoveDirection.Right;
	public InitialLook initLook = InitialLook.Right;

	ISea sea;

	public void Init (ISea sea) {
		this.sea = sea;

		if (this.moveDirection == MoveDirection.Random) {
			this.moveDirection = (Random.Range (0, 2) == 0) ? MoveDirection.Left : MoveDirection.Right;
		}

		if (this.initLook == InitialLook.Right && this.moveDirection == MoveDirection.Left ||
			this.initLook == InitialLook.Left && this.moveDirection == MoveDirection.Right) {
			this.transform.Rotate(new Vector3(0, 180, 0));	
		}

	}
		
	public float Speed {
		set {
			this.horizontalSpeed = value;
		}
	}

	public float horizontalSpeed = 0.0f;

	public float HorizontalSpeed {
		get {
			return this.horizontalSpeed;
		}

		set {
			this.horizontalSpeed = value;
		}
	}

	float verticalSpeed = 0.0f;

	public float VerticalSpeed {
		get {
			return this.verticalSpeed;	
		}

		set {
			
		}
	}

	public void Move() {
		if (this.moveDirection == MoveDirection.Left && this.transform.position.x > -this.sea.Width / 2)
		{
			this.transform.position += Vector3.left * Time.deltaTime * this.horizontalSpeed;
		}
		else if (this.moveDirection == MoveDirection.Left && this.transform.position.x <= -this.sea.Width / 2)
		{
			this.moveDirection = MoveDirection.Right;
			this.transform.Rotate(new Vector3(0, 180, 0));
		}
		else if (this.moveDirection == MoveDirection.Right && this.transform.position.x < this.sea.Width / 2)
		{
			this.transform.position += Vector3.right * Time.deltaTime * this.horizontalSpeed;
		}
		else if (this.moveDirection == MoveDirection.Right && this.transform.position.x >= this.sea.Width / 2)
		{
			this.moveDirection = MoveDirection.Left;
			this.transform.Rotate(new Vector3(0, 180, 0));
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
